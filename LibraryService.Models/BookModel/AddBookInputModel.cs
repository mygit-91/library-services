using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.BookModel
{
    public class AddBookInputModel
    {
        [Required]
        public required string ISBN { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Author { get; set; }

        public string? Publisher { get; set; }
        public int PublishYear { get; set; }
        public required int TotalCopies { get; set; }
        public required int AvailableCopies { get; set; }
        public required Guid CategoryId { get; set; }
        public required bool IsActive { get; set; }

        [Required]
        public required string Location { get; set; }
    }
}
