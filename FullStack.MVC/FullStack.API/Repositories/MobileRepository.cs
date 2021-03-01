using FullStack.API.Data;
using FullStack.API.Model;
using FullStack.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Repositories
{
    public class MobileRepository : IMobileRepository
    {
        private readonly APIDbContext _dbContext;

        public MobileRepository(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Mobile> AddAsync(Mobile item)
        {
            var addedItem = _dbContext.Mobiles.Add(item);
            await _dbContext.SaveChangesAsync();
            
            addedItem.Reference(m => m.Brand).Load();

            return addedItem.Entity;
        }

        public List<Mobile> GetAll()
        {
            return _dbContext.Mobiles
                .Include(m => m.Brand)
                .ToList();
        }

        public async Task<Mobile> GetAsync(int id)
        {
            var mobile = await _dbContext.Mobiles.FirstOrDefaultAsync(brand => brand.Id == id);
            return mobile;
        }

        public void Update(Mobile item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
