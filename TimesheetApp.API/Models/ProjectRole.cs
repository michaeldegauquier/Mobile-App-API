using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimesheetApp.API.Models
{
    public class ProjectRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public List<UserProjectRole> UserProjectRoles { get; set; }
    }
}
