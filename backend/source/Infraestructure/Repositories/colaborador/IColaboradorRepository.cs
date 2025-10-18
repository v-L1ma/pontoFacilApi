using pontoFacilApi.source.Domain.Models;

public interface IColaboradorRepository
{
    public List<Colaborador> BuscarUsuariosPaginado(int pageSize, int pageNumber);
    public Colaborador? BuscarPorId(string id);
    public Colaborador? BuscarPorEmail(string email);
    public Task EditarColaborador(string id, EditarColaboradorDTO dto);
    public void CadastrarColaborador(CadastrarColaboradorDto dto);
    public Task DesativarPerfil(string id);
}