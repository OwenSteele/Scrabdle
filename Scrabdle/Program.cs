using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scrabdle.Clients;
using Scrabdle.Proxies;
using Scrabdle.Stores;
using Scrabdle.Validation;

namespace Scrabdle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateDefaultApp(args);
            var host = hostBuilder.Build();

            Console.WriteLine("starting");
            var app = host.Services.GetRequiredService<IGameManager>();
            host.Start();
            app.Run();
            Console.WriteLine("stopped");
        }

        private static IHostBuilder CreateDefaultApp(string[] args)
        {
            var builder = Host.CreateDefaultBuilder();
            builder.ConfigureServices(conf =>
            {
                conf.AddTransient<IGameManager, GameManager>();
                conf.AddTransient<IGameRunner, GameRunner>();
                conf.AddScoped<IInputProcessor, InputProcessor>();
                conf.AddScoped<IOutputProvider, OutputProvider>();
                conf.AddSingleton<IGuessStore, GuessStore>();
                conf.AddSingleton<IGameStore, GameStore>();
                conf.AddScoped<IGuessValidator, GuessValidator>();
                conf.AddSingleton<IDictionaryHttpClient, DictionaryHttpClient>();
                conf.AddSingleton<IHerokuHttpClient, HerokuHttpClient>();
                conf.AddScoped<IDictionaryApiProxy, DictionaryApiProxy>();
                conf.AddScoped<IHerokuApiProxy, HerokuApiProxy>();
                conf.AddScoped<IDictionaryProcessor, DictionaryProcessor>();
            });

            builder.UseConsoleLifetime();

            return builder;
        }
    }
}