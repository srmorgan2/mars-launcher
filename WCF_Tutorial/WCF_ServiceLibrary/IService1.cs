using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_ServiceLibrary
{
    
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetMessage(int value);

        [OperationContract]
        CompositeType Run(CompositeType composite);
    }

    [DataContract]
    public class CompositeType
    {
        int resultCode = 0;
        string errorMessage = "";
        DataSet data;
        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }

            set
            {
                errorMessage = value;
            }
        }
        [DataMember]
        public DataSet Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }
        [DataMember]
        public int ResultCode
        {
            get
            {
                return resultCode;
            }

            set
            {
                resultCode = value;
            }
        }
    }
}
