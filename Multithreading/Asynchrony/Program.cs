using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Asynchrony
{
    class Program
    {
        public delegate int DisplayHandler();
        public delegate int ParameterizedDisplayHanler(int k);

        static void Main(string[] args)
        {
            DisplayHandler handler = Display;

            Console.WriteLine("---==Calling Display method in sychronous mode==---\n");
            //---------------------------------------------------------
            int result = handler.Invoke();
            Console.WriteLine("Method Main continue to work...");
            Console.WriteLine("Sync Result is {0}\n", result);
            //---------------------------------------------------------
            Console.WriteLine("---==Calling Display method in asychronous mode==---\n");
            var asyncResult = handler.BeginInvoke(null, null);
            Console.WriteLine("Method Main continue to work...");
            var res = handler.EndInvoke(asyncResult);
            Console.WriteLine("Async Result is {0}", res);
            //---------------------------------------------------------
            Console.WriteLine("---==Using paramethers in async code==---\n");

            ParameterizedDisplayHanler parameterizedDisplayHanler = ParameterizedDisplay;
            IAsyncResult resultObj =
                parameterizedDisplayHanler.BeginInvoke(10, new AsyncCallback(AsyncCompleted), "Asynchronous calls");

            Console.WriteLine("Method Main continue to work...");
            int paramRes = parameterizedDisplayHanler.EndInvoke(resultObj);
            Console.WriteLine("Async Result with parameters: {0}", paramRes);
            //---------------------------------------------------------
            Console.WriteLine("---==Using async and await==---\n");

            DisplayResultAsync("Task1", 5).NoWarning();//using extension
            Console.WriteLine("This line was printed without waiting for Task1.");

            Task t = DisplayResultAsync("Task2", 6);
            t.Wait();
            Console.WriteLine("This line was printed after task Task2 completion.");


            DisplayResultAsync("Task3", 7).GetAwaiter().GetResult();//another way of waiting for task completion

            Console.WriteLine("This line was printed after task Task3 completion.");

            SequentialAsyncCall().NoWarning();

            ParallelAsyncCall().NoWarning();

            //Ex handling
            DisplayResultAsync("Task with Exception", 0).GetAwaiter().GetResult();

            //Ex handling in void methods
            //try
            //{
            //    VoidMethodWithEx(0);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            HandleSeveralExceptions().NoWarning();

            DisplayResultWithCancellationAsync(5);

            Console.ReadLine();
        }
        
        static int Display()
        {
            Console.WriteLine("The work of method Display is being started....");

            int result = 0;
            for (int i = 1; i < 10; i++)
            {
                result += i * i;
            }
            Thread.Sleep(2000);
            Console.WriteLine("The work of method Display is being ended....");
            return result;
        }

        static int ParameterizedDisplay(int k)
        {
            Console.WriteLine("The work of Parameterized method Display is being started....");
            int result = 0;
            for (int i = 1; i < 10; i++)
            {
                result += k * i;
            }
            Thread.Sleep(2000);
            Console.WriteLine("The work of Parameterized method Display is being ended....");
            return result;
        }

        static void AsyncCompleted(IAsyncResult resObj)
        {
            var msg = (string)resObj.AsyncState;
            Console.WriteLine("Value of async state: '{0}'", msg);
            Console.WriteLine("The work of asynchronous delegate has been finished.");
        }


        static async Task DisplayResultAsync(string taskName, int num)
        {
            var factorial = FactorialAsync(num);
            try
            { 
                var result = await factorial;
                Thread.Sleep(3000);
                Console.WriteLine("{0}: The factorial of number {1} is equal to {2}",taskName , num, result);
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);//Either
                Console.WriteLine(factorial.Exception.InnerException.Message);//Or
                Console.WriteLine("IsFaulted: {0}", factorial.IsFaulted);
                Console.WriteLine("IsCanceled: {0}", factorial.IsCanceled);
                Console.WriteLine("IsCompleted: {0}", factorial.IsCompleted);
            }
        }

        static async Task<int> FactorialAsync(int x)
        {
            if (x < 1)
            {
                throw new Exception("The number can`t be less than 1.");
            }
            var result = 1;
            return await Task.Run(() =>
            {
                for (var i = 1; i <= x; i++)
                {
                    result *= i;
                }
                return result;
            });
        }

        static async Task SequentialAsyncCall()
        {
            int num = 5;
            int result = await FactorialAsync(num);
            Console.WriteLine("The factorial of number {0} is equal to {1}", num, result);
            num = 6;
            result = FactorialAsync(num).GetAwaiter().GetResult();
            Console.WriteLine("The factorial of number {0} is equal to {1}", num, result);

            result = await Task.Run(() =>
            {
                var res = 1;
                for (var i = 1; i <= 9; i++)
                {
                    res += i * i;
                }
                return res;
            });
            Console.WriteLine("The sum of number squared is equal to {0}", result);
        }

        static async Task ParallelAsyncCall()
        {
            //int num1 = 5;
            //int num2 = 6;
            int num1 =0, num2 = 0;
            Task<int> t1 = FactorialAsync(num1);
            Task<int> t2 = FactorialAsync(num2);
            Task<int> t3 = Task.Run(() =>
            {
                var res = 1;
                for (var i = 1; i <= 9; i++)
                {
                    if (i == 2)
                    {
                        throw new Exception("i = 2 !!");
                    }
                    res += i * i;
                }
                return res;
            });

            var allRes = await Task.WhenAll(t1, t2, t3);

            Console.WriteLine("The factorial of number {0} is equal to {1}", num1, t1.Result);
            Console.WriteLine("The factorial of number {0} is equal to {1}", num2, t2.Result);
            Console.WriteLine("The sum of number squared is equal to {0}", t3.Result);
        }

        private static async Task HandleSeveralExceptions()
        {
            Task allTasks = null;
            try
            {
                var t1 = ExMethod1Async();
                var t2 = ExMethod2Async();
                var t3 = ExMethod3Async();

                allTasks = Task.WhenAll(t1, t2, t3);
                await allTasks;
                
            }
            catch (ArithmeticException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("IsFaulted: " + allTasks.IsFaulted);
                Console.WriteLine("IsCompleted: " + allTasks.IsCompleted);
                Console.WriteLine("IsCanceled: " + allTasks.IsCanceled);
                foreach (var inx in allTasks.Exception.InnerExceptions)
                {
                    Console.WriteLine("Inner Exception: " + inx.Message);
                }
                var ae = allTasks.Exception.Flatten();
                Console.WriteLine("Aggregate Exception: " + ae.Message);
                ae.InnerExceptions.ToList().ForEach(x => { Console.WriteLine("From aggregate exception {0}: {1}",ae.InnerExceptions.IndexOf(x) + 1, x.Message); });
            }
            catch (ArrayTypeMismatchException ex)
            {
                await Task.Run(() => Console.WriteLine("Await inside of Catch"));
            }
            finally
            {
                await Task.Run(() => Console.WriteLine("Await inside of Finally"));
            }
        }

        //Exception from void method can`t be handled. Need to return a task to handle ex.
        static async void VoidMethodWithEx(int x)
        {
            await Task.Delay(500);
            if (x < 1)
            {
                throw new Exception("The number can`t be less than 1.");
            }
        }

        static async Task ExMethod1Async()
        {
            await Task.Delay(100);
            throw new ArithmeticException("ExMethod1Async threw an exception!!!");
        }
        static async Task ExMethod2Async()
        {
            await Task.Delay(100);
            throw new ArithmeticException("ExMethod2Async threw an exception!!!");
        }

        static async Task ExMethod3Async()
        {
            await Task.Delay(100);
            throw new ArrayTypeMismatchException("ExMethod3Async threw an exception!!!");
        }

        static async void DisplayResultWithCancellationAsync(int num)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            try
            {
                var t1 = FactorialWithCancellationAsync(num, cts.Token);
                var t2 = Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    cts.Cancel(); 
                });
                await Task.WhenAll(t1, t2);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cts.Dispose();
            }
        }

        static Task FactorialWithCancellationAsync(int x, CancellationToken token)
        {
            return Task.Run(() =>
            {
                int result = 1;
                for (int i = 1; i <= x; i++)
                {
                    token.ThrowIfCancellationRequested();
                    result *= i;
                    Console.WriteLine("Factorial of number {0} is equal to {1}", i, result);
                    Thread.Sleep(1000);
                }
            });
        }
    }

    static class TaskExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NoWarning(this Task task) {}
    }
}
