using Microsoft.AspNetCore.Mvc;
using pontoFacilApi.source.Application.DTOs;
using pontoFacilApi.source.Domain.Models;

[ApiController]
[Route("[Controller]")]
public class AutenticacaoController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public AutenticacaoController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("cadastro")]
    public async Task<ActionResult<ResponseBase<Usuario>>> CadastrarUsuario(CadastrarUsuarioDTO dto)
    {
        ResponseBase<Usuario> response = await _usuarioService.CadastrarUsuario(dto);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ResponseBase<string>>> LoginUsuario(LoginUsuarioDTO dto)
    {
        ResponseBase<string> response = await _usuarioService.LoginUsuario(dto);
        return Ok(response);
    }
}