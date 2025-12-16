using System.ComponentModel.DataAnnotations;
using PesquisasStartup.Dominio.Entidades;

namespace api.Domain.Model;

public class Resposta
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid PerguntaId { get; private set; }
    [Required]
    public char AlternativaSelecionada { get; private set; }
    
    public Pergunta Pergunta { get; private set; }

    public Resposta(Guid perguntaId, char alternativaSelecionada)
    {
        Id = Guid.NewGuid();
        PerguntaId = perguntaId;
        AlternativaSelecionada = alternativaSelecionada;
    }
}