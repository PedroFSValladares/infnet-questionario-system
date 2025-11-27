using api.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configurations;

public class PesquisaConfiguration : IEntityTypeConfiguration<Pesquisa>
{
    public void Configure(EntityTypeBuilder<Pesquisa> builder)
    {
        builder.HasMany(p => p.Perguntas)
            .WithOne(p => p.Pesquisa)
            .HasForeignKey(p => p.PesquisaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}