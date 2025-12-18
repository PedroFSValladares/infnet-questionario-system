using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;
using PesquisasStartup.Dominio.Services;

namespace PesquisasStartup.Dominio.Testes.Pesquisas;

public class TestesFinalizarPesquisa
{
    [Fact]
    public void TestaFinalizarPesquisaComPerfilDeGestor_DeveObterSucesso()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        PesquisaService.FinalizarPesquisa(gestor, pesquisa);
        
        Assert.Equal(4, pesquisa.Situacoes.Count);
        Assert.Equal(TipoSituacaoPesquisa.Finalizada, pesquisa.Situacoes.Last().TipoSituacao);
    }
    
    [Fact]
    public void TestaFinalizarPesquisaSemPublicar_DeveFalhar()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
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
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.FinalizarPesquisa(gestor, pesquisa));
    }

    [Fact]
    public void TestaFinalizarPesquisaSemPerfilDeGestor_DeveFalhar()
    {
        var pessoa = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Cadastrador);
        var revisor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Revisor);
        var gestor = Pessoa.CriarPessoa("84637291003", "Pessoa falsa", Perfil.Gestor);
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
        
        PesquisaService.MarcarPesquisaComoPronta(revisor, pesquisa);
        PesquisaService.PublicarPesquisa(gestor, pesquisa);
        
        Assert.Throws<InvalidOperationException>(() => PesquisaService.FinalizarPesquisa(pessoa, pesquisa));
    }
}