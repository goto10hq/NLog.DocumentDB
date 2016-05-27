using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace Nlog.DocumentDBTarget.DocumentDB
{
    public static class Core
    {                           
        /// <summary>
        /// Execute db action with retries.
        /// </summary>
        internal static async Task<T2> ExecuteWithRetriesAsync<T2>(Func<Task<T2>> function)
        {
            while (true)
            {
                TimeSpan sleepTime;

                try
                {
                    return await function();
                }
                catch (DocumentClientException de)
                {
                    if (de.StatusCode != null &&
                        ((int)de.StatusCode != 429 &&
                        (int)de.StatusCode != 503))
                    {
                        throw;
                    }
                    sleepTime = de.RetryAfter;
                }
                catch (AggregateException ae)
                {
                    if (!(ae.InnerException is DocumentClientException))
                    {
                        throw;
                    }

                    var de = (DocumentClientException)ae.InnerException;
                    if (de.StatusCode != null &&
                        ((int)de.StatusCode != 429 &&
                        (int)de.StatusCode != 503))
                    {
                        throw;
                    }

                    sleepTime = de.RetryAfter;
                }

                await Task.Delay(sleepTime);
            }
        }
    }
}
