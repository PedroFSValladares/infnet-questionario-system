using api.Domain.DTOs.Pergunta;
using api.Domain.Enuns;

namespace api.Domain.DTOs.Pesquisa;

public class AlterarPesquisaDto : PesquisaDto
{
    public Guid Id { get; set; }
    public StatusPesquisa StatusPesquisa { get; set; }
    public new List<AlterarPerguntaDto> Perguntas { get; set; }
    
    public Model.Pesquisa ToEntity()
    {
        return new Model.Pesquisa(Id, Nome, StatusPesquisa, Perguntas.Select(p => p.ToEntity()).ToList());
    }
}