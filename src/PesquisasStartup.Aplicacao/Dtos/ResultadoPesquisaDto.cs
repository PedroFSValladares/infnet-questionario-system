namespace PesquisasStartup.Aplicacao.Dtos;

public class ResultadoPesquisaDto
{
    public Guid IdPesquisa { get; set; }
    public string NomePesquisa { get; set; }
    public List<(string pergunta, List<(char opcao, int quantitativo)>)> Resultados { get; set; }
}