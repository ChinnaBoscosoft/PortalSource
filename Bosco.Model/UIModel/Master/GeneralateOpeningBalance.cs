using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.UIModel
{
    public class GeneralateOpeningBalance : SystemBase
    {
        ResultArgs resultArgs = new ResultArgs();
        public DataTable dtOpeningBalance { get; set; }
        public string dtYearFrom { get; set; }
        public GeneralateOpeningBalance()
        {

        }
        public ResultArgs FetchGeneralateAssetLiability(DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationOpeningBalance.FetchGeneralateAssetLiabilty, dbType))
            {
                dataManager.Parameters.Add(this.AppSchema.GeneralateOpeningBalance.ACC_YEAR_FROMColumn, this.DateSet.ToDate(dtYearFrom, false));
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// chinna 10.01.2019
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveOpeningDetails()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();

                resultArgs = SaveOpeningDetails(dataManager);

                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        /// <summary>
        /// Update the Approved Amount based on the Number (Ledger and sub Ledger)
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        public ResultArgs SaveOpeningDetails(DataManager dataManagers)
        {
            if (dtOpeningBalance != null)
            {
                resultArgs = DeleteGenOpeningBalancebyYear(dataManagers);
                if (resultArgs.Success)
                {
                    foreach (DataRow drItem in dtOpeningBalance.Rows)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.CongregationOpeningBalance.SaveGeneralateOpeningDetails, DataBaseType.HeadOffice))
                        {
                            dataManager.Database = dataManagers.Database;
                            if (drItem[this.AppSchema.GeneralateOpeningBalance.CON_LEDGER_IDColumn.ColumnName].ToString() != string.Empty)
                            {
                                if (!(NumberSet.ToDecimal(drItem["AMOUNT"].ToString()).Equals(0)))
                                {
                                    dataManager.Parameters.Add(this.AppSchema.GeneralateOpeningBalance.CON_LEDGER_IDColumn.ColumnName, NumberSet.ToInteger(drItem["CON_LEDGER_ID"].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GeneralateOpeningBalance.AMOUNTColumn, NumberSet.ToDecimal(drItem["AMOUNT"].ToString()));
                                    dataManager.Parameters.Add(this.AppSchema.GeneralateOpeningBalance.ACC_YEAR_FROMColumn, this.DateSet.ToDate(dtYearFrom, false));
                                    if ((drItem[this.AppSchema.GeneralateOpeningBalance.CON_LEDGER_NAMEColumn.ColumnName].ToString().Equals("Cash")))
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.GeneralateOpeningBalance.IS_CBColumn, this.NumberSet.ToInteger("1"));
                                    }
                                    else if (drItem[this.AppSchema.GeneralateOpeningBalance.CON_LEDGER_NAMEColumn.ColumnName].ToString().Equals("Bank accounts"))
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.GeneralateOpeningBalance.IS_CBColumn, this.NumberSet.ToInteger("2"));
                                    }
                                    else
                                    {
                                        dataManager.Parameters.Add(this.AppSchema.GeneralateOpeningBalance.IS_CBColumn, this.NumberSet.ToInteger("0"));
                                    }
                                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                    resultArgs = dataManager.UpdateData();
                                }
                            }
                        }
                        if (!resultArgs.Success) { break; }
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteGenOpeningBalancebyYear(DataManager dataManagers)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.CongregationOpeningBalance.DeleteGenOpeningBalance, DataBaseType.HeadOffice))
            {
                datamanager.Database = dataManagers.Database;
                datamanager.Parameters.Add(this.AppSchema.GeneralateOpeningBalance.ACC_YEAR_FROMColumn, this.DateSet.ToDate(dtYearFrom, false));
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }
    }
}
