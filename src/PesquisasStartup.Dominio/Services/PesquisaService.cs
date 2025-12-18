using PesquisasStartup.Dominio.Entidades.Pesquisas;
using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Services;

public static class PesquisaService
{
    public static Pesquisa CriarPesquisa(Pessoa pessoa, string nome,
        List<(string, List<(char opcao, string texto)>)> perguntas)
    {
        if (pessoa.Perfil != Perfil.Cadastrador)
            throw new InvalidOperationException(
                "Para cadastrar uma nova pesquisa é necessário ter o perfil de cadastrador");
        
        return Pesquisa.CriarPesquisa(pessoa, nome, perguntas);
    }
    
    public static void AdicionarPergunta(Pessoa pessoa, Pesquisa pesquisa, string enunciado, List<(char opcao, string texto)> alternativas)
    {
        if (pessoa.Perfil > Perfil.Revisor)
            throw new InvalidOperationException(
                "Para alterar uma pesquisa é necessário ter o perfil de revisor ou cadastrador");
        
        pesquisa.AdicionarPergunta(enunciado, alternativas);
    }
    
    public static void RemoverPergunta(Pessoa pessoa, Pesquisa pesquisa, string enunciado)
    {
        if (pessoa.Perfil > Perfil.Revisor)
            throw new InvalidOperationException(
                "Para alterar uma pesquisa é necessário ter o perfil de revisor ou cadastrador");
        
        pesquisa.RemoverPergunta(enunciado);
    }

    public static void AtualizarNome(Pessoa pessoa, Pesquisa pesquisa, string nome)
    {
        if (pessoa.Perfil > Perfil.Revisor)
            throw new InvalidOperationException(
                "Para alterar uma pesquisa é necessário ter o perfil de revisor ou cadastrador");
        
        pesquisa.AtualizarNome(nome);
    }

    public static void MarcarPesquisaComoPronta(Pessoa pessoa, Pesquisa pesquisa)
    {
        if (pessoa.Perfil != Perfil.Revisor)
            throw new InvalidOperationException(
                "Para marcar uma pesquisa como pronta é necessário ter perfil de revisor");
        
        pesquisa.MarcarComoPronta(pessoa);
    }

    public static void PublicarPesquisa(Pessoa pessoa, Pesquisa pesquisa)
    {
        if (pessoa.Perfil != Perfil.Gestor)
            throw new InvalidOperationException(
                "Para publicar uma pesquisa é necessário ter perfil de gestor");
        
        pesquisa.PublicarPesquisa(pessoa);
    }

    public static void FinalizarPesquisa(Pessoa pessoa, Pesquisa pesquisa)
    {
        if(pessoa.Perfil != Perfil.Gestor)
            throw new InvalidOperationException(
                "Para publicar uma pesquisa é necessário ter perfil de gestor");
        
        pesquisa.FinalizarPesquisa(pessoa);
    }

    public static void ResponderPesquisa(Pessoa pessoa, Pesquisa pesquisa, List<(string pergunta, char alternativa)> respostas)
    {
        pesquisa.Responder(pessoa, respostas);
    }
}