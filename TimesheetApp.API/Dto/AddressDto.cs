using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class AddressDto
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string BoxNumber { get; set; }
    }
}
