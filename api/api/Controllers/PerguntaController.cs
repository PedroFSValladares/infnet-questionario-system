using api.Domain.DTOs.Pergunta;
using api.Domain.Factories;
using api.Domain.Model;
using api.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerguntaController : ControllerBase
{
    
    private readonly PerguntaRepository _perguntaRepository;

    public PerguntaController(PerguntaRepository perguntaRepository)
    {
        _perguntaRepository = perguntaRepository;
    }

    [HttpPost]
    public IActionResult IncluirAsync([FromBody] IncluirPerguntaDto perguntaDto)
    {
        var pergunta = PerguntaFactory.NovaPergunta(perguntaDto);
        return Created(nameof(ObterPorIdAsync), _perguntaRepository.SalvarAsync(pergunta));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Alterar(Guid id, [FromBody] AlterarPerguntaDto perguntaDto)
    {
        var pergunta = PerguntaFactory.NovaPergunta(perguntaDto);
        var perguntaAtualizada = await _perguntaRepository.AtualizarAsync(pergunta);
        
        return perguntaAtualizada == null ?  NotFound() : Ok(perguntaAtualizada.ToResponseDto()); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirAsync(Guid id)
    {
        await _perguntaRepository.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> ObterTodosAsync()
    {
        var perguntas = await _perguntaRepository.ObterTodosAsync();
        return perguntas.Count > 0 ? Ok(perguntas.Select(e => e.ToPerguntaResumidaResponseDto())) : NoContent();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorIdAsync(string id)
    {
        var pergunta = await _perguntaRepository.ObterPorIdAsync(new Guid(id));
        return pergunta != null ? Ok(pergunta.ToResponseDto()) : NotFound();
    }
}