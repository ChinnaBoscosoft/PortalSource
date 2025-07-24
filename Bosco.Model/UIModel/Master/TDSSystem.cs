using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.UIModel.Master
{
    public class TDSSystem : SystemBase
    {
        ResultArgs resultArgs = null;
        public ResultArgs FetchDutyTaxTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDS.FetchDutyTax,DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDeducteeTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDS.FetchDeducteeTypes, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchTDSSectionDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TDS.FetchTDSSection, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchNatureofPaymentsSections()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.TDS.FetchNatureofPaymentsSection, DataBaseType.HeadOffice))
            {
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

    }
}
