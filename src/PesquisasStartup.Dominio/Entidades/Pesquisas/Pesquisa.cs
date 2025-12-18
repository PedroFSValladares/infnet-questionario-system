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

    private Pesquisa()
    {
        Id = Guid.NewGuid();
        _situacoes.Add(SituacaoPesquisa.CriarSituacao(TipoSituacaoPesquisa.EmProducao));
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

        if (_situacoes.Last().TipoSituacao != TipoSituacaoPesquisa.EmProducao)
            throw new InvalidOperationException("A pesquisa só pode ser alterada se estiver em produção.");
        
        Nome = nome;
    }

    public void AdicionarPergunta(string enunciado, List<(char opcao, string texto)> alternativas)
    {
        if (_situacoes.Last().TipoSituacao != TipoSituacaoPesquisa.EmProducao)
            throw new InvalidOperationException("A pesquisa só pode ser alterada se estiver em produção.");
        _perguntas.Add(Pergunta.CriarPergunta(enunciado, alternativas));
    }

    public void RemoverPergunta(string enunciado)
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

    public void PublicarPesquisa()
    {
        if (_situacoes.Last().TipoSituacao == TipoSituacaoPesquisa.Finalizada)
            throw new InvalidOperationException("A pesquisa atual já foi finalizada");
        
        _situacoes.Add(SituacaoPesquisa.CriarSituacao(TipoSituacaoPesquisa.Publicada));
    }

    public void FinalizarPesquisa()
    {
        if (_situacoes.Last().TipoSituacao != TipoSituacaoPesquisa.Publicada)
            throw new InvalidOperationException("A situação atual não permite finalizar a pesquisa");
        
        _situacoes.Add(SituacaoPesquisa.CriarSituacao(TipoSituacaoPesquisa.Finalizada));
    }

    public void Responder(Cpf cpfPessoa, List<(string pergunta, char alternativa)> respostas)
    {
        if (_situacoes.Last().TipoSituacao != TipoSituacaoPesquisa.Publicada)
            throw new InvalidOperationException("A pesquisa precisa estar publicada para ser respondida.");
        
        if (_respostas.Any(resposta => resposta.CpfPessoa.Value == cpfPessoa.Value))
            throw new InvalidOperationException("Pesquisa já respondida por essa pessoa");
        
        if(respostas.Count == 0 || respostas.Count != _perguntas.Count)
            throw new InvalidOperationException("Todas as perguntas da pesquisa precisam ser respondidas");
        
        List<Resposta> respostasASalvar = [];
        
        respostas.ForEach(resposta =>
        {
            var pergunta = _perguntas.Find(pergunta => pergunta.Enunciado == resposta.pergunta);
            
            if(pergunta ==  null)
                throw new ArgumentException("Resposta inválida, pergunta informada não existe na pesquisa", nameof(resposta));

            var alternativa = pergunta.Alternativas.FirstOrDefault(alternativa => alternativa.Opcao == resposta.alternativa);
            
            if (alternativa == null)
                throw new ArgumentException("Resposta invãlida, uma das alternativas informadas não existe na pergunta", nameof(resposta));

            if (respostas.Count(resp => resp.pergunta == resposta.pergunta) > 1)
                throw new ArgumentException("Não podem existir respostas repetidas para uma pesquisa", nameof(resposta));
            
            respostasASalvar.Add(Resposta.CriarResposta(pergunta, alternativa, cpfPessoa));
        });
        
        _respostas.AddRange(respostasASalvar);
    }
}
