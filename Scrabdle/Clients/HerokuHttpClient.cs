namespace Scrabdle.Clients
{
    public class HerokuHttpClient : IHerokuHttpClient
    {
        private readonly HttpClient _httpClient = new();

        public HttpClient HttpClient => _httpClient;
    }
}
