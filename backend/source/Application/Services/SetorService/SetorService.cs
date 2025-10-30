
using System.Threading.Tasks;

public class SetorService : ISetorService
{
    private ISetorRepository _setorRepository;
    public SetorService(
        ISetorRepository setorRepository
    )
    {
        _setorRepository = setorRepository;
    }
    public async Task<ResponseBase<List<SetorDto>>> BuscarTodos(){

        List<SetorDto> setores = await _setorRepository.BuscarTodos();

        if(setores is null)
        {
            throw new NaoEncontradoException("Nenhum setor cadastrado.");
        }

        return new ResponseBase<List<SetorDto>>
        {
            Dados = setores,
            Message = "Setores listados com sucesso!"
        };
    }
    public async Task<ResponseBase<PaginacaoDTO<SetorDto>>> BuscarTodosPaginado(int pageSize, int pageNumber)
    {
        if (pageSize < 0 || pageNumber < 0)
        {
            throw new ParametroInvalidoException("O tamanho e o numero da pagina devem ser maior que zero!");
        }

        PaginacaoDTO<SetorDto> setores = await _setorRepository.BuscarTodosPaginado(pageSize, pageNumber);

        if(setores is null)
        {
            throw new NaoEncontradoException("Nenhum setor cadastrado.");
        }

        return new ResponseBase<PaginacaoDTO<SetorDto>>
        {
            Dados = setores,
            Message = "Setores listados com sucesso!"
        };
    }

    public async Task<ResponseBase<SetorDto>> Cadastrar(CadastrarSetorDto dto)
    {
        dto.Nome = dto.Nome.Trim();
        if (string.IsNullOrEmpty(dto.Nome) || dto.Nome.Length <= 5)
        {
            throw new ParametroInvalidoException("Digite um nome válido para o setor.");
        }

        SetorDto? setor = _setorRepository.BuscarPorNome(dto.Nome);

        if (setor != null)
        {
            throw new ParametroInvalidoException("Já existem um setor com esse nome.");
        }

        SetorDto setorNovo = await _setorRepository.Cadastrar(dto);

        return new ResponseBase<SetorDto>
        {
            Dados=setorNovo,
            Message="Cargo criado com sucesso!"
        };
    }

    public async Task<ResponseBase<SetorDto>> Editar(int id, EditarSetorDTO dto)
    {
        dto.Nome = dto.Nome.Trim();
        if (id <= 0)
        {
            throw new ParametroInvalidoException("Forneça um id válido.");
        }

        if (string.IsNullOrEmpty(dto.Nome) || dto.Nome.Length <= 5)
        {
            throw new ParametroInvalidoException("Digite um nome válido para o setor.");
        }

        SetorDto? setor = _setorRepository.BuscarPorNome(dto.Nome);

        if (setor != null)
        {
            throw new ParametroInvalidoException("Já existem um setor com esse nome.");
        }

        SetorDto setorNovo = await _setorRepository.Editar(id,dto);

        return new ResponseBase<SetorDto>
        {
            Dados=setorNovo,
            Message="Setor editado com sucesso!"
        };
    }

    public async Task<ResponseBase<string>> Excluir(int id)
    {
        if (id <= 0)
        {
            throw new ParametroInvalidoException("Forneça um id válido.");
        }

        SetorDto? setor = _setorRepository.BuscarPorId(id);

        if (setor is null)
        {
            throw new NaoEncontradoException("Setor fornecido não encontrado.");
        }

        await _setorRepository.Excluir(id);

        return new ResponseBase<string>
        {
            Dados=null,
            Message="Setor removido com sucesso!"
        };
    }
}