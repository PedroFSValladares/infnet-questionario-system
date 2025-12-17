using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Testes.Pessoas;

public class TestesAtualizarNomePessoa
{
    [Fact]
    public void TestaAtualizarNomePessoaComNomeValido_DeveObterSucesso()
    {
        var pessoa = Pessoa.CriarPessoa("60669257095", "José", Perfil.Admin);
        
        pessoa.AtualizarNome("Carlos");
        
        Assert.Equal("Carlos", pessoa.Nome);
    }
    
    [Fact]
    public void TestaAtualizarNomePessoaComNomeVazio_DeveFalhar()
    {
        var pessoa = Pessoa.CriarPessoa("60669257095", "José", Perfil.Admin);
        
        Assert.Throws<ArgumentNullException>(() => pessoa.AtualizarNome(""));
    }
}