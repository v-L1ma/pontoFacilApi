using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pontoFacilApi.source.Domain.Models;

namespace pontoFacilApi.source.Infraestructure.Data;
public class UsuarioDbContext : IdentityDbContext<Usuario>
{
    public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options)
    {
        
    }
}