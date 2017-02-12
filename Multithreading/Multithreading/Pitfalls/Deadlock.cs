using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading.Pitfalls
{
    public static class Deadlock
    {
        public static void Run()
        {
            var lockA = new object();
            var lockB = new object();

            var tasks = new List<Task> {

            new Task(()  =>
            {
                //Solution 1 - always lock the two mutexes in the same order
                lock (lockA)
                {
                    Console.WriteLine("Thread 1: lock A locked.");
                    Console.WriteLine("Thread 1: trying to lock on lock B....");

                    //Problem
                    lock (lockB)
                    {
                        Console.WriteLine("Thread 1: in Critical Section.");
                        Console.WriteLine("\tThread 1: lock B locked.");
                    }

                    //Solution 2 - use class Monitor.
                    //if (Monitor.TryEnter(lockB, 5000))
                    //{
                    //    try
                    //    {
                    //        Console.WriteLine("\tThread 1: in Critical Section.");
                    //        Console.WriteLine("\tThread 1: lock B locked.");
                    //    }
                    //    finally
                    //    {
                    //        Monitor.Exit(lockB);
                    //        Console.WriteLine("Thread 1: lock B released");
                    //    }
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Unable to acquire lock, exiting Thread 1.");
                    //}
                }
                Console.WriteLine("Thread 1: lock A released");

            }),
            new Task(() =>
            {
                Console.WriteLine("Thread 2: trying to lock on lock A....");
                //Thread.Sleep(500);
                lock (lockB)
                {
                    Console.WriteLine("Thread 2: lock B locked.");
                    Console.WriteLine("Thread 2: trying to lock on lock A....");
                    lock (lockA)
                    {
                        Console.WriteLine("\tThread 2: in Critical Section.");
                        Console.WriteLine("\tThread 2: lock A locked.");
                    }
                    Console.WriteLine("Thread 2: lock A released");
                }
                Console.WriteLine("Thread 2: lock B released");
            })};

            tasks.ForEach(t => t.Start());
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("All Tasks are finished.");
        }
    }
}
