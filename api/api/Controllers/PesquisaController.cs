using api.Domain.DTOs.Pesquisa;
using api.Domain.Factories;
using api.Domain.Model;
using api.Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PesquisaController : ControllerBase
{
    private readonly PesquisaRepository _pesquisaRepository;

    public PesquisaController(PesquisaRepository pesquisaRepository)
    {
        _pesquisaRepository = pesquisaRepository;
    }

    [HttpPost]
    public IActionResult Incluir([FromBody] IncluirPesquisaDto pesquisaDto)
    {
        var pesquisaAIncluir = PesquisaFactory.CriarPesquisa(pesquisaDto);
        var pesquisa = _pesquisaRepository.Salvar(pesquisaAIncluir);
        return Created(nameof(ObterPorId) ,pesquisa);
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(Guid id)
    {
        var pesquisa = _pesquisaRepository.ObterPorId(id);
        return pesquisa != null ? Ok(pesquisa) : NotFound();
    }
}