using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesPublicarPesquisa
{
    [Fact]
    public void TestaPublicarPesquisaFinalizada_DeveFalhar()
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
            }
        );
        
        pesquisa.PublicarPesquisa();
        pesquisa.FinalizarPesquisa();
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.PublicarPesquisa());
    }
    
    [Fact]
    public void TestaPublicarPesquisaEmProducao_DeveObterSucesso()
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
            }
        );

        pesquisa.PublicarPesquisa();
        
        Assert.Equal(2, pesquisa.Situacoes.Count);
        Assert.Equal(TipoSituacaoPesquisa.Publicada, pesquisa.Situacoes.Last().TipoSituacao);
    }
}