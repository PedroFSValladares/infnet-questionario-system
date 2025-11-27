using api.Domain.DTOs.Pesquisa;
using api.Domain.Factories;
using api.Services;
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
    public async Task<IActionResult> Incluir([FromBody] IncluirPesquisaDto pesquisaDto)
    {
        var pesquisaAIncluir = PesquisaFactory.CriarPesquisa(pesquisaDto);
        var pesquisa = await _pesquisaService.IncluirPesquisaAsync(pesquisaAIncluir);
        return Created(nameof(ObterPorId), pesquisa);
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(Guid id)
    {
        var pesquisa = _pesquisaService.BuscarPesquisaPorId(id);
        return pesquisa != null ? Ok(pesquisa) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var pesquisas = await _pesquisaService.ListarPesquisasAsync();
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