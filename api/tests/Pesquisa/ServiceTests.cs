using api.Context;
using api.Controllers;
using api.Domain.Factories;
using api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using tests.TestServices;

namespace tests.Pesquisa;

public class ServiceTests
{
    private QuestionarioContext _context;
    private PesquisaRepository _pesquisaRepository;
    private api.Services.PesquisaService _pesquisaService;

    public ServiceTests()
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase("ServiceTests").Options;
        _context = new QuestionarioContext(options);
        
        _pesquisaRepository = new PesquisaRepository(_context);
        _pesquisaService = new api.Services.PesquisaService(_pesquisaRepository);
    }

    [Fact]
    public void TestaIncluirPesquisaValida()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var result = _pesquisaService.IncluirPesquisa(pesquisaEntidade);
        
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        PesquisaService.ComparaPesquisas(result, pesquisaDto);
    }

    [Fact]
    public void TestaAlterarPesquisaValida()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var result = _pesquisaService.IncluirPesquisa(pesquisaEntidade);

        result.Nome = "TESTE 123";
        result.Perguntas[0].Enunciado = "4321";

        var alterarResult = _pesquisaService.AlterarPesquisa(result);
        
        Assert.NotNull(alterarResult);
        Assert.Equal("TESTE 123", alterarResult.Nome);
        Assert.Equal("4321",  alterarResult.Perguntas[0].Enunciado);
    }

    [Fact]
    public void TestaAlterarPesquisaInexistente()
    {
        var pesquisa = new api.Domain.Model.Pesquisa();
        var result = _pesquisaService.AlterarPesquisa(pesquisa);
        
        Assert.Null(result);
    }

    [Fact]
    public void TestaListarPesquisas()
    {
        var tamanhoAnterior = _context.Pesquisas.Count();
        var registrosAInserir = 10;

        for (int i = 0; i < registrosAInserir; i++)
        {
            var pesquisaDto =  Exemplos.ObterExemplosValidos().FirstOrDefault();
            var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
            
            _pesquisaService.IncluirPesquisa(pesquisaEntidade);
        }
        
        var result = _pesquisaService.ListarPesquisas();
        
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(registrosAInserir + tamanhoAnterior, result.Count);
    }

    [Fact]
    public void TestaBuscarPesquisaExistente()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var inclusaoResult = _pesquisaService.IncluirPesquisa(pesquisaEntidade);
        
        var consultaResult = _pesquisaService.BuscarPesquisaPorId(inclusaoResult.Id);
        
        Assert.NotNull(consultaResult);
        Assert.Equal(inclusaoResult.Id, consultaResult.Id);
        PesquisaService.ComparaPesquisas(consultaResult, pesquisaDto);
    }

    [Fact]
    public void TestaBuscarPesquisaExistenteInexistente()
    {
        var result = _pesquisaService.BuscarPesquisaPorId(Guid.NewGuid());
        
        Assert.Null(result);
    }

    [Fact]
    public void TestaDeletarPesquisaExistente()
    {
        var quantidadeDeRegistrosInicial = _context.Pesquisas.Count();
        var pesquisaADeletar = _pesquisaService.ListarPesquisas().FirstOrDefault();
        
        _pesquisaService.ExcluirPesquisa(pesquisaADeletar.Id);
        
        Assert.Equal(quantidadeDeRegistrosInicial - 1, _context.Pesquisas.Count());
    }

    [Fact]
    public void TestaDeletarPesquisaInexistente()
    {
        var quantidadeDeRegistrosInicial = _context.Pesquisas.Count();
        
        _pesquisaService.ExcluirPesquisa(Guid.NewGuid());
        
        Assert.Equal(quantidadeDeRegistrosInicial, _context.Pesquisas.Count());
    }
}