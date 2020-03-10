using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class CompanyToCreateDto
    {
        public string Name { get; set; }
        public AddressToCreateDto Address { get; set; }
    }
}
