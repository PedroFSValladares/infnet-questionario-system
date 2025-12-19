using PesquisasStartup.Dominio.Enums;

namespace PesquisasStartup.Aplicacao.Dtos;

public class PesquisaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public List<(TipoSituacaoPesquisa tipoSituacao, DateTime data)> Situacoes { get; set; }
    public List<(string enunciado, List<(char opcao, string texto)> alternvativas)> Perguntas { get; set; }

    public override string ToString()
    {
        return $"{{Id: {Id}, Nome: {Nome}}}";
    }
}