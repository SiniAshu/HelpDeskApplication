using HelpDesk.Domain;
using HelpDesk.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Services.Services.Validator
{
    public class TicketValidator
    {
        public static void Validate(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            if (string.IsNullOrWhiteSpace(ticket.Title))
                throw new ArgumentException("Title is required.");

            if (string.IsNullOrWhiteSpace(ticket.Description))
                throw new ArgumentException("Description is required.");

            if (string.IsNullOrWhiteSpace(ticket.Title))
                throw new ArgumentException("Title is required.");

            if (string.IsNullOrWhiteSpace(ticket.Description))
                throw new ArgumentException("Description is required.");

            if (ticket.Department == null || ticket.Department.Id == 0)
                throw new ArgumentException("Department is required.");

            if (ticket.Location == null || ticket.Location.Id == 0)
                throw new ArgumentException("Location is required.");

            if (ticket.Category == null || ticket.Category.Id == 0)
                throw new ArgumentException("Category is required.");

            if (!Enum.IsDefined(typeof(TicketStatus), ticket.Status))
                throw new ArgumentException("Invalid ticket status.");

            if (!Enum.IsDefined(typeof(TicketPriority), ticket.Priority))
                throw new ArgumentException("Invalid ticket priority.");

            if (ticket.AssignedTo == 0)
                throw new ArgumentException("AssignedTo is required.");
        }
    }
}
