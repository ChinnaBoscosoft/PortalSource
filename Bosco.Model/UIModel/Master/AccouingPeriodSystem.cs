using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
namespace Bosco.Model.UIModel
{
   public class AccouingPeriodSystem :SystemBase
   {
       #region VariableDeclaration
       ResultArgs resultArgs = null;
       #endregion

       #region Constructor
       public AccouingPeriodSystem()
       {
 
       }
       public AccouingPeriodSystem(int accPeriodId)
       {
           AccPeriodId = accPeriodId;
           FillAccountingPeriodDetails();
       }
       #endregion

       #region Properties
       public new int AccPeriodId { get; set; }
       public new string YearFrom { get; set; }
       public new string YearTo { get; set; }
       public int Status { get; set; }
       public string BooksBeginingDate { get; set; }
       public bool IsFirstAccYear { get; set; }
       public new int BranchId { get; set; }
       public new int ProjectId { get; set; }
       #endregion

       #region Methods
       public ResultArgs FetchAccountingPeriodDetails()
       {
          using (DataManager dataManager=new DataManager(SQLCommand.AccountingPeriod.FetchAll,DataBaseType.HeadOffice))
          {
              resultArgs = dataManager.FetchData(DataSource.DataTable);
          }
          return resultArgs;
       }

       public ResultArgs CheckIsTransaction()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.CheckIstransacton,DataBaseType.HeadOffice))
           {
               dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }
       public ResultArgs FetchMaxAccountingPeriod()
       {
           using (DataManager datamanager = new DataManager(SQLCommand.AccountingPeriod.FetchMaxAccountingPeriod, DataBaseType.HeadOffice))
           {
               resultArgs = datamanager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs IsActivePeriod(int accountyearid)
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.IsActivePeriod, DataBaseType.HeadOffice))
           {
               dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, accountyearid);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs FetchAccountingPeriodDetailsForSettings()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchForSettings))
           {
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs DeleteAccountingPeriodDetials(int accountinyearid)
       {
           using(DataManager dataManager=new DataManager(SQLCommand.AccountingPeriod.Delete,DataBaseType.HeadOffice))
           {
               dataManager.Parameters.Add(this.AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, accountinyearid);
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }

       public ResultArgs SaveAccountingPeriodDetails()
       {
           using (DataManager dataManager = new DataManager((AccPeriodId == 0) ? SQLCommand.AccountingPeriod.Add : SQLCommand.AccountingPeriod.Update,DataBaseType.HeadOffice))
           {
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId,true);
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
              // dataManager.Parameters.Add(AppSchema.AccountingPeriod.STATUSColumn, Status);
               resultArgs = dataManager.UpdateData();
               if (resultArgs.Success && resultArgs.RowsAffected > 0)
               {
                   AccPeriodId = (Convert.ToInt32(resultArgs.RowUniqueId.ToString()) == 0) ? AccPeriodId : Convert.ToInt32(resultArgs.RowUniqueId.ToString());
               }
           }
           return resultArgs;
       }


       public ResultArgs InsertAndActivateAccYear(string YearFrom,string YearTo)
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.InsertAccountingYear, DataBaseType.HeadOffice))
           {
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }


       public ResultArgs UpdateBooksBeginningFrom()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.UpdateBooksbeginningDate))
           {
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn, AccPeriodId);
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BooksBeginingDate);
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }
    
       private ResultArgs FillAccountingPeriodDetails()
       {
           resultArgs = AccountingYearId();
           if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
           {
               YearFrom = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AccountingPeriod.YEAR_FROMColumn.ColumnName].ToString();
               YearTo = resultArgs.DataSource.Table.Rows[0][this.AppSchema.AccountingPeriod.YEAR_TOColumn.ColumnName].ToString();
           }
           return resultArgs;
       }

       private ResultArgs AccountingYearId()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.Fetch,DataBaseType.HeadOffice))
           {
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName, AccPeriodId);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs FetchActiveAccountingYearId(string AccYearID)
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.IsActivePeriodId, DataBaseType.HeadOffice))
           {
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName, AccYearID);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       private int IsFirstAccountingYear()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchIsFirstAccountingyear))
           {
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.ACC_YEAR_IDColumn.ColumnName, AccPeriodId);
               resultArgs = dataManager.FetchData(DataSource.Scalar);
           }
           return resultArgs.DataSource.Sclar.ToInteger ;
       }

       public ResultArgs FetchBooksBeginingFrom()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchBooksBeginingFrom))
           {
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs FetchYearTo()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchTransactionYearTo))
           {
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }
       public ResultArgs ValidateBooksBeginning()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.ValidateBooksBegining))
           {
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.BOOKS_BEGINNING_FROMColumn, BookBeginFrom);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs FetchActiveTransactionPeriod()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchActiveTransactionperiod,DataBaseType.HeadOffice))
           {
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs FetchRecentProjectDetails(string UserId)
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FecthRecentProjectDetails))
           {
               dataManager.Parameters.Add(this.AppSchema.VoucherMaster.CREATED_BYColumn, UserId);
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs FetchRecentVoucherDate()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.FetchRecentVoucherDate,DataBaseType.HeadOffice))
           {
               if (BranchId > 0)
               {
                   dataManager.Parameters.Add(this.AppSchema.BranchOffice.BRANCH_OFFICE_IDColumn, BranchId);
               }
               if (ProjectId > 0)
               {
                   dataManager.Parameters.Add(this.AppSchema.Project.PROJECT_IDColumn, ProjectId);
               }
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }

       public ResultArgs IsAccountingPeriodExists()
       {
           using (DataManager dataManager = new DataManager(SQLCommand.AccountingPeriod.IsAccountingPeriodExists, DataBaseType.HeadOffice))
           {
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_FROMColumn, YearFrom);
               dataManager.Parameters.Add(AppSchema.AccountingPeriod.YEAR_TOColumn, YearTo);
               resultArgs = dataManager.FetchData(DataSource.DataTable);
           }
           return resultArgs;
       }


       #endregion
   }
}
