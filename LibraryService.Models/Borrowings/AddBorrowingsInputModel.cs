using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.Borrowings
{
    public class AddBorrowingsInputModel
    {
        [Required]
        public required Guid BookId { get; set; }

        [Required]
        public required Guid MemberId { get; set; }

        [Required]
        public required Guid StaffId { get; set; }

        [Required]
        public required DateOnly BorrowDate { get; set; }

        [Required]
        public required DateOnly DueDate { get; set; }
    }
}
