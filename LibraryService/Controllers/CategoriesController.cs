using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.CategoriesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController(ICategoriesLogic categoriesLogic) : ControllerBase
    {
        private readonly ICategoriesLogic _categoriesLogic = categoriesLogic;

        [HttpGet("get-category")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseModel>> GetCategory(string categoryName = "")
        {
            ResponseModel response = await _categoriesLogic.GetCategoryAsync(categoryName);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<ResponseModel>> AddCategory(AddCategoryInputModel data)
        {
            ResponseModel response = await _categoriesLogic.AddCategoryAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("update-category")]
        public async Task<ActionResult<ResponseModel>> UpdateCategory(UpdateCategoryInputModel data)
        {
            ResponseModel response = await _categoriesLogic.UpdateCategoryAsync(data);
            if (response.Status == StatusCodes.Status200OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(response.Status, response);
            }
        }

        [HttpPost("delete-category")]
        public async Task<ActionResult<ResponseModel>> DeleteCategory(DeleteCategoryInputModel data)
        {
            ResponseModel response = await _categoriesLogic.DeleteCategoryAsync(data);
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
