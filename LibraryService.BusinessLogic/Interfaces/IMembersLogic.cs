using LibraryService.Models;
using LibraryService.Models.MemberModel;

namespace LibraryService.BusinessLogic.Interfaces
{
    public interface IMembersLogic
    {
        Task<ResponseModel> GetMembersListAsync();
        Task<ResponseModel> GetMembersByIdCardAsync(string idCard);
        Task<ResponseModel> AddMembersAsync(AddMemberInputModel data);
        Task<ResponseModel> UpdateMembersAsync(UpdateMemberInputModel data);
        Task<ResponseModel> DeleteMembersAsync(Guid memberId);
    }
}
