using PesquisasStartup.Aplicacao.Dtos;
using PesquisasStartup.Dominio.Entidades.Pesquisas;

namespace PesquisasStartup.Aplicacao.Extensions;

public static class DtoExtensions
{
    public static PesquisaDto ToPesquisaDto(this Pesquisa pesquisa)
    {
        return new PesquisaDto()
        {
            Id = pesquisa.Id,
            Nome = pesquisa.Nome,
            Situacoes = pesquisa.Situacoes.Select(s => (s.TipoSituacao, s.DataHora)).ToList(),
            Perguntas = pesquisa.Perguntas
                .Select(p => (p.Enunciado, p.Alternativas.Select(a => (a.Opcao, a.Texto)).ToList())).ToList()
        };
    }
}