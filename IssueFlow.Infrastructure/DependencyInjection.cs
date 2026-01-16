using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IssueFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IssueFlowDbContext>(options => options.UseSqlServer(connectionString));

        // Add other services in the future like:
        // services.AddTransient<IEmailSender, SmtpEmailSender>();
        // services.AddScoped<ISlackMessageSender, SlackMessageSender>();

        return services;
    }
}
