using PrimeNumbers.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

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
            var acquisitionTimeSeconds = 10;
            var logginPeriodMillis = 200;
            var resultList = new List<PrimeNumbersProgressModel>();
            void reportAction(PrimeNumbersProgressModel progressModel)
            {               
                resultList.Add(progressModel);               
            }
            var finder = new PrimeNumbersFinder(reportAction);

            var timerStart = DateTime.Now;
            var timer = new Timer(obj=>{
                Console.CursorVisible=false;
                var currentTime = DateTime.Now;
                var timePassed = currentTime.Subtract(timerStart);
                Console.CursorLeft=0;                
                Console.Write($"Time passed: {Math.Round(timePassed.TotalSeconds,2,MidpointRounding.ToZero)} s {new String(' ',10)}");
            },null,0, 1000);

            finder.FindPrimes(
                new TimeSpan(0, 0, acquisitionTimeSeconds),
                new TimeSpan(0, 0, 0, 0, logginPeriodMillis));

            foreach(var progressItem in resultList)
            {
                var resultString = $"{Convert.ToInt32(progressItem.MillisecondsPassed.TotalMilliseconds)}, " +
                    $"{progressItem.NumberOfPrimeNumbers}, " +
                    $"{progressItem.NumberOfIterations}, "+
                    $"{progressItem.MaxPrimeNumber}";

                File.AppendAllText("Result.scv", resultString + "\n");
            }

            timer?.Dispose();
            Console.CursorVisible=true;
        }
    }
}
