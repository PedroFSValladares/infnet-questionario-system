using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;
using api.Domain.Validation;

namespace api.Domain.DTOs.Pergunta;

public class IncluirPerguntaDto : PerguntaDto
{
    [Required(ErrorMessage = "Devem ser infomadas alternativas para a pergunta")]
    [MinLength(2, ErrorMessage = "Devem ser informadas ao menos duas alternativas.")]
    [OpcaoRepetidaValidation]
    public new List<IncluirAlternativaDto> Alternativas { get; set; }
}