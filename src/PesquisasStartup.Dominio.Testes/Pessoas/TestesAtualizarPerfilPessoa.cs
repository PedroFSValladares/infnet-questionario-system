using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Testes.Pessoas;

public class TestesAtualizarPerfilPessoa
{
    [Fact]
    public void TestaAtualizarPerfilPessoa_DeveObterSucesso()
    {
        var pessoa = Pessoa.CriarPessoa("60669257095", "José", Perfil.Admin);
        
        pessoa.AtualizarPerfil(Perfil.Convidado);
        
        Assert.Equal(Perfil.Convidado, pessoa.Perfil);
    }
}