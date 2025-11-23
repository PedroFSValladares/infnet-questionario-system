using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;
using api.Domain.Validation;

namespace api.Domain.DTOs.Pergunta;

public abstract class PerguntaDto
{
    [Required(ErrorMessage = "O enunciado da pergunta deve ser informado.")]
    public string Enunciado  { get; set; }
    [Required(ErrorMessage = "Devem ser infomadas alternativas para a pergunta")]
    [MinLength(2, ErrorMessage = "Devem ser informadas ao menos duas alternativas.")]
    [OpcaoRepetidaValidation]
    public List<AlternativaDto> Alternativas { get; set; }
}