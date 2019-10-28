using PrimeNumbers.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeNumbers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            if (File.Exists("Result.csv"))
            {
                File.Delete("Result.csv");
            }
            var acquisitionTimeSeconds = 60;
            var logginPeriodMilliseconds = 200;
            var timeStarted = DateTime.Now;
            var resultList = new List<PrimeNumbersProgressModel>();
            var finder = new PrimeNumbersFinder(reportAction);
            Console.CursorVisible = false;
            var timer = new Timer((obj) =>
            {               
                var timePassed = DateTime.Now.Subtract(timeStarted);
                Console.Write($" Seconds passed: {Convert.ToInt32(timePassed.TotalSeconds)}");
                Console.CursorLeft = 0;
            },null,0,1000);

            finder.FindPrimes(
                new TimeSpan(0,0, acquisitionTimeSeconds),
                new TimeSpan(0, 0, 0, 0, logginPeriodMilliseconds));
            
            foreach(var progressItem in resultList)
            {
                var resultString = $"{Convert.ToInt32(progressItem.MillisecondsPassed.TotalMilliseconds)}," +
                    $"{progressItem.NumberOfPrimeNumbers}, " +
                    $"{progressItem.NumberOfIterations}, " +
                    $"{progressItem.MaxPrimeNumber}";
                File.AppendAllText("Result.csv", resultString + "\n");
            }

            void reportAction(PrimeNumbersProgressModel progressModel)
            {
                resultList.Add(progressModel);
            }
            Console.CursorVisible = true;
            timer?.Dispose();
        }
    }
}
