
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pontoFacilApi.source.Infraestructure.Data;

public class SetorRepository : ISetorRepository
{
    private AppDbContext _context;
    public SetorRepository(
        AppDbContext context
    )
    {
        _context = context;
    }
    public SetorDto? BuscarPorId(int id)
    {
        var setorBanco = _context.Setores
                                .FirstOrDefault(s => s.Id == id);

        return setorBanco != null ? new SetorDto
        {
            Id = setorBanco.Id,
            Nome = setorBanco.Nome
        } : null;
    }

    public SetorDto? BuscarPorNome(string nome)
    {
        var setorBanco = _context.Setores
                                .FirstOrDefault(s => s.Nome == nome);

        return setorBanco != null ? new SetorDto
        {
            Id = setorBanco.Id,
            Nome = setorBanco.Nome
        } : null;
    }

    public async Task<PaginacaoDTO<SetorDto>> BuscarTodos(int pageSize, int pageNumber)
    {
        List<SetorDto> setoresBanco = await _context.Setores
                                        .OrderBy(s => s.Nome)
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .Select(s => new SetorDto { Id = s.Id, Nome = s.Nome })
                                        .ToListAsync();
        
        int total = _context.Setores.ToList().Count();

        return new PaginacaoDTO<SetorDto>
        {
            Itens = setoresBanco,
            Total = total
        };
    }

    public async Task<SetorDto> Cadastrar(CadastrarSetorDto dto)
    {
        Setor setorNovo = new Setor
        {
            Nome = dto.Nome
        };

        _context.Setores.Add(setorNovo);
        await _context.SaveChangesAsync();

        return new SetorDto
        {
            Id = setorNovo.Id,
            Nome = setorNovo.Nome
        };
    }

    public async Task<SetorDto> Editar(int id, EditarSetorDTO dto)
    {
        Setor? setor = _context.Setores.FirstOrDefault(s => s.Id == id);

        if (setor == null)
        {
            throw new NaoEncontradoException("Setor não encontrado.");
        }

        setor.Nome = dto.Nome;

        _context.Setores.Update(setor);
        await _context.SaveChangesAsync();

        return new SetorDto
        {
            Id = setor.Id,
            Nome=setor.Nome
        };
    }

    public async Task Excluir(int id)
    {
        Setor? setor = _context.Setores.FirstOrDefault(s => s.Id == id);

        if (setor == null)
        {
            throw new NaoEncontradoException("Setor não encontrado.");
        }

        _context.Setores.Remove(setor);
        await _context.SaveChangesAsync();
    }
}