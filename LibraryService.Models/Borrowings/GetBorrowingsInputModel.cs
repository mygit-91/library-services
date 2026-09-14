using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.Borrowings
{
    public class GetBorrowingsInputModel
    {
        public string? Member_Id_Card { get; set; }
        public Guid? Staff_Id { get; set; }
        public bool Is_Staff { get; set; } = false;
    }
}
