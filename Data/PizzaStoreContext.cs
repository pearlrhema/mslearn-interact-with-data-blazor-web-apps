// This class creates a database context we can use to register a database service.
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza.Data;

public class PizzaStoreContext : DbContext
{
    public PizzaStoreContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PizzaSpecial> Specials { get; set; }
}