using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository;

public interface IProductRepository
{
    public Task<Product> Create(Product product);
    public Task<IEnumerable<Product>> GetAll(string? category = null);
    public Task<Product?> Get(int id);
    public Task<Product> Update(Product product);
    public Task<Product?> Delete(int id);
}