using IssueFlow.Application.Auth;
using IssueFlow.Application.Issues;
using IssueFlow.Application.Organizations;
using IssueFlow.Application.Profiles;
using IssueFlow.Application.Projects;
using IssueFlow.Application.Repositories;
using IssueFlow.Infrastructure.Identity;
using IssueFlow.Infrastructure.Persistence;
using IssueFlow.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IssueFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new InvalidOperationException("Database connection string is not configured.");
        services.AddDbContext<IssueFlowDbContext>(options => options.UseSqlServer(connectionString));

        // Add other services in the future like:
        // services.AddTransient<IEmailSender, SmtpEmailSender>();
        // services.AddScoped<ISlackMessageSender, SlackMessageSender>();

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<IssueFlowDbContext>()
        .AddDefaultTokenProviders();

        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key is not configured.");
        var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured.");
        var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

        // Auth Services
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthService, IdentityAuthService>();

        // Repositories
        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<IIssuePriorityRepository, IssuePriorityRepository>();
        services.AddScoped<IIssueStatusRepository, IssueStatusRepository>();
        services.AddScoped<IIssueTypeRepository, IssueTypeRepository>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();

        // Services
        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IIssuePriorityService, IssuePriorityService>();
        services.AddScoped<IIssueStatusService, IssueStatusService>();
        services.AddScoped<IIssueTypeService, IssueTypeService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IProjectService, ProjectService>();

        return services;
    }
}
