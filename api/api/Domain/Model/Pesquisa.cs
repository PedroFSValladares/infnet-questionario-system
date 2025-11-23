using System.ComponentModel.DataAnnotations;
using api.Domain.Enuns;

namespace api.Domain.Model;

public class Pesquisa
{
    [Key]
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public StatusPesquisa StatusPesquisa { get; private set; }
    public DateTime? DataDispnibilizacao { get; private set; }
    public DateTime? DataFinalizacao { get; private set; }
    public List<Pergunta> Perguntas { get; private set; }

    public Pesquisa(string nome, List<Pergunta> perguntas)
    {
        Nome = nome;
        Perguntas = perguntas;
        Id = new Guid();
        StatusPesquisa = StatusPesquisa.Criada;
    }

    public Pesquisa(Guid id, string nome, StatusPesquisa statusPesquisa, DateTime? dataDispnibilizacao,
        DateTime? dataFinalizacao, List<Pergunta>? perguntas)
    {
        Id = id;
        Nome = nome;
        StatusPesquisa = statusPesquisa;
        DataDispnibilizacao = dataDispnibilizacao;
        DataFinalizacao = dataFinalizacao;
        Perguntas = perguntas;
    }

    public Pesquisa(Guid id, string nome, StatusPesquisa statusPesquisa, List<Pergunta> perguntas)
    {
        Id = id;
        Nome = nome;
        StatusPesquisa = statusPesquisa;
        Perguntas = perguntas;
    }
    
    public Pesquisa(){}
}