/********************************************************************************************
 *                                              Class      :MappingSystem.cs
 *                                              Purpose    :All the Major Logics for Mapping 
 *                                              Author     : Carmel Raj M
 *********************************************************************************************/
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Model.Transaction;
using AcMEDSync.Model;
using System.Collections.Generic;
using System;

namespace Bosco.Model.UIModel
{
    public class MappingSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        const string AMOUNT = "AMOUNT";
        const string TRANS_MODE = "TRANS_MODE";
        #endregion

        #region Properties
        public bool IsFDLedger { get; set; }
        public int ProjectId { get; set; }
        public int LedgerId { get; set; }
        public int CostCenterId { get; set; }
        public int DonorId { get; set; }
        public string Trans_mode { get; set; }
        public string FDTransType { get; set; }
        public DataTable dtLedgerIDCollection { get; set; }
        public DataTable dtMovedLedgerIDCollection { get; set; }
        public DataTable dtAmount { get; set; }
        public DataTable dtProjectIDCollection { get; set; }
        public DataTable dtCostCenterIDCollection { get; set; }
        public DataTable dtDonorMapping { get; set; }
        public DataTable dtContributionMapping { get; set; }
        public DataTable dtMappingLedger { get; set; }
        public string OpeningBalanceDate { get; set; }
        public int BranchId { get; set; }
        public int SubsidyLedger { get; set; }
        public List<object> GeneralateLedgersMapping { get; set; }
        public bool IsSubsidyLedger { get; set; }
        public string LedgerIdCollection { get; set; }

        #endregion

        #region Project System
        public ResultArgs FetchProjectsLookup()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectforLookup))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectsGridView()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchProjectForGridView))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadProjectMappingGrid(int Id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadProjectMappingGrid))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        //public ResultArgs FetchInvestedFdLedgers()
        //{
        //    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchInvestedLedger))
        //    {
        //        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
        //        resultArgs = dataManager.FetchData(DataSource.DataTable);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs LoadProjectDonorMappingGrid(int Id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadProjectDonorGrid))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadProjectCostCentreMappingGrid(int Id)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadProjectCostCentreGrid))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, Id);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedProjects()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedProject))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingProject(DataTable dtBreakUpDetails, DataTable dtFDDetailsForBreakup, BankAccountSystem FDUpdation, bool IsFdLedger)
        {
            ResultArgs IsSuccess = null;
            if (IsFdLedger)
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    //Saving FD Details
                    IsSuccess = resultArgs = FDUpdation.UpdateFD(dataManager, dtFDDetailsForBreakup);
                    if (resultArgs.Success)
                    {
                        using (BreakUpSystem breakUpSystem = new BreakUpSystem())
                        {
                            //Saving Break Up details
                            resultArgs = breakUpSystem.UpdateBreakUpDetails(dtBreakUpDetails, dataManager, FDUpdation);
                        }
                        if (resultArgs != null)
                        {
                            if (resultArgs.Success)
                            {
                                resultArgs = DeleteMappedProject(dataManager, LedgerId);
                                if (resultArgs.Success)
                                    MapProject(dataManager, dtProjectIDCollection, LedgerId);
                            }
                        }
                        else
                            resultArgs = IsSuccess;
                    }
                    dataManager.EndTransaction();
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.BeginTransaction();
                    resultArgs = DeleteMappedProject(dataManager, LedgerId);
                    if (resultArgs.Success)
                        MapProject(dataManager, dtProjectIDCollection, LedgerId);
                    dataManager.EndTransaction();
                }
            }
            return resultArgs;
        }

        private ResultArgs MapProject(DataManager dataManagers, DataTable dtProjectId, int LedgerId)
        {
            if (dtProjectId != null && dtProjectId.Rows.Count > 0)
            {
                foreach (DataRow dr in dtProjectId.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                if (balanceSystem.HasBalance(NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId))
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.Cancel);
                                balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                            }
                        }
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteMappedProject(DataManager dataManagers, int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LedgerProjectMappingDelete))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion

        #region Ledger System
        public ResultArgs FetchLedgerDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadAllLedgers))
            {
                // dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLedgerFD()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadLedgerFD))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs LoadAllLedgerByProjectId(int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadLedgerByProId))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TransBalance.FetchOpBalanceList,DataBaseType.HeadOffice))
            {
                if (ProjectId != 0)
                    dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckLedgerMapped()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CheckLedgerMapped))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckCostCentreMapped()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.CheckCostCentreMapped))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);

                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedLedgersByLedgerId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedLedgers))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchProjectIdByFDLedgerId(int FDId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedFDByFDId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, FDId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }


        public ResultArgs MappingLedgers()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                AccountMappingLedger(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingLedger(DataManager dataManagers)
        {
            ResultArgs IsSuccess = null;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                //  dataManager.BeginTransaction();
                if (IsFDLedger)
                {
                    //Saving FD Details
                    using (BankAccountSystem FDUpdation = new BankAccountSystem())
                    {
                        IsSuccess = resultArgs = FDUpdation.UpdateFD(dataManager, dtMappingLedger);
                        if (resultArgs.Success)
                        {
                            if (resultArgs != null)
                            {
                                if (resultArgs.Success)
                                {
                                    resultArgs = DeleteMapLedger(dataManager, ProjectId);
                                    if (resultArgs.Success)
                                        MapLedger(dataManager, dtLedgerIDCollection, ProjectId);
                                }
                            }
                            else
                                resultArgs = IsSuccess;
                        }
                    }
                }
                else
                {
                    resultArgs = DeleteMapLedger(dataManager, ProjectId);
                    if (resultArgs.Success)
                    {
                        resultArgs = DeleteLedgerBalance(dataManager);
                        if (resultArgs.Success)
                            MapLedger(dataManager, dtLedgerIDCollection, ProjectId);
                    }
                }
                //  dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingByLedgerId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManagers.Database = dataManagers.Database;
                resultArgs = UnMapLedgerByLedgerId(dataManager);
                if (resultArgs.Success)
                    MapLedgerByLedgerId(dataManager);
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingByFDId(DataTable dtBreakUpDetails, BankAccountSystem FDUpdation, int BankId)
        {
            ResultArgs IsSuccess = null;
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                if (dtBreakUpDetails != null)
                {
                    //Saving FD Details
                    IsSuccess = resultArgs = FDUpdation.UpdateFD(dataManager, dtBreakUpDetails, BankId);
                    if (resultArgs.Success)
                    {
                        if (resultArgs != null)
                        {
                            if (resultArgs.Success)
                            {
                                resultArgs = UnMapLedgerByLedgerId(dataManager);
                                if (resultArgs.Success)
                                    AccountMappingFDByFDId(dataManager, FDUpdation);
                            }
                        }
                        else
                            resultArgs = IsSuccess;
                    }
                }
                else
                {
                    resultArgs = UnMapLedgerByLedgerId(dataManager);
                    if (resultArgs.Success)
                        AccountMappingFDByFDId(dataManager, FDUpdation);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingFDByFDId(DataManager dataManagers, BankAccountSystem FDUpdation)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, FDUpdation.ProjectId);
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        if (balanceSystem.HasBalance(FDUpdation.ProjectId, LedgerId))
                            balanceSystem.UpdateOpBalance(OpeningBalanceDate, FDUpdation.ProjectId, LedgerId, NumberSet.ToDouble(FDUpdation.Amount.ToString()), FDUpdation.TransMode, TransactionAction.Cancel);
                        balanceSystem.UpdateOpBalance(OpeningBalanceDate, FDUpdation.ProjectId, LedgerId, NumberSet.ToDouble(FDUpdation.Amount.ToString()), FDUpdation.TransMode, TransactionAction.New);
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs MapLedger(DataManager dataManagers, DataTable LedgerIds, int ProjectId)
        {
            if (LedgerIds != null && LedgerIds.Rows.Count > 0)
            {
                foreach (DataRow dr in LedgerIds.Rows)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        if (balanceSystem.HasBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString())))
                            balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.EditBeforeSave);
                    }
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()));
                        resultArgs = dataManager.UpdateData();
                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            using (BalanceSystem balanceSystem = new BalanceSystem())
                            {
                                if (balanceSystem.HasBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString())))
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.EditAfterSave);
                                else
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()), this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                            }
                        }
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            if (dtMovedLedgerIDCollection != null && dtMovedLedgerIDCollection.Rows.Count > 0)
            {
                foreach (DataRow dr in dtMovedLedgerIDCollection.Rows)
                {
                    using (BalanceSystem balanceSystem = new BalanceSystem())
                    {
                        resultArgs = balanceSystem.DeleteBalance(ProjectId, this.NumberSet.ToInteger(dr[this.AppSchema.Ledger.LEDGER_IDColumn.ColumnName].ToString()));
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteMapLedger(DataManager dataManagers, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingDelete))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        private ResultArgs DeleteLedgerBalance(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteMappedLedgerBalance))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MapLedgerByLedgerId(DataManager dataManagers)
        {
            if (dtMappingLedger != null && dtMappingLedger.Rows.Count > 0)
            {
                foreach (DataRow dr in dtMappingLedger.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectLedgerMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                        if (FDTransType != LedgerTypes.FD.ToString())
                        {
                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
                            {
                                using (BalanceSystem balanceSystem = new BalanceSystem())
                                {
                                    if (balanceSystem.HasBalance(NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId))
                                        balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.Cancel);
                                    balanceSystem.UpdateOpBalance(OpeningBalanceDate, NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()), LedgerId, this.NumberSet.ToDouble(dr[AMOUNT].ToString()), dr[TRANS_MODE].ToString(), TransactionAction.New);
                                }
                            }
                        }
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs UnMapLedgerByLedgerId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UnMapProjectLedger))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

        #region Cost Center System
        public ResultArgs FetchCostCentreDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadAllCostCentre))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedCostCenter()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedCostCenter))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedCostCenterByCostCenterid()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedCostCenterByCostCenterId))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingCostCenter()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteMappedCostCenter(dataManager, ProjectId);
                if (resultArgs.Success)
                {
                    MapCostCentre(dtCostCenterIDCollection, ProjectId);
                }
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingCostCenterByCCId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                resultArgs = UnMapMappedCostCentreByCCId(dataManager);
                if (resultArgs.Success)
                    MapCostCentreByCCId(dataManager);
            }
            return resultArgs;
        }

        private ResultArgs MapCostCentre(DataTable dtCostCenterId, int ProjectId)
        {
            if (dtCostCenterId != null && dtCostCenterId.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCostCenterId.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectCostCentreMappingAdd))
                    {
                        dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, dr[this.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, dr[this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, dr[this.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        private ResultArgs DeleteMappedCostCenter(DataManager dataManagers, int ProjectId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DeleteProjectCostCenterMapping))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UnMapMappedCostCentreByCCId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.UnMapCostCentreByCCId))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MapCostCentreByCCId(DataManager dataManagers)
        {
            if (dtCostCenterIDCollection != null)
            {
                foreach (DataRow dr in dtCostCenterIDCollection.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Mapping.ProjectCostCentreMappingAdd))
                    {
                        dataManager.Database = dataManagers.Database;
                        dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                        dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.AMOUNTColumn, dr[this.AppSchema.FDRegisters.AMOUNTColumn.ColumnName]);
                        dataManager.Parameters.Add(this.AppSchema.FDRegisters.TRANS_MODEColumn, dr[this.AppSchema.FDRegisters.TRANS_MODEColumn.ColumnName]);
                        resultArgs = dataManager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }
        #endregion

        #region Donor System
        /// <summary>
        /// To get all the Available Donors
        /// </summary>
        /// <returns>ResultArgs Type </returns>
        public ResultArgs LoadAllDonors()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadAllDonor))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedDonor()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchDonorMapped))
            {
                dataManager.Parameters.Add(AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchMappedDonorById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.FetchMappedDonorByDonorId))
            {
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MappDonor()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                AccountMappingDonor(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingDonor(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                resultArgs = UnMapDonor(dataManager);
                if (resultArgs.Success)
                    MapDonor(dataManager);
            }
            return resultArgs;
        }

        public ResultArgs AccountMappingDonorByDonorId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.Database = dataManagers.Database;
                resultArgs = UnMapDonorByDonorId(dataManager);
                if (resultArgs.Success)
                    MapDonorByDonorId(dataManager);
            }
            return resultArgs;
        }

        private ResultArgs MapDonor(DataManager dataManagers)
        {
            foreach (DataRow dr in dtDonorMapping.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorMap))
                {
                    dataManager.Database = dataManagers.Database;
                    if (dtDonorMapping != null && dtDonorMapping.Rows.Count > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                        dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, this.NumberSet.ToInteger(dr[this.AppSchema.DonorAuditor.DONAUD_IDColumn.ColumnName].ToString()));
                        resultArgs = dataManager.UpdateData();
                    }
                    if (!resultArgs.Success)
                        break;
                }

            }
            return resultArgs;
        }

        private ResultArgs MapDonorByDonorId(DataManager dataManagers)
        {
            foreach (DataRow dr in dtDonorMapping.Rows)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorMap))
                {
                    if (dtDonorMapping != null && dtDonorMapping.Rows.Count > 0)
                    {
                        dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, this.NumberSet.ToInteger(dr[this.AppSchema.Project.PROJECT_IDColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                        resultArgs = dataManager.UpdateData();
                    }
                    if (!resultArgs.Success)
                        break;
                }

            }
            return resultArgs;
        }
        /// <summary>
        /// Map Donor from transaction
        /// </summary>
        /// <param name="dataManagers"></param>
        /// <returns></returns>
        public ResultArgs MapDonorTransaction()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorMap))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectVoucher.PROJECT_IDColumn, ProjectId);
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UnMapDonor(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorUnMap))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs UnMapDonorByDonorId(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.DonorUnMapByDonorId))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.DonorAuditor.DONAUD_IDColumn, DonorId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

        #region Generalate Report Mapping
        public ResultArgs MapGeneralateLedgersMap()
        {
            using (DataManager dataManger=new DataManager())
            {
                dataManger.BeginTransaction();
                resultArgs = DeleteGeneralateLedgerMapping();
                if (resultArgs != null && resultArgs.Success)
                    resultArgs = MapGeneralateLedgers();
                dataManger.EndTransaction();
            }
            return resultArgs;
        }

        private ResultArgs DeleteGeneralateLedgerMapping()
        {
            using (DataManager dataManager=new DataManager(SQLCommand.Mapping.DeleteGeneralateMapping,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, SubsidyLedger);
                dataManager.Parameters.Add(AppSchema.GeneralateMapping.IS_SUBSIDY_LEDGERColumn, IsSubsidyLedger ? 0 : 1);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MapGeneralateLedgers()
        {
            foreach (object item in GeneralateLedgersMapping)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Mapping.MapGeneralateLedgers,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, SubsidyLedger);
                    dataManager.Parameters.Add(AppSchema.GeneralateMapping.GENERALATE_MAPPING_LEDGERIDColumn, NumberSet.ToInteger(item.ToString()));
                    dataManager.Parameters.Add(AppSchema.GeneralateMapping.IS_SUBSIDY_LEDGERColumn, IsSubsidyLedger ? 0 : 1);
                    resultArgs = dataManager.UpdateData();
                }
            }
             return resultArgs;
        }

        public ResultArgs LoadMappedLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadMappedLedgers, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, SubsidyLedger);
                if (SubsidyLedger == 1)
                {
                    dataManager.Parameters.Add(AppSchema.LedgerGroup.NATURE_IDColumn, 1);
                }
                else 
                {
                    dataManager.Parameters.Add(AppSchema.LedgerGroup.NATURE_IDColumn, 2);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    LedgerIdCollection = String.Join(",", (from id in resultArgs.DataSource.Table.AsEnumerable()
                                                           where !string.IsNullOrEmpty(id.Field<String>(this.AppSchema.GeneralateMapping.GENERALATE_MAPPING_LEDGERIDColumn.ColumnName))
                                                           select id.Field<String>(this.AppSchema.GeneralateMapping.GENERALATE_MAPPING_LEDGERIDColumn.ColumnName)));
                }
            }
            return resultArgs;
        }
        public ResultArgs LoadMappedGeneralateLedgers()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.LoadMappedGeneralateLedgers, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion

        #region Common System
        public ResultArgs FetchBankId(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Mapping.BankIdByLedgerId))
            {
                dataManager.Parameters.Add(this.AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.FetchData(DataSource.DataTable); ;
            }
            return resultArgs;
        }
        public ResultArgs TransactionFD()
        {
            using (DataManager dm = new DataManager(SQLCommand.Mapping.TransactionFixedDepositId))
            {
                resultArgs = dm.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
