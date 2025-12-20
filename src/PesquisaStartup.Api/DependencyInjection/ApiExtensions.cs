using Microsoft.EntityFrameworkCore;
using PesquisasStartup.Aplicacao.Services;
using PesquisasStartup.Dominio.Repositorios.Especializacoes;
using PesquisasStartup.Infraestrutura.Context;
using PesquisasStartup.Infraestrutura.Repositorios;

namespace PesquisaStartup.Api.DependencyInjection;

public static class ApiExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IPesquisaRepository, PesquisaRepository>();
        services.AddScoped<PesquisaService>();
    }

    public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PesquisasStartupContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddInMemrory(this IServiceCollection services)
    {
        services.AddDbContext<PesquisasStartupContext>(options =>
            options.UseInMemoryDatabase("PesquisasStartup"));
    }
}