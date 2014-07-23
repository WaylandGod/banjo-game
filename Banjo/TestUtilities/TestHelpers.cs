//-----------------------------------------------------------------------
// <copyright file="TestHelpers.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Threading;
using NUnit.Framework;

namespace TestUtilities
{
    /// <summary>General test helpers</summary>
    public static class TestHelpers
    {
        /// <summary>Runs an action multiple times and returns how long it took</summary>
        /// <param name="iterations">How many times to run the action</param>
        /// <param name="timeout">How long to wait for the run to complete (in milliseconds)</param>
        /// <param name="action">The action to run (int parameter is the iteration)</param>
        /// <returns>How long the run took to complete (in milliseconds)</returns>
        public static double TimedRun(int iterations, int timeout, Action<int> action)
        {
            var wait = new AutoResetEvent(false);
            double time = 0;
            var thread = new Thread(() =>
                {
                    var i = 0;
                    var start = DateTime.UtcNow;
                    do
                    {
                        action(i);
                    }
                    while (++i < iterations);

                    time = (DateTime.UtcNow - start).TotalMilliseconds;
                    wait.Set();
                });
            
            thread.Start();
            Assert.IsTrue(wait.WaitOne(timeout), "Timed run failed to complete within {0} milliseconds.".FormatInvariant(timeout));
            return time;
        }
    }
}
