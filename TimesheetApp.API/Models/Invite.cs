using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Models
{
    public class Invite
    {
        public int InviteId { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }
    }
}
