using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.UIModel
{
    public class CostCentreSystem : SystemBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public CostCentreSystem()
        {
        }

        public CostCentreSystem(int CostCentreId)
        {
            FillCostCentreProperties(CostCentreId);
        }
        #endregion

        #region Cost Centre Properties
        public int CostCentreId { get; set; }
        public string CostCentreAbbrevation { get; set; }
        public string CostCentreName { get; set; }
        public string Notes { get; set; }
        public int ProjectId { get; set; }
        public int MapCostCentreId { get; set; }
        public DataTable dtMapCostCentre { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchCostCentreDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchAll))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchforLookUpDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchforLookup))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteCostCentreDetails(int CostCentreId)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.CostCentre.Delete))
            {
                dataMember.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                resultArgs = dataMember.UpdateData(dataMember, "", SQLType.SQLStatic);
            }
            return resultArgs;
        }

        public ResultArgs SaveCostCentre()
        {
            using (DataManager dataManager = new DataManager())
            {
                dataManager.BeginTransaction();
                resultArgs = SaveCostCentreDetails(dataManager);
                if (resultArgs.Success)
                    MappCostCentre(dataManager);
                dataManager.EndTransaction();
            }
            return resultArgs;
        }

        public ResultArgs SaveCostCentreDetails(DataManager dataManagers)
        {
            using (DataManager dataManager = new DataManager((CostCentreId == 0) ? SQLCommand.CostCentre.Add : SQLCommand.CostCentre.Update))
            {
                dataManager.Database = dataManagers.Database;
                dataManager.Parameters.Add(this.AppSchema.CostCentre.ABBREVATIONColumn, CostCentreAbbrevation);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn, CostCentreName);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.NOTESColumn, Notes);
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCentreId);
                resultArgs = dataManager.UpdateData(dataManager, "", SQLType.SQLStatic);
            }
            return resultArgs;
        }

        private ResultArgs MappCostCentre(DataManager dataManager)
        {
            using (MappingSystem mappingSystem = new MappingSystem())
            {
                mappingSystem.CostCenterId = MapCostCentreId.Equals(0) ? NumberSet.ToInteger(resultArgs.RowUniqueId.ToString()) : MapCostCentreId;
                mappingSystem.dtCostCenterIDCollection = dtMapCostCentre;
                resultArgs = mappingSystem.AccountMappingCostCenterByCCId(dataManager);
            }
            return resultArgs;
        }

        public void FillCostCentreProperties(int CostCenterId)
        {
            resultArgs = FetchCostCentreDetailsById(CostCenterId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                CostCentreAbbrevation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentre.ABBREVATIONColumn.ColumnName].ToString();
                CostCentreName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString();
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.CostCentre.NOTESColumn.ColumnName].ToString();
            }
        }

        private ResultArgs FetchCostCentreDetailsById(int CostCenterId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.Fetch))
            {
                dataManager.Parameters.Add(this.AppSchema.CostCentre.COST_CENTRE_IDColumn, CostCenterId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchCostCentreCodes()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.CostCentre.FetchCostCentreCodes))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        #endregion
    }
}
