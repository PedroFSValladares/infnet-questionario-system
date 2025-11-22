using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;
using api.Domain.Model;
using api.Domain.Validation;

namespace api.Domain.DTOs.Pergunta;

public class IncluirPerguntaDto
{
    [Required(ErrorMessage = "O enunciado da pergunta deve ser informado.")]
    public string Enunciado  { get; set; }
    [Required(ErrorMessage = "Devem ser infomadas alternativas para a pergunta")]
    [MinLength(2, ErrorMessage = "Devem ser informadas ao menos duas alternativas.")]
    [OpcaoRepetidaValidation]
    public List<IncluirAlternativaDto> Alternativas { get; set; }

    public Model.Pergunta ToEntity()
    {
        return new Model.Pergunta(Enunciado, Alternativas.Select(a => a.ToEntity()).ToList());
    }
}