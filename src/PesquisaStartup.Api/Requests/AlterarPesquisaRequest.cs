using System.ComponentModel.DataAnnotations;
using PesquisaStartup.Api.Requests.RequestModels;

namespace PesquisaStartup.Api.Requests;

public class AlterarPesquisaRequest
{
    [Required]
    public string Nome { get; set; }
    [Required]
    [MinLength(2, ErrorMessage = "Devem ser informadas ao menos duas perguntas.")]
    public List<Pergunta> Perguntas { get; set; }
}