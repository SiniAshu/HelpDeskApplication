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
        public required string Title { get; set; }

        public required string Description { get; set; }

        public int LocationId { get; set; }

        public int DepartmentId { get; set; }

        public int CategoryId { get; set; }

        public TicketStatus Status { get; set; }

        public TicketPriority Priority { get; set; }

        public int AssignedTo { get; set; }
    }

}
