public interface ISetorService
{   
    Task<ResponseBase<PaginacaoDTO<SetorDto>>> BuscarTodos(int pageSize, int pageNumber);

    Task<ResponseBase<SetorDto>> Cadastrar(CadastrarSetorDto dto);

    Task<ResponseBase<SetorDto>> Editar(int id, EditarSetorDTO dto);

    Task<ResponseBase<string>> Excluir(int id);
}