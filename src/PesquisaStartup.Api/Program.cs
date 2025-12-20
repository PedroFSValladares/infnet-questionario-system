using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PesquisasStartup.Infraestrutura.Context;
using PesquisaStartup.Api.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDomainServices();
builder.Services.AddSqlServer(builder.Configuration);
//builder.Services.AddInMemrory();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetService<PesquisasStartupContext>();
        context.Database.Migrate();
    }
    catch (SqlException e)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogWarning(e, "A migração do banco não foi executada pois o banco já existe.");
    }
    catch (Exception e)
    {
        var logger =  services.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "Ocorreu um erro ao executar a migração do banco.");
    }
}

app.MapControllers();

app.UseHttpsRedirection();


app.Run();
