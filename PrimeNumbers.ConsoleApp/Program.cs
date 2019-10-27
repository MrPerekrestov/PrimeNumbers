using PrimeNumbers.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrimeNumbers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            if (File.Exists("Result.txt"))
            {
                File.Delete("Result.txt");
            }
            var resultList = new List<PrimeNumbersProgressModel>();
            void reportAction(PrimeNumbersProgressModel progressModel)
            {               
                resultList.Add(progressModel);               
            }
            var finder = new PrimeNumbersFinder(reportAction);
            
            finder.FindPrimes(new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 0, 0, 200));
            foreach(var progressItem in resultList)
            {
                var resultString = $"passed: {Convert.ToInt32(progressItem.MillisecondsPassed.TotalMilliseconds)} ms," +
                    $" number of primes: {progressItem.NumberOfPrimeNumbers}," +
                    $" number of iterations {progressItem.NumberOfIterations}";
                File.AppendAllText("Result.txt", resultString + "\n");
            }
        }
    }
}
