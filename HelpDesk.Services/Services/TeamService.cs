using HelpDesk.Domain;
using HelpDesk.Repositories.Interfaces;
using HelpDesk.Repositories.Repositories;
using HelpDesk.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Services.Services
{
    public class TeamService(ITeamRepository teamRepository) : ITeamService
    {
        public async Task<int> Create(Team team)
        {
            return await teamRepository.Create(team);
        }

        public async Task<Team> GetById(int id)
        {
            var team = await teamRepository.GetById(id);
            return team ?? throw new KeyNotFoundException("Team not found.");
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await teamRepository.GetAll();
        }

        public async Task Update(UpdateTeam team)
        {
            var existingTeam = await teamRepository.GetById(team.Id) ?? throw new KeyNotFoundException("Team not found.");
            existingTeam.Name = team.Name ?? existingTeam.Name;
            existingTeam.DepartmentId = team.DepartmentId ?? existingTeam.DepartmentId;
            existingTeam.CategoryId = team.CategoryId ?? existingTeam.CategoryId;
            existingTeam.IsActive = team.IsActive ?? existingTeam.IsActive;

            await teamRepository.Update(existingTeam);
        }

        public async Task Delete(int id)
        {
            var existing = await teamRepository.GetById(id) ?? throw new KeyNotFoundException("Team not found.");
            await teamRepository.Delete(existing.Id);
        }
    }
}
