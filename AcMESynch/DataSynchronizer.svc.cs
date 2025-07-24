using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;
using AcMeERP.Base;
using System.Web;
namespace AcMESynch
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class DataSynchronizer : IDataSynchronizer
    {
        private string dataSetName = "MASTER_DATA";
        public DataSet ExportERPMasters(string headOfficeCode, string branchOfficeCode)
        {
            try
            {
                AcMeERP.Base.UIBase objBase = new UIBase();
                DataSet dsMaster = new DataSet(dataSetName);
                 
                if (!(string.IsNullOrEmpty(headOfficeCode) && string.IsNullOrEmpty(branchOfficeCode)))
                {
                    objBase.HeadOfficeCode = headOfficeCode;

                    //Validate HeadOfficeCode and BranchOfficeCode
                    dsMaster.Tables.Add(OfficeData(headOfficeCode, branchOfficeCode));

                    using (LegalEntitySystem legalSystem = new LegalEntitySystem())
                    {
                        ResultArgs resultArgs = legalSystem.LegalEntityFechAll(DataBaseType.HeadOffice);

                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dsMaster.Tables.Add(resultArgs.DataSource.Table);
                        }
                    }
                    using (ProjectCatogorySystem projectCategorySystem = new ProjectCatogorySystem())
                    {
                        ResultArgs resultArgs = projectCategorySystem.ProjectCatogoryFecthAll(DataBaseType.HeadOffice);

                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dsMaster.Tables.Add(resultArgs.DataSource.Table);
                        }
                    }
                    using (ProjectSystem projectSystem = new ProjectSystem())
                    {
                        ResultArgs resultArgs = projectSystem.ProjectsFetchAll(DataBaseType.HeadOffice);

                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dsMaster.Tables.Add(resultArgs.DataSource.Table);
                        }
                    }
                    using (LedgerGroupSystem ledgerGroupSystem = new LedgerGroupSystem())
                    {
                        ResultArgs resultArgs = ledgerGroupSystem.LedgerGroupFetchAll(DataBaseType.HeadOffice);

                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dsMaster.Tables.Add(resultArgs.DataSource.Table);
                        }
                    }
                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        ResultArgs resultArgs = ledgerSystem.LedgerFetchAll(DataBaseType.HeadOffice);

                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dsMaster.Tables.Add(resultArgs.DataSource.Table);
                        }
                    }
                    using (PurposeSystem purposeSystem = new PurposeSystem())
                    {
                        ResultArgs resultArgs = purposeSystem.PurposeFetchAll(DataBaseType.HeadOffice);

                        if (resultArgs.Success && resultArgs.RowsAffected > 0)
                        {
                            dsMaster.Tables.Add(resultArgs.DataSource.Table);
                        }
                    }
                   
                }
                return dsMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        
        }
        private DataTable OfficeData(string headOfficeCode, string branchOfficeCode)
        {
            AcMeERP.Base.UIBase objBase = new UIBase();
            DataTable dtOfficeData = new DataTable("OfficeData");
            dtOfficeData.Columns.Add(objBase.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName, (objBase.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.DataType));
            dtOfficeData.Columns.Add(objBase.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName, (objBase.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.DataType));
            DataRow drOfficeRow = dtOfficeData.NewRow();
            drOfficeRow[objBase.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn.ColumnName] = CommonMember.EncryptValue(headOfficeCode);
            drOfficeRow[objBase.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn.ColumnName] = CommonMember.EncryptValue(branchOfficeCode);
            dtOfficeData.Rows.Add(drOfficeRow);
            return dtOfficeData;
        }
    }
}
