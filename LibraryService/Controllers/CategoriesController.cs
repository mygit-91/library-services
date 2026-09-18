using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Models;
using LibraryService.Models.CategoriesModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryService.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoriesController(ICategoriesLogic categoriesLogic) : ControllerBase
    {
        private readonly ICategoriesLogic _categoriesLogic = categoriesLogic;

        [HttpPost("list")]
        public async Task<ActionResult<ResponseModel>> GetCategory(GetCategoryInputModel data)
        {
            ResponseModel response = await _categoriesLogic.GetCategoryAsync(data);
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

        [HttpPut("update")]
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

        [HttpDelete("delete")]
        public async Task<ActionResult<ResponseModel>> DeleteCategory([Required] Guid id)
        {
            ResponseModel response = await _categoriesLogic.DeleteCategoryAsync(id);
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
