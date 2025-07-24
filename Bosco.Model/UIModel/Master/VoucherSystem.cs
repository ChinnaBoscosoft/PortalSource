/************************************************************************************************************************
 *                                              Form Name  :VoucherSystem.cs (Class File)
 *                                              Purpose    :Business logics of the Voucher
 *                                              Author     : Carmel Raj M
 *                                              Created On :02-Sep-2013
 * 
 * **********************************************************************************************************************/
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using System.Collections;

namespace Bosco.Model.UIModel.Master
{
    public class VoucherSystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties

        public int VoucherId { get; set; }
        public int StartingNumber { get; set; }
        public int NumericalWith { get; set; }
        public int PrefixWithZero { get; set; }
        public int Allow_Duplicate { get; set; }
        public int Duration { get; set; }
        public string VoucherName { get; set; }
        public string PrefixCharacter { get; set; }
        public string SuffixCharacter { get; set; }
        public string Month { get; set; }
        public string VoucherType { get; set; }
        public string VoucherMethod { get; set; }
        public string Note { get; set; }

        #endregion

        #region Constructor

        public VoucherSystem()
        {
        }

        public VoucherSystem(int VoucherId)
        {
            this.VoucherId = VoucherId;
            FillBankProperties();
        }

        #endregion

        #region Methods

        public ResultArgs VoucherDetailsById()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.Voucher.FetchByVoucherId))
            {
                dataMember.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, this.VoucherId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucerDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteVoucherDetails()
        {
            using (DataManager dataMember = new DataManager(SQLCommand.Voucher.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveVocherDetails()
        {
            using (DataManager dataManager = new DataManager(VoucherId.Equals(0) ? SQLCommand.Voucher.Add : SQLCommand.Voucher.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_NAMEColumn, VoucherName);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, VoucherType);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_METHODColumn, VoucherMethod);
                dataManager.Parameters.Add(this.AppSchema.Voucher.PREFIX_CHARColumn, PrefixCharacter);
                dataManager.Parameters.Add(this.AppSchema.Voucher.SUFFIX_CHARColumn, SuffixCharacter);
                dataManager.Parameters.Add(this.AppSchema.Voucher.STARTING_NUMBERColumn, StartingNumber);
                dataManager.Parameters.Add(this.AppSchema.Voucher.NUMBERICAL_WITHColumn, NumericalWith);
                dataManager.Parameters.Add(this.AppSchema.Voucher.PREFIX_WITH_ZEROColumn, PrefixWithZero);
                dataManager.Parameters.Add(this.AppSchema.Voucher.MONTHColumn, Month);
                dataManager.Parameters.Add(this.AppSchema.Voucher.DURATIONColumn, Duration);
                dataManager.Parameters.Add(this.AppSchema.Voucher.ALLOW_DUPLICATEColumn, Allow_Duplicate);
                dataManager.Parameters.Add(this.AppSchema.Voucher.NOTEColumn, Note);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public void FillBankProperties()
        {
            resultArgs = VoucherDetailsById();
            DataTable dtVoucherEdit = resultArgs.DataSource.Table;
            if (dtVoucherEdit != null && dtVoucherEdit.Rows.Count > 0)
            {

                this.VoucherId = NumberSet.ToInteger(dtVoucherEdit.Rows[0][AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString());
                VoucherName = dtVoucherEdit.Rows[0][AppSchema.Voucher.VOUCHER_NAMEColumn.ColumnName].ToString();
                VoucherType = dtVoucherEdit.Rows[0][AppSchema.Voucher.VOUCHER_TYPEColumn.ColumnName].ToString();
                VoucherMethod = dtVoucherEdit.Rows[0][AppSchema.Voucher.VOUCHER_METHODColumn.ColumnName].ToString();
                PrefixCharacter = dtVoucherEdit.Rows[0][AppSchema.Voucher.PREFIX_CHARColumn.ColumnName].ToString();
                SuffixCharacter = dtVoucherEdit.Rows[0][AppSchema.Voucher.SUFFIX_CHARColumn.ColumnName].ToString();
                StartingNumber = NumberSet.ToInteger(dtVoucherEdit.Rows[0][AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString());
                NumericalWith = NumberSet.ToInteger(dtVoucherEdit.Rows[0][AppSchema.Voucher.NUMBERICAL_WITHColumn.ColumnName].ToString());
                PrefixWithZero = NumberSet.ToInteger(dtVoucherEdit.Rows[0][AppSchema.Voucher.PREFIX_WITH_ZEROColumn.ColumnName].ToString());
                Month = dtVoucherEdit.Rows[0][AppSchema.Voucher.MONTHColumn.ColumnName].ToString();
                Duration = NumberSet.ToInteger(dtVoucherEdit.Rows[0][AppSchema.Voucher.DURATIONColumn.ColumnName].ToString());
                Allow_Duplicate = NumberSet.ToInteger(dtVoucherEdit.Rows[0][AppSchema.Voucher.ALLOW_DUPLICATEColumn.ColumnName].ToString());
                Note = dtVoucherEdit.Rows[0][AppSchema.Voucher.NOTEColumn.ColumnName].ToString();
            }
        }

        #endregion
    }
}