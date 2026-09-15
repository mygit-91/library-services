using LibraryService.Models;
using LibraryService.Models.BookModel;

namespace LibraryService.BusinessLogic.Interfaces
{
    public interface IBooksLogic
    {
        Task<ResponseModel> GetBooksByIdAsync(Guid bookId);
        Task<ResponseModel> GetBooksListAsync(GetBookInputModel data);
        Task<ResponseModel> AddBooksAsync(AddBookInputModel data);
        Task<ResponseModel> UpdateBooksAsync(UpdateBookInputModel data);
        Task<ResponseModel> DeleteBooksAsync(Guid bookId);
    }
}
