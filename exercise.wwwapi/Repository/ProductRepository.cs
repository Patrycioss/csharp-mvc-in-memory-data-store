using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _db;
    private readonly DbSet<Product> _table;

    public ProductRepository(DataContext db)
    {
        _db = db;
        _table = _db.Set<Product>();
    }

    public async Task<Product> Create(Product product)
    {
        await _table.AddAsync(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetAll(string? category)
    {
        if (category == null)
        {
            return await _table.ToListAsync();
        }

        return await _table.Where(p => p.Category == category).ToListAsync();
    }

    public async Task<Product?> Get(int id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<Product> Update(Product product)
    {
        _table.Attach(product);
        _db.Entry(product).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> Delete(int id)
    {
        var product = await _table.FindAsync(id);

        if (product != null)
        {
            _table.Remove(product);
            await _db.SaveChangesAsync();
        }

        return product;
    }
}