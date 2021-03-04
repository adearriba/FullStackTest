using StackExchange.Redis;

namespace FullStack.API.Services.Interfaces
{
    public interface IRedisDbConnectionService
    {
        IDatabase GetDatabase();
    }
}
