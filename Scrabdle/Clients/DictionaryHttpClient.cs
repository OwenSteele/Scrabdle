namespace Scrabdle.Clients
{
    public class DictionaryHttpClient
        : IDictionaryHttpClient
    {
        private readonly HttpClient _httpClient = new();

        public HttpClient HttpClient => _httpClient;
    }
}
