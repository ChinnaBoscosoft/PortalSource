using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.Utility;
using Bosco.Model;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Utility.ConfigSetting;

namespace Bosco.Model.UIModel.TroubleTicket
{
    public class TroubleTicketingSystem : SystemBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = null;
        #endregion

        #region Constructor
        public TroubleTicketingSystem()
        {

        }
        public TroubleTicketingSystem(int ticketId)
        {
            this.TicketId = ticketId;
            this.RepliedTicketId = ticketId;
            FillTicketProperties(DataBaseType.Portal);
        }
        #endregion

        #region Trouble Ticket Properties
        public int TicketId { get; set; }
        public string HeadOfficeCode { get; set; }
        public string BranchOfficeCode { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string AttachFileName { get; set; }
        public int PostedBy { get; set; }
        public int RepliedTicketId { get; set; }
        public string UserName { get; set; }
        public string AttachFileNamePhysical { get; set; }
        public int Status { get; set; }

        #endregion

        #region Methods
        public ResultArgs SaveTicketDetails(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager((TicketId == 0) ? SQLCommand.TroubleTicket.Add : SQLCommand.TroubleTicket.Update, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.TICKET_IDColumn, TicketId);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.SUBJECTColumn, Subject);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.DESCRIPTIONColumn, Description);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.PRIORITYColumn, Priority);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.POSTED_DATEColumn, PostedDate);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.COMPLETED_DATEColumn, CompletedDate);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.ATTACH_FILE_NAMEColumn, AttachFileName);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.POSTED_BYColumn, PostedBy);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.REPLIED_TICKET_IDColumn, RepliedTicketId);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.USER_NAMEColumn, UserName);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.PHYSICAL_FILE_NAMEColumn, AttachFileNamePhysical);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.STATUSColumn, Status);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        private void FillTicketProperties(DataBaseType connectTo)
        {
            resultArgs = FetchTicketDetailsById(connectTo);
            DataTable dtTicketEdit = resultArgs.DataSource.Table;
            if (dtTicketEdit != null && dtTicketEdit.Rows.Count > 0)
            {
                //      TicketId = NumberSet.ToInteger(dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.TICKET_IDColumn.ColumnName].ToString());
                HeadOfficeCode = dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.HEAD_OFFICE_CODEColumn.ColumnName].ToString();
                BranchOfficeCode = dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.BRANCH_OFFICE_CODEColumn.ColumnName].ToString();
                Subject = dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.SUBJECTColumn.ColumnName].ToString();
                Description = dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.DESCRIPTIONColumn.ColumnName].ToString();
                Priority = NumberSet.ToInteger(dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.PRIORITYColumn.ColumnName].ToString());
                //    PostedDate = DateSet.ToDate(dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.POSTED_DATEColumn.ColumnName].ToString());
                //    CompletedDate=DateSet.ToDate(dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.COMPLETED_DATEColumn.ColumnName].ToString();
                AttachFileName = dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.ATTACH_FILE_NAMEColumn.ColumnName].ToString();
                PostedBy = NumberSet.ToInteger(dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.POSTED_BYColumn.ColumnName].ToString());
                RepliedTicketId = NumberSet.ToInteger(dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.REPLIED_TICKET_IDColumn.ColumnName].ToString());
                UserName = dtTicketEdit.Rows[0][AppSchema.TROUBLETICKET.USER_NAMEColumn.ColumnName].ToString();
            }
        }
        public ResultArgs FetchAllTroubleTicket(DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TroubleTicket.FetchAll, connectTo))
            {
                if (!string.IsNullOrEmpty(base.HeadOfficeCode))
                    dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.HEAD_OFFICE_CODEColumn, base.HeadOfficeCode);
                if (base.IsBranchOfficeUser)
                {
                    if (!string.IsNullOrEmpty(base.HeadOfficeCode))
                        dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.BRANCH_OFFICE_CODEColumn, base.LoginUserBranchOfficeCode);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs DeleteTroubleTicket(int ticketId, DataBaseType connectTo)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TroubleTicket.Delete, connectTo))
            {
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.TICKET_IDColumn, ticketId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchTicketDetailsById(DataBaseType connectTo)
        {
            using (DataManager dataMember = new DataManager(SQLCommand.TroubleTicket.FetchTicketById, connectTo))
            {
                dataMember.Parameters.Add(this.AppSchema.TROUBLETICKET.TICKET_IDColumn, TicketId);
                resultArgs = dataMember.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs UpdateStatus()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TroubleTicket.UpdateStatus, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(AppSchema.TROUBLETICKET.STATUSColumn, Status);
                dataManager.Parameters.Add(AppSchema.TROUBLETICKET.TICKET_IDColumn, TicketId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;

        }

        public ResultArgs FetchReplies()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TroubleTicket.FetchReplies, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(AppSchema.TROUBLETICKET.REPLIED_TICKET_IDColumn, TicketId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public ResultArgs FetchBranchOfficeTickets()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TroubleTicket.FetchTicketsByBranch, DataBaseType.Portal))
            {
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }

        public void FetchLoginUserDetailsbyBOCode()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.TroubleTicket.FetchUserDetailsByBOCode, DataBaseType.HeadOffice))
            {
                using (Bosco.Model.SettingSystem settingSystem = new Bosco.Model.SettingSystem())
                {
                    using (UserProperty userpro = new UserProperty())
                    {
                        string hoConnectionString = settingSystem.GetHeadOfficeDBConnection(HeadOfficeCode);
                        userpro.HeadOfficeDBConnection = hoConnectionString;

                        dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.HEAD_OFFICE_CODEColumn, HeadOfficeCode);
                        dataManager.Parameters.Add(this.AppSchema.TROUBLETICKET.BRANCH_OFFICE_CODEColumn, BranchOfficeCode);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            PostedBy = NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.User.USER_IDColumn.ColumnName].ToString());
                            UserName = resultArgs.DataSource.Table.Rows[0][this.AppSchema.User.USER_NAMEColumn.ColumnName].ToString();
                        }
                    }
                }
            }
        }

        #endregion

    }
}
