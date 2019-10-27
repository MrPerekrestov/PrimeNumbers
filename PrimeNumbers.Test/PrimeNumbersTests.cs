using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using PrimeNumbers.Core;
using System;
using System.Threading.Tasks;

namespace PrimeNumbers.Test
{
    [TestFixture]
    public class PrimeNumbersTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TimespanTotalMillisecons_work()
        {
            var timeSpan = new TimeSpan(0,0,5);
            TestContext.WriteLine(timeSpan.TotalMilliseconds);
            Assert.That(true);
        }
        [Test]
        public async Task DatetimeNow_allwaysNewValue()
        {
            var now = DateTime.Now;
            TestContext.WriteLine(now);
            await Task.Delay(10);
            TestContext.WriteLine(now);
            Assert.That(true);
        }
        [Test]
        public void PrimeNumberFinder_DuringFiveSeconds_FindsSomePrimes()
        {
            static void loggin(PrimeNumbersProgressModel result)
            {

            }

            var primNumberFinder = new PrimeNumbersFinder(loggin);
            var result = primNumberFinder.FindPrimes(new TimeSpan(0, 0, 5));
            TestContext.WriteLine(result.NumberOfPrimeNumbers);
            Assert.That(result, Is.Not.Null);
        }
    }
}