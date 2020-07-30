using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Repositories
{
    public class ProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> Get()
        {
            return await _context
                .Products
                .Include(x => x.Category)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Category = x.Category.Title,
                    CategoryId = x.Category.Id
                })
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Product> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<int> Save(Product product)
        {
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<Product> Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<int> Delete(Product product)
        {
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }
    }
}