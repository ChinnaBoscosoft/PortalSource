using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.Model.UIModel
{
    public class AmendmentSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properties

        public int AmendmentId { get; set; }
        public string BranchOfficeCode { get; set; }
        public DateTime AmendmentDate { get; set; }
        public string Remarks { get; set; }
        public int Status { get; set; }
        public string HeadOfficeCode { get; set; }
        public string VoucherIds { get; set; }
        public int VoucherId { get; set; }
        public int BranchId { get; set; }
        #endregion

        #region Method

        private ResultArgs UpdateStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Amendments.UpdateStatus, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Amendments.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs SaveAmendementHistory()
        {

            using (DataManager dataManager = new DataManager(SQLCommand.Amendments.Save, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.Amendments.VOUCHER_IDColumn, VoucherId);
                dataManager.Parameters.Add(AppSchema.Amendments.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
                dataManager.Parameters.Add(AppSchema.Amendments.AMENDMENT_DATEColumn, AmendmentDate);
                dataManager.Parameters.Add(AppSchema.Amendments.REMARKSColumn, Remarks);
                dataManager.Parameters.Add(AppSchema.Amendments.STATUSColumn, Status);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;

        }


        public ResultArgs UpdateAmendment()
        {
            using (DataManager DataManager = new DataManager())
            {
                DataManager.BeginTransaction();
                UpdateAmendmentStatus(DataManager);
                DataManager.EndTransaction();
            }
            return resultArgs;

        }

        private ResultArgs UpdateAmendmentStatus(DataManager dataManager)
        {
            using (DataManager DataManager = new DataManager())
            {
                DataManager.Database = dataManager.Database;

                resultArgs = UpdateStatus();
                if (resultArgs.Success)
                {
                    resultArgs = SaveAmendementHistory();
                }
            }
            return resultArgs;
        }

        public string FetchRemark()
        {
            string Remarks = string.Empty;
            using (DataManager DataManager = new DataManager(SQLCommand.Amendments.FetchRemark, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.Amendments.VOUCHER_IDColumn, VoucherId);
                resultArgs= DataManager.FetchData(DataSource.Scalar);
            }
            return Remarks = resultArgs.DataSource.Sclar.ToString;
        }

        public ResultArgs UpdateRemarks()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.Amendments.UpdateRemark, DataBaseType.HeadOffice))
            {

                DataManager.Parameters.Add(AppSchema.Amendments.VOUCHER_IDColumn, VoucherId);
                DataManager.Parameters.Add(AppSchema.Amendments.REMARKSColumn, Remarks);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchAmendmentHistory()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.Amendments.FetchAmendmentHistory, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = DataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }



        #endregion

    }
}
