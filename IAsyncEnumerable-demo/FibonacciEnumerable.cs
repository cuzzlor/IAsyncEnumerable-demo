using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IAsyncEnumerable_demo
{
    public static class FibonacciEnumerable
    {
        public static IEnumerable<long> Fibonacci()
        {
            yield return 0;
            yield return 1;

            var previous = 0;
            var current = 1;

            while (true)
            {
                var next = previous + current;
                previous = current;
                current = next;
                yield return next;
            }
        }

        public static async IAsyncEnumerable<long> Fibonacci(int delay = 1000, int? howMany = null, CancellationToken cancellationToken = new CancellationToken())
        {
            var iterations = 0;

            foreach (var value in Fibonacci())
            {
                yield return value;
                iterations++;
                if (iterations >= howMany)
                    break;
                await Task.Delay(delay, cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                    yield break;
            }
        }
    }
}
