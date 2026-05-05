using HelpDesk.Domain;
using HelpDesk.Domain.Enum;
using HelpDesk.Repositories.Repositories;
using HelpDesk.Services.Interfaces;
using HelpDesk.Services.Services.Validator;

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

            TicketValidator.Validate(ticket);

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
            existing.Category = new Category()
            {
                Id = ticket.Category.Id
            };
            existing.Location = new Location()
            {
                Id = ticket.Location.Id
            };
            existing.Department = new Department()
            {
                Id = ticket.Department.Id
            };
            existing.Priority = ticket.Priority;
            existing.AssignedTo = ticket.AssignedTo;
            existing.ModifiedBy = ticket.ModifiedBy;
            await TicketRepository.Update(existing);
        }
    }
}
