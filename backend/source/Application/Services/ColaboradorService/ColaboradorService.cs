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
    public ResponseBase<List<Colaborador>> BuscarColaboradoresPaginado(int pageSize, int pageNumber)
    {
        if (pageSize < 0 || pageNumber < 0)
        {
            throw new ApplicationException("O tamanho e o numero da pagina devem ser maior que zero!");
        }
        List<Colaborador> colaboradores = _colaboradorRepository.BuscarUsuariosPaginado(pageSize, pageNumber);

        return new ResponseBase<List<Colaborador>>
        {
            Dados = colaboradores,
            Message = "Colaboradores listados com sucesso!"
        };
    }

    public ResponseBase<Colaborador> BuscarColaboradorPorId(string id)
    {
         if (string.IsNullOrEmpty(id))
        {
            throw new ApplicationException("O id não pode ser vazio.");
        }

        Colaborador? colaborador = _colaboradorRepository.BuscarPorId(id);

        if (colaborador is null)
        {
            throw new ApplicationException("Nenhum colaborador encontrado");
        }

        return new ResponseBase<Colaborador>
        {
            Dados = colaborador,
            Message = "Informações do colaborador encontradas com sucesso!"
        };
    }

    public ResponseBase<string> CadastrarColaborador(CadastrarColaboradorDto dto)
    {
        if (dto.CargoId == 0 || string.IsNullOrEmpty(dto.Nome))
        {
            throw new ApplicationException("Dados de cadastro inválidos!");
        }

        if (dto.Nome.Length<5)
        {
            throw new ApplicationException("O nome do colaborador deve ter mais de 5 caracteres.");
        }
        
        _colaboradorRepository.CadastrarColaborador(dto);

        return new ResponseBase<string>()
        { 
            Message = "Colaborador cadastrado com sucesso!"
        };
    }

    public async Task<ResponseBase<Colaborador>> EditarColaborador(string id, EditarColaboradorDTO dto)
    {
        Colaborador? colaboradorBanco = _colaboradorRepository.BuscarPorId(id);

        if (colaboradorBanco is null)
        {
            throw new ApplicationException("Colaborador não cadastrado");
        }

        await _colaboradorRepository.EditarColaborador(id, dto);

        colaboradorBanco = _colaboradorRepository.BuscarPorId(id);

        return new ResponseBase<Colaborador>()
        {
            Message = "Permissões alteradas com sucesso.",
            Dados = colaboradorBanco
        };
    }

    public ResponseBase<string> ExcluirColaborador(string id)
    {
        Colaborador? colaboradorBanco = _colaboradorRepository.BuscarPorId(id);

        if (colaboradorBanco is null)
        {
            throw new ApplicationException("Colaborador não cadastrado");
        }

        _colaboradorRepository.DesativarPerfil(id);

        return new ResponseBase<string>()
        {
            Message = "Colaborador excluido com sucesso."
        };
    }
}