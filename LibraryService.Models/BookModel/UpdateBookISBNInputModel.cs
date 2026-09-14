using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.BookModel
{
    public class UpdateBookISBNInputModel
    {
        public required Guid Book_Id { get; set; }
        public required string ISBN { get; set; }
    }
}
