using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_ServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetMessage(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType Run(CompositeType input)
        {
            if (input == null)
                input = new CompositeType();

            if (input.Data == null)
                input.Data = new DataSet();

            input.Data.Tables.Clear();
            var myTable = new DataTable("Results");
            myTable.Columns.Add("Asofdate", typeof(DateTime));
            myTable.Columns.Add("TradeID", typeof(String));
            myTable.Columns.Add("Price", typeof(double));
            myTable.Columns.Add("Currency", typeof(String));

            var myDate = new DateTime(2020, 1, 2);
            for (int i = 1; i <= 20; i++)
            {
                var myRow = myTable.NewRow();
                var myName = string.Format("ABC{0:D4}", i);
                double myPrice = 1E6 + i * 10000;

                myRow["Asofdate"] = myDate;
                myRow["TradeID"] = myName;
                myRow["Price"] = myPrice;
                myRow["Currency"] = "GBP";

                myTable.Rows.Add(myRow);

                myDate = myDate.AddDays(1);
            }
           

            input.Data.Tables.Add(myTable);

            return input;
        }
    }
}
