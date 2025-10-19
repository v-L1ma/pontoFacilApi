using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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
    public Colaborador? BuscarPorEmail(string email)
    {
        throw new NotImplementedException();
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
            Cargo = usuarioBanco.Cargo.Nome,
            Setor = usuarioBanco.Cargo.Setor.Nome
        } : null;
    }

    public async Task<List<ColaboradorDto>> BuscarColaboradoresPaginado(int pageSize, int pageNumber)
    {
        var colaboradoresBanco = await _context.Colaboradores
                        .Include(colaborador => colaborador.Cargo)
                        .ThenInclude(cargo => cargo.Setor)
                        .OrderBy(c => c.Id)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

        return colaboradoresBanco.Select(c => new ColaboradorDto
        {
            Id = c.Id,
            Nome = c.Nome,
            Cargo = c.Cargo.Nome,
            Setor = c.Cargo.Setor.Nome
        }).ToList();
    }

    public async Task<ColaboradorDto> CadastrarColaborador(CadastrarColaboradorDto dto)
    {
        Colaborador novo = new Colaborador
        {
            Nome = dto.Nome,
            CargoId = dto.CargoId
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

        if (colaborador == null)
        {
            throw new ApplicationException("Nenhum colaborador com esse id foi encontrado!");
        }

        if (string.IsNullOrEmpty(dto.Nome))
        {
            colaborador!.Nome = dto.Nome;
        }

        if (dto.CargoId > 0 && dto.CargoId<28)
        {
            colaborador!.CargoId = dto.CargoId;
        }

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
            Cargo = colaboradorAtualizado.Cargo.Nome,
            Setor = colaboradorAtualizado.Cargo.Setor.Nome
        };
    }

}