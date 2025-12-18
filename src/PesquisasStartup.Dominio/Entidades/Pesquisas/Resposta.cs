namespace PesquisasStartup.Dominio.Entidades.Pesquisas;

public class Resposta
{
    public Alternativa Alternativa { get; private set; }
    public Pergunta Pergunta { get; private set; }
    public Cpf CpfPessoa { get; private set; }
    public DateTime Data { get; private set; }

    private Resposta(Pergunta pergunta, Alternativa alternativa, Cpf cpfPessoa)
    {
        Pergunta = pergunta;
        CpfPessoa = cpfPessoa;
        Data = DateTime.Now;
        Alternativa = alternativa;
    }
    
    public static Resposta CriarResposta(Pergunta pergunta, Alternativa alternativa, Cpf pessoa)
    {
        return new Resposta(pergunta, alternativa, pessoa);
    }
}