using System.ComponentModel.DataAnnotations;
using PesquisaStartup.Api.Requests.RequestModels;

namespace PesquisaStartup.Api.Requests;

public class RespostaRequest
{
    [Required]
    [MinLength(11, ErrorMessage = "Cpf informado é muito curto"), MaxLength(11, ErrorMessage = "Cpf informado é muito longo")]
    public string Cpf { get; set; }
    [Required]
    public List<Resposta> Respostas { get; set; }
}