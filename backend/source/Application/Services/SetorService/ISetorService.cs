public interface ISetorService
{   
    Task<ResponseBase<List<SetorDto>>> BuscarTodos();
    Task<ResponseBase<PaginacaoDTO<SetorDto>>> BuscarTodosPaginado(int pageSize, int pageNumber);

    Task<ResponseBase<SetorDto>> Cadastrar(CadastrarSetorDto dto);

    Task<ResponseBase<SetorDto>> Editar(int id, EditarSetorDTO dto);

    Task<ResponseBase<string>> Excluir(int id);
}