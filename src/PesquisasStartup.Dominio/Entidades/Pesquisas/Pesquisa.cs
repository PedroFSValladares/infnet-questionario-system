using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Entidades.Pesquisas;

public class Pesquisa
{
    public Guid Id { get; private set; }
    
    public string Nome { get; private set; }

    private List<Pergunta> _perguntas = [];
    public IReadOnlyList<Pergunta> Perguntas => _perguntas.AsReadOnly();

    private List<SituacaoPesquisa> _situacoes = [];
    public IReadOnlyList<SituacaoPesquisa> Situacoes => _situacoes.AsReadOnly();
    
    private List<Resposta> _respostas = [];
    public IReadOnlyList<Resposta> Respostas => _respostas.AsReadOnly();

    private Pesquisa(Pessoa pessoa)
    {
        Id = Guid.NewGuid();
        _situacoes.Add(SituacaoPesquisa.CriarSituacao(pessoa, TipoSituacaoPesquisa.EmProducao));
    }
    
    internal static Pesquisa CriarPesquisa(Pessoa criador, string nome, List<(string, List<(char opcao, string texto)>)> perguntas)
    {
        if(perguntas.Count < 2)
            throw new ArgumentNullException(nameof(perguntas), "A pesquisa deve conter no mínimo duas perguntas.");
        
        Pesquisa pesquisa = new Pesquisa(criador);
        pesquisa.AtualizarNome(nome);
        perguntas.ForEach(pergunta => pesquisa.AdicionarPergunta(pergunta.Item1, pergunta.Item2));
        
        return pesquisa;
    }

    internal void AtualizarNome(string nome)
    {
        if(string.IsNullOrEmpty(nome.Trim()))
            throw new ArgumentNullException(nameof(nome), "O nome da pesquisa deve ser informado.");

        if (_situacoes.Last().TipoSituacao != TipoSituacaoPesquisa.EmProducao)
            throw new InvalidOperationException("A pesquisa só pode ser alterada se estiver em produção.");
        
        Nome = nome;
    }

    internal void AdicionarPergunta(string enunciado, List<(char opcao, string texto)> alternativas)
    {
        if (_situacoes.Last().TipoSituacao != TipoSituacaoPesquisa.EmProducao)
            throw new InvalidOperationException("A pesquisa só pode ser alterada se estiver em produção.");
        _perguntas.Add(Pergunta.CriarPergunta(enunciado, alternativas));
    }

    internal void RemoverPergunta(string enunciado)
    {
        if (_situacoes.Last().TipoSituacao != TipoSituacaoPesquisa.EmProducao)
            throw new InvalidOperationException("A pesquisa só pode ser alterada se estiver em produção.");
        
        var perguntaARemover = _perguntas.FirstOrDefault(pergunta => pergunta.Enunciado == enunciado);

        if (perguntaARemover == null)
            throw new InvalidOperationException("A pergunta a ser removida não existe.");

        if (_perguntas.Count - 1 < 2)
            throw new InvalidOperationException("A pergunta deve ter ao menos duas perguntas.");
        
        _perguntas.Remove(perguntaARemover);
    }

    internal void MarcarComoPronta(Pessoa pessoa)
    {
        if (_situacoes.Last().TipoSituacao != TipoSituacaoPesquisa.EmProducao)
            throw new InvalidOperationException("A situação atual não permite marcar a pesquisa como pronta");
        
        _situacoes.Add(SituacaoPesquisa.CriarSituacao(pessoa, TipoSituacaoPesquisa.Pronta));
    }

    internal void PublicarPesquisa(Pessoa pessoa)
    {
        throw new NotImplementedException(); //TODO
    }

    internal void FinalizarPesquisa(Pessoa pessoa)
    {
        throw new NotImplementedException(); //TODO
    }

    internal void Responder(List<(string pergunta, string alternativa, Pessoa pessoa)> respostas)
    {
        throw new NotImplementedException(); //TODO
    }
}
