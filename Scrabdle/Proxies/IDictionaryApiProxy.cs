using Scrabdle.Entities;

namespace Scrabdle.Proxies
{
    public interface IDictionaryApiProxy
    {
        Task<DictionaryApiResponse> CheckWordAsync(string word);
    }
}