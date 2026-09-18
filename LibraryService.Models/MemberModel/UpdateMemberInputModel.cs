using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.MemberModel
{
    public class UpdateMemberInputModel
    {
        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public required string IdCard { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required bool IsActive { get; set; }

        [Required]
        public required Guid StaffId { get; set; }
    }
}
