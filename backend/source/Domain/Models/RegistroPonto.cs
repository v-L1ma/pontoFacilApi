using pontoFacilApi.source.Domain.Models;

public class RegistroPonto
{
    public int Id { get; set; }

    public string UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
    public DateTime DataHora { get; set; }
}