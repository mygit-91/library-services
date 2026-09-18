using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Commons.Constants;
using LibraryService.Entities;
using LibraryService.Entities.Data;
using LibraryService.Models;
using LibraryService.Models.Borrowings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.BusinessLogic
{
    public class BorrowingsLogic(AppDbContext context) : IBorrowingsLogic
    {
        private readonly AppDbContext _context = context;

        public async Task<ResponseModel> GetBorrowingListAsync(GetBorrowingsInputModel data)
        {
            ResponseModel response;

            try
            {
                // Check status
                string status = "Returned";
                if (data.IsBorrowList)
                {
                    status = "Borrowed";
                }
                
                // Create query
                IQueryable<Borrowings> borrowQuery = _context.Borrowings.Where(borrow => borrow.Status == status);
                IQueryable<Books> booksQuery = _context.Books;
                //IQueryable<Staff> staffQuery = _context.Staff;
                IQueryable<Members> memberQuery = _context.Members;
                IQueryable<Fines> finesQuery = _context.Fines;

                // Add filter condition to query
                string searchTopic = data.SearchTopic.ToLower();
                string searchText = data.SearchText.ToLower();

                if(data.IsStaff)
                {
                    // For staff search
                    if (searchTopic == "isbn")
                    {
                        booksQuery = booksQuery.Where(books => books.ISBN.Contains(searchText));
                    }
                    else if (searchTopic == "title")
                    {
                        booksQuery = booksQuery.Where(books => books.Title.Contains(searchText));
                    }
                    else if (searchTopic == "membercardid")
                    {
                        memberQuery = memberQuery.Where(member => member.ID_Card.Contains(searchText));
                    }
                    else
                    {
                        // Seclect all
                    }
                } else
                {
                    // For member search
                    memberQuery = memberQuery.Where(member => member.ID_Card == searchText);
                }

                // Join with Categories
                var today = DateOnly.FromDateTime(DateTime.Today);
                var joinedQuery = (from borrow in borrowQuery
                                   join fines in finesQuery
                                   on borrow.Borrow_Id equals fines.Borrow_Id
                                   join books in booksQuery
                                   on borrow.Book_Id equals books.Book_Id
                                   join member in memberQuery
                                   on borrow.Member_Id equals member.Member_Id
                                   
                                   select new
                                   {
                                       borrowId = borrow.Borrow_Id,
                                       borrowDate = borrow.Borrow_Date,
                                       dueDate = borrow.Due_Date,
                                       returnDate = borrow.Return_Date,
                                       bookId = books.Book_Id,
                                       author = books.Author,
                                       isbn = books.ISBN,
                                       bookTitle = books.Title,
                                       memberId = member.Member_Id,
                                       memberCardId = member.ID_Card,
                                       memberName = member.First_Name + " " + member.Last_Name,
                                       finesId = fines.Fine_Id,
                                       amount = fines.Amount,
                                       paymentStatus = fines.Payment_Status,
                                       paidDate = fines.Paid_Date,
                                       status = borrow.Status,
                                       isOverTime = borrow.Due_Date >= today ? false : true,
                                   });

                // Process query
                var result = await joinedQuery.ToListAsync();
                response = new ResponseModel(StatusCodes.Status200OK, AppMessage.SUCCESS, result);
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> AddBorrowingAsync(AddBorrowingsInputModel data)
        {
            ResponseModel response;

            try
            {
                //Check existing
                bool isExist = await _context.Books.AnyAsync(books => books.Book_Id == data.BookId && books.Available_Copies > 0);
                if (isExist)
                {
                    // Save borrow to database
                    Borrowings borrowings = new()
                    {
                        Book_Id = data.BookId,
                        Member_Id = data.MemberId,
                        Staff_Id = data.StaffId,
                        Borrow_Date = data.BorrowDate,
                        Due_Date = data.DueDate,
                        Status = "Borrowed"
                    };

                    _context.Borrowings.Add(borrowings);
                    await _context.SaveChangesAsync();

                    // Save fines to database
                    Fines fines = new()
                    {
                        Borrow_Id = borrowings.Borrow_Id,
                        Amount = 0,
                    };

                    _context.Fines.Add(fines);
                    await _context.SaveChangesAsync();

                    // Update book available
                    await _context.Books
                    .Where(books => books.Book_Id ==data.BookId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(books => books.Available_Copies, books => books.Available_Copies - 1));

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.INSERT_SUCCESS, borrowings);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.BOOK_NOT_AVAILABLE);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> ReturnBookAsync(ReturnBookInputModel data)
        {
            ResponseModel response;

            try
            {
                // Validate fines data
                bool isValid = true;
                if (data.IsOverTime)
                {
                    if(data.Amount == null || data.Amount <= 0)
                    {
                        isValid = false;
                    } else if (string.IsNullOrEmpty(data.PaymentStatus))
                    {
                        isValid = false;
                    } else if (data.PaymentStatus == null)
                    {
                        isValid = false;
                    } else
                    {
                        isValid = true;
                    }
                }

                if (isValid)
                {
                    // Update borrowing
                    await _context.Borrowings
                    .Where(borrowings => borrowings.Borrow_Id == data.BorrowId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(borrowings => borrowings.Return_Date, data.ReturnDate)
                        .SetProperty(borrowings => borrowings.Status, "Returned"));

                    // Update fines
                    if (data.IsOverTime)
                    {
                        await _context.Fines
                        .Where(fines => fines.Fine_Id == data.FinesId)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(fines => fines.Amount, data.Amount)
                            .SetProperty(fines => fines.Payment_Status, data.PaymentStatus)
                            .SetProperty(fines => fines.Paid_Date, data.PaidDate));
                    }

                    // Update book available
                    await _context.Books
                    .Where(books => books.Book_Id == data.BookId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(books => books.Available_Copies, books => books.Available_Copies + 1));

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.UPDATE_SUCCESS);
                } else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.FINES_IS_REQUIRED);
                }
                
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }
    }
}
