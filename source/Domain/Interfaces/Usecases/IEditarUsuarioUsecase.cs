public interface IEditarUsuarioUsecase
{
    Task<ResponseBase<string>> Executar(string idUsuario, EditarUsuarioDTO dto);
}