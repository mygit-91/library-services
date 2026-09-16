using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.CategoriesModel
{
    public class GetCategoryInputModel
    {
        [Required]
        public required string SearchTopic { get; set; }

        public required string SearchText { get; set; }
    }
}
