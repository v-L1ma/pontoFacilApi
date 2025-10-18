using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pontoFacilApi.source.Domain.Models;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class ColaboradorController : ControllerBase
{

private readonly IColaboradorService _colaboradorService;

    public ColaboradorController(
        IColaboradorService colaboradorService
    )
    {
        _colaboradorService = colaboradorService;
    }

    [HttpGet("buscarColaboradoresPaginado")]
    public ActionResult<ResponseBase<List<Colaborador>>> BuscarColaboradoresPaginado([FromQuery] int pageSize, int pageNumber)
    {
        ResponseBase<List<Colaborador>> response = _colaboradorService.BuscarColaboradoresPaginado(pageSize, pageNumber);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<ResponseBase<Colaborador>> BuscarPorId(string idUsuario)
    {
        ResponseBase<Colaborador> response = _colaboradorService.BuscarColaboradorPorId(idUsuario);
        return Ok(response);
    }

    [HttpPost]
    public ActionResult<ResponseBase<string>> CadastrarColaborador (CadastrarColaboradorDto dto)
    {
        ResponseBase<string> response = _colaboradorService.CadastrarColaborador(dto);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseBase<Colaborador>>> EditarPerfil(string idUsuario, [FromBody] EditarColaboradorDTO dto)
    {    
        ResponseBase<Colaborador> response = await _colaboradorService.EditarColaborador(idUsuario, dto);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult<ResponseBase<string>> ExcluirPerfil(string idUsuario)
    {
        ResponseBase<string> response = _colaboradorService.ExcluirColaborador(idUsuario);
        return Ok(response);
    }



}