using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.StaffModel
{
    public class UpdateStaffPwdInputModel
    {
        [Required]
        public required Guid StaffId { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string CurrentPassword { get; set; }

        [Required]
        public required string NewPassword { get; set; }
    }
}
