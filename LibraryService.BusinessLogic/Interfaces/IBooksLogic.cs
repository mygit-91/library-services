using LibraryService.Models;
using LibraryService.Models.BookModel;

namespace LibraryService.BusinessLogic.Interfaces
{
    public interface IBooksLogic
    {
        Task<ResponseModel> GetBooksAsync(GetBookInputModel data);
        Task<ResponseModel> AddBooksAsync(AddBookInputModel data);
        Task<ResponseModel> UpdateBooksAsync(UpdateBookInputModel data);
        Task<ResponseModel> UpdateISBNAsync(UpdateBookISBNInputModel data);
        Task<ResponseModel> DeleteBooksAsync(DeleteBookInputModel data);
    }
}
