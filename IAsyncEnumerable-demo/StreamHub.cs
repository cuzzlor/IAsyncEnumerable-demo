
using System;
using System.Collections.Generic;
using System.Threading;
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
