using pontoFacilApi.source.Domain.Models;

public interface IColaboradorService
{
    ResponseBase<Colaborador> BuscarColaboradorPorId(string idUsuario);
    ResponseBase<List<Colaborador>> BuscarColaboradoresPaginado(int pageSize, int pageNumber);
    Task<ResponseBase<Colaborador>> EditarColaborador(string idUsuario, EditarColaboradorDTO dto);
    ResponseBase<string> ExcluirColaborador(string idUsuario);
    ResponseBase<string> CadastrarColaborador(CadastrarColaboradorDto dto);
}