using api.Domain.Factories;
using tests.Factories;
using tests.TestServices;

namespace tests.Pesquisa.Factories;

public class PesquisaFactoryTests
{
    [Fact]
    public void TestaCriarEntidadeAPartirDeIncluiPesquisaDtoValido()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        Assert.NotNull(pesquisaEntidade);
        Assert.NotEqual(Guid.Empty, pesquisaEntidade.Id);
        PesquisaService.ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }

    [Fact]
    public void TestaCriarEntidadeAPartirDeAlterarPesquisaDtoValido()
    {
        var incluirPesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
    }
}