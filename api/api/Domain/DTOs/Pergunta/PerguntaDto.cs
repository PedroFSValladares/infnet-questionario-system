using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;
using api.Domain.Validation;

namespace api.Domain.DTOs.Pergunta;

public abstract class PerguntaDto
{
    [Required(ErrorMessage = "O enunciado da pergunta deve ser informado.")]
    public string Enunciado  { get; set; }
}