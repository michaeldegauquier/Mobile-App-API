using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class UserToCreateDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Psw { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public AddressToCreateDto Address{ get; set;}
    }
}
