using System.ComponentModel.DataAnnotations;
using PesquisasStartup.Dominio.Entidades.Pessoas;

namespace PesquisasStartup.Dominio.Entidades.Pesquisas;

public class Resposta
{
    public Alternativa Alternativa { get; private set; }
    public Pergunta Pergunta { get; private set; }
    public Pessoa Pessoa { get; private set; }
    public DateTime Data { get; private set; }

    private Resposta(Pergunta pergunta, Alternativa alternativa, Pessoa pessoa)
    {
        Pergunta = pergunta;
        Pessoa = pessoa;
        Data = DateTime.Now;
        Alternativa = alternativa;
    }
    
    internal static Resposta CriarResposta(Pergunta pergunta, Alternativa alternativa, Pessoa pessoa)
    {
        return new Resposta(pergunta, alternativa, pessoa);
    }
}