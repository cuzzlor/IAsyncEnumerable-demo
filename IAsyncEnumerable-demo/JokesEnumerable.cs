using System.Collections.Generic;
using System.Net.Http;

namespace IAsyncEnumerable_demo
{
    public class JokesEnumerable
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JokesEnumerable(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async IAsyncEnumerable<string> Jokes()
        {
            using (var httpClient = _httpClientFactory.CreateClient("jokes"))
            {
                while (true)
                {
                    var response = await httpClient.GetAsync("https://icanhazdadjoke.com/");
                    yield return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
