using pontoFacilApi.source.Domain.Models;

public interface IEditarPermissoesUsuarioUsecase
{
    Task<ResponseBase<Usuario>> Executar(string idUsuario, AdminEditarUsuarioDTO dto);
}