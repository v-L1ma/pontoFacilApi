
using pontoFacilApi.source.Application.DTOs;
using pontoFacilApi.source.Domain.Models;

public interface IUsuarioService
{
    ResponseBase<Usuario> BuscarUsuarioPorId(string idUsuario);
    // ResponseBase<List<UsuarioDto>> BuscarUsuarioPaginado(int pageSize, int pageNumber);
    Task<ResponseBase<string>> EditarUsuario(string idUsuario, EditarUsuarioDTO dto);
    Task<ResponseBase<string>> ExcluirUsuario(string idUsuario);
    Task<ResponseBase<Usuario>> CadastrarUsuario(CadastrarUsuarioDTO dto);
    Task<ResponseBase<string>> LoginUsuario(LoginUsuarioDTO dto);
    Task<ResponseBase<string>> AlterarSenha(string id,AlterarSenhaDTO dto);

}