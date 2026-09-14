using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Entities.Data
{
    public class Categories
    {
        public Guid Category_Id { get; set; }
        public required string Category_Name { get; set; }
    }
}
