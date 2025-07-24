/* Page Name  : DataSynchronizer.svc.cs
 * Purpose    : Service for getting ERP Masters for each Branch Office
 * Created On : 27/06/2014
 * Created By : Chinna M
 * */
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Activation;


using Bosco.DAO;
using Bosco.Model.UIModel;
using Bosco.DAO.Data;
using Bosco.Utility;
using AcMeERP.Base;
using AcMEDSync.Model;
using System.Text;
using Bosco.Model.UIModel.TroubleTicket;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using Bosco.Model.Transaction;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Bosco.Model.UIModel.Master;


namespace AcMeERP.DataSyncService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class DataSynchronizer : IDataSynchronizer
    {
        #region Property
        private string acmeerpservicemessage = string.Empty;
        private string dataSetName = "MASTER_DATA";
        private const string SESSION_DB = "SESSION_DB";
        private const string HEAD_CODE = "HEAD_OFFICE_CODE";
        private const string BRANCH_CODE = "BRANCH_OFFICE_CODE";
        private const string KEY_GENERATED_DATE = "KEY_GENERATED_DATE";
        private const string HEAD_ID = "HEAD_OFFICE_ID";
        private const string BRANCH_ID = "BRANCH_OFFICE_ID";
        private const string PROJECT = "PROJECT";
        private const string LEDGER = "LEDGER_NAME";
        private const string BRANCH_PROJECT = "Project";
        private const string BRANCH_LEDGER = "Ledger";
        private const string headOfficeTableName = "Header";
        private const string VOUCHER_TABLENAME = "VoucherMasters";
        private const string FD_VOUCHER_TABLENAME = "FD_Voucher_Master_Trans";
        private IDatabase database = null;
        private string mailId = string.Empty;

        /// <summary>
        /// Sets the Datasync folder location
        /// </summary>
        private static string DataSyncLocation
        {
            get
            {
                string dataSyncLocation = string.Empty;
                if (ConfigurationManager.AppSettings["DataSyncLocation"] != null)
                {
                    dataSyncLocation = ConfigurationManager.AppSettings["DataSyncLocation"].ToString();
                }
                return dataSyncLocation;
            }

        }

        /// <summary>
        /// Setting the Database for Web service
        /// </summary>
        private IDatabase SessionDB
        {
            get
            {
                //get database for web app
                if (HttpContext.Current.Session != null && HttpContext.Current.Session[SESSION_DB] != null)
                {
                    database = HttpContext.Current.Session[SESSION_DB] as IDatabase;
                }

                return database;
            }
            set
            {
                //set database for web app
                HttpContext.Current.Session[SESSION_DB] = "Bosco.DAO.MySQL.MySQLDataHandler";
                if (value == null)
                {
                    HttpContext.Current.Session.Remove(SESSION_DB);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This methods gets the erp masters headoffice, branch office info, project, project category, financial year , Ledger group and Ledger 
        /// Queries are written in common Balance dll of the acme.erp desktop product. (need to udpate Balance and datasync dll from acme.erp )
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        private ResultArgs GetERPMasters(string headOfficeCode, string branchOfficeCode)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            DataSet dsMaster = new DataSet(dataSetName);
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                if (!(string.IsNullOrEmpty(headOfficeCode) && string.IsNullOrEmpty(branchOfficeCode)))
                {
                    objBase.HeadOfficeCode = headOfficeCode;//To Connect Head Office Database

                    // This is from AcMEDSyn and Bosoc.HOSQL DLLs(AcME ERP Product)
                    using (ExportMasters exportMasterSystem = new ExportMasters())
                    {
                        resultArgs = exportMasterSystem.GetMasters(branchOfficeCode);
                        if (resultArgs.Success && resultArgs.DataSource != null)
                        {
                            dsMaster = resultArgs.DataSource.TableSet;
                            if (dsMaster != null && dsMaster.Tables.Count > 0)
                            {
                                //Include Office Data
                                dsMaster.Tables.Add(OfficeData(headOfficeCode, branchOfficeCode));

                                //Fetch Sub Branch Details
                                using (BranchOfficeSystem branchSystem = new BranchOfficeSystem())
                                {
                                    branchSystem.BranchOfficeCode = branchOfficeCode;
                                    resultArgs = branchSystem.FetchSubBranchDetailsByBranchCode();

                                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                                    {
                                        dsMaster.Tables.Add(resultArgs.DataSource.Table);
                                    }
                                }
                                resultArgs.DataSource.Data = dsMaster;
                                resultArgs.Success = true;
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }

            return resultArgs;
        }

        /// <summary>
        /// This method generates the office info based on the headofficecode and branchofficecode passed if it is active.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        private DataTable OfficeData(string headOfficeCode, string branchOfficeCode)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            DataTable dtOfficeData = new DataTable(headOfficeTableName);
            if (!(string.IsNullOrEmpty(headOfficeCode) && string.IsNullOrEmpty(branchOfficeCode)))
            {
                dtOfficeData.Columns.Add(objBase.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName, (objBase.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.DataType));
                dtOfficeData.Columns.Add(objBase.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName, (objBase.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.DataType));
                DataRow drOfficeRow = dtOfficeData.NewRow();
                drOfficeRow[objBase.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName] = CommonMember.EncryptValue(headOfficeCode);
                drOfficeRow[objBase.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName] = CommonMember.EncryptValue(branchOfficeCode);
                dtOfficeData.Rows.Add(drOfficeRow);
            }
            return dtOfficeData;

        }

        /// <summary>
        /// This method fetches all the mismatched ledgers with branch office and headoffice ledgers for master data synchronization
        /// </summary>
        /// <param name="dtHeadOfficeLedgers"></param>
        /// <param name="dtBranchHeadOfficeLedgers"></param>
        /// <returns></returns>
        private DataTable FetchMisMatchedLedgers(DataTable dtHeadOfficeLedgers, DataTable dtBranchHeadOfficeLedgers)
        {
            DataTable dtLedgers = new DataTable();

            var matched = from table1 in dtBranchHeadOfficeLedgers.AsEnumerable()
                          join table2 in dtHeadOfficeLedgers.AsEnumerable() on table1.Field<string>(LEDGER) equals table2.Field<string>(LEDGER)
                          select table1;


            var missing = from table1 in dtBranchHeadOfficeLedgers.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtLedgers = missing.CopyToDataTable();
            }
            return dtLedgers;
        }
        /// <summary>
        /// This method fetches all the mismatched projects Branch office vs head office.
        /// </summary>
        /// <param name="dtHeadOfficeProjects"></param>
        /// <param name="dtBranchHeadOfficeProjects"></param>
        /// <returns></returns>
        private DataTable FetchMisMatchedProjects(DataTable dtHeadOfficeProjects, DataTable dtBranchHeadOfficeProjects)
        {
            DataTable dtLedgers = new DataTable();

            var matched = from table1 in dtBranchHeadOfficeProjects.AsEnumerable()
                          join table2 in dtHeadOfficeProjects.AsEnumerable() on table1.Field<string>(PROJECT) equals table2.Field<string>(PROJECT)
                          select table1;


            var missing = from table1 in dtBranchHeadOfficeProjects.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtLedgers = missing.CopyToDataTable();
            }
            return dtLedgers;
        }
        /// <summary>
        /// This method gets the Head office active projects for each branch office
        /// </summary>
        /// <param name="HeadOfficeCode"></param>
        /// <param name="BranchOfficeCode"></param>
        /// <returns></returns>
        private ResultArgs GetHeadOfficeProjects(string HeadOfficeCode, string BranchOfficeCode)
        {
            ResultArgs resultArgs = new ResultArgs();
            AcMeERP.Base.UIBase objBase = new UIBase();
            objBase.HeadOfficeCode = HeadOfficeCode;
            try
            {
                using (ProjectSystem projectSystem = new ProjectSystem())
                {
                    projectSystem.Branch_Office_Code = BranchOfficeCode;
                    resultArgs = projectSystem.ProjectsFetchAll(DataBaseType.HeadOffice);
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }
            return resultArgs;
        }


        /// <summary>
        /// This method is used to return project ids from portal db
        /// </summary>
        /// <param name="HeadOfficeCode"></param>
        /// <param name="BranchOfficeCode"></param>
        /// <param name="dtProjects"></param>
        /// <returns></returns>
        private ResultArgs GetHeadofficeProjectIdsByBOProject(string HeadOfficeCode, string BranchOfficeCode, DataTable dtProjects)
        {
            ResultArgs resultArgs = new ResultArgs();
            string HeadOfficeProjectIds = string.Empty;
            AcMeERP.Base.UIBase objBase = new UIBase();

            using (ProjectSystem projectsystem = new ProjectSystem())
            {
                foreach (DataRow drproject in dtProjects.Rows)
                {
                    string projectname = drproject[objBase.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                    resultArgs = projectsystem.FetchProjectIdByProjectName(projectname, DataBaseType.HeadOffice);
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtHOProjects = resultArgs.DataSource.Table;
                        HeadOfficeProjectIds += dtHOProjects.Rows[0][objBase.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString() + ",";
                    }
                    else
                    {
                        HeadOfficeProjectIds = string.Empty;
                        break;
                    }
                }
            }


            if (!string.IsNullOrEmpty(HeadOfficeProjectIds) && resultArgs.Success)
            {
                HeadOfficeProjectIds = HeadOfficeProjectIds.TrimEnd(',');
                resultArgs.ReturnValue = HeadOfficeProjectIds;
            }

            return resultArgs;
        }

        /// <summary>
        /// This method fetches the Head office mapped ledgers for the particular branch office.
        /// </summary>
        /// <param name="HeadOfficeCode"></param>
        /// <param name="BranchOfficeCode"></param>
        /// <returns></returns>
        private ResultArgs GetHeadOfficeLedgers(string HeadOfficeCode, string BranchOfficeCode)
        {
            ResultArgs resultArgs = new ResultArgs();
            AcMeERP.Base.UIBase objBase = new UIBase();
            objBase.HeadOfficeCode = HeadOfficeCode;
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    resultArgs = ledgerSystem.LedgerFetchAll(BranchOfficeCode, DataBaseType.HeadOffice);
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }
            return resultArgs;
        }

        /// <summary>
        /// This method gets the branch office ledgers
        /// </summary>
        /// <param name="dtBranchLedgers"></param>
        /// <returns></returns>
        private DataTable GetBranchOfficeLedgers(DataTable dtBranchLedgers)
        {
            DataTable dtBranchOfficeLedgers = null;
            if (dtBranchLedgers != null)
            {
                DataView dvLedgers = new DataView(dtBranchLedgers);
                dvLedgers.RowFilter = "GROUP_ID NOT IN(12,13,14)";
                dtBranchOfficeLedgers = dvLedgers.ToTable();
            }
            return dtBranchOfficeLedgers;
        }
        /// <summary>
        /// This method updates the voucher uploaded status in the datasync_status table, with its project, location and uploaded by detials
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="FileName"></param>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        private ResultArgs UpdateVoucherUploadStatus(string headOfficeCode, string branchOfficeCode, string FileName, string ProjectName, string Location, string UploadedBy)
        {
            //On 24/02/2018, update Projects, branch location and who is uploaded details in data suyn table
            ResultArgs resultArgs = new ResultArgs();
            int headOfficeId = 0; int branchOfficeId = 0;
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                headOfficeSystem.HeadOfficeCode = headOfficeCode;
                headOfficeSystem.BranchOfficeCode = branchOfficeCode;
                resultArgs = headOfficeSystem.FetchActiveOfficeInfo();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    new ErrorLog().WriteError("FetchActiveOfficeInfo Success");
                    headOfficeId = Convert.ToInt32(resultArgs.DataSource.Table.Rows[0][HEAD_ID].ToString());
                    branchOfficeId = Convert.ToInt32(resultArgs.DataSource.Table.Rows[0][BRANCH_ID].ToString());
                    //Update Status and send mail.
                    headOfficeSystem.HeadOfficeId = headOfficeId;
                    headOfficeSystem.BranchOfficeId = branchOfficeId;
                    headOfficeSystem.xmlFileName = FileName;
                    headOfficeSystem.ProjectName = ProjectName;
                    headOfficeSystem.Location = Location;
                    headOfficeSystem.UploadedBy = UploadedBy;
                    headOfficeSystem.ProjectName = ProjectName;
                    resultArgs = headOfficeSystem.ScheduleDataSynchroination();
                }
                else
                {
                    resultArgs.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                }

            }
            return resultArgs;

        }

        /// <summary>
        /// This method cross checks if the project mismatched for Head office vs branch office
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dtBranchOfficeProjects"></param>
        /// <returns></returns>
        private ResultArgs IsProjectMismatched(string headOfficeCode, string branchOfficeCode, DataTable dtBranchOfficeProjects)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                resultArgs = GetHeadOfficeProjects(headOfficeCode, branchOfficeCode);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataTable dtMismatchedProjects = FetchMisMatchedProjects(resultArgs.DataSource.Table, dtBranchOfficeProjects);
                    if (dtMismatchedProjects != null && dtMismatchedProjects.Rows.Count > 0)
                    {
                        resultArgs.Success = false;
                        resultArgs.Message = MessageCatalog.Message.WebServiceMessage.ProjectMismatched;
                        resultArgs.DataSource.Data = dtMismatchedProjects;
                        for (int i = 0; i < dtMismatchedProjects.Rows.Count; i++)
                        {
                            new ErrorLog().WriteError("-----Mismatched Projects from HO:: " + headOfficeCode + "BO:: " + branchOfficeCode + "::" + dtMismatchedProjects.Rows[i]["PROJECT_CODE"].ToString()
                                + " " + dtMismatchedProjects.Rows[i]["PROJECT"].ToString());
                        }
                    }
                    else
                    {
                        resultArgs.Success = true;
                    }
                }
                else
                {
                    resultArgs.Message = MessageCatalog.Message.WebServiceMessage.ProjectNotAvailable;
                }

            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// This method checks if the ledgers mismatched headoffice vs branch offfice
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dtBranchOfficeLedgers"></param>
        /// <returns></returns>
        private ResultArgs IsLedgerMismatched(string headOfficeCode, string branchOfficeCode, DataTable dtBranchOfficeLedgers)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                resultArgs = GetHeadOfficeLedgers(headOfficeCode, branchOfficeCode);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    DataTable dtMismatchedLedgers = FetchMisMatchedLedgers(resultArgs.DataSource.Table, dtBranchOfficeLedgers);
                    if (dtMismatchedLedgers != null && dtMismatchedLedgers.Rows.Count > 0)
                    {
                        resultArgs.Success = false;
                        resultArgs.Message = MessageCatalog.Message.WebServiceMessage.LedgerMismatched;
                        resultArgs.DataSource.Data = dtMismatchedLedgers;
                        for (int i = 0; i < dtMismatchedLedgers.Rows.Count; i++)
                        {
                            new ErrorLog().WriteError("-----Mismatched ledgers from HO:: " + headOfficeCode + "BO:: " + branchOfficeCode + "::" + dtMismatchedLedgers.Rows[i]["LEDGER_CODE"].ToString()
                                + " " + dtMismatchedLedgers.Rows[i]["LEDGER_NAME"].ToString());
                        }
                    }
                    else
                    {
                        resultArgs.Success = true;
                    }
                }
                else
                {
                    resultArgs.Message = MessageCatalog.Message.WebServiceMessage.LedgerNotAvailable;
                }

            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// this method fetches all the amemdments made against the vouchers for the branch office
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        private ResultArgs GetAmendments(string headOfficeCode, string branchOfficeCode)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                if (!(string.IsNullOrEmpty(headOfficeCode) && string.IsNullOrEmpty(branchOfficeCode)))
                {
                    objBase.HeadOfficeCode = headOfficeCode;//To Connect Head Office Database
                    using (AmendmentSystem amendmentSystem = new AmendmentSystem())
                    {
                        amendmentSystem.BranchOfficeCode = branchOfficeCode;
                        resultArgs = amendmentSystem.FetchAmendmentHistory();
                    }
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
            }
            return resultArgs;
        }

        /// <summary>
        /// This method fetches the Datasynch message
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        private ResultArgs FetchSyncStatusMessage(string headOfficeCode, string branchOfficeCode)
        {
            DataTable dt = new DataTable();
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (HeadOfficeSystem headSys = new HeadOfficeSystem())
                {
                    headSys.BranchOfficeCode = branchOfficeCode;
                    headSys.HeadOfficeCode = headOfficeCode;
                    resultArgs = headSys.FetchDatasyncMessage();
                    resultArgs.DataSource.Table.TableName = "DataStatus";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Source + Environment.NewLine + ex.Message;

            }
            return resultArgs;
        }
        /// <summary>
        /// This method fetches all the board case message and sends to the branch office on demand based.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        private ResultArgs FetchBoradCastMessage(string headOfficeCode, string branchOfficeCode)
        {
            DataTable dt = new DataTable();
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                {
                    branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                    resultArgs = branchOfficeSystem.FetchBroadCastMessage();
                    resultArgs.DataSource.Table.TableName = "BroadCastMessage";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Source + Environment.NewLine + ex.Message;

            }
            return resultArgs;
        }
        /// <summary>
        /// This method fetches all the tickets posted for each head office and branch office to the branch office home screen.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        private ResultArgs FetchTroubleTickets(string headOfficeCode, string branchOfficeCode)
        {
            DataTable dtTickets = new DataTable();
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (TroubleTicketingSystem troubleTicket = new TroubleTicketingSystem())
                {
                    troubleTicket.BranchOfficeCode = branchOfficeCode;
                    troubleTicket.HeadOfficeCode = headOfficeCode;
                    resultArgs = troubleTicket.FetchBranchOfficeTickets();
                    resultArgs.DataSource.Table.TableName = "Tickets";
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Source + Environment.NewLine + ex.Message;

            }
            return resultArgs;
        }

        // Common method to send Emails.
        // by Arockia Raj on 18-07-2016
        public static ResultArgs SendEmail(string toEmailId, string toCCEmailId, string subject, string message, bool sendMailBCC)
        {
            return AcMEDSync.Common.SendEmail(toEmailId, toCCEmailId, subject, message, sendMailBCC);
        }

        /// <summary>
        /// This method gets the branch office project names for which data is uploded to the head office from the branch office.
        /// </summary>
        /// <param name="dtBOProjects"></param>
        /// <returns></returns>
        private string GetBOProjectNames(DataTable dtBOProjects)
        {
            StringBuilder sbProjectNames = new StringBuilder();
            try
            {
                if (dtBOProjects != null && dtBOProjects.Rows.Count > 0)
                {
                    foreach (DataRow drProjects in dtBOProjects.Rows)
                    {
                        sbProjectNames.Append(drProjects["PROJECT"].ToString()).Append(',');
                    }

                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Error:GetBOProjectNames::" + ex.Message);
            }
            return sbProjectNames.ToString().TrimEnd(',');
        }
        #endregion

        #region Interface Implementation

        // This method returns the head office mail address.
        public string GetHeadOfficeMailAddress(string headOfficeCode)
        {
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                headOfficeSystem.HeadOfficeCode = headOfficeCode;
                mailId = headOfficeSystem.FetchHeadOfficeMailId();
            }
            return mailId;
        }

        // This method returns the branch office mail address.
        public string GetBranchMailAddress(string branchOfficeCode)
        {
            using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
            {
                headOfficeSystem.BranchOfficeCode = branchOfficeCode;
                mailId = headOfficeSystem.FetchBranchOfficeMailId();
            }
            return mailId;
        }

        // This Method checks whether the main branch is active,does not include subbranches.
        public bool IsBranchExists(string headOfficeCode, string branchOfficeCode)
        {
            bool isBranchExists = false;
            ResultArgs resultArgs = new ResultArgs();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            try
            {
                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                {
                    resultArgs = branchOfficeSystem.IsBranchExists(headOfficeCode, branchOfficeCode);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0 && resultArgs.DataSource != null)
                    {
                        isBranchExists = true;
                    }
                    else
                    {
                        exceptionDetails.Message = !string.IsNullOrEmpty(resultArgs.Message) ? resultArgs.Message : MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
            }
            catch (Exception)
            {
                exceptionDetails.Message = !string.IsNullOrEmpty(resultArgs.Message) ? resultArgs.Message : MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return isBranchExists;
        }

        // This Method returns if the given branch office is active and valid info
        public DataTable GetBranchDetails(string headOfficeCode, string branchOfficeCode)
        {
            DataTable dtBranchInfo = new DataTable();
            ResultArgs resultArgs = new ResultArgs();
            AcMeServiceException exceptionDetails = new AcMeServiceException();

            using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
            {
                resultArgs = branchOfficeSystem.GetActiveBranchInfo(headOfficeCode, branchOfficeCode);
                if (resultArgs.Success && resultArgs.RowsAffected > 0 && resultArgs.DataSource != null)
                {
                    dtBranchInfo = resultArgs.DataSource.Table;
                }
                else
                {
                    exceptionDetails.Message = string.IsNullOrEmpty(resultArgs.Message) ? resultArgs.Message : MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            return dtBranchInfo;
        }

        // This Method returns the branchoffice info for the branch office credential details
        public DataTable GetBranchDetailsByCredentials(string userName, string password)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            ResultArgs resultArgs = new ResultArgs();
            DataTable dtBranchOfficeDetail = new DataTable();
            AcMeServiceException exceptionDetails = new AcMeServiceException();

            if (!(string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password)))
            {
                using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                {
                    branchOfficeSystem.BranchOfficeCode = userName.Trim();
                    resultArgs = branchOfficeSystem.FetchBranchByBranchPartCode(DataBaseType.Portal);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        using (UserSystem userSystem = new UserSystem())
                        {
                            DataTable dtBranchOfficeInfo = resultArgs.DataSource.Table;
                            //Connects to the concern head office database
                            objBase.HeadOfficeCode = dtBranchOfficeInfo.Rows[0][HEAD_CODE].ToString();
                            //Validate username and password
                            resultArgs = userSystem.AuthenticateUser(userName, CommonMember.EncryptValue(password), DataBaseType.HeadOffice);
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                DataTable dtBranchLicense = GetLicenseDetails(dtBranchOfficeInfo.Rows[0][HEAD_CODE].ToString(),
                                    dtBranchOfficeInfo.Rows[0][BRANCH_CODE].ToString());
                                dtBranchOfficeDetail = dtBranchLicense;
                                if (dtBranchLicense == null && dtBranchLicense.Rows.Count == 0)
                                {
                                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.LicenseNotAvailable;
                                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                                }
                            }
                            else
                            {
                                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.InvalidBranchOfficeCredentials;
                                throw new FaultException<AcMeServiceException>(exceptionDetails);
                            }
                        }
                    }
                }
            }
            return dtBranchOfficeDetail;
        }
        /// <summary>
        /// This method returns the license details for the requested branch office as encrypted data.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        public DataTable GetLicenseDetails(string headOfficeCode, string branchOfficeCode)
        {
            ResultArgs resultArgs = new ResultArgs();
            DataTable dtLicense = new DataTable();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            if (!string.IsNullOrEmpty(headOfficeCode) && !(string.IsNullOrEmpty(branchOfficeCode)))
            {
                if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                {
                    using (HeadOfficeSystem headOfficeSystem = new HeadOfficeSystem())
                    {
                        //Connects to Admin_Portal Database
                        headOfficeSystem.HeadOfficeCode = headOfficeCode;
                        headOfficeSystem.BranchOfficeCode = branchOfficeCode;
                        resultArgs = headOfficeSystem.FetchActiveOfficeInfo();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            using (LicenseSystem licenseSystem = new LicenseSystem())
                            {
                                //Connects to Head Office Databse
                                AcMeERP.Base.UIBase objBase = new UIBase();
                                objBase.HeadOfficeCode = headOfficeCode;
                                string locationName = licenseSystem.GetBranchLocation(branchOfficeCode);
                                //Connects to Admin_Portal Databse, to connect admin_portal db , set headofficeCoe as empty
                                objBase.HeadOfficeCode = string.Empty;
                                resultArgs = licenseSystem.GetBranchOfficeLicense(branchOfficeCode, locationName);
                                if (resultArgs.Success && resultArgs.DataSource != null)
                                {
                                    dtLicense = resultArgs.DataSource.Table;
                                }
                                else
                                {
                                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.LicenseNotAvailable;
                                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                                }
                            }
                        }
                        else
                        {
                            exceptionDetails.Message = resultArgs.Message;
                            throw new FaultException<AcMeServiceException>(exceptionDetails);
                        }
                    }
                }
                else
                {
                    exceptionDetails.Message = "Branch Office does not exist or Inactive.(Head Office Code:"
                                             + headOfficeCode + " ,Branch Office Code:" + branchOfficeCode + ")";
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            return dtLicense;
        }

        /// <summary>
        /// This method gets the Brach Office Locked Projects
        /// </summary>
        /// <param name="HeadOfficeCode"></param>
        /// <returns></returns>
        private ResultArgs GetLockVouchers(string HeadOfficeCode, string BranchOfficeCode)
        {
            ResultArgs resultArgs = new ResultArgs();
            AcMeERP.Base.UIBase objBase = new UIBase();
            objBase.HeadOfficeCode = HeadOfficeCode;
            try
            {
                using (BranchOfficeSystem BranchOffice = new BranchOfficeSystem())
                {
                    BranchOffice.BranchOfficeCode = BranchOfficeCode;
                    BranchOffice.HeadOfficeCode = HeadOfficeCode;
                    resultArgs = BranchOffice.FetchBranchByProject(DataBaseType.HeadOffice);
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }
            return resultArgs;
        }

        /// <summary>
        /// This method sends the master data from the head office to branch office.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        public DataSet GetMasterDetails(string headOfficeCode, string branchOfficeCode)
        {
            DataSet dsMaster = new DataSet();
            DataSet dsTDSMaster = new DataSet();
            ResultArgs resultArgs = new ResultArgs();
            AcMeServiceException exceptionDetails = new AcMeServiceException();

            if (!(string.IsNullOrEmpty(headOfficeCode)) && !(string.IsNullOrEmpty(branchOfficeCode)))
            {
                if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                {
                    resultArgs = GetERPMasters(headOfficeCode, branchOfficeCode);
                    if (resultArgs.Success && resultArgs.DataSource != null)
                    {
                        dsMaster = resultArgs.DataSource.TableSet;
                        if (!(dsMaster != null && dsMaster.Tables.Count > 0 && dsMaster.Tables.Contains(BRANCH_PROJECT)))
                        {
                            exceptionDetails.Message = MessageCatalog.Message.UploadVoucher.DownloadNoMasterDataProject;
                            throw new FaultException<AcMeServiceException>(exceptionDetails);
                        }
                        else if (!(dsMaster != null && dsMaster.Tables.Count > 0 && dsMaster.Tables.Contains(BRANCH_LEDGER)))
                        {
                            exceptionDetails.Message = MessageCatalog.Message.UploadVoucher.DownloadNoMasterDataLedger;
                            throw new FaultException<AcMeServiceException>(exceptionDetails);
                        }
                    }
                    else
                    {
                        exceptionDetails.Message = resultArgs.Message;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                    //Include TDS Masters (tds_section, tds_tax_rate, tds_policy, tds_natureofpayment, tds_deducteetype,tds_dutytax
                    dsTDSMaster = GetTDSMasterDetails(headOfficeCode, branchOfficeCode);
                    if (dsTDSMaster != null && dsTDSMaster.Tables.Count > 0)
                    {
                        dsMaster.Merge(dsTDSMaster);
                    }
                    else
                    {
                        exceptionDetails.Message = "TDS Masters are not available in Head office";
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
                else
                {
                    exceptionDetails.Message = "Branch Office does not exist or Inactive.(Head Office Code:" +
                    headOfficeCode + " , Branch Office Code:" + branchOfficeCode + ")";
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            else
            {
                exceptionDetails.Message = "GetMasterDetails Method requires valid Head Office and Branch Office Code";
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return dsMaster;
        }

        /// <summary>
        /// This method is used to download approved budget details  from acmeerp port and its concern head office
        /// for given branch and datefrom, dateto, budgettype and projects
        /// 1. check Branch and head office details
        /// 2. if valid brnach and head office, will return given budget details if its approved
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="budgettypeId"></param>
        /// <param name="dtBudgetBOProjects"></param>
        /// <returns></returns>
        public DataSet GetApprovedBudgetsDetails(string headOfficeCode, string branchOfficeCode, DateTime dFrom, DateTime dTo, Int32 budgettypeId, DataTable dtBudgetBOProjects)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            DataSet dsBudgetDetails = new DataSet("BudgetDetails");
            ResultArgs resultArgs = new ResultArgs();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            try
            {
                if (!(string.IsNullOrEmpty(headOfficeCode)) && !(string.IsNullOrEmpty(branchOfficeCode)))
                {
                    if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                    {
                        objBase.HeadOfficeCode = headOfficeCode;//To Connect Head Office Database

                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.HeadOfficeCode = headOfficeCode;
                            branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                            Int32 branchOfficeId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.HeadOffice, branchOfficeCode);
                            if (branchOfficeId > 0)
                            {
                                new ErrorLog().WriteError("FetchActiveOfficeInfo Success");

                                // This is from AcMEDSyn and Bosoc.HOSQL DLLs(AcME ERP Product)
                                using (ExportMasters exportMasterSystem = new ExportMasters())
                                {
                                    new ErrorLog().WriteError("Started export master");
                                    resultArgs = GetHeadofficeProjectIdsByBOProject(headOfficeCode, branchOfficeCode, dtBudgetBOProjects);
                                    if (resultArgs.Success)
                                    {
                                        new ErrorLog().WriteError("fetch the HO Project by Branch Office Projects is Sucess");
                                        string HOProjectIds = resultArgs.ReturnValue.ToString();
                                        resultArgs = exportMasterSystem.GetBudget(branchOfficeId, dFrom, dTo, HOProjectIds, budgettypeId);
                                        if (resultArgs.Success && resultArgs.DataSource != null)
                                        {
                                            new ErrorLog().WriteError("Get Budget is Sucess");
                                            dsBudgetDetails = resultArgs.DataSource.TableSet;
                                            resultArgs.Success = true;
                                            new ErrorLog().WriteError("Budget dataset is Sucess");
                                        }
                                        else
                                        {
                                            new ErrorLog().WriteError("Get Budet is failed!");
                                        }
                                    }
                                    else
                                    {
                                        new ErrorLog().WriteError("Get HeadOffice Project using Branch office Sucess");
                                    }
                                }
                            }
                            else
                            {
                                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                                throw new FaultException<AcMeServiceException>(exceptionDetails);
                            }
                        }
                    }
                    else
                    {
                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
                else
                {
                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }

            return dsBudgetDetails;
        }

        /// <summary>
        /// this method is used to upload budget details to acmeerp port and its concern head office
        /// 1. check Branch and head office details
        /// 2. if valid brnach and head office, will return given budget details if its approved
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dsBudgetDetails"></param>
        /// <returns></returns>
        public bool UploadBudgetsDetails(string headOfficeCode, string branchOfficeCode, DataSet dsBudgetDetails, bool isMail)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            ResultArgs resultArgs = new ResultArgs();

            AcMeServiceException exceptionDetails = new AcMeServiceException();
            try
            {
                if (!(string.IsNullOrEmpty(headOfficeCode)) && !(string.IsNullOrEmpty(branchOfficeCode)))
                {
                    if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                    {
                        objBase.HeadOfficeCode = headOfficeCode;//To Connect Head Office Database
                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.HeadOfficeCode = headOfficeCode;
                            branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                            Int32 branchOfficeId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.HeadOffice, branchOfficeCode);
                            if (branchOfficeId > 0)
                            {
                                //new ErrorLog().WriteError("FetchActiveOfficeInfo Success");

                                // This is from AcMEDSyn and Bosoc.HOSQL DLLs(AcME ERP Product)
                                using (ImportVoucherSystem importvoucher = new ImportVoucherSystem())
                                {
                                    resultArgs = importvoucher.UpdateBudgetDetails(branchOfficeId, dsBudgetDetails);
                                    if (resultArgs.Success && resultArgs.DataSource != null)
                                    {
                                        resultArgs.Success = true;
                                        DataTable dtBudetMaster = dsBudgetDetails.Tables["BudgetMaster"];
                                        if (dtBudetMaster.Rows.Count > 0)
                                        {
                                            string budgetname = dtBudetMaster.Rows[0][objBase.AppSchema.Budget.BUDGET_NAMEColumn.ColumnName].ToString();
                                            DateTime dtfrom = objBase.Member.DateSet.ToDate(dtBudetMaster.Rows[0][objBase.AppSchema.Budget.DATE_FROMColumn.ColumnName].ToString(), false);
                                            DateTime dtto = objBase.Member.DateSet.ToDate(dtBudetMaster.Rows[0][objBase.AppSchema.Budget.DATE_TOColumn.ColumnName].ToString(), false);
                                            string datefrom = dtfrom.Day.ToString("D2") + "/" + dtfrom.Month.ToString("D2") + "/" + dtfrom.Year.ToString("D2");
                                            string dateto = dtto.Day.ToString("D2") + "/" + dtto.Month.ToString("D2") + "/" + dtto.Year.ToString("D2");
                                            string projects = dtBudetMaster.Rows[0][objBase.AppSchema.Project.PROJECTColumn.ColumnName].ToString();

                                            //For Temp 24/04/2021 ------------------------------------------------------- it, neded 
                                            if (isMail)
                                                branchOfficeSystem.SendBudgetMail(branchOfficeId, budgetname, datefrom, dateto, projects, BudgetAction.Recommended);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                                throw new FaultException<AcMeServiceException>(exceptionDetails);
                            }
                        }
                    }
                    else
                    {
                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
                else
                {
                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }
            finally
            {
                //On 24/03/2023, to raise proper failied message
                if (!resultArgs.Success)
                {
                    exceptionDetails.Message = resultArgs.Message;
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }

            return resultArgs.Success;
        }

        /// <summary>
        /// This method gets the uploaded vouchers as byte and converted to xml file for the datasync service.
        /// </summary>
        /// <param name="Vouchers"></param>
        /// <returns></returns>
        public bool UploadVoucher(byte[] Vouchers)
        {
            string UploadDirectory = DataSyncLocation;
            string FileName = string.Empty;
            string FilePath = string.Empty;
            bool isFileUpload = false;
            bool isLicenseUpdated = true;
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            if (Vouchers != null)
            {
                DataSet dsVouchers = CommonMethod.DecompressData(Vouchers);
                ResultArgs resultArgs = new ResultArgs();
                if (dsVouchers != null && dsVouchers.Tables.Count > 0 &&
                (dsVouchers.Tables.Contains(VOUCHER_TABLENAME) || dsVouchers.Tables.Contains(FD_VOUCHER_TABLENAME)))
                {
                    DataTable dtValidOfficeData = new DataTable();
                    string BranchOfficeCode = string.Empty; string HeadOfficeCode = string.Empty;
                    string Location = string.Empty; string UploadedBy = string.Empty;

                    //Header Table Contains Branch Office Info
                    dtValidOfficeData = dsVouchers.Tables[headOfficeTableName];
                    if (dtValidOfficeData != null && dtValidOfficeData.Rows.Count > 0)
                    {
                        DataRow drOffice = dtValidOfficeData.Rows[0];
                        HeadOfficeCode = CommonMember.DecryptValue(drOffice[HEAD_CODE].ToString());
                        BranchOfficeCode = CommonMember.DecryptValue(drOffice[BRANCH_CODE].ToString());
                        Location = CommonMember.DecryptValue(drOffice["LOCATION"].ToString());
                        UploadedBy = CommonMember.DecryptValue(drOffice["UPLOADED_BY"].ToString());
                        if (!(string.IsNullOrEmpty(BranchOfficeCode) && string.IsNullOrEmpty(HeadOfficeCode)))
                        {
                            if (IsBranchExists(HeadOfficeCode, BranchOfficeCode))
                            {
                                //Alert if the latest license is not available in the branch either to upload Data or to import masters
                                if (dtValidOfficeData.Columns.Contains("KEY_GENERATED_DATE"))
                                {
                                    DateTime dtBranchLicenseDate = DateTime.Parse(CommonMember.DecryptValue(drOffice[KEY_GENERATED_DATE].ToString())).Date;
                                    isLicenseUpdated = IsLatestLicenseAvailable(HeadOfficeCode, BranchOfficeCode, dtBranchLicenseDate);
                                }
                                if (isLicenseUpdated)
                                {
                                    if (dsVouchers.Tables.Contains(BRANCH_PROJECT) && dsVouchers.Tables[BRANCH_PROJECT].Rows.Count > 0)
                                    {
                                        if (dsVouchers.Tables.Contains(BRANCH_LEDGER) && dsVouchers.Tables[BRANCH_LEDGER].Rows.Count > 0)
                                        {
                                            DataTable dtBranchOfficeProjects = dsVouchers.Tables[BRANCH_PROJECT];
                                            DataTable dtBranchOfficeLedgers = GetBranchOfficeLedgers(dsVouchers.Tables[BRANCH_LEDGER]);
                                            //Get Mismatched Projects and Ledgers
                                            resultArgs = IsProjectMismatched(HeadOfficeCode, BranchOfficeCode, dtBranchOfficeProjects);
                                            if (resultArgs.Success)
                                            {
                                                new ErrorLog().WriteError("Projects are not mismatched");
                                                //  resultArgs = IsLedgerMismatched(HeadOfficeCode, BranchOfficeCode, dtBranchOfficeLedgers);
                                                // if (resultArgs.Success)
                                                // {
                                                //    new ErrorLog().WriteError("Ledgers are not mismatched");
                                                //upload file // Branch Code can be added
                                                FileName = BranchOfficeCode + "_";
                                                FileName += DateTime.Now.Ticks.ToString() + ".xml";
                                                FilePath = UploadDirectory + FileName;
                                                if (!Directory.Exists(UploadDirectory))
                                                {
                                                    Directory.CreateDirectory(UploadDirectory);
                                                }
                                                resultArgs = XMLConverter.WriteToXMLFile(dsVouchers, FilePath);
                                                if (resultArgs.Success)
                                                {
                                                    isFileUpload = true;
                                                    new ErrorLog().WriteError("WriteToXMLFile is Success File Name is ::" + FileName);
                                                    string projectName = GetBOProjectNames(dtBranchOfficeProjects);
                                                    //On 24/02/2018, update Projects, branch location and who is uploaded details in data suyn table
                                                    resultArgs = UpdateVoucherUploadStatus(HeadOfficeCode, BranchOfficeCode, FileName, projectName, Location, UploadedBy);
                                                    if (!resultArgs.Success)
                                                    {
                                                        new ErrorLog().WriteError("Error:UpdateVoucherUploadStatus:" + resultArgs.Message);
                                                    }
                                                    //}
                                                }
                                                else
                                                {
                                                    exceptionDetails.Message = resultArgs.Message;
                                                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                                                }
                                            }
                                            else
                                            {
                                                exceptionDetails.Message = resultArgs.Message;
                                                throw new FaultException<AcMeServiceException>(exceptionDetails);
                                            }
                                        }
                                        else
                                        {
                                            exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchLedgerNotAvailable;
                                            throw new FaultException<AcMeServiceException>(exceptionDetails);
                                        }

                                    }
                                    else
                                    {
                                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchProjectNotAvailable;
                                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                                    }
                                }
                            }
                            else
                            {
                                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                                throw new FaultException<AcMeServiceException>(exceptionDetails);
                            }

                        }
                    }
                }
                else
                {
                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.InvalidVouchers;
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            else
            {
                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.UploadVoucherRequired;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return isFileUpload;
        }

        /// <summary>
        /// this method sends the amendments to the concern branch office
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        public DataTable GetVoucherAmendments(string headOfficeCode, string branchOfficeCode)
        {
            DataTable dtVoucherAmendments = new DataTable();
            ResultArgs resultArgs = new ResultArgs();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            if (!string.IsNullOrEmpty(headOfficeCode) && !string.IsNullOrEmpty(branchOfficeCode))
            {
                if (IsBranchExists(headOfficeCode, branchOfficeCode))
                {
                    resultArgs = GetAmendments(headOfficeCode, branchOfficeCode);
                    if (resultArgs.Success && resultArgs.DataSource != null)
                    {
                        dtVoucherAmendments = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        exceptionDetails.Message = !string.IsNullOrEmpty(resultArgs.Message) ? resultArgs.Message :
                            MessageCatalog.Message.WebServiceMessage.AmendmentNotesNotAvailable;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
            }
            else
            {
                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.AmendmentNotesRequired;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return dtVoucherAmendments;
        }

        /// <summary>
        /// this method sends the mismatched projects if any Head office vs branch office while uploading vouhcer file or synchronization
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dtBOProjects"></param>
        /// <returns></returns>
        public DataTable GetMismatchedProjects(string headOfficeCode, string branchOfficeCode, DataTable dtBOProjects)
        {
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs resultArgs = new ResultArgs();
            DataTable dtMismatchedProjects = new DataTable();
            if (dtBOProjects != null)
            {
                resultArgs = IsProjectMismatched(headOfficeCode, branchOfficeCode, dtBOProjects);
                if (!resultArgs.Success)
                {
                    dtMismatchedProjects = resultArgs.DataSource.Table;
                    new ErrorLog().WriteError("Error in False:GetMismatchedProjects:" + resultArgs.Message);
                }
                else
                {
                    new ErrorLog().WriteError("Error in True:GetMismatchedProjects:" + resultArgs.Message);
                }
            }
            else
            {
                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.NotSentBranchOfficeProjects;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            dtMismatchedProjects.TableName = "MismatchedProjects";
            return dtMismatchedProjects;

        }
        /// <summary>
        /// This method sends the mismatched ledgers if any Head office vs branch office while uploading vouhcer file or synchronization
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dtBOLedgers"></param>
        /// <returns></returns>
        public DataTable GetMismatchedLedgers(string headOfficeCode, string branchOfficeCode, DataTable dtBOLedgers)
        {
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs resultArgs = new ResultArgs();
            DataTable dtMismatchedLedgers = new DataTable();
            if (dtBOLedgers != null)
            {
                resultArgs = IsLedgerMismatched(headOfficeCode, branchOfficeCode, dtBOLedgers);
                if (!resultArgs.Success)
                {
                    dtMismatchedLedgers = resultArgs.DataSource.Table;
                    new ErrorLog().WriteError("Error in False:GetMismatchedLedgers:" + resultArgs.Message);
                }
                else
                {
                    new ErrorLog().WriteError("Error iin True:GetMismatchedLedgers:" + resultArgs.Message);
                }
            }
            else
            {
                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.NotSentBranchOfficeLedgers;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            dtMismatchedLedgers.TableName = "MismatchedLedgers";
            return dtMismatchedLedgers;
        }


        /// <summary>
        /// This method gets the voucher for previous month data is available or not based on given date
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dtBOLedgers"></param>
        /// <returns></returns>
        public bool GetExportDatatoPortalExists(string headOfficeCode, string branchOfficeCode, DateTime dtFrom)
        {
            bool isOneMonthData = false;
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs resultArgs = new ResultArgs();
            DateTime BeforeDF = dtFrom.AddMonths(-3);
            DateTime BeforeDT = dtFrom.AddDays(-1);
            if (!string.IsNullOrEmpty(headOfficeCode) && !(string.IsNullOrEmpty(branchOfficeCode)))
            {
                AcMeERP.Base.UIBase objBase = new UIBase();
                objBase.HeadOfficeCode = headOfficeCode;

                if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                {
                    if (BeforeDF != null && BeforeDT != null)
                    {
                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.HeadOfficeCode = headOfficeCode;
                            branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                            Int32 branchOfficeId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.HeadOffice, branchOfficeCode);
                            if (branchOfficeId > 0)
                            {
                                using (VoucherTransactionSystem vouchertranssystem = new VoucherTransactionSystem())
                                {
                                    resultArgs = vouchertranssystem.CheckTransVoucherExists(branchOfficeId, BeforeDF, BeforeDT);
                                    if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                                    {
                                        isOneMonthData = true;
                                    }
                                    else
                                    {
                                        exceptionDetails.Message = "Previous Year Actual balances not exported., Please export the previous Year data and send the Budget.";
                                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                exceptionDetails.Message = "Provide HeadOfficeCode and BranchOfficeCode";
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return isOneMonthData;
        }

        /// <summary>
        /// This method updates datasyncstatus for the branch office after finishing the datasynchronization
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public bool UpdateDsyncStatus(string headOfficeCode, string branchOfficeCode, string FileName,
            string Project = "", string Location = "", string UploadedBy = "")
        {
            bool DsyncStatus = false;
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs resultArgs = new ResultArgs();
            if (!string.IsNullOrEmpty(headOfficeCode) && !string.IsNullOrEmpty(branchOfficeCode) && !string.IsNullOrEmpty(FileName))
            {
                resultArgs = UpdateVoucherUploadStatus(headOfficeCode, branchOfficeCode, FileName, Project, Location, UploadedBy);
                if (resultArgs.Success)
                {
                    DsyncStatus = true;
                }
                else
                {
                    new ErrorLog().WriteError("Error:UpdateDsyncStatus::UpdateVoucherUploadStatus:" + resultArgs.Message);
                    exceptionDetails.Message = resultArgs.Message;
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            else
            {
                new ErrorLog().WriteError("Error:UpdateDsyncStatus::UpdateVoucherUploadStatus:Head office ,Branch Office code and Filename empty");
                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.NotSentDataSyncUpdateStatus;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return DsyncStatus;
        }

        //Fetch TDS masters TDS Section, TDS Deductee Type, TDS Duty Tax and TDS Policy
        public DataSet GetTDSMasterDetails(string headOfficeCode, string branchOfficeCode)
        {
            DataSet dsTDSMasters = new DataSet();

            ResultArgs resultArgs = new ResultArgs();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            if (!(string.IsNullOrEmpty(headOfficeCode)) && !(string.IsNullOrEmpty(branchOfficeCode)))
            {
                if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                {
                    using (ExportMasters exportMasterSystem = new ExportMasters())
                    {
                        resultArgs = exportMasterSystem.GetTDSMasters();
                        if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.TableSet.Tables.Count > 0)
                        {
                            dsTDSMasters = resultArgs.DataSource.TableSet;
                            dsTDSMasters.DataSetName = "TDSMasters";
                        }
                        else
                        {
                            exceptionDetails.Message = !string.IsNullOrEmpty(resultArgs.Message) ? resultArgs.Message : "TDS Masters are not available in Head office";
                            throw new FaultException<AcMeServiceException>(exceptionDetails);
                        }
                    }
                }
                else
                {
                    exceptionDetails.Message = "Branch Office does not exist or Inactive.(Head Office Code:" +
                    headOfficeCode + " , Branch Office Code:" + branchOfficeCode + ")";
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            else
            {
                exceptionDetails.Message = "GetMasterDetails Method requires valid Head Office and Branch Office Code";
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }

            return dsTDSMasters;
        }

        /// <summary>
        /// Gets the Head office datasync message and all the borad cast messages to the branch office 
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <returns></returns>
        public DataSet GetHeadOfficeMessages(string headOfficeCode, string branchOfficeCode)
        {
            ResultArgs resultArgs = new ResultArgs();
            DataSet dsMessages = new DataSet();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            if (!string.IsNullOrEmpty(headOfficeCode) && !(string.IsNullOrEmpty(branchOfficeCode)))
            {
                if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                {
                    //Connects to Admin_Portal Database
                    resultArgs = FetchSyncStatusMessage(headOfficeCode, branchOfficeCode);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dsMessages.Tables.Add(resultArgs.DataSource.Table);
                    }
                    else
                    {
                        new ErrorLog().WriteError("Error:GetHeadOfficeMessagess::FetchSyncStatusMessage:" + resultArgs.Message);
                        exceptionDetails.Message = resultArgs.Message;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                    //Connect to Head office Database
                    AcMeERP.Base.UIBase objBase = new UIBase();
                    objBase.HeadOfficeCode = headOfficeCode;

                    resultArgs = GetAmendments(headOfficeCode, branchOfficeCode);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dsMessages.Tables.Add(resultArgs.DataSource.Table);
                    }
                    else
                    {
                        new ErrorLog().WriteError("Error:GetHeadOfficeMessagess::GetAmendment:" + resultArgs.Message);
                        exceptionDetails.Message = resultArgs.Message;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }

                    resultArgs = FetchBoradCastMessage(headOfficeCode, branchOfficeCode);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dsMessages.Tables.Add(resultArgs.DataSource.Table);
                    }
                    else
                    {
                        new ErrorLog().WriteError("Error:GetHeadOfficeMessagess::FetchBoradCastMessage:" + resultArgs.Message);
                        exceptionDetails.Message = resultArgs.Message;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                    // Connects to Admin_Portal Database for Trouble Tickets
                    resultArgs = FetchTroubleTickets(headOfficeCode, branchOfficeCode);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dsMessages.Tables.Add(resultArgs.DataSource.Table);
                    }
                    else
                    {
                        new ErrorLog().WriteError("Error:GetHeadOfficeMessagess::FetchTroubleTickets:" + resultArgs.Message);
                        exceptionDetails.Message = resultArgs.Message;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }

                    if (dsMessages.Tables.Contains("datastatus") && dsMessages.Tables["datastatus"] != null)
                    {
                        DataTable dtSyncStatus = dsMessages.Tables["datastatus"];
                        using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                        {
                            dtSyncStatus.Columns.Add("REMARK_IN_DETAIL", typeof(System.String));
                            dtSyncStatus.DefaultView.RowFilter = vouchersystem.AppSchema.Datasync_Task.STATUSColumn.ColumnName + " = '" + DataSyncStatus.Failed.ToString() + "'";
                            if (dtSyncStatus.DefaultView.Count > 0)
                            {
                                foreach (DataRow dr in dtSyncStatus.Rows)
                                {
                                    string remarks = dr[vouchersystem.AppSchema.Datasync_Task.REMARKSColumn.ColumnName].ToString();
                                    dr.BeginEdit();
                                    remarks = GetProjectVoucherDetailsByDataSycErrorMessage(headOfficeCode, branchOfficeCode, remarks);
                                    dr[objBase.AppSchema.Datasync_Task.REMARKSColumn.ColumnName] = remarks;
                                    dr.EndEdit();

                                }
                            }
                            dtSyncStatus.DefaultView.RowFilter = string.Empty;
                        }
                    }
                }
                else
                {
                    exceptionDetails.Message = "Branch Office does not exist or Inactive.(Head Office Code:"
                                                 + headOfficeCode + " ,Branch Office Code:" + branchOfficeCode + ")";
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            else
            {
                exceptionDetails.Message = "Provide HeadOfficeCode and BranchOfficeCode";
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }

            return dsMessages;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetProjectVoucherDetailsByDataSycErrorMessage(string headOfficeCode, string branchOfficeCode, string datasyncerrormessage)
        {
            string rtn = datasyncerrormessage.Trim();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            AcMeERP.Base.UIBase objBase = new UIBase();
            objBase.HeadOfficeCode = headOfficeCode;
            try
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    string pattern = @"The Record is Available '\d+-\d+-\d+'";
                    Match m = Regex.Match(datasyncerrormessage, pattern, RegexOptions.IgnoreCase);
                    if (m.Success && !string.IsNullOrEmpty(m.Value))
                    {
                        string[] temp = Regex.Split(m.Value, @"'");
                        if (temp.Length == 3)
                        {
                            string[] digits = Regex.Split(temp.GetValue(1).ToString(), @"-");
                            if (digits.Length == 3)
                            {
                                Int32 branchid = objBase.Member.NumberSet.ToInteger(digits.GetValue(0).ToString());
                                Int32 voucherid = objBase.Member.NumberSet.ToInteger(digits.GetValue(1).ToString());
                                Int32 locationid = objBase.Member.NumberSet.ToInteger(digits.GetValue(2).ToString());

                                ResultArgs result = vouchersystem.FetchMasterByBranchLocationVoucherId(branchid, locationid, voucherid);
                                if (result != null && result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                                {
                                    DataTable dtVoucher = result.DataSource.Table;
                                    string content = "The following Voucher is already available in the Head Office for different Project or Date.";
                                    string vtype = dtVoucher.Rows[0][objBase.AppSchema.VoucherMaster.VOUCHER_TYPEColumn.ColumnName].ToString();
                                    string remarkindetails = "Head Office Voucher " + System.Environment.NewLine;
                                    remarkindetails += "Project           : " + dtVoucher.Rows[0][objBase.AppSchema.Project.PROJECTColumn.ColumnName].ToString() + System.Environment.NewLine;
                                    remarkindetails += "Voucher Date  : " + objBase.Member.DateSet.ToDate(dtVoucher.Rows[0][objBase.AppSchema.VoucherMaster.VOUCHER_DATEColumn.ColumnName].ToString(), false).ToShortDateString();
                                    remarkindetails += "   Voucher No : " + dtVoucher.Rows[0][objBase.AppSchema.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                                    remarkindetails += "   Voucher Type : " + (vtype == "RC" ? VoucherType.Receipts.ToString() : vtype == "PY" ? VoucherType.Payments.ToString() :
                                                                        vtype == "CN" ? VoucherType.Contra.ToString() : VoucherType.Journal.ToString()) + System.Environment.NewLine;

                                    rtn += "." + System.Environment.NewLine + System.Environment.NewLine + content + System.Environment.NewLine + remarkindetails;

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                exceptionDetails.Message = err.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return rtn;
        }

        /// <summary>
        /// this method gets the posted tickets from the acme.erp desktop product and udpates in the head office portal
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dtTicket"></param>
        /// <returns></returns>
        public bool PostTicket(string headOfficeCode, string branchOfficeCode, DataTable dtTicket)
        {
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            AcMeERP.Base.UIBase objBase = new UIBase();
            bool _IsSuccess = false;
            if (!string.IsNullOrEmpty(headOfficeCode) && !(string.IsNullOrEmpty(branchOfficeCode)))
            {
                if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                {
                    if (dtTicket != null && dtTicket.Rows.Count > 0)
                    {
                        TroubleTicketingSystem ticketSystem = new TroubleTicketingSystem();
                        for (int i = 0; i < dtTicket.Rows.Count; i++)
                        {
                            int TicketId = Convert.ToInt16(dtTicket.Rows[i]["TICKET_ID"].ToString());
                            int RepliedTicketId = Convert.ToInt16(dtTicket.Rows[i]["REPLIED_TICKET_ID"].ToString());
                            ticketSystem.TicketId = TicketId == 0 ? (int)AddNewRow.NewRow : TicketId;
                            ticketSystem.HeadOfficeCode = headOfficeCode.Trim();
                            ticketSystem.BranchOfficeCode = branchOfficeCode.Trim();
                            ticketSystem.Subject = dtTicket.Rows[i]["SUBJECT"].ToString();
                            ticketSystem.Description = dtTicket.Rows[i]["DESCRIPTION"].ToString();
                            ticketSystem.Priority = Convert.ToInt16(dtTicket.Rows[i]["PRIORITY"].ToString());
                            ticketSystem.PostedDate = DateTime.Now;
                            ticketSystem.CompletedDate = DateTime.Now;
                            ticketSystem.FetchLoginUserDetailsbyBOCode();
                            //ticketSystem.PostedBy = Convert.ToInt16(dtTicket.Rows[i]["POSTED_BY"].ToString());
                            //ticketSystem.UserName = !string.IsNullOrEmpty(objBase.LoginUser.LoginUserName) ? objBase.LoginUser.LoginUserName :
                            //    dtTicket.Rows[i]["USER_NAME"].ToString();
                            ticketSystem.AttachFileName = string.Empty;
                            ticketSystem.AttachFileNamePhysical = string.Empty;
                            ticketSystem.RepliedTicketId = RepliedTicketId;
                            if (TicketId == 0 && RepliedTicketId == 0)
                                ticketSystem.Status = (int)TroubleTicketStatus.Posted;
                            ResultArgs resulArgs = ticketSystem.SaveTicketDetails(DataBaseType.Portal);
                            if (resulArgs != null && resulArgs.Success)
                            {
                                _IsSuccess = true;
                            }
                        }
                    }
                }
                else
                {
                    exceptionDetails.Message = "Branch Office does not exist or Inactive.(Head Office Code:"
                                                 + headOfficeCode + " ,Branch Office Code:" + branchOfficeCode + ")";
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            else
            {
                exceptionDetails.Message = "Provide HeadOfficeCode and BranchOfficeCode";
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return _IsSuccess;
        }

        /// <summary>
        /// this method checks if the latest license is available and indicates the user to update the new license for the product to be up-to-date.
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dtCurrentLicenseDate"></param>
        /// <returns></returns>
        public bool IsLatestLicenseAvailable(string headOfficeCode, string branchOfficeCode, DateTime dtCurrentLicenseDate)
        {
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            bool _IsSuccess = false;
            if (!string.IsNullOrEmpty(headOfficeCode) && !(string.IsNullOrEmpty(branchOfficeCode)) && dtCurrentLicenseDate != null)
            {
                if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                {
                    DataTable dtLicense = GetLicenseDetails(headOfficeCode, branchOfficeCode);
                    if (dtLicense != null && dtLicense.Rows.Count > 0)
                    {
                        DateTime dteLicenseDate = Convert.ToDateTime(CommonMember.DecryptValue(dtLicense.Rows[0][KEY_GENERATED_DATE].ToString()));
                        if (dteLicenseDate.Date.Equals(dtCurrentLicenseDate.Date))
                        {
                            _IsSuccess = true;
                        }
                        else
                        {
                            new ErrorLog().WriteError("Error:IsLatestLicenseAvailable:dtCurrentLicenseDate:" + dtCurrentLicenseDate + ":dteLicenseDate" + dteLicenseDate);
                            exceptionDetails.Message = "Your license key is not up-to-date to import/export Masters/Vouchers";
                            throw new FaultException<AcMeServiceException>(exceptionDetails);
                        }
                    }
                }
                else
                {
                    exceptionDetails.Message = "Branch Office does not exist or Inactive.(Head Office Code:"
                                                 + headOfficeCode + " ,Branch Office Code:" + branchOfficeCode + ")";
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            else
            {
                exceptionDetails.Message = "Provide HeadOfficeCode, BranchOfficeCode and LicensedDate";
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return _IsSuccess;
        }
        /// <summary>
        /// This method returns acme.erp updater latest product version.
        /// </summary>
        /// <returns></returns>
        public string GetAcmeERPProductVersion()
        {
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            string erpVersionNumber = string.Empty;
            try
            {
                var version = FileVersionInfo.GetVersionInfo(PagePath.AcMEERPLatestVersionExePath.ToString());
                erpVersionNumber = version.ProductVersion;
            }
            catch (Exception ex)
            {
                exceptionDetails.Message = ex.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);

            }

            return erpVersionNumber;
        }

        /// <summary>
        /// This method returns acme.erp Server Current Date.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentServerDate()
        {
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            string AcmeerpServer = string.Empty;
            try
            {
                Bosco.Utility.CommonMemberSet.DateSetMember dataset = new Bosco.Utility.CommonMemberSet.DateSetMember();
                AcmeerpServer = dataset.ToCurrentDateTime("dd/MM/yyyy h:mm:ss tt");
            }
            catch (Exception ex)
            {
                exceptionDetails.Message = ex.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return AcmeerpServer;
        }

        /// <summary>
        /// This  method return Locked Branch Office Projects
        /// </summary>
        /// <param name="BranchOfficeCode"></param>
        /// <returns></returns>
        public DataTable GetLockVoucher(string HeadOfficecode, string BranchOfficeCode)
        {
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs resultArgs = new ResultArgs();
            DataTable dtLockedVoucherProjects = new DataTable();
            if (dtLockedVoucherProjects != null)
            {
                resultArgs = GetLockVouchers(HeadOfficecode, BranchOfficeCode);
                if (resultArgs.Success)
                {
                    dtLockedVoucherProjects = resultArgs.DataSource.Table;
                    new ErrorLog().WriteError("Error in False:GetLockVouchers:" + resultArgs.Message);
                }
                else
                {
                    new ErrorLog().WriteError("Error in True:GetLockVouchers:" + resultArgs.Message);
                }
            }
            else
            {
                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.NotSentBranchOfficeProjects;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            dtLockedVoucherProjects.TableName = "LockVouchers";
            return dtLockedVoucherProjects;
        }

        /// <summary>
        /// This method gets the Brach Office voucher lock grace days
        /// </summary>
        /// <param name="HeadOfficeCode"></param>
        /// <returns></returns>
        public DataTable GetLockVoucherGraceDays(string HeadOfficeCode, string BranchOfficeCode, string BranchLocation)
        {
            Int32 branchOfficeId = 0;
            Int32 branchOfficelocationId = 0;
            DataTable dtLockedVoucherGraceDays = new DataTable();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs resultArgs = new ResultArgs();
            AcMeERP.Base.UIBase objBase = new UIBase();
            objBase.HeadOfficeCode = HeadOfficeCode;
            try
            {
                if (!(string.IsNullOrEmpty(HeadOfficeCode)) && !(string.IsNullOrEmpty(BranchOfficeCode)))
                {
                    if (IsBranchExists(HeadOfficeCode.Trim(), BranchOfficeCode.Trim()))
                    {
                        objBase.HeadOfficeCode = HeadOfficeCode;//To Connect Head Office Database

                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.HeadOfficeCode = HeadOfficeCode;
                            branchOfficeSystem.BranchOfficeCode = BranchOfficeCode;
                            branchOfficeId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.HeadOffice, BranchOfficeCode);
                            if (branchOfficeId > 0)
                            {
                                new ErrorLog().WriteError("GetLockVoucherGraceDays: FetchActiveOfficeInfo Success");
                                using (BranchLocationSystem branchlocation = new BranchLocationSystem())
                                {
                                    //1 -Primary Location (Default)
                                    if (BranchLocation.Trim().ToUpper() == "PRIMARY")
                                        branchOfficelocationId = 1;
                                    else
                                    {
                                        branchOfficelocationId = branchlocation.GetLocationId(branchOfficeId, BranchLocation);
                                    }
                                }

                                if (branchOfficelocationId > 0)
                                {
                                    using (LockVoucherSystem lockvouchersystem = new LockVoucherSystem())
                                    {
                                        resultArgs = lockvouchersystem.FetchBranchLockVoucherGraceDaysByBranchLocation(branchOfficeId, branchOfficelocationId);
                                        if (resultArgs.Success)
                                        {
                                            dtLockedVoucherGraceDays = resultArgs.DataSource.Table;
                                            new ErrorLog().WriteError("Received GetLockVoucherGraceDays");
                                        }
                                        else
                                        {
                                            new ErrorLog().WriteError("Error in True:GetLockVoucherGraceDays:" + resultArgs.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    new ErrorLog().WriteError("FetchActiveOfficeInfo Success : BranchLocationNotAvailable");
                                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchLocationNotAvailable;
                                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                                }
                            }
                            else
                            {
                                new ErrorLog().WriteError("FetchActiveOfficeInfo Success : BranchNotAvailable");
                                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                                throw new FaultException<AcMeServiceException>(exceptionDetails);
                            }
                        }
                    }
                    else
                    {
                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
                else
                {
                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            catch (Exception e)
            {
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }
            return dtLockedVoucherGraceDays;
        }


        /// <summary>
        /// this method is used to update branch office logged history
        /// </summary>
        /// <param name="BranchOfficeCode"></param>
        /// <param name="HeadOfficeCode"></param>
        /// <param name="BranchName"></param>
        /// <param name="HeadOfficeName"></param>
        /// <param name="Location"></param>
        /// <param name="loggedDateTime"></param>
        /// <returns></returns>
        public bool UpdateBranchLoggedHistory(string BranchOfficeCode, string HeadOfficeCode, string BranchOfficeName, string HeadOfficeName, string Location, DateTime LoggedDateTime, string LicenseKeyNumber, string Remarks)
        {
            bool Rtn = false;
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs resultArgs = new ResultArgs();
            using (BranchOfficeSystem branchsystem = new BranchOfficeSystem())
            {
                resultArgs = branchsystem.UpdateBranchLoggedHistory(BranchOfficeCode, HeadOfficeCode, BranchOfficeName, HeadOfficeName, Location, LoggedDateTime, LicenseKeyNumber, Remarks);
                Rtn = resultArgs.Success;
            }
            return Rtn;
        }

        /// <summary>
        /// 17/02/2020, This Temporary method and process, to update Sub Ledger Vouchers
        /// 
        /// ***** This logic should be implemented/moved to DataSync Windows Service  *******************
        /// 1. Get List of Sub Ledger from Sub ledger trans, Insert/Update to Sub Ledger Master List
        /// 2. Clear Concern vouchers's sub ledger vouchers
        /// 3. Map Ledger with Sub Ledger for those who have vouchers 
        /// </summary>
        /// <param name="BranchOfficeCode"></param>
        /// <param name="HeadOfficeCode"></param>
        /// <param name="Location"></param>
        /// <param name="dtSubLedgerVouchers"> Contain Sub Ledger Voucher Details with Ledger Name and Sub Ledger</param>
        /// <returns></returns>
        public string UpdateSubLedgerVouchers(string BranchOfficeCode, string HeadOfficeCode, string Location, DateTime FrmDate, DateTime ToDate, DataTable dtSubLedgerVouchers)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            DataTable dtResult = new DataTable();
            string rtnmessage = string.Empty;
            ResultArgs resultArg = new ResultArgs();

            DataRow drResult = dtResult.NewRow();
            dtResult.Columns.Add("Result", typeof(System.String));
            dtResult.Columns["Result"].DefaultValue = rtnmessage;

            try
            {
                if (!(string.IsNullOrEmpty(HeadOfficeCode)) && !(string.IsNullOrEmpty(BranchOfficeCode)))
                {
                    if (IsBranchExists(HeadOfficeCode.Trim(), BranchOfficeCode.Trim()))
                    {
                        objBase.HeadOfficeCode = HeadOfficeCode;//To Connect Head Office Database
                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.HeadOfficeCode = HeadOfficeCode;
                            branchOfficeSystem.BranchOfficeCode = BranchOfficeCode;
                            Int32 branchOfficeId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.HeadOffice, BranchOfficeCode);
                            if (branchOfficeId > 0)
                            {
                                if (dtSubLedgerVouchers != null && dtSubLedgerVouchers.Rows.Count > 0)
                                {
                                    using (ImportVoucherSystem importsystem = new ImportVoucherSystem())
                                    {

                                        //Get List of Sub Ledgters
                                        DataTable dtSubLedgers = dtSubLedgerVouchers.DefaultView.ToTable(true, new string[] { importsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, 
                                                                        importsystem.AppSchema.VoucherSubLedger.SUB_LEDGER_NAMEColumn.ColumnName });
                                        if (dtSubLedgers != null && dtSubLedgers.Rows.Count > 0)
                                        {
                                            //Insert/Update Sub Ledgers
                                            resultArg = importsystem.ImportSubLedger(dtSubLedgers);
                                            if (resultArg.Success)
                                            {
                                                //Clear and Insert Sub Ledgers Vouchers
                                                resultArg = importsystem.ImportVoucherSubLedgers(dtSubLedgerVouchers, branchOfficeId, 0);
                                            }
                                        }
                                    }
                                    rtnmessage = resultArg.Message;
                                }
                            }
                            else
                            {
                                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                                throw new FaultException<AcMeServiceException>(exceptionDetails);
                            }
                        }
                    }
                    else
                    {
                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                rtnmessage = ex.Message;
            }

            drResult["Result"] = rtnmessage;
            dtResult.Rows.Add(drResult);
            return rtnmessage;
        }

        public bool UpdateProjectClosedDate(string branchOfficeCode, string headOfficeCode, string location, string projectname, string projectcloseddate)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            bool rtn = false;
            bool canUpdaeClosedDate = true;
            string projectstartdate = "";
            ResultArgs resultArgs = new ResultArgs();

            AcMeServiceException exceptionDetails = new AcMeServiceException();
            try
            {
                rtn = ValidateBranch(branchOfficeCode, headOfficeCode, location);
                if (rtn)
                {
                    //Get Project Id and its Start date
                    int projectid = 0;

                    using (ProjectSystem projectsystem = new ProjectSystem(projectid, DataBaseType.HeadOffice))
                    {
                        resultArgs = projectsystem.FetchProjectIdByProjectName(projectname, DataBaseType.HeadOffice);
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtProject = resultArgs.DataSource.Table;
                            projectid = objBase.Member.NumberSet.ToInteger(dtProject.Rows[0][projectsystem.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                            if (projectid > 0)
                            {
                                resultArgs = projectsystem.FetchProjectDetailsById(projectid, DataBaseType.HeadOffice);
                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    dtProject = resultArgs.DataSource.Table;
                                    projectstartdate = dtProject.Rows[0][projectsystem.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString();
                                }
                            }
                        }
                    }

                    //Check Transactions are avilable or not
                    if (projectid > 0)
                    {

                        DateTime staratdate = objBase.Member.DateSet.ToDate(projectstartdate, false);
                        DateTime closeddate = string.IsNullOrEmpty(projectcloseddate) ? DateTime.MinValue : objBase.Member.DateSet.ToDate(projectcloseddate, false);

                        if (!String.IsNullOrEmpty(projectstartdate))
                        {
                            if (closeddate != DateTime.MinValue)
                            {
                                if (!objBase.Member.DateSet.ValidateDate(staratdate, closeddate))
                                {
                                    canUpdaeClosedDate = false;
                                    exceptionDetails.Message = "Project Closed date ('" + closeddate + "') cannot be less than Started On ('" + staratdate + "') in Acmeerp Portal.";
                                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                                }

                                if (canUpdaeClosedDate && projectid > 0)
                                {
                                    using (VoucherTransactionSystem vouchertranssystem = new VoucherTransactionSystem())
                                    {
                                        resultArgs = vouchertranssystem.CheckTransVoucherDetailsByDateProject(projectid, closeddate);
                                        if (resultArgs.Success && resultArgs.DataSource.Sclar.ToInteger > 0)
                                        {
                                            canUpdaeClosedDate = false;
                                            exceptionDetails.Message = "Transaction is made for this Closed date ('" + closeddate + "') , Project can not be closed.";
                                            throw new FaultException<AcMeServiceException>(exceptionDetails);
                                        }
                                    }
                                }
                            }

                            if (canUpdaeClosedDate)
                            {
                                using (ProjectSystem projectsystem = new ProjectSystem())
                                {
                                    projectsystem.ProjectId = projectid;
                                    projectsystem.Closed_On = closeddate;
                                    resultArgs = projectsystem.UpdateProjectClosedDate();
                                    if (resultArgs.Success)
                                    {
                                        rtn = true;
                                    }
                                    else
                                    {
                                        exceptionDetails.Message = resultArgs.Message;
                                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.ProjectNotAvailable;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                rtn = false;
                resultArgs.Message = ex.Message;
                resultArgs.Success = false;
                throw ex;
            }
            catch (Exception e)
            {
                rtn = false;
                resultArgs.Message = e.Message;
                resultArgs.Success = false;

                exceptionDetails.Message = e.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            return rtn;
        }

        /// <summary>
        /// On 22/03/2022, To get Local community cleint details like license code, head office code, branch office code, ip and mac address
        /// 1. Update all these detaisl against Local community license into super admin
        /// 2. Generate local community LC ky dell.
        /// 3. send to Branch Local Community. 
        /// </summary>
        /// <param name="licensecode"></param>
        /// <param name="hocode"></param>
        /// <param name="bocode"></param>
        /// <param name="clientip"></param>
        /// <param name="clientmacaddress"></param>
        /// <returns></returns>
        public DataTable RequestLocalCommunityKey(string licensekey, string hocode, string bocode, string location, string clientip, string clientmacaddress, string localcommunityuser)
        {
            LCBranchModuleStatus lcbranchModuleStatus = LCBranchModuleStatus.Disabled;
            byte[] resultDLLFile = new byte[0];
            string rtn = string.Empty;
            string lcrequestcode = string.Empty;
            AcMeERP.Base.UIBase objBase = new UIBase();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs result = new ResultArgs();
            DataTable dtReturnData = new DataTable("RequestEnableStatus");
            dtReturnData.Columns.Add(objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName, typeof(System.String));
            dtReturnData.Columns.Add(objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName, typeof(System.Int32));
            dtReturnData.Columns.Add("DATA", typeof(System.Byte[]));
            dtReturnData.Columns.Add(objBase.AppSchema.LcBranchEnableTrackModules.RETURN_MESSAGEColumn.ColumnName, typeof(System.String));

            try
            {
                if (ValidateBranch(bocode, hocode, location))
                {
                    using (LicenseSystem licensesys = new LicenseSystem())
                    {
                        //Check and get already requests details
                        result = licensesys.FetchLCBranchClientEnableModuleRequestsByBranch(hocode, bocode, location);
                        if (result.Success && result.DataSource.Table != null)
                        {
                            DataTable dtLCRequestedDetails = result.DataSource.Table;
                            result = MakeLCBranchRequest(dtLCRequestedDetails, licensekey, hocode, bocode, location, clientip, clientmacaddress, localcommunityuser);

                            //Generate encrypted key to enable request module
                            if (result.Success && result.ReturnValue != null)
                            {
                                lcbranchModuleStatus = (LCBranchModuleStatus)result.ReturnValue;
                                lcrequestcode = result.DataSource.Data.ToString();

                                if (string.IsNullOrEmpty(result.Message))
                                {
                                    if (lcbranchModuleStatus == LCBranchModuleStatus.Approved)
                                    {
                                        //Generate Key
                                        resultDLLFile = CreateLocalCommunityEnableModuleKey(lcrequestcode, licensekey, hocode, bocode, location, clientip, clientmacaddress);
                                        if (resultDLLFile.Length == 0)
                                        {
                                            rtn = "Not able to complete all the request process";
                                        }
                                    }
                                    else if (lcbranchModuleStatus == LCBranchModuleStatus.Requested)
                                    {
                                        rtn = "Your request is created or not yet approved in Acme.erp portal, Contact Province Economer Office.";
                                    }
                                    else if (lcbranchModuleStatus == LCBranchModuleStatus.Disabled)
                                    {
                                        rtn = "Your request is disabled (locked), Contact Province Economer Office.";
                                    }
                                }
                                else
                                {
                                    rtn = result.Message; //Our own messages
                                }
                            }
                            else
                            {
                                rtn = result.Message;
                            }
                        }
                        else
                        {
                            rtn = "Not able to make the request to enable Receipt Module to Acme.erp portal";
                        }
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                exceptionDetails.Message = ex.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            catch (Exception e)
            {
                exceptionDetails.Message = e.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            finally
            {
                //If unique request code for branch is empty, set back proper message
                if (string.IsNullOrEmpty(rtn) && string.IsNullOrEmpty(lcrequestcode))
                {
                    lcbranchModuleStatus = LCBranchModuleStatus.Disabled;
                    rtn = "Request code is not generated properly, Contact Province Economer Office";
                }

                //Send back Request status and Message
                DataRow drResult = dtReturnData.NewRow();
                drResult[objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName] = lcrequestcode;
                drResult[objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName] = lcbranchModuleStatus;
                drResult["DATA"] = resultDLLFile;
                drResult[objBase.AppSchema.LcBranchEnableTrackModules.RETURN_MESSAGEColumn.ColumnName] = rtn;
                dtReturnData.Rows.Add(drResult);

                using (LicenseSystem licensesys = new LicenseSystem())
                {
                    if (lcbranchModuleStatus == LCBranchModuleStatus.Requested)
                    {
                        licensesys.SendModuleRequestAndApproved(bocode, location, localcommunityuser, LCBranchModuleStatus.Requested);
                    }
                }
            }

            return dtReturnData.DefaultView.ToTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtLCBranchRequestedList"></param>
        /// <param name="licensekey"></param>
        /// <param name="hocode"></param>
        /// <param name="bocode"></param>
        /// <param name="location"></param>
        /// <param name="clientip"></param>
        /// <param name="clientmacaddress"></param>
        /// <param name="localcommunityuser"></param>
        /// <returns></returns>
        private ResultArgs MakeLCBranchRequest(DataTable dtLCBranchRequestedList, string licensekey, string hocode, string bocode,
                    string location, string clientip, string clientmacaddress, string localcommunityuser)
        {
            ResultArgs result = new ResultArgs();
            AcMeERP.Base.UIBase objBase = new UIBase();
            AcMeServiceException exceptionDetails = new AcMeServiceException();

            try
            {
                if (dtLCBranchRequestedList != null)
                {
                    using (LicenseSystem licensesys = new LicenseSystem())
                    {
                        result = licensesys.GetBranchOfficeLicense(bocode, location); //Get Branch Recent License details
                        if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtBranchLicenseDetails = result.DataSource.Table;
                            string branchdepolymenttype = CommonMember.DecryptValue(dtBranchLicenseDetails.Rows[0][objBase.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName].ToString());

                            if (branchdepolymenttype == DeploymentType.ClientServer.ToString())
                            {
                                string clientdetails = objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName + " = '" + clientip + "' AND " +
                                                       objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn.ColumnName + " = '" + clientmacaddress + "'";

                                dtLCBranchRequestedList.DefaultView.RowFilter = clientdetails;
                                result = CreateLCRequestLCBranchRequests(dtLCBranchRequestedList.DefaultView.ToTable(), licensekey, hocode, bocode, location, clientip, clientmacaddress, localcommunityuser);
                            }
                            else //For Standalone, check its status
                            {
                                result = CreateLCRequestLCBranchRequests(dtLCBranchRequestedList, licensekey, hocode, bocode, location, clientip, clientmacaddress, localcommunityuser);
                            }
                        }
                        else
                        {
                            result.Message = "Branch License details are not available.";
                        }
                    }
                }
                else
                {
                    result.Message = "Local Community Branch requested details are not found.";
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                exceptionDetails.Message = ex.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            catch (Exception e)
            {
                exceptionDetails.Message = e.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }

            return result;
        }

        private ResultArgs CreateLCRequestLCBranchRequests(DataTable dtLCBranchRequestedList, string licensekey, string hocode, string bocode,
                    string location, string clientip, string clientmacaddress, string localcommunityuser)
        {
            string RecentLCUniqueRequestCode = string.Empty;
            string LCUniqueRequestCode = string.Empty;
            string lockedrequestFitler = string.Empty;
            string clientdetailsFitler = string.Empty;
            string branchdepolymenttype = DeploymentType.Standalone.ToString();

            ResultArgs result = new ResultArgs();
            Int32 receiptsstatus = (Int32)LCBranchModuleStatus.Disabled;
            AcMeERP.Base.UIBase objBase = new UIBase();
            AcMeServiceException exceptionDetails = new AcMeServiceException();

            try
            {
                if (dtLCBranchRequestedList != null)
                {
                    using (LicenseSystem licensesys = new LicenseSystem())
                    {
                        //1. There is no request for Branch with location, make it request and return its receipt module status
                        if (dtLCBranchRequestedList.Rows.Count == 0)
                        {
                            result = licensesys.RequestLCBranchClientEnableModuleRequests(licensekey, hocode, bocode, location, clientip, clientmacaddress, localcommunityuser);
                            if (result.Success)
                            {
                                if (result.ReturnValue != null) LCUniqueRequestCode = result.ReturnValue.ToString();
                                receiptsstatus = (Int32)LCBranchModuleStatus.Requested;
                                result.Success = true;
                            }
                        }
                        else //2. There are request already available for Branch with location, validate it
                        {
                            dtLCBranchRequestedList.DefaultView.RowFilter = string.Empty;
                            Int32 totalrequests = dtLCBranchRequestedList.Rows.Count;
                            //3. Get recent request details
                            if (dtLCBranchRequestedList.Rows.Count > 0)
                            {
                                dtLCBranchRequestedList.DefaultView.Sort = objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName + " DESC";
                                RecentLCUniqueRequestCode = dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName].ToString();
                                receiptsstatus = objBase.Member.NumberSet.ToInteger(dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName].ToString());
                            }

                            //4. Get current request detials and Check is it recent requests
                            clientdetailsFitler = objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName + " = '" + clientip + "' AND " +
                                                   objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn.ColumnName + " = '" + clientmacaddress + "'";
                            dtLCBranchRequestedList.DefaultView.Sort = objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName + " DESC";
                            dtLCBranchRequestedList.DefaultView.RowFilter = clientdetailsFitler;
                            if (dtLCBranchRequestedList.DefaultView.Count > 0 && RecentLCUniqueRequestCode == dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName].ToString())
                            {
                                LCUniqueRequestCode = dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName].ToString();
                                receiptsstatus = objBase.Member.NumberSet.ToInteger(dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName].ToString());
                                result.Success = true;
                            }
                            else //5. If there are no requests from same system, but there are many requests, check its recent recent request status information
                            {
                                //6. If all the existing requests are disabled, current request will be treated as new request
                                lockedrequestFitler = objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName + " = " + (Int32)LCBranchModuleStatus.Disabled;
                                dtLCBranchRequestedList.DefaultView.RowFilter = string.Empty;
                                dtLCBranchRequestedList.DefaultView.RowFilter = lockedrequestFitler;
                                if (totalrequests == dtLCBranchRequestedList.DefaultView.Count &&
                                    (clientip != dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName].ToString()
                                      || clientmacaddress != dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn.ColumnName].ToString()))
                                {
                                    result = licensesys.RequestLCBranchClientEnableModuleRequests(licensekey, hocode, bocode, location, clientip, clientmacaddress, localcommunityuser);
                                    if (result.Success)
                                    {
                                        if (result.ReturnValue != null) LCUniqueRequestCode = result.ReturnValue.ToString();
                                        receiptsstatus = (Int32)LCBranchModuleStatus.Requested;
                                        result.Success = true;
                                    }
                                }
                                //else if (clientip == dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName].ToString()
                                //      && clientmacaddress == dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn.ColumnName].ToString())
                                //{   //6.  If Recent request was from same client, return its details
                                //    result.Success = true;
                                //}
                                else
                                {   //7. If Recent request was from different client, return its details
                                    //(clientip != dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName].ToString()
                                    //  || clientmacaddress != dtLCBranchRequestedList.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn.ColumnName].ToString())
                                    result.Message = "Your Branch request is already available, Might be from someother computer, Contact your Province Economer Office";
                                    result.Success = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    result.Message = "Local Community Branch requested details are not found";
                }

                result.ReturnValue = null;
                if (result.Success)
                {
                    result.DataSource.Data = LCUniqueRequestCode;
                    result.ReturnValue = receiptsstatus;
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                exceptionDetails.Message = ex.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            catch (Exception e)
            {
                exceptionDetails.Message = e.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }

            return result;
        }


        /// <summary>
        /// On 22/03/2022, To get Local community cleint details like license code, head office code, branch office code, ip and mac address
        /// 1. Update all these detaisl against Local community license into super admin
        /// 2. Generate local community LC ky dell.
        /// 3. send to Branch Local Community. 
        /// </summary>
        /// <param name="licensecode"></param>
        /// <param name="hocode"></param>
        /// <param name="bocode"></param>
        /// <param name="clientip"></param>
        /// <param name="clientmacaddress"></param>
        /// <returns></returns>
        private byte[] CreateLocalCommunityEnableModuleKey(string requestcode, string licensekey, string hocode, string bocode, string location, string clientip, string clientmacaddress)
        {
            byte[] filedetails = new byte[0];
            AcMeERP.Base.UIBase objBase = new UIBase();
            bool rtn = false;
            AcMeServiceException exceptionDetails = new AcMeServiceException();

            try
            {
                rtn = ValidateBranch(bocode, hocode, location);
                if (rtn)
                {
                    ResultArgs resultarg = CommonMember.CreateLocalCommunityEnableModuleKey(requestcode, licensekey, hocode, bocode, location, clientip, clientmacaddress);
                    if (resultarg.Success)
                    {
                        string dllkeypath = Path.Combine(PagePath.MultilicensekeySettingFileName, requestcode + "_" + hocode + "_" + bocode + "_" + CommonMember.dllnameLC);
                        if (File.Exists(dllkeypath))
                        {
                            filedetails = File.ReadAllBytes(dllkeypath);
                            rtn = true;
                        }
                    }
                    else
                    {
                        rtn = false;
                        exceptionDetails.Message = resultarg.Message;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                rtn = false;
                exceptionDetails.Message = ex.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            catch (Exception e)
            {
                rtn = false;
                exceptionDetails.Message = e.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }

            return filedetails;
        }


        /// <summary>
        /// On 18/04/2022, To get Local communities receipt module rights
        /// </summary>
        /// <param name="licensekey"></param>
        /// <param name="hocode"></param>
        /// <param name="bocode"></param>
        /// <param name="location"></param>
        /// <param name="clientip"></param>
        /// <param name="clientmacaddress"></param>
        /// <returns></returns>
        public string GetLocalCommunityReceiptModuleRightStatus(string licensekey, string hocode, string bocode, string location, string clientip, string clientmacaddress)
        {
            string rtn = string.Empty;
            LCBranchModuleStatus lcbranchModuleStatus = LCBranchModuleStatus.Disabled;
            byte[] resultDLLFile = new byte[0];
            string lcrequestcode = string.Empty;
            AcMeERP.Base.UIBase objBase = new UIBase();
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            ResultArgs result = new ResultArgs();

            try
            {
                if (ValidateBranch(bocode, hocode, location))
                {
                    using (LicenseSystem licensesys = new LicenseSystem())
                    {
                        //Check and get already requests details
                        result = licensesys.FetchLCBranchClientEnableModuleRequestsByBranch(hocode, bocode, location);
                        if (result.Success && result.DataSource.Table != null)
                        {
                            DataTable dtLCRequestedDetails = result.DataSource.Table;

                            result = licensesys.GetBranchOfficeLicense(bocode, location); //Get Branch Recent License details
                            if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count > 0)
                            {
                                DataTable dtBranchLicenseDetails = result.DataSource.Table;
                                string branchdepolymenttype = CommonMember.DecryptValue(dtBranchLicenseDetails.Rows[0][objBase.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName].ToString());
                                string clientdetails = objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_LOCATIONColumn.ColumnName + " = '" + location + "' AND " +
                                                       objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_IP_ADDRESSColumn.ColumnName + " = '" + clientip + "' AND " +
                                                       objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_CLIENT_MAC_ADDRESSColumn.ColumnName + " = '" + clientmacaddress + "'";

                                dtLCRequestedDetails.DefaultView.RowFilter = clientdetails;
                                dtLCRequestedDetails.DefaultView.Sort = objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_REQUEST_CODEColumn.ColumnName + " DESC";
                                if (dtLCRequestedDetails.DefaultView.Count > 0)
                                {
                                    result.Success = true;
                                    result.ReturnValue = objBase.Member.NumberSet.ToInteger(dtLCRequestedDetails.DefaultView[0][objBase.AppSchema.LcBranchEnableTrackModules.LC_BRANCH_RECEIPT_MODULE_STATUSColumn.ColumnName].ToString());
                                }
                            }
                            else
                            {
                                result.Message = "Branch License details are not available.";
                            }
                        }
                        else
                        {
                            rtn = "Not able to make the request to enable Receipt Module to Acme.erp portal";
                        }
                    }
                }
            }
            catch (FaultException<DataSyncService.AcMeServiceException> ex)
            {
                exceptionDetails.Message = ex.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            catch (Exception e)
            {
                exceptionDetails.Message = e.Message;
                throw new FaultException<AcMeServiceException>(exceptionDetails);
            }
            finally
            {
                rtn = "Checking Receipt Module Rights.....";
                if (result.Success && result.ReturnValue != null)
                {
                    lcbranchModuleStatus = (LCBranchModuleStatus)result.ReturnValue;

                    if (lcbranchModuleStatus == LCBranchModuleStatus.Approved)
                    {
                        rtn = string.Empty;
                    }
                    else
                    {
                        rtn = lcbranchModuleStatus.ToString();
                    }
                }
            }

            return rtn;
        }

        /// <summary>
        /// On 15/05/2024, To get Vouchers for given branch, location and other than given date range
        /// It will be used for the below purpose
        /// # When export from local branch, data sync get is get failed due to records is available issue
        /// # the reason is that Voucher(s) are already available in portal for different project or different date 
        /// </summary>
        /// <param name="headOfficeCode"></param>
        /// <param name="branchOfficeCode"></param>
        /// <param name="dFrom"></param>
        /// <param name="dTo"></param>
        /// <param name="projectname"></param>
        /// <returns></returns>
        public DataTable CheckVouchersInOtherProjectsOrDates(string headOfficeCode, string branchOfficeCode,
                                                string locationname, DateTime dFrom, DateTime dTo)
        {
            DataTable dtVouchers = new DataTable("VouchersInOtherProjectsOrDates");
            AcMeERP.Base.UIBase objBase = new UIBase();
            Int32 branchOfficeId = 0;
            Int32 locationid = 0;
            string branchprojectids = string.Empty;
            ResultArgs resultArgs = new ResultArgs();
            dtVouchers = null;
            AcMeServiceException exceptionDetails = new AcMeServiceException();
            try
            {
                if (!(string.IsNullOrEmpty(headOfficeCode)) && !(string.IsNullOrEmpty(branchOfficeCode)))
                {
                    if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                    {
                        objBase.HeadOfficeCode = headOfficeCode;//To Connect Head Office Database

                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.HeadOfficeCode = headOfficeCode;
                            branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                            branchOfficeId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.HeadOffice, branchOfficeCode);

                            locationid = 0;
                            using (BranchLocationSystem branchlocation = new BranchLocationSystem())
                            {
                                locationid = branchlocation.GetLocationId(branchOfficeId, locationname);
                            }

                            using (ProjectSystem projectsystem = new ProjectSystem())
                            {
                                branchprojectids = projectsystem.FetchProjectIdByBranchLocation(branchOfficeId, locationid);
                            }

                            //branchOfficeId = 52;
                            if (branchOfficeId == 0)
                            {
                                new ErrorLog().WriteError(MessageCatalog.Message.WebServiceMessage.BranchNotAvailable);
                            }
                            //else if (projectid == 0)
                            //{
                            //    new ErrorLog().WriteError(MessageCatalog.Message.WebServiceMessage.ProjectNotAvailable);
                            //}
                            else
                            {
                                using (VoucherTransactionSystem vouchertransSystem = new VoucherTransactionSystem())
                                {
                                    resultArgs = vouchertransSystem.FetchVouchersInOtherProjectsOrDates(branchOfficeId, locationid, branchprojectids, dFrom, dTo);
                                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                    {
                                        dtVouchers = resultArgs.DataSource.Table;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                        new ErrorLog().WriteError(MessageCatalog.Message.WebServiceMessage.BranchNotAvailable);
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
                else
                {
                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                    new ErrorLog().WriteError(MessageCatalog.Message.WebServiceMessage.BranchNotAvailable);
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            catch (Exception e)
            {
                new ErrorLog().WriteError(e.Message);
                resultArgs.Message = e.Message;
                exceptionDetails.Message = e.Message;
                resultArgs.Success = false;
            }
            finally
            {
                acmeerpservicemessage = (exceptionDetails.Message == null ? string.Empty : exceptionDetails.Message);
            }

            return dtVouchers;
        }

        /// <summary>
        /// On 16/12/2020, It is common method to check given branch and location are correct and active;
        /// 
        /// It could be used for all the web methods. 
        /// It should be called in the web methods. 
        /// </summary>
        /// <param name="branchOfficeCode"></param>
        /// <param name="headOfficeCode"></param>
        /// <param name="Location"></param>
        /// <returns></returns>
        private bool ValidateBranch(string branchOfficeCode, string headOfficeCode, string Location)
        {
            bool rtn = false;
            AcMeERP.Base.UIBase objBase = new UIBase();
            ResultArgs resultArgs = new ResultArgs();

            AcMeServiceException exceptionDetails = new AcMeServiceException();
            try
            {
                if (!(string.IsNullOrEmpty(headOfficeCode)) && !(string.IsNullOrEmpty(branchOfficeCode)))
                {
                    if (IsBranchExists(headOfficeCode.Trim(), branchOfficeCode.Trim()))
                    {
                        objBase.HeadOfficeCode = headOfficeCode;//To Connect Head Office Database
                        using (BranchOfficeSystem branchOfficeSystem = new BranchOfficeSystem())
                        {
                            branchOfficeSystem.HeadOfficeCode = headOfficeCode;
                            branchOfficeSystem.BranchOfficeCode = branchOfficeCode;
                            Int32 branchOfficeId = branchOfficeSystem.FetchBranchIdByBranchCode(DataBaseType.HeadOffice, branchOfficeCode);
                            if (branchOfficeId > 0)
                            {
                                using (LicenseSystem licenseSystem = new LicenseSystem())
                                {
                                    resultArgs = licenseSystem.GetBranchOfficeLicense(branchOfficeCode, Location);
                                    if (resultArgs.Success && resultArgs.DataSource != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                    {
                                        rtn = true;
                                    }
                                    else
                                    {
                                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.LicenseNotAvailable;
                                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                                    }
                                }
                            }
                            else
                            {
                                exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                                throw new FaultException<AcMeServiceException>(exceptionDetails);
                            }
                        }
                    }
                    else
                    {
                        exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                        throw new FaultException<AcMeServiceException>(exceptionDetails);
                    }
                }
                else
                {
                    exceptionDetails.Message = MessageCatalog.Message.WebServiceMessage.BranchNotAvailable;
                    throw new FaultException<AcMeServiceException>(exceptionDetails);
                }
            }
            catch (Exception e)
            {
                rtn = false;
                resultArgs.Message = e.Message;
                resultArgs.Success = false;
            }

            return rtn;
        }
        #endregion
    }

}

