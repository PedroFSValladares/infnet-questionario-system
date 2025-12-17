using PesquisasStartup.Dominio.Entidades.Pessoas;
using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Entidades.Pesquisas;

public class SituacaoPesquisa
{
    public Pessoa Responsavel { get; private set; }
    public DateTime DataHora  { get; private set; }
    public TipoSituacaoPesquisa TipoSituacao { get; private set; }

    private SituacaoPesquisa(Pessoa responsavel, TipoSituacaoPesquisa tipoSituacao)
    {
        Responsavel = responsavel;
        DataHora = DateTime.Now;
        TipoSituacao = tipoSituacao;
    }

    public static SituacaoPesquisa CriarSituacao(Pessoa responsavel, TipoSituacaoPesquisa tipoSituacao)
    {
        return new SituacaoPesquisa(responsavel, tipoSituacao);
    }
}