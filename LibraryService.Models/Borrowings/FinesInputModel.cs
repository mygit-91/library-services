using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.Borrowings
{
    public class FinesInputModel
    {
        public required Guid Fine_Id { get; set; }
        public required decimal Amount { get; set; }
        public string? Payment_Status { get; set; }
        public DateTime? Paid_date { get; set; }
    }
}
