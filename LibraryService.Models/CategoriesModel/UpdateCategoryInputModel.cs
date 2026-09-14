using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.CategoriesModel
{
    public class UpdateCategoryInputModel
    {
        public required Guid Category_Id { get; set; }
        public required string Category_Name { get; set; }
    }
}
