using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

class ThreadSafeRandom
{
    static int seed = Environment.TickCount;

    static readonly ThreadLocal<Random> random =
        new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

    public static int Next(int n)
    {
        return random.Value.Next(n);
    }

    public static int NextRange(int first, int last)
    {
        return random.Value.Next(first, last);
    }
}
