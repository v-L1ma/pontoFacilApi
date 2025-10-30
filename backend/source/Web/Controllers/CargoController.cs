using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pontoFacilApi.source.Domain.Models;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class CargosController : ControllerBase
{

private readonly ICargoService _cargoService;

    public CargosController(
        ICargoService cargoService
    )
    {
        _cargoService = cargoService;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseBase<PaginacaoDTO<CargoDto>>>> BuscarTodos([FromQuery] int pageSize, int pageNumber)
    {
        ResponseBase<PaginacaoDTO<CargoDto>> response = await _cargoService.BuscarTodos(pageSize,pageNumber);
        return Ok(response);
    }

    [HttpGet("setor/{id}")]
    public ActionResult<ResponseBase<List<CargoDto>>> BuscarPorSetor(int id)
    {
        ResponseBase<List<CargoDto>> response = _cargoService.BuscarPorSetor(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseBase<CargoDto>>> Cadastrar (CadastrarCargoDto dto)
    {
        ResponseBase<CargoDto> response = await _cargoService.Cadastrar(dto);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseBase<CargoDto>>> Editar(int id, [FromBody] EditarCargoDTO dto)
    {    
        ResponseBase<CargoDto> response = await _cargoService.Editar(id, dto);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseBase<string>>> Excluir(int id)
    {
        ResponseBase<string> response = await _cargoService.Excluir(id);
        return Ok(response);
    }

}