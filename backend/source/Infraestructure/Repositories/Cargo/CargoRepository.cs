
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pontoFacilApi.source.Infraestructure.Data;

public class CargoRepository : ICargoRespository
{
    private AppDbContext _context;
    public CargoRepository(
        AppDbContext context
    )
    {
        _context = context;
    }
    public CargoDto? BuscarPorId(int id)
    {
        var cargoBanco = _context.Cargos
                                .FirstOrDefault(s => s.Id == id);

        return cargoBanco != null ? new CargoDto
        {
            Id = cargoBanco.Id,
            Nome = cargoBanco.Nome
        } : null;
    }

    public CargoDto? BuscarPorNome(string nome)
    {
        var cargoBanco = _context.Cargos
                                .FirstOrDefault(c => c.Nome.ToUpper() == nome.ToUpper());

        return cargoBanco != null ? new CargoDto
        {
            Id = cargoBanco.Id,
            Nome = cargoBanco.Nome
        } : null;
    }

        public List<CargoDto> BuscarPorSetor(int idSetor)
    {
        var cargosBanco = _context.Cargos
                                .Where(c=>c.SetorId==idSetor)
                                .OrderBy(c => c.Nome)
                                .Select(c => new CargoDto {
                                    Id = c.Id,
                                    Nome = c.Nome,
                                    SetorId=c.SetorId
                                })
                                .ToList();

        return cargosBanco;
    }

    public async Task<PaginacaoDTO<CargoDto>> BuscarTodos(int pageSize, int pageNumber)
    {
        List<CargoDto> cargosBanco = await _context.Cargos
                                        .OrderBy(c => c.Nome)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .Select(s => new CargoDto
                                        {
                                            Id = s.Id,
                                            Nome = s.Nome,
                                            SetorId = s.SetorId
                                        })
                                        .ToListAsync();

        int total = _context.Cargos.ToList().Count();                  

        return new PaginacaoDTO<CargoDto>
        {
            Itens = cargosBanco,
            Total = total
        };
    }

    public async Task<CargoDto> Cadastrar(CadastrarCargoDto dto)
    {
        Cargo cargoNovo = new Cargo
        {
            Nome = dto.Nome,
            SetorId = dto.SetorId,
        };

        _context.Cargos.Add(cargoNovo);
        await _context.SaveChangesAsync();

        return new CargoDto
        {
            Id = cargoNovo.Id,
            Nome = cargoNovo.Nome,
            SetorId=cargoNovo.SetorId

        };
    }

    public async Task<CargoDto> Editar(int id, EditarCargoDTO dto)
    {
        Cargo? cargo = _context.Cargos.FirstOrDefault(c => c.Id == id);

        if (cargo == null)
        {
            throw new NaoEncontradoException("Cargo não encontrado.");
        }

        if (cargo.Nome == dto.Nome &&
            cargo.SetorId == dto.SetorId)
        {
            throw new ParametroInvalidoException("Nenhum campo precisa ser editado.");
        }

        if (cargo.Nome != dto.Nome)
        {
            cargo.Nome = dto.Nome;
        }

        if (cargo.SetorId != dto.SetorId)
        {
            cargo.SetorId = dto.SetorId;
        }

        _context.Cargos.Update(cargo);
        await _context.SaveChangesAsync();

        return new CargoDto
        {
            Id = cargo.Id,
            Nome=cargo.Nome,
            SetorId=cargo.SetorId
        };
    }

    public async Task Excluir(int id)
    {
        Cargo? cargo = _context.Cargos.FirstOrDefault(s => s.Id == id);

        if (cargo == null)
        {
            throw new NaoEncontradoException("Setor não encontrado.");
        }

        _context.Cargos.Remove(cargo);
        await _context.SaveChangesAsync();
    }
}