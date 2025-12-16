using PesquisasStartup.Dominio.Entidades;
using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Dominio.Testes;

public class TestesPesquisa
{
    [Fact]
    public void TestaCriarPesquisaValida_DeveRetornarSucesso()
    {
        var pesquisa = Pesquisa.CriarPesquisa("PesquisaXPTO", 
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
        
        Assert.NotNull(pesquisa);
        Assert.NotEqual(Guid.Empty, pesquisa.Id);
        Assert.Equal("PesquisaXPTO", pesquisa.Nome);
        Assert.True(pesquisa.Perguntas.Count == 2);
        Assert.NotNull(pesquisa.Respostas);
    }
    
    [Fact]
    public void TestaCriarPesquisaComNomeVazio_DeveFalhar()
    {
        Assert.Throws<ArgumentNullException>(() => Pesquisa.CriarPesquisa("", 
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
    public void TestaAdicionarPerguntaAPesquisa_DeveRetornarSucesso()
    {
        var pesquisa = Pesquisa.CriarPesquisa("PesquisaXPTO", 
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
    public void TestaRemoverPerguntaExistente_DeveRetornarSucesso()
    {
        var pesquisa = Pesquisa.CriarPesquisa("PesquisaXPTO", 
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
        var pesquisa = Pesquisa.CriarPesquisa("PesquisaXPTO", 
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
        var pesquisa = Pesquisa.CriarPesquisa("PesquisaXPTO", 
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