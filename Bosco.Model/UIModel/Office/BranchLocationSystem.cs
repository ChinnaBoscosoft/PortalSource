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
    public class BranchLocationSystem : SystemBase
    {
        #region Constructor

        public BranchLocationSystem()
        {

        }

        public BranchLocationSystem(int LocationId, DataBaseType connectTo)
        {
            this.LocationId = LocationId;
            FillBranchLocationDetails(LocationId, connectTo);
        }

        #endregion

        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region BranchOfficeProperties

        public int LocationId { get; set; }
        public int BranchId { get; set; }
        public string LocationName { get; set; }
        public int LoginBranchId { get; set; }

        #endregion

        #region Methods

        public ResultArgs SaveBranchOfficeDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((LocationId == 0) ? SQLCommand.BranchLocation.Add : SQLCommand.BranchLocation.Update, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_IDColumn, LocationId);
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.BRANCH_IDColumn, BranchId);
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_NAMEColumn, LocationName);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillBranchLocationDetails(int LocationId, DataBaseType connectTo)
        {
            resultArgs = BranchLocationDetailbyId(LocationId, connectTo);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                LocationId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchLocation.LOCATION_IDColumn.ColumnName].ToString());
                BranchId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchLocation.BRANCH_IDColumn.ColumnName].ToString());
                LocationName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.BranchLocation.LOCATION_NAMEColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        public ResultArgs BranchLocationDetailbyId(int LocationId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchLocation.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_IDColumn, LocationId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteLocation(int LocationId)
        {
            try
            {
                using (DataManager Datamanager = new DataManager(SQLCommand.BranchLocation.Delete, DataBaseType.HeadOffice))
                {
                    Datamanager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_IDColumn, LocationId);
                    resultArgs = Datamanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchLocationAll(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchLocation.FetchAll, connectTo))
            {
                //fetch branch which are assigned to the particular branch user based on the login
                if (!string.IsNullOrEmpty(base.LoginUserBranchOfficeCode))
                {

                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, LoginUserBranchOfficeCode);
                }
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public ResultArgs FetchBranchLocationByBranch(DataBaseType connectTo, string branchOfficeCode)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchLocation.FetchBranchLocation, connectTo))
            {
                if (!string.IsNullOrEmpty(branchOfficeCode))
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, branchOfficeCode);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.Scalar);
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchLocationByBranchId(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.BranchLocation.FetchLocationbyBranch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.BRANCH_IDColumn, BranchId);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }

        public Int32 GetLocationId(int branchId, string LocationName)
        {
            Int32 rtn = 0;
            using (DataManager dataManager = new DataManager(SQLCommand.BranchLocation.FetchLocationbyBranchLocation, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.BRANCH_IDColumn, branchId);
                dataManager.Parameters.Add(this.AppSchema.BranchLocation.LOCATION_NAMEColumn, LocationName);
                
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null & resultArgs.DataSource.Table != null)
                {
                    DataTable dtBranchLocation = resultArgs.DataSource.Table;
                    if (dtBranchLocation.Rows.Count > 0)
                    {
                        rtn = NumberSet.ToInteger(dtBranchLocation.Rows[0][AppSchema.BranchLocation.LOCATION_IDColumn.ColumnName].ToString());
                    }
                }
            }

            return rtn;
        }

        #endregion
    }
}
