using System.Text.RegularExpressions;
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
    public async Task<ResponseBase<PaginacaoDTO<ColaboradorDto>>> BuscarColaboradoresPaginado(int pageSize, int pageNumber)
    {
        if (pageSize < 0 || pageNumber < 0)
        {
            throw new ParametroInvalidoException("O tamanho e o numero da pagina devem ser maior que zero!");
        }

        PaginacaoDTO<ColaboradorDto> paginacao = await _colaboradorRepository.BuscarColaboradoresPaginado(pageSize, pageNumber);

        return new ResponseBase<PaginacaoDTO<ColaboradorDto>>
        {
            Dados = paginacao,
            Message = "Colaboradores listados com sucesso!"
        };
    }

    public ResponseBase<ColaboradorDto> BuscarColaboradorPorId(string id)
    {
         if (string.IsNullOrEmpty(id))
        {
            throw new ParametroInvalidoException("O id não pode ser vazio.");
        }

        ColaboradorDto? colaborador = _colaboradorRepository.BuscarPorId(id);

        if (colaborador is null)
        {
            throw new NaoEncontradoException("Nenhum colaborador encontrado");
        }

        return new ResponseBase<ColaboradorDto>
        {
            Dados = colaborador,
            Message = "Informações do colaborador encontradas com sucesso!"
        };
    }

    public async Task<ResponseBase<ColaboradorDto>> CadastrarColaborador(CadastrarColaboradorDto dto)
    {
        string nome = dto.Nome.TrimStart();
        nome = nome.TrimEnd();

        nome = Regex.Replace(nome, @"[^\p{L}\s']", "");
        
        if (string.IsNullOrEmpty(nome))
        {
            throw new ParametroInvalidoException("Insira um nome válido!");
        }

        if (dto.CargoId < 1)
        {
            throw new ParametroInvalidoException("Insira um cargo válido!");
        }

        if (nome.Length<3)
        {
            throw new ParametroInvalidoException("O nome do colaborador deve ter mais de 5 caracteres.");
        }

        if (string.IsNullOrEmpty(dto.CPF) || !isCPFValido(dto.CPF))
        {
            throw new ParametroInvalidoException("CPF inválido");
        }

        ColaboradorDto? colaborador = _colaboradorRepository.BuscarPorCPF(dto.CPF);

        if (colaborador!=null)
        {
            throw new EmUsoException("Este CPF já pertence a outro colaborador.");
        }
        
        var colaboradorNovo = await _colaboradorRepository.CadastrarColaborador(new CadastrarColaboradorDto
        {
            Nome = nome,
            CargoId = dto.CargoId,
            CPF = dto.CPF
        });

        return new ResponseBase<ColaboradorDto>()
        {
            Message = "Colaborador cadastrado com sucesso!",
            Dados = colaboradorNovo
        };
    }

    public async Task<ResponseBase<ColaboradorDto>> EditarColaborador(string id, EditarColaboradorDTO dto)
    {
        string nome = dto.Nome.Trim();

        nome = Regex.Replace(nome, @"[^\p{L}\s]", "");

        if (string.IsNullOrEmpty(nome))
        {
            throw new ParametroInvalidoException("Insira um nome válido!");
        }

        if (dto.CargoId < 1)
        {
            throw new ParametroInvalidoException("Insira um cargo válido!");
        }

        if (nome.Length < 3)
        {
            throw new ParametroInvalidoException("O nome do colaborador deve ter mais de 5 caracteres.");
        }
        
        if (!isCPFValido(dto.CPF))
        {
            throw new ParametroInvalidoException("CPF inválido");
        }
        
        ColaboradorDto? colaboradorBanco = _colaboradorRepository.BuscarPorId(id);

        if (colaboradorBanco is null)
        {
            throw new NaoEncontradoException("Colaborador não cadastrado");
        }

        if (dto.CPF != colaboradorBanco.CPF && _colaboradorRepository.BuscarPorCPF(dto.CPF) != null)
        {
            throw new EmUsoException("Este CPF já pertence a outro colaborador.");
        }

        await _colaboradorRepository.EditarColaborador(id, new EditarColaboradorDTO{
            Nome = nome,
            CPF = dto.CPF,
            CargoId = dto.CargoId
        });

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
            throw new NaoEncontradoException("Colaborador não cadastrado");
        }

        _colaboradorRepository.ExcluirColaborador(id);

        return new ResponseBase<string>()
        {
            Message = "Colaborador excluido com sucesso."
        };
    }

    public async Task<ResponseBase<EstatisticasColaboradoresDto>> EstatisticasColaboradores()
    {

        EstatisticasColaboradoresDto response = await _colaboradorRepository.EstatisticasColaboradores();

        return new ResponseBase<EstatisticasColaboradoresDto>
        {
            Dados=response,
            Message="Estatisticas listadas com sucesso"
        };
    }
    private bool isCPFValido(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        cpf = cpf.Trim();

        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCpf = tempCpf + digito;

        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }
}