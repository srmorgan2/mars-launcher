using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleClient2.ServiceReference1;

namespace ConsoleClient2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleClient2.ServiceReference1.Service1Client client = new Service1Client();
            ConsoleClient2.ServiceReference1.CompositeType input = new CompositeType();
            ConsoleClient2.ServiceReference1.CompositeType result = client.Run(input);

            Console.WriteLine("Number of rows = {0}", result.Data.Tables[0].Rows.Count);

            Console.WriteLine("\nPress <Enter> to terminate the client.");
            Console.ReadLine();

            client.Close();


        }
    }
}
