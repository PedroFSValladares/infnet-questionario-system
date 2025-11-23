namespace api.Domain.DTOs.Pesquisa;

public class IncluirPesquisaDto : PesquisaDto
{
    public Model.Pesquisa ToEntity()
    {
        return new Model.Pesquisa(Nome, Perguntas.Select(p => p.ToEntity()).ToList());
    }
}