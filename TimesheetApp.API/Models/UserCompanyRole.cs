using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Models
{
    public class UserCompanyRole
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public CompanyRole CompanyRole { get; set; }
        public int CompanyRoleId { get; set; }
    }
}
