using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.Borrowings
{
    public class GetBorrowingsInputModel
    {
        [Required]
        public required string SearchTopic { get; set; }

        public required string SearchText { get; set; }

        public required bool IsStaff { get; set; }

        [Required]
        public required bool IsBorrowList { get; set; }
    }
}
