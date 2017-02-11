using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Pitfalls
{
    public class RaceCondition
    {
        public static void Run()
        {
            var result = 0;
            var result2 = 0;
            var l = new object();
            const int expected = 111;
            var lockFreeTasks = new List<Task<int>>
            {
                new Task<int>(() => result2 + 1),
                new Task<int>(() => result2 + 10),
                new Task<int>(() => result2 + 100)
            };
            var tasks = new List<Task>
            {
                //new Task(() => result += 1),
                //new Task(() => result += 10),
                //new Task(() => result += 100),
                new Task(() => Interlocked.Add(ref result, 1)),
                new Task(() => Interlocked.Add(ref result, 10)),
                new Task(() => Interlocked.Add(ref result, 100)),
                //new Task(() =>
                //{
                //    lock (l)
                //    {
                //        result += 1;
                //    }
                //}),
                //new Task(() =>
                //{
                //    lock (l)
                //    {
                //        result += 10;
                //    }
                //}),
                //new Task(() =>
                //{
                //    lock (l)
                //    {
                //        result += 100;
                //    }
                //})
            };
            tasks.ForEach(i => i.Start());
            lockFreeTasks.ForEach(i => i.Start());
            var lockFreeTasksRes = lockFreeTasks.Sum(i => i.Result);
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Result must be {expected}. Actual result is {result}. Statement is {result == expected}");
            Console.WriteLine($"Lock free: Actual result is {lockFreeTasksRes}. Statement is {lockFreeTasksRes == expected}");
        }

        public static void UseNonThreadSafeClasses()
        {
            var results = new List<int>();
            for (var i = 0; i < 100; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    results.Add(GetRandInt());
                });
            }
            results.ForEach(Console.WriteLine);
            Console.WriteLine(results.Count);
        }

        public static void UseThreadSafeClasses()
        {
            var tasks = new List<Task>();
            var results = new ConcurrentQueue<int>();
            for (var i = 0; i < 100; i++)
            {
                tasks.Add(new Task(() =>
                {
                    results.Enqueue(GetRandInt());
                }));
            }
            tasks.ForEach(t => t.Start());
            Task.WaitAll();

            foreach (var r in results)
            {
                Console.WriteLine(r);
            }
            Console.WriteLine(results.Count);
        }

        private static int GetRandInt()
        {
            var rng = new RNGCryptoServiceProvider();
            var data = new byte[4];
            rng.GetBytes(data);
            var seed = BitConverter.ToInt32(data, 0);
            var rand = new Random(seed);
            return rand.Next();
        }
    }
}
