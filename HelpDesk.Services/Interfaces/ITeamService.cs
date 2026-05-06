using HelpDesk.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Services.Interfaces
{
    public interface ITeamService
    {
        Task<int> Create(Team team);
        Task<Team> GetById(int id);
        Task<IEnumerable<Team>> GetAll();
        Task Update(UpdateTeam team);
        Task Delete(int id);
    }
}
