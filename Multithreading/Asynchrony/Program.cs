using System;
using System.Threading;

namespace Asynchrony
{
    class Program
    {
        public delegate int DisplayHandler();
        public delegate int ParameterizedDisplayHanler(int k);

        static void Main(string[] args)
        {
            DisplayHandler handler = Display;

            //Console.WriteLine("---==Calling Display method in sychronous mode==---\n");
            ////---------------------------------------------------------
            //int result = handler.Invoke();
            //Console.WriteLine("Method Main continue to work...");
            //Console.WriteLine("Sync Result is {0}\n", result);
            ////---------------------------------------------------------
            //Console.WriteLine("---==Calling Display method in asychronous mode==---\n");
            //var asyncResult = handler.BeginInvoke(null, null);
            //Console.WriteLine("Method Main continue to work...");
            //var res = handler.EndInvoke(asyncResult);
            //Console.WriteLine("Async Result is {0}", res);
            ////---------------------------------------------------------
            Console.WriteLine("---==Using paramethers in async code==---\n");

            ParameterizedDisplayHanler parameterizedDisplayHanler = ParameterizedDisplay;
            IAsyncResult resultObj = 
                parameterizedDisplayHanler.BeginInvoke(10, new AsyncCallback(AsyncCompleted), "Asynchronous calls");

            Console.WriteLine("Method Main continue to work...");
            int paramRes = parameterizedDisplayHanler.EndInvoke(resultObj);
            Console.WriteLine("Async Result with parameters: {0}", paramRes);


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
    }
}
