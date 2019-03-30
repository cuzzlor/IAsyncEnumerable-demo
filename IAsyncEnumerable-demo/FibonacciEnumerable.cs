using System.Collections.Generic;

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
    }
}
