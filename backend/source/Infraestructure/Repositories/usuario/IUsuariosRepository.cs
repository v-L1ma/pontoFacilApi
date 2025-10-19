using pontoFacilApi.source.Domain.Models;

public interface IUsuariosRepository
{
    public List<UsuarioDto> BuscarUsuariosPaginado(int pageSize, int pageNumber);
    public Usuario? BuscarPorId(string idUsuario);
    public Usuario? BuscarPorEmail(string email);
    public Task EditarPerfil(string id, EditarUsuarioDTO dto);
    public Task DesativarPerfil(string idUsuario);
}