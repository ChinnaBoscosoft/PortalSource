using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace AcMESynch
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract (SessionMode=SessionMode.Allowed)]
    public interface IDataSynchronizer
    {

        [OperationContract]
        DataSet ExportERPMasters(string headOfficeCode,string branchOfficeCode);

        // TODO: Add your service operations here
    }
}
