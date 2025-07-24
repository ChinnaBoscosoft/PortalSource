using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.UIModel.Master
{
    public class GeneralateSystem : SystemBase
    {

        ResultArgs resultargs = null;

        public int GeneralateLedgerId { get; set; }
        public int GeneralateParentLedgerId { get; set; }
        public int MainParent { get; set; }
        public string GeneralateLedgerCode { get; set; }
        public string GeneralateLedgerName { get; set; }
        public string GeneralateLedgerIds { get; set; }

        public int GrouppedProjectCategoryLedgerId { get; set; }

        public GeneralateSystem(int LedgerId)
        {
            AssignLedgerValues(LedgerId, DataBaseType.HeadOffice);
        }

        public GeneralateSystem()
        {

        }

        public ResultArgs SaveGeneralateLedger(DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager((GeneralateLedgerId == 0) ? SQLCommand.CongregationLedger.Add : SQLCommand.CongregationLedger.Update, dbType))
            {
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn, GeneralateLedgerId, true);
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_LEDGER_CODEColumn, GeneralateLedgerCode);
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_LEDGER_NAMEColumn, GeneralateLedgerName);
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_PARENT_LEDGER_IDColumn, GeneralateParentLedgerId);
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_MAIN_PARENT_IDColumn, MainParent);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.UpdateData();
            }
            return resultargs;
        }

        public ResultArgs DeleteLedger(int LedgerId, DataBaseType dbType)
        {
            resultargs = DelteMappedGeneralateLedger(LedgerId, DataBaseType.HeadOffice);
            if (resultargs.Success)
            {
                resultargs = DelteGeneralateLedger(LedgerId, dbType);
            }
            return resultargs;
        }

        private ResultArgs DelteGeneralateLedger(int LedgerId, DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.Delete, dbType))
            {
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn, LedgerId);
                resultargs = dataManager.UpdateData();
            }
            return resultargs;
        }

        private ResultArgs DelteMappedGeneralateLedger(int LedgerId, DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.DeleteMappedLedgers, dbType))
            {
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn, LedgerId);
                resultargs = dataManager.UpdateData();
            }
            return resultargs;
        }

        private ResultArgs FetchGeneralateLedgerById(int LedgerId, DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchById, dbType))
            {
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn, LedgerId);
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchAllGeneralateLedgers(DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchAll, dbType))
            {
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }
        public ResultArgs FetchAllGeneralateParentLedgers(DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchAllParents, dbType))
            {
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchAllChildLedgers(DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchAllChildLedgers, dbType))
            {
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchmappedLedgers(DataBaseType dbType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchMappedLedgers, dbType))
            {
                dataManager.Parameters.Add(this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn, GeneralateLedgerId);
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchParentLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchParentLedgers, DataBaseType.HeadOffice))
            {
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchGroupedParentLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchGroupedParentLedgers, DataBaseType.HeadOffice))
            {
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchProjectCategoryDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchProjectCategoryLedgers, DataBaseType.HeadOffice))
            {
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }

        public ResultArgs FetchProjectCategoryByGroupedProjectCategory()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CongregationLedger.FetchProjectCategorybyGroupedProjectCategory, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogoryGroup.PROJECT_CATOGORY_GROUP_IDColumn, GrouppedProjectCategoryLedgerId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;

        }

        public void AssignLedgerValues(int LedgerId, DataBaseType connectTo)
        {
            if (LedgerId > 0)
            {
                resultargs = FetchGeneralateLedgerById(LedgerId, connectTo);
                if (resultargs.Success && resultargs.DataSource.Table.Rows.Count > 0)
                {
                    this.GeneralateLedgerId = NumberSet.ToInteger(resultargs.DataSource.Table.Rows[0][this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn.ColumnName].ToString());
                    this.GeneralateLedgerName = resultargs.DataSource.Table.Rows[0][this.AppSchema.CongregationLedger.CON_LEDGER_NAMEColumn.ColumnName].ToString();
                    this.GeneralateLedgerCode = resultargs.DataSource.Table.Rows[0][this.AppSchema.CongregationLedger.CON_LEDGER_CODEColumn.ColumnName].ToString();
                    this.GeneralateParentLedgerId = NumberSet.ToInteger(resultargs.DataSource.Table.Rows[0][this.AppSchema.CongregationLedger.CON_PARENT_LEDGER_IDColumn.ColumnName].ToString());
                }
            }
        }

        public ResultArgs GetLedgerList(DataBaseType dbType)
        {
            using (DataManager datamanager = new DataManager(SQLCommand.CongregationLedger.FetchLedgerList, dbType))
            {
                datamanager.Parameters.Add(this.AppSchema.CongregationLedger.CON_LEDGER_IDColumn, GeneralateLedgerIds);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }
    }
}
