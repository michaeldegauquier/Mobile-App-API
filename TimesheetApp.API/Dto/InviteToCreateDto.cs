using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class InviteToCreateDto
    {
        public string Email { get; set; }
        public int CompanyId { get; set; }
    }
}
