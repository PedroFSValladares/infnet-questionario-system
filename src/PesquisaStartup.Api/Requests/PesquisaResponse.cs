using PesquisasStartup.Dominio.Enums;
using PesquisaStartup.Api.Requests.RequestModels;

namespace PesquisaStartup.Api.Requests;

public class PesquisaResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public TipoSituacaoPesquisa Situacao { get; set; }
    public List<Pergunta> Perguntas { get; set; }
}