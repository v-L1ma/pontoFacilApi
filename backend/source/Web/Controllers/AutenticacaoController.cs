using Microsoft.AspNetCore.Mvc;
using pontoFacilApi.source.Application.DTOs;
using pontoFacilApi.source.Application.Usecases.CadastrarUsuario;
using pontoFacilApi.source.Application.Usecases.LoginUsuario;
using pontoFacilApi.source.Domain.Models;

[ApiController]
[Route("[Controller]")]
public class AutenticacaoController : ControllerBase
{
    private readonly ICadastrarUsuarioUseCase _cadastrarUsuarioUsecase;
    private readonly ILoginUsuarioUsecase _loginUsuarioUsecase;

    public AutenticacaoController(ICadastrarUsuarioUseCase cadastrarUsuarioUseCase, ILoginUsuarioUsecase loginUsuarioUsecase)
    {
        _cadastrarUsuarioUsecase = cadastrarUsuarioUseCase;
        _loginUsuarioUsecase = loginUsuarioUsecase;
    }

    [HttpPost("cadastro")]
    public async Task<ActionResult<ResponseBase<Usuario>>> CadastrarUsuario(CadastrarUsuarioDTO dto)
    {
        ResponseBase<Usuario> response = await _cadastrarUsuarioUsecase.Executar(dto);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ResponseBase<string>>> LoginUsuario(LoginUsuarioDTO dto)
    {
        ResponseBase<string> response = await _loginUsuarioUsecase.Executar(dto);
        return Ok(response);
    }
}