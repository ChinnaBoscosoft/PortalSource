using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.UIModel.Master
{
    public class LockVoucherSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public LockVoucherSystem()
        {
        }
        public LockVoucherSystem(int LockTransId, DataBaseType connectTo)
        {
            FillLockVoucherDetails(LockTransId, connectTo);
        }
        #endregion

        #region Lockvoucher Properties
        public int LockTransId { get; set; }
        public int BranchId { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Password { get; set; }
        public string Reasons { get; set; }
        public string PasswordHint { get; set; }
        public int LockByPortal { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchBranchByProject(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LockVoucher.FetchBranchByProject, connectTo))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLockedProjects(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LockVoucher.FetchLockVoucher, connectTo))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveLockVoucher(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((LockTransId == 0) ? SQLCommand.LockVoucher.Add : SQLCommand.LockVoucher.Update, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.LOCK_TRANS_IDColumn, LockTransId);
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.BRANCH_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.DATE_FROMColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.DATE_TOColumn, DateTo);
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.REASONColumn, Reasons);
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.PASSWORDColumn, Password);
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.PASSWORD_HINTColumn, PasswordHint);
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.LOCK_TYPEColumn, "Pre Audit");
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.LOCK_BY_PORTALColumn, LockByPortal);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteLockDetails(int LockId, DataBaseType connectTo)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LockVoucher.Delete, connectTo))
            {
                dataMember.Parameters.Add(this.AppSchema.LockVoucher.LOCK_TRANS_IDColumn, LockId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillLockVoucherDetails(int LockTransId, DataBaseType connectTo)
        {
            resultArgs = LockVoucherById(LockTransId, connectTo);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                LockTransId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.LOCK_TRANS_IDColumn.ColumnName].ToString());
                BranchId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.BRANCH_IDColumn.ColumnName].ToString());
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());

                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.DATE_FROMColumn.ColumnName] != DBNull.Value)
                {
                    DateFrom = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.DATE_FROMColumn.ColumnName].ToString(), false);
                }
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.DATE_TOColumn.ColumnName] != DBNull.Value)
                {
                    DateTo = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.DATE_TOColumn.ColumnName].ToString(), false);
                }
                Password = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.PASSWORDColumn.ColumnName].ToString();
                PasswordHint = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.PASSWORD_HINTColumn.ColumnName].ToString();
                Reasons = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.REASONColumn.ColumnName].ToString();
                LockByPortal = this.NumberSet.ToInteger(this.resultArgs.DataSource.Table.Rows[0][this.AppSchema.LockVoucher.LOCK_BY_PORTALColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs LockVoucherById(int LockId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LockVoucher.FetchLockVoucherById, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LockVoucher.LOCK_TRANS_IDColumn.ColumnName, LockId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchLockVoucherGraceDays()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LockVoucher.FetchBranchLockVoucherGraceDays, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchLockVoucherGraceDaysByBranchLocation(Int32 BranchId, Int32 LocationId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LockVoucher.FetchBranchLockVoucherGraceDaysByBranchLocation, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.BRANCH_IDColumn.ColumnName, BranchId);
                dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.LOCATION_IDColumn.ColumnName, LocationId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
