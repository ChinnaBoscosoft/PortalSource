/****************************************************************************************************************************
 * Purpose       : This is to handle business logics for branch office .
 * Created Date  : 26 April 2014
 * Modified Date : 
 * **************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;

namespace Bosco.Model.UIModel
{
    public class BranchOfficeSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;

        public DataTable dtSource { get; set; }
        #endregion

        #region Constructor
        public BranchOfficeSystem()
        {
        }
        public BranchOfficeSystem(int MessageId)
        {
            FillMessageDetails(MessageId);
        }
        public BranchOfficeSystem(int BranchOfficeId, DataBaseType connectTo)
        {
            FillBranchOfficeDetails(BranchOfficeId, connectTo);
        }
        #endregion

        #region BranchOfficeProperties
        public int BranchOfficeId { get; set; }
        public string BranchOfficeCode { get; set; }
        public string BranchOfficeName { get; set; }
        public string HeadOffice_Code { get; set; }
        public int Deployment_Type { get; set; }
        public string PhoneNo { get; set; }
        public string BranchPartCode { get; set; }
        public string ThirdParyCode { get; set; }
        public string ThirdParyMode { get; set; }
        public string ThirdParyURL { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public int Country_Id { get; set; }
        public int State_Id { get; set; }
        public string BranchEmail { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int UserCreatedStatus { get; set; }
        public string CountryCode { get; set; }
        public string BranchOfficeCodeUpdate { get; set; }
        public string BranchKeyCode { get; set; }
        public int IsSubBranch { get; set; }
        public string PersonIncharge { get; set; }
        public string AssociateBranchCode { get; set; }

        #region MessageProperties

        public int BranchId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int MessageType { get; set; }
        public int MessageId { get; set; }
        public string BranchIdCollection { get; set; }
        #endregion

        #endregion

        #region Methods

        public ResultArgs FetchBranchOfficeDetails()
        {
            return FetchBranchOfficeDetails(DataBaseType.Portal);
        }

        public ResultArgs FetchBranchOfficeDetails(DataBaseType connectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                //if (base.IsHeadOfficeUserRights) // To Set the Admin User for if set the rights admin in the database
                //{
                //    resultArgs = FetchBranchOfficeByUser(connectTo);
                //}
                // else
                //{
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchAll, connectTo))
                {
                    //fetch branch which are assigned to the particular branch user based on the login
                    if (!string.IsNullOrEmpty(base.LoginUserHeadOfficeCode))
                    {
                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, base.LoginUserHeadOfficeCode);
                        if (!string.IsNullOrEmpty(base.LoginUserBranchOfficeCode))
                            dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode.ToString().ToLower());
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                // }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    resultArgs = FetchBranchOfficeByUserView(connectTo);
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchOfficeByUser(DataBaseType connectTo) // FetchAllBranchesByUser
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchByUser, connectTo))
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

        public ResultArgs FetchBranchOfficeByUserView(DataBaseType connectTo) // FetchAllBranchesByUser
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchAllBranchesByUser, connectTo))
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

        public ResultArgs FetchBranchByProject(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.BranchByProject, connectTo))
            {
                if (!string.IsNullOrEmpty(BranchOfficeCode))
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                }

                if (!string.IsNullOrEmpty(HeadOfficeCode))
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FillMessageDetails(int messageId)
        {
            resultArgs = MessageDeatailsById(messageId, DataBaseType.HeadOffice);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                BranchIdCollection = String.Join(",", (from id in resultArgs.DataSource.Table.AsEnumerable()
                                                       select id.Field<UInt32>(this.AppSchema.SendMessage.BRANCH_IDColumn.ColumnName)));
                BranchId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.SendMessage.BRANCH_IDColumn.ColumnName].ToString());
                MessageId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.SendMessage.IDColumn.ColumnName].ToString());
                Subject = resultArgs.DataSource.Table.Rows[0][this.AppSchema.SendMessage.SUBJECTColumn.ColumnName].ToString();
                Content = resultArgs.DataSource.Table.Rows[0][this.AppSchema.SendMessage.CONTENTColumn.ColumnName].ToString();
                MessageType = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.SendMessage.TYPEColumn.ColumnName].ToString());
            }
            return resultArgs;
        }

        public ResultArgs MessageDeatailsById(int MessageId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchMessage, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.SendMessage.IDColumn, MessageId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchActiveBranch(DataBaseType ConnectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchforMasterDownload, ConnectTo))
                {
                    if (!base.IsPortalUser)
                    {
                        if (!string.IsNullOrEmpty(base.LoginUserHeadOfficeCode.Trim()))
                            dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, base.LoginUserHeadOfficeCode);
                        if (!string.IsNullOrEmpty(base.LoginUserBranchOfficeCode.Trim()))
                            dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchforMasterDownloadByUser, ConnectTo))
                    {
                        if (!base.IsPortalUser)
                        {
                            if (!string.IsNullOrEmpty(base.LoginUserHeadOfficeCode.Trim()))
                                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, base.LoginUserHeadOfficeCode);
                            if (!string.IsNullOrEmpty(base.LoginUserBranchOfficeCode.Trim()))
                                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                            if (base.LoginUserId > 0)
                                dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, base.LoginUserId);
                        }
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs IsBranchExists(string HeadOfficeCode, string BranchOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchActiveBranchs, DataBaseType.Portal))
            {
                if (!string.IsNullOrEmpty(HeadOfficeCode.Trim()))
                    dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode.Trim());
                if (!string.IsNullOrEmpty(BranchOfficeCode.Trim()))
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode.Trim());
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs GetActiveBranchInfo(string HeadOfficeCode, string BranchOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchActiveBranchs, DataBaseType.Portal))
            {
                if (!string.IsNullOrEmpty(HeadOfficeCode.Trim()))
                    dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode.Trim());
                if (!string.IsNullOrEmpty(BranchOfficeCode.Trim()))
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode.Trim());
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectBYBranch(int BranchId)
        {
            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser)
            {
                //For Head Office Admin and Branch Office Admin User
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectsForVoucherLock, DataBaseType.HeadOffice))
                {
                    if (BranchId > 0)
                    {

                        dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    //Fetch by Login User
                    resultArgs = FetchBranchProjectsbyLoginUser(BranchId);
                }
            }
            return resultArgs;
        }
        public ResultArgs FetchBranchforDownloadKey(DataBaseType ConnectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchforKeyDownload, ConnectTo))
                {
                    if (!base.IsPortalUser)
                    {
                        if (!string.IsNullOrEmpty(base.LoginUserHeadOfficeCode.Trim()))
                            dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, base.LoginUserHeadOfficeCode);
                        if (!string.IsNullOrEmpty(base.LoginUserBranchOfficeCode.Trim()))
                            dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    string BranchCodes = string.Empty;
                    if (base.LoginUserId > 0)
                    {
                        BranchCodes = GetBranchCode();
                    }
                    using (DataManager dataManager1 = new DataManager(SQLCommand.BranchOffice.FetchBranchforKeyDownloadByUser, ConnectTo))
                    {
                        if (!base.IsPortalUser)
                        {
                            if (!string.IsNullOrEmpty(base.LoginUserHeadOfficeCode.Trim()))
                                dataManager1.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, base.LoginUserHeadOfficeCode);
                            if (!string.IsNullOrEmpty(BranchCodes))
                                dataManager1.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchCodes);
                            dataManager1.DataCommandArgs.IsDirectReplaceParameter = true;
                        }
                        resultArgs = dataManager1.FetchData(DataSource.DataTable);
                    }
                }
            }
            return resultArgs;
        }

        public string GetBranchCode()
        {
            string rtn = string.Empty;
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchforKeyDownloadByUserId, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, base.LoginUserId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                rtn = resultArgs.DataSource.Table.Rows[0]["BRANCH_OFFICE_CODE"].ToString();
                rtn = rtn.Replace(",", "','");
                rtn = rtn.Substring(1, rtn.Length - 3);
            }
            return rtn;
        }
        public ResultArgs IsValidOffice(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchAll, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOffice_Code);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchOfficeView(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.View, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAllBranchsToExport(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchAllToExport, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchOfficeToBeApproved(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchAllBranchOffice, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchOfficeApprovedByHeadoffice(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchAllBranchOfficeByHeadOffice, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOffice_Code);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 02/12/2020, To show Renewal Branches for given Days
        /// 
        /// Skip All SDB HOs, Bosco Demo and License generated before 01/04/2017
        /// </summary>
        public ResultArgs FetchRenewalBranchOfficeByDays(DataBaseType connectTo)
        {
            //Skip All SDB HOs, Bosco Demo and License generated before 01/04/2017
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchRenewalBranchOfficeByDays, connectTo))
            {
                if (!String.IsNullOrEmpty(HeadOffice_Code))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOffice_Code);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        /// <summary>
        /// On 12/05/2022, To get Branchs which is using more than one system in local Communities
        /// </summary>
        /// <param name="connectTo"></param>
        /// <returns></returns>
        public ResultArgs FetchBranchHistoryMoreThanOneSystem(DataBaseType connectTo, bool isLocationbased, string hocode = "")
        {
            SQLCommand.BranchOffice cmd = SQLCommand.BranchOffice.FetchBranchHistoryMoreThanOneSystemByBranch;

            if (isLocationbased)
            {
                cmd = SQLCommand.BranchOffice.FetchBranchHistoryMoreThanOneSystemByBranchLocation;
            }

            using (DataManager dataManager = new DataManager(cmd, connectTo))
            {
                if (!string.IsNullOrEmpty(hocode))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, hocode);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs FetchBranch(DataBaseType connectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranch, connectTo))
                {
                    if (!string.IsNullOrEmpty(BranchOfficeCode))
                    {
                        dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    resultArgs = FetchBranchOfficeByUser(connectTo);
                }
            }
            return resultArgs;

        }

        public ResultArgs FetchBranchbyLocations(DataBaseType connectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchbyLocations, connectTo))
                {
                    if (!string.IsNullOrEmpty(BranchOfficeCode))
                    {
                        dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    resultArgs = FetchBranchOfficeByUser(connectTo);
                }
            }
            return resultArgs;

        }

        public ResultArgs FetchBranchbyGraceDays(DataBaseType connectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchbyGracedays, connectTo))
                {
                    if (!string.IsNullOrEmpty(BranchOfficeCode))
                    {
                        dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    resultArgs = FetchBranchOfficeByUser(connectTo);
                }
            }
            return resultArgs;
        }

        public ResultArgs InsertUpdateGraceDays(DataBaseType connectTo)
        {
            resultArgs = DeleteGraceDays();
            if (resultArgs.Success)
            {
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    foreach (DataRow drRow in dtSource.Rows)
                    {
                        using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.InsertGraceDays, connectTo))
                        {
                            dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.BRANCH_IDColumn, this.NumberSet.ToInteger(drRow["BRANCH_OFFICE_ID"].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.LOCATION_IDColumn, this.NumberSet.ToInteger(drRow["LOCATION_ID"].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.ENFORCE_GRACE_DAYSColumn, drRow["ENFORCE_GRACE_DAYS"].ToString() == "Yes" ? 2 : 1);
                            dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.GRACE_DAYSColumn, this.NumberSet.ToInteger(drRow["GRACE_DAYS"].ToString()));
                            dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.GRACE_TMP_DATE_FROMColumn, drRow["GRACE_TMP_DATE_FROM"] != DBNull.Value ? this.DateSet.ToDateTime(drRow["GRACE_TMP_DATE_FROM"].ToString(), Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true) : null);   //  DateSet.ToDate(drRow["GRACE_TMP_DATE_FROM"].ToString(), false) : null);
                            dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.GRACE_TMP_DATE_TOColumn, drRow["GRACE_TMP_DATE_TO"] != DBNull.Value ? this.DateSet.ToDateTime(drRow["GRACE_TMP_DATE_TO"].ToString(), Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true) : null);
                            dataManager.Parameters.Add(this.AppSchema.BranchVoucherGraceDays.GRACE_TMP_VALID_UPTOColumn, drRow["GRACE_TMP_VALID_UPTO"] != DBNull.Value ? this.DateSet.ToDateTime(drRow["GRACE_TMP_VALID_UPTO"].ToString(), Bosco.Utility.DateFormatInfo.MySQLFormat.DateFormat, true) : null);
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            resultArgs = dataManager.UpdateData();
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }

                    }
                }
            }
            return resultArgs;

        }

        public ResultArgs DeleteGraceDays()
        {
            try
            {
                using (DataManager Datamanager = new DataManager(SQLCommand.BranchOffice.DeleteGraceDays, DataBaseType.HeadOffice))
                {
                    resultArgs = Datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return resultArgs;
        }
        public ResultArgs FetchBranchByBudget(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchByBudget, connectTo))
            {
                if (!string.IsNullOrEmpty(BranchOfficeCode))
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranch()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranch, DataBaseType.HeadOffice))
            {
                if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
                {
                    if (!string.IsNullOrEmpty(BranchOfficeCode))
                    {
                        dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                else
                {
                    if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                    {
                        resultArgs = FetchBranchLoginUser();
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchMailIdByBranchId(int BranchId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchMailByBranch, DataBaseType.HeadOffice))
            {
                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMailIdByBranchCode(string branchcode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchMailByBranchCode, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, branchcode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBudgetProject(int BranchId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBudgetProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBrachByHeadOffice()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchbyHeadOffice, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteBranchOfficeDetails(DataBaseType ConnectTo, string BranchOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.Delete, ConnectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteBranchLicenseDetails(DataBaseType ConnectTo, int BranchId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.DeleteLicenseByBranch, ConnectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public int FetchBranchIdByBranchCode(DataBaseType ConnectTo, string BranchOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchIdByBranchCode, ConnectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs UpdateOfficeStatus(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.Status, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.UpdateData();
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
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.UpdateUserStatus, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        /// <summary>
        /// This is to fetch branch office admin details from portal data base inorder to move to head office data base during branch office activation
        /// </summary>
        /// <param name="connectTo"></param>
        /// <returns></returns>
        public ResultArgs FetchBranchOfficeAdmin(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBOAUser, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }

        public ResultArgs SaveBranchOfficeDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((BranchOfficeId == 0) ? SQLCommand.BranchOffice.Add : SQLCommand.BranchOffice.Update, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchOfficeId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn, BranchOfficeName);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn, BranchEmail);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOffice_Code);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn, Deployment_Type);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_IDColumn, Country_Id);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.STATE_IDColumn, State_Id);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CITYColumn, City);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.PINCODEColumn, PinCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.PHONE_NOColumn, PhoneNo);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn, BranchPartCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.THIRDPARTY_CODEColumn, ThirdParyCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MOBILE_NOColumn, MobileNo);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CREATED_DATEColumn, CreatedDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CREATED_BYColumn, LoginUserId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_DATEColumn, ModifiedDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_BYColumn, LoginUserId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.STATUSColumn, Status);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CODEColumn, BranchOfficeCodeUpdate);
                dataManager.Parameters.Add(this.AppSchema.Branch_License.BRANCH_KEY_CODEColumn, BranchKeyCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.INCHARGE_NAMEColumn, PersonIncharge);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.IS_SUBBRANCHColumn, IsSubBranch);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.ASSOCIATE_BRANCH_CODEColumn, AssociateBranchCode);

                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveUserUpdateDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.UpdateUserInfo, connectTo))
            {
                int BranchUserId = GetBranchUserId(DataBaseType.HeadOffice);
                dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, BranchUserId);
                dataManager.Parameters.Add(AppSchema.User.FIRSTNAMEColumn, BranchOfficeCode);
                dataManager.Parameters.Add(AppSchema.User.ADDRESSColumn, Address);
                dataManager.Parameters.Add(AppSchema.User.CONTACT_NOColumn, MobileNo);
                dataManager.Parameters.Add(AppSchema.User.CITYColumn, City);
                dataManager.Parameters.Add(AppSchema.User.EMAIL_IDColumn, BranchEmail);
                dataManager.Parameters.Add(AppSchema.User.BRANCH_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveMessageDetails()
        {
            using (DataManager dataManager = new DataManager((MessageId == 0) ? SQLCommand.BranchOffice.SendMessage : SQLCommand.BranchOffice.UpdateMessage, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.SendMessage.IDColumn, MessageId);
                dataManager.Parameters.Add(this.AppSchema.SendMessage.DATEColumn, DateTime.Today);
                dataManager.Parameters.Add(this.AppSchema.SendMessage.SUBJECTColumn, Subject, true);
                dataManager.Parameters.Add(this.AppSchema.SendMessage.CONTENTColumn, Content);
                dataManager.Parameters.Add(this.AppSchema.SendMessage.TYPEColumn, MessageType);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveBranchMessage()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.AddMessage, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.SendMessage.MESSAGE_IDColumn, MessageId);
                dataManager.Parameters.Add(this.AppSchema.SendMessage.BRANCH_IDColumn, BranchId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteMailToBranches(int mailId)
        {
            try
            {
                using (DataManager Datamanager = new DataManager(SQLCommand.BranchOffice.DeleteMessageBranch, DataBaseType.HeadOffice))
                {
                    Datamanager.Parameters.Add(this.AppSchema.SendMessage.MESSAGE_IDColumn, mailId);
                    resultArgs = Datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchAllMessageDetail()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.ViewMessageDetail, DataBaseType.HeadOffice))
            {
                if (base.IsBranchOfficeAdminUser)
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchBroadCastMessage()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.ViewMessageDetail, DataBaseType.HeadOffice))
            {
                if (!string.IsNullOrEmpty(BranchOfficeCode))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FillBranchOfficeDetails(int BranchOfficeId, DataBaseType connectTo)
        {
            resultArgs = BranchOfficeDetailsById(BranchOfficeId, connectTo);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                BranchOfficeCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                BranchOfficeName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn.ColumnName].ToString();
                HeadOffice_Code = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                BranchEmail = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn.ColumnName].ToString();
                Country_Id = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.COUNTRY_IDColumn.ColumnName].ToString());
                State_Id = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.STATE_IDColumn.ColumnName].ToString());
                City = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.CITYColumn.ColumnName].ToString();
                PinCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.PINCODEColumn.ColumnName].ToString();
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.ADDRESSColumn.ColumnName].ToString();
                PhoneNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.PHONE_NOColumn.ColumnName].ToString();
                BranchPartCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn.ColumnName].ToString();
                ThirdParyCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.THIRDPARTY_CODEColumn.ColumnName].ToString();
                MobileNo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.MOBILE_NOColumn.ColumnName].ToString();
                CountryCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.COUNTRY_CODEColumn.ColumnName].ToString();
                Deployment_Type = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn.ColumnName].ToString());
                UserCreatedStatus = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.USER_CREATED_STATUSColumn.ColumnName].ToString());
                Status = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.STATUSColumn.ColumnName].ToString());
                BranchKeyCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Branch_License.BRANCH_KEY_CODEColumn.ColumnName].ToString();
                PersonIncharge = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.INCHARGE_NAMEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs BranchOfficeDetailsById(int BranchOfficeId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn.ColumnName, BranchOfficeId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn, BranchOfficeName);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn, BranchEmail);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOffice_Code);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CREATED_DATEColumn, CreatedDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CREATED_BYColumn, LoginUserId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn, Deployment_Type);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.STATE_IDColumn, State_Id);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CITYColumn, City);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.PINCODEColumn, PinCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_IDColumn, Country_Id);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.PHONE_NOColumn, PhoneNo);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn, BranchPartCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.THIRDPARTY_CODEColumn, ThirdParyCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MOBILE_NOColumn, MobileNo);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_DATEColumn, ModifiedDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_BYColumn, LoginUserId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.STATUSColumn, Status);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs BranchOfficeDetailsByCode(string BranchOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchByBranchCode))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn, BranchOfficeName);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn, BranchEmail);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOffice_Code);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CREATED_DATEColumn, CreatedDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CREATED_BYColumn, LoginUserId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn, Deployment_Type);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.STATE_IDColumn, State_Id);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CITYColumn, City);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.PINCODEColumn, PinCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_IDColumn, Country_Id);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.PHONE_NOColumn, PhoneNo);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn, BranchPartCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MOBILE_NOColumn, MobileNo);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_DATEColumn, ModifiedDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_BYColumn, LoginUserId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.IS_SUBBRANCHColumn, IsSubBranch);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public bool IsSubBranchInfo()
        {
            bool isSubBranch = false;
            resultArgs = BranchOfficeDetailsByCode(BranchOfficeCode);
            if (resultArgs.Success && resultArgs.RowsAffected == 1)
            {
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchOffice.IS_SUBBRANCHColumn.ColumnName].ToString().Equals("1"))
                {
                    isSubBranch = true;
                }

            }
            return isSubBranch;
        }

        public ResultArgs BranchOfficeDetailsByCodeAvailable(string BranchOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchByBranchCodeAvailable))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_NAMEColumn, BranchOfficeName);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_EMAIL_IDColumn, BranchEmail);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.HEAD_OFFICE_CODEColumn, HeadOffice_Code);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CREATED_DATEColumn, CreatedDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CREATED_BYColumn, LoginUserId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.DEPLOYMENT_TYPEColumn, Deployment_Type);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.STATE_IDColumn, State_Id);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.CITYColumn, City);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.PINCODEColumn, PinCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_IDColumn, Country_Id);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.PHONE_NOColumn, PhoneNo);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_PART_CODEColumn, BranchPartCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MOBILE_NOColumn, MobileNo);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.COUNTRY_CODEColumn, CountryCode);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_DATEColumn, ModifiedDate);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.MODIFIED_BYColumn, LoginUserId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.USER_CREATED_STATUSColumn, UserCreatedStatus);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteMappedProject(int BranchId)
        {
            try
            {
                using (DataManager Datamanager = new DataManager(SQLCommand.BranchOffice.DeleteMappedBranchtoProjects, DataBaseType.HeadOffice))
                {
                    Datamanager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    resultArgs = Datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs MapBranchtoProject(int BranchId, List<object> lProjectId, List<object> lLocationId)
        {
            resultArgs = DeleteMappedProject(BranchId);
            if (resultArgs.Success)
            {
                int i = 0;
                foreach (object ProjectId in lProjectId)
                {
                    using (DataManager datamanger = new DataManager(SQLCommand.BranchOffice.MapBranchtoProject, DataBaseType.HeadOffice))
                    {
                        datamanger.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                        //1 -Primary Location (Default)
                        datamanger.Parameters.Add(AppSchema.BranchLocation.LOCATION_IDColumn, (!string.IsNullOrEmpty(lLocationId[i].ToString()) ? lLocationId[i] : 1));
                        datamanger.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId.ToString());
                        resultArgs = datamanger.UpdateData();
                        i++;
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs MapBranch(int BranchId, List<object> lProjectId, List<object> lLocationId)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                MapBranchtoProject(BranchId, lProjectId, lLocationId);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsbyBranch(int BranchId, DataBaseType ConnectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectbyBranch, ConnectTo))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjects(int BranchId)
        {
            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser)
            {
                //For Head Office Admin and Branch Office Admin User
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectsforCombo, DataBaseType.HeadOffice))
                {
                    if (BranchId > 0)
                    {
                        dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    //Fetch by Login User
                    resultArgs = FetchBranchProjectsbyLoginUser(BranchId);
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchBudget(int BranchId, int ProjectId, bool IsTwoMonths = false)
        {
            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser)
            {
                //For Head Office Admin and Branch Office Admin User
                SQLCommand.BranchOffice sqlcmd = SQLCommand.BranchOffice.FetchBudgetforCombo;
                if (IsTwoMonths)
                {
                    sqlcmd = SQLCommand.BranchOffice.FetchBudgetforComboByTwoMonths;
                }

                using (DataManager dataManager = new DataManager(sqlcmd, DataBaseType.HeadOffice))
                {
                    if (BranchId > 0 && ProjectId > 0)
                    {
                        dataManager.Parameters.Add(AppSchema.Budget.BUDGET_IDColumn, BranchId);
                        dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    resultArgs = FetchBranchProjectBudgetsbyLoginUser(BranchId, ProjectId);
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchBudget(int BranchId, DateTime datefrom, DateTime dateto)
        {
            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser)
            {
                //For Head Office Admin and Branch Office Admin User
                SQLCommand.BranchOffice sqlcmd = SQLCommand.BranchOffice.FetchBudgetforCombo;
                if (IS_SDBINM_CONGREGATION)
                    sqlcmd = SQLCommand.BranchOffice.FetchBudgetforCombosdbinm;
                using (DataManager dataManager = new DataManager(sqlcmd, DataBaseType.HeadOffice))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    if (BranchId > 0)
                    {
                        dataManager.Parameters.Add(AppSchema.Budget.BRANCH_OFFICE_IDColumn, BranchId);
                        dataManager.Parameters.Add(AppSchema.Budget.DATE_FROMColumn, datefrom);
                        dataManager.Parameters.Add(AppSchema.Budget.DATE_TOColumn, dateto);

                        //dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDsColumn, ProjectIds);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            //else
            //{
            //    if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
            //    {
            //        resultArgs = FetchBranchProjectBudgetsbyLoginUser(BranchId, ProjectId);
            //    }
            //}
            return resultArgs;
        }

        private ResultArgs FetchBranchProjectBudgetsbyLoginUser(int BranchId, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectsforComboByLoginUser, DataBaseType.HeadOffice))
            {
                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                }
                if (base.LoginUserId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, base.LoginUserId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchBranchProjectsbyLoginUser(int BranchId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectsforComboByLoginUser, DataBaseType.HeadOffice))
            {
                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                if (base.LoginUserId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, base.LoginUserId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FetchBranchLoginUser()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchByUser, DataBaseType.HeadOffice))
            {
                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                if (base.LoginUserId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, base.LoginUserId);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSubBranchDetailsByBranchCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchSubBranches, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchByBranchPartCode(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchByBranchPartCode, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetBranchUserId(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchUserId, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchBranchLoggedHistoryByHeadOfficeCode(string HeadOfficeCode, string dtfilterdate)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchLoggedInfoByHeadOfficeCode, DataBaseType.Portal))
                {
                    if (!string.IsNullOrEmpty(HeadOfficeCode))
                    {
                        dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                    }

                    if (!string.IsNullOrEmpty(dtfilterdate))
                    {
                        dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.LOGGED_ONColumn, dtfilterdate);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    string BranchCodes = string.Empty;
                    if (base.LoginUserId > 0)
                    {
                        BranchCodes = GetBranchCode();
                    }
                    using (DataManager dataManager1 = new DataManager(SQLCommand.BranchOffice.FetchBranchLoggedInfoByHeadOfficeCodeByBranch, DataBaseType.Portal))
                    {
                        if (!string.IsNullOrEmpty(HeadOfficeCode))
                            dataManager1.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, HeadOfficeCode);

                        if (!string.IsNullOrEmpty(BranchCodes))
                            dataManager1.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchCodes);
                        if (!string.IsNullOrEmpty(dtfilterdate))
                            dataManager1.Parameters.Add(this.AppSchema.BranchLoggedHistory.LOGGED_ONColumn, dtfilterdate);
                        dataManager1.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dataManager1.FetchData(DataSource.DataTable);
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchHeadOfficewiseBranchOffice()
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchHeadOfficewiseBranchOffice, DataBaseType.Portal))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            return resultArgs;
        }


        public ResultArgs FetchHeadOfficewiseBranchOfficeDetailed()
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchHeadOfficewiseBranchOfficeDetailed, DataBaseType.Portal))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchHeadOfficewiseBranchOfficeCount()
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchHeadOfficewiseBranchOfficeCount, DataBaseType.Portal))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            return resultArgs;
        }
        public ResultArgs UpdateBranchLoggedHistory(string BranchOfficeCode, string HeadOfficeCode, string BranchName,
                                                    string HeadOfficeName, string Location, DateTime loggedDateTime, string licenseKeyNumber, string Remarks)
        {
            //1. Check branch history is available with location
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.IsExistsBranchLoggedInfo, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.LOCATIONColumn, Location);
                dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.REMARKSColumn, Remarks);

                //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            //2. insert or update branch logged history
            if (resultArgs.Success)
            {
                bool branchloggedAvailable = (resultArgs.DataSource.Table.Rows.Count > 0);
                string remarks = Remarks;
                if (remarks.Length > 150)
                {
                    remarks = remarks.Substring(0, 150);
                }
                using (DataManager dataManager = new DataManager((branchloggedAvailable ? SQLCommand.BranchOffice.UpdateBranchLoggedInfo : SQLCommand.BranchOffice.InsertBranchLoggedInfo), DataBaseType.Portal))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                    dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                    dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.BRANCH_OFFICE_NAMEColumn, BranchName);
                    dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.HEAD_OFFICE_NAMEColumn, HeadOfficeName);
                    dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.LOCATIONColumn, Location);
                    dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.LOGGED_ONColumn, loggedDateTime);
                    dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.LICENSE_KEY_NUMBERColumn, licenseKeyNumber);
                    dataManager.Parameters.Add(this.AppSchema.BranchLoggedHistory.REMARKSColumn, remarks);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            return resultArgs;
        }

        public ResultArgs SendBudgetMail(Int32 branchofficeid, string budgetname, string datefrom, string dateto, string projects, BudgetAction budetaction)
        {
            DataTable dtProjects = new DataTable();
            ResultArgs result = FetchMailIdByBranchId(branchofficeid);
            if (result.Success)
            {
                DataTable dtBranchMailInfo = result.DataSource.Table;
                if (dtBranchMailInfo.Rows.Count > 0)
                {
                    string HoCode = dtBranchMailInfo.Rows[0]["HEAD_OFFICE_CODE"].ToString();
                    string branchname = dtBranchMailInfo.Rows[0]["BRANCH"].ToString();
                    string MailId = dtBranchMailInfo.Rows[0]["MAIL_ID"].ToString();
                    // string ccMailId = "alex@boscosofttech.com,aaacdurai@gmail.com,Kali@boscoits.com,bangalore@boscosofttech.com,alwar@boscoits.com";
                    datefrom = this.DateSet.ToDate(datefrom);
                    dateto = this.DateSet.ToDate(dateto);

                    string Subject = "Budget has been uploaded to Head Office portal (" + HoCode + ")";
                    string budgetmsg = "The above Budget has been uploaded to Head Office, It will be approved by Head Office";
                    if (budetaction == BudgetAction.Approved)
                    {
                        Subject = "Budget has been approved by Head Office portal (" + HoCode + ")";
                        budgetmsg = "Your Budget has been approved by Head Office, You can make Voucher transactions";
                    }

                    string MainContent = "<b>Budget Details</b>"
                                              + "<br />"
                                              + "Branch Office Name : " + branchname + " <br />"
                                              + "Budget Name        : " + budgetname + " <br />"
                        // + "Date Range         : " + datefrom + " - " + dateto + "<br />"
                                              + "Projects           : " + projects + " <br />"
                                              + "<br /> <br />"
                                              + "<b>" + budgetmsg + "</b>";

                    result = AcMEDSync.Common.SendEmail(CommonMethod.GetFirstValue(MailId), "", Subject, MainContent, true);
                }
                else
                {
                    result.Message = "Budget mail information is not found";
                }
            }
            return result;
        }

        #region Map Projects AND Branch to User
        public ResultArgs MapProjectByUser(int UserId, List<object> lProjectId)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                MapProjectToUser(UserId, lProjectId);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs MapProjectToUser(int UserId, List<object> lProjectId)
        {
            resultArgs = DeleteMappedProjectToUser(UserId);
            if (resultArgs.Success)
            {
                int i = 0;
                foreach (object ProjectId in lProjectId)
                {
                    using (DataManager datamanger = new DataManager(SQLCommand.BranchOffice.MapProjectToUser, DataBaseType.HeadOffice))
                    {
                        datamanger.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                        datamanger.Parameters.Add(AppSchema.User.USER_TYPEColumn, (base.IsHeadOfficeAdminUser ? (int)UserType.HeadOffice : (int)UserType.BranchOffice));
                        datamanger.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId.ToString());
                        resultArgs = datamanger.UpdateData();
                        i++;
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteMappedProjectToUser(int UserId)
        {
            try
            {
                using (DataManager Datamanager = new DataManager(SQLCommand.BranchOffice.DeleteMappedUsertoProjects, DataBaseType.HeadOffice))
                {
                    Datamanager.Parameters.Add(this.AppSchema.User.USER_IDColumn, UserId);
                    resultArgs = Datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return resultArgs;
        }


        // -----------------BRANCH TO USER MAPPED 
        public ResultArgs MapBranchByUser(int UserId, List<object> lBranchId)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                MapBranchToUser(UserId, lBranchId);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs MapBranchToUser(int UserId, List<object> lBranchId)
        {
            resultArgs = DeleteMappedBranchToUser(UserId);
            if (resultArgs.Success)
            {
                int i = 0;
                foreach (object branchid in lBranchId)
                {
                    using (DataManager datamanger = new DataManager(SQLCommand.BranchOffice.MapBranchToUser, DataBaseType.HeadOffice))
                    {
                        datamanger.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                        datamanger.Parameters.Add(AppSchema.User.USER_TYPEColumn, (base.IsHeadOfficeAdminUser ? (int)UserType.HeadOffice : (int)UserType.BranchOffice));
                        datamanger.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, branchid.ToString());
                        resultArgs = datamanger.UpdateData();
                        i++;
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteMappedBranchToUser(int UserId)
        {
            try
            {
                using (DataManager Datamanager = new DataManager(SQLCommand.BranchOffice.DeleteMappedUsertoBranch, DataBaseType.HeadOffice))
                {
                    Datamanager.Parameters.Add(this.AppSchema.User.USER_IDColumn, UserId);
                    resultArgs = Datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return resultArgs;
        }


        public ResultArgs FetchProjectsByHeadOfficeUsers(int UserId, DataBaseType ConnectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectbyHeadOfficeUsers, ConnectTo))
                {
                    dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectbyHeadOfficeUsersFilter, ConnectTo))
                    {
                        dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchByHeadOfficeUsers(int UserId, DataBaseType ConnectTo)
        {
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchbyHeadOfficeUsers, ConnectTo))
                {
                    dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchbyHeadOfficeUsersFilter, ConnectTo))
                    {
                        dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsByBranchOfficeUsers(int UserId, DataBaseType ConnectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectbyBranchOfficeUsers, ConnectTo))
            {
                dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchByBranchOfficeUsers(int UserId, DataBaseType ConnectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchBranchbyBranchOfficeUsers, ConnectTo))
            {
                dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, UserId);
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #endregion
    }
}
