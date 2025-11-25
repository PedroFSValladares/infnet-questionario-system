using api.Domain.Factories;
using tests.TestServices;

namespace tests.Pesquisa.Factories;

public class PesquisaFactoryTests
{
    [Fact]
    public void TestaCriarEntidadeAPartirDeIncluiPesquisaDtoValido()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        Assert.NotNull(pesquisaEntidade);
        Assert.NotEqual(Guid.Empty, pesquisaEntidade.Id);
        PesquisaService.ComparaPesquisas(pesquisaEntidade, pesquisaDto);
    }

    [Fact]
    public void TestaCriarEntidadeAPartirDeIncluiPesquisaDtoInvalido()
    {
        Assert.Throws<ArgumentNullException>(() => PesquisaFactory.CriarPesquisa(Exemplos.ObterExemplosInvalidos().FirstOrDefault()));
    }
    
}