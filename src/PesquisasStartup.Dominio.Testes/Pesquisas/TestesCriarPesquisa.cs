using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesCriarPesquisa
{
    [Fact]
    public void TestaCriarPesquisaValida_DeveRetornarSucesso()
    {
        var pesquisa = Pesquisa.CriarPesquisa(
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
    }
    
    [Fact]
    public void TestaCriarPesquisaComNomeVazio_DeveFalhar()
    {
        Assert.Throws<ArgumentNullException>(() => Pesquisa.CriarPesquisa(
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
    public void TestaCriarPesquisaComUmaAlternativa_DeveFalhar()
    {
        Assert.Throws<ArgumentNullException>(() => Pesquisa.CriarPesquisa(
            "PesquisaXPTO", 
            new List<(string, List<(char opcao, string texto)>)>
            {
                ("Pergunta 1", new List<(char, string)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" ),
                })
            })
        );
    }
    
    [Fact]
    public void TestaCriarPesquisaComAlternativaRepetida_DeveFalhar()
    {
        Assert.Throws<InvalidOperationException>(() => Pesquisa.CriarPesquisa(
            "PesquisaXPTO", 
            new List<(string, List<(char opcao, string texto)>)>
            {
                ("Pergunta 1", new List<(char, string)>
                {
                    ('A',  "Alternativa A" ),
                    ('A',  "Alternativa B" ),
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