using LibraryService.BusinessLogic;
using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.MemberModel;
using LibraryService.Models.StaffModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Controllers
{
    [Route("api/members")]
    [ApiController]
    [Authorize]
    public class MembersController(IMembersLogic membersLogic) : ControllerBase
    {
        private readonly IMembersLogic _membersLogic = membersLogic;

        [HttpGet("list")]
        public async Task<ActionResult<ResponseModel>> GetMembersList()
        {
            ResponseModel response = await _membersLogic.GetMembersListAsync();
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpGet("get-byidcard")]
        public async Task<ActionResult<ResponseModel>> GetMembersByIdCard([Required] string id)
        {
            ResponseModel response = await _membersLogic.GetMembersByIdCardAsync(id);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("add")]
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

        [HttpPut("update")]
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

        [HttpDelete("delete")]
        public async Task<ActionResult<ResponseModel>> DeleteMembers([Required] Guid id)
        {
            ResponseModel response = await _membersLogic.DeleteMembersAsync(id);
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
