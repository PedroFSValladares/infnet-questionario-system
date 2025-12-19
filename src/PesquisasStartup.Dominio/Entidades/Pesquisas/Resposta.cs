namespace PesquisasStartup.Dominio.Entidades.Pesquisas;

public class Resposta
{
    public char Alternativa { get; private set; }
    public string Pergunta { get; private set; }
    public Cpf CpfPessoa { get; private set; }
    public DateTime Data { get; private set; }

    private Resposta() { }
    
    private Resposta(string pergunta, char alternativa, Cpf cpfPessoa)
    {
        Pergunta = pergunta;
        CpfPessoa = cpfPessoa;
        Data = DateTime.Now;
        Alternativa = alternativa;
    }
    
    public static Resposta CriarResposta(string pergunta, char alternativa, Cpf pessoa)
    {
        return new Resposta(pergunta, alternativa, pessoa);
    }
}