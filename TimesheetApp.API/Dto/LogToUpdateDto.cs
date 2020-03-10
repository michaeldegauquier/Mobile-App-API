using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Dto
{
    public class LogToUpdateDto
    {
        public int ProjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public string Description { get; set; }
    }
}
