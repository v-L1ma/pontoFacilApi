using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using pontoFacilApi.source.Domain.Models;

public class EditarPermissoesUsuarioUsecase : IEditarPermissoesUsuarioUsecase
{
    private readonly UserManager<Usuario> _userManager;

    public EditarPermissoesUsuarioUsecase(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ResponseBase<Usuario>> Executar(string idUsuario, string role)
    {
        Usuario? usuarioBanco = await _userManager.FindByIdAsync(idUsuario);

        if (usuarioBanco is null)
        {
            throw new ApplicationException("Usuário não cadastrado");
        }

        if (await _userManager.IsInRoleAsync(usuarioBanco, role))
        {
            throw new ApplicationException($"Usuário já possui permissões de {role}");
        }
        
        var usuarioRole = await _userManager.GetRolesAsync(usuarioBanco);

        foreach (var item in usuarioRole)
        {
            await _userManager.RemoveFromRoleAsync(usuarioBanco, item);
        }

        var result = await _userManager.AddToRoleAsync(usuarioBanco, role);

        if (!result.Succeeded)
        {
            throw new ApplicationException($"Ocorreu um erro ao alterar permissoes");
        }

        return new ResponseBase<Usuario>()
        {
            Message = "Permissões alteradas com sucesso.",
            Dados = usuarioBanco
        };
    }
}