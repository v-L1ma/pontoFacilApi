using System.ComponentModel.DataAnnotations;

public class CadastrarColaboradorDto
{
    [Required]
    public string Nome { get; set; }
    [Required]
    public int CargoId { get; set; }
}