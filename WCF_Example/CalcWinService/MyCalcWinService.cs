using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCFCalcLib;

namespace WCFHostedWindowsService
{
    public partial class MyCalcWinService : ServiceBase
    {
        ServiceHost _serciveHost = null ;

        public MyCalcWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (_serciveHost != null) _serciveHost.Close();
                
            string httpAddress = "http://localhost:9001/CalcService";
            string tcpAddress = "net.tcp://localhost:9002/CalcService";

            Uri[] adresses = { new Uri(httpAddress), new Uri(tcpAddress) };
            _serciveHost = new ServiceHost(typeof(WCFCalcLib.CalcService), adresses);

            ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
            _serciveHost.Description.Behaviors.Add(mBehave);

            BasicHttpBinding httpb = new BasicHttpBinding();
            _serciveHost.AddServiceEndpoint(typeof(WCFCalcLib.ICalcService), httpb, httpAddress);
            _serciveHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            NetTcpBinding tcpb = new NetTcpBinding();
            _serciveHost.AddServiceEndpoint(typeof(WCFCalcLib.ICalcService), tcpb, tcpAddress);
            _serciveHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

            _serciveHost.Open();
        }

        protected override void OnStop()
        {
            if (_serciveHost != null)
            {
                _serciveHost.Close();
                _serciveHost = null;
            }
        }
    }
}
