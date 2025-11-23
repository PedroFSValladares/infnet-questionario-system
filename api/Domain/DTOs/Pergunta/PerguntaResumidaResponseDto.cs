using System.ComponentModel.DataAnnotations;

namespace api.Domain.DTOs.Pergunta;

public class PerguntaResumidaResponseDto
{
    public Guid Id { get; set; }
    public string Enunciado  { get; set; }
}