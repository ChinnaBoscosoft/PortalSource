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
    public class BankSystem : SystemBase
    {

        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public BankSystem()
        {
        }

        public BankSystem(int BankId)
        {
            FillBankProperties(BankId);
        }
        #endregion

        #region Bank Properties
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public string IFSCCode { get; set; }
        public string MICRCode { get; set; }
        public string ContactNumber { get; set; }
        public string AccountName { get; set; }
        public string SWIFTCode { get; set; }
        public string Notes { get; set; }
        #endregion

        #region Methods
        
        public ResultArgs FetchBankDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBankDetailsforLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchforLookup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchBankDetailsforSettings()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchSettingBankAccount))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteBankDetails(int BankId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.Bank.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.Bank.BANK_IDColumn, BankId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveBankDetails()
        {
            using (DataManager dataManager = new DataManager((BankId == 0) ? SQLCommand.Bank.Add : SQLCommand.Bank.Update))
            {
                dataManager.BeginTransaction();
                dataManager.Parameters.Add(this.AppSchema.Bank.BANK_CODEColumn, BankCode);
                dataManager.Parameters.Add(this.AppSchema.Bank.BANKColumn, BankName);
                dataManager.Parameters.Add(this.AppSchema.Bank.BRANCHColumn, Branch);
                dataManager.Parameters.Add(this.AppSchema.Bank.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.Bank.IFSCCODEColumn, IFSCCode);
                dataManager.Parameters.Add(this.AppSchema.Bank.MICRCODEColumn, MICRCode);
                dataManager.Parameters.Add(this.AppSchema.Bank.CONTACTNUMBERColumn, ContactNumber);
                dataManager.Parameters.Add(this.AppSchema.Bank.SWIFTCODEColumn, SWIFTCode);
                dataManager.Parameters.Add(this.AppSchema.Bank.BANK_IDColumn, BankId);
                dataManager.Parameters.Add(this.AppSchema.Bank.NOTESColumn,Notes);
                resultArgs = dataManager.UpdateData();
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public void FillBankProperties(int BankId)
        {
            resultArgs = FetchBankDetailsById(BankId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                BankCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.BANK_CODEColumn.ColumnName].ToString();
                BankName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.BANKColumn.ColumnName].ToString();
                Branch = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.BRANCHColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.ADDRESSColumn.ColumnName].ToString();
                IFSCCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.IFSCCODEColumn.ColumnName].ToString();
                MICRCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.MICRCODEColumn.ColumnName].ToString();
                ContactNumber = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.CONTACTNUMBERColumn.ColumnName].ToString();
                SWIFTCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.SWIFTCODEColumn.ColumnName].ToString();
                Notes= resultArgs.DataSource.Table.Rows[0][this.AppSchema.Bank.NOTESColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchBankDetailsById(int BankId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.Bank.BANK_IDColumn, BankId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataTable FetchBankByProjectId(string ProjectId,string BranchId)
        {
            DataTable dtBank = new DataTable();
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankByProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                if (!string.IsNullOrEmpty(BranchId) && BranchId != "0")
                {
                    dataManager.Parameters.Add("BRANCH_ID", BranchId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public DataTable FetchFDByProjectId(string ProjectId,string BranchId)
        {
            DataTable dtBank = new DataTable();
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchFDByProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerBalance.PROJECT_IDColumn, ProjectId);
                if (!string.IsNullOrEmpty(BranchId) && BranchId != "0")
                {
                    dataManager.Parameters.Add("BRANCH_ID", BranchId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            resultArgs.DataSource.Table.Columns.Add("SELECT", typeof(Int32));
            return resultArgs.DataSource.Table;
        }
        
       public ResultArgs FetchBankCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Bank.FetchBankCodes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                return resultArgs;
            }
        }
        #endregion
    }
}
