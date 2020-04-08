using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCF_ServiceLibrary;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8974/WCF_ServiceLibrary/Service1/");

            ServiceHost myHost = new ServiceHost(typeof(Service1), baseAddress);

            try
            {
                myHost.AddServiceEndpoint(typeof(IService1), new BasicHttpBinding(), "Service1");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                myHost.Description.Behaviors.Add(smb);

                myHost.Open();
                Console.WriteLine("The service is ready.");

                // Close the ServiceHost to stop the service.
                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                myHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("A communication exception occurred: {0}", ce.Message);
                myHost.Abort();
            }
            catch (Exception ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                myHost.Abort();
            }
        }
    }
}
