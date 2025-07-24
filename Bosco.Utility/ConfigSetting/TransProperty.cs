using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using System.Data;

namespace Bosco.Utility.ConfigSetting
{
    public class TransProperty : CommonMember, IDisposable
    {
        private static DataView dvTransInfo = null;
        private static DataView dvCashTransInfo = null;
        private static DataSet dsCostCentreInfo = null;
        private static DataTable dvFixedDepositInfo = null;
        private static DataTable dvFixedDepositInteretstInfo = null;

        /// <summary>
        /// Set Transaction Info as Dataview
        /// </summary>
        public DataView TransInfo
        {
            set
            {
                TransProperty.dvTransInfo = value;
            }
            get
            {
                return dvTransInfo;
            }
        }

        /// <summary>
        /// Set Transaction Info as Dataview
        /// </summary>
        public DataView CashTransInfo
        {
            set
            {
                TransProperty.dvCashTransInfo = value;
            }
            get
            {
                return dvCashTransInfo;
            }
        }

        public DataTable FixedDepositInfo
        {
            set
            {
                TransProperty.dvFixedDepositInfo = value;
            }
            get
            {
                return dvFixedDepositInfo;
            }
        }

        public DataTable FixedDepositInterestInfo
        {
            set
            {
                TransProperty.dvFixedDepositInteretstInfo = value;
            }
            get
            {
                return dvFixedDepositInteretstInfo;
            }
        }

        public DataView GetCostCentreByLedgerID(string LedgerID)
        {
            return dsCostCentreInfo.Tables[dsCostCentreInfo.Tables.IndexOf(LedgerID)].DefaultView;
        }

        public DataSet CostCenterInfo
        {
            set
            {
                dsCostCentreInfo = value;
            }
        }

        public bool HasCostCentre(string LedgerID)
        {
            return dsCostCentreInfo.Tables.Contains(LedgerID);
        }

        public virtual void Dispose()
        {
            GC.Collect();
        }
    }
}
