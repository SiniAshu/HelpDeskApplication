using HelpDesk.Domain;
using HelpDesk.Domain.Enum;
using HelpDesk.Repositories.Repositories;
using HelpDesk.Services.Interfaces;

namespace HelpDesk.Services.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository TicketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            TicketRepository = ticketRepository;
        }

        public async Task<int> Create(Ticket ticket)
        {
            ArgumentNullException.ThrowIfNull(ticket);

            if (string.IsNullOrWhiteSpace(ticket.Title))
                throw new ArgumentException("Title is required.");

            if (string.IsNullOrWhiteSpace(ticket.Description))
                throw new ArgumentException("Description is required.");

            // Business default rules
            ticket.Status = TicketStatus.Open;

            return await TicketRepository.Create(ticket);
        }

        public async Task<Ticket> GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ticket id.");

            return await TicketRepository.GetById(id);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await TicketRepository.GetAll();
        }

        public async Task Update(Ticket ticket)
        {
            ArgumentNullException.ThrowIfNull(ticket);

            if (ticket.Id <= 0)
                throw new ArgumentException("Invalid ticket id.");

            var existing = await TicketRepository.GetById(ticket.Id);

            // Update only allowed fields
            existing.Title = ticket.Title;
            existing.Description = ticket.Description;
            existing.Priority = ticket.Priority;
            existing.AssignedTo = ticket.AssignedTo;
            existing.ModifiedBy = ticket.ModifiedBy;
            await TicketRepository.Update(existing);
        }
    }
}
