using HelpDesk.Domain;
using HelpDesk.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HelpDesk.Repositories.Repositories
{
    public class TeamRepository(IConfiguration configuration) : ITeamRepository
    {
        private readonly string connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<int> Create(Team team)
        {
            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand("CreateTeam", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            CreateTeam(team, command);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task<Team> GetById(int id)
        {
            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand("GetTeams", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return MapTeam(reader);
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            var teams = new List<Team>();

            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand("GetTeams", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", DBNull.Value);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                teams.Add(MapTeam(reader));
            }

            return teams;
        }

        public async Task Update(Team team)
        {
            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand("UpdateTeam", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", team.Id);
            UpdateTeam(team, command);

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
                throw new KeyNotFoundException("Team not found.");
        }

        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand(
                "UPDATE [dbo].[Teams] SET IsActive = 0 WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
                throw new KeyNotFoundException("Team not found.");
        }

        private static Team MapTeam(SqlDataReader reader)
        {
            return new Team
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy"))
            };
        }

        private static void CreateTeam(Team team, SqlCommand command)
        {
            command.Parameters.AddWithValue("@Name", team.Name);
            command.Parameters.AddWithValue("@DepartmentId", team.DepartmentId);
            command.Parameters.AddWithValue("@CategoryId", team.CategoryId);
            command.Parameters.AddWithValue("@IsActive", team.IsActive);
            command.Parameters.AddWithValue("@CreatedBy", team.CreatedBy);
        }

        private static void UpdateTeam(Team team, SqlCommand command)
        {
            command.Parameters.AddWithValue("@Name", team.Name);
            command.Parameters.AddWithValue("@DepartmentId", team.DepartmentId);
            command.Parameters.AddWithValue("@CategoryId", team.CategoryId);
            command.Parameters.AddWithValue("@IsActive", team.IsActive);
            command.Parameters.AddWithValue("@ModifiedBy", team.ModifiedBy);
        }

    }
}
