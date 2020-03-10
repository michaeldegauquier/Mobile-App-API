using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Models
{
    public class TimeLog
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
