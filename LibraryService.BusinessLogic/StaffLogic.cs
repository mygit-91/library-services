using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Commons.Constants;
using LibraryService.Commons.Interfaces;
using LibraryService.Entities;
using LibraryService.Entities.Data;
using LibraryService.Models;
using LibraryService.Models.StaffModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.BusinessLogic
{
    public class StaffLogic(AppDbContext context, IJwtService jwt) : IStaffLogic
    {
        private readonly AppDbContext _context = context;
        private readonly IJwtService _jwt = jwt;

        public async Task<ResponseModel> LoginStaffAsync(LoginStaffInputModel data)
        {
            ResponseModel response;

            try
            {
                // Check username and password
                IQueryable<Staff> query = _context.Staff.Where(staff => staff.Username == data.Username);
                var result = await query.ToListAsync();

                if (result != null && result.Count > 0 && BCrypt.Net.BCrypt.Verify(data.Password, result[0].Password))
                {
                    // Generate token
                    string tokenKey = _jwt.GenerateJwtToken(data.Username);

                    LoginStaffOutputModel output = new()
                    {
                        Token = tokenKey,
                        StaffId = result[0].Staff_Id,
                        IdCard = result[0].ID_Card,
                        FirstName = result[0].First_Name,
                        LastName = result[0].Last_Name,
                        Position = result[0].Position,
                        Email = result[0].Email,
                        Phone = result[0].Phone,
                        IsActive = result[0].Is_Active,
                    };

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.SUCCESS, output);
                } else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.INVALID_USER_LOGIN);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> GetStaffByIdCardAsync(string idCard)
        {
            ResponseModel response;

            try
            {
                // Create query
                IQueryable<Staff> query = _context.Staff;

                // Add filter condition to query
                if (!string.IsNullOrEmpty(idCard))
                {
                    query = query.Where(staff => staff.ID_Card.Contains(idCard));
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

        public async Task<ResponseModel> AddStaffAsync(AddStaffInputModel data)
        {
            ResponseModel response;

            try
            {
                //Check existing
                bool isExist = await _context.Staff.AnyAsync(staff => staff.ID_Card == data.IdCard || staff.Username == data.Username);

                if (!isExist)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(data.Password);
                    Staff staff = new()
                    {
                        ID_Card = data.IdCard,
                        Username = data.Username,
                        Password = passwordHash,
                        First_Name = data.FirstName,
                        Last_Name = data.LastName,
                        Position = data.Position,
                        Email = data.Email,
                        Phone = data.Phone,
                        Is_Active = data.IsActive,
                        Create_Date = DateTime.UtcNow
                    };

                    // Save to database
                    _context.Staff.Add(staff);
                    await _context.SaveChangesAsync();

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.INSERT_SUCCESS, staff);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DUPLICATE_DATA);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> UpdateStaffAsync(UpdateStaffInputModel data)
        {
            ResponseModel response;

            try
            {
                // Update data
                int rowsAffected = await _context.Staff
                    .Where(staff => staff.Staff_Id == data.StaffId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(staff => staff.ID_Card, data.IdCard)
                        .SetProperty(staff => staff.First_Name, data.FirstName)
                        .SetProperty(staff => staff.Last_Name, data.LastName)
                        .SetProperty(staff => staff.Email, data.Email)
                        .SetProperty(staff => staff.Phone, data.Phone)
                        .SetProperty(staff => staff.Position, data.Position)
                        .SetProperty(staff => staff.Is_Active, data.IsActive)
                        .SetProperty(staff => staff.Update_Date, DateTime.UtcNow));

                if (rowsAffected > 0)
                {
                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.UPDATE_SUCCESS);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DATA_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> UpdatePasswordAsync(UpdateStaffPwdInputModel data)
        {
            ResponseModel response;

            try
            {
                // Check username and password
                IQueryable<Staff> query = _context.Staff.Where(staff => staff.Staff_Id == data.StaffId && staff.Username == data.Username);
                var result = await query.ToListAsync();

                if (result != null && result.Count > 0 && BCrypt.Net.BCrypt.Verify(data.CurrentPassword, result[0].Password))
                {
                    // Update password
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(data.NewPassword);
                    int rowsAffected = await _context.Staff
                        .Where(staff => staff.Staff_Id == data.StaffId)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(staff => staff.Password, passwordHash)
                            .SetProperty(staff => staff.Update_Date, DateTime.UtcNow));

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.UPDATE_SUCCESS);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.INVALID_USER_LOGIN);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> DeleteStaffAsync(Guid staffId)
        {
            ResponseModel response;

            try
            {
                int rowsAffected = await _context.Staff
                    .Where(staff => staff.Staff_Id == staffId)
                    .ExecuteDeleteAsync();

                if (rowsAffected > 0)
                {
                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.DELETE_SUCCESS);
                }
                else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.DATA_NOT_FOUND);
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
