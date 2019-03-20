using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IAsyncEnumerable_demo
{
    public class JokesEnumerable
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JokesEnumerable(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async IAsyncEnumerable<string> GetAsyncEnumerable(int delay = 1000, int? howMany = null, CancellationToken cancellationToken = new CancellationToken())
        {
            using (var httpClient = _httpClientFactory.CreateClient("jokes"))
            {
                int iterations = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    var response = await httpClient.GetAsync("https://icanhazdadjoke.com/", cancellationToken);
                    var joke = await response.Content.ReadAsStringAsync();
                    yield return joke;
                    iterations++;
                    if (iterations >= howMany)
                        yield break;
                    await Task.Delay(delay, cancellationToken);
                }
            }
        }
    }
}
