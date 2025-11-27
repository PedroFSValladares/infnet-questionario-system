using System.ComponentModel.DataAnnotations;
using api.Domain.Enuns;

namespace api.Domain.Model;

public class Pesquisa
{
    [Key] 
    private Guid _id;
    private string _nome;
    private StatusPesquisa _statusPesquisa;
    private DateTime? _dataDispnibilizacao;
    private DateTime? _dataFinalizacao;
    private List<Pergunta> _perguntas;

    public Guid Id
    {
        get => _id;
        set => _id = value;
    }

    public string Nome
    {
        get => _nome;
        set => _nome = value ?? throw new ArgumentNullException(nameof(value));
    }

    public StatusPesquisa StatusPesquisa
    {
        get => _statusPesquisa;
        set => _statusPesquisa = value;
    }

    public DateTime? DataDispnibilizacao
    {
        get => _dataDispnibilizacao;
        set => _dataDispnibilizacao = value;
    }

    public DateTime? DataFinalizacao
    {
        get => _dataFinalizacao;
        set => _dataFinalizacao = value;
    }

    public List<Pergunta> Perguntas
    {
        get => _perguntas;
        set => _perguntas = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public Pesquisa()
    {
        
    }

    public Pesquisa(Guid id)
    {
        _id = id;
    }
}