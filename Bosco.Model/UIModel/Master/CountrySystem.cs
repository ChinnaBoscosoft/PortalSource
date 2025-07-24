/*  Class Name      : CountrySystem.cs
 *  Purpose         : To have all the logic of Country Details
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using System.Runtime.InteropServices;

namespace Bosco.Model.UIModel
{
    public class CountrySystem :SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor

        public CountrySystem()
        {
        }
        public CountrySystem(int CountryId)
        {
            FillCountryDetails(CountryId,DataBaseType.HeadOffice);
        }
        #endregion
       
        #region Country Properties
        public int CountryId {get;set;}
        public string CountryCode { get; set; }
        public string CountryName {get;set; }
        public new string CurrencyCode { get; set; }
        public string CurrencySymbol {get;set;}
        public string CurrencyName { get; set; }
        #endregion
        
        #region Methods
        public ResultArgs FetchCountryDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchAll, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCountryListDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCountryCodeListDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCountryCodeList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCurrencySymbolsListDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCurrencySymbolsList, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCurrencyCodeListDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCurrencyCodeList,connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchCurrencyNameListDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.FetchCurrencyNameList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs DeleteCountryDetails(int CountryId,DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.Delete, connectTo))
            {
                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveCountryDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((CountryId == 0) ? SQLCommand.Country.Add : SQLCommand.Country.Update, connectTo))
            {
                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(AppSchema.Country.COUNTRYColumn,CountryName);
                dataManager.Parameters.Add(AppSchema.Country.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(AppSchema.Country.CURRENCY_CODEColumn, CurrencyCode);
                dataManager.Parameters.Add(AppSchema.Country.CURRENCY_SYMBOLColumn, CurrencySymbol);
                dataManager.Parameters.Add(AppSchema.Country.CURRENCY_NAMEColumn, CurrencyName);
                resultArgs = dataManager.UpdateData();
              }
            return resultArgs;
        }

        private ResultArgs FillCountryDetails(int countryId,DataBaseType connectTo)
        {
            resultArgs = CountryDetailsbyId(countryId, connectTo);
            if (resultArgs.Success && resultArgs.DataSource.Table !=null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                CountryName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                CountryCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRY_CODEColumn.ColumnName].ToString();
                CurrencyCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.CURRENCY_CODEColumn.ColumnName].ToString();
                CurrencySymbol = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.CURRENCY_SYMBOLColumn.ColumnName].ToString();
                CurrencyName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.CURRENCY_NAMEColumn.ColumnName].ToString();
                CountryId =this.NumberSet.ToInteger( resultArgs.DataSource.Table.Rows[0][this.AppSchema.Country.COUNTRY_IDColumn.ColumnName].ToString());

            } 
            return resultArgs;
        }

        private ResultArgs CountryDetailsbyId(int CountryId,DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Country.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Country.COUNTRY_IDColumn, CountryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
