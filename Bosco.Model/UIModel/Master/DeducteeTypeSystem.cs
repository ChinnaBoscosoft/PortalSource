using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel.Master;

namespace Bosco.Model.UIModel
{
    public class DeducteeTypeSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion
        #region Constructor
        public DeducteeTypeSystem()
        { 
        
        }
        public DeducteeTypeSystem(int DeducteeTypeId)
        {
          
        }
        #endregion

        #region Methods

        public ResultArgs FetchActiveDeductTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DeducteeType.FetchActiveDeductTypes,DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs NOP()
        {
            using (DataManager tax = new DataManager(SQLCommand.NatureofPayments.FetchNatureofPayments,DataBaseType.HeadOffice))
            {
                resultArgs = tax.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
