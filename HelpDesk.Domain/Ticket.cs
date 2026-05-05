using HelpDesk.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Domain
{
    public class Ticket : BaseDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public  Location Location { get; set; } = new();

        public Department Department { get; set; } = new();

        public Category Category { get; set; } = new();

        public TicketStatus Status { get; set; }

        public TicketPriority Priority { get; set; }

        public int AssignedTo { get; set; }
    }

}
