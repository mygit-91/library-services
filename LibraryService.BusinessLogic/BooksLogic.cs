using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Commons.Constants;
using LibraryService.Entities;
using LibraryService.Entities.Data;
using LibraryService.Models;
using LibraryService.Models.BookModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace LibraryService.BusinessLogic
{
    public class BooksLogic(AppDbContext context) : IBooksLogic
    {
        private readonly AppDbContext _context = context;

        public async Task<ResponseModel> GetBooksAsync(GetBookInputModel data)
        {
            ResponseModel response;

            try
            {
                // Create query
                IQueryable<Books> booksQuery = _context.Books;
                IQueryable<Categories> categoryQuery = _context.Categories;

                // Add filter condition to query
                string searchTopic = data.SearchTopic.ToLower();
                string searchText = data.SearchText.ToLower();

                if (!data.IsStaff)
                {
                    booksQuery = booksQuery.Where(books => books.Is_Active == true);
                }

                if (searchTopic == "title")
                {
                    booksQuery = booksQuery.Where(books => books.Title.Contains(searchText));
                } else if (searchTopic == "author")
                {
                    booksQuery = booksQuery.Where(books => books.Author.Contains(searchText));
                } else if (searchTopic == "isbn")
                {
                    booksQuery = booksQuery.Where(books => books.ISBN.Contains(searchText));
                } else if (searchTopic == "category")
                {
                    categoryQuery = categoryQuery.Where(categories => categories.Category_Name.Contains(searchText));
                }
                else
                {
                    // Seclect all
                }

                // Join with Categories
                var joinedQuery = (from books in booksQuery
                                   join categories in categoryQuery
                                   on books.Category_Id equals categories.Category_Id
                                   select new
                                   {
                                       bookId = books.Book_Id,
                                       isbn = books.ISBN,
                                       title = books.Title,
                                       author = books.Author,
                                       publisher = books.Publisher,
                                       publishYear = books.Publish_Year,
                                       availableCopies = books.Available_Copies,
                                       location = books.Location,
                                       isActive = books.Is_Active,
                                       categoryId = books.Category_Id,
                                       categoryName = categories.Category_Name,
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

        public async Task<ResponseModel> AddBooksAsync(AddBookInputModel data)
        {
            ResponseModel response;

            try
            {
                //Check existing
                bool isExist = await _context.Books.AnyAsync(books => books.ISBN == data.ISBN);

                if (!isExist)
                {
                    Books book = new()
                    {
                        ISBN = data.ISBN,
                        Title = data.Title,
                        Author = data.Author,
                        Publisher = data.Publisher,
                        Publish_Year = data.PublishYear,
                        Total_Copies = data.TotalCopies,
                        Available_Copies = data.AvailableCopies,
                        Location = data.Location,
                        Is_Active = data.IsActive,
                        Category_Id = data.CategoryId
                    };

                    // Save to database
                    _context.Books.Add(book);
                    await _context.SaveChangesAsync();

                    var output = new
                    {
                        BookId = book.Book_Id
                    };

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.INSERT_SUCCESS, output);
                } else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DUPLICATE_DATA, data);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> UpdateBooksAsync(UpdateBookInputModel data)
        {
            ResponseModel response;

            try
            {
                // Update data
                int rowsAffected = await _context.Books
                    .Where(books => books.Book_Id == data.Book_Id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(books => books.Title, data.Title)
                        .SetProperty(books => books.Author, data.Author)
                        .SetProperty(books => books.Publisher, data.Publisher)
                        .SetProperty(books => books.Publish_Year, data.Publish_Year)
                        .SetProperty(books => books.Total_Copies, data.Total_Copies)
                        .SetProperty(books => books.Available_Copies, data.Available_Copies)
                        .SetProperty(books => books.Location, data.Location)
                        .SetProperty(books => books.Is_Active, data.Is_Active)
                        .SetProperty(books => books.Category_Id, data.Category_Id));

                if (rowsAffected > 0)
                {
                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.UPDATE_SUCCESS, data);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DATA_NOT_FOUND, data);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> UpdateISBNAsync(UpdateBookISBNInputModel data)
        {
            // To Do

            return new ResponseModel(0, "", null);
        }

        public async Task<ResponseModel> DeleteBooksAsync(DeleteBookInputModel data)
        {
            ResponseModel response;

            try
            {
                int rowsAffected = await _context.Books
                    .Where(books => books.Book_Id == data.Book_Id)
                    .ExecuteDeleteAsync();

                if (rowsAffected > 0)
                {
                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.DELETE_SUCCESS, null);
                } else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DATA_NOT_FOUND, data);
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
