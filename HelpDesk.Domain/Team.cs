using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Domain
{
    public class Team : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
