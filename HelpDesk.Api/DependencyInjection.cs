using HelpDesk.Repositories.Interfaces;
using HelpDesk.Repositories.Repositories;
using HelpDesk.Services.Interfaces;
using HelpDesk.Services.Services;

namespace HelpDesk.Api
{
    public class DependencyInjection
    {
        public static void Register(WebApplicationBuilder builder)
        {
            RegisterRepositories(builder);
            RegisterServices(builder);
        }

        private static void RegisterRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<ITeamRepository, TeamRepository>();
        }

        private static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
        }
    }
}
