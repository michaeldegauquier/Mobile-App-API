using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Models
{
    public class UserProjectRole
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public ProjectRole ProjectRole { get; set; }
        public int ProjectRoleId { get; set; }
    }
}
