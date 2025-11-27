using api.Context;
using api.Domain.Interfaces;
using api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Domain.Repositories;

public class PesquisaRepository : IIncludableRepository<Pesquisa>, IUpdatebleRepository<Pesquisa>, IListableRepository<Pesquisa>, IDeletableRepository<Guid>
{
    
    private readonly QuestionarioContext _context;
    
    public PesquisaRepository(QuestionarioContext context)
    {
        _context = context;
    }
    
    public async Task<Pesquisa> SalvarAsync(Pesquisa entity)
    {
        var pesquisa = await _context.Pesquisas.AddAsync(entity);
        await _context.SaveChangesAsync();
        return pesquisa.Entity;
    }

    public async Task<Pesquisa?> ObterPorIdAsync(Guid id)
    {
        var pesquisa = await _context.Pesquisas
            .Where(p => p.Id == id)
            .Include(p => p.Perguntas)
            .ThenInclude(p => p.Alternativas)
            .FirstOrDefaultAsync();
        return pesquisa;
    }
    
    public async Task<Pesquisa?> AtualizarAsync(Pesquisa pesquisaDto)
    {
        var pesquisa = await ObterPorIdAsync(pesquisaDto.Id);
        if (pesquisa == null) return null;

        pesquisa.Nome = pesquisaDto.Nome;
        pesquisa.StatusPesquisa = pesquisaDto.StatusPesquisa;
        pesquisa.DataDispnibilizacao = pesquisaDto.DataDispnibilizacao;
        pesquisa.DataFinalizacao = pesquisaDto.DataFinalizacao;
        
        var perguntasARemover = pesquisa.Perguntas
            .Where(p => pesquisaDto.Perguntas.All(e => e.Id != p.Id))
            .ToList();
        foreach (var pergunta in perguntasARemover)
            pesquisa.Perguntas.Remove(pergunta);

        foreach (var pergunta in pesquisaDto.Perguntas)
        {
            var perguntaExistente = pesquisa.Perguntas.FirstOrDefault(p => p.Id == pergunta.Id);

            if (perguntaExistente != null)
            {
                perguntaExistente.Enunciado = pergunta.Enunciado;
                AtualizarAlternativas(perguntaExistente.Alternativas, pergunta.Alternativas);
            }
            else
            {
                pesquisa.Perguntas.Add(pergunta);
                _context.Entry(pergunta).State = EntityState.Added;
            }
        }
        
        await _context.SaveChangesAsync();
        return pesquisa;
    }

    public async Task<List<Pesquisa>> ListarTodosAsync()
    {
        return await _context.Pesquisas.ToListAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var pesquuisa = await ObterPorIdAsync(id);
        
        if (pesquuisa == null) return false;
        
        _context.Pesquisas.Remove(pesquuisa);
        await _context.SaveChangesAsync();
        return true;
    }

    private void AtualizarAlternativas(List<Alternativa> alternativasExistentes, List<Alternativa> alternativasDto)
    {
        var alterntantivasARemover = alternativasExistentes
            .Where(a => alternativasDto.All(x => a.Id != x.Id))
            .ToList();
        foreach (var alternativa in alterntantivasARemover)
            alternativasExistentes.Remove(alternativa);

        foreach (var alternativa in alternativasDto)
        {
            var alternativaExistente = alternativasExistentes.FirstOrDefault(a => a.Id == alternativa.Id);
            if(alternativaExistente == null){
                alternativasExistentes.Add(alternativa);
                _context.Entry(alternativa).State = EntityState.Added;
            }
            else
            {
                alternativaExistente.Opcao = alternativa.Opcao;
                alternativaExistente.Texto = alternativa.Texto;
            }
        }
    }
}