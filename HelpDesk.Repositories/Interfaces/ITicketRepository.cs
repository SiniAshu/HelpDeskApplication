using HelpDesk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Repositories.Repositories
{
    public interface ITicketRepository
    {
        Task<int> Create(Ticket ticket);

        Task<Ticket> GetById(int id);

        Task<IEnumerable<Ticket>> GetAll();

        Task Update(Ticket ticket);
    }
}
