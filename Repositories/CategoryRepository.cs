using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories
{
    public class CategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> Get()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }
        public async Task<Category> Get(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<List<Product>> GetProducts(int id)
        {
            return await _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
        }
        public async Task<int> Save(Category category)
        {
            _context.Categories.Add(category);
            return await _context.SaveChangesAsync();
        }
        public async Task<Category> Update(Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<int> Exclude(Category category)
        {
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }
    }
}