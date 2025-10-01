using pontoFacilApi.source.Domain.Models;

namespace pontoFacilApi.source.Application.Usecases.LoginUsuario;
public interface ILoginUsuarioUsecase
{
    Task<ResponseBase<string>> Executar(LoginUsuarioDTO dto);
}