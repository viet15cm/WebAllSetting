using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.Models;

namespace WebProject.Services
{
    public class ProductServices
    {
        private readonly AppDbContext context;
        public ProductServices(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task<ICollection<Product>> GetAll()
        {
            return await context.products.ToListAsync();
        }
    }
}
