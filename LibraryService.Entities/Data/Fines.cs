using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Entities.Data
{
    public class Fines
    {
        public Guid Fine_Id { get; set; }
        public required Guid Borrow_Id { get; set; }
        public required decimal Amount { get; set; }
        public string? Payment_Status { get; set; }
        public DateOnly? Paid_Date { get; set; }
    }
}
