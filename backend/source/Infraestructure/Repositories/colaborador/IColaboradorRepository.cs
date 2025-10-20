using pontoFacilApi.source.Domain.Models;

public interface IColaboradorRepository
{
    public Task<List<ColaboradorDto>> BuscarColaboradoresPaginado(int pageSize, int pageNumber);
    public ColaboradorDto? BuscarPorId(string id);
    public ColaboradorDto? BuscarPorCPF(string cpf);
    public Task<ColaboradorDto> EditarColaborador(string id, EditarColaboradorDTO dto);
    public Task<ColaboradorDto> CadastrarColaborador(CadastrarColaboradorDto dto);
    public Task ExcluirColaborador(string id);
}