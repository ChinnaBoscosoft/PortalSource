using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.DAO.Schema;


namespace Bosco.Model.UIModel
{
    public class PurposeSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public PurposeSystem()
        {
        }
        public PurposeSystem(int PurposeId, DataBaseType connectTo)
        {
            FillPurposeDetails(PurposeId, connectTo);
        }
        #endregion

        #region Property
        public int PurposeId { get; set; }
        public string purposeCode { get; set; }
        public string PurposeHead { get; set; }
        #endregion

        #region Methods
        public ResultArgs FetchPurposeDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.FetchAll, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeletePurposeDetails(int PurposeId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs SavePurposeDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((PurposeId == 0) ? SQLCommand.Purposes.Add : SQLCommand.Purposes.Update, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Purposes.CODEColumn, purposeCode);
                dataManager.Parameters.Add(this.AppSchema.Purposes.FC_PURPOSEColumn, PurposeHead);
                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, PurposeId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FillPurposeDetails(int PurposeId, DataBaseType connectTo)
        {
            resultArgs = PurposeDetailsbyId(PurposeId, connectTo);
            if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                purposeCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Purposes.CODEColumn.ColumnName].ToString();
                PurposeHead = resultArgs.DataSource.Table.Rows[0][this.AppSchema.Purposes.FC_PURPOSEColumn.ColumnName].ToString();

            }
            return resultArgs;
        }
        public ResultArgs PurposeDetailsbyId(int purposeId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.Fetch, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.Purposes.CONTRIBUTION_IDColumn, purposeId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }
        public ResultArgs PurposeFetchAll(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Purposes.PurposeFetchAll, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs;
        }
        #endregion
    }
}
