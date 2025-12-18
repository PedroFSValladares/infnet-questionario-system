using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Services;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesAlterarInformacoesPesquisa
{
    [Fact]
    public void TestaAlterarNomePesquisaComPerfilDeCadastrador_DeveObterSucesso()
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
        
        PesquisaService.AtualizarNome(pessoa, pesquisa, "PesquisaXPTO2");
        
        Assert.Equal("PesquisaXPTO2", pesquisa.Nome);
    }
    
    [Fact]
    public void TestaAlterarNomePesquisaComPerfilDeRevisor_DeveObterSucesso()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
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
        
        PesquisaService.AtualizarNome(revisor, pesquisa, "PesquisaXPTO2");
        
        Assert.Equal("PesquisaXPTO2", pesquisa.Nome);
    }
    
    [Fact]
    public void TestaAlterarNomePesquisaComPerfilDeIncorreto_DeveFalhar()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
        var convidado = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Convidado);
        var admin = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Admin);
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
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.AtualizarNome(gestor, pesquisa, "PesquisaXPTO2"));
        Assert.Throws<InvalidOperationException>(() => PesquisaService.AtualizarNome(convidado, pesquisa, "PesquisaXPTO2"));
        Assert.Throws<InvalidOperationException>(() => PesquisaService.AtualizarNome(admin, pesquisa, "PesquisaXPTO2"));
        
    }
}