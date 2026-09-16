using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.CategoriesModel
{
    public class UpdateCategoryInputModel
    {
        [Required]
        public required Guid CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
