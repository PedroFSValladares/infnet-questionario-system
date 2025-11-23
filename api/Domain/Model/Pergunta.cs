using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;
using api.Domain.DTOs.Pergunta;

namespace api.Domain.Model;

public class Pergunta
{
    [Key]
    public Guid Id { get; private set; }
    [Required]
    public string Enunciado  { get; private set; }
    [Required]
    public List<Alternativa> Alternativas { get; private set; }
    
    
    public List<Resposta> Respostas { get; private set; }

    public Pergunta(string enunciado, List<Alternativa> alternativas)
    {
        Id = Guid.NewGuid();
        Enunciado = enunciado;
        Alternativas = alternativas;
    }

    public Pergunta(Guid id, string enunciado, List<Alternativa> alternativas)
    {
        Id = id;
        Enunciado = enunciado;
        Alternativas = alternativas;
    }
    
    public Pergunta(){}

    public void AtualizarEnunciado(string enunciado)
    {
        Enunciado = enunciado;
    }

    public void AtualizarAlternativas(List<Alternativa> alternativas)
    {
        Alternativas = alternativas;
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