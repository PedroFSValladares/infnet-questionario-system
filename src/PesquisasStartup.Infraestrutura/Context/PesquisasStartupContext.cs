using Microsoft.EntityFrameworkCore;
using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Infraestrutura.Context;

public class PesquisasStartupContext : DbContext
{
    
    public PesquisasStartupContext(DbContextOptions options) :base(options){}
    
    public DbSet<Pesquisa> Pesquisas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pesquisa>(pesquisa =>
        {
            pesquisa.HasKey(p => p.Id);
            pesquisa.OwnsMany(p => p.Perguntas)
                .OwnsMany(p => p.Alternativas);
            pesquisa.OwnsMany(p => p.Situacoes);
            pesquisa.OwnsMany(p => p.Respostas)
                .OwnsOne(r => r.CpfPessoa);
        });

        modelBuilder.Entity<Pesquisa>().Navigation(p => p.Situacoes).UsePropertyAccessMode(PropertyAccessMode.Field);
        modelBuilder.Entity<Pesquisa>().Navigation(p => p.Perguntas).UsePropertyAccessMode(PropertyAccessMode.Field);
        modelBuilder.Entity<Pesquisa>().Navigation(p => p.Respostas).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}