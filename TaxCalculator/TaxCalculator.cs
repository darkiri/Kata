using System;
using System.Linq;
using NUnit.Framework;

namespace TaxCalculator
{
    [TestFixture]
    public class TaxCalculatorTests
    {
        [TestCase(5000, Result = 500, Description = "10% Rate")]
        [TestCase(5800, Result = 609.2, Description = "14% Rate")]
        [TestCase(9000, Result = 1087.8, Description = "23% Rate")]
        [TestCase(15000, Result = 2532.9, Description = "30% Rate")]
        [TestCase(50000, Result = 15068.1, Description = "45% Rate")]
        public double TaxCalculatorTest(double income)
        {
            var calc = new Calculator();
            return calc.GetTax(income);
        }
    }

    public class Calculator
    {
        private readonly double[] _incomeRange = new double[] { 0, 5070, 8660, 14070, 21240, 40230 };
        private readonly double[] _taxRates = new[] { .10, .14, .23, .3, .33, .45 };

        public double GetTax(double income)
        {
            return Enumerable
                .Range(0, _taxRates.Length)
                .Select(taxLevel => GetTaxableAmount(income, taxLevel) * GetRate(taxLevel))
                .Sum();
        }

        private double GetTaxableAmount(double income, int taxLevel)
        {
            return income > GetHigh(taxLevel)
                       ? GetHigh(taxLevel) - GetLow(taxLevel)
                       : income > GetLow(taxLevel)
                             ? income - GetLow(taxLevel)
                             : 0;
        }

        private double GetRate(int taxLevel)
        {
            return _taxRates[taxLevel];
        }

        private double GetHigh(int taxLevel)
        {
            return taxLevel >= _incomeRange.Length - 1
                       ? Double.MaxValue
                       : _incomeRange[taxLevel + 1];
        }

        private double GetLow(int taxLevel)
        {
            return _incomeRange[taxLevel];
        }
    }
}