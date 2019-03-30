using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IAsyncEnumerable_demo
{
    public class AsyncEnumerableHub : Hub
    {
        private readonly JokesEnumerable _jokesEnumerable;

        public AsyncEnumerableHub(JokesEnumerable jokesEnumerable)
        {
            _jokesEnumerable = jokesEnumerable;
        }

        public async IAsyncEnumerable<string> Jokes(int delay, CancellationToken cancellationToken)
        {
            await foreach (var value in _jokesEnumerable.Jokes())
            {
                yield return value;
                await Task.Delay(delay);
                if (cancellationToken.IsCancellationRequested)
                    yield break;
            }
        }
    }
}
