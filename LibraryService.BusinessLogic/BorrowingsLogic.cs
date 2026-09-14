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

        public async Task<ResponseModel> GetBorrowingAsync(GetBorrowingsInputModel data)
        {
            ResponseModel response;

            try
            {
                // Get member id
                Guid memberId = Guid.Empty;
                if (!string.IsNullOrEmpty(data.Member_Id_Card))
                {
                    var members = await _context.Members.Where(member => member.ID_Card == data.Member_Id_Card).ToListAsync();
                    memberId = members[0].Member_Id;
                }

                // Create query
                IQueryable<Borrowings> query = _context.Borrowings;

                // Check condition
                if (data.Is_Staff)
                {
                    if (data.Staff_Id != Guid.Empty && data.Staff_Id != null)
                    {
                        query = query.Where(borrowings => borrowings.Staff_Id == data.Staff_Id);
                    }

                    if (memberId != Guid.Empty)
                    {
                        query = query.Where(borrowings => borrowings.Member_Id == memberId);
                    }

                } else
                {
                    query = query.Where(borrowings => borrowings.Member_Id == memberId);
                }

                // Add join Fines
                var joinedQuery = query.Join(
                    _context.Fines,
                    borrowings => borrowings.Borrow_Id,
                    fines => fines.Borrow_Id,
                    (borrowings, fines) => new
                    {
                        Borrowings = borrowings,
                        Fines = fines
                    }
                );

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
                bool isExist = await _context.Books.AnyAsync(books => books.Book_Id == data.Book_Id && books.Available_Copies > 0);
                if (isExist)
                {
                    Borrowings borrowings = new()
                    {
                        Book_Id = data.Book_Id,
                        Member_Id = data.Member_Id,
                        Staff_Id = data.Staff_Id,
                        Borrow_Date = data.Borrow_Date,
                        Due_Date = data.Due_Date,
                        Status = "Borrowed"
                    };

                    Fines fines = new()
                    {
                        Borrow_Id = borrowings.Borrow_Id,
                        Amount = 0,
                    };

                    // Save to database
                    _context.Borrowings.Add(borrowings);
                    await _context.SaveChangesAsync();

                    _context.Fines.Add(fines);
                    await _context.SaveChangesAsync();

                    // Update book available
                    await _context.Books
                    .Where(books => books.Book_Id ==data.Book_Id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(books => books.Available_Copies, books => books.Available_Copies - 1));

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.INSERT_SUCCESS, borrowings);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.BOOK_NOT_AVAILABLE, data);
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
                // Update borrowing
                await _context.Borrowings
                .Where(borrowings => borrowings.Borrow_Id == data.Borrow_Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(borrowings => borrowings.Return_Date, data.Return_Date)
                    .SetProperty(borrowings => borrowings.Status, "Returned"));

                // Update fines
                if (data.Fines.Amount > 0)
                {
                    await _context.Fines
                    .Where(fines => fines.Fine_Id == data.Fines.Fine_Id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(fines => fines.Amount, data.Fines.Amount)
                        .SetProperty(fines => fines.Payment_Status, data.Fines.Payment_Status)
                        .SetProperty(fines => fines.Paid_date, data.Fines.Paid_date));
                }

                // Update book available
                await _context.Books
                .Where(books => books.Book_Id == data.Book_Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(books => books.Available_Copies, books => books.Available_Copies + 1));

                response = new ResponseModel(StatusCodes.Status200OK, AppMessage.UPDATE_SUCCESS, null);
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }
    }
}
