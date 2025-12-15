using System.ComponentModel.DataAnnotations;
using api.Domain.Model;

namespace PesquisasStartup.Dominio.Entities;

public class Pergunta
{
    private Guid id;
    private string enunciado;
    private List<Alternativa> alternativas;
    private Guid pesquisaId;
    private Pesquisa pesquisa;
    private List<Resposta> respostas;

    public Guid Id
    {
        get => id;
        set => id = value;
    }

    public string Enunciado
    {
        get => enunciado;
        set => enunciado = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Alternativa> Alternativas
    {
        get => alternativas;
        set => alternativas = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Guid PesquisaId
    {
        get => pesquisaId;
        set => pesquisaId = value;
    }

    public Pesquisa Pesquisa
    {
        get => pesquisa;
        set => pesquisa = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Resposta> Respostas
    {
        get => respostas;
        set => respostas = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Pergunta()
    {
        Id = Guid.NewGuid();
    }

    public Pergunta(Guid id)
    {
        Id = id;
    }

    public override string ToString()
    {
        string alternativas = Alternativas.Select(a => a.ToString()).Aggregate((a, b) => $"{a} | {b}");
        return $"{Id} | {Enunciado} | {alternativas}";
    }
}