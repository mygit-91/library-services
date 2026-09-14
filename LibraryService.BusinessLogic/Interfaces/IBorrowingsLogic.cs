using LibraryService.Models;
using LibraryService.Models.Borrowings;

namespace LibraryService.BusinessLogic.Interfaces
{
    public interface IBorrowingsLogic
    {
        Task<ResponseModel> GetBorrowingAsync(GetBorrowingsInputModel data);
        Task<ResponseModel> AddBorrowingAsync(AddBorrowingsInputModel data);
        Task<ResponseModel> ReturnBookAsync(ReturnBookInputModel data);
    }
}
