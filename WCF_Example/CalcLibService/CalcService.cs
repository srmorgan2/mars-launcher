//  Listing of CalcService.cs
using System;
using System.ServiceModel;
using System.Text;

namespace WCFCalcLib
{
    public class CalcService : ICalcService
    {
        public double Add(double a, double b)
        {
            return (a + b);
        }

        public double Subtract(double a, double b)
        {
            return (a - b);
        }

        public double Multiply(double a, double b)
        {
            return (a * b);
        }

        public double Divide(double a, double b)
        {
            return ((b == 0) ? 0 : (a / b));
        }
    }
}