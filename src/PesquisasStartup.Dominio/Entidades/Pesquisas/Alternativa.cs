namespace PesquisasStartup.Dominio.Entidades;

public class Alternativa
{
    public char Opcao { get; private set; }

    public string Texto { get; private set; }

    private Alternativa(char opcao, string texto)
    {
        Opcao = opcao;
        Texto = texto;
    }

    public static Alternativa CriarAlternativa(char opcao, string texto)
    {
        return string.IsNullOrEmpty(texto.Trim()) ? throw new ArgumentNullException(nameof(texto), "O Texto da alternativa deve ser informado") : new Alternativa(opcao, texto);
    }
}