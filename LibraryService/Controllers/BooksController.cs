using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.BookModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Controllers
{
    [Route("api/book")]
    [ApiController]
    [Authorize]
    public class BooksController(IBooksLogic bookLogic) : ControllerBase
    {
        private readonly IBooksLogic _bookLogic = bookLogic;

        [HttpGet("get-byid")]
        public async Task<ActionResult<ResponseModel>> GetBooksById([Required] Guid id)
        {
            ResponseModel response = await _bookLogic.GetBooksByIdAsync(id);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("list")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> GetBooksList(GetBookInputModel data)
        {
            ResponseModel response = await _bookLogic.GetBooksListAsync(data);
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

        [HttpDelete("delete")]
        public async Task<ActionResult<ResponseModel>> DeleteBooks([Required] Guid id)
        {
            ResponseModel response = await _bookLogic.DeleteBooksAsync(id);
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
