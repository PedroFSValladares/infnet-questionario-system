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

    public static void AdicionarPerguntas(Pessoa pessoa, Pesquisa pesquisa, List<(string, List<(char opcao, string texto)>)> perguntas)
    {
        throw new NotImplementedException();
    }
    
    public static void AdicionarPergunta(Pessoa pessoa, Pesquisa pesquisa, string enunciado, List<(char opcao, string texto)> alternativas)
    {
        throw new NotImplementedException();
    }

    public static void RemoverPerguntas(Pessoa pessoa, Pesquisa pesquisa, List<string> enunciados)
    {
        throw new NotImplementedException();
    }
    
    public static void RemoverPergunta(Pessoa pessoa, Pesquisa pesquisa, string enunciado)
    {
        throw new NotImplementedException();
    }

    public static void AtualizarNome(Pessoa pessoa, string nome)
    {
        throw new NotImplementedException();
    }

    public static void MarcarPesquisaComoPronta(Pessoa pessoa, Pesquisa pesquisa)
    {
        if (pessoa.Perfil != Perfil.Cadastrador)
            throw new InvalidOperationException(
                "Para marcar uma pesquisa como pronta é necessário ter perfil de cadastrador");
        
        pesquisa.MarcarComoPronta(pessoa);
    }

    public static void PublicarPesquisa(Pessoa pessoa, Pesquisa pesquisa)
    {
        throw new NotImplementedException();
    }

    public static void FinalizarPesquisa(Pessoa pessoa, Pesquisa pesquisa)
    {
        throw new NotImplementedException();
    }

    public static void ResponderPesquisa(Pessoa pessoa, Pesquisa pesquisa)
    {
        throw new NotImplementedException();
    }
}