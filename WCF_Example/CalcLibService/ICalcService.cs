//  Listing of ICalcService.cs
using System;
using System.Text;
using System.ServiceModel;

namespace WCFCalcLib
{
    [ServiceContract]
    public interface ICalcService
    {
        [OperationContract]
        double Add(double a, double b);

        [OperationContract]
        double Subtract(double a, double b);

        [OperationContract]
        double Multiply(double a, double b);

        [OperationContract]
        double Divide(double a, double b);
    }
}
