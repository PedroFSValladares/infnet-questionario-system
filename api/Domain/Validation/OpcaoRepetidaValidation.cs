using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;
using api.Domain.DTOs.Pergunta;

namespace api.Domain.Validation;

public class OpcaoRepetidaValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (validationContext.ObjectType == typeof(IncluirPerguntaDto))
            return Validate(value as List<IncluirAlternativaDto>);
        
        return Validate(value as List<AlterarAlternativaDto>);
    }

    private ValidationResult? Validate(List<IncluirAlternativaDto> alternativas)
    {
        foreach (var alternativa in alternativas)
        {
            var ocorrencias = alternativas.FindAll(x => x.Opcao == alternativa.Opcao).Count;
            if (ocorrencias > 1)
                return new ValidationResult("A alternativa não pode conter opções repetidas");
        }
        return ValidationResult.Success;
    }
    
    private ValidationResult? Validate(List<AlterarAlternativaDto> alternativas)
    {
        foreach (var alternativa in alternativas)
        {
            var ocorrencias = alternativas.FindAll(x => x.Opcao == alternativa.Opcao).Count;
            if (ocorrencias > 1)
                return new ValidationResult("A alternativa não pode conter opções repetidas");
        }
        return ValidationResult.Success;
    }
}