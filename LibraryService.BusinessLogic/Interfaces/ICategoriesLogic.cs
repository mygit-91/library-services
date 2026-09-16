using LibraryService.Models;
using LibraryService.Models.CategoriesModel;

namespace LibraryService.BusinessLogic.Interfaces
{
    public interface ICategoriesLogic
    {
        Task<ResponseModel> GetCategoryAsync(GetCategoryInputModel data);
        Task<ResponseModel> AddCategoryAsync(AddCategoryInputModel data);
        Task<ResponseModel> UpdateCategoryAsync(UpdateCategoryInputModel data);
        Task<ResponseModel> DeleteCategoryAsync(Guid categoryId);
    }
}
