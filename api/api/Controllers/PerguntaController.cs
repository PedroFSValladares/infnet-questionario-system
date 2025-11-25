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
    public IActionResult Incluir([FromBody] IncluirPerguntaDto perguntaDto)
    {
        var pergunta = PerguntaFactory.NovaPergunta(perguntaDto);
        return Created(nameof(ObterPorId), _perguntaRepository.Salvar(pergunta).ToResponseDto());
    }

    [HttpPut("{id}")]
    public IActionResult Alterar(Guid id, [FromBody] AlterarPerguntaDto perguntaDto)
    {
        var pergunta = PerguntaFactory.NovaPergunta(perguntaDto);
        var perguntaAtualizada = _perguntaRepository.Atualizar(id, pergunta);
        
        return perguntaAtualizada == null ?  NotFound() : Ok(perguntaAtualizada.ToResponseDto()); 
    }

    [HttpDelete("{id}")]
    public IActionResult Excluir(Guid id)
    {
        _perguntaRepository.Delete(id);
        return NoContent();
    }
    
    [HttpGet]
    public IActionResult ObterTodos()
    {
        var perguntas =  _perguntaRepository.ObterTodos();
        return perguntas.Count > 0 ? Ok(perguntas.Select(e => e.ToPerguntaResumidaResponseDto())) : NoContent();
    }
    
    [HttpGet("{id}")]
    public IActionResult ObterPorId(string id)
    {
        var pergunta = _perguntaRepository.ObterPorId(new Guid(id));
        return pergunta != null ? Ok(pergunta.ToResponseDto()) : NotFound();
    }
}