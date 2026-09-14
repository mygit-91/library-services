using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.Borrowings
{
    public class ReturnBookInputModel
    {
        public required Guid Borrow_Id { get; set; }
        public required Guid Book_Id { get; set; }
        public required Guid Member_Id { get; set; }
        public required Guid Staff_Id { get; set; }
        public required DateOnly Return_Date { get; set; }
        public required FinesInputModel Fines { get; set; }
    }
}
