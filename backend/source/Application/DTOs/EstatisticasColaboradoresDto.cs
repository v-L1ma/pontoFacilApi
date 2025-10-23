
public class EstatisticasColaboradoresDto
{
    public int ColaboradoresAtivos  { get; set; }
    public int NovosColaboradoresMes { get; set; }
    public int ColaboradoresDemitidos { get; set; }
    public DadosGraficoDto ColaboradorDepartamento { get; set; }
    public DadosGraficoDto ColaboradoresTotalTempo { get; set; }
}