using System.ComponentModel.DataAnnotations;

namespace api.Domain.Model;

public class Resposta
{
    [Key]
    public Guid Id { get; private set; }
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