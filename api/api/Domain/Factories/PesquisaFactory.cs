using api.Domain.DTOs.Pesquisa;
using api.Domain.Model;

namespace api.Domain.Factories;

public static class PesquisaFactory
{
    public static Pesquisa CriarPesquisa(IncluirPesquisaDto incluirPesquisaDto)
    {
        Pesquisa pesquisa = new Pesquisa
        {
            Nome = incluirPesquisaDto.Nome,
            Perguntas = incluirPesquisaDto.Perguntas.Select(PerguntaFactory.NovaPergunta).ToList()
        };

        return pesquisa;
    }
}