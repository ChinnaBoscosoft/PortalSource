using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;


namespace Bosco.Model.UIModel
{
    public class LedgerGroupSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public int GroupId { get; set; }
        public string Abbrevation { get; set; }
        public string Group { get; set; }
        public int ParentGroupId { get; set; }
        public int NatureId { get; set; }
        public int MainGroupId { get; set; }
        public string GroupIds { get; set; }
        public int ImageId { get; set; }
        public int SortOrder { get; set; }

        #endregion

        #region Constructor
        public LedgerGroupSystem()
        {

        }

        public LedgerGroupSystem(int LedgerGroupId)
        {
            FillGroupProperties(LedgerGroupId, DataBaseType.HeadOffice);
        }
        #endregion

        public ResultArgs SaveLedgerGroupDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((GroupId == 0) ? SQLCommand.LedgerGroup.Add : SQLCommand.LedgerGroup.Update, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_CODEColumn, Abbrevation);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, Group);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.NATURE_IDColumn, NatureId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn, MainGroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.IMAGE_IDColumn, ImageId);
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.SORT_ORDERColumn, SortOrder);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs GetLedgerGroupSource(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchAll, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs GetLedgerList(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerList, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }
        public ResultArgs LoadLedgerGroupSource(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchforLookup, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs LoadLedgerGroupforLedgerLoodkup(ledgerSubType ledgerType, DataBaseType connectTo)
        {
            if (ledgerType == ledgerSubType.GN)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchforLedgerLookup, connectTo))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchFDLedger))
                {
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }

            return resultArgs;
        }

        public ResultArgs GetLedgerGroupId(string LedgerGroup, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.GetGroupId, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, LedgerGroup);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetLedgerGroupByIdList(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchByGroupId, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.LEDGER_GROUPColumn, GroupIds);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs GetSubGroupByIdList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchSubgroupById))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs DeleteLedgerGroup(int LedgerGroupId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int GetNatureId(int LedgerGroupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchNatureId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs ValidateGroupId(int LedgerGroupId, DataBaseType connectTo)
        {
            ResultArgs resultArgs = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchValidateGroup, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public int GetAccessFlag(int LedgerGroupId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchAccessFlag, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }

            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs UpdateImageIndex(int GroupId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.UpdateImageIndex))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, GroupId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private void FillGroupProperties(int LedgerGroupId, DataBaseType connectTo)
        {
            resultArgs = GetLedgerGroupById(LedgerGroupId, connectTo);
            if (resultArgs.RowsAffected > 0)
            {
                Abbrevation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.GROUP_CODEColumn.ColumnName].ToString();
                Group = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ColumnName].ToString();
                ParentGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn.ColumnName].ToString());
                NatureId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.NATURE_IDColumn.ColumnName].ToString());
                MainGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.MAIN_GROUP_IDColumn.ColumnName].ToString());
                ImageId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerGroup.IMAGE_IDColumn.ColumnName].ToString());
            }
        }

        private ResultArgs GetLedgerGroupById(int LedgerGroupId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, LedgerGroupId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }
        public ResultArgs LoadFDLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchFDLedger))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs GetAccountType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchAccoutType))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }
        public ResultArgs FecthLedgerGroupCodes(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchLedgerGroupCodes, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs LedgerGroupFetchAll(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.LedgerGroupFetchAll, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string GetGroupName(int groupId)
        {
            ResultArgs result = new ResultArgs();

            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchGroupName, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, groupId);
                result = dataManager.FetchData(DataSource.Scalar);
            }

            return result.DataSource.Sclar.ToString;
        }
        public int FetchSortOrder()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchSortOrder, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.PARENT_GROUP_IDColumn, ParentGroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public int FetchMainGroupSortOrder()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerGroup.FetchMainGroupSortOrder, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerGroup.GROUP_IDColumn, ParentGroupId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

    }
}
