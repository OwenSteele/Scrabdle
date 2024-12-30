using Scrabdle.Clients;
using Scrabdle.Entities;

namespace Scrabdle.Proxies
{
    public class DictionaryApiProxy(IDictionaryHttpClient dictionaryHttpClient) : IDictionaryApiProxy
    {
        private const string uri = "https://api.dictionaryapi.dev/api/v2/entries/en/";
        private readonly IDictionaryHttpClient dictionaryHttpClient = dictionaryHttpClient;

        public async Task<DictionaryApiResponse> CheckWordAsync(string word)
        {
            var response = await dictionaryHttpClient.HttpClient.GetAsync(GetUrl(word));

            if (response == null)
            {
                return new("Failed to check word");
            }
            else if (response.IsSuccessStatusCode)
            {
                return new(true, "Word Found");
            }
            else
            {
                return new("Word not found");
            }
        }

        private static string GetUrl(string qs) => uri + qs;
    }
}
