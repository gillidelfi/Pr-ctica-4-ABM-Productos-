using Microsoft.EntityFrameworkCore;
using Practica_CSR.Entities;

namespace Practica_CSR.Data;

public class PracticaCSRContext : DbContext
{
    public PracticaCSRContext(
        DbContextOptions<PracticaCSRContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
}