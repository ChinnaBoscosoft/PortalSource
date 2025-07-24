/*  Class Name      : CountrySystem.cs
 *  Purpose         : To have all the logic of Executive Member Details
 *  Author          : Chinna
 *  Created on      : 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Collections;
using System.IO;

namespace Bosco.Model.UIModel
{
    public class ExecutiveMemberSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public ExecutiveMemberSystem()
        {
        }
        public ExecutiveMemberSystem(int ExecutiveId)
        {
            FillExecutiveMember(ExecutiveId);
        }
        #endregion

        #region ExecutiveMember Property
        public int ExecutiveId { get; set; }
        public string ExecutiveName { get; set; }
        public string FatherName { get; set; }
        public string DateOfBirth { get; set; }
        public string Religion { get; set; }
        public string Role { get; set; }
        public string Nationality { get; set; }
        public string Occupation { get; set; }
        public string Association { get; set; }
        public string OfficeBearer { get; set; }
        public string Place { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Pan_SSN { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string URL { get; set; }
        public string DateOfAppointment { get; set; }
        public string DateOfExit { get; set; }
        public byte[] ImageData { get; set; }
        public string Notes { get; set; }
        public int LegalEntityId { get; set; }
        #endregion

        #region Methods

        public ResultArgs FetchExecutiveMemberDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExecutiveMembers.FetchAll, connectTo))
            {
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs DeleteExecuteMember(int ExecutiveId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExecutiveMembers.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn, ExecutiveId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs SaveExecutiveMemberDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((ExecutiveId == 0) ? SQLCommand.ExecutiveMembers.Add : SQLCommand.ExecutiveMembers.Update, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn, ExecutiveId);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVEColumn, ExecutiveName);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.NAMEColumn, FatherName);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.DATE_OF_BIRTHColumn, DateOfBirth);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.RELIGIONColumn, Religion);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.ROLEColumn, Role);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.NATIONALITYColumn, Nationality);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.OCCUPATIONColumn, Occupation);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.ASSOCIATIONColumn, Association);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.OFFICE_BEARERColumn, OfficeBearer);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.PLACEColumn, Place);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.STATE_IDColumn, StateId);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.COUNTRY_IDColumn, CountryId);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.ADDRESSColumn, Address);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.PIN_CODEColumn, PinCode);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.PAN_SSNColumn, Pan_SSN);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.PHONEColumn, Phone);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.FAXColumn, Fax);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EMAILColumn, EMail);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.URLColumn, URL);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.DATE_OF_APPOINTMENTColumn, DateOfAppointment);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.DATE_OF_EXITColumn, DateOfExit);
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.NOTESColumn, Notes);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;

        }

        private ResultArgs FillExecutiveMember(int ExecutiveId)
        {
            resultArgs = FillDetailsbyId(ExecutiveId);
            if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
            {
                ExecutiveName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.EXECUTIVEColumn.ColumnName].ToString();
                FatherName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.NAMEColumn.ColumnName].ToString();
                DateOfBirth = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.DATE_OF_BIRTHColumn.ColumnName].ToString();
                Religion = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.RELIGIONColumn.ColumnName].ToString();
                Role = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.ROLEColumn.ColumnName].ToString();
                Nationality = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.NATIONALITYColumn.ColumnName].ToString();
                Occupation = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.OCCUPATIONColumn.ColumnName].ToString();
                Association = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.ASSOCIATIONColumn.ColumnName].ToString();
                OfficeBearer = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.OFFICE_BEARERColumn.ColumnName].ToString();
                Place = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.PLACEColumn.ColumnName].ToString();
                StateId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.STATE_IDColumn.ColumnName].ToString());
                CountryId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.COUNTRY_IDColumn.ColumnName].ToString());
                Address = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.ADDRESSColumn.ColumnName].ToString();
                PinCode = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.PIN_CODEColumn.ColumnName].ToString();
                Pan_SSN = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.PAN_SSNColumn.ColumnName].ToString();
                Phone = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.PHONEColumn.ColumnName].ToString();
                Fax = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.FAXColumn.ColumnName].ToString();
                EMail = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.EMAILColumn.ColumnName].ToString();
                URL = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.URLColumn.ColumnName].ToString();
                DateOfAppointment = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.DATE_OF_APPOINTMENTColumn.ColumnName].ToString();
                DateOfExit = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.DATE_OF_EXITColumn.ColumnName].ToString();
                Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.ExecutiveMembers.NOTESColumn.ColumnName].ToString();
            }
            return resultArgs;
        }

        private ResultArgs FillDetailsbyId(int ExecutiveId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.ExecutiveMembers.Fetch, DataBaseType.HeadOffice))
            {
                dataManager.Parameters.Add(this.AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn, ExecutiveId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs MapGoverningMembers(List<object> ExecutiveId, int customerId)
        {
            using (DataManager dataManager = new DataManager(DataBaseType.HeadOffice))
            {
                dataManager.BeginTransaction();
                resultArgs = MapGoverningMembertoLegalEntity(dataManager, ExecutiveId, customerId);
                dataManager.EndTransaction();
            }

            return resultArgs;
        }

        private ResultArgs MapGoverningMembertoLegalEntity(DataManager dManager, List<object> ExecuteIds, int customerId)
        {
            using (DataManager DataManager = new DataManager())
            {
                DataManager.Database = dManager.Database;
                LegalEntityId = customerId;
                resultArgs = DeleteMappedGoverningMember();
               if (resultArgs.Success)
               {
                    foreach (object ExecutiveID in ExecuteIds)
                    {
                        resultArgs = MapGoverningMember(customerId, this.NumberSet.ToInteger(ExecutiveID.ToString()));
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }


        public ResultArgs DeleteMappedGoverningMember()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.ExecutiveMembers.UnmapLegalEntitytoGoverningMember, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.ExecutiveMembers.CUSTOMERIDColumn,LegalEntityId);
                resultArgs = DataManager.UpdateData();
            }
            return resultArgs;
        }

        private ResultArgs MapGoverningMember(int customerId, int ExecutiveId)
        {
            using (DataManager DataManager = new DataManager(SQLCommand.ExecutiveMembers.MapGoverningMember, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.ExecutiveMembers.CUSTOMERIDColumn, customerId);
                DataManager.Parameters.Add(AppSchema.ExecutiveMembers.EXECUTIVE_IDColumn, ExecutiveId);
                resultArgs = DataManager.UpdateData();
            }

            return resultArgs;
        }

        public ResultArgs FetchGoverningMembersByLegalEntity()
        {
            using (DataManager DataManager = new DataManager(SQLCommand.ExecutiveMembers.FetchGoverningMemberByLegalEntity, DataBaseType.HeadOffice))
            {
                DataManager.Parameters.Add(AppSchema.ExecutiveMembers.CUSTOMERIDColumn, LegalEntityId);
                resultArgs = DataManager.FetchData(DataSource.DataTable);
            }

            return resultArgs;
        }
        #endregion
    }
}
