using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.StaffModel
{
    public class UpdateStaffPwdInputModel
    {
        public required Guid Staff_Id { get; set; }
        public required string Username { get; set; }
        public required string Current_Password { get; set; }
        public required string New_Password { get; set; }
    }
}
