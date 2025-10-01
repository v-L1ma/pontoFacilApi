using Microsoft.AspNetCore.Identity;
using pontoFacilApi.source.Application.DTOs;
using pontoFacilApi.source.Domain.Models;

namespace pontoFacilApi.source.Application.Usecases.CadastrarUsuario;

public class CadastrarUsuarioUseCase : ICadastrarUsuarioUseCase
{
    private readonly UserManager<Usuario> _userManager;
    public CadastrarUsuarioUseCase(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }
    public async Task<ResponseBase<Usuario>> Executar(CadastrarUsuarioDTO dto)
    {
        Usuario usuario = new Usuario
        {
            UserName = dto.Username,
            CargoId = 1,
            Email = dto.Email
        };

        if (await _userManager.FindByEmailAsync(dto.Email)!=null)
        {
            throw new ApplicationException("Email já está em uso.");
        }

        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

        Usuario? usuarioBanco = await _userManager.FindByEmailAsync(dto.Email);

        if (!resultado.Succeeded || usuarioBanco == null)
        {
            throw new ApplicationException("Falha ao cadastrar usuário!");
        }

        await _userManager.AddToRoleAsync(usuarioBanco, "Colaborador");

        return new ResponseBase<Usuario>()
        {
            Message = "Usuário cadastrado com sucesso!",
            Dados = usuarioBanco
        };
    }
}