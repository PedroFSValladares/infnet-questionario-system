using System.ComponentModel.DataAnnotations;

namespace api.Domain.DTOs.Alternativa;

public class AlterarAlternativaDto : AlternativaDto
{
    [Required(ErrorMessage = "O Id da alternativa deve ser informado.")]
    public Guid Id { get; set; }
    
    public Model.Alternativa ToEntity()
    {
        return new Model.Alternativa(Id, Opcao, Texto);
    }
}