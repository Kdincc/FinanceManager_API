using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Tests
{
    internal static class Utilities
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public static async IAsyncEnumerable<T> GetEmptyAsyncEnumerable<T>()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            yield break;
        }

        public static async IAsyncEnumerable<T> GetAsyncEnumerable<T>(params T[] items)
        {
            foreach (var item in items)
            {
                yield return item;

                await Task.Yield();
            }
        }
    }
}
