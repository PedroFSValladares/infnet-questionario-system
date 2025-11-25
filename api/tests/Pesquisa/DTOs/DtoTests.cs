using tests.Factories;
using tests.TestServices;

namespace tests.Pesquisa.DTOs;

public class DtoTests
{
    [Fact]
    public void ConverteIncluirPesquisaDtoParaEntidade()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        var pesquisaEntidade = pesquisaDto.ToEntity();
        
        Assert.NotNull(pesquisaEntidade);
        PesquisaService.ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }

    [Fact]
    public void ConverteAlterarPesquisaDtoParaEntidade()
    {
        throw new NotImplementedException(); //TODO
    }
}