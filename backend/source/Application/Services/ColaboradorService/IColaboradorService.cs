using pontoFacilApi.source.Domain.Models;

public interface IColaboradorService
{
    ResponseBase<ColaboradorDto> BuscarColaboradorPorId(string idUsuario);
    Task<ResponseBase<PaginacaoDTO<ColaboradorDto>>> BuscarColaboradoresPaginado(int pageSize, int pageNumber);
    Task<ResponseBase<ColaboradorDto>> EditarColaborador(string idUsuario, EditarColaboradorDTO dto);
    ResponseBase<string> ExcluirColaborador(string idUsuario);
    Task<ResponseBase<ColaboradorDto>> CadastrarColaborador(CadastrarColaboradorDto dto);
}