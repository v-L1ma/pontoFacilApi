using pontoFacilApi.source.Domain.Models;

public interface IBuscarUsuariosPaginadoUsecase
{
    public ResponseBase<List<Usuario>> Executar(int pageSize, int pageNumber);
}