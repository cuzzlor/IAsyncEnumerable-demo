using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace IAsyncEnumerable_demo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var hostingTask = host.RunAsync();

            var jokesEnumerable = (JokesEnumerable)host.Services.GetService(typeof(JokesEnumerable));

            await foreach (var joke in jokesEnumerable.Jokes())
            {
                Console.WriteLine(joke + Environment.NewLine + Environment.NewLine);
                await Task.Delay(3000);
            }

            await hostingTask;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
