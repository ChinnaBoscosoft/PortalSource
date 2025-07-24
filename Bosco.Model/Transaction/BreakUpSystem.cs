using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using System.Collections;
using System;
using Bosco.Model.UIModel;
using System.Threading;

namespace Bosco.Model.Transaction
{
    public class BreakUpSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties
        public string AccounNumber { get; set; }
        public string FDNo { get; set; }
        public DateTime InvestedDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InterestAmount { get; set; }
        public int BankAccountId { get; set; }
        public int Status { get; set; }
        public string TranMode { get; set; }
        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }
        public int PeriodDay { get; set; }
        #endregion

        #region Construtor
        public BreakUpSystem()
        {
        }
        #endregion

        #region Public Methods

        public ResultArgs FetchBreakUpDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.FixedDeposit.BreakUpFetchByAccountNo))
            {
                dataMember.Parameters.Add(this.AppSchema.FDRegisters.ACCOUNT_NOColumn, AccounNumber);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs SaveBreakUp(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.BreakUpAdd))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(AppSchema.FDRegisters.ACCOUNT_NOColumn, AccounNumber);
                dataManager.Parameters.Add(AppSchema.BreakUp.FD_NOColumn, FDNo);
                dataManager.Parameters.Add(AppSchema.BreakUp.INVESTED_ONColumn, InvestedDate);
                dataManager.Parameters.Add(AppSchema.BreakUp.MATURITY_DATEColumn, MaturityDate);
                dataManager.Parameters.Add(AppSchema.BreakUp.AMOUNTColumn, Amount);
                dataManager.Parameters.Add(AppSchema.BreakUp.INTEREST_RATEColumn, InterestRate);
                dataManager.Parameters.Add(AppSchema.BreakUp.INTEREST_AMOUNTColumn, InterestAmount);
                dataManager.Parameters.Add(AppSchema.FDRegisters.BANK_ACCOUNT_IDColumn, BankAccountId);
                dataManager.Parameters.Add(AppSchema.FDRegisters.STATUSColumn, Status);
                dataManager.Parameters.Add(AppSchema.FDRegisters.TRANS_MODEColumn, TranMode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        //public ResultArgs GetBreakUpDetails(DataTable dtBreakUpDetails)
        //{
        //    using (DataManager dataManager = new DataManager())
        //    {
        //        dataManager.BeginTransaction();
        //        resultArgs = DeleteBreakUp(AccounNumber);
        //        if (resultArgs.Success && dtBreakUpDetails != null && dtBreakUpDetails.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dtBreakUpDetails.Rows)
        //            {
        //                this.AccounNumber = AccounNumber;
        //                FDNo = dr[AppSchema.BreakUp.FD_NOColumn.ColumnName].ToString();
        //                InvestedDate = (DateTime)dr[AppSchema.BreakUp.INVESTED_ONColumn.ColumnName];
        //                MaturityDate = (DateTime)dr[AppSchema.BreakUp.MATURITY_DATEColumn.ColumnName];
        //                Amount = this.NumberSet.ToDecimal(dr[AppSchema.BreakUp.AMOUNTColumn.ColumnName].ToString());
        //                InterestRate = this.NumberSet.ToDecimal(dr[AppSchema.BreakUp.INTEREST_RATEColumn.ColumnName].ToString());
        //                InterestAmount = this.NumberSet.ToDecimal(dr[AppSchema.BreakUp.INTEREST_AMOUNTColumn.ColumnName].ToString());
        //                resultArgs = SaveBreakUp();
        //            }
        //        }
        //        dataManager.EndTransaction();
        //    }
        //    return resultArgs;

        //}

        public ResultArgs UpdateBreakUpDetails(DataTable dtFDHistory, DataManager dataManagers, BankAccountSystem FDUpdation)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                if (dtFDHistory != null)
                {
                    if (dtFDHistory.Columns.Contains("AccountNo"))
                    {
                        DataTable dtAccountNo = dtFDHistory.DefaultView.ToTable(true, new string[] { "AccountNo" });
                        foreach (DataRow drDel in dtAccountNo.Rows)
                        {
                            if (!string.IsNullOrEmpty(drDel["AccountNo"].ToString()))
                                resultArgs = DeleteBreakUp(dataManager, drDel["AccountNo"].ToString());
                            else
                                break;
                        }
                    }
                    foreach (DataRow dr in dtFDHistory.Rows)
                    {
                        if (resultArgs.Success && dtFDHistory != null && dtFDHistory.Rows.Count > 0)
                        {
                            this.AccounNumber = dr["AccountNo"].ToString();
                            FDNo = dr[AppSchema.BreakUp.FD_NOColumn.ColumnName].ToString();
                            InvestedDate = (DateTime)dr[AppSchema.BreakUp.INVESTED_ONColumn.ColumnName];
                            MaturityDate = (DateTime)dr[AppSchema.BreakUp.MATURITY_DATEColumn.ColumnName];
                            Amount = this.NumberSet.ToDecimal(dr[AppSchema.BreakUp.AMOUNTColumn.ColumnName].ToString());
                            InterestRate = this.NumberSet.ToDecimal(dr[AppSchema.BreakUp.INTEREST_RATEColumn.ColumnName].ToString());
                            InterestAmount = this.NumberSet.ToDecimal(dr[AppSchema.BreakUp.INTEREST_AMOUNTColumn.ColumnName].ToString());
                            BankAccountId = NumberSet.ToInteger(dr["BankAccountId"].ToString());
                            Status = (int)YesNo.No;
                            TranMode = "OP";
                            resultArgs = SaveBreakUp(dataManagers);
                            if (!resultArgs.Success)
                                break;
                        }
                    }

                }
            }
            return resultArgs;

        }
        private ResultArgs DeleteBreakUp(DataManager dataManagers, string FDAccountId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.FixedDeposit.BreakUpDelete))
            {
                dataManager.Parameters.Add(this.AppSchema.BreakUp.ACCOUNT_NUMBERColumn, FDAccountId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
