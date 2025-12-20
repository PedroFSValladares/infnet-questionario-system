using System.ComponentModel.DataAnnotations;

namespace PesquisaStartup.Api.Requests.RequestModels;

public class Resposta
{
    [Required]
    public string Pergunta  { get; set; }
    [Required]
    public char Opcao { get; set; }
}