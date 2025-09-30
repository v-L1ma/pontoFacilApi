using pontoFacilApi.source.Domain.Models;

public interface ITokenService
{
    public string GerarToken(Usuario usuario, string roles);
}