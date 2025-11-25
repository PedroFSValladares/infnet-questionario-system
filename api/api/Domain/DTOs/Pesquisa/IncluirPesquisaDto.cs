using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Pergunta;

namespace api.Domain.DTOs.Pesquisa;

public class IncluirPesquisaDto : PesquisaDto
{
    [Required(ErrorMessage = "A pesquisa deve conter ao menos uma pergunta.")]
    public List<IncluirPerguntaDto> Perguntas { get; set; }
}