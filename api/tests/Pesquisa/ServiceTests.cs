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
    public async Task TestaIncluirPesquisaValida()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var result = await _pesquisaService.IncluirPesquisaAsync(pesquisaEntidade);
        
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        PesquisaService.ComparaPesquisas(result, pesquisaDto);
    }

    [Fact]
    public async Task TestaAlterarPesquisaValida()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var result = await _pesquisaService.IncluirPesquisaAsync(pesquisaEntidade);

        result.Nome = "TESTE 123";
        result.Perguntas[0].Enunciado = "4321";

        var alterarResult = await _pesquisaService.AlterarPesquisaAsync(result);
        
        Assert.NotNull(alterarResult);
        Assert.Equal("TESTE 123", alterarResult.Nome);
        Assert.Equal("4321",  alterarResult.Perguntas[0].Enunciado);
    }

    [Fact]
    public void TestaAlterarPesquisaInexistente()
    {
        var pesquisa = new api.Domain.Model.Pesquisa();
        var result = _pesquisaService.AlterarPesquisaAsync(pesquisa);
        
        Assert.Null(result);
    }

    [Fact]
    public async Task TestaListarPesquisas()
    {
        var tamanhoAnterior = _context.Pesquisas.Count();
        var registrosAInserir = 10;

        for (int i = 0; i < registrosAInserir; i++)
        {
            var pesquisaDto =  Exemplos.ObterExemplosValidos().FirstOrDefault();
            var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
            
            await _pesquisaService.IncluirPesquisaAsync(pesquisaEntidade);
        }
        
        var result = await _pesquisaService.ListarPesquisasAsync();
        
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(registrosAInserir + tamanhoAnterior, result.Count);
    }

    [Fact]
    public async Task TestaBuscarPesquisaExistente()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var inclusaoResult = await _pesquisaService.IncluirPesquisaAsync(pesquisaEntidade);
        
        var consultaResult = await _pesquisaService.BuscarPesquisaPorIdAsync(inclusaoResult.Id);
        
        Assert.NotNull(consultaResult);
        Assert.Equal(inclusaoResult.Id, consultaResult.Id);
        PesquisaService.ComparaPesquisas(consultaResult, pesquisaDto);
    }

    [Fact]
    public async Task TestaBuscarPesquisaExistenteInexistente()
    {
        var result = await _pesquisaService.BuscarPesquisaPorIdAsync(Guid.NewGuid());
        
        Assert.Null(result);
    }

    [Fact]
    public async Task TestaDeletarPesquisaExistente()
    {
        var quantidadeDeRegistrosInicial = _context.Pesquisas.Count();
        var consulta = await _pesquisaService.ListarPesquisasAsync();
        var pesquisaADeletar = consulta.FirstOrDefault();
        
        await _pesquisaService.ExcluirPesquisaAsync(pesquisaADeletar.Id);
        
        Assert.Equal(quantidadeDeRegistrosInicial - 1, _context.Pesquisas.Count());
    }

    [Fact]
    public async Task TestaDeletarPesquisaInexistente()
    {
        var quantidadeDeRegistrosInicial = _context.Pesquisas.Count();
        
        await _pesquisaService.ExcluirPesquisaAsync(Guid.NewGuid());
        
        Assert.Equal(quantidadeDeRegistrosInicial, _context.Pesquisas.Count());
    }
}