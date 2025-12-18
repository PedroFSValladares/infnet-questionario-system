using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Services;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesRemoverPerguntas
{
    [Fact]
    public void TestaRemoverPerguntaExistente_DeveRetornarSucesso()
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
                }),
                ("Pergunta 3", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" )
                })
            }
        );
        
        PesquisaService.RemoverPergunta(pessoa, pesquisa, "Pergunta 3");
        
        Assert.Equal(2, pesquisa.Perguntas.Count);
        Assert.DoesNotContain(pesquisa.Perguntas, pergunta => pergunta.Enunciado == "Pergunta 3");
    }
    
    [Fact]
    public void TestaRemoverPerguntaInexistente_DeveFalhar()
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
                }),
                ("Pergunta 3", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" )
                })
            }
        );
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.RemoverPergunta(pessoa, pesquisa, "Pergunta 4"));
    }
    
    [Fact]
    public void TestaDeixarPesquisaComUmaPergunta_DeveFalhar()
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
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.RemoverPergunta(pessoa, pesquisa, "Pergunta 2"));
    }
    
    [Fact]
    public void TestaRemoverPerguntaAPesquisaComoCadastrador_DeveRetornarSucesso()
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
                }),
                ("Pergunta 3", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" )
                })
            }
        );

        PesquisaService.RemoverPergunta(pessoa, pesquisa, "Pergunta 3");
        
        Assert.Equal(2, pesquisa.Perguntas.Count);
        Assert.DoesNotContain(pesquisa.Perguntas, pergunta => pergunta.Enunciado == "Pergunta 3");
    }

    [Fact]
    public void TestaRemoverPerguntaAPesquisaComoRevisor_DeveRetornarSucesso()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa 2", Perfil.Revisor);
        var pesquisa = PesquisaService.CriarPesquisa(
            pessoa,
            "PesquisaXPTO",
            new List<(string, List<(char opcao, string texto)>)>
            {
                ("Pergunta 1", new List<(char, string)>
                {
                    ('A', "Alternativa A"),
                    ('B', "Alternativa B"),
                }),
                ("Pergunta 2", new List<(char opcao, string texto)>
                {
                    ('A', "Alternativa A"),
                    ('B', "Alternativa B"),
                }),
                ("Pergunta 3", new List<(char opcao, string texto)>
                {
                    ('A',  "Alternativa A" ),
                    ('B',  "Alternativa B" )
                })
            }
        );
        
        PesquisaService.RemoverPergunta(revisor, pesquisa, "Pergunta 3");
        
        Assert.Equal(2, pesquisa.Perguntas.Count);
        Assert.DoesNotContain(pesquisa.Perguntas, pergunta => pergunta.Enunciado == "Pergunta 3");
    }
}