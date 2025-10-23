using System.Threading.Tasks;
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
    public async Task<ActionResult<ResponseBase<PaginacaoDTO<ColaboradorDto>>>> BuscarColaboradoresPaginado([FromQuery] int pageSize, int pageNumber)
    {
        ResponseBase<PaginacaoDTO<ColaboradorDto>> response = await _colaboradorService.BuscarColaboradoresPaginado(pageSize, pageNumber);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<ResponseBase<ColaboradorDto>> BuscarPorId(string id)
    {
        ResponseBase<ColaboradorDto> response = _colaboradorService.BuscarColaboradorPorId(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseBase<ColaboradorDto>>> CadastrarColaborador (CadastrarColaboradorDto dto)
    {
        ResponseBase<ColaboradorDto> response = await _colaboradorService.CadastrarColaborador(dto);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseBase<ColaboradorDto>>> EditarPerfil(string id, [FromBody] EditarColaboradorDTO dto)
    {    
        ResponseBase<ColaboradorDto> response = await _colaboradorService.EditarColaborador(id, dto);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public ActionResult<ResponseBase<string>> ExcluirPerfil(string id)
    {
        ResponseBase<string> response = _colaboradorService.ExcluirColaborador(id);
        return Ok(response);
    }

    [HttpGet("estatisticas")]
    public async Task<ActionResult<ResponseBase<EstatisticasColaboradoresDto>>> EstatisticasColaboradores()
    {
        ResponseBase<EstatisticasColaboradoresDto> response = await _colaboradorService.EstatisticasColaboradores();
        return Ok(response);
    }

}