using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStack.API.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();

        Task<T> GetAsync(int id);

        Task<T> AddAsync(T item);

        void Update(T item);

        void Remove(T item);
    }
}
