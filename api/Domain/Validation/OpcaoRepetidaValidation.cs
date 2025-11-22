using System.ComponentModel.DataAnnotations;
using api.Domain.DTOs.Alternativa;

namespace api.Domain.Validation;

public class OpcaoRepetidaValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var opcoes = value as List<IncluirAlternativaDto>;
        foreach (var alternativa in opcoes)
        {
            var ocorrencias = opcoes.FindAll(x => x.Opcao == alternativa.Opcao).Count;
            if (ocorrencias > 1)
                return new ValidationResult("A alternativa não pode conter opções repetidas");
        }
        return ValidationResult.Success;
    }
}