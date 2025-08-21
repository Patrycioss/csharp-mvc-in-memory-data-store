using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Data;

public class DataContext : DbContext
{
    public DbSet<Product> Products { get; set; }    
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
}