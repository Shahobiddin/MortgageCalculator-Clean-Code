using System;
using Comments.Task1.ThirdParty;

namespace Comments.Task1
{
    public static class MortgageCalculator
    {
        public static double CalculateMonthlyPayment(int principalAmount, int termInYear, double rateOfInterest)
        {
            ValidateInputs(principalAmount, termInYear, rateOfInterest);

            return GetMonthlyPayment(principalAmount, termInYear, rateOfInterest);
        }

        private static void ValidateInputs(int principalAmount, int termInYear, double rateOfInterest)
        {
            if (principalAmount < 0 || termInYear < 0 || rateOfInterest < 0)
            {
                throw new InvalidInputException("Negative values are not allowed");
            }
        }

        private static double GetMonthlyPayment(int principalAmount, int termInYear, double rateOfInterest)
        {
            (int termInMonth, double rateOfInterestInDecimal) = GetMonthlyTermAndRateOfInterest(termInYear, rateOfInterest);

            return IsZeroInterestRate(rateOfInterestInDecimal)
                    ? GetMonthlyPaymentWithZeroInterestRate(principalAmount, termInMonth)
                    : GetMonthlyPaymentWithInterestRate(principalAmount, termInMonth, rateOfInterestInDecimal);
        }
        private static bool IsZeroInterestRate(double rateOfInterestInDecimal) => rateOfInterestInDecimal == 0;

        private static double GetMonthlyPaymentWithZeroInterestRate(int principalAmount, int termInMonth)
        {
            return (double)principalAmount / termInMonth;
        }

        private static double GetMonthlyPaymentWithInterestRate(int principalAmount, int termInMonth, double rateOfInterestInDecimal)
        {
            double monthlyRate = rateOfInterestInDecimal / 12;

            double monthlyPayment = principalAmount * monthlyRate / (1 - Math.Pow(1 + monthlyRate, -termInMonth));

            return monthlyPayment;
        }

        private static (int, double) GetMonthlyTermAndRateOfInterest(int termInYear, double rateOfInterest)
        {
            var termInMonth = termInYear * 12;

            var rateOfInterestInDecimal = rateOfInterest / 100;

            return (termInMonth, rateOfInterestInDecimal);
        }
    }
}
