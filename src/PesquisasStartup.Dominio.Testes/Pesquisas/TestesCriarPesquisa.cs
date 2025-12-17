using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Services;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesCriarPesquisa
{
    [Fact]
    public void TestaCriarPesquisaValida_DeveRetornarSucesso()
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
            });
        
        var situacaoPesquisa = pesquisa.Situacoes.FirstOrDefault();
        
        Assert.NotNull(pesquisa);
        Assert.NotEqual(Guid.Empty, pesquisa.Id);
        Assert.Equal("PesquisaXPTO", pesquisa.Nome);
        Assert.True(pesquisa.Perguntas.Count == 2);
        Assert.NotNull(pesquisa.Respostas);
        Assert.Single(pesquisa.Situacoes);
        Assert.NotNull(situacaoPesquisa);
        Assert.Equal(TipoSituacaoPesquisa.EmProducao, situacaoPesquisa.TipoSituacao);
        Assert.Equal(pessoa, situacaoPesquisa.Responsavel);
    }
    
    [Fact]
    public void TestaCriarPesquisaComNomeVazio_DeveFalhar()
    {
        Assert.Throws<ArgumentNullException>(() => PesquisaService.CriarPesquisa(
            Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador),
            "", 
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
                
            })
        );
    }

    [Fact]
    public void TestaCriarPesquisaComPerfilIncorreto_DeveFalhar()
    {
        Assert.Throws<InvalidOperationException>(() => PesquisaService.CriarPesquisa(
            Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado),
            "", 
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
                
            })
        );
    }
}