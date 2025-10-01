using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class RegistroPontoController : ControllerBase
{

    [HttpGet("data/{idUsuario}")]
    public void BuscarRegistrosPorData(string idUsuario, [FromQuery] string data)
    {
        throw new NotImplementedException();
    }

    [HttpGet("periodo/{idUsuario}")]
    public void BuscarRegistrosPorPeriodo(string idUsuario, [FromQuery] string periodo)
    {
        throw new NotImplementedException();
    }

    [HttpGet("saldoHoras/{idUsuario}")]
    public void BuscarSaldo(string idUsuario)
    {
        throw new NotImplementedException();
    }

    [HttpPost()]
    public void RegistrarPonto([FromBody] string RegistrarPontoDTO)
    {
        throw new NotImplementedException();
    }


}