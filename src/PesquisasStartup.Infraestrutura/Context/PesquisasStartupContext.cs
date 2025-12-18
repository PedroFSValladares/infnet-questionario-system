using Microsoft.EntityFrameworkCore;
using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Entidades.Pessoas;

namespace PesquisasStartup.Infraestrutura.Context;

public class PesquisasStartupContext : DbContext
{
    public DbSet<Pesquisa> Pesquisas { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pesquisa>(pesquisa =>
        {
            pesquisa.HasKey(p => p.Id);
            pesquisa.OwnsMany(p => p.Perguntas)
                .OwnsMany(p => p.Alternativas);
            pesquisa.OwnsMany(p => p.Situacoes);
            pesquisa.OwnsMany(p => p.Respostas);
        });

        modelBuilder.Entity<Pessoa>(pessoa =>
        {
            pessoa.HasKey(p => p.Cpf);
        });
    }
}