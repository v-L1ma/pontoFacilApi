using pontoFacilApi.source.Application.DTOs;
using pontoFacilApi.source.Domain.Models;

namespace pontoFacilApi.source.Application.Usecases.CadastrarUsuario;
public interface ICadastrarUsuarioUseCase
{
    Task<ResponseBase<Usuario>> Executar(CadastrarUsuarioDTO dto);
}