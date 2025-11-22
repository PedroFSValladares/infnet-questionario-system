using api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Context;

public class QuestionarioContext : DbContext
{
    public QuestionarioContext(DbContextOptions<QuestionarioContext> options) : base(options) { }
    
    public DbSet<Pergunta> Perguntas { get; set; }
    public DbSet<Resposta> Respostas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Pergunta>()
            .HasMany(p => p.Alternativas)
            .WithOne(a => a.Pergunta)
            .HasForeignKey(a => a.PerguntaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pergunta>()
            .HasMany(p => p.Respostas)
            .WithOne(a => a.Pergunta)
            .HasForeignKey(a => a.PerguntaId);
    }
}