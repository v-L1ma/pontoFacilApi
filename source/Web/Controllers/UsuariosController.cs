using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pontoFacilApi.source.Application.DTOs;
using pontoFacilApi.source.Application.Usecases.CadastrarUsuario;
using pontoFacilApi.source.Application.Usecases.LoginUsuario;
using pontoFacilApi.source.Domain.Models;

namespace pontoFacilApi.source.Web.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuariosController : ControllerBase
{

    private readonly ICadastrarUsuarioUseCase _cadastrarUsuarioUsecase;
    private readonly ILoginUsuarioUsecase _loginUsuarioUsecase;
    private readonly IEditarPermissoesUsuarioUsecase _editarPermissoesUsuarioUsecase;

    public UsuariosController(ICadastrarUsuarioUseCase cadastrarUsuarioUseCase, ILoginUsuarioUsecase loginUsuarioUsecase, IEditarPermissoesUsuarioUsecase editarPermissoesUsuarioUsecase)
    {
        _cadastrarUsuarioUsecase = cadastrarUsuarioUseCase;
        _loginUsuarioUsecase = loginUsuarioUsecase;
        _editarPermissoesUsuarioUsecase = editarPermissoesUsuarioUsecase;
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

    [Authorize(Roles = "Admin")]
    [HttpPut("permissoes/{idUsuario}")]
    public async Task<ActionResult<ResponseBase<Usuario>>> EditarPermissoesUsuario(string idUsuario, [FromBody] EditarPermissoesUsuarioDTO dto)
    {
        ResponseBase<Usuario> response = await _editarPermissoesUsuarioUsecase.Executar(idUsuario, dto.novaRole);
        return Ok(response);
    }


}