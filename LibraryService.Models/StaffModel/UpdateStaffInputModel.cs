using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.StaffModel
{
    public class UpdateStaffInputModel
    {
        public required Guid Staff_Id { get; set; }
        public required string First_Name { get; set; }
        public required string Last_Name { get; set; }
        public required string Position { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required bool Is_Active { get; set; }
    }
}
