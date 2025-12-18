using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesAlterarInformacoesPesquisa
{
    [Fact]
    public void TestaAlterarNomeDePesquisaEmProducao_DeveObterSucesso()
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
                }),
                ("Pergunta 3", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" )
                })
            }
        );
        
        pesquisa.AtualizarNome("PesquisaXPTO2");
        
        Assert.Equal("PesquisaXPTO2", pesquisa.Nome);
    }
    
    [Fact]
    public void TestaAlterarNomeDePesquisaPublicada_DeveFalhar()
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
                }),
                ("Pergunta 3", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" )
                })
            }
        );
        
        pesquisa.PublicarPesquisa();
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.AtualizarNome("PesquisaXPTO2"));
    }
    
    [Fact]
    public void TestaAlterarNomeDePesquisaFinalizada_DeveFalhar()
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
                }),
                ("Pergunta 3", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" )
                })
            }
        );
        
        pesquisa.PublicarPesquisa();
        pesquisa.FinalizarPesquisa();
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.AtualizarNome("PesquisaXPTO2"));
    }
}