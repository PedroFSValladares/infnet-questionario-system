using api.Domain.DTOs.Alternativa;
using api.Domain.DTOs.Pergunta;
using api.Domain.DTOs.Pesquisa;

namespace tests;

public static class PesquisaConverter
{
    public static AlterarPesquisaDto ConverterEntidadeParaAlterarPesquisaDto(api.Domain.Model.Pesquisa pesquisa)
    {
        return new AlterarPesquisaDto
        {
            Id = pesquisa.Id,
            Nome = pesquisa.Nome,
            StatusPesquisa = pesquisa.StatusPesquisa,
            Perguntas = pesquisa.Perguntas.Select(p => new AlterarPerguntaDto()
            {
                Id = p.Id,
                Enunciado = p.Enunciado,
                Alternativas = p.Alternativas.Select(a => new AlterarAlternativaDto
                {
                    Id = a.Id,
                    Opcao = a.Opcao,
                    Texto = a.Texto
                }).ToList()
            }).ToList()
        };
    }
}