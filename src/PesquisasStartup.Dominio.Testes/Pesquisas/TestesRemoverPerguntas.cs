using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesRemoverPerguntas
{
    [Fact]
    public void TestaRemoverPerguntaExistente_DeveRetornarSucesso()
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
        
        pesquisa.RemoverPergunta("Pergunta 3");
        
        Assert.Equal(2, pesquisa.Perguntas.Count);
        Assert.DoesNotContain(pesquisa.Perguntas, pergunta => pergunta.Enunciado == "Pergunta 3");
    }
    
    [Fact]
    public void TestaRemoverPerguntaInexistente_DeveFalhar()
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
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.RemoverPergunta("Pergunta 4"));
    }
    
    [Fact]
    public void TestaDeixarPesquisaComUmaPergunta_DeveFalhar()
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
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.RemoverPergunta("Pergunta 2"));
    }
}