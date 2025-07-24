using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Runtime.InteropServices;

namespace Bosco.Model.UIModel
{
    public class ProjectSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;

        #endregion

        #region Constructor
        public ProjectSystem()
        {
        }

        public ProjectSystem(int ProjectId, DataBaseType connectTo)
        {
            FillProjectProperties(ProjectId, connectTo);
        }
        #endregion

        #region Project Properties
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public DateTime AccountDate { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime Closed_On { get; set; }
        //public string ClosedOn { get; set; }
        public string Description { get; set; }
        public int ProjectVoucherId { get; set; }
        public int VoucherId { get; set; }
        public string Notes { get; set; }
        public int ProjectCategroyId { get; set; }
        public int LedgerId { get; set; }
        public int SocietyId { get; set; }
        public DataTable dtProjectVouchers { get; set; }
        public string VoucherProjectId { get; set; }
        public int MapProjectId { get; set; }
        public DataTable dtMapLedger { get; set; }
        public string Branch_Office_Code { get; set; }
        public int BranchId { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchDivision(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchDivision, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherTypes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchVoucherTypes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchProjectCodes(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectCodes, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchSocietyNames()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.FetchSocieties, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDefaultProjectVouchers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchDefaultProjectVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_IDColumn, VoucherProjectId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        //public ResultArgs FetchVoucherTypes(string VoucherId)
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchAvailableVouchers))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.VoucherTransaction.VOUCHER_IDColumn, VoucherId);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs FetchProjectlistDetails(int BranchId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetProjectCount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.ProjectCount, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public int GetProjectCategoryViseProjectCount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.ProjectCategoryViseProjectCount, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_CATEGORY_IDColumn, ProjectCategroyId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs SaveProject(DataBaseType connectTo)
        {
            using (DataManager projectdataManager = new DataManager())
            {
                // projectdataManager.BeginTransaction();
                resultArgs = SaveProjectDetails(projectdataManager, connectTo);
                //if (resultArgs.Success & resultArgs.RowsAffected >0)
                //{
                //    MapProjectId = MapProjectId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapProjectId;
                //    resultArgs = MapProject(projectdataManager);
                //}
                // projectdataManager.EndTransaction();
            }
            return resultArgs;
        }


        private ResultArgs MapProject(DataManager dataManagers)
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.ProjectId = MapProjectId;
                mappingSystem.OpeningBalanceDate = BookBeginFrom;
                mappingSystem.dtLedgerIDCollection = dtMapLedger;
                if (dtMapLedger.Rows.Count > 0 && dtMapLedger != null)
                    resultArgs = mappingSystem.AccountMappingLedger(dataManagers);
            }
            return resultArgs;
        }
        public ResultArgs SaveProjectDetails(DataManager projectDataManager, DataBaseType connecTo)
        {
            using (DataManager dataManager = new DataManager(ProjectId == 0 ? SQLCommand.Project.Add : SQLCommand.Project.Update, connecTo))
            {
                dataManager.Database = projectDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId, true);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CODEColumn, ProjectCode);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECTColumn, ProjectName);
                if (AccountDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.ACCOUNT_DATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.ACCOUNT_DATEColumn, AccountDate);
                }
                dataManager.Parameters.Add(this.AppSchema.Project.DESCRIPTIONColumn, Description);
                if (StartedOn == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_STARTEDColumn, StartedOn);
                }
                if (Closed_On == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, Closed_On);
                }
                dataManager.Parameters.Add(this.AppSchema.Project.DIVISION_IDColumn, DivisionId);
                dataManager.Parameters.Add(this.AppSchema.Project.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_CATEGORY_IDColumn, ProjectCategroyId);
                dataManager.Parameters.Add(this.AppSchema.SOCIETY.CUSTOMERIDColumn, SocietyId);

                //On 05/07/2023 - To update closed by
                dataManager.Parameters.Add(this.AppSchema.Project.CLOSED_BYColumn, (Closed_On != null && Closed_On != DateTime.MinValue ? 1 : 0));

                resultArgs = dataManager.UpdateData();
                //if (ProjectId == 0)
                //{
                //    if (resultArgs.Success)
                //    {
                //        this.ProjectId = ProjectId == 0 ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : ProjectId;
                //        for (int i = 1; i <= 4; i++)
                //        {
                //            VoucherId = i;
                //            resultArgs = SaveProjectVoucherDetails(dataManager);
                //            if (!resultArgs.Success)
                //                break;
                //        }
                //    }
                //}
                return resultArgs;
            }
        }

        public ResultArgs SaveProjectVoucherDetails(DataManager voucherDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.AddProjectVouchers))
            {
                dataManager.Database = voucherDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.VOUCHER_IDColumn, VoucherId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteProjectDetails(int ProjectId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager())
            {

                this.ProjectId = ProjectId;
                if (HasBalance() == 0)
                {
                    dataManager.BeginTransaction();
                    resultArgs = DeleteProjectInformation();
                    dataManager.EndTransaction();
                }
                else
                {
                    resultArgs.Message = "Project is running currently and Vouchers available.";
                }
            }
            return resultArgs;

        }

        private ResultArgs DeleteProjectInformation()
        {
            resultArgs = DeleteProjectCostCentre();
            if (resultArgs.Success)
            {
                resultArgs = DeleteProjectLedger();
                if (resultArgs.Success)
                {
                    resultArgs = DeleteProjectBranch();
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteProject();
                        if (!resultArgs.Success)
                            resultArgs.Message = "Proejct is not Deleted";
                    }
                    else
                    {
                        resultArgs.Message = "Project Branch is not Deleted";
                    }
                }
                else
                {
                    resultArgs.Message = "Project Ledger is not Deleted";
                }
            }
            else
            {
                resultArgs.Message = "Project CostCentre is not Deleted";
            }
            return resultArgs;
        }

        private int HasBalance()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.ProjectBalance, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        private ResultArgs DeleteProject()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteProjectCostCentre()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectCostCentre, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteProjectLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectLedger, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteProjectCategoryLedger()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectCategoryLedger, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs DeleteProjectBranch()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectBranch, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchProjects(DataBaseType connectTo)
        {
            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser)
            {
                //For Head Office Admin and Branch Office Admin User
                using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchAll, connectTo))
                {
                    if (BranchId != 0)
                        dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    //Fetch by Login User
                    resultArgs = FetchProjectsByLoginUser(DataBaseType.HeadOffice);
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsWithBranch(DataBaseType connectTo)
        {
            //  if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser) && base.IsHeadOfficeUserRights == false)
            if ((base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsAdminUser) && base.IsHeadOfficeUserRights == false)
            {
                //For Head Office Admin and Branch Office Admin User
                using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchAllWithBranch, connectTo))
                {
                    if (BranchId != 0)
                        dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    //Fetch by Login User
                    resultArgs = FetchProjectsByLoginUser(DataBaseType.HeadOffice);
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchProjectsByLoginUser(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectByUser, connectTo))
            {
                if (base.LoginUserId > 0)
                    dataManager.Parameters.Add(AppSchema.User.USER_IDColumn, base.LoginUserId);
                //dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsForCombo(int BranchId)
        {
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.BranchOffice.FetchProjectsforCombo, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVouchers(DataBaseType connectTo)
        {
            using (DataManager dataManger = new DataManager(SQLCommand.Project.FetchVouchers, connectTo))
            {
                resultArgs = dataManger.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private ResultArgs FillProjectProperties(int ProjectId, DataBaseType connectTo)
        {
            resultArgs = FetchProjectDetailsById(ProjectId, connectTo);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
            {
                ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString());
                ProjectCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_CODEColumn.ColumnName].ToString();
                ProjectName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                ProjectCategroyId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.PROJECT_CATEGORY_IDColumn.ColumnName].ToString());
                DivisionId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Division.DIVISION_IDColumn.ColumnName].ToString());
                DivisionName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Division.DIVISIONColumn.ColumnName].ToString();
                SocietyId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.LegalEntity.CUSTOMERIDColumn.ColumnName].ToString());
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.ACCOUNT_DATEColumn.ColumnName] != DBNull.Value)
                {
                    AccountDate = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.ACCOUNT_DATEColumn.ColumnName].ToString(), false);
                }
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DATE_STARTEDColumn.ColumnName] != DBNull.Value)
                {
                    StartedOn = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DATE_STARTEDColumn.ColumnName].ToString(), false);
                }
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName] != DBNull.Value)
                {
                    Closed_On = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DATE_CLOSEDColumn.ColumnName].ToString(), false);
                }

                Description = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.DESCRIPTIONColumn.ColumnName].ToString();
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Project.NOTESColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectDetailsById(int ProjectId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AvailableVouchers(int ProjectId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.AvailableVoucher, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedProjectVouchers(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchSelectedProjectVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs ProjectVouchers(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.ProjectVoucher))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectIdByProjectName(string projectname, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectIdByProjectName, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_NAMEColumn, projectname);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteProjectVouchers(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteProjectVouchers))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgersForProject(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchLedgers, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataSet LoadProjectsDetails()
        {
            DataSet ds = new DataSet();
            resultArgs = FetchProjects(DataBaseType.HeadOffice);
            if (resultArgs.Success)
            {

                resultArgs.DataSource.Table.TableName = "Project";
                ds.Tables.Add(resultArgs.DataSource.Table);
                resultArgs = FetchLedgersForProject(DataBaseType.HeadOffice);
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Table.TableName = "Ledger";
                    ds.Tables.Add(resultArgs.DataSource.Table);
                    resultArgs = FetchVouchers(DataBaseType.HeadOffice);
                    if (resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "Voucher";
                        ds.Tables.Add(resultArgs.DataSource.Table);
                        ds.Relations.Add(ds.Tables[1].TableName, ds.Tables[0].Columns[this.AppSchema.Project.PROJECT_IDColumn.ToString()], ds.Tables[1].Columns[this.AppSchema.Project.PROJECT_IDColumn.ToString()]);
                        ds.Relations.Add(ds.Tables[2].TableName, ds.Tables[0].Columns[this.AppSchema.Project.PROJECT_IDColumn.ToString()], ds.Tables[2].Columns[this.AppSchema.ProjectVoucher.PROJECT_IDColumn.ToString()]);
                    }
                }
            }
            return ds;
        }

        public ResultArgs FetchProjectCategroy(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.ProjectCategory, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadAllLedgerByProId(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.LoadAllLedgerByProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDefaultVouchers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchDefaultVouchers))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchVoucherByProjectId(int ProjectId, string VoucherType)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchVoucherDetailsByProjectId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Voucher.VOUCHER_TYPEColumn, VoucherType);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchRecentProject(string UserId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchRecentProject))
            {
                dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, UserId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        private int CheckProjectExits(int ProId, DataBaseType connecTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.VoucherMaster.CheckProjectExist, connecTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs DeleteVoucherStatus(DataManager deleteDataManager)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.DeleteVoucher))
            {
                dataManager.Database = deleteDataManager.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success)
                {
                    if (dtProjectVouchers != null && dtProjectVouchers.Rows.Count != 0)
                    {
                        foreach (DataRow drProject in dtProjectVouchers.Rows)
                        {
                            VoucherId = drProject[AppSchema.Voucher.VOUCHER_IDColumn.ColumnName] != null ? NumberSet.ToInteger(drProject[AppSchema.Voucher.VOUCHER_IDColumn.ColumnName].ToString()) : 0;
                            resultArgs = SaveProjectVoucherDetails(dataManager);
                        }
                    }
                }
            }
            return resultArgs;
        }

        public int CheckLedgerProjectExist(int projectId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LedgerBank.CheckProjectExist, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, projectId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchProjectsDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectDetails, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveProjectVouchers()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                DeleteVoucherStatus(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs FetchAllProjects(string branchOfficeId)
        {
            ResultArgs resultArgs = null;

            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser)
            {
                //For Head Office Admin and Branch Office Admin User
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjects, DataBaseType.HeadOffice))
                {
                    if (!string.IsNullOrEmpty(branchOfficeId) && branchOfficeId != "0")
                    {
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, branchOfficeId);
                    }
                    resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    //Fetch by Login User
                    resultArgs = FetchAllProjectsByLoginUser(branchOfficeId);
                }
            }
            return resultArgs;
        }

        private ResultArgs FetchAllProjectsByLoginUser(string branchOfficeId)
        {
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectsByLoginUser, DataBaseType.HeadOffice))
            {
                if (base.LoginUserId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, base.LoginUserId);
                }
                if (!string.IsNullOrEmpty(branchOfficeId) && branchOfficeId != "0")
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, branchOfficeId);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjetBySociety(string SocietyId, string BranchCode, string ProjectCategoryId)
        {
            DataTable dtProject = new DataTable();
            ResultArgs resultArgs = null;
            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser)
            {
                //For Head Office Admin and Branch Office Admin User
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectBySociety, DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.AppSchema.SOCIETY.CUSTOMERIDColumn, SocietyId);
                    if (!string.IsNullOrEmpty(BranchCode))
                    {
                        //  dataManager.Parameters.Add(this.appSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchCode);
                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchCode);
                    }
                    if (!string.IsNullOrEmpty(ProjectCategoryId))
                    {
                        dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjectCategoryId);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
                }
            }
            else
            {
                if (base.IsHeadOfficeUser || base.IsBranchOfficeUser)
                {
                    //Fetch by Login User
                    resultArgs = FetchProjetByLoginUser(SocietyId, BranchCode);
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchProjetByBranch(string BranchId)
        {
            DataTable dtProject = new DataTable();
            ResultArgs resultArgs = null;
            if (base.IsHeadOfficeAdminUser || base.IsBranchOfficeAdminUser || base.IsHeadOfficeUser || base.IsBranchOfficeUser)
            {
                //For Head Office Admin and Branch Office Admin User
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectBySociety, DataBaseType.HeadOffice))
                {
                    if (!string.IsNullOrEmpty(BranchId))
                    {
                        dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
                }
            }
            return resultArgs;
        }

        public string FetchProjectIdByBranchLocation(int BranchId, int LocationId)
        {
            string rtn = "0";
            DataTable dtProject = new DataTable();
            ResultArgs resultArgs = new ResultArgs();
            if (LocationId == 0) LocationId = 1; //For Primary it will be 1 always
           
            //For Head Office Admin and Branch Office Admin User
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectIdByBranchLocation, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.BRANCH_IDColumn.ColumnName, BranchId);
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_IDColumn.ColumnName, LocationId);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
            }
            

            if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table!=null)
            {
                dtProject = resultArgs.DataSource.Table;

                if (dtProject.Rows.Count > 0)
                {
                    rtn = dtProject.Rows[0][AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString();
                }
            }
            return rtn;
        }
        
        private ResultArgs FetchProjetByLoginUser(string SocietyId, string BranchCode)
        {
            DataTable dtProject = new DataTable();
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectBySocietyUser, DataBaseType.HeadOffice))
            {
                if (base.LoginUserId > 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.User.USER_IDColumn, base.LoginUserId);
                }
                dataManager.Parameters.Add(this.AppSchema.SOCIETY.CUSTOMERIDColumn, SocietyId);
                if (!string.IsNullOrEmpty(BranchCode))
                {
                    //  dataManager.Parameters.Add(this.appSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchCode);
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchCode);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs ProjectsFetchAll(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.ProjectFetchAll, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, Branch_Office_Code);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MapLedgers(List<object> LedgerId, int ProjectCategoryId)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                resultArgs = MapLedgertoProjectCategory(dataManager, LedgerId, ProjectCategoryId);
                dataManager.EndTransaction();
            }

            return resultArgs;
        }

        private ResultArgs MapLedgertoProjectCategory(DataManager dManager, List<object> LedgerIds, int ProjectCategoryId)
        {
            using (DataManager DataManager = new DataManager())
            {
                DataManager.Database = dManager.Database;
                resultArgs = DeleteMappedLedger();
                if (resultArgs.Success)
                {
                    foreach (object LedgerID in LedgerIds)
                    {
                        resultArgs = MapLedger(ProjectCategoryId, this.NumberSet.ToInteger(LedgerID.ToString()));
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteMappedLedger()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.ProjectCatogory.UnmapProjectCategorytoLedger, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.Project.PROJECT_CATEGORY_IDColumn, ProjectCategroyId);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteMappedProjectCategory()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.LedgerBank.DeleteProjectCategoryByLedger, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FethcProjectIdByProjectCategory()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.Project.FetchProjectIdByProjectCategory, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.Project.PROJECT_CATEGORY_IDColumn, ProjectCategroyId);
                resultArgs = DataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public string FethcProjectIdsByProjectCategory(string projectcategoryids)
        {
            string rtn = "0";
            using (DataManager DataManager = new DataManager(SQLCommand.Project.FetchProjectIdsByProjectCategory, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.Project.PROJECT_CATEGORY_IDColumn, projectcategoryids);
                DataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = DataManager.FetchData(DataSource.Scalar);
            }

            if (resultArgs.Success && resultArgs.DataSource.Sclar != null)
            {
                rtn = resultArgs.DataSource.Sclar.ToString;
            }
            return rtn;
        }

        public ResultArgs MapLedger(int ProjectCategoryId, int LedgerId)
        {
            using (DataManager DataManager = new DataManager(SQLCommand.ProjectCatogory.MapProjectCategorytoLedger, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjectCategoryId);
                DataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                DataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = DataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs FetchProjectsByBranch(string DateFrom, string DateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchProjectByBranch, DataBaseType.HeadOffice))
            {
                DateTime BalanceDate = Convert.ToDateTime(AccountDate);

                if (BranchId > 0)
                {
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                }
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BalanceDate);
                dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchBalance(string DateFrom, string DateTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchBranchBalance, DataBaseType.HeadOffice))
            {
                DateTime BalanceDate = Convert.ToDateTime(AccountDate);
                if (!string.IsNullOrEmpty(LoginUserBranchOfficeCode))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, LoginUserBranchOfficeCode);
                }
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BalanceDate);
                dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchDashboardBranchDetails(DateTime DateFrom, DateTime DateTo)
        {
            DateTime BlDate;
            BlDate = DateFrom.AddDays(-1);
            using (DataManager dataManager = new DataManager(SQLCommand.Project.FetchBranchBalance, DataBaseType.HeadOffice))
            {
                if (!string.IsNullOrEmpty(LoginUserBranchOfficeCode))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, LoginUserBranchOfficeCode);
                }
                dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BlDate);
                dataManager.Parameters.Add(AppSchema.Project.DATE_STARTEDColumn, DateFrom);
                dataManager.Parameters.Add(AppSchema.Project.DATE_CLOSEDColumn, DateTo);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs UpdateProjectClosedDate()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Project.UpdateClosedDate, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                if (Closed_On == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(this.AppSchema.Project.DATE_CLOSEDColumn, Closed_On);
                }
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }


        #endregion

    }
}
