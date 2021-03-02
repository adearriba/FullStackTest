using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.MVC.Services.Interfaces
{
    public interface ICRUDBaseService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T item);
        Task UpdateAsync(T item);
        Task RemoveAsync(int id);
    }
}
