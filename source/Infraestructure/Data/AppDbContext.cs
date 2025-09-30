using Microsoft.EntityFrameworkCore;

namespace pontoFacilApi.source.Infraestructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Setor> Setores { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Setores
        modelBuilder.Entity<Setor>().HasData(
            new Setor { Id = 1, Nome = "Administrativo" },
            new Setor { Id = 2, Nome = "Financeiro" },
            new Setor { Id = 3, Nome = "Recursos Humanos" },
            new Setor { Id = 4, Nome = "Comercial" },
            new Setor { Id = 5, Nome = "Tecnologia da Informação" },
            new Setor { Id = 6, Nome = "Logística" },
            new Setor { Id = 7, Nome = "Jurídico" },
            new Setor { Id = 8, Nome = "Marketing" },
            new Setor { Id = 9, Nome = "Produção" },
            new Setor { Id = 10, Nome = "Atendimento ao Cliente" }
        );

        // Cargos
        modelBuilder.Entity<Cargo>().HasData(
            new Cargo { Id = 1, Nome = "Estagiário", SetorId = 1 },
            new Cargo { Id = 2, Nome = "Assistente Administrativo", SetorId = 1 },
            new Cargo { Id = 3, Nome = "Analista Administrativo", SetorId = 1 },
            new Cargo { Id = 4, Nome = "Coordenador Administrativo", SetorId = 1 },

            new Cargo { Id = 5, Nome = "Assistente Financeiro", SetorId = 2 },
            new Cargo { Id = 6, Nome = "Analista Financeiro", SetorId = 2 },
            new Cargo { Id = 7, Nome = "Gerente Financeiro", SetorId = 2 },

            new Cargo { Id = 8, Nome = "Analista de RH", SetorId = 3 },
            new Cargo { Id = 9, Nome = "Coordenador de RH", SetorId = 3 },
            new Cargo { Id = 10, Nome = "Recrutador", SetorId = 3 },

            new Cargo { Id = 11, Nome = "Vendedor", SetorId = 4 },
            new Cargo { Id = 12, Nome = "Representante Comercial", SetorId = 4 },
            new Cargo { Id = 13, Nome = "Gerente Comercial", SetorId = 4 },

            new Cargo { Id = 14, Nome = "Desenvolvedor", SetorId = 5 },
            new Cargo { Id = 15, Nome = "Analista de Sistemas", SetorId = 5 },
            new Cargo { Id = 16, Nome = "Administrador de Redes", SetorId = 5 },
            new Cargo { Id = 17, Nome = "Coordenador de TI", SetorId = 5 },

            new Cargo { Id = 18, Nome = "Auxiliar de Logística", SetorId = 6 },
            new Cargo { Id = 19, Nome = "Supervisor de Logística", SetorId = 6 },

            new Cargo { Id = 20, Nome = "Advogado", SetorId = 7 },
            new Cargo { Id = 21, Nome = "Assistente Jurídico", SetorId = 7 },

            new Cargo { Id = 22, Nome = "Analista de Marketing", SetorId = 8 },
            new Cargo { Id = 23, Nome = "Designer Gráfico", SetorId = 8 },
            new Cargo { Id = 24, Nome = "Social Media", SetorId = 8 },

            new Cargo { Id = 25, Nome = "Operador de Máquina", SetorId = 9 },
            new Cargo { Id = 26, Nome = "Supervisor de Produção", SetorId = 9 },

            new Cargo { Id = 27, Nome = "Atendente", SetorId = 10 },
            new Cargo { Id = 28, Nome = "Supervisor de Atendimento", SetorId = 10 }
        );
    }

}