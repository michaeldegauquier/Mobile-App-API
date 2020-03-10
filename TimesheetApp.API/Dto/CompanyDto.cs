using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class CompanyDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }
}
