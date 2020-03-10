using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class CompanyToUpdateDto
    {
        public string Name { get; set; }
        public AddressToUpdateDto Address { get; set; }
    }
}
