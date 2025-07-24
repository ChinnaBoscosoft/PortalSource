using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.DAO.Schema;
using Bosco.DAO.Data;
namespace Bosco.Model.UIModel
{
    public class ProjectCatogorySystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ProjectCatogorySystem()
        {
        }
        public ProjectCatogorySystem(int ProjectCatogoryId, DataBaseType connectTo)
        {
            FillProjectCatogoryDetails(ProjectCatogoryId, connectTo);
        }
        #endregion

        #region ProjectCatogoryProperties
        public int ProjectCatogoryId { get; set; }
        public string ProjectCatogoryName { get; set; }
        public int GeneralateCategoryId { get; set; }
        public int ITRGroupId { get; set; }
        public int BranchId { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchProjectCatogoryDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.FetchAll, connectTo))
            {
                if (BranchId != 0)
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteProjectCatogoryDetails(int ProjectCatogoryId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName, ProjectCatogoryId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveProjectCatogoryDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((ProjectCatogoryId == 0) ? SQLCommand.ProjectCatogory.Add : SQLCommand.ProjectCatogory.Update, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjectCatogoryId);
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCatogoryName);
                if (GeneralateCategoryId != 0)
                    dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_GROUP_IDColumn, GeneralateCategoryId);

                if (ITRGroupId != 0)
                {
                    dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_ITRGROUP_IDColumn, ITRGroupId);
                }

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveUpdateProjectCategoryDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.CreatUpdateDefaultLedgerDetails, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn, ProjectCatogoryId);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillProjectCatogoryDetails(int ProjectCategoryId, DataBaseType connectTo)
        {
            resultArgs = ProjectCatogoryDetailsById(ProjectCategoryId, connectTo);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                ProjectCatogoryName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn.ColumnName].ToString();
                if (resultArgs.DataSource.Table.Columns.Contains(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_GROUP_IDColumn.ColumnName))
                {
                    GeneralateCategoryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_GROUP_IDColumn.ColumnName].ToString());
                    ITRGroupId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_ITRGROUP_IDColumn.ColumnName].ToString());
                }

            }
            return resultArgs;
        }

        public ResultArgs ProjectCatogoryDetailsById(int ProjectCatogoryId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_IDColumn.ColumnName, ProjectCatogoryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchITRGroupCategory(DataBaseType connecTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.FetchITR, connecTo))
            {

                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_ITRGROUP_IDColumn.ColumnName, ITRGroupId == 0 ? 1 : ITRGroupId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;

        }

        public ResultArgs ProjectCatogoryDetailsByName(string ProjectCatogory, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.FetchByName, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.ProjectCatogory.PROJECT_CATOGORY_NAMEColumn, ProjectCatogory);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs ProjectCatogoryFecthAll(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.ProjectCategoryFetchAll, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public int GetCount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ProjectCatogory.ProjectCategoryCount, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;

        }
        #endregion
    }
}
