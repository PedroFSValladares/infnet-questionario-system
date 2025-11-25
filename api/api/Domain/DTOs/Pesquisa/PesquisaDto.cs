using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Pergunta;

namespace api.Domain.DTOs.Pesquisa;

public class PesquisaDto
{
    [Required(ErrorMessage = "Informe um nome para a Pesquisa")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "A pesquisa deve conter ao menos uma pergunta.")]
    public List<IncluirPerguntaDto> Perguntas { get; set; }
}