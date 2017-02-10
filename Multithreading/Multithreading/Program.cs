using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int result = 0;
                var l = new object();

                var tasks = new List<Task>
                {
                    new Task(() =>
                    {
                        //Console.WriteLine("Thread 3: Start...");
                        lock (l)
                        {
                            if (result%2 == 0)
                            {
                                Console.WriteLine(String.Format("Result%2 = {0}", result%2));
                                Console.WriteLine(String.Format("{0} is Even number.", result));
                            }
                        }
                        //Console.WriteLine("Thread 3: Finish.\n");
                    }),
                    new Task(() =>
                    {
                        //Console.WriteLine("Thread 1: Start...");
                        result = 1;
                        //Console.WriteLine("Thread 1: result=1. Finish.\n");
                    }),
                    new Task(() =>
                    {
                        //Console.WriteLine("Thread 2: Start...");
                        result = 2;
                        //Console.WriteLine("Thread 2: result=2. Finish.\n");
                    })
                    
                };

                foreach (var t in tasks)
                {
                    t.Start();
                }

                Task.WaitAll(tasks.ToArray());

                Console.WriteLine($"All tasks have been finihed. Final result: {result}\n");
                
                Console.ReadLine();
            }
        }
    }
}
