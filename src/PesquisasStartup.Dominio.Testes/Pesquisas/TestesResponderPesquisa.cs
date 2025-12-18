using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Services;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesResponderPesquisa
{
    [Fact]
    public void TestaResponderPesquisaPublicadaComRespostasValidas_DeveObterSucesso()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        
        var pesquisa = PesquisaService.CriarPesquisa(
            cadastrador, 
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta);
        
        Assert.Equal(2, pesquisa.Respostas.Count);
        Assert.Contains(pesquisa.Respostas, res => res.Pergunta.Enunciado == "Pergunta 1");
        Assert.Contains(pesquisa.Respostas, res => res.Alternativa.Opcao == 'A');
        Assert.Contains(pesquisa.Respostas, res => res.Pergunta.Enunciado == "Pergunta 2");
        Assert.Contains(pesquisa.Respostas, res => res.Alternativa.Opcao == 'B');
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaDuasVezes_DeveFalhar()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        
        var pesquisa = PesquisaService.CriarPesquisa(
            cadastrador, 
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta);
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComMenosRespostasDoQuePerguntas_DeveFalhar()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        
        var pesquisa = PesquisaService.CriarPesquisa(
            cadastrador, 
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComMaisRespostasDoQuePerguntas_DeveFalhar()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        
        var pesquisa = PesquisaService.CriarPesquisa(
            cadastrador, 
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComRespostasRepetidas_DeveFalhar()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        
        var pesquisa = PesquisaService.CriarPesquisa(
            cadastrador, 
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        
        Assert.Throws<ArgumentException>(() => PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComPerguntaInexistente_DeveFalhar()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        
        var pesquisa = PesquisaService.CriarPesquisa(
            cadastrador, 
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        
        Assert.Throws<ArgumentException>(() => PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaPublicadaComAlternativaInexistente_DeveFalhar()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        
        var pesquisa = PesquisaService.CriarPesquisa(
            cadastrador, 
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        
        Assert.Throws<ArgumentException>(() => PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta));
    }
    
    [Fact]
    public void TestaResponderPesquisaNãoPublicada_DeveFalhar()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        
        var pesquisa = PesquisaService.CriarPesquisa(
            cadastrador, 
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.ResponderPesquisa(convidado, pesquisa, resposta));
    }
}