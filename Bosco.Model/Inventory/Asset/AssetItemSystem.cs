using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;
using Bosco.Model.UIModel;

namespace Bosco.Model
{
    public class AssetItemSystem : SystemBase
    {

        #region Variable Declearation
        ResultArgs resultArgs = new ResultArgs();
        private static DataView dvSetting = null;
        #endregion

        #region Constructor
        public AssetItemSystem()
        {
        }
        public AssetItemSystem(int Item_Id)
        {
            this.ItemId = Item_Id;
            AssignToAssetItemPoroperties();
        }

        #endregion

        #region Properties
        public int ItemId { get; set; }
        public int ProjectId { get; set; }
        public int AccountLedgerId { get; set; }
        public double TempSummaryValue { get; set; }
        public double TotalSummmaryValue { get; set; }
        public string AssetID { get; set; }
        public int AssetClassId { get; set; }
        public string AssetClass { get; set; }
        public int DepreciationLedger { get; set; }
        public int DisposalLedger { get; set; }
        public int AccountLeger { get; set; }
        public int Catogery { get; set; }
        public string Name { get; set; }
        public string ItemKind { get; set; }
        public int Unit { get; set; }
        public int Custodian { get; set; }
        public string Method { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int StartingNo { get; set; }
        public int Quantity { get; set; }
        public decimal RatePerItem { get; set; }
        public decimal Amount { get; set; }
        public DataTable dtAssetDetail { get; set; }
        public int Location_id { get; set; }
        public int PurchaseId { get; set; }
        public int SourceFlag { get; set; }
        public int Status { get; set; }
        public decimal UsefulLife { get; set; }
        public decimal SalvageLife { get; set; }
        public int SalesId { get; set; }
        public int RetentionYrs { get; set; }
        public int DepreciationYrs { get; set; }
        public int InsuranceApplicable { get; set; }
        public int AMCApplicable { get; set; }
        public int DepreciatonApplicable { get; set; }
        public int AssetItemMode { get; set; }
        public string UOM { get; set; }

        public int ConditionID { get; set; }
        public int ManufacturerId { get; set; }
        public int InsurancePlanId { get; set; }
        public string PolicyNO { get; set; }
        public int AMCMonths { get; set; }
        public string Condition { get; set; }
        public int BranchId { get; set; }
        public int ItemDetailId { get; set; }
        public int InoutDetailId { get; set; }
        public DataTable dtUpdateAssetDetails { get; set; }
        public int LedgerGroupId { get; set; }

        #endregion

        #region Methods
       
        /// <summary>
        /// Save asset item details.
        /// </summary>
        /// <returns></returns>
        public ResultArgs SaveItemDetails()
        {
            try
            {
                using (DataManager dataManager = new DataManager((ItemId == 0) ? SQLCommand.AssetItem.Add : SQLCommand.AssetItem.Update,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.ITEM_IDColumn, ItemId,true);
                    dataManager.Parameters.Add(this.AppSchema.AssetClass.ASSET_CLASS_IDColumn, AssetClassId);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.DEPRECIATION_LEDGER_IDColumn, DepreciationLedger);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.DISPOSAL_LEDGER_IDColumn, DisposalLedger);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.ACCOUNT_LEDGER_IDColumn, AccountLeger);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.ASSET_ITEMColumn, Name);
                    dataManager.Parameters.Add(this.AppSchema.AssetUnitofMeasure.UOM_IDColumn, Unit);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.PREFIXColumn, Prefix);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.SUFFIXColumn, Suffix);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.RETENTION_YRSColumn, RetentionYrs);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.DEPRECIATION_YRSColumn, DepreciationYrs);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.IS_INSURANCEColumn, InsuranceApplicable);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.IS_AMCColumn, AMCApplicable);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.IS_ASSET_DEPRECIATIONColumn, DepreciatonApplicable);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.STARTING_NOColumn, StartingNo);
                    dataManager.Parameters.Add(this.AppSchema.AssetItems.ASSET_MODEColumn, AssetItemMode);
                    resultArgs = dataManager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
            }
            finally
            { }

            return resultArgs;

        }

        /// <summary>
        /// Fetch all the asset item details for the view form.
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchAssetItemDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAll,DataBaseType.HeadOffice))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }
      

        /// <summary>
        /// Fetch asset item details by Id for Edit
        /// </summary>
        /// <returns></returns>
        public ResultArgs FetchAssetItemDetailsById()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.Fetch,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetItems.ITEM_IDColumn, this.ItemId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        /// <summary>
        /// Assign values to AssetItemSystem properties for edit.
        /// </summary>
        private void AssignToAssetItemPoroperties()
        {
            resultArgs = FetchAssetItemDetailsById();
            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs.DataSource.Table != null)
            {
                AssetClassId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetClass.ASSET_CLASS_IDColumn.ColumnName].ToString());
                AssetClass = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetClass.ASSET_CLASSColumn.ColumnName].ToString();
                DepreciationLedger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.DEPRECIATION_LEDGER_IDColumn.ColumnName].ToString());
                DisposalLedger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.DISPOSAL_LEDGER_IDColumn.ColumnName].ToString());
                AccountLeger = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.ACCOUNT_LEDGER_IDColumn.ColumnName].ToString());
                Name = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.ASSET_ITEMColumn.ColumnName].ToString();
                Unit = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetUnitofMeasure.UOM_IDColumn.ColumnName].ToString());
                UOM = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetUnitofMeasure.SYMBOLColumn.ColumnName].ToString();
                Prefix = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.PREFIXColumn.ColumnName].ToString();
                Suffix = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.SUFFIXColumn.ColumnName].ToString();
                StartingNo = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.STARTING_NOColumn.ColumnName].ToString());
                RetentionYrs = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.RETENTION_YRSColumn.ColumnName].ToString());
                DepreciationYrs = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.DEPRECIATION_YRSColumn.ColumnName].ToString());
                AMCApplicable = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.IS_AMCColumn.ColumnName].ToString());
                InsuranceApplicable = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.IS_INSURANCEColumn.ColumnName].ToString());
                DepreciatonApplicable = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.IS_ASSET_DEPRECIATIONColumn.ColumnName].ToString());
                AssetItemMode = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetItems.ASSET_MODEColumn.ColumnName].ToString());
            }
        }
        /// <summary>
        /// Delete asset item details by the ItemId.
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public ResultArgs DeleteAssetItem()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.Delete, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetItems.ITEM_IDColumn, ItemId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchAllAssetItems()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetItem.FetchAllAssetItems,DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public int FetchAssetItemIdByName()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetItem.FetchAssetItemIdByName, DataBaseType.HeadOffice))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetItems.ASSET_ITEMColumn, Name.Trim());
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion Methods

    }
}

