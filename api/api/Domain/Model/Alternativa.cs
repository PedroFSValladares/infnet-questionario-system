using System.ComponentModel.DataAnnotations;

namespace api.Domain.Model;

public class Alternativa
{
    [Key] private Guid id;
    private char opcao;
    private string texto;
    private Guid perguntaId;
    private Pergunta pergunta;
    
    public Alternativa(Guid id)
    {
        Id = id;
    }

    public Alternativa()
    {
        
    }

    public Guid Id
    {
        get => id;
        set => id = value;
    }

    public char Opcao
    {
        get => opcao;
        set => opcao = value;
    }

    public string Texto
    {
        get => texto;
        set => texto = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Guid PerguntaId
    {
        get => perguntaId;
        set => perguntaId = value;
    }

    public Pergunta Pergunta
    {
        get => pergunta;
        set => pergunta = value ?? throw new ArgumentNullException(nameof(value));
    }

    public override string ToString()
    {
        return $"{Opcao}.) {Texto}";
    }
}