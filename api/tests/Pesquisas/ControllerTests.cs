using api.Context;
using api.Controllers;
using api.Domain.Factories;
using api.Domain.Repositories;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace tests.Pesquisas;

public class ControllerTests
{
    
    private QuestionarioContext _context;
    private PesquisaRepository _pesquisaRepository;
    private PesquisaService _pesquisaService;
    private PesquisaController _pesquisaController;

    public ControllerTests()
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>()
            .UseInMemoryDatabase("ControllerTests")
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .Options;
        _context = new QuestionarioContext(options);
        
        _pesquisaRepository = new PesquisaRepository(_context);
        _pesquisaService = new PesquisaService(_pesquisaRepository);
        _pesquisaController = new PesquisaController(_pesquisaService);
    }
    
    [Fact]
    public async Task TestaIncluirPesquisaController()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        
        var result = await _pesquisaController.Incluir(pesquisaDto);
        var registros = await _pesquisaService.ListarPesquisasAsync();
        Assert.IsType<CreatedResult>(result);
        Assert.NotEmpty(registros);
    }
    
    [Fact]
    public async Task TestaObterPesquisaController()
    {
        var pesquisaDto = Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var pesquisaSalva = await _pesquisaRepository.SalvarAsync(pesquisaEntidade);
        
        var consultaResult = _pesquisaController.ObterPorId(pesquisaSalva.Id);
        Assert.IsType<OkObjectResult>(consultaResult);
    }
    
    [Fact]
    public void TestaObterPesquisaInexistenteController()
    {
        var consultaResult = _pesquisaController.ObterPorId(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(consultaResult);
    }

    [Fact]
    public void TestaListarPesquisaController()
    {
        var consultaResult = _pesquisaController.ObterTodos();
        Assert.IsType<OkObjectResult>(consultaResult);
    }
    
    [Fact]
    public async Task TestaAlterarPesquisaController()
    {
        var incluirPesquisaDto =  Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntity = PesquisaFactory.CriarPesquisa(incluirPesquisaDto);
        
        var pesquisaInclusa = await _pesquisaService.IncluirPesquisaAsync(pesquisaEntity);
        
        var alterarPesquisaDto = PesquisaConverter.ConverterEntidadeParaAlterarPesquisaDto(pesquisaInclusa);

        alterarPesquisaDto.Nome = "aaaaa";
        alterarPesquisaDto.Perguntas[0].Enunciado = "123";
        
        var result = _pesquisaController.Alterar(alterarPesquisaDto);
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public void TestaAlterarPesquisaInexistenteController()
    {
        var incluirPesquisaDto =  Exemplos.ObterExemplosValidos().FirstOrDefault();
        var pesquisaEntity = PesquisaFactory.CriarPesquisa(incluirPesquisaDto);
        var alterarPesquisaDto = PesquisaConverter.ConverterEntidadeParaAlterarPesquisaDto(pesquisaEntity);
        
        var result = _pesquisaController.Alterar(alterarPesquisaDto);
        Assert.IsType<NotFoundResult>(result);
    }
}