using System.ComponentModel.DataAnnotations;

namespace api.Domain.DTOs.Alternativa;

public class AlterarAlternativaDto : AlternativaDto
{
    [Required(ErrorMessage = "O Id da alternativa deve ser informado.")]
    public Guid Id { get; set; }
}