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
    private readonly IEditarPermissoesUsuarioUsecase _editarPermissoesUsuarioUsecase;

    public UsuariosController(IEditarPermissoesUsuarioUsecase editarPermissoesUsuarioUsecase)
    {
        _editarPermissoesUsuarioUsecase = editarPermissoesUsuarioUsecase;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("permissoes/{idUsuario}")]
    public async Task<ActionResult<ResponseBase<Usuario>>> EditarPermissoesUsuario(string idUsuario, [FromBody] EditarPermissoesUsuarioDTO dto)
    {
        ResponseBase<Usuario> response = await _editarPermissoesUsuarioUsecase.Executar(idUsuario, dto.novaRole);
        return Ok(response);
    }


}