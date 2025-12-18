using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Dominio.Entidades.Pesquisas;

public class SituacaoPesquisa
{
    public DateTime DataHora  { get; private set; }
    public TipoSituacaoPesquisa TipoSituacao { get; private set; }

    private SituacaoPesquisa(TipoSituacaoPesquisa tipoSituacao)
    {
        DataHora = DateTime.Now;
        TipoSituacao = tipoSituacao;
    }

    public static SituacaoPesquisa CriarSituacao(TipoSituacaoPesquisa tipoSituacao)
    {
        return new SituacaoPesquisa(tipoSituacao);
    }
}