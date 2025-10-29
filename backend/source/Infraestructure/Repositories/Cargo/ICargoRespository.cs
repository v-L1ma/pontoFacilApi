public interface ICargoRespository
{
    CargoDto? BuscarPorId(int id);
    CargoDto? BuscarPorNome(string nome);
    List<CargoDto> BuscarPorSetor(int idSetor);
    Task<PaginacaoDTO<CargoDto>> BuscarTodos(int pageSize, int pageNumber);
    Task<CargoDto> Cadastrar(CadastrarCargoDto dto);
    Task<CargoDto> Editar(int id, EditarCargoDTO dto);
    Task Excluir(int id);
}