using Microsoft.AspNetCore.Identity;
using pontoFacilApi.source.Application.Usecases.LoginUsuario;
using pontoFacilApi.source.Domain.Models;

public class LoginUsuarioUsecase : ILoginUsuarioUsecase
{
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UserManager<Usuario> _userManager;
    private readonly ITokenService _tokenService;
    public LoginUsuarioUsecase
    (
        SignInManager<Usuario> signInManager,
        UserManager<Usuario> userManager,
        ITokenService tokenService
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<ResponseBase<string>> Executar(LoginUsuarioDTO dto)
    {
        var usuarioBanco = await _userManager.FindByEmailAsync(dto.Email);

        if (usuarioBanco == null)
        {
            throw new ApplicationException("Usuario não cadastrado.");
        }

        var usuarioRole = await _userManager.GetRolesAsync(usuarioBanco);

        var resultado = await _signInManager.PasswordSignInAsync(usuarioBanco, dto.Password, false, false);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Senha inválida.");
        }

        string token = _tokenService.GerarToken(usuarioBanco,usuarioRole[0]);

        return new ResponseBase<string>()
        {
            Message = "Login efetuado com sucesso!",
            Dados = token
        };
    }
}