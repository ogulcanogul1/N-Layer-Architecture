    using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Products
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Product>> TopSellingProducts(int count)
        {
            return _context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
        }
    }
}
