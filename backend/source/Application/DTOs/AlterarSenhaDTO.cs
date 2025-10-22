using System.ComponentModel.DataAnnotations;

public class AlterarSenhaDTO
{
    [Required]
    public string SenhaAtual { get; set; }
    [Required]
    public string SenhaNova { get; set; }
    [Required]
    public string ConfirmarSenhaNova { get; set; }
}