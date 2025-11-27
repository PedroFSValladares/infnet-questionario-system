using api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Context;

public class QuestionarioContext : DbContext
{
    public QuestionarioContext(DbContextOptions<QuestionarioContext> options) : base(options) { }
    
    public DbSet<Pergunta> Perguntas { get; set; }
    public DbSet<Alternativa> Alternativas { get; set; }
    public DbSet<Resposta> Respostas { get; set; }
    public DbSet<Pesquisa> Pesquisas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestionarioContext).Assembly);
    }
}