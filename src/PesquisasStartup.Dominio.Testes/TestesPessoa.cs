using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Testes;

public class TestesPessoa
{
    [Fact]
    public void TestaCriarPessoaComCpfValido_DeveRetornarSucesso()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "José", Perfil.Cadastrador);
        
        Assert.NotNull(pessoa);
        Assert.Equal("84637291003", pessoa.Cpf);
        Assert.Equal("José", pessoa.Nome);
        Assert.Equal(Perfil.Cadastrador, pessoa.Perfil);
    }
    
    [Fact]
    public void TestaCriarPessoaComCpfDeNumerosAleatorios_DeveFalhar() =>
        Assert.Throws<ArgumentException>(() => Pessoa.CriarPessoa("12385462045", "José", Perfil.Cadastrador));
    
    [Fact]
    public void TestaCriarPessoaComCpfDeNumerosRepetidos_DeveFalhar() => 
        Assert.Throws<ArgumentException>(() => Pessoa.CriarPessoa("11111111111", "José", Perfil.Cadastrador));
}