using FluentValidation;
using IssueFlow.Application.Auth.Validators;
using IssueFlow.Infrastructure;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;


var builder = WebApplication.CreateBuilder(args);

// AddInfrastructure is defined in IssueFlow.Infrastructure.DependencyInjection.cs
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>(ServiceLifetime.Transient);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
