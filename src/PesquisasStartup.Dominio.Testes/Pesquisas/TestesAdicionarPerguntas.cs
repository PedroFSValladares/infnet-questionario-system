using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Services;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesAdicionarPerguntas
{
    [Fact]
    public void TestaAdicionarPerguntaAPesquisaComoCadastrador_DeveRetornarSucesso()
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

        PesquisaService.AdicionarPergunta(pessoa, pesquisa, "Pergunta 3", new List<(char opcao, string texto)>
        {
            ('A',  "Alternativa A" ),
            ('B',  "Alternativa B" )
        });
        
        Assert.Equal(3, pesquisa.Perguntas.Count);
        Assert.Contains(pesquisa.Perguntas, pergunta => pergunta.Enunciado == "Pergunta 3");
    }

    [Fact]
    public void TestaAdicionarPerguntaAPesquisaComoRevisor_DeveRetornarSucesso()
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
                })
            }
        );
        
        PesquisaService.AdicionarPergunta(revisor, pesquisa, "Pergunta 3", new List<(char opcao, string texto)>
        {
            ('A',  "Alternativa A" ),
            ('B',  "Alternativa B" )
        });
        
        Assert.Equal(3, pesquisa.Perguntas.Count);
        Assert.Contains(pesquisa.Perguntas, pergunta => pergunta.Enunciado == "Pergunta 3");
    }

    [Fact]
    public void TestaAdicionarPerguntaAPesquisaComPerfilIncorreto_DeveFalhar()
    {
        var cadastrador = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        var admin = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Admin);
        var enunciadoPergunta = "Pergunta 3";
        var alternativas = new List<(char opcao, string texto)>
        {
            ('A', "Alternativa A"),
            ('B', "Alternativa B")
        };
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

        Assert.Throws<InvalidOperationException>(() => PesquisaService.AdicionarPergunta(gestor, pesquisa, enunciadoPergunta, alternativas));
        Assert.Throws<InvalidOperationException>(() => PesquisaService.AdicionarPergunta(convidado, pesquisa, enunciadoPergunta, alternativas));
        Assert.Throws<InvalidOperationException>(() => PesquisaService.AdicionarPergunta(admin, pesquisa, enunciadoPergunta, alternativas));
    }
}