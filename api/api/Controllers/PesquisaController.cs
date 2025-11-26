using api.Domain.DTOs.Pesquisa;
using api.Domain.Factories;
using api.Domain.Model;
using api.Domain.Repositories;
using api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PesquisaController : ControllerBase
{
    private readonly PesquisaService _pesquisaService;

    public PesquisaController(PesquisaService pesquisaService)
    {
        _pesquisaService = pesquisaService;
    }

    [HttpPost]
    public IActionResult Incluir([FromBody] IncluirPesquisaDto pesquisaDto)
    {
        var pesquisaAIncluir = PesquisaFactory.CriarPesquisa(pesquisaDto);
        var pesquisa = _pesquisaService.IncluirPesquisa(pesquisaAIncluir);
        return Created(nameof(ObterPorId), pesquisa);
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(Guid id)
    {
        var pesquisa = _pesquisaService.BuscarPesquisaPorId(id);
        return pesquisa != null ? Ok(pesquisa) : NotFound();
    }

    [HttpGet]
    public IActionResult ObterTodos()
    {
        var pesquisas = _pesquisaService.ListarPesquisas();
        return pesquisas.Count > 0 ? Ok(pesquisas) : NoContent();
    }

    [HttpPut]
    public IActionResult Alterar([FromBody] AlterarPesquisaDto pesquisaDto)
    {
        var pesquisa = PesquisaFactory.CriarPesquisa(pesquisaDto);
        var result = _pesquisaService.AlterarPesquisa(pesquisa);
        return result != null ? Ok(result) : NotFound();
    }
}