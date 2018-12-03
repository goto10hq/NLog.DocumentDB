using System;

namespace Nlog.DocumentDBTarget.Tools
{
    internal static class Extensions
    {
        /// <summary>
        /// Convert date time to epoch.
        /// </summary>
        public static int ToEpoch(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1);
            var epochTimeSpan = date - epoch;

            return (int)epochTimeSpan.TotalSeconds;
        }
    }
}