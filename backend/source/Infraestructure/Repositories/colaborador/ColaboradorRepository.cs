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

    public Colaborador? BuscarPorId(string id)
    {
        string sql = "SELECT * FROM Colaboradores WHERE Id = @id;";
        var usuarioBanco = _context.Database.SqlQueryRaw<Colaborador>(sql, new SqlParameter("idUsuario", id)).ToList().FirstOrDefault();
        return usuarioBanco;
    }

    public List<Colaborador> BuscarUsuariosPaginado(int pageSize, int pageNumber)
    {
        string sql = @"SELECT 
                            * 
                        FROM Colaboradores 
                        ORDER BY Id ASC OFFSET ((@pageNumber - 1) * @pageSize) 
                        ROWS FETCH NEXT @pageSize 
                        ROWS ONLY;";

        var parametros = new SqlParameter[]{
            new SqlParameter("pageNumber",pageNumber),
            new SqlParameter("pageSize",pageSize),
        };

        var usuariosBanco = _context.Database.SqlQueryRaw<List<Colaborador>>(sql, parametros).ToList().FirstOrDefault();
        return usuariosBanco != null ? usuariosBanco : [];
    }

    public void CadastrarColaborador(CadastrarColaboradorDto dto)
    {
        string sql = @"INSERT INTO Colaboradores (Nome, CargoId)
                        VALUES (@nome, @cargoId);";

        var parametros = new SqlParameter[]{
            new SqlParameter("nome",dto.Nome),
            new SqlParameter("cargoId",dto.CargoId),
        };

        _context.Colaboradores.FromSqlRaw(sql, parametros);
        _context.SaveChanges();
    }

    public async Task DesativarPerfil(string id)
    {
        string sql = @"DELETE FROM Colaboradores WHERE Id=@id;";

        await _context.Database.ExecuteSqlRawAsync(sql, new SqlParameter("id", id));
        _context.SaveChanges();
    }

    public async Task EditarColaborador(string id, EditarColaboradorDTO dto)
    {
        string sql = "UPDATE Colaboradores";

        string set = "";

        var parametros = new SqlParameter[] { new SqlParameter("id", id) };

        if (!string.IsNullOrEmpty(dto.Nome))
        {
            set += " SET Nome = @nome";
            parametros = parametros.Append(new SqlParameter("nome", dto.Nome)).ToArray();
        }

        if (!string.IsNullOrEmpty(dto.CargoId))
        {
            set += ", CargoId = @cargoId";
            parametros = parametros.Append(new SqlParameter("cargoId", dto.CargoId)).ToArray();
        }

        if (set == "")
        {
            throw new ApplicationException("Nenhum dado para ser atualizado.");
        }

        sql += set + " WHERE Id = @id;";

        await _context.Database.ExecuteSqlRawAsync(sql, parametros);
        _context.SaveChanges();
    }

    
}