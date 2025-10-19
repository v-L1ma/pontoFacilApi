using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pontoFacilApi.source.Domain.Models;

namespace pontoFacilApi.source.Web.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(
        IUsuarioService usuarioService
    )
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("buscarUsuariosPaginado")]
    public ActionResult<ResponseBase<List<UsuarioDto>>> BuscarUsuariosPaginado([FromQuery] int pageSize, int pageNumber)
    {
        ResponseBase<List<UsuarioDto>> response = _usuarioService.BuscarUsuarioPaginado(pageSize, pageNumber);
        return Ok(response);
    }

    [HttpGet("{idUsuario}")]
    public ActionResult<ResponseBase<Usuario>> BuscarPorId(string idUsuario)
    {
        ResponseBase<Usuario> response = _usuarioService.BuscarUsuarioPorId(idUsuario);
        return Ok(response);
    }

    [HttpPut()]
    public async Task<ActionResult<ResponseBase<string>>> EditarPerfil([FromBody] EditarUsuarioDTO dto)
    {
        string? idUsuario = User.FindFirst("Id")?.Value;
        ResponseBase<string> response = await _usuarioService.EditarUsuario(idUsuario!,dto);
        return Ok(response);
    }

    [HttpDelete("{idUsuario}")]
    public async Task<ActionResult<ResponseBase<string>>> ExcluirPerfil(string idUsuario)
    {
        ResponseBase<string> response = await _usuarioService.ExcluirUsuario(idUsuario);
        return Ok(response);
    }

}