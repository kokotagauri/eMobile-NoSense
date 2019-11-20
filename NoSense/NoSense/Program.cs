using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NoSense
{
    public static class Program
    {
        public static void Main()
        {
            Console.Write("Enter array: ");
            while (true)
            {
                var numberStr = Console.ReadLine();
                if (IsValidArray(numberStr))
                {
                    Work(numberStr);
                    break;
                }

                Console.Write("Please enter valid array: ");
            }
        }


        public static void Work(string arrayInput)
        {
            var data = arrayInput.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            Console.WriteLine("Return value for first case: " + data.ThisDoesntMakeAnySense(i => i % 1 == 0, NewValue));
            Thread.Sleep(2000);
            Console.WriteLine("Return value for second case: " + data.ThisDoesntMakeAnySense(i => i*2 % 2 != 0, NewValue));
            Thread.Sleep(2000);
            Console.WriteLine("Third case: Exception will be thrown in");
            Console.WriteLine("3");
            Thread.Sleep(1000);
            Console.WriteLine("2");
            Thread.Sleep(1000);
            Console.WriteLine("1");
            Thread.Sleep(1000);
            Console.WriteLine("Boom!");
            Thread.Sleep(1000);
            Console.WriteLine(data.ThisDoesntMakeAnySense(i => i % 2 == 0, null));
        }

        public static T ThisDoesntMakeAnySense<T>(this IEnumerable<T> data, Func<T, bool> predicate = null, Func<T> newValue = null)
        {
            if (data == null || predicate == null || newValue == null)
                throw new NullReferenceException();

            foreach (var value in data)
                if (predicate(value))
                    return default;

            return newValue();
        }

        private static bool IsValidArray(string arr)
        {
            try
            {
                var array = arr.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int NewValue()
        {
            return 100;
        }
    }
}
