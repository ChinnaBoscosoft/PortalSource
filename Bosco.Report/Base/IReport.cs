/*  Class Name      : IReport.cs
 *  Purpose         : Interface to ReportBase class object
 *  Author          : CS
 *  Created on      : 20-Jul-2009
 */

using System;
using System.Collections.Generic;

namespace Bosco.Report.Base
{
    public interface IReport
    {
        void ShowReport();
        void VoucherPrint(string VoucherId, string VoucherType, string ProjectName);
    }
}
