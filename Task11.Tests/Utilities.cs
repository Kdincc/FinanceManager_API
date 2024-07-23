using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Tests
{
    internal static class Utilities
    {
        public static async IAsyncEnumerable<T> GetEmptyAsyncEnumerable<T>()
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
