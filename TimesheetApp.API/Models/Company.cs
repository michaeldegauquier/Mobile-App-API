using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Address Address { get; set; }
        public int AddressId { get; set; }
        
        public List<Project> Projects;
        public List<CompanyRole> CompanyRoles;
    }
}
