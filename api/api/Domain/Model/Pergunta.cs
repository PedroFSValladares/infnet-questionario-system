using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;
using api.Domain.DTOs.Pergunta;

namespace api.Domain.Model;

public class Pergunta
{
    [Key] private Guid id;
    [Required] private string enunciado;
    [Required] private List<Alternativa> alternativas;
    private Guid pesquisaId;
    private Pesquisa pesquisa;
    private List<Resposta> respostas;

    public Guid Id
    {
        get => id;
        set => id = value;
    }

    public string Enunciado
    {
        get => enunciado;
        set => enunciado = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Alternativa> Alternativas
    {
        get => alternativas;
        set => alternativas = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Guid PesquisaId
    {
        get => pesquisaId;
        set => pesquisaId = value;
    }

    public Pesquisa Pesquisa
    {
        get => pesquisa;
        set => pesquisa = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Resposta> Respostas
    {
        get => respostas;
        set => respostas = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Pergunta()
    {
        Id = Guid.NewGuid();
    }

    public Pergunta(Guid id)
    {
        Id = id;
    }

    public override string ToString()
    {
        string alternativas = Alternativas.Select(a => a.ToString()).Aggregate((a, b) => $"{a} | {b}");
        return $"{Id} | {Enunciado} | {alternativas}";
    }

    public PerguntaResponseDto ToResponseDto()
    {
        return new PerguntaResponseDto()
        {
            Id = Id,
            Enunciado = Enunciado,
            Alternativas = Alternativas.Select(alternativa =>
                new AlternativaResponseDto()
                {
                    Opcao = alternativa.Opcao,
                    Texto = alternativa.Texto

                }).ToList()
        };
    }

    public PerguntaResumidaResponseDto ToPerguntaResumidaResponseDto()
    {
        return new PerguntaResumidaResponseDto
        {
            Id = Id,
            Enunciado = Enunciado
        };
    }
}