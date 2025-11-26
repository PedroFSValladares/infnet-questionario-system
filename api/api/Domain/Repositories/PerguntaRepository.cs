using api.Context;
using api.Domain.Interfaces;
using api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Domain.Repositories;

public class PerguntaRepository : 
    IIncludableRepository<Pergunta>, IQueryableRepository<Guid, Pergunta>, IUpdatebleRepository<Pergunta>, IDeletableRepository<Guid>
{
    private readonly QuestionarioContext _context;
    
    public PerguntaRepository(QuestionarioContext context)
    {
        _context = context;
    }
    
    public Pergunta Salvar(Pergunta entity)
    {
        _context.Perguntas.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public Pergunta? ObterPorId(Guid id)
    {
        return _context.Perguntas
            .Where(p => p.Id == id)
            .Include(pergunta => pergunta.Alternativas)
            .FirstOrDefault();
    }

    public List<Pergunta> ObterTodos()
    {
        return  _context.Perguntas.ToList();
    }

    public Pergunta? Atualizar(Pergunta entity)
    {
        var pergunta = ObterPorId(entity.Id);

        if (pergunta == null)
            return null;
         
        pergunta.Enunciado = entity.Enunciado;
        pergunta.Alternativas = entity.Alternativas;

        _context.Perguntas.Update(pergunta);
        _context.SaveChanges();
        return pergunta;
    }

    public bool Delete(Guid id)
    {
        var pergunta = ObterPorId(id);
        
        if (pergunta == null) return false;
        
        _context.Perguntas.Remove(pergunta);
        _context.SaveChanges();
        return true;
    }
}