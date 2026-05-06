using HelpDesk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<int> Create(Team team);

        Task<Team> GetById(int id);

        Task<IEnumerable<Team>> GetAll();

        Task Update(Team team);

        Task Delete(int id);
    }
}
