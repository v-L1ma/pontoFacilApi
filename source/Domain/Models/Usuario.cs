using Microsoft.AspNetCore.Identity;

namespace pontoFacilApi.source.Domain.Models;

public class Usuario : IdentityUser
{
    
    public int CargoId { get; set; }
    public virtual Cargo Cargo { get; set; } 
    public virtual ICollection<RegistroPonto> RegistrosPontos { get; set; }
    public Usuario() : base() { }
}