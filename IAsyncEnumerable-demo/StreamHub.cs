
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace IAsyncEnumerable_demo
{
    public class StreamHub : Hub
    {
        private readonly JokesEnumerable _jokesEnumerable;

        public StreamHub(JokesEnumerable jokesEnumerable)
        {
            _jokesEnumerable = jokesEnumerable;
        }

        // as per the official doc: https://docs.microsoft.com/en-us/aspnet/core/signalr/streaming?view=aspnetcore-3.0
        public async IAsyncEnumerable<int> Counter(
            int count,
            int delay,
            CancellationToken cancellationToken)
        {
            for (var i = 0; i < count; i++)
            {
                // Check the cancellation token regularly so that the server will stop
                // producing items if the client disconnects.
                cancellationToken.ThrowIfCancellationRequested();

                yield return i;

                // Use the cancellationToken in other APIs that accept cancellation
                // tokens so the cancellation can flow down to them.
                await Task.Delay(delay, cancellationToken);
            }
        }

        public async IAsyncEnumerable<string> Jokes(
            int count,
            int delay,
            CancellationToken cancellationToken)
        {
            await foreach (var value in _jokesEnumerable.GetAsyncEnumerable(delay, count, cancellationToken))
            {
                yield return value;
            }
        }

        public async IAsyncEnumerable<long> Fibonacci(
            int count,
            int delay,
            CancellationToken cancellationToken)
        {
            await foreach (var value in FibonacciEnumerable.GetAsyncEnumerable(delay, count, cancellationToken))
            {
                yield return value;
            }
        }
    }
}
