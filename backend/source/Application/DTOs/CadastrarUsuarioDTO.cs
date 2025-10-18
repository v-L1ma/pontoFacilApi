using System.ComponentModel.DataAnnotations;

namespace pontoFacilApi.source.Application.DTOs;

public class CadastrarUsuarioDTO
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public int CargoId { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
}