using LibraryService.BusinessLogic.Interfaces;
using LibraryService.Commons.Constants;
using LibraryService.Entities;
using LibraryService.Entities.Data;
using LibraryService.Models;
using LibraryService.Models.MemberModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.BusinessLogic
{
    public class MembersLogic(AppDbContext context) : IMembersLogic
    {
        private readonly AppDbContext _context = context;

        public async Task<ResponseModel> GetMembersAsync(string idCard)
        {
            ResponseModel response;

            try
            {
                // Create query
                IQueryable<Members> query = _context.Members;

                // Add filter condition to query
                if (!string.IsNullOrEmpty(idCard))
                {
                    query = query.Where(members => members.ID_Card.Contains(idCard));
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

        public async Task<ResponseModel> AddMembersAsync(AddMemberInputModel data)
        {
            ResponseModel response;

            try
            {
                //Check existing
                bool isExist = await _context.Members.AnyAsync(members => members.ID_Card == data.ID_Card);

                if (!isExist)
                {
                    Members member = new()
                    {
                        ID_Card = data.ID_Card,
                        First_Name = data.First_Name,
                        Last_Name = data.Last_Name,
                        Email = data.Email,
                        Phone = data.Phone,
                        Address = data.Address,
                        Is_Active = data.Is_Active,
                        Create_Date = DateTime.UtcNow,
                        Create_By = data.Staff_Id
                    };

                    // Save to database
                    _context.Members.Add(member);
                    await _context.SaveChangesAsync();

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.INSERT_SUCCESS, member);
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

        public async Task<ResponseModel> UpdateMembersAsync(UpdateMemberInputModel data)
        {
            ResponseModel response;

            try
            {
                // Update data
                int rowsAffected = await _context.Members
                    .Where(members => members.Member_Id == data.Member_Id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(members => members.First_Name, data.First_Name)
                        .SetProperty(members => members.Last_Name, data.Last_Name)
                        .SetProperty(members => members.Email, data.Email)
                        .SetProperty(members => members.Phone, data.Phone)
                        .SetProperty(members => members.Address, data.Address)
                        .SetProperty(members => members.Is_Active, data.Is_Active)
                        .SetProperty(members => members.Update_Date, DateTime.UtcNow)
                        .SetProperty(members => members.Update_By, data.Staff_Id));

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

        public async Task<ResponseModel> DeleteMembersAsync(DeleteMemberInputModel data)
        {
            ResponseModel response;

            try
            {
                int rowsAffected = await _context.Members
                    .Where(members => members.Member_Id == data.Member_Id)
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

        public async Task<ResponseModel> UpdateIdCardAsync(UpdateMemberIDCardInputModel data)
        {
            // To Do

            return new ResponseModel(0, "", null);
        }
    }
}
