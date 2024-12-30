using Scrabdle.Clients;
using Scrabdle.Entities;
using System.Runtime.InteropServices;

namespace Scrabdle.Proxies
{
    public class HerokuApiProxy(IHerokuHttpClient httpClient) : IHerokuApiProxy
    {
        private const string uri = "https://random-word-api.herokuapp.com/";
        private const string randomWordLengthQS = "word?length=";
        private readonly IHerokuHttpClient _httpClient = httpClient;

        public async Task<RandomWordResponse> GetRandomWordAsync(int length)
        {
            var response = await _httpClient.HttpClient.GetAsync(GetUrl([randomWordLengthQS, length.ToString()]));

            if (response == null)
            {
                return new($"Failed to get random word of length {length}");
            }
            else if (response.IsSuccessStatusCode)
            {
                return new(GetResponse(await response.Content.ReadAsStringAsync()));
            }
            else
            {
                return new(false, await response.Content.ReadAsStringAsync());
            }
        }

        private static string GetUrl(string[] qs) => uri + string.Join(string.Empty, qs);

        private static string GetResponse(string r) => r.Substring(2, r.Length - 4);
    }
}
