namespace PesquisasStartup.Dominio.Entidades;

public class Pergunta
{
    public string Enunciado { get; private set; }

    private List<Alternativa> _alternativas = [];
    public IReadOnlyList<Alternativa> Alternativas => _alternativas.AsReadOnly();

    private Pergunta(string enunciado)
    {
        Enunciado = enunciado;
    }
    
    public static Pergunta CriarPergunta(string enunciado, List<(char opcao, string texto)> alternativas)
    {
        if(string.IsNullOrEmpty(enunciado.Trim()))
            throw new ArgumentNullException(nameof(enunciado), "O Enunciado da pergunta deve ser informado.");

        if (alternativas.Count < 2)
            throw new ArgumentException(paramName: nameof(alternativas),
                message: "A pergunta deve conter ao menos duas alternativas.");
        
        Pergunta pergunta = new Pergunta(enunciado);
        alternativas.ForEach(alt => pergunta.AdicionarAlternativa(alt.opcao, alt.texto));
        
        return pergunta;
    }

    public void AdicionarAlternativa(char opcao, string texto)
    {
        if (_alternativas.Any(alt => alt.Opcao == opcao))
            throw new InvalidOperationException("A pergunta não pode ter alternativas repetidas.");
        _alternativas.Add(Alternativa.CriarAlternativa(opcao, texto));
    }

    public void RemoverAlternativa(char opcao, string texto)
    {
        var alternativaARemover = _alternativas.FirstOrDefault(alt => alt.Opcao == opcao && alt.Texto == texto);

        if (alternativaARemover == null)
            throw new InvalidOperationException("A alternativa informada para remoção não existe.");

        if (_alternativas.Count - 1 == 1)
            throw new InvalidOperationException("A pergunta deve ter mais de uma alternativa.");
        
        _alternativas.Remove(alternativaARemover);
    }
}