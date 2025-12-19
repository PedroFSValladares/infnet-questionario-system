namespace PesquisasStartup.Aplicacao.Dtos;

public class RespostaDto
{
    public Guid IdPesquisa { get; set; }
    public string Cpf { get; set; }
    public List<(string pergunta, char opcao)> Respostas  { get; set; }
}