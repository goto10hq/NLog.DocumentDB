using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nlog.DocumentDBTarget.Tools
{
    public static class AsyncTools
    {
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        /// <summary>
        /// Run task sync.
        /// </summary>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _myTaskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }        
    }
}
