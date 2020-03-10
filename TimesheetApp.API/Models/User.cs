using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [MaxLength(64)]
        public byte[] PswHash { get; set; }
        [MaxLength(128)]
        public byte[] PswSalt { get; set; }

        public Address Address { get; set; }
        public int AddressId { get; set; }

        public List<TimeLog> TimeLogs { get; set; }
        public List<UserCompanyRole> UserCompanyRoles { get; set; }
        public List<ProjectUser> UserProjects { get; set; }
    }
}
