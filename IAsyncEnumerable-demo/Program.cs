using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IAsyncEnumerable_demo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var hostingTask = host.RunAsync();

            var jokesEnumerable = (JokesEnumerable)host.Services.GetService(typeof(JokesEnumerable));

            await foreach (var joke in jokesEnumerable.Jokes(3000))
            {
                Console.WriteLine();
                Console.WriteLine(joke);
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
