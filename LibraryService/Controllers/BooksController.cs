using LibraryService.BusinessLogic;
using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.BookModel;
using LibraryService.Models.StaffModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController(IBooksLogic bookLogic) : ControllerBase
    {
        private readonly IBooksLogic _bookLogic = bookLogic;

        [HttpPost("get")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> GetBooks(GetBookInputModel data)
        {
            ResponseModel response = await _bookLogic.GetBooksAsync(data);
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
        public async Task<ActionResult<ResponseModel>> AddBooks(AddBookInputModel data)
        {
            ResponseModel response = await _bookLogic.AddBooksAsync(data);
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
        public async Task<ActionResult<ResponseModel>> UpdateBooks(UpdateBookInputModel data)
        {
            ResponseModel response = await _bookLogic.UpdateBooksAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("update-isbn")]
        public async Task<ActionResult<ResponseModel>> UpdateISBN(UpdateBookISBNInputModel data)
        {
            ResponseModel response = await _bookLogic.UpdateISBNAsync(data);
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
        public async Task<ActionResult<ResponseModel>> DeleteBooks(DeleteBookInputModel data)
        {
            ResponseModel response = await _bookLogic.DeleteBooksAsync(data);
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
