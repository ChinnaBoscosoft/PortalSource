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
    public class DonorAuditorSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public DonorAuditorSystem()
        {
        }
        public DonorAuditorSystem(int DonAudId)
        {
            FillDonorAuditorProperties(DonAudId);
        }
        #endregion

        #region DonorAuditor Properties
        public int DonAudId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Place { get; set; }
        public string CompanyName { get; set; }
        public int CountryId { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int IdentityKey { get; set; }
        public string URL { get; set; }
        public int FcDonor { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string PAN { get; set; }
        public DataTable dtMapDonor { get; set; }
        public int MapDonorId { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchDonorDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchDonor))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAuditorDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchAuditor))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAuditorList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.FetchAuditorList))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteDonorAuditorDetails(int DonAudId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.DonorAuditor.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonAudId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveDonorAuditor()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                SaveDonorAuditorDetails(dataManager);
                //if (resultArgs.Success)
                //{
                //    MapDonorId = MapDonorId.Equals(0) ? this.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapDonorId;
                //    MappDonor(dataManager);
                //}
                dataManager.EndTransaction();
            }
            return resultArgs;
        }


        private ResultArgs SaveDonorAuditorDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager((DonAudId == 0) ? SQLCommand.DonorAuditor.Add : SQLCommand.DonorAuditor.Update))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonAudId, true);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NAMEColumn, Name);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.TYPEColumn, Type);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PLACEColumn, Place);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.COMPANY_NAMEColumn, CompanyName);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PHONEColumn, Phone);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.FAXColumn, Fax);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.EMAILColumn, Email);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.IDENTITYKEYColumn, IdentityKey);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.URLColumn, URL);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.STATEColumn, State);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.PANColumn, PAN);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MappDonor(DataManager dataManagers)
        {
            using (MappingSystem mappDonor = new MappingSystem())
            {
                mappDonor.DonorId = MapDonorId;
                mappDonor.dtDonorMapping = dtMapDonor;
                mappDonor.AccountMappingDonorByDonorId(dataManagers);
            }
            return resultArgs;
        }
        private void FillDonorAuditorProperties(int DonAudId)
        {
            resultArgs = FetchDonorAuditorDetailsById(DonAudId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.NAMEColumn.ColumnName].ToString();
                Type = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.TYPEColumn.ColumnName].ToString());
                Place = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PLACEColumn.ColumnName].ToString();
                CompanyName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.COMPANY_NAMEColumn.ColumnName].ToString();
                CountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.COUNTRY_IDColumn.ColumnName].ToString());
                Pincode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PINCODEColumn.ColumnName].ToString();
                Phone = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PHONEColumn.ColumnName].ToString();
                Email = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.EMAILColumn.ColumnName].ToString();
                IdentityKey = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.IDENTITYKEYColumn.ColumnName].ToString());
                Fax = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.FAXColumn.ColumnName].ToString();
                URL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.URLColumn.ColumnName].ToString();
                State = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.STATEColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.ADDRESSColumn.ColumnName].ToString();
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.NOTESColumn.ColumnName].ToString();
                PAN = resultArgs.DataSource.Table.Rows[0][this.AppSchema.DonorAuditor.PANColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchDonorAuditorDetailsById(int DonAudId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.DonorAuditor.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonAudId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
