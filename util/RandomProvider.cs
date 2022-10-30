using System;
using System.Threading;

namespace MediaViewer.Utility
{
    /// <summary>
    /// A static random number generator, for use in the entire app. 
    /// </summary>
    public static class RandomProvider
    {
        private static int seed = (int)DateTime.UtcNow.Ticks;

        private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref seed))
        );

        public static Random GetThreadRandom()
        {
            return randomWrapper.Value;
        }
    }
}