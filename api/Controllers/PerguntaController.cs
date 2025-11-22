using api.Domain.DTOs.Pergunta;
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
        var pergunta = perguntaDto.ToEntity();
        return Ok(_perguntaRepository.Salvar(pergunta).ToResponseDto());
    }
    
    [HttpGet]
    public IActionResult ObterTodos()
    {
        var perguntas =  _perguntaRepository.ObterTodos();
        return perguntas.Count > 0 ? Ok(perguntas) : NoContent();
    }
    
    [HttpGet("{id}")]
    public IActionResult ObterPorId(string id)
    {
        var pergunta = _perguntaRepository.ObterPorId(new Guid(id));
        return pergunta != null ? Ok(pergunta) : NotFound();
    }
}