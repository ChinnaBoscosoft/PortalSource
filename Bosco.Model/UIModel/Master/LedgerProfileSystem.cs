using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel.Master
{
    public class LedgerProfileSystem : SystemBase
    {
        #region Properties

        int DefaultValue = 0;
        public ResultArgs resultArgs = null;

        public int LedgerID { get; set; }
        public int LedgerGroupID { get; set; }
        public int CreditorsProfileId { get; set; }
        public int NatureofPaymentid { get; set; }
        public int DeducteeTypeId { get; set; }
        public string ProfileLedgerName { get; set; }
        public string Mail { get; set; }
        public string ProfileAddress { get; set; }
        public string PANNo { get; set; }
        public int ProfileState { get; set; }
        public int ProfileCountry { get; set; }
        public string ProfilePinCode { get; set; }
        public string MobileNumber { get; set; }
        public DataTable dtLedgerProfile { get; set; }
        #endregion
        #region Constructor

        public LedgerProfileSystem()
        {

        }
        public LedgerProfileSystem(int LedgerId)
        {
               FillLedgerProfileDetails(LedgerId,DataBaseType.HeadOffice);
        }

        #endregion
        #region Methods

        public ResultArgs SaveLedgeProfile(DataManager dataManagerLedgerProfile,DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((CreditorsProfileId == 0) ? SQLCommand.LedgerProfile.Add : SQLCommand.LedgerProfile.Update, connectTo))
            {
                dataManager.Database = dataManagerLedgerProfile.Database;
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.CREDITORS_PROFILE_IDColumn, CreditorsProfileId);
                if(LedgerGroupID.Equals(26))
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfile.DEDUTEE_TYPE_IDColumn, NatureofPaymentid);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfile.NATURE_OF_PAYMENT_IDColumn, DefaultValue);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfile.NATURE_OF_PAYMENT_IDColumn, NatureofPaymentid);
                    dataManager.Parameters.Add(this.AppSchema.LedgerProfile.DEDUTEE_TYPE_IDColumn, DefaultValue);
                }
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.LEDGER_IDColumn, LedgerID);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.NAMEColumn, ProfileLedgerName);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.ADDRESSColumn, ProfileAddress);
                dataManager.Parameters.Add(this.AppSchema.State.STATE_IDColumn, ProfileState);
                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, ProfileCountry);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.PAN_NUMBERColumn, PANNo);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.PIN_CODEColumn, ProfilePinCode);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.CONTACT_NUMBERColumn, MobileNumber);
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.EMAILColumn, Email);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteLedgerProfile(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerProfile.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.LEDGER_IDColumn, LedgerID);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private void FillLedgerProfileDetails(int LedgerID,DataBaseType connectTo)
        {
            resultArgs = FetchLedgerProfile(LedgerID,connectTo);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                LedgerID = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.LEDGER_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.LEDGER_IDColumn.ColumnName].ToString()) : 0;
                CreditorsProfileId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.CREDITORS_PROFILE_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.CREDITORS_PROFILE_IDColumn.ColumnName].ToString()) : 0;
                ProfileLedgerName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.NAMEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.NAMEColumn.ColumnName].ToString() : string.Empty;
                Mail = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.EMAILColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.EMAILColumn.ColumnName].ToString() : string.Empty;
                ProfileAddress = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.ADDRESSColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.ADDRESSColumn.ColumnName].ToString() : string.Empty;
                ProfileState = resultArgs.DataSource.Table.Rows[0][this.AppSchema.State.STATE_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.State.STATE_IDColumn.ColumnName].ToString()) : 0;
                ProfileCountry = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRY_IDColumn.ColumnName] != DBNull.Value ? this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRY_IDColumn.ColumnName].ToString()) : 0;
                PinCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.PIN_CODEColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.PIN_CODEColumn.ColumnName].ToString() : string.Empty;
                PANNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.PAN_NUMBERColumn.ColumnName] != DBNull.Value ? resultArgs.DataSource.Table.Rows[0][this.AppSchema.LedgerProfile.PAN_NUMBERColumn.ColumnName].ToString() : string.Empty;
            }
        }

        public ResultArgs FetchLedgerProfile(int LedgerId,DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerProfile.Fetch,connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.LedgerProfile.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
