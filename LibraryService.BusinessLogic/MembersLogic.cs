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

        public async Task<ResponseModel> GetMembersListAsync()
        {
            ResponseModel response;

            try
            {
                // Process query
                var result = await _context.Members.ToListAsync();

                response = new ResponseModel(StatusCodes.Status200OK, AppMessage.SUCCESS, result);
            }
            catch (Exception ex)
            {
                response = new ResponseModel(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        public async Task<ResponseModel> GetMembersByIdCardAsync(string idCard)
        {
            ResponseModel response;

            try
            {
                // Process query
                var result = await _context.Members.Where(member => member.ID_Card == idCard.Trim() && member.Is_Active == true).ToListAsync();
                if (result.Count > 0)
                {
                    var output = new
                    {
                        memberId = result[0].Member_Id,
                        idCard = result[0].ID_Card,
                        firstName = result[0].First_Name,
                        lastName = result[0].Last_Name,
                        email = result[0].Email,
                        phone = result[0].Phone,
                        address = result[0].Address,
                        isActive = result[0].Is_Active,
                    };

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.SUCCESS, output);
                } else
                {
                    response = new ResponseModel(StatusCodes.Status400BadRequest, AppMessage.NO_MEMBER_FOUND);
                }
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
                bool isExist = await _context.Members.AnyAsync(members => members.ID_Card == data.IdCard);

                if (!isExist)
                {
                    Members member = new()
                    {
                        ID_Card = data.IdCard,
                        First_Name = data.FirstName,
                        Last_Name = data.LastName,
                        Email = data.Email,
                        Phone = data.Phone,
                        Address = data.Address,
                        Is_Active = data.IsActive,
                        Create_Date = DateTime.UtcNow,
                        Create_By = data.StaffId
                    };

                    // Save to database
                    _context.Members.Add(member);
                    await _context.SaveChangesAsync();

                    response = new ResponseModel(StatusCodes.Status200OK, AppMessage.INSERT_SUCCESS);
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

        public async Task<ResponseModel> UpdateMembersAsync(UpdateMemberInputModel data)
        {
            ResponseModel response;

            try
            {
                // Update data
                int rowsAffected = await _context.Members
                    .Where(members => members.Member_Id == data.MemberId)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(members => members.ID_Card, data.IdCard)
                        .SetProperty(members => members.First_Name, data.FirstName)
                        .SetProperty(members => members.Last_Name, data.LastName)
                        .SetProperty(members => members.Email, data.Email)
                        .SetProperty(members => members.Phone, data.Phone)
                        .SetProperty(members => members.Address, data.Address)
                        .SetProperty(members => members.Is_Active, data.IsActive)
                        .SetProperty(members => members.Update_Date, DateTime.UtcNow)
                        .SetProperty(members => members.Update_By, data.StaffId));

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

        public async Task<ResponseModel> DeleteMembersAsync(Guid memberId)
        {
            ResponseModel response;

            try
            {
                int rowsAffected = await _context.Members
                    .Where(members => members.Member_Id == memberId)
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
