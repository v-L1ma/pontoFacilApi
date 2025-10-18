using pontoFacilApi.source.Domain.Models;

public class Cargo
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int SetorId { get; set; }
    public virtual Setor Setor { get; set; }
    public virtual ICollection<Colaborador> Colaboradores { get; set; }
}