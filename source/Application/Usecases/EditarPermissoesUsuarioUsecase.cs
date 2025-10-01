using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using pontoFacilApi.source.Domain.Models;

public class EditarPermissoesUsuarioUsecase : IEditarPermissoesUsuarioUsecase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IUsuariosRepository _usuariosRepository;

    public EditarPermissoesUsuarioUsecase(UserManager<Usuario> userManager, IUsuariosRepository usuariosRepository)
    {
        _userManager = userManager;
        _usuariosRepository = usuariosRepository;
    }

    public async Task<ResponseBase<Usuario>> Executar(string idUsuario, AdminEditarUsuarioDTO dto)
    {
        Usuario? usuarioBanco = await _userManager.FindByIdAsync(idUsuario);

        if (usuarioBanco is null)
        {
            throw new ApplicationException("Usuário não cadastrado");
        }

        if (!string.IsNullOrEmpty(dto.Role) && await _userManager.IsInRoleAsync(usuarioBanco, dto.Role))
        {
            throw new ApplicationException($"Usuário já possui permissões de {dto.Role}");
        }
        
        var usuarioRole = await _userManager.GetRolesAsync(usuarioBanco);

        foreach (var item in usuarioRole)
        {
            await _userManager.RemoveFromRoleAsync(usuarioBanco, item);
        }

        if (!string.IsNullOrEmpty(dto.Role)){

            var result = await _userManager.AddToRoleAsync(usuarioBanco, dto.Role);

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Ocorreu um erro ao alterar permissoes");
            }
        }

        await _usuariosRepository.AdminEditarPerfil(idUsuario, dto);

        usuarioBanco = await _userManager.FindByIdAsync(idUsuario);

        return new ResponseBase<Usuario>()
        {
            Message = "Permissões alteradas com sucesso.",
            Dados = usuarioBanco
        };
    }
}