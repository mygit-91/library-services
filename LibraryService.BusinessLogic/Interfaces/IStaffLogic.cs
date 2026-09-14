using LibraryService.Models;
using LibraryService.Models.StaffModel;

namespace LibraryService.BusinessLogic.Interfaces
{
    public interface IStaffLogic
    {
        Task<ResponseModel> LoginStaffAsync(LoginStaffInputModel data);
        Task<ResponseModel> GetStaffAsync(string idCard);
        Task<ResponseModel> AddStaffAsync(AddStaffInputModel data);
        Task<ResponseModel> UpdateStaffAsync(UpdateStaffInputModel data);
        Task<ResponseModel> DeleteStaffAsync(DeleteStaffInputModel data);
        Task<ResponseModel> UpdateIdCardAsync(UpdateStaffIDCardInputModel data);
        Task<ResponseModel> UpdatePasswordAsync(UpdateStaffPwdInputModel data);
    }
}
