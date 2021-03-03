using System.Threading.Tasks;

namespace FullStack.API.Services.Interfaces
{
    public interface ICacheService
    {
        Task<string> GetValueAsync(string key);
        Task SetValueAsync(string key, string value);
    }
}
