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
    public class LegalEntitySystem : SystemBase
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Properties

        public int CustomerId { get; set; }
        public string InstitueName { get; set; }
        public string SocietyName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Place { get; set; }
        public string Fax { get; set; }
        public string GIRNo { get; set; }
        public string A12No { get; set; }
        public string PANNo { get; set; }
        public string TANNo { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string EMail { get; set; }
        public string Pincode { get; set; }
        public new string Country { get; set; }
        public string URL { get; set; }
        public string RegNo { get; set; }
        public string PermissionNo { get; set; }
        public string AssoicationNature { get; set; }
        public int Denomination { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime PermissionDate { get; set; }
        public string Branch_Office_Code { get; set; }
        public string AssociationOther { get; set; }
        public string DenominationOther { get; set; }
        public string FCRINo { get; set; }
        public DateTime FCRIRegisterDate { get; set; }
        public string G80No { get; set; }
        public DateTime G80RegDate { get; set; }
        public int Is_Foundation { get; set; }
        public string PrincipalActivity { get; set; }
        public int BranchId { get; set; }

        #endregion

        #region Constructor
        public LegalEntitySystem()
        {
        }

        public LegalEntitySystem(int CustomerId)
        {
            this.CustomerId = CustomerId;
            FillBankProperties(DataBaseType.HeadOffice);
        }
        #endregion

        #region Methods
        public ResultArgs SaveLegalEntityDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(CustomerId.Equals(0) ? SQLCommand.LegalEntity.Add : SQLCommand.LegalEntity.Update, connectTo))
            {
                dataManager.Parameters.Add(AppSchema.LegalEntity.CUSTOMERIDColumn, CustomerId);
                dataManager.Parameters.Add(AppSchema.LegalEntity.INSTITUTENAMEColumn, InstitueName);
                dataManager.Parameters.Add(AppSchema.LegalEntity.SOCIETYNAMEColumn, SocietyName);
                dataManager.Parameters.Add(AppSchema.LegalEntity.CONTACTPERSONColumn, ContactPerson);
                dataManager.Parameters.Add(AppSchema.LegalEntity.ADDRESSColumn, Address);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PLACEColumn, Place);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PHONEColumn, Phone);
                dataManager.Parameters.Add(AppSchema.LegalEntity.FAXColumn, Fax);
                dataManager.Parameters.Add(AppSchema.LegalEntity.COUNTRYColumn, Country);
                dataManager.Parameters.Add(AppSchema.LegalEntity.A12NOColumn, A12No);
                dataManager.Parameters.Add(AppSchema.LegalEntity.GIRNOColumn, GIRNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.TANNOColumn, TANNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PANNOColumn, PANNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.STATEColumn, State);
                dataManager.Parameters.Add(AppSchema.LegalEntity.EMAILColumn, EMail);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PINCODEColumn, Pincode);
                dataManager.Parameters.Add(AppSchema.LegalEntity.URLColumn, URL);
                dataManager.Parameters.Add(AppSchema.LegalEntity.REGNOColumn, RegNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONNOColumn, PermissionNo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.FCRINOColumn, FCRINo);
                dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTYGNOColumn, G80No);
                dataManager.Parameters.Add(AppSchema.LegalEntity.IS_FOUNDATIONColumn, Is_Foundation);
                dataManager.Parameters.Add(AppSchema.LegalEntity.PRINCIPAL_ACTIVITYColumn, PrincipalActivity);

                dataManager.Parameters.Add(AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn, AssociationOther);
                dataManager.Parameters.Add(AppSchema.LegalEntity.OTHER_DENOMINATIONColumn, DenominationOther);
                if (FCRIRegisterDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.FCRIREGDATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.FCRIREGDATEColumn, FCRIRegisterDate);
                }
                if (RegDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.REGDATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.REGDATEColumn, RegDate);
                }
                if (PermissionDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONDATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.PERMISSIONDATEColumn, PermissionDate);
                }

                if (G80RegDate == DateTime.MinValue)
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn, null);
                }
                else
                {
                    dataManager.Parameters.Add(AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn, G80RegDate);
                }

                dataManager.Parameters.Add(AppSchema.LegalEntity.ASSOCIATIONNATUREColumn, AssoicationNature);
                dataManager.Parameters.Add(AppSchema.LegalEntity.DENOMINATIONColumn, Denomination);


                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs FetchLegalEntity(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.FetchAll, DataBaseType.HeadOffice))
            {
                if (BranchId != 0)
                    dataManager.Parameters.Add(AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FillBankProperties(DataBaseType connectTo)
        {
            resultArgs = LegalEntityDetailsById(connectTo);
            DataTable dtLegalEntityData = resultArgs.DataSource.Table;
            if (dtLegalEntityData != null && dtLegalEntityData.Rows.Count > 0)
            {
                //InstitueName = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.INSTITUTENAMEColumn.ColumnName].ToString();
                SocietyName = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.SOCIETYNAMEColumn.ColumnName].ToString();
                ContactPerson = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.CONTACTPERSONColumn.ColumnName].ToString();
                Address = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.ADDRESSColumn.ColumnName].ToString();
                Place = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PLACEColumn.ColumnName].ToString();
                Fax = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.FAXColumn.ColumnName].ToString();
                GIRNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.GIRNOColumn.ColumnName].ToString();
                A12No = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.A12NOColumn.ColumnName].ToString();
                PANNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PANNOColumn.ColumnName].ToString();
                TANNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.TANNOColumn.ColumnName].ToString();
                Phone = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PHONEColumn.ColumnName].ToString();
                State = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.STATEColumn.ColumnName].ToString();
                EMail = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.EMAILColumn.ColumnName].ToString();
                Pincode = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PINCODEColumn.ColumnName].ToString();
                Country = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.COUNTRYColumn.ColumnName].ToString();
                URL = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.URLColumn.ColumnName].ToString();
                RegNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.REGNOColumn.ColumnName].ToString();
                PermissionNo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PERMISSIONNOColumn.ColumnName].ToString();
                AssoicationNature = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.ASSOCIATIONNATUREColumn.ColumnName].ToString();
                Denomination = NumberSet.ToInteger(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.DENOMINATIONColumn.ColumnName].ToString());
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.LegalEntity.REGDATEColumn.ColumnName] != DBNull.Value)
                {
                    RegDate = DateSet.ToDate(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.REGDATEColumn.ColumnName].ToString(), false);
                }
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName] != DBNull.Value)
                {
                    PermissionDate = DateSet.ToDate(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PERMISSIONDATEColumn.ColumnName].ToString(), false);
                }
                FCRINo = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.FCRINOColumn.ColumnName].ToString();
                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName] != DBNull.Value)
                {
                    FCRIRegisterDate = DateSet.ToDate(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.FCRIREGDATEColumn.ColumnName].ToString(), false);
                }
                G80No = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.EIGHTYGNOColumn.ColumnName].ToString();

                if (resultArgs.DataSource.Table.Rows[0][this.AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName] != DBNull.Value)
                {
                    G80RegDate = DateSet.ToDate(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.EIGHTY_GNO_REG_DATEColumn.ColumnName].ToString(), false);
                }

                Is_Foundation = NumberSet.ToInteger(dtLegalEntityData.Rows[0][AppSchema.LegalEntity.IS_FOUNDATIONColumn.ColumnName].ToString());
                PrincipalActivity = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.PRINCIPAL_ACTIVITYColumn.ColumnName].ToString();

                AssociationOther = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.OTHER_ASSOCIATION_NATUREColumn.ColumnName].ToString();
                DenominationOther = dtLegalEntityData.Rows[0][AppSchema.LegalEntity.OTHER_DENOMINATIONColumn.ColumnName].ToString();
            }
        }

        private ResultArgs LegalEntityDetailsById(DataBaseType connectTo)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LegalEntity.FetchByID, connectTo))
            {
                dataMember.Parameters.Add(AppSchema.LegalEntity.CUSTOMERIDColumn, CustomerId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteLegalEntityData(int Customerid, DataBaseType connectTo)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LegalEntity.Delete, connectTo))
            {
                this.CustomerId = Customerid;
                dataMember.Parameters.Add(this.AppSchema.LegalEntity.CUSTOMERIDColumn, CustomerId);
                resultArgs = dataMember.UpdateData();
            }
            return resultArgs;
        }
        public ResultArgs LegalEntityFechAll(DataBaseType connectTo)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.LegalEntity.LegalEntityFetchAll, connectTo))
            {
                dataMember.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_CODEColumn, Branch_Office_Code);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchSocietybyBranch(string BranchCode)
        {
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.FetchLegalEntityByBranch, DataBaseType.HeadOffice))
            {
                if (!string.IsNullOrEmpty(BranchCode) && BranchCode != "0")
                {
                    dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchCode);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                }
                resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchAttachedSociety()
        {
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.FetchBranchAttachedSociety, DataBaseType.HeadOffice))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchLegalEntity()
        {
            ResultArgs resultArgs = null;
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.FetchAll, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(dataManager, DataSource.DataTable);
            }
            return resultArgs;
        }

        public DataTable CheckNoofLegalentity(string Projectid)
        {
            ResultArgs resultArgs = null;
            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.CheckLegalEntity, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, Projectid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }

        public ResultArgs FetchSocietyByProject(string Projectid)
        {
            ResultArgs resultArgs = null;
            using (Bosco.DAO.Data.DataManager dataManager = new DAO.Data.DataManager(Bosco.DAO.Schema.SQLCommand.LegalEntity.FetchSocietyByProject, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, Projectid);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable);
            }
            return resultArgs;
        }

        public int GetCount()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.LegalEntityCount, DataBaseType.HeadOffice))
            {
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs FetchCustomerIdBySociety()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.LegalEntity.FetchCustomerIdyBySocietyName, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(AppSchema.LegalEntity.SOCIETYNAMEColumn, SocietyName);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        #endregion
    }
}
