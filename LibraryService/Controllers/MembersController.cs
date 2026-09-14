using LibraryService.BusinessLogic;
using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.MemberModel;
using LibraryService.Models.StaffModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MembersController(IMembersLogic membersLogic) : ControllerBase
    {
        private readonly IMembersLogic _membersLogic = membersLogic;

        [HttpGet("get-members")]
        public async Task<ActionResult<ResponseModel>> GetMembers(string idCard = "")
        {
            ResponseModel response = await _membersLogic.GetMembersAsync(idCard);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("add-members")]
        public async Task<ActionResult<ResponseModel>> AddMembers(AddMemberInputModel data)
        {
            ResponseModel response = await _membersLogic.AddMembersAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("update-members")]
        public async Task<ActionResult<ResponseModel>> UpdateMembers(UpdateMemberInputModel data)
        {
            ResponseModel response = await _membersLogic.UpdateMembersAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("delete-members")]
        public async Task<ActionResult<ResponseModel>> DeleteMembers(DeleteMemberInputModel data)
        {
            ResponseModel response = await _membersLogic.DeleteMembersAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("update-idcard")]
        public async Task<ActionResult<ResponseModel>> UpdateIdCard(UpdateMemberIDCardInputModel data)
        {
            ResponseModel response = await _membersLogic.UpdateIdCardAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }
    }
}
