using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class SetorController : ControllerBase
{
    private readonly ISetorService _setorService;
    public SetorController(ISetorService setorService)
    {
        _setorService = setorService;
    }

     [HttpGet]
    public async Task<ActionResult<ResponseBase<PaginacaoDTO<SetorDto>>>> BuscarTodos([FromQuery] int pageSize, int pageNumber)
    {
        ResponseBase<PaginacaoDTO<SetorDto>> response = await _setorService.BuscarTodos(pageSize,pageNumber);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseBase<SetorDto>>> Cadastrar (CadastrarSetorDto dto)
    {
        ResponseBase<SetorDto> response = await _setorService.Cadastrar(dto);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseBase<SetorDto>>> Editar(int id, [FromBody] EditarSetorDTO dto)
    {    
        ResponseBase<SetorDto> response = await _setorService.Editar(id, dto);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseBase<string>>> Excluir(int id)
    {
        ResponseBase<string> response = await _setorService.Excluir(id);
        return Ok(response);
    }
}