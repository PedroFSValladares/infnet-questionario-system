using api.Context;
using api.Domain.Interfaces;
using api.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Domain.Repositories;

public class PerguntaRepository : IIncludableRepository<Pergunta>, IQueryableRepository<Guid, Pergunta>
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
        return _context.Perguntas.Include(x => x.Alternativas).FirstOrDefault();
    }

    public List<Pergunta> ObterTodos()
    {
        return  _context.Perguntas.ToList();
    }
}