using System.ComponentModel.DataAnnotations;

namespace api.Domain.Model;

public class Alternativa
{
    [Key]
    public Guid Id { get; private set; }
    public char Opcao { get; private set; }
    public string Texto { get; private set; }
    public Guid PerguntaId { get; private set; }
    
    public Pergunta Pergunta { get; private set; }
    
    public Alternativa(char opcao, string texto)
    {
        Id = Guid.NewGuid();
        Opcao = opcao;
        Texto = texto;
    }

    public Alternativa(Guid id, char opcao, string texto)
    {
        id = Id;
        Opcao = opcao;
        Texto = texto;
    }

    public override string ToString()
    {
        return $"{Opcao}. {Texto}";
    }
}