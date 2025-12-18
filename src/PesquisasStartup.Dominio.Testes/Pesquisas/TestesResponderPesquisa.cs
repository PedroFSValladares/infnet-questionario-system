using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesResponderPesquisa
{
    [Fact]
    public void TestaResponderPesquisaPublicadaComRespostasValidas_DeveObterSucesso()
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

        var resposta = new List<(string pergunta, char alternativa)>
        {
            ("Pergunta 1", 'A'),
            ("Pergunta 2", 'B')
        };
        
        pesquisa.PublicarPesquisa();
        pesquisa.Responder("88348380094" ,resposta);
        
        Assert.Equal(2, pesquisa.Respostas.Count);
        Assert.Contains(pesquisa.Respostas, res => res.Pergunta.Enunciado == "Pergunta 1");
        Assert.Contains(pesquisa.Respostas, res => res.Alternativa.Opcao == 'A');
        Assert.Contains(pesquisa.Respostas, res => res.Pergunta.Enunciado == "Pergunta 2");
        Assert.Contains(pesquisa.Respostas, res => res.Alternativa.Opcao == 'B');
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaDuasVezes_DeveFalhar()
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

        var resposta = new List<(string pergunta, char alternativa)>
        {
            ("Pergunta 1", 'A'),
            ("Pergunta 2", 'B')
        };
        
        pesquisa.PublicarPesquisa();
        pesquisa.Responder("88348380094", resposta);
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.Responder("88348380094", resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComMenosRespostasDoQuePerguntas_DeveFalhar()
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

        var resposta = new List<(string pergunta, char alternativa)>
        {
            ("Pergunta 2", 'B')
        };
        
        pesquisa.PublicarPesquisa();
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.Responder("88348380094", resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComMaisRespostasDoQuePerguntas_DeveFalhar()
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

        var resposta = new List<(string pergunta, char alternativa)>
        {
            ("Pergunta 1", 'A'),
            ("Pergunta 2", 'B'),
            ("Pergunta 3", 'B'),
        };
        
        pesquisa.PublicarPesquisa();
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.Responder("88348380094", resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComRespostasRepetidas_DeveFalhar()
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

        var resposta = new List<(string pergunta, char alternativa)>
        {
            ("Pergunta 2", 'A'),
            ("Pergunta 2", 'B'),
        };
        
        pesquisa.PublicarPesquisa();
        
        Assert.Throws<ArgumentException>(() => pesquisa.Responder("88348380094", resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComPerguntaInexistente_DeveFalhar()
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

        var resposta = new List<(string pergunta, char alternativa)>
        {
            ("Pergunta 1", 'A'),
            ("Pergunta 3", 'B')
        };
        
        pesquisa.PublicarPesquisa();
        
        Assert.Throws<ArgumentException>(() => pesquisa.Responder("88348380094", resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComAlternativaInexistente_DeveFalhar()
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

        var resposta = new List<(string pergunta, char alternativa)>
        {
            ("Pergunta 1", 'A'),
            ("Pergunta 2", 'E')
        };
        
        pesquisa.PublicarPesquisa();
        
        Assert.Throws<ArgumentException>(() => pesquisa.Responder("88348380094", resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaNãoPublicada_DeveFalhar()
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

        var resposta = new List<(string pergunta, char alternativa)>
        {
            ("Pergunta 1", 'A'),
            ("Pergunta 2", 'B')
        };
        
        Assert.Throws<InvalidOperationException>(() => pesquisa.Responder("88348380094", resposta));
    }
}