using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Commons.Constants;
using LibraryService.Entities;
using LibraryService.Entities.Data;
using LibraryService.Models;
using LibraryService.Models.CategoriesModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.BusinessLogic
{
    public class CategoriesLogic(AppDbContext context) : ICategoriesLogic
    {
        private readonly AppDbContext _context = context;

        public async Task<ResponseModel> GetCategoryAsync(string categoryName)
        {
            ResponseModel response;

            try
            {
                // Create query
                IQueryable<Categories> query = _context.Categories;

                // Add filter condition to query
                if (!string.IsNullOrEmpty(categoryName))
                {
                    query = query.Where(categories => categories.Category_Name.Contains(categoryName));
                }

                // Process query
                var result = await query.ToListAsync();

                response = new ResponseModel(StatusCodes.Status200OK, AppMessage.SUCCESS, result);
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> AddCategoryAsync(AddCategoryInputModel data)
        {
            ResponseModel response;

            try
            {
                //Check existing
                bool isExist = await _context.Categories.AnyAsync(categories => categories.Category_Name == data.Category_Name);

                if (!isExist)
                {
                    Categories category = new()
                    {
                        Category_Name = data.Category_Name
                    };

                    // Save to database
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.INSERT_SUCCESS, category);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DUPLICATE_DATA, data);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> UpdateCategoryAsync(UpdateCategoryInputModel data)
        {
            ResponseModel response;

            try
            {
                // Update data
                int rowsAffected = await _context.Categories
                    .Where(categories => categories.Category_Id == data.Category_Id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(categories => categories.Category_Name, data.Category_Name));

                if (rowsAffected > 0)
                {
                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.UPDATE_SUCCESS, data);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DATA_NOT_FOUND, data);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> DeleteCategoryAsync(DeleteCategoryInputModel data)
        {
            ResponseModel response;

            try
            {
                int rowsAffected = await _context.Categories
                    .Where(categories => categories.Category_Id == data.Category_Id)
                    .ExecuteDeleteAsync();

                if (rowsAffected > 0)
                {
                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.DELETE_SUCCESS, null);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DATA_NOT_FOUND, data);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }
    }
}
