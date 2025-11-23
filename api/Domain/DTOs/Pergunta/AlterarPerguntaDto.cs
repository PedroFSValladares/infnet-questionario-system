using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;
using api.Domain.Validation;

namespace api.Domain.DTOs.Pergunta;

public class AlterarPerguntaDto : PerguntaDto
{
    [Required(ErrorMessage = "É necessário informar o ID da pergunta.")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "É necessário informar ao menos duas altenartivas")]
    [MinLength(2, ErrorMessage = "É necessário informar ao menos duas altenartivas")]
    [OpcaoRepetidaValidation]
    public new List<AlterarAlternativaDto> Alternativas { get; set; }

    public virtual Model.Pergunta ToEntity()
    {
        return new Model.Pergunta(Id, Enunciado, Alternativas.Select(a => a.ToEntity()).ToList());
    }
}