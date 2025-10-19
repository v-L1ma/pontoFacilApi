using System.Threading.Tasks;
using pontoFacilApi.source.Domain.Models;

public class ColaboradorService : IColaboradorService
{
    private readonly IColaboradorRepository _colaboradorRepository;
    public ColaboradorService(
        IColaboradorRepository colaboradorRepository
    )
    {
        _colaboradorRepository = colaboradorRepository;
    }
    public async Task<ResponseBase<List<ColaboradorDto>>> BuscarColaboradoresPaginado(int pageSize, int pageNumber)
    {
        if (pageSize < 0 || pageNumber < 0)
        {
            throw new ApplicationException("O tamanho e o numero da pagina devem ser maior que zero!");
        }

        List<ColaboradorDto> colaboradores = await _colaboradorRepository.BuscarColaboradoresPaginado(pageSize, pageNumber);

        return new ResponseBase<List<ColaboradorDto>>
        {
            Dados = colaboradores,
            Message = "Colaboradores listados com sucesso!"
        };
    }

    public ResponseBase<ColaboradorDto> BuscarColaboradorPorId(string id)
    {
         if (string.IsNullOrEmpty(id))
        {
            throw new ApplicationException("O id não pode ser vazio.");
        }

        ColaboradorDto? colaborador = _colaboradorRepository.BuscarPorId(id);

        if (colaborador is null)
        {
            throw new ApplicationException("Nenhum colaborador encontrado");
        }

        return new ResponseBase<ColaboradorDto>
        {
            Dados = colaborador,
            Message = "Informações do colaborador encontradas com sucesso!"
        };
    }

    public async Task<ResponseBase<ColaboradorDto>> CadastrarColaborador(CadastrarColaboradorDto dto)
    {
        if (string.IsNullOrEmpty(dto.Nome))
        {
            throw new ApplicationException("Insira um nome válido!");
        }

        if (dto.CargoId < 1 || dto.CargoId > 28)
        {
            throw new ApplicationException("Insira um cargo válido!");
        }

        if (dto.Nome.Length<5)
        {
            throw new ApplicationException("O nome do colaborador deve ter mais de 5 caracteres.");
        }
        
        var colaboradorNovo = await _colaboradorRepository.CadastrarColaborador(dto);

        return new ResponseBase<ColaboradorDto>()
        {
            Message = "Colaborador cadastrado com sucesso!",
            Dados = colaboradorNovo
        };
    }

    public async Task<ResponseBase<ColaboradorDto>> EditarColaborador(string id, EditarColaboradorDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Nome))
        {
            throw new ApplicationException("Insira um nome válido!");
        }

        if (dto.CargoId < 1 || dto.CargoId > 28)
        {
            throw new ApplicationException("Insira um cargo válido!");
        }

        if (dto.Nome.Length<5)
        {
            throw new ApplicationException("O nome do colaborador deve ter mais de 5 caracteres.");
        }
        
        ColaboradorDto? colaboradorBanco = _colaboradorRepository.BuscarPorId(id);

        if (colaboradorBanco is null)
        {
            throw new ApplicationException("Colaborador não cadastrado");
        }

        await _colaboradorRepository.EditarColaborador(id, dto);

        colaboradorBanco = _colaboradorRepository.BuscarPorId(id);

        return new ResponseBase<ColaboradorDto>()
        {
            Message = "Informacoes do colaborador alteradas com sucesso.",
            Dados = colaboradorBanco
        };
    }

    public ResponseBase<string> ExcluirColaborador(string id)
    {
        ColaboradorDto? colaboradorBanco = _colaboradorRepository.BuscarPorId(id);

        if (colaboradorBanco is null)
        {
            throw new ApplicationException("Colaborador não cadastrado");
        }

        _colaboradorRepository.ExcluirColaborador(id);

        return new ResponseBase<string>()
        {
            Message = "Colaborador excluido com sucesso."
        };
    }
}