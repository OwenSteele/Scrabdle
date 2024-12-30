using Scrabdle.Entities;

namespace Scrabdle.Proxies
{
    public interface IHerokuApiProxy
    {
        Task<RandomWordResponse> GetRandomWordAsync(int length);
    }
}