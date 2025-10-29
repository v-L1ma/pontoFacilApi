public interface ICargoService
{
    Task<ResponseBase<PaginacaoDTO<CargoDto>>> BuscarTodos(int pageSize, int pageNumber);
    ResponseBase<List<CargoDto>> BuscarPorSetor(int idSetor);

    Task<ResponseBase<CargoDto>> Cadastrar(CadastrarCargoDto dto);

    Task<ResponseBase<CargoDto>> Editar(int id, EditarCargoDTO dto);

    Task<ResponseBase<string>> Excluir(int id);
}