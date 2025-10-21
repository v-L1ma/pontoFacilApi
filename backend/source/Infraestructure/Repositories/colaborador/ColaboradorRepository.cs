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
                        .OrderBy(c => c.Id)
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
                        .ToList()
                        .FirstOrDefault(c => c.Id == id);
        if (colaboradorBanco != null)
        {
            _context.Remove(colaboradorBanco);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<ColaboradorDto> EditarColaborador(string id ,EditarColaboradorDTO dto)
    {
        Colaborador? colaborador = await _context.Colaboradores.FirstOrDefaultAsync(c => c.Id == id);

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

}