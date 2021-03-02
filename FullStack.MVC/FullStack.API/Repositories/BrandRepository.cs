using FullStack.API.Data;
using FullStack.API.Repositories.Interfaces;
using FullStack.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly APIDbContext _dbContext;

        public BrandRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Brand> AddAsync(Brand item)
        {
            var addedItem = _dbContext.Brands.Add(item);
            await _dbContext.SaveChangesAsync();
            return addedItem.Entity;
        }

        public List<Brand> GetAll()
        {
            return _dbContext.Brands.ToList<Brand>();
        }

        public async Task<Brand> GetAsync(int id)
        {
            var brand = await _dbContext.Brands
                .Include(b => b.Mobiles)
                .FirstOrDefaultAsync(brand => brand.Id == id);

            return brand;
        }

        public void Remove(Brand item)
        {
            _dbContext.Brands.Remove(item);
            _dbContext.SaveChanges();
        }

        public void Update(Brand item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
