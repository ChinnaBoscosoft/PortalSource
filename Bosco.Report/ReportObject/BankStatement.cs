using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using Bosco.Utility;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using System.Data;
using Bosco.Report.Base;

namespace Bosco.Report.ReportObject
{
    public partial class BankStatement : Report.Base.ReportBase
    {
        #region Variable Decelaration
        ResultArgs resultArgs = null;
        private string BankStatments = string.Empty;
        object sqlCommandId = string.Empty;
        #endregion

        public BankStatement()
        {
            InitializeComponent();
        }
        public override void ShowReport()
        {
            // BindCashBankOpBalance();
            base.ShowReport();
        }
        private ResultArgs GetReportSource(BankType bankType)
        {
            try
            {
                BankStatments = FetchBankTypeQueries(bankType);

                using (DataManager dataManager = new DataManager(sqlCommandId,DataBaseType.HeadOffice))
                {
                    dataManager.Parameters.Add(this.ReportParameters.PROJECT_IDColumn, this.ReportProperties.Project);
                    dataManager.Parameters.Add(this.ReportParameters.DATE_AS_ONColumn, this.ReportProperties.DateAsOn);


                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DAO.Data.DataSource.DataTable, BankStatments);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
            return resultArgs;
        }

        private string FetchBankTypeQueries(BankType bankType)
        {
            string query = string.Empty;
            try
            {
                switch (bankType)
                {
                    case BankType.Cleared:
                        {
                            query = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ChequeCleared);
                            sqlCommandId = SQL.ReportSQLCommand.BankReport.ChequeCleared;
                            break;
                        }
                    case BankType.Uncleared:
                        {
                            query = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ChequeUncleared);
                            sqlCommandId = SQL.ReportSQLCommand.BankReport.ChequeUncleared;
                            break;
                        }
                    case BankType.Realized:
                        {
                            query = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ChequeRealiszed);
                            sqlCommandId = SQL.ReportSQLCommand.BankReport.ChequeRealiszed;
                            break;
                        }
                    case BankType.Unrealized:
                        {
                            query = this.GetBankReportSQL(SQL.ReportSQLCommand.BankReport.ChequeUnrealized);
                            sqlCommandId = SQL.ReportSQLCommand.BankReport.ChequeUnrealized;
                            break;
                            
                        }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
            return query;
        }

        public void BindReportSource(BankType bankType)
        {
            try
            {
                resultArgs = GetReportSource(bankType);
                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                {
                    resultArgs.DataSource.Table.TableName = "BankCheque";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
                else
                {
                    resultArgs.DataSource.Table.TableName = "BankCheque";
                    this.DataSource = resultArgs.DataSource.Table;
                    this.DataMember = resultArgs.DataSource.Table.TableName;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }
    }
}
