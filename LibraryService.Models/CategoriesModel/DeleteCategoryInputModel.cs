using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.CategoriesModel
{
    public class DeleteCategoryInputModel
    {
        public required Guid Category_Id { get; set; }
    }
}
