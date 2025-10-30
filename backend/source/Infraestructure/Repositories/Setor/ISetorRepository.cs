public interface ISetorRepository
{
    Task<List<SetorDto>> BuscarTodos();
    Task<PaginacaoDTO<SetorDto>> BuscarTodosPaginado(int pageSize, int pageNumber);
    SetorDto? BuscarPorId(int id);
    SetorDto? BuscarPorNome(string nome);
    Task<SetorDto> Cadastrar(CadastrarSetorDto dto);
    Task<SetorDto> Editar(int id, EditarSetorDTO dto);
    Task Excluir(int id);
}