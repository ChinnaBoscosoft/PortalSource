using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;

namespace AcMeERP.DataSyncService
{
    /// <summary>
    /// this class file is used to return our own exception when Branch office communicates to Head office portal for master updation, license updation and Datasynchronization.
    /// </summary>
    [DataContract]
    public class AcMeServiceException
    {
        [DataMember]
        public string Message { get; set; }
    }
}