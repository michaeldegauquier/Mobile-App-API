using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class CompanyRoleDto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public bool ManageCompany { get; set; }
        public bool ManageUsers { get; set; }
        public bool ManageProjects { get; set; }
        public bool ManageRoles { get; set; }
    }
}
