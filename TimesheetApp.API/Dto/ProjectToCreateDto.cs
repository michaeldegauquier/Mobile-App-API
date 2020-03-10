using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class ProjectToCreateDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
