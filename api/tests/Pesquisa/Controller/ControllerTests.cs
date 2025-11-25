using api.Context;
using api.Controllers;
using api.Domain.Factories;
using api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tests.Factories;

namespace tests.Pesquisa.Controller;

public class ControllerTests
{
    
    private QuestionarioContext _context;
    private PesquisaRepository _pesquisaRepository;
    private PesquisaController _pesquisaController;

    public ControllerTests()
    {
        var options = new DbContextOptionsBuilder<QuestionarioContext>().UseInMemoryDatabase("QuestionarioContext").Options;
        _context = new QuestionarioContext(options);
        
        _pesquisaRepository = new PesquisaRepository(_context);
        _pesquisaController = new PesquisaController(_pesquisaRepository);
    }
    
    [Fact]
    public void TestaIncluirPesquisaController()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        
        var result = _pesquisaController.Incluir(pesquisaDto);
        Assert.IsType<CreatedResult>(result);
    }
    
    [Fact]
    public void TestaObterPesquisaController()
    {
        var pesquisaDto = PesquisaDtoFactory.CreateIncluirPesquisaDto();
        var pesquisaEntidade = PesquisaFactory.CriarPesquisa(pesquisaDto);
        
        var pesquisaSalva = _pesquisaRepository.Salvar(pesquisaEntidade);
        
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
    public void TestaAlterarPesquisaController()
    {
        throw new NotImplementedException(); //TODO
    }
    
    [Fact]
    public void TestaAlterarPesquisaInexistenteController()
    {
        throw new NotImplementedException(); //TODO
    }
}