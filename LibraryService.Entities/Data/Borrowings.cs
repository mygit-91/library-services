using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Entities.Data
{
    public class Borrowings
    {
        public Guid Borrow_Id { get; set; }
        public required Guid Book_Id { get; set; }
        public required Guid Member_Id { get; set; }
        public required Guid Staff_Id { get; set; }
        public required DateOnly Borrow_Date { get; set; }
        public required DateOnly Due_Date { get; set; }
        public DateOnly? Return_Date { get; set; }
        public required string Status { get; set; }
    }
}
