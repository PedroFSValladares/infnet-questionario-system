using api.Domain.DTOs.Alternativa;

namespace api.Domain.DTOs.Pergunta;

public class PerguntaResponseDto
{
    public Guid Id { get; set; }
    public string Enunciado  { get; set; }
    public List<AlternativaResponseDto> Alternativas { get; set; }
}