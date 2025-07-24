using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using System.Data;

namespace Bosco.Model.UIModel
{
    public class HeadOfficeSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public HeadOfficeSystem()
        {
        }
        public HeadOfficeSystem(int HeadOfficeId, DataBaseType connectTo)
        {
            FillHeadOfficeDetails(HeadOfficeId, connectTo);
        }
        #endregion

        #region HeadOfficeProperties
        public int HeadOfficeId { get; set; }
        public string HeadOfficeCode { get; set; }
        public string BranchOfficeCode { get; set; }
        public string HeadOfficeName { get; set; }
        public int Type { get; set; }
        public string BelongsTo { get; set; }
        public string Designation { get; set; }
        public string Org_Mail_Id { get; set; }
        public string Incharge_Name { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int Status { get; set; }
        public string City { get; set; }
        public string HostName { get; set; }
        public string DBName { get; set; }
        public string DBUsername { get; set; }
        public string DBPassword { get; set; }
        public int Country_Id { get; set; }
        public int State_Id { get; set; }
        public string Pincode { get; set; }
        public string SR_Incharge_Name { get; set; }
        public string SR_Mobile_No { get; set; }
        public string SR_Phone_No { get; set; }
        public string SR_EmailId { get; set; }
        public string Mobile_No { get; set; }
        public string Phone_No { get; set; }
        public string CountryCode { get; set; }
        public int UserCreatedStatus { get; set; }
        public string HeadOfficeCodeUpdate { get; set; }
        public int AccountingPeriodType { get; set; }

        //Table Name:DataSync_Task
        public int BranchOfficeId { get; set; }
        public string xmlFileName { get; set; }
        public string ProjectName { get; set; }
        public string Location { get; set; }
        public string UploadedBy { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchHeadOfficeDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchAll, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchHeadOfficeToExport(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchAllToExport, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveHeadOfficeDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchActiveHeadOffice, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchHeadOfficeDBDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchDatabase))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchHeadOfficeToBeApproved(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchAllHeadOffice, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteHeadOfficeDetails(DataBaseType connectTo, string HeadOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs UpdateOfficeStatus(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.Status, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveHeadOfficeDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(((HeadOfficeId == 0) ? SQLCommand.HeadOffice.Add : SQLCommand.HeadOffice.Update), connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_IDColumn, HeadOfficeId);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn, HeadOfficeName);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.TYPE_IDColumn, Type);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.BELONGSTOColumn, BelongsTo);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.DESIGNATIONColumn, Designation);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.INCHARGE_NAMEColumn, Incharge_Name);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.ORG_MAIL_IDColumn, Org_Mail_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.MOBILE_NOColumn, Mobile_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.PHONE_NOColumn, Phone_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.COUNTRY_IDColumn, Country_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.STATE_IDColumn, State_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.CITYColumn, City);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_INCHARGE_NAMEColumn, SR_Incharge_Name);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_MOBILE_NOColumn, SR_Mobile_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_PHONE_NOColumn, SR_Phone_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_MAIL_IDColumn, SR_EmailId);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.MODIFIED_DATEColumn, ModifiedDate);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.MODIFIED_BYColumn, ModifiedBy);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HOST_NAMEColumn, HostName);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.DB_NAMEColumn, DBName);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.USERNAMEColumn, DBUsername);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.PASSWORDColumn, DBPassword);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.CREATED_DATEColumn, CreatedDate);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.CREATED_BYColumn, CreatedBy);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.CODEColumn, HeadOfficeCodeUpdate);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.ACCOUNTING_YEAR_TYPEColumn, AccountingPeriodType);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillHeadOfficeDetails(int HeadOfficeId, DataBaseType connectTo)
        {
            resultArgs = HeadOfficeDetailsById(HeadOfficeId, connectTo);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                HeadOfficeCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                HeadOfficeName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn.ColumnName].ToString();
                Type = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.TYPE_IDColumn.ColumnName].ToString());
                BelongsTo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.BELONGSTOColumn.ColumnName].ToString();
                Designation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.DESIGNATIONColumn.ColumnName].ToString();
                Incharge_Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.INCHARGE_NAMEColumn.ColumnName].ToString();
                Org_Mail_Id = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.ORG_MAIL_IDColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.ADDRESSColumn.ColumnName].ToString();
                Mobile_No = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.MOBILE_NOColumn.ColumnName].ToString();
                CountryCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.COUNTRY_CODEColumn.ColumnName].ToString();
                Phone_No = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.PHONE_NOColumn.ColumnName].ToString();
                Country_Id = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.COUNTRY_IDColumn.ColumnName].ToString());
                State_Id = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.STATE_IDColumn.ColumnName].ToString());
                City = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.CITYColumn.ColumnName].ToString();
                Pincode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.PINCODEColumn.ColumnName].ToString();
                SR_Incharge_Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.SR_INCHARGE_NAMEColumn.ColumnName].ToString();
                SR_Mobile_No = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.SR_MOBILE_NOColumn.ColumnName].ToString();
                SR_Phone_No = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.SR_PHONE_NOColumn.ColumnName].ToString();
                SR_EmailId = resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.SR_MAIL_IDColumn.ColumnName].ToString();
                UserCreatedStatus = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.USER_CREATED_STATUSColumn.ColumnName].ToString());
                Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.STATUSColumn.ColumnName].ToString());
                AccountingPeriodType = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.HeadOffice.ACCOUNTING_YEAR_TYPEColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs HeadOfficeDetailsById(int HeadOfficeId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_IDColumn.ColumnName, HeadOfficeId);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn, HeadOfficeName);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.TYPE_IDColumn, Type);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.BELONGSTOColumn, BelongsTo);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.DESIGNATIONColumn, Designation);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.INCHARGE_NAMEColumn, Incharge_Name);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.ORG_MAIL_IDColumn, Org_Mail_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.MOBILE_NOColumn, Mobile_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.PHONE_NOColumn, Phone_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.COUNTRY_IDColumn, Country_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.STATE_IDColumn, State_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.CITYColumn, City);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_INCHARGE_NAMEColumn, SR_Incharge_Name);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_MOBILE_NOColumn, SR_Incharge_Name);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_PHONE_NOColumn, SR_Phone_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_MAIL_IDColumn, SR_EmailId);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs HeadOfficeDetailsByCode(string headOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchByCode))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, headOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_NAMEColumn, HeadOfficeName);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.TYPE_IDColumn, Type);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.BELONGSTOColumn, BelongsTo);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.DESIGNATIONColumn, Designation);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.INCHARGE_NAMEColumn, Incharge_Name);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.ORG_MAIL_IDColumn, Org_Mail_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.MOBILE_NOColumn, Mobile_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.PHONE_NOColumn, Phone_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.COUNTRY_IDColumn, Country_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.STATE_IDColumn, State_Id);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.CITYColumn, City);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_INCHARGE_NAMEColumn, SR_Incharge_Name);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_MOBILE_NOColumn, SR_Incharge_Name);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_PHONE_NOColumn, SR_Phone_No);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.SR_MAIL_IDColumn, SR_EmailId);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchBranchByHeadOffice(string headOfficeCode)
        {
            return FetchBranchByHeadOffice(headOfficeCode, DataBaseType.Portal);
        }

        public ResultArgs FetchBranchByHeadOffice(string headOfficeCode, DataBaseType connectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchBranch, connectTo))
                {
                    dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, headOfficeCode);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                resultArgs = FetchBranchOfficeByUser(connectTo);
            }

            return resultArgs;
        }

        public ResultArgs FetchBranchOfficeByUser(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchBranchByUser, connectTo))
            {
                //fetch branch which are assigned to the particular branch user based on the login
                if (!string.IsNullOrEmpty(base.LoginUserHeadOfficeCode))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, base.LoginUserHeadOfficeCode);
                    if (!string.IsNullOrEmpty(base.LoginUserBranchOfficeCode))
                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode.ToString().ToLower());
                    if (base.LoginUserId > 0)
                    {
                        dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, base.LoginUserId);
                    }
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLoginUserHeadOfficeDetails(string headOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.LoginUserHeadOffice, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, headOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public int FetchAccountingYearType(string HeadOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchAccountingYearType, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        /// <summary>
        /// This is to fetch country details
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <returns></returns>
        public ResultArgs FetchCountry()
        {
            return FetchCountry(DataBaseType.Portal);
        }

        /// <summary>
        /// This is to load country details
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="connectTo"></param>
        /// <returns></returns>

        public ResultArgs FetchCountry(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchCountry, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        /// <summary>
        /// This is to fetch state details by countryid
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <returns></returns>
        public ResultArgs FetchStateByCountry()
        {
            return FetchStateByCountry(DataBaseType.Portal);
        }

        /// <summary>
        /// This is to fetch state details by countryid
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="connectTo"></param>
        /// <returns></returns>

        public ResultArgs FetchStateByCountry(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchState, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.COUNTRY_IDColumn, Country_Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        /// <summary>
        /// This is to fetch head office type
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <returns></returns>
        public ResultArgs FetchHeadOfficeType()
        {
            return FetchHeadOfficeType(DataBaseType.Portal);
        }

        /// <summary>
        /// This is to fetch head office type
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="connectTo"></param>
        /// <returns></returns>

        public ResultArgs FetchHeadOfficeType(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FecthType, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        /// <summary>
        /// This is to fetch head office admin details from portal data base inorder to move to head office data base during head office activation
        /// </summary>
        /// <param name="connectTo"></param>
        /// <returns></returns>
        public ResultArgs FetchHeadOfficeAdmin(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchHOAUser, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        /// <summary>
        /// This is to update the user created status in both database
        /// </summary>
        /// <param name="connectTo"></param>
        /// <returns></returns>
        public ResultArgs UpdateUserCreatedStatus(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.UpdateUserStatus, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        /// <summary>
        /// This is to update the database info in portal database
        /// </summary>
        /// <param name="connectTo"></param>
        /// <returns></returns>
        public ResultArgs UpdateDatabase()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.UpdateDB))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.DB_NAMEColumn, DBName);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HOST_NAMEColumn, HostName);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.USERNAMEColumn, DBUsername);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.PASSWORDColumn, DBPassword);
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public string FetchOfficeMailAddress()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchMailId))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data.ToString();
        }
        public string FetchHeadOfficeMailId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchHeadOfficeMailId))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data.ToString();
        }

        public ResultArgs FetchHeadOffice()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchHeadOfficeforCombo, DataBaseType.Portal))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string FetchBranchOfficeMailId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchBranchOfficeMailId))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Data.ToString();
        }

        public ResultArgs FetchMainContent()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchMainContent))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs ScheduleDataSynchroination()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.ScheduleDataSycnTask, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.Datasync_Task.HEAD_OFFICE_IDColumn, HeadOfficeId);
                dataManager.Parameters.Add(this.AppSchema.Datasync_Task.BRANCH_OFFICE_IDColumn, BranchOfficeId);
                dataManager.Parameters.Add(this.AppSchema.Datasync_Task.STATUSColumn, (int)DataSyncStatus.Received);
                dataManager.Parameters.Add(this.AppSchema.Datasync_Task.REMARKSColumn, "File Scheduled for DataSync");
                dataManager.Parameters.Add(this.AppSchema.Datasync_Task.XML_FILENAMEColumn, xmlFileName);
                dataManager.Parameters.Add(this.AppSchema.Datasync_Task.LOCATIONColumn, Location);
                dataManager.Parameters.Add(this.AppSchema.Datasync_Task.UPLOADED_BYColumn, UploadedBy);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveOfficeInfo()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.GetActiveOfficeInfo, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;

        }

        public ResultArgs FetchDataSyncStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchDataSyncStatus, DataBaseType.Portal))
            {
                if (!string.IsNullOrEmpty(HeadOfficeCode))
                {
                    dataManager.Parameters.Add(AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                }
                if (!string.IsNullOrEmpty(BranchOfficeCode))
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDatasyncMessage()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchDataSyncMessage, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
