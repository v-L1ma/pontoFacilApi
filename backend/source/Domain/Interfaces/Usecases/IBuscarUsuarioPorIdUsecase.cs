using pontoFacilApi.source.Domain.Models;

public interface IBuscarUsuarioPorIdUsecase
{
    public ResponseBase<Usuario> Executar(string idUsuario);
}