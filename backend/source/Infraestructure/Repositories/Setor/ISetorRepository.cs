public interface ISetorRepository
{
    Task<PaginacaoDTO<SetorDto>> BuscarTodos(int pageSize, int pageNumber);
    SetorDto? BuscarPorId(int id);
    SetorDto? BuscarPorNome(string nome);
    Task<SetorDto> Cadastrar(CadastrarSetorDto dto);
    Task<SetorDto> Editar(int id, EditarSetorDTO dto);
    Task Excluir(int id);
}