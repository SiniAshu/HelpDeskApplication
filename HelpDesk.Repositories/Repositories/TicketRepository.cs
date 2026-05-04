using HelpDesk.Domain;
using HelpDesk.Domain.Enum;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Repositories.Repositories
{
    public class TicketRepository(IConfiguration configuration) : ITicketRepository
    {
        private readonly string connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task<int> Create(Ticket ticket)
        {
            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand("CreateTicket", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            CreateTicket(ticket, command);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }


        public async Task<IEnumerable<Ticket>> GetAll()
        {
            var tickets = new List<Ticket>();

            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand("GetTickets", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", DBNull.Value);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tickets.Add(MapTicket(reader));
            }

            return tickets;
        }

        public async Task<Ticket> GetById(int id)
        {
            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand("GetTickets", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return MapTicket(reader);
        }

        private static Ticket MapTicket(SqlDataReader reader)
        {
            return new Ticket
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                LocationId = reader.GetInt32(reader.GetOrdinal("LocationId")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                Status = (TicketStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                Priority = (TicketPriority)reader.GetInt32(reader.GetOrdinal("Priority")),
                AssignedTo = reader.IsDBNull(reader.GetOrdinal("AssignedTo"))
                    ? 0
                    : reader.GetInt32(reader.GetOrdinal("AssignedTo")),
                TenantId = reader.IsDBNull(reader.GetOrdinal("TenantId"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("TenantId")),
                CreatedBy = reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("ModifiedBy"))
            };
        }

        public async Task Update(Ticket ticket)
        {
            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand("UpdateTicket", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", ticket.Id);

            CreateTicket(ticket, command);

            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 0)
                throw new KeyNotFoundException("Ticket not found.");
        }

        private static void CreateTicket(Ticket ticket, SqlCommand command)
        {
            command.Parameters.AddWithValue("@Title", ticket.Title);
            command.Parameters.AddWithValue("@Description", ticket.Description);
            command.Parameters.AddWithValue("@LocationId", ticket.LocationId);
            command.Parameters.AddWithValue("@DepartmentId", ticket.DepartmentId);
            command.Parameters.AddWithValue("@CategoryId", ticket.CategoryId);
            command.Parameters.AddWithValue("@Status", (int)ticket.Status);
            command.Parameters.AddWithValue("@Priority", (int)ticket.Priority);
            command.Parameters.AddWithValue("@AssignedTo", ticket.AssignedTo);
            command.Parameters.AddWithValue("@TenantId", ticket.TenantId);
            command.Parameters.AddWithValue("@CreatedBy", ticket.CreatedBy);
        }
    }
}
