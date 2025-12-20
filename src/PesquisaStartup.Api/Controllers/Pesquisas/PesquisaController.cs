using Microsoft.AspNetCore.Mvc;
using PesquisasStartup.Aplicacao.Dtos;
using PesquisasStartup.Aplicacao.Services;
using PesquisaStartup.Api.Requests;
using PesquisaStartup.Api.Requests.RequestModels;

namespace PesquisaStartup.Api.Controllers.Pesquisas;

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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarPesquisaAsync(CreatePesquisaRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var pesquisaDto = CreatePesquisaDto(request);
        try
        {
            var result = await _pesquisaService.SalvarPesquisaAsync(pesquisaDto);

            return Created(nameof(ObterPesquisaPorIdAsync), new CreatePesquisaResponse() { PesquisaId = result.Id });
        }
        catch (Exception e) when (e is ArgumentException or ArgumentNullException or InvalidOperationException)
        {
            return BadRequest("Erro ao criar a Pesquisa" + e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "Ocorreu um erro interno ao processar sua requisição." });
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterPesquisaPorIdAsync(Guid id)
    {
        var pesquisa = await _pesquisaService.ObterPesquisaPorIdAsync(id);
        if (pesquisa == null)
            return NoContent();
        
        return Ok(new PesquisaResponse()
        {
            Id = pesquisa.Id,
            Nome = pesquisa.Nome,
            Perguntas = pesquisa.Perguntas.Select(pergunta => new Pergunta()
            {
                Enunciado = pergunta.enunciado,
                Alternativas = pergunta.alternvativas.Select(alternativa => new Alternativa()
                {
                    Opcao = alternativa.opcao,
                    Texto = alternativa.texto
                }).ToList()
            }).ToList(),
            Situacao = pesquisa.Situacoes.Last().tipoSituacao
        });
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AlterarPesquisaAsync(Guid id, [FromBody] AlterarPesquisaRequest request)
    {
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var pesquisaDto = CreatePesquisaDto(request);
        pesquisaDto.Id = id;
        try
        {
            var result = await _pesquisaService.AlterarPesquisaAsync(pesquisaDto);
            return Ok(new PesquisaResponse(){ Id = result.Id });

        }
        catch (Exception e) when (e is ArgumentException or ArgumentNullException or InvalidOperationException)
        {
            return BadRequest("Erro ao criar a Pesquisa" + e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = "Ocorreu um erro interno ao processar sua requisição." });
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ObterPesquisasAsync()
    {
        var pesquisas = await _pesquisaService.ObterTodosAsync();
        return pesquisas.Count == 0
            ? NoContent()
            : Ok(pesquisas.Select(pesquisa => new PesquisaResponse()
            {
                Id = pesquisa.Id,
                Nome = pesquisa.Nome
            }));
    }

    [HttpPatch("[action]/{id:guid}")]
    public async Task<IActionResult> PublicarPesquisaAsync(Guid id)
    {
        try
        {
            var result = await _pesquisaService.PublicarAsync(id);
            return Ok(new PesquisaResponse() {Id  = result.Id});
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Houve um erro ao processar sua requisição {e.Message}");
        }
    }
    
    [HttpPatch("[action]/{id:guid}")]
    public async Task<IActionResult> FinalizarPesquisaAsync(Guid id)
    {
        try
        {
            var result = await _pesquisaService.FinalizarAsync(id);
            return Ok(new PesquisaResponse() {Id  = result.Id});
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Houve um erro ao processar sua requisição {e.Message}");
        }
    }
    
    [HttpPatch("[action]/{id:guid}")]
    public async Task<IActionResult> ResponderPesquisaAsync(Guid id, [FromBody] RespostaRequest request)
    {
        await _pesquisaService.RespoderPesquisaAsync(new RespostaDto()
        {
            Cpf = request.Cpf,
            IdPesquisa = id,
            Respostas = request.Respostas.Select(resposta => (resposta.Pergunta, resposta.Opcao)).ToList()
        });

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> ExcluirPesquisaAsync(Guid id)
    {
        await _pesquisaService.ExcluirPesquisaAsync(id);
        return Ok();
    }

    private PesquisaDto CreatePesquisaDto(CreatePesquisaRequest request)
    {
        return new PesquisaDto()
        {
            Nome = request.Nome,
            Perguntas = request.Perguntas.Select(pergunta => (pergunta.Enunciado, 
                pergunta.Alternativas.Select(alternativa => (alternativa.Opcao, alternativa.Texto)).ToList())).ToList()
        };
    }
    
    private PesquisaDto CreatePesquisaDto(AlterarPesquisaRequest request)
    {
        return new PesquisaDto()
        {
            Nome = request.Nome,
            Perguntas = request.Perguntas.Select(pergunta => (pergunta.Enunciado, 
                pergunta.Alternativas.Select(alternativa => (alternativa.Opcao, alternativa.Texto)).ToList())).ToList()
        };
    }
}