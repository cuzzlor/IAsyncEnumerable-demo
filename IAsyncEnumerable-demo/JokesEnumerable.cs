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
                    yield return await httpClient.GetAsync("https://icanhazdadjoke.com/", cancellationToken).Result
                        .Content.ReadAsStringAsync();
                    iterations++;
                    if (iterations >= howMany)
                        yield break;
                    await Task.Delay(delay, cancellationToken);
                }
            }
        }
    }
}
