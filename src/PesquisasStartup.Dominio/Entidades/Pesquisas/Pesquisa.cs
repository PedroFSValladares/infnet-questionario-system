using PesquisasStartup.Dominio.Entidades.Pessoas;

namespace PesquisasStartup.Dominio.Entidades.Pesquisas;

public class Pesquisa
{

    public Guid Id { get; private set; }
    
    public string Nome { get; private set; }

    private List<Pergunta> _perguntas = [];
    public IReadOnlyList<Pergunta> Perguntas => _perguntas.AsReadOnly();

    private List<SituacoesPesquisa> _situacoes = [];
    public IReadOnlyList<SituacoesPesquisa> Situacoes => _situacoes.AsReadOnly();
    
    private List<Resposta> _respostas = [];
    public IReadOnlyList<Resposta> Respostas => _respostas.AsReadOnly();

    private Pesquisa()
    {
        Id = Guid.NewGuid();
    }
    
    public static Pesquisa CriarPesquisa(string nome, List<(string, List<(char opcao, string texto)>)> perguntas)
    {
        if(perguntas.Count < 2)
            throw new ArgumentNullException(nameof(perguntas), "A pesquisa deve conter no mínimo duas perguntas.");
        
        Pesquisa pesquisa = new Pesquisa();
        pesquisa.AtualizarNome(nome);
        perguntas.ForEach(pergunta => pesquisa.AdicionarPergunta(pergunta.Item1, pergunta.Item2));
        
        return pesquisa;
    }

    public void AtualizarNome(string nome)
    {
        if(string.IsNullOrEmpty(nome.Trim()))
            throw new ArgumentNullException(nameof(nome), "O nome da pesquisa deve ser informado.");
        
        Nome = nome;
    }

    public void AdicionarPergunta(string enunciado, List<(char opcao, string texto)> alternativas)
    {
        _perguntas.Add(Pergunta.CriarPergunta(enunciado, alternativas));
    }

    public void RemoverPergunta(string enunciado)
    {
        var perguntaARemover = _perguntas.FirstOrDefault(pergunta => pergunta.Enunciado == enunciado);

        if (perguntaARemover == null)
            throw new InvalidOperationException("A pergunta a ser removida não existe.");

        if (_perguntas.Count - 1 < 2)
            throw new InvalidOperationException("A pergunta deve ter ao menos duas perguntas.");
        
        _perguntas.Remove(perguntaARemover);
    }

    public void MarcarComoPronta(Pessoa pessoa)
    {
        throw new NotImplementedException(); //TODO
    }

    public void PublicarPesquisa(Pessoa pessoa)
    {
        throw new NotImplementedException(); //TODO
    }

    public void FinalizarPesquisa(Pessoa pessoa)
    {
        throw new NotImplementedException(); //TODO
    }

    public void Responder(List<(string pergunta, string alternativa, Pessoa pessoa)> respostas)
    {
        throw new NotImplementedException(); //TODO
    }
}
