using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.UIModel.Master
{
    public class AddressBookSystem :SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public AddressBookSystem()
        {
        }
        public AddressBookSystem(int AddressId)
        {
            FillAddressBookProperties(AddressId);
        }
        #endregion

        #region AddressBook Properties
        public int AddressId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Category { get; set; }
        public string Place { get; set; }
        public int CountryId { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public int FcDonor { get; set; }
        public string State { get; set; }
        public int IdentityKey { get; set; }
        public string Address { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchDonorDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AddressBook.FetchDonor))
            {
                 resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
       
        public ResultArgs FetchAuditorDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AddressBook.FetchAuditor))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchOtherDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AddressBook.FetchOthers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AddressBook.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
       
        public ResultArgs DeleteAddressDetails(int AddressId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.AddressBook.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.AddressBook.DONAUD_IDColumn, AddressId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveAddressBook()
        {
            using (DataManager dataManager = new DataManager((AddressId == 0) ? SQLCommand.AddressBook.Add : SQLCommand.AddressBook.Update))
            {
                dataManager.Parameters.Add(this.AppSchema.AddressBook.DONAUD_IDColumn,AddressId);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.NAMEColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.TYPEColumn, Type);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.PLACEColumn, Place);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.PHONEColumn, Phone);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.FAXColumn, Fax);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.EMAILColumn,Email);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.IDENTITYKEYColumn, IdentityKey);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.URLColumn,URL);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.FCDONORColumn, FcDonor);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.STATEColumn, State);
                dataManager.Parameters.Add(this.AppSchema.AddressBook.ADDRESSColumn,Address);
                resultArgs = dataManager.UpdateData(); 
            }
            return resultArgs;
        }

        private void FillAddressBookProperties(int AddressId)
        {
            resultArgs = FetchAddressBookDetailsById(AddressId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.NAMEColumn.ColumnName].ToString();
                Type =this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.TYPEColumn.ColumnName].ToString());
                Place = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.PLACEColumn.ColumnName].ToString();
                CountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.COUNTRY_IDColumn.ColumnName].ToString());
                Pincode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.PINCODEColumn.ColumnName].ToString();
                Phone = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.PHONEColumn.ColumnName].ToString();
                Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.EMAILColumn.ColumnName].ToString();
                Fax = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.FAXColumn.ColumnName].ToString();
                URL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.URLColumn.ColumnName].ToString();
                IdentityKey=this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.IDENTITYKEYColumn.ColumnName].ToString());
                FcDonor = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.FCDONORColumn.ColumnName].ToString());
                State = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.STATEColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AddressBook.ADDRESSColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchAddressBookDetailsById(int AddressId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AddressBook.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.AddressBook.DONAUD_IDColumn,AddressId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
