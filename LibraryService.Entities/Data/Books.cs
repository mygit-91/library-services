using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Entities.Data
{
    public class Books
    {
        public Guid Book_Id { get; set; }
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public string? Publisher { get; set; }
        public int Publish_Year { get; set; }
        public required int Total_Copies { get; set; }
        public required int Available_Copies { get; set; }
        public required string Location { get; set; }
        public required Guid Category_Id { get; set; }
        public required bool Is_Active { get; set; }
    }
}
