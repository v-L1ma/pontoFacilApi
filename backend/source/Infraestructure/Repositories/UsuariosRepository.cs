using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using pontoFacilApi.source.Domain.Models;
using pontoFacilApi.source.Infraestructure.Data;

public class UsuariosRepository : IUsuariosRepository
{
    private readonly AppDbContext _context;
    public UsuariosRepository(AppDbContext context)
    {
        _context = context;
    }
    public void AlterarCargoUsuario()
    {
        throw new NotImplementedException();
    }

    public Usuario? BuscarPorId(string idUsuario)
    {
        string sql = "SELECT * FROM AspNetUsers WHERE Id = @idUsuario;";
        var usuarioBanco = _context.Users.FromSqlRaw(sql, new SqlParameter("idUsuario", idUsuario)).ToList().FirstOrDefault();
        return usuarioBanco;
    }

    public Usuario? BuscarPorEmail(string email)
    {
        string sql = "SELECT * FROM AspNetUsers WHERE NormalizedEmail = @email;";
        var usuarioBanco = _context.Users.FromSqlRaw(sql, new SqlParameter("email", email.ToUpper())).ToList().FirstOrDefault();
        return usuarioBanco;
    }
    public List<UsuarioDto> BuscarUsuariosPaginado(int pageSize, int pageNumber)
    {
        var usuarios = _context.Users
        .Include(u => u.Cargo)
        .OrderBy(u => u.Id)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nome = u.UserName,
            Email = u.Email,
            Cargo = u.Cargo != null ? u.Cargo.Nome : null
        })
        .ToList();

        return usuarios;
    }

    public async Task DesativarPerfil(string idUsuario)
    {
        string sql = @"DELETE FROM AspNetUsers WHERE Id=@id;";

        await _context.Database.ExecuteSqlRawAsync(sql, new SqlParameter("id", idUsuario));
    }

    public async Task EditarPerfil(string idUsuario, EditarUsuarioDTO dto)
    {
        string sql = "UPDATE AspNetUsers";

        string set = "";

        var parametros = new SqlParameter[] { new SqlParameter("id", idUsuario) };

        if (!string.IsNullOrEmpty(dto.Nome))
        {
            set += " SET UserName = @nome, NormalizedUserName = @normalizedUserName";
            parametros = parametros.Append(new SqlParameter("nome", dto.Nome)).ToArray();
            parametros = parametros.Append(new SqlParameter("normalizedUserName", dto.Nome.ToUpper())).ToArray();
        }

        if (!string.IsNullOrEmpty(dto.Email))
        {
            set += ", Email = @email, NormalizedEmail = @normalizedEmail";
            parametros = parametros.Append(new SqlParameter("email", dto.Email.ToLower())).ToArray();
            parametros = parametros.Append(new SqlParameter("normalizedEmail", dto.Email.ToUpper())).ToArray();
        }

        if (set == "")
        {
            throw new ApplicationException("Nenhum dado para ser atualizado.");
        }

        sql += set + " WHERE Id = @id";

        await _context.Database.ExecuteSqlRawAsync(sql, parametros);
    }
    
    public async Task AdminEditarPerfil(string idUsuario, AdminEditarUsuarioDTO dto)
    {
        string sql = "UPDATE AspNetUsers";

        string set = "";

        var parametros = new SqlParameter[] { new SqlParameter("id",idUsuario) };

        if (!string.IsNullOrEmpty(dto.Nome))
        {
            set += " SET UserName = @nome, NormalizedUserName = @normalizedUserName";
            parametros = parametros.Append(new SqlParameter("nome", dto.Nome)).ToArray();
            parametros = parametros.Append(new SqlParameter("normalizedUserName", dto.Nome.ToUpper())).ToArray();
        }

        if (!string.IsNullOrEmpty(dto.Email))
        {
            set += ", Email = @email, NormalizedEmail = @normalizedEmail";
            parametros = parametros.Append(new SqlParameter("email", dto.Email.ToLower())).ToArray();
            parametros = parametros.Append(new SqlParameter("normalizedEmail", dto.Email.ToUpper())).ToArray();
        }

        if (dto.CargoId>=0 && dto.CargoId!=null)
        {
            set += ", CargoId = @cargoId";
            parametros = parametros.Append(new SqlParameter("cargoId", dto.CargoId)).ToArray();
        }

        if (set == "")
        {
            throw new ApplicationException("Nenhum dado para ser atualizado.");
        }

        sql += set + " WHERE Id = @id";

        await _context.Database.ExecuteSqlRawAsync(sql, parametros);
    }
}