using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
namespace Bosco.Model.UIModel
{
   public class AuditInfoSystem :SystemBase
   {
       #region VariableDeclaration
       ResultArgs resultArgs = null;
       #endregion

       #region Constructor
       public AuditInfoSystem()
       {
 
       }
       public AuditInfoSystem(int AuditInfoId)
       {
           FillAuditorInfoDetails(AuditInfoId);
       }
       #endregion

       #region Properties
       public int AuditInfoId { get; set; }
       public int ProjectId { get; set; }
       public DateTime AuditBegin { get; set; }
       public DateTime AuditEnd { get; set; }
       public DateTime AuditedON { get; set; }
       public string AuditType { get; set; }
       public int AuditorId { get; set; }
       public string Notes { get; set; }
       #endregion

       #region Methods
       public ResultArgs FetchAuditorInfoDetails()
       {
          using (DataManager dataManager=new DataManager(SQLCommand.AuditInfo.FetchAll))
          {
              resultArgs = dataManager.FetchData(DataSource.DataTable);
          }
          return resultArgs;
       }

       public ResultArgs DeleteAuditorInfoDetials(int AuditInfoId)
       {
           using(DataManager dataManager=new DataManager(SQLCommand.AuditInfo.Delete))
           {
               dataManager.Parameters.Add(this.AppSchema.AuditInfo.AUDIT_INFO_IDColumn, AuditInfoId);
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }

       public ResultArgs SaveAuditorInfoDetails()
       {
           using (DataManager dataManager = new DataManager((AuditInfoId == 0) ? SQLCommand.AuditInfo.Add : SQLCommand.AuditInfo.Update))
           {
               dataManager.Parameters.Add(AppSchema.AuditInfo.AUDIT_INFO_IDColumn, AuditInfoId);
               dataManager.Parameters.Add(AppSchema.AuditInfo.PROJECT_IDColumn, ProjectId);
               dataManager.Parameters.Add(AppSchema.AuditInfo.AUDIT_BEGINColumn, AuditBegin);
               dataManager.Parameters.Add(AppSchema.AuditInfo.AUDIT_ENDColumn, AuditEnd);
               dataManager.Parameters.Add(AppSchema.AuditInfo.AUDITED_ONColumn, AuditedON);
               dataManager.Parameters.Add(AppSchema.AuditInfo.AUDIT_TYPEColumn, AuditType );
               dataManager.Parameters.Add(AppSchema.AuditInfo.DONAUD_IDColumn, AuditorId);
               dataManager.Parameters.Add(AppSchema.AuditInfo.NOTESColumn, Notes);
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }

       private ResultArgs FillAuditorInfoDetails(int AuditorInfoId)
       {
           resultArgs = AuditorInfoDetailsbyId(AuditorInfoId);
           if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
           {
               ProjectId =this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AuditInfo.PROJECT_IDColumn.ColumnName].ToString());
               AuditBegin=this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AuditInfo.AUDIT_BEGINColumn.ColumnName].ToString(),false);
               AuditEnd = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AuditInfo.AUDIT_ENDColumn.ColumnName].ToString(), false);
               AuditedON = this.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AuditInfo.AUDITED_ONColumn.ColumnName].ToString(), false);
               AuditType = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AuditInfo.AUDIT_TYPEColumn.ColumnName].ToString();
               AuditorId=this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][this.AppSchema.AuditInfo.DONAUD_IDColumn.ColumnName].ToString());
               Notes = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AuditInfo.NOTESColumn.ColumnName].ToString();
           }
           return resultArgs;
       }

       private ResultArgs AuditorInfoDetailsbyId(int AuditInfoId)
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AuditInfo.Fetch))
           {
               dataManager.Parameters.Add(AppSchema.AuditInfo.AUDIT_INFO_IDColumn.ColumnName, AuditInfoId);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }
       #endregion
   }
}
