﻿using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
	{
        private readonly ApplicationDbContext context;
		public ProductRepository(ApplicationDbContext context)
		{
			this.context = context;
		}

		public async Task<Product> GetProductByIdAsync(Guid id)
		{
            return await context.Products.FindAsync(id);
        }

		public async Task<IEnumerable<Product>> GetProductsAsync()
		{
            return await context.Products.ToListAsync();
        }

		public async Task<Guid> AddProductAsync(Product product)
		{
			await context.Products.AddAsync(product);
			await context.SaveChangesAsync();
			return product.Id;
		}

		public async Task UpdateProductAsync(Product product)
		{
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

		public async Task DeleteProductAsync(Guid id)
		{
			await context.Products.FindAsync(id);
			context.Products.Remove(await context.Products.FindAsync(id));
            await context.SaveChangesAsync();

        }
	}
}
