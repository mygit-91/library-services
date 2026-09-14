using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Entities.Data
{
    public class Members
    {
        public Guid Member_Id { get; set; }
        public required string ID_Card { get; set; }
        public required string First_Name { get; set; }
        public required string Last_Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required bool Is_Active { get; set; }
        public Guid? Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public Guid? Update_By { get; set; }
        public DateTime? Update_Date { get; set; }
    }
}
