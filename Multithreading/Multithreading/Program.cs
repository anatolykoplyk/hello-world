using System;
using Multithreading.Pitfalls;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                RaceCondition.UseNonThreadSafeClasses();
                Console.ReadLine();
            }
        }
    }
}
