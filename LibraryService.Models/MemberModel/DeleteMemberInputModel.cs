using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Models.MemberModel
{
    public class DeleteMemberInputModel
    {
        public required Guid Member_Id { get; set; }
    }
}
