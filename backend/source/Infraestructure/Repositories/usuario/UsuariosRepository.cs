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

    public Usuario? BuscarPorId(string idUsuario)
    {
        string sql = "SELECT * FROM AspNetUsers WHERE Id = @idUsuario;";

        var usuarioBanco = _context.Database
                                    .SqlQueryRaw<Usuario>(sql, new SqlParameter("idUsuario", idUsuario))
                                    .AsEnumerable()
                                    .FirstOrDefault();
        return usuarioBanco;
    }

    public Usuario? BuscarPorEmail(string email)
    {
        string sql = "SELECT * FROM AspNetUsers WHERE NormalizedEmail = @email;";
        var usuarioBanco = _context.Database
                                    .SqlQueryRaw<Usuario>(sql, new SqlParameter("email", email.ToUpper()))
                                    .AsEnumerable()
                                    .FirstOrDefault();
        return usuarioBanco;
    }
    // public List<UsuarioDto> BuscarUsuariosPaginado(int pageSize, int pageNumber)
    // {
    //     string sql = @"
    //         SELECT Id, UserName AS Nome, Email
    //         FROM AspNetUsers
    //         ORDER BY Id ASC
    //         OFFSET ((@pageNumber - 1) * @pageSize) ROWS
    //         FETCH NEXT @pageSize ROWS ONLY;
    //     ";

    //     var parametros = new[]
    //     {
    //         new SqlParameter("pageNumber", pageNumber),
    //         new SqlParameter("pageSize", pageSize)
    //     };

    //     var usuarios = _context.Database
    //                         .SqlQueryRaw<UsuarioDto>(sql, parametros)
    //                         .ToList();
    //     return usuarios;
    // }

    public async Task DesativarPerfil(string id)
    {
        string sql = @"DELETE FROM AspNetUsers WHERE Id=@id;";

        await _context.Database.ExecuteSqlRawAsync(sql, new SqlParameter("id", id));
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

}