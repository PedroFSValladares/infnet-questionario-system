using System.ComponentModel.DataAnnotations;

namespace PesquisaStartup.Api.Requests.RequestModels;

public class Pergunta
{
    [Required]
    public string Enunciado { get; set; }
    [Required]
    public List<Alternativa> Alternativas { get; set; }
}