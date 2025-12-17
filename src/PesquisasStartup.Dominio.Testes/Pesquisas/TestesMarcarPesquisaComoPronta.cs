using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Services;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesMarcarPesquisaComoPronta
{
    [Fact]
    public void TestaMarcarPesquisaComoProntaComPerfilDeCadastrador_DeveObterSucesso()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var pesquisa = PesquisaService.CriarPesquisa(
            pessoa,
            "PesquisaXPTO", 
            new List<(string, List<(char opcao, string texto)>)>
            {
                ("Pergunta 1", new List<(char, string)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" ),
                }),
                ("Pergunta 2", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" ),
                })
            }
        );
        
        PesquisaService.MarcarPesquisaComoPronta(pessoa, pesquisa);
        
        Assert.Equal(2, pesquisa.Situacoes.Count);
        Assert.Equal(TipoSituacaoPesquisa.Pronta, pesquisa.Situacoes.Last().TipoSituacao);
    }

    [Fact]
    public void TestaMarcarPesquisaComoProntaSemPerfilDeCadastrador_DeveFalhar()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var pesquisa = PesquisaService.CriarPesquisa(
            pessoa,
            "PesquisaXPTO", 
            new List<(string, List<(char opcao, string texto)>)>
            {
                ("Pergunta 1", new List<(char, string)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" ),
                }),
                ("Pergunta 2", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" ),
                })
            }
        );
        
        pessoa.AtualizarPerfil(Perfil.Gestor);
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.MarcarPesquisaComoPronta(pessoa, pesquisa));
    }
}