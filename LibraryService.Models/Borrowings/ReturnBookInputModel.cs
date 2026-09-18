using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.Borrowings
{
    public class ReturnBookInputModel
    {
        [Required]
        public required Guid BorrowId { get; set; }

        [Required]
        public required Guid BookId { get; set; }

        [Required]
        public required Guid MemberId { get; set; }

        [Required]
        public required DateOnly ReturnDate { get; set; }

        [Required]
        public required Guid FinesId { get; set; }

        public decimal? Amount { get; set; }
        public string? PaymentStatus { get; set; }
        public DateOnly? PaidDate { get; set; }
        public required bool IsOverTime { get; set; }
        public required Guid StaffId { get; set; }
    }
}
