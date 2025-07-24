using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.Model.UIModel.Master
{
    public class CongregationLedgerMappingSystem : SystemBase
    {
        #region Constructor

        public CongregationLedgerMappingSystem()
        {

        }

        #endregion

        #region Preperties
        ResultArgs resultArgs = null;
        public int GeneralateLedgerId { get; set; }
        public string ProjectCatogoryLedgerId { get; set; }
        public int ProjectCategoryGroupedLedgerId { get; set; }
        #endregion

        #region Methods

        public ResultArgs LoadLedger()
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.FetchLedgerByCongregationLedger, DataBaseType.HeadOffice))
                {
                    DataManager.Parameters.Add(AppSchema.CongregationLedger.CON_LEDGER_IDColumn, GeneralateLedgerId);
                    DataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = DataManager.FetchData(DataSource.DataTable);
                }
            }
            return resultArgs;
        }

        public ResultArgs LoadProjectCategoryLedger()
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.FetchLedgerByProjectCategoryLedger, DataBaseType.HeadOffice))
                {
                    DataManager.Parameters.Add(AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjectCatogoryLedgerId);
                    DataManager.Parameters.Add(AppSchema.ProjectCatogoryGroup.PROJECT_CATOGORY_GROUP_IDColumn, ProjectCategoryGroupedLedgerId);
                    DataManager.Parameters.Add(AppSchema.GeneralateMapping.INTER_AC_FROM_TRANSFER_IDColumn, InterAccountFromLedgerIds);
                    DataManager.Parameters.Add(AppSchema.GeneralateMapping.INTER_AC_TO_TRANSFER_IDColumn, InterAccountToLedgerIds);
                    DataManager.Parameters.Add(AppSchema.GeneralateMapping.CONTRIBUTION_FROM_PROVINCE_IDColumn, ProvinceFromLedgerIds);
                    DataManager.Parameters.Add(AppSchema.GeneralateMapping.CONTRIBUTION_TO_PROVINCE_IDColumn, ProvinceToLedgerIds);
                    DataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = DataManager.FetchData(DataSource.DataTable);
                }
            }
            return resultArgs;
        }

        public ResultArgs MapLedgers(List<object> LedgerId, int GeneralateLedgerId)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                resultArgs = MapLedgertoGeneralateLedger(dataManager, LedgerId, GeneralateLedgerId);
                dataManager.EndTransaction();
            }

            return resultArgs;
        }

        private ResultArgs MapLedgertoGeneralateLedger(DataManager dManager, List<object> LedgerIds, int GeneralateLedger)
        {
            using (DataManager DataManager = new DataManager())
            {
                DataManager.Database = dManager.Database;
                resultArgs = DeleteMappedLedger();
                if (resultArgs.Success)
                {

                    //15.02.2018    chinna

                    //string LedCollection = string.Empty;
                    //foreach (object se in LedgerIds)
                    //{
                    //   LedCollection += se.ToString() + ",";
                    //}
                    //LedCollection = LedCollection.Trim(',');

                    //if (!string.IsNullOrEmpty(LedCollection))
                    //{
                    //resultArgs = CheckingMappingCount(LedCollection); // Checking ledgers are already mapped with any other generalate ledger.

                    //if (!(resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0))
                    //{
                    foreach (object LedgerID in LedgerIds)
                    {
                        resultArgs = MapLedger(GeneralateLedger, this.NumberSet.ToInteger(LedgerID.ToString()));
                        if (!resultArgs.Success)
                            break;
                    }
                    //}
                    //else
                    //{
                    //   resultArgs.Success = false;
                    //  resultArgs.Message = MessageCatalog.Message.GeneralateLedger.MappingWhicharenotMappedAlready;
                    // }
                    //}
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Map Catogory 
        /// </summary>
        /// <param name="LedgerId"></param>
        /// <param name="GeneralateLedgerId"></param>
        /// <returns></returns>
        public ResultArgs MapGroupedCatogorywithGeneralate(DataTable dtMappedLedgers, int GroupedGeneralateLedgerId)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                resultArgs = DeleteGroupedProjectCategoryMappedLedger();
                if (resultArgs.Success)
                {
                    foreach (DataRow dr in dtMappedLedgers.Rows)
                    {
                        int LedgerId = NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                        int GenLedgerid = NumberSet.ToInteger(dr["CON_LEDGER_ID"].ToString());
                        resultArgs = MapProjectCatogoryGroupedLedgers(LedgerId, GenLedgerid, GroupedGeneralateLedgerId);
                        if (!resultArgs.Success)
                        {
                            break;
                        }
                    }
                }

                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        ///// <summary>
        ///// Map Generalate Ledgers 
        ///// </summary>
        ///// <param name="dManager"></param>
        ///// <param name="LedgerIds"></param>
        ///// <param name="GeneralateLedger"></param>
        ///// <returns></returns>
        //private ResultArgs MapGroupedCatogorywithGeneralateLedger(DataManager dManager, List<object> LedgerIds, List<object> GeneralateLedgers, int GroupedProjectCatogory)
        //{
        //    using (DataManager DataManager = new DataManager())
        //    {
        //        DataManager.Database = dManager.Database;
        //        resultArgs = DeleteMappedLedger();
        //        if (resultArgs.Success)
        //        {
        //            foreach (object LedgerID in LedgerIds)
        //            {
        //                foreach (object GeneralateLedger in GeneralateLedgers)
        //                {
        //                    resultArgs = MapProjectCatogoryGroupedLedgers(0, 0, 0);
        //                    if (!resultArgs.Success)
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    return resultArgs;
        //}

        public ResultArgs DeleteMappedLedger()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.DeleteMappedLedger, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.CongregationLedger.CON_LEDGER_IDColumn, GeneralateLedgerId);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteSingleMappedLedger(int Ledger_id)
        {
            using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.DeleteIndividualMappedLedger, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, Ledger_id);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs CheckingMappingCount(string LedgerId)
        {
            using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.CheckingMappedCount, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDSColumn, LedgerId);
                DataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = DataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs CheckingSameNature(int CongregationLedgerId)
        {
            using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.CheckingSameNature, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.CongregationLedger.CON_LEDGER_IDColumn, CongregationLedgerId);
                DataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = DataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MapLedger(int ProjectCategoryId, int LedgerId)
        {
            using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.MapCongregationLedger, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.CongregationLedger.CON_LEDGER_IDColumn, GeneralateLedgerId);
                DataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteGroupedProjectCategoryMappedLedger()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.DeleteGroupedProjectCatogoryLedgers, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.ProjectCatogoryGroup.PROJECT_CATOGORY_GROUP_IDColumn, ProjectCategoryGroupedLedgerId);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs MapProjectCatogoryGroupedLedgers(int LedgerId, int GeneralatgeLedgerId, int GroupedProjectCatogoryId)
        {
            using (DataManager DataManager = new DataManager(SQLCommand.CongregationMapping.MapProjectCatogoryCongregationLedger, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.Ledger.LEDGER_IDColumn, LedgerId);
                DataManager.Parameters.Add(AppSchema.CongregationLedger.CON_LEDGER_IDColumn, GeneralatgeLedgerId);
                DataManager.Parameters.Add(AppSchema.ProjectCatogoryGroup.PROJECT_CATOGORY_GROUP_IDColumn, GroupedProjectCatogoryId);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        #endregion

    }
}
