using System.Threading;

namespace Acolyte.Threading
{
    /// <summary>
    /// Contains additional logic to work with <see cref="ThreadPool" /> class.
    /// </summary>
    public static class ThreadPoolHelper
    {
        public static int GetThreadPoolThreadsInUse()
        {
            ThreadPool.GetMaxThreads(out int max, out int _);
            ThreadPool.GetAvailableThreads(out int available, out int _);
            int running = max - available;
            return running;
        }
    }
}
