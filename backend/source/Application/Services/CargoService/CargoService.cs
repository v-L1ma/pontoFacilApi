
using System.Threading.Tasks;

public class CargoService : ICargoService
{
    private ICargoRespository _cargoRespository;
    private ISetorRepository _setorRepository;
    public CargoService(
        ICargoRespository cargoRespository,
        ISetorRepository setorRepository
    )
    {
        _cargoRespository = cargoRespository;
        _setorRepository = setorRepository;
    }
    public async Task<ResponseBase<PaginacaoDTO<CargoDto>>> BuscarTodos(int pageSize, int pageNumber)
    {
        PaginacaoDTO<CargoDto> cargos = await _cargoRespository.BuscarTodos(pageSize, pageNumber);

        if(cargos is null)
        {
            throw new NaoEncontradoException("Nenhum cargo cadastrado.");
        }

        return new ResponseBase<PaginacaoDTO<CargoDto>>
        {
            Dados = cargos,
            Message = "Cargos listados com sucesso!"
        };
    }    
    public ResponseBase<List<CargoDto>> BuscarPorSetor(int idSetor)
    {
        if (idSetor<=0)
        {
            throw new ParametroInvalidoException("Selecione um setor válido");
        }

        List<CargoDto> cargos = _cargoRespository.BuscarPorSetor(idSetor);

        if(cargos is null)
        {
            throw new NaoEncontradoException("Nenhum cargo cadastrado.");
        }

        return new ResponseBase<List<CargoDto>>
        {
            Dados = cargos,
            Message = "Cargos listados com sucesso!"
        };
    }

    public async Task<ResponseBase<CargoDto>> Cadastrar(CadastrarCargoDto dto)
    {
        dto.Nome = dto.Nome.Trim();

        if (string.IsNullOrEmpty(dto.Nome) || dto.Nome.Length < 5)
        {
            throw new ParametroInvalidoException("Digite um nome válido para o cargo.");
        }

        var setor = _setorRepository.BuscarPorId(dto.SetorId);

        if (setor is null)
        {
            throw new NaoEncontradoException("Setor informado não encontrado");
        }

        var cargo = _cargoRespository.BuscarPorNome(dto.Nome);

        if (cargo != null)
        {
            throw new ParametroInvalidoException("Esse cargo já existe");
        }

        CargoDto cargoNovo = await _cargoRespository.Cadastrar(dto);

        return new ResponseBase<CargoDto>
        {
            Dados=cargoNovo,
            Message="Cargo criado com sucesso!"
        };
    }

    public async Task<ResponseBase<CargoDto>> Editar(int id, EditarCargoDTO dto)
    {
        dto.Nome = dto.Nome.Trim();
        if (id <= 0)
        {
            throw new ParametroInvalidoException("Forneça um id válido.");
        }

        if (string.IsNullOrEmpty(dto.Nome) || dto.Nome.Length <= 5)
        {
            throw new ParametroInvalidoException("Digite um nome válido para o cargo.");
        }

        var setor = _setorRepository.BuscarPorId(dto.SetorId);

        if (setor is null)
        {
            throw new NaoEncontradoException("Setor informado não encontrado");
        }

        CargoDto cargoNovo = await _cargoRespository.Editar(id,dto);

        return new ResponseBase<CargoDto>
        {
            Dados=cargoNovo,
            Message="Cargo editado com sucesso!"
        };
    }

    public async Task<ResponseBase<string>> Excluir(int id)
    {
        if (id <= 0)
        {
            throw new ParametroInvalidoException("Forneça um id válido.");
        }

        CargoDto? cargo = _cargoRespository.BuscarPorId(id);

        if (cargo is null)
        {
            throw new NaoEncontradoException("Cargo fornecido não encontrado.");
        }

        await _cargoRespository.Excluir(id);

        return new ResponseBase<string>
        {
            Dados=null,
            Message="Cargo removido com sucesso!"
        };
    }
}