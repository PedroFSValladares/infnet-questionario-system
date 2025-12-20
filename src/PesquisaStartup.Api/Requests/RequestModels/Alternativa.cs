using System.ComponentModel.DataAnnotations;

namespace PesquisaStartup.Api.Requests.RequestModels;

public class Alternativa
{
    [Required]
    public char Opcao { get; set; }
    [Required]
    public string Texto { get; set; }
}