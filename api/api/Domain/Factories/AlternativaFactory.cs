using api.Domain.DTOs.Alternativa;
using api.Domain.Model;

namespace api.Domain.Factories;

public static class AlternativaFactory
{
    public static Alternativa CriarAlternativa(IncluirAlternativaDto alternativaDto)
    {
        Alternativa alternativa = new Alternativa
        {
            Opcao = alternativaDto.Opcao,
            Texto = alternativaDto.Texto
        };

        return alternativa;
    }
    
    public static Alternativa CriarAlternativa(AlterarAlternativaDto alternativaDto)
    {
        Alternativa alternativa = new Alternativa(alternativaDto.Id)
        {
            Opcao = alternativaDto.Opcao,
            Texto = alternativaDto.Texto
        };

        return alternativa;
    }
}