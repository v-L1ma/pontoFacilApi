using System.Security.Claims;
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
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly IEditarPermissoesUsuarioUsecase _editarPermissoesUsuarioUsecase;
    private readonly IBuscarUsuarioPorIdUsecase _buscarUsuarioPorIdUsecase;
    private readonly IBuscarUsuariosPaginadoUsecase _buscarUsuariosPaginadoUsecase;
    private readonly IEditarUsuarioUsecase _editarUsuarioUsecase;

    public UsuariosController(
        IEditarPermissoesUsuarioUsecase editarPermissoesUsuarioUsecase,
        IBuscarUsuarioPorIdUsecase buscarUsuarioPorIdUsecase,
        IBuscarUsuariosPaginadoUsecase buscarUsuariosPaginadoUsecase,
        IEditarUsuarioUsecase editarUsuarioUsecase)
    {
        _editarPermissoesUsuarioUsecase = editarPermissoesUsuarioUsecase;
        _buscarUsuarioPorIdUsecase = buscarUsuarioPorIdUsecase;
        _buscarUsuariosPaginadoUsecase = buscarUsuariosPaginadoUsecase;
        _editarUsuarioUsecase = editarUsuarioUsecase;
    }

    [Authorize(Roles = "Admin,RH,Gestor")]
    [HttpGet("buscarUsuariosPaginado")]
    public ActionResult<ResponseBase<List<Usuario>>> BuscarUsuariosPaginado([FromQuery] int pageSize, int pageNumber)
    {
        ResponseBase<List<Usuario>> response = _buscarUsuariosPaginadoUsecase.Executar(pageSize, pageNumber);
        return Ok(response);
    }

    [HttpGet("{idUsuario}")]
    public ActionResult<ResponseBase<Usuario>> BuscarPorId(string idUsuario)
    {
        ResponseBase<Usuario> response = _buscarUsuarioPorIdUsecase.Executar(idUsuario);
        return Ok(response);
    }

    [HttpPut()]
    public async Task<ActionResult<ResponseBase<string>>> EditarPerfil([FromBody] EditarUsuarioDTO dto)
    {
        string? idUsuario = User.FindFirst("Id")?.Value;
        ResponseBase<string> response = await _editarUsuarioUsecase.Executar(idUsuario!,dto);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("/{idUsuario}")]
    public async Task<ActionResult<ResponseBase<Usuario>>> AdminEditarPerfilUsuarioPorId(string idUsuario, [FromBody] AdminEditarUsuarioDTO dto)
    {
        ResponseBase<Usuario> response = await _editarPermissoesUsuarioUsecase.Executar(idUsuario, dto);
        return Ok(response);
    }

    [HttpDelete("{idUsuario}")]
    public async Task<ActionResult<ResponseBase<Usuario>>> DesativarPerfil(string idUsuario)
    {
        throw new NotImplementedException();
        // return Ok(response);
    }

}