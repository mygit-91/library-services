using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Entities.Data
{
    public class Staff
    {
        public Guid Staff_Id { get; set; }
        public required string ID_Card { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string First_Name { get; set; }
        public required string Last_Name { get; set; }
        public required string Position { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required bool Is_Active { get; set; }
        public DateTime? Create_Date { get; set; }
        public DateTime? Update_Date { get; set; }
    }
}
