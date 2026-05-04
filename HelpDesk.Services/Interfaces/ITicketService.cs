using HelpDesk.Domain;

namespace HelpDesk.Services.Interfaces
{
    public interface ITicketService
    {
        Task<int> Create(Ticket ticket);

        Task<Ticket> GetById(int id);

        Task<IEnumerable<Ticket>> GetAll();

        Task Update(Ticket ticket);
    }
}
