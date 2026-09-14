using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.StaffModel
{
    public class UpdateStaffIDCardInputModel
    {
        public required Guid Staff_Id { get; set; }
        public required string ID_Card { get; set; }
    }
}
