using api.Domain.Model;
using PesquisasStartup.Dominio.Entidades;

namespace PesquisasStartup.Dominio.Testes;

public class TestesAlternativa
{
    [Fact]
    public void TestaCriarAlternativaValida_DeveObterSucesso()
    {
        var alternativa = Alternativa.CriarAlternativa('A', "Alternativa A");
        
        Assert.NotNull(alternativa);
        Assert.Equal('A', alternativa.Opcao);
        Assert.Equal("Alternativa A", alternativa.Texto);
    }
    
    [Fact]
    public void TestaCriarAlternativaComTextoVazio_DeveFalhar()
    {
        Assert.Throws<ArgumentNullException>(() => Alternativa.CriarAlternativa('A', "  "));
    }
}