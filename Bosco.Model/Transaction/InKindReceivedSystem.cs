using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.Transaction
{
    public class InKindReceivedSystem : SystemBase
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public InKindReceivedSystem()
        {

        }
        public InKindReceivedSystem(int InKindId)
        {
            FillInKindReceivedProperties(InKindId);
        }
        #endregion

        #region Properties
        public int ProjectId { get; set; }
        public int InKindArticleId { get; set; }
        public int PurposeId { get; set; }
        public int DonorId { get; set; }
        public int InKindTransId { get; set; }
        public DateTime InKindTransDate { get; set; }
        public int SequenceNo { get; set; }
        public int LedgerId { get; set; }
        public decimal InKindQuantity { get; set; }
        public decimal InKindValue { get; set; }
        public string InKindUnit { get; set; }
        public string ContributionType { get; set; }
        public string ReceivedInformation { get; set; }
        public int BankAccountNo { get; set; }
        public string ChequeNo { get; set; }
        public string Narration { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchInKindReceived()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.InKindReceived.Fetch))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllInKindReceived()
        {
            using(DataManager dataManager=new DataManager(SQLCommand.InKindReceived.FetchAll))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.RECEIVED_INFORMATIONColumn,ReceivedInformation);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                resultArgs=dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveInKindReceived()
        {
            using (DataManager dataManager = new DataManager(InKindTransId == 0 ? SQLCommand.InKindReceived.Add : SQLCommand.InKindReceived.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.INKIND_TRANS_IDColumn, InKindTransId);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.INKIND_TRANS_DATEColumn, InKindTransDate);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.SEQUENCE_NOColumn, SequenceNo);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.ARTICLE_IDColumn, InKindArticleId);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.PURPOSE_IDColumn, PurposeId);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.CONTRIBUTION_TYPEColumn, ContributionType);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.INKIND_QUANTITYColumn, InKindQuantity);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.INKIND_UNITColumn, InKindUnit);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.INKIND_VALUEColumn, InKindValue);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.RECEIVED_INFORMATIONColumn, ReceivedInformation);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.DONOR_IDColumn, DonorId);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.BANK_ACCOUNT_NOColumn, BankAccountNo);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.CHEQUE_NOColumn, ChequeNo);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.NARRATIONColumn, Narration);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.CREATED_ONColumn, CreatedOn);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.CREATED_BYColumn, CreatedBy);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.MODIFIED_ONColumn, ModifiedOn);
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.MODIFIED_BYColumn, ModifiedBy);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteInKindTrans()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.InKindReceived.Delete))
            {
                dataManager.Parameters.Add(this.AppSchema.InKindTrans.INKIND_TRANS_IDColumn, InKindTransId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchInKindTransById(int InKindId)
        {
            using (DataManager dataManger = new DataManager(SQLCommand.InKindReceived.Fetch))
            {
                dataManger.Parameters.Add(this.AppSchema.InKindTrans.INKIND_TRANS_IDColumn, InKindId);
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        
        #endregion

        #region Private Methods
        private ResultArgs FillInKindReceivedProperties(int InKindID)
        {
            resultArgs = FetchInKindTransById(InKindID);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                InKindTransId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.INKIND_TRANS_IDColumn.ColumnName].ToString());
                InKindTransDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.INKIND_TRANS_DATEColumn.ColumnName].ToString(), false);
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.PROJECT_IDColumn.ColumnName].ToString());
                LedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.LEDGER_IDColumn.ColumnName].ToString());
                InKindArticleId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.ARTICLE_IDColumn.ColumnName].ToString());
                PurposeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.PURPOSE_IDColumn.ColumnName].ToString());
                DonorId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.DONOR_IDColumn.ColumnName].ToString());
                SequenceNo = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.SEQUENCE_NOColumn.ColumnName].ToString());
                ContributionType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.CONTRIBUTION_TYPEColumn.ColumnName].ToString();
                InKindQuantity = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.INKIND_QUANTITYColumn.ColumnName].ToString());
                InKindUnit = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.INKIND_UNITColumn.ColumnName].ToString();
                InKindValue = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.INKIND_VALUEColumn.ColumnName].ToString());
                ReceivedInformation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.RECEIVED_INFORMATIONColumn.ColumnName].ToString();
                BankAccountNo = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.BANK_ACCOUNT_NOColumn.ColumnName].ToString());
                ChequeNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.CHEQUE_NOColumn.ColumnName].ToString();
                Narration = resultArgs.DataSource.Table.Rows[0][this.AppSchema.InKindTrans.NARRATIONColumn.ColumnName].ToString();
            }
            return resultArgs;
        }
        #endregion

    }
}
