using PirmeNumbers.Core;
using System;

namespace PrimeNumbers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            static void reportAction(PrimeNumbersProgressModel progressModel)
            {
                Console.WriteLine($"passed: {progressModel.MillisecondsPassed.TotalMilliseconds} ms, number of primes: {progressModel.NumberOfPrimeNumbers}");
            }
            var finder = new PrimeNumbersFinder(reportAction);

            finder.FindPrimes(new TimeSpan(0, 0, 5),new TimeSpan(0,0,0,0,200));
        }
    }
}
