using System.ComponentModel.DataAnnotations;

namespace pontoFacilApi.source.Domain.Models;

public class Colaborador
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime dataCriacao { get; set; } = DateTime.UtcNow;
    public Nullable<DateTime> dataExclusao { get; set; } = null;
    public string Status { get; set; } = StatusEnum.ATIVO.ToString();
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public int CargoId { get; set; }
    public virtual Cargo Cargo { get; set; } = null!;
}