using System.ComponentModel.DataAnnotations;

public class CargoDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; }
    [Required]
    public int SetorId { get; set; }
}