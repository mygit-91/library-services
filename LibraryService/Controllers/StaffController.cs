using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.StaffModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StaffController(IStaffLogic staffLogic) : ControllerBase
    {
        private readonly IStaffLogic _staffLogic = staffLogic;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> LoginStaff(LoginStaffInputModel data)
        {
            ResponseModel response = await _staffLogic.LoginStaffAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            } else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<ResponseModel>> GetStaff(string idCard = "")
        {
            ResponseModel response = await _staffLogic.GetStaffAsync(idCard);
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
        public async Task<ActionResult<ResponseModel>> AddStaff(AddStaffInputModel data)
        {
            ResponseModel response = await _staffLogic.AddStaffAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult<ResponseModel>> UpdateStaff(UpdateStaffInputModel data)
        {
            ResponseModel response = await _staffLogic.UpdateStaffAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<ResponseModel>> DeleteStaff(DeleteStaffInputModel data)
        {
            ResponseModel response = await _staffLogic.DeleteStaffAsync(data);
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
        public async Task<ActionResult<ResponseModel>> UpdateIdCard(UpdateStaffIDCardInputModel data)
        {
            ResponseModel response = await _staffLogic.UpdateIdCardAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("update-password")]
        public async Task<ActionResult<ResponseModel>> UpdatePassword(UpdateStaffPwdInputModel data)
        {
            ResponseModel response = await _staffLogic.UpdatePasswordAsync(data);
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
