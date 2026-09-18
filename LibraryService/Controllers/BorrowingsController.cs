using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.Borrowings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [Route("api/borrowings")]
    [ApiController]
    [Authorize]
    public class BorrowingsController(IBorrowingsLogic borrowing) : ControllerBase
    {
        private readonly IBorrowingsLogic _borrowing = borrowing;

        [HttpPost("list")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> GetBorrowingList(GetBorrowingsInputModel data)
        {
            ResponseModel response = await _borrowing.GetBorrowingListAsync(data);
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
        public async Task<ActionResult<ResponseModel>> AddBorrowing(AddBorrowingsInputModel data)
        {
            ResponseModel response = await _borrowing.AddBorrowingAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPut("return")]
        public async Task<ActionResult<ResponseModel>> ReturnBook(ReturnBookInputModel data)
        {
            ResponseModel response = await _borrowing.ReturnBookAsync(data);
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
