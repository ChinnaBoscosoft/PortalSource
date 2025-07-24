using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using System.Data;
using Bosco.Utility;

namespace Bosco.Model
{
    public class AssetClassSystem : SystemBase
    {
        #region Variable Declearation
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public AssetClassSystem()
        {

        }
        public AssetClassSystem(int assetClassId)
        {
            AssetClassId = assetClassId;
            AssignClassProperties();
         }
        #endregion

        #region Properties
        public int AssetClassId
        {
            get;
            set;
        }

        public string AssetClassIds
        {
            get;
            set;
        }

        public double Depreciation
        {
            get;
            set;
        }

        public string AssetClass
        {
            get;
            set;
        }

        public int ParentClassId
        {
            get;
            set;
        }

        public string ClassId
        {
            get;
            set;
        }

        public int Method
        {
            get;
            set;
        }   
        #endregion

        #region Methods

        public ResultArgs FetchClassDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetClass.FetchAll,DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSelectedClassDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetClass.FetchSelectedClass,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetClass.ASSET_CLASS_IDColumn,AssetClassIds);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs SaveClassDetails()
        {
            using (DataManager datamanager = new DataManager((AssetClassId == 0) ? SQLCommand.AssetClass.Add : SQLCommand.AssetClass.Update,DataBaseType.HeadOffice))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetClass.ASSET_CLASS_IDColumn, AssetClassId, true);
                datamanager.Parameters.Add(this.AppSchema.AssetClass.ASSET_CLASSColumn, AssetClass);
                datamanager.Parameters.Add(this.AppSchema.AssetClass.PARENT_CLASS_IDColumn, ParentClassId);
                datamanager.Parameters.Add(this.AppSchema.AssetClass.METHOD_IDColumn, Method);
                datamanager.Parameters.Add(this.AppSchema.AssetClass.DEP_PERCENTAGEColumn, Depreciation);
                resultArgs = datamanager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs DeleteClassDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.AssetClass.Delete,DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.AssetClass.ASSET_CLASS_IDColumn, AssetClassId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public void AssignClassProperties()
        {
            resultArgs = FetchClassDetailsById();
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                AssetClass = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetClass.ASSET_CLASSColumn.ColumnName].ToString();
                ParentClassId = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetClass.PARENT_CLASS_IDColumn.ColumnName].ToString());
                Method = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetClass.METHOD_IDColumn.ColumnName].ToString());
                Depreciation = NumberSet.ToDouble(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AssetClass.DEP_PERCENTAGEColumn.ColumnName].ToString());
            }
        }

        public ResultArgs FetchClassDetailsById()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchbyID, DataBaseType.HeadOffice))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetClass.ASSET_CLASS_IDColumn, AssetClassId);
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchParentClassName()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchClassNameByParentID,DataBaseType.HeadOffice))
            {
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetSubClassByAssetParentId()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchAssetSubClassbyAssetParentId, DataBaseType.HeadOffice))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetClass.ASSET_CLASS_IDColumn, AssetClassIds);
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchAssetDepreciationMethods()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchDepreciationMethod,DataBaseType.HeadOffice))
            {
                datamanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = datamanager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public int FetchAssetClassIdByName()
        {
            using (DataManager datamanager = new DataManager(SQLCommand.AssetClass.FetchAssetClassIdByName, DataBaseType.HeadOffice))
            {
                datamanager.Parameters.Add(this.AppSchema.AssetClass.ASSET_CLASSColumn, AssetClass.Trim());
                resultArgs = datamanager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        #endregion
    }
}
