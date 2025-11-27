using api.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations;

public class PerguntaConfiguration : IEntityTypeConfiguration<Pergunta>
{
    public void Configure(EntityTypeBuilder<Pergunta> builder)
    {
        builder.HasMany(p => p.Alternativas)
            .WithOne(a => a.Pergunta)
            .HasForeignKey(a => a.PerguntaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Respostas)
            .WithOne(a => a.Pergunta)
            .HasForeignKey(a => a.PerguntaId);
    }
}