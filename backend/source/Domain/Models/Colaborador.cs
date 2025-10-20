using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace pontoFacilApi.source.Domain.Models;

public class Colaborador
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public int CargoId { get; set; }
    public virtual Cargo Cargo { get; set; } = null!;
}