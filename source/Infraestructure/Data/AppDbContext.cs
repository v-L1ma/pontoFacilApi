using Microsoft.EntityFrameworkCore;

namespace pontoFacilApi.source.Infraestructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    

}