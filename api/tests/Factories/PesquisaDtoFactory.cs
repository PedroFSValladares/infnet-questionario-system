using api.Domain.DTOs.Alternativa;
using api.Domain.DTOs.Pergunta;
using api.Domain.DTOs.Pesquisa;
using api.Domain.Enuns;

namespace tests.Factories;

public class PesquisaDtoFactory
{
    public static IncluirPesquisaDto CreateIncluirPesquisaDto()
    {
        return new IncluirPesquisaDto
        {
            Nome = "Pesquisa XYZ",
            Perguntas = new List<IncluirPerguntaDto>
            {
                new IncluirPerguntaDto
                {
                    Enunciado = "abcd",
                    Alternativas = new List<IncluirAlternativaDto>
                    {
                        new IncluirAlternativaDto
                        {
                            Opcao = 'A',
                            Texto = "teste"
                        },
                        new IncluirAlternativaDto
                        {
                            Opcao = 'B',
                            Texto = "abcd"
                        }
                    }
                },
                new IncluirPerguntaDto
                {
                    Enunciado = "abcd",
                    Alternativas = new List<IncluirAlternativaDto>
                    {
                        new IncluirAlternativaDto
                        {
                            Opcao = 'A',
                            Texto = "teste"
                        },
                        new IncluirAlternativaDto
                        {
                            Opcao = 'B',
                            Texto = "abcd"
                        }
                    }
                },
                new IncluirPerguntaDto
                {
                    Enunciado = "abcd",
                    Alternativas = new List<IncluirAlternativaDto>
                    {
                        new IncluirAlternativaDto
                        {
                            Opcao = 'A',
                            Texto = "teste"
                        },
                        new IncluirAlternativaDto
                        {
                            Opcao = 'B',
                            Texto = "abcd"
                        }
                    }
                }
            }
        };
    }

    public static AlterarPesquisaDto CreateAlterarPesquisaDto(api.Domain.Model.Pesquisa pesquisa)
    {
        return new AlterarPesquisaDto
        {
            Nome = pesquisa.Nome,
            Perguntas = pesquisa.Perguntas.Select(p => new AlterarPerguntaDto
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