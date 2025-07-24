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
    public class NumberSystem : SystemBase
    {
        ResultArgs resultArgs = null;
       
      

        #region Properties
        private int NumberId { get; set; }
        private int VoucherNumberFormatId { get; set; }
        private string VoucherLastNumber { get; set; }
        private int VoucherRunningNumber { get; set; }
        private string VoucherNumberFormat { get; set; }
        private string NumericalWidth { get; set; }
        private int Prefilwitzero { get; set; }
        #endregion 
        
        public string getNewNumber(DataManager dataManage,NumberFormat numberFormatId, string projectId, int TransType)//,
        {
            string numbFormat = "";
            string formatedValue = "";
            string returnValue = "";
            string Prifix = "";
            string Sufix = "";
            int StartingNumber = 0;
            int LastRunningDigit = 0;
            DataView dvNoFormat = new DataView();

            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchVoucherNumberFormat))
            {
                dataManager.Database = dataManage.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectId); //Project Id
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, TransType);//Voucher Type
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            //return resultArgs;


            if (!resultArgs.Success | resultArgs.RowsAffected < 1)
                return returnValue;
            dvNoFormat = ((DataView)resultArgs.DataSource.TableView);
            if (dvNoFormat[0][this.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString() == "")
                return returnValue;
            Prifix = dvNoFormat[0][this.AppSchema.Voucher.PREFIX_CHARColumn.ColumnName].ToString();
            Sufix = dvNoFormat[0][this.AppSchema.Voucher.SUFFIX_CHARColumn.ColumnName].ToString();
            StartingNumber = this.NumberSet.ToInteger( dvNoFormat[0][this.AppSchema.Voucher.STARTING_NUMBERColumn.ColumnName].ToString());
            Prefilwitzero = this.NumberSet.ToInteger(dvNoFormat[0][this.AppSchema.Voucher.PREFIX_WITH_ZEROColumn.ColumnName].ToString());
            NumericalWidth= dvNoFormat[0][this.AppSchema.Voucher.NUMBERICAL_WITHColumn.ColumnName].ToString();
            numbFormat = (Prifix + Sufix).ToString();
           
            NumberId = (int)NumberFormat.VoucherNumber;
            // Fetch Existing Number Format
            DataView dvNumbFormat = FetchVoucherNumber(dataManage,NumberId.ToString());
            for (int i = 0; i <= dvNumbFormat.Count - 1; i++)
            {
                VoucherNumberFormat = dvNumbFormat[i][this.AppSchema.VoucherNumber.NUMBER_FORMATColumn.ColumnName].ToString();
                if (numbFormat == VoucherNumberFormat)
                {
                    VoucherLastNumber = dvNumbFormat[i][this.AppSchema.VoucherNumber.LAST_VOUCHER_NUMBERColumn.ColumnName].ToString();
                    LastRunningDigit = this.NumberSet.ToInteger(dvNumbFormat[i][this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn.ColumnName].ToString());
                    if (LastRunningDigit >= StartingNumber)
                    {
                        LastRunningDigit = LastRunningDigit + 1;
                    }
                    else
                    {
                        LastRunningDigit = StartingNumber;
                    }
                   // LastRunningDigit = (LastRunningDigit < StartingNumber) ? LastRunningDigit + 1 : (LastRunningDigit == StartingNumber) ? StartingNumber + 1 : StartingNumber;
                    VoucherRunningNumber = LastRunningDigit;
                    formatedValue = VoucherLastNumber = (Prefilwitzero == (int)YesNo.No) ? (Prifix + LastRunningDigit.ToString().PadLeft(int.Parse(NumericalWidth), '0') + Sufix).ToString() : (Prifix + LastRunningDigit + Sufix).ToString();
                    NumberId = this.NumberSet.ToInteger(dvNumbFormat[i][this.AppSchema.VoucherNumber.NUMBER_IDColumn.ColumnName].ToString());
                    VoucherNumberFormatId = this.NumberSet.ToInteger(dvNumbFormat[i][this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn.ColumnName].ToString());
                    updateLastVoucherNumber(dataManage);
                }
            }
            if (string.IsNullOrEmpty(formatedValue))
            {
                formatedValue = (Prefilwitzero == (int)YesNo.No) ? (Prifix + StartingNumber.ToString().PadLeft(int.Parse(NumericalWidth), '0') + Sufix).ToString() : (Prifix + StartingNumber + Sufix).ToString();
                VoucherNumberFormat = (Prifix + Sufix).ToString();
                VoucherRunningNumber = StartingNumber;
                VoucherNumberFormatId = NumberId;
                VoucherLastNumber = formatedValue;
                InsertVoucherNumber(dataManage);
            }
            return formatedValue;
        }

        private ResultArgs updateLastVoucherNumber(DataManager dataManage)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.UpdateLastVoucherNumber))
            {
                dataManager.Database = dataManage.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_IDColumn, NumberId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn, VoucherNumberFormatId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.LAST_VOUCHER_NUMBERColumn, VoucherLastNumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn, VoucherRunningNumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMATColumn, VoucherNumberFormat);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs InsertVoucherNumber(DataManager dataManage)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.InsertVoucherNumber))
            {
                dataManager.Database = dataManage.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn, VoucherNumberFormatId);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.LAST_VOUCHER_NUMBERColumn, VoucherLastNumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.RUNNING_NUMBERColumn, VoucherRunningNumber);
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMATColumn, VoucherNumberFormat);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private DataView FetchVoucherNumber(DataManager dataManage, string VoucherNumberFormatId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Voucher.FetchVoucherNumberFormatExist))
            {
                dataManager.Database = dataManage.Database;
                dataManager.Parameters.Add(this.AppSchema.VoucherNumber.NUMBER_FORMAT_IDColumn, VoucherNumberFormatId);
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs.DataSource.TableView;
        }

    }
    public static class SequenceNumber
    {
        private static int SerialNumber = 1;
        public static int GetSequenceNumber()
        {
            return SerialNumber++;
        }
        public static void ReSetSequenceNumber()
        {
            SerialNumber = 1;
        }
    }
}
