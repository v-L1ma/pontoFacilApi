using System.ComponentModel.DataAnnotations;

public class EditarCargoDTO
{
    [Required]
    public string Nome { get; set; }
    [Required]
    public int SetorId { get; set; }
}