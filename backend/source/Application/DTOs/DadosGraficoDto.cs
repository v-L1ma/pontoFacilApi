public class DadosGraficoDto
{
    public string?[] Labels { get; set; }
    public DataSet Dataset { get; set; }
}

public class DataSet
{
    public string Label { get; set; }
    public int[] Data { get; set; }
}