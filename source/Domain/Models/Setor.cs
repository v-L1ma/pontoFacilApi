public class Setor
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public virtual ICollection<Cargo> Cargos { get; set; }
    
}