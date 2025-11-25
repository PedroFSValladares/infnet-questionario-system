using api.Domain.DTOs.Pergunta;
using api.Domain.Model;

namespace api.Domain.Factories;

public static class PerguntaFactory
{
    public static Pergunta NovaPergunta(IncluirPerguntaDto perguntaDto)
    {
        Pergunta pergunta = new Pergunta
        {
            Enunciado = perguntaDto.Enunciado,
            Alternativas = perguntaDto.Alternativas.Select(AlternativaFactory.CriarAlternativa).ToList()
        };

        return pergunta;
    }
    
    public static Pergunta NovaPergunta(AlterarPerguntaDto perguntaDto)
    {
        Pergunta pergunta = new Pergunta(perguntaDto.Id)
        {
            Enunciado = perguntaDto.Enunciado,
            Alternativas = perguntaDto.Alternativas.Select(AlternativaFactory.CriarAlternativa).ToList()
        };

        return pergunta;
    }
}