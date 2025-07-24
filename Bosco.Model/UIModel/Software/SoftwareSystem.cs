using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.UIModel
{
    public  class SoftwareSystem:SystemBase
    {
         #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion
        #region Constructor
        public SoftwareSystem()
        {

        }
        public SoftwareSystem(int VersionId)
        {
            FillSoftwareDetails(VersionId);
        }
        #endregion

        #region SoftwareProperties
        public int VersionId { get; set; }
        public DateTime DATE_OF_RELEASE { get; set; }
        public DateTime DATE_OF_UPLOAD { get; set; }
        public string DESC_OF_RELEASE { get; set; }
        public string TITLE { get; set; }
        public string RELEASE_VERSION { get; set; }
        public int UPLOAD_TYPE { get; set; }//0-Build or Setup 1- Prerequisite for AcME ERP
        public string BUILDFILE_ACTUAL { get; set; }
        public string BUILDFILE_PHYSICAL { get; set; }
        public string RELEASENOTES_ACTUAL { get; set; }
        public string RELEASENOTES_PHYSICAL { get; set; }
        public string CONTENT_TYPE { get; set; }
        public string FILESIZE { get; set; }
        public string RELEASETIME { get; set; }
        
        #endregion

        #region Methods
        public ResultArgs FetchSoftwareDetails()
        {
            using(DataManager dataManager=new DataManager(SQLCommand.Software.FetchAll,DataBaseType.Portal))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchSoftwareDetailsByType()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Software.FetchByType, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.Software.UPLOAD_TYPEColumn, UPLOAD_TYPE);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSoftwareDetailsByCurrentMonth()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Software.FetchByCurrentMonth, DataBaseType.Portal))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteSoftwareDetails(int VersoinId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Software.Delete, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.Software.VERSION_IDColumn.ColumnName, VersoinId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveSoftwareDetails()
        {
            using (DataManager dataManager = new DataManager((VersionId == 0) ? SQLCommand.Software.Add : SQLCommand.Software.Update, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.Software.VERSION_IDColumn.ColumnName, VersionId);
                dataManager.Parameters.Add(this.AppSchema.Software.DATE_OF_RELEASEColumn,DATE_OF_RELEASE);
                dataManager.Parameters.Add(this.AppSchema.Software.DATE_Of_UPLOADColumn, DateTime.Now);
                dataManager.Parameters.Add(this.AppSchema.Software.DESC_OF_RELEASEColumn, DESC_OF_RELEASE);
                dataManager.Parameters.Add(this.AppSchema.Software.TITLEColumn, TITLE);
                dataManager.Parameters.Add(this.AppSchema.Software.RELEASE_VERSIONColumn, RELEASE_VERSION);
                dataManager.Parameters.Add(this.AppSchema.Software.UPLOAD_TYPEColumn, UPLOAD_TYPE);
                dataManager.Parameters.Add(this.AppSchema.Software.BUILDFILE_ACTUALColumn, BUILDFILE_ACTUAL);
                dataManager.Parameters.Add(this.AppSchema.Software.BUILDFILE_PHYSICALColumn, BUILDFILE_PHYSICAL);
                dataManager.Parameters.Add(this.AppSchema.Software.RELEASENOTES_ACTUALColumn, RELEASENOTES_ACTUAL);
                dataManager.Parameters.Add(this.AppSchema.Software.RELEASENOTES_PHYSICALColumn, RELEASENOTES_PHYSICAL);
                dataManager.Parameters.Add(this.AppSchema.Software.CONTENT_TYPEColumn, CONTENT_TYPE);
                dataManager.Parameters.Add(this.AppSchema.Software.FILESIZEColumn, FILESIZE);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillSoftwareDetails(int VersionId)
        {
            resultArgs = SoftwareDetailsById(VersionId);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                DESC_OF_RELEASE = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.DESC_OF_RELEASEColumn.ColumnName].ToString();
                DATE_OF_UPLOAD = Convert.ToDateTime(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.DATE_Of_UPLOADColumn.ColumnName].ToString());
                DATE_OF_RELEASE = Convert.ToDateTime(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.DATE_OF_RELEASEColumn.ColumnName].ToString());
                UPLOAD_TYPE = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.UPLOAD_TYPEColumn.ColumnName].ToString());
                TITLE = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.TITLEColumn.ColumnName].ToString();
                BUILDFILE_ACTUAL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.BUILDFILE_ACTUALColumn.ColumnName].ToString();
                BUILDFILE_PHYSICAL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.BUILDFILE_PHYSICALColumn.ColumnName].ToString();
                RELEASENOTES_ACTUAL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.RELEASENOTES_ACTUALColumn.ColumnName].ToString();
                RELEASENOTES_PHYSICAL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.RELEASENOTES_PHYSICALColumn.ColumnName].ToString();
                RELEASE_VERSION = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.RELEASE_VERSIONColumn.ColumnName].ToString();
                CONTENT_TYPE = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.CONTENT_TYPEColumn.ColumnName].ToString();
                FILESIZE = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.FILESIZEColumn.ColumnName].ToString();
                RELEASETIME = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Software.RELEASETIMEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs SoftwareDetailsById(int VersionId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Software.Download, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.Software.VERSION_IDColumn.ColumnName, VersionId);
                dataManager.Parameters.Add(this.AppSchema.Software.DATE_OF_RELEASEColumn, DATE_OF_RELEASE);
                dataManager.Parameters.Add(this.AppSchema.Software.DATE_Of_UPLOADColumn, DateTime.Now);
                dataManager.Parameters.Add(this.AppSchema.Software.DESC_OF_RELEASEColumn, DESC_OF_RELEASE);
                dataManager.Parameters.Add(this.AppSchema.Software.TITLEColumn, TITLE);
                dataManager.Parameters.Add(this.AppSchema.Software.RELEASE_VERSIONColumn, RELEASE_VERSION);
                dataManager.Parameters.Add(this.AppSchema.Software.UPLOAD_TYPEColumn, UPLOAD_TYPE);
                dataManager.Parameters.Add(this.AppSchema.Software.BUILDFILE_ACTUALColumn, BUILDFILE_ACTUAL);
                dataManager.Parameters.Add(this.AppSchema.Software.BUILDFILE_PHYSICALColumn, BUILDFILE_PHYSICAL);
                dataManager.Parameters.Add(this.AppSchema.Software.RELEASENOTES_ACTUALColumn, RELEASENOTES_ACTUAL);
                dataManager.Parameters.Add(this.AppSchema.Software.RELEASENOTES_PHYSICALColumn, RELEASENOTES_PHYSICAL);
                dataManager.Parameters.Add(this.AppSchema.Software.CONTENT_TYPEColumn, CONTENT_TYPE);
                dataManager.Parameters.Add(this.AppSchema.Software.FILESIZEColumn, FILESIZE);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
