using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Services;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesAdicionarPerguntas
{
    [Fact]
    public void TestaAdicionarPerguntaAPesquisaComPerfilCorreto_DeveRetornarSucesso()
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
    public void TestaAdicionarPerguntaAPesquisaComPerfilIncorreto_DeveFalhar()
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
        pessoa.AtualizarPerfil(Perfil.Gestor);
        Assert.Throws<InvalidOperationException>(() => PesquisaService.AdicionarPergunta(pessoa, pesquisa, "Pergunta 3", new List<(char opcao, string texto)>
        {
            ('A',  "Alternativa A" ),
            ('B',  "Alternativa B" )
        }));
    }
}