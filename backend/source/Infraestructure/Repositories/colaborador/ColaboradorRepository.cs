using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pontoFacilApi.source.Domain.Models;
using pontoFacilApi.source.Infraestructure.Data;

public class ColaboradorRepository : IColaboradorRepository
{
    private readonly AppDbContext _context;
    public ColaboradorRepository(
        AppDbContext context
    )
    {
        _context = context;
    }
    public ColaboradorDto? BuscarPorCPF(string cpf)
    {
        var usuarioBanco = _context.Colaboradores
                                    .Include(c => c.Cargo)
                                    .ThenInclude(c => c.Setor)
                                    .Where(c=> c.Status==StatusEnum.ATIVO.ToString())
                                    .ToList()
                                    .FirstOrDefault(c => c.CPF == cpf);

        return usuarioBanco != null ? new ColaboradorDto
        {
            Id = usuarioBanco.Id,
            Nome = usuarioBanco.Nome,
            CPF = usuarioBanco.CPF,
            Cargo = usuarioBanco.Cargo.Nome,
            Setor = usuarioBanco.Cargo.Setor.Nome
        } : null;
    }

    public ColaboradorDto? BuscarPorId(string id)
    {
        var usuarioBanco = _context.Colaboradores
                                    .Include(c => c.Cargo)
                                    .ThenInclude(c => c.Setor)
                                    .Where(c=> c.Status==StatusEnum.ATIVO.ToString())
                                    .ToList()
                                    .FirstOrDefault(c => c.Id == id);

        return usuarioBanco != null ? new ColaboradorDto
        {
            Id = usuarioBanco.Id,
            Nome = usuarioBanco.Nome,
            CPF = usuarioBanco.CPF,
            Cargo = usuarioBanco.Cargo.Nome,
            Setor = usuarioBanco.Cargo.Setor.Nome
        } : null;
    }

    public async Task<PaginacaoDTO<ColaboradorDto>> BuscarColaboradoresPaginado(int pageSize, int pageNumber)
    {
        var colaboradoresBanco = await _context.Colaboradores
                        .Include(colaborador => colaborador.Cargo)
                        .ThenInclude(cargo => cargo.Setor)
                        .OrderBy(c => c.dataCriacao)
                        .Where(c=> c.Status==StatusEnum.ATIVO.ToString())
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

        List<ColaboradorDto> colaboradores = colaboradoresBanco.Select(c => new ColaboradorDto
        {
            Id = c.Id,
            Nome = c.Nome,
            CPF = c.CPF,
            Cargo = c.Cargo.Nome,
            Setor = c.Cargo.Setor.Nome
        }).ToList();

        int total = _context.Colaboradores.ToList().Count();

        return new PaginacaoDTO<ColaboradorDto>
        {
            Itens = colaboradores,
            Total = total
        };

    }

    public async Task<ColaboradorDto> CadastrarColaborador(CadastrarColaboradorDto dto)
    {
        Colaborador novo = new Colaborador
        {
            Nome = dto.Nome,
            CargoId = dto.CargoId,
            CPF =  dto.CPF
        };

        _context.Colaboradores.Add(novo);
        await _context.SaveChangesAsync();

        var colaboradorBanco = await _context.Colaboradores
            .Include(c => c.Cargo)
            .ThenInclude(cargo => cargo.Setor)
            .FirstOrDefaultAsync(c => c.Id == novo.Id);

        if (colaboradorBanco == null)
            throw new ApplicationException("Erro ao buscar colaborador cadastrado.");

        return new ColaboradorDto
        {
            Id = colaboradorBanco.Id,
            Nome = colaboradorBanco.Nome,
            CPF = colaboradorBanco.CPF,
            Cargo = colaboradorBanco.Cargo.Nome,
            Setor = colaboradorBanco.Cargo.Setor.Nome
        };
        
    }

    public async Task ExcluirColaborador(string id)
    {
        var colaboradorBanco = _context.Colaboradores
                        .Include(c => c.Cargo)
                        .ThenInclude(c => c.Setor)
                        .Where(c=> c.Status==StatusEnum.ATIVO.ToString())
                        .ToList()
                        .FirstOrDefault(c => c.Id == id);

        if (colaboradorBanco != null)
        {
            colaboradorBanco.Status = StatusEnum.INATIVO.ToString();
            _context.Update(colaboradorBanco);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<ColaboradorDto> EditarColaborador(string id, EditarColaboradorDTO dto)
    {
        Colaborador? colaborador = await _context.Colaboradores.Where(c=> c.Status==StatusEnum.ATIVO.ToString()).FirstOrDefaultAsync(c => c.Id == id);

        if (
            dto.Nome == colaborador!.Nome &&
            dto.CPF == colaborador!.CPF &&
            dto.CargoId == colaborador!.CargoId
        )
        {
            throw new ParametroInvalidoException("Nenhum dado precisa ser atualizado.");
        }

        colaborador!.Nome = dto.Nome;
        colaborador!.CargoId = dto.CargoId;
        colaborador.CPF = dto.CPF;

        _context.Update(colaborador);
        await _context.SaveChangesAsync();

        Colaborador? colaboradorAtualizado = await _context.Colaboradores
                                                    .Include(c => c.Cargo)
                                                    .ThenInclude(cargo => cargo.Setor)
                                                    .FirstOrDefaultAsync(c => c.Id == colaborador.Id);
        return new ColaboradorDto
        {
            Id = colaboradorAtualizado.Id,
            Nome = colaboradorAtualizado.Nome,
            CPF = colaboradorAtualizado.CPF,
            Cargo = colaboradorAtualizado.Cargo.Nome,
            Setor = colaboradorAtualizado.Cargo.Setor.Nome
        };
    }
    
    public async Task<EstatisticasColaboradoresDto> EstatisticasColaboradores()
{
    var colaboradoresBanco = await _context.Colaboradores
        .Include(c => c.Cargo)
        .ThenInclude(c => c.Setor)
        .ToListAsync();

    int colaboradoresAtivos = colaboradoresBanco.Where(c=> c.Status==StatusEnum.ATIVO.ToString()).Count();
    int colaboradoresDemitidos = colaboradoresBanco.Where(c=> c.Status==StatusEnum.INATIVO.ToString()).Count();
    int novosColaboradoresMes = colaboradoresBanco.Where(c => c.dataCriacao.Month == DateTime.UtcNow.Month).Count();
    

    var mesesEAnos = colaboradoresBanco
        .Select(c => new { c.dataCriacao.Year, c.dataCriacao.Month })
        .Distinct()
        .OrderBy(c => c.Year)
        .ThenBy(c => c.Month)
        .Select(c => $"{c.Month:D2}/{c.Year}")
        .ToArray();

    var totalColaboradoresPorMesEAno = colaboradoresBanco
        .GroupBy(c => new { c.dataCriacao.Year, c.dataCriacao.Month }) 
        .OrderBy(g => g.Key.Year)
        .ThenBy(g => g.Key.Month)
        .Select(g => g.Count()) 
        .ToArray();

    string?[] departamentos = colaboradoresBanco
        .Select(c => c.Cargo?.Setor?.Nome)
        .Where(nome => !string.IsNullOrEmpty(nome))
        .Distinct()
        .ToArray();

        var totalColaboradoresPorSetor = colaboradoresBanco
            .Where(c => c.Status == StatusEnum.ATIVO.ToString())
            .Select(c => c.Cargo?.Setor?.Nome)
            .Where(nome => !string.IsNullOrEmpty(nome))
            .GroupBy(nome => nome)
            .OrderBy(g => g.Key)
            .Select(g => g.Count())
            .ToArray();

        if (departamentos==null)
        {
            throw new EmUsoException("O DEPARTAMENTO TA NULL");
        }

    return new EstatisticasColaboradoresDto
    {
        ColaboradorDepartamento =
        new DadosGraficoDto {
            Labels=departamentos,
            Dataset = new DataSet
            {
                Label= "Departamentos",
                Data= totalColaboradoresPorSetor
            }
        },
        ColaboradoresAtivos = colaboradoresAtivos,
        ColaboradoresDemitidos = colaboradoresDemitidos,
        NovosColaboradoresMes = novosColaboradoresMes,
        ColaboradoresTotalTempo = new DadosGraficoDto
        {
            Labels=mesesEAnos,
            Dataset = new DataSet
            {
                Label= "Colaboradores",
                Data= totalColaboradoresPorMesEAno 
            }
        },
    };
}

}