using api.Domain.DTOs.Pergunta;
using api.Domain.DTOs.Pesquisa;
using api.Domain.Model;

namespace tests.TestServices;

public static class PesquisaService
{
    internal static void ComparaPesquisas(api.Domain.Model.Pesquisa pesquisaEntidade, IncluirPesquisaDto pesquisaDto)
    {
        Assert.Equal(pesquisaDto.Nome, pesquisaEntidade.Nome);
        Assert.All(pesquisaEntidade.Perguntas, (pergunta, index) =>
        {
            ComparaPerguntas(pesquisaDto.Perguntas[index], pergunta);
        });
    }
    
    internal static void ComparaPerguntas(IncluirPerguntaDto perguntaDto,
        Pergunta perguntaEntidade)
    {
        Assert.Equal(perguntaDto.Enunciado, perguntaEntidade.Enunciado);
        Assert.All(perguntaDto.Alternativas, (alternativaDto, i) =>
        {
            Assert.Equal(alternativaDto.Opcao, perguntaEntidade.Alternativas[i].Opcao);
            Assert.Equal(alternativaDto.Texto, perguntaEntidade.Alternativas[i].Texto);
        });
    }
}