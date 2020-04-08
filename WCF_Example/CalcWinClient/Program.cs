using System;
using System.Text;

namespace CalcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 2000.0;
            double y = 100.0;
            double result = 0;

            try
            {
                Console.WriteLine(@"Username = {0}", Environment.UserName);
                Console.WriteLine(@"Domain = {0}", Environment.UserDomainName);

                /*----------------------------
                 *  Using Basic HTTP Binding
                 *  -------------------------*/
                Console.WriteLine("Using Basic HTTP Binding", x, y, result);

                CalcServiceClient objCalcClient2 = new CalcServiceClient("BasicHttpBinding_ICalcService");

                result = objCalcClient2.Add(x, y);
                Console.WriteLine("Calling Add >>  X : {0:F2}  Y : {1:F2}  Result : {2:F2}", x, y, result);

                result = objCalcClient2.Subtract(x, y);
                Console.WriteLine("Calling Sub >>  X : {0:F2}  Y : {1:F2}  Result : {2:F2}", x, y, result);

                result = objCalcClient2.Multiply(x, y);
                Console.WriteLine("Calling Mul >>  X : {0:F2}  Y : {1:F2}  Result : {2:F2}", x, y, result);

                result = objCalcClient2.Divide(x, y);
                Console.WriteLine("Calling Sub >>  X : {0:F2}  Y : {1:F2}  Result : {2:F2}", x, y, result);

                /*------------------------
                 *  Using TCP Binding
                 *  ----------------------*/
                Console.WriteLine("Using TCP Binding", x, y, result);

                CalcServiceClient objCalcClient1 = new CalcServiceClient("NetTcpBinding_ICalcService");

                result = objCalcClient1.Add(x, y);
                Console.WriteLine("Calling Add >>  X : {0:F2}  Y : {1:F2}  Result : {2:F2}", x, y, result);

                result = objCalcClient1.Subtract(x, y);
                Console.WriteLine("Calling Sub >>  X : {0:F2}  Y : {1:F2}  Result : {2:F2}", x, y, result);

                result = objCalcClient1.Multiply(x, y);
                Console.WriteLine("Calling Mul >>  X : {0:F2}  Y : {1:F2}  Result : {2:F2}", x, y, result);

                result = objCalcClient1.Divide(x, y);
                Console.WriteLine("Calling Sub >>  X : {0:F2}  Y : {1:F2}  Result : {2:F2}", x, y, result);


            }
            catch (Exception eX)
            {
                Console.WriteLine("There was an error while calling Service [" + eX.Message + "]");
            }
        }
    }
}
