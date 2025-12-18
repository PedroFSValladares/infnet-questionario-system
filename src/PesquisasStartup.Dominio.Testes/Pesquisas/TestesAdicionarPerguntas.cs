using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesAdicionarPerguntas
{
    [Fact]
    public void TestaAdicionarPerguntaAPesquisaValida_DeveRetornarSucesso()
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

        pesquisa.AdicionarPergunta("Pergunta 3", new List<(char opcao, string texto)>
        {
            ('A',  "Alternativa A" ),
            ('B',  "Alternativa B" )
        });
        
        Assert.Equal(3, pesquisa.Perguntas.Count);
        Assert.Contains(pesquisa.Perguntas, pergunta => pergunta.Enunciado == "Pergunta 3");
    }
    
    [Fact]
    public void TestaAdicionarPerguntaComUmaAlternativa_DeveFalhar()
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

        Assert.Throws<ArgumentException>(() => pesquisa.AdicionarPergunta("Pergunta 3", new List<(char opcao, string texto)>
        {
            ('A',  "Alternativa A" )
        }));
    }
    
    [Fact]
    public void TestaAdicionarPerguntaComAlternativaRepetida_DeveFalhar()
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

        Assert.Throws<InvalidOperationException>(() => pesquisa.AdicionarPergunta("Pergunta 3", new List<(char opcao, string texto)>
        {
            ('A',  "Alternativa A" ),
            ('A',  "Alternativa A" )
        }));
    }
}