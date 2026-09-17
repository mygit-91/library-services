using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.StaffModel
{
    public class UpdateStaffInputModel
    {
        [Required]
        public required Guid StaffId { get; set; }

        [Required]
        public required string IdCard { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Position { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required bool IsActive { get; set; }
    }
}
