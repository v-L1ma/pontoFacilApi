using Microsoft.AspNetCore.Identity;

namespace pontoFacilApi.source.Domain.Models;

public class Colaborador
{

    public string Id { get; set; }
    public string Nome { get; set; }
    public int CargoId { get; set; }
    public virtual Cargo Cargo { get; set; } 
}