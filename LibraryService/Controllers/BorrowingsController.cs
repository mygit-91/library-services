using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.Borrowings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowingsController(IBorrowingsLogic borrowing) : ControllerBase
    {
        private readonly IBorrowingsLogic _borrowing = borrowing;

        [HttpPost("get-borrowing")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> GetBorrowing(GetBorrowingsInputModel data)
        {
            ResponseModel response = await _borrowing.GetBorrowingAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("add-borrowing")]
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

        [HttpPost("return-book")]
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
