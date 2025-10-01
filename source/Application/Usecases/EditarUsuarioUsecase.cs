using pontoFacilApi.source.Domain.Models;

public class EditarUsuarioUsecase : IEditarUsuarioUsecase
{
    private readonly IUsuariosRepository _usuariosRepository;

    public EditarUsuarioUsecase(IUsuariosRepository usuariosRepository)
    {
        _usuariosRepository = usuariosRepository;
    }
    public async Task<ResponseBase<string>> Executar(string idUsuario, EditarUsuarioDTO dto)
    {
        if (string.IsNullOrEmpty(idUsuario))
        {
            throw new ApplicationException("O Id não pode ser vazio.");
        }

        Usuario? usuarioBanco = _usuariosRepository.BuscarPorId(idUsuario);

        if (usuarioBanco is null)
        {
            throw new ApplicationException("Usuario não encontrado.");
        }
        
        if (!string.IsNullOrEmpty(dto.Email) && _usuariosRepository.BuscarPorEmail(dto.Email) != null && usuarioBanco.Email != dto.Email)
        {
            throw new ApplicationException("O email fornecido já está em uso.");
        }

        await _usuariosRepository.EditarPerfil(idUsuario, dto);

        return new ResponseBase<string>
        {
            Message="Informações atualizadas com sucesso."
        };
    }
}