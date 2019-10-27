using System;

namespace PirmeNumbers.Core
{
    public class PrimeNumbersFinder
    {
        private readonly Action<PrimeNumbersProgressModel> _reportAction;        

        public PrimeNumbersFinder(Action<PrimeNumbersProgressModel> reportAction)
        {
            _reportAction = reportAction;            
        }

        public PrimeNumbersCalculationResult FindPrimes(TimeSpan duration)
        {
            long numberOfIterations = 0;
            int numberOfPrimeNumbers = 0;
            long maxPrimeNumber = 3;
            var initialTimeStamp = DateTime.UtcNow;            
            long currentNumber = 4;

            while (DateTime.UtcNow.Subtract(initialTimeStamp)<duration)
            {               
                var isPrime = true;
                for (long n = 2;n<Math.Sqrt(currentNumber); n++)
                {
                    if (currentNumber % n == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                numberOfIterations += Convert.ToInt64(Math.Sqrt(currentNumber))-2;
                if (isPrime)
                {
                    maxPrimeNumber = currentNumber;
                    numberOfPrimeNumbers++;
                }
                currentNumber++;
            }

            return new PrimeNumbersCalculationResult
            {
                MaxPrimeNumber = maxPrimeNumber,
                NumberOfIterations = numberOfIterations,
                NumberOfPrimeNumbers = numberOfPrimeNumbers
            };
        }

        public PrimeNumbersCalculationResult FindPrimes(TimeSpan duration, TimeSpan logginPeriod)
        {
            long numberOfIterations = 0;
            int numberOfPrimeNumbers = 0;
            long maxPrimeNumber = 3;
            var initialTimeStamp = DateTime.UtcNow;
            var lastLogged = DateTime.UtcNow;
            long currentNumber = 4;
            TimeSpan secondsPassedFromLastLoggin;

            while (DateTime.UtcNow.Subtract(initialTimeStamp) < duration)
            {
                var isPrime = true;
                for (long n = 2; n < Math.Sqrt(currentNumber); n++)
                {
                    if (currentNumber % n == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                numberOfIterations += Convert.ToInt64(Math.Sqrt(currentNumber)) - 1;
                if (isPrime)
                {
                    maxPrimeNumber = currentNumber;
                    numberOfPrimeNumbers++;
                }
                currentNumber++;

                secondsPassedFromLastLoggin = DateTime.UtcNow.Subtract(lastLogged);

                if (secondsPassedFromLastLoggin > logginPeriod)
                {
                    _reportAction?.Invoke(new PrimeNumbersProgressModel
                    {
                        MaxPrimeNumber= maxPrimeNumber,
                        NumberOfIterations = numberOfIterations,
                        NumberOfPrimeNumbers = numberOfPrimeNumbers,
                        MillisecondsPassed = secondsPassedFromLastLoggin
                    });
                    lastLogged = DateTime.UtcNow;
                }
            }

            return new PrimeNumbersCalculationResult
            {
                MaxPrimeNumber = maxPrimeNumber,
                NumberOfIterations = numberOfIterations,
                NumberOfPrimeNumbers = numberOfPrimeNumbers
            };
        }

    }
}
