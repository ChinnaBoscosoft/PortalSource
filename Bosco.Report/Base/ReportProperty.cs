/*  Class Name      : ReportProperty.cs
 *  Purpose         : To get Assembly Qualified Name of a report
 *                    by selected report id and get report source 
 *                    from mapped report interface object
 *  Author          : CS
 *  Created on      : 21-Jul-2009
 */

using System;
using System.Data;
using Bosco.Report;
using System.Xml;
using Bosco.Utility;
using System.Resources;
using System.Reflection;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.SessionState;
namespace Bosco.Report.Base
{
    public class ReportProperty : CommonMember
    {
        private ReportProperty current = null;
        //private  DataView dvReportSettingInfo = null;
        private ReportSetting.ReportSettingDataTable dtReportSettingSchema = new ReportSetting.ReportSettingDataTable();
        private static object objLoc = new object();
        private const string reportSettingFile = "ReportSetting.xml";
        private string reportId = "";

        public ReportProperty Current
        {
            get
            {
                if (current == null)
                {
                    current = new ReportProperty();
                    current.LoadReportSetting();
                }
                return current;
            }
        }

        public string ReportId
        {
            get { return (string)HttpContext.Current.Session["reportId"]; }
            set
            {
                current = new ReportProperty();
                current.LoadReportSetting();
                HttpContext.Current.Session["reportId"] = value;
                SetReportSettingInfo();

            }
        }

        private DataView dvReportSettingInfo
        {
            set { HttpContext.Current.Session["dvReportSettingInfo"] = value; }

            get
            {
                if (HttpContext.Current.Session["dvReportSettingInfo"] == null)
                    return null;
                else
                    return (DataView)HttpContext.Current.Session["dvReportSettingInfo"];
            }
        }
        public string ReportGroup
        {
            get
            {
                if (HttpContext.Current.Session["ReportGroup"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ReportGroup"];
            }
            set { HttpContext.Current.Session["ReportGroup"] = value; }
        }
        public string ReportName
        {
            get
            {
                if (HttpContext.Current.Session["ReportName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ReportName"];
            }
            set { HttpContext.Current.Session["ReportName"] = value; }
        }
        public string ReportTitle
        {
            get
            {
                if (HttpContext.Current.Session["ReportTitle"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ReportTitle"];
            }
            set { HttpContext.Current.Session["ReportTitle"] = value; }
        }
        public string ReportDescription
        {
            get
            {
                if (HttpContext.Current.Session["ReportDescription"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ReportDescription"];
            }
            set { HttpContext.Current.Session["ReportDescription"] = value; }
        }
        public string ReportAssembly
        {
            get
            {
                if (HttpContext.Current.Session["ReportAssembly"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ReportAssembly"];
            }
            set { HttpContext.Current.Session["ReportAssembly"] = value; }
        }
        public string AccounYear
        {
            get
            {
                if (HttpContext.Current.Session["AccounYear"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["AccounYear"];
            }
            set { HttpContext.Current.Session["AccounYear"] = value; }
        }
        string datefrom = string.Empty;
        public string DateFrom
        {
            get
            {
                if (HttpContext.Current.Session["DateFrom"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["DateFrom"];
            }
            set { HttpContext.Current.Session["DateFrom"] = value; }
        }

        string dateto = string.Empty;

        public string DateTo
        {
            get
            {
                if (HttpContext.Current.Session["DateTo"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["DateTo"];
            }
            set { HttpContext.Current.Session["DateTo"] = value; }
        }


        bool istwomonthmonth = false;

        public bool IsTwoMonthBudget
        {
            get
            {
                if (HttpContext.Current.Session["IsTwoMonthBudget"] == null)
                    return false;
                else
                    return (bool)HttpContext.Current.Session["IsTwoMonthBudget"];
            }
            set { HttpContext.Current.Session["IsTwoMonthBudget"] = value; }
        }

        string prevdatefrom = string.Empty;
        public string PrevDateFrom
        {
            get
            {
                if (HttpContext.Current.Session["PrevDateFrom"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["PrevDateFrom"];
            }
            set { HttpContext.Current.Session["PrevDateFrom"] = value; }
        }

        string prevdateto = string.Empty;

        public string PrevDateTo
        {
            get
            {
                if (HttpContext.Current.Session["PrevDateTo"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["PrevDateTo"];
            }
            set { HttpContext.Current.Session["PrevDateTo"] = value; }
        }

        public string BudgetPrevDateCaption
        {
            get
            {
                if (HttpContext.Current.Session["BudgetPrevDateCaption"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BudgetPrevDateCaption"];
            }
            set { HttpContext.Current.Session["BudgetPrevDateCaption"] = value; }
        }


        public string BudgetM1PropsedDateCaption
        {
            get
            {
                if (HttpContext.Current.Session["BudgetM1PropsedDateCaption"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BudgetM1PropsedDateCaption"];
            }
            set { HttpContext.Current.Session["BudgetM1PropsedDateCaption"] = value; }
        }

        public string BudgetM2PropsedDateCaption
        {
            get
            {
                if (HttpContext.Current.Session["BudgetM2PropsedDateCaption"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BudgetM2PropsedDateCaption"];
            }
            set { HttpContext.Current.Session["BudgetM2PropsedDateCaption"] = value; }
        }

        public string DateAsOn
        {
            get
            {
                if (HttpContext.Current.Session["DateAsOn"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["DateAsOn"];
            }
            set { HttpContext.Current.Session["DateAsOn"] = value; }
        }

        string showbudgetledgerActualBalance = string.Empty;
        public string ShowBudgetLedgerActualBalance
        {
            get
            {
                if (HttpContext.Current.Session["DateFrom"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["DateFrom"];
            }
            set { HttpContext.Current.Session["DateFrom"] = value; }
        }


        public int IncludeAllLedger
        {
            get
            {
                if (HttpContext.Current.Session["IncludeAllLedger"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeAllLedger"];
            }
            set { HttpContext.Current.Session["IncludeAllLedger"] = value; }
        }
        public int ShowByLedger
        {
            get
            {
                if (HttpContext.Current.Session["ShowByLedger"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowByLedger"];
            }
            set { HttpContext.Current.Session["ShowByLedger"] = value; }
        }
        public int ShowByLedgerGroup
        {
            get
            {
                if (HttpContext.Current.Session["ShowByLedgerGroup"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowByLedgerGroup"];
            }
            set { HttpContext.Current.Session["ShowByLedgerGroup"] = value; }
        }
        public int BreakByCostCentre
        {
            get
            {
                if (HttpContext.Current.Session["BreakByCostCentre"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["BreakByCostCentre"];
            }
            set { HttpContext.Current.Session["BreakByCostCentre"] = value; }
        }
        public int ShowDailyBalance
        {
            get
            {
                if (HttpContext.Current.Session["ShowDailyBalance"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowDailyBalance"];
            }
            set { HttpContext.Current.Session["ShowDailyBalance"] = value; }
        }

        public int FDRegisterStatus
        {
            get
            {
                if (HttpContext.Current.Session["FDRegisterStatus"] == null)
                    return 1;//1-ALL 2-Active 3-Closed
                else
                    return (int)HttpContext.Current.Session["FDRegisterStatus"];
            }
            set { HttpContext.Current.Session["FDRegisterStatus"] = value; }
        }

        public int ShowByCostCentre
        {
            get
            {
                if (HttpContext.Current.Session["ShowByCostCentre"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowByCostCentre"];
            }
            set { HttpContext.Current.Session["ShowByCostCentre"] = value; }
        }
        public int ReportBorderStyle
        {
            get
            {
                if (HttpContext.Current.Session["ReportBorderStyle"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ReportBorderStyle"];
            }
            set { HttpContext.Current.Session["ReportBorderStyle"] = value; }
        }

        public int ShowByCostCentreCategory
        {
            get
            {
                if (HttpContext.Current.Session["ShowByCostCentreCategory"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowByCostCentreCategory"];
            }
            set { HttpContext.Current.Session["ShowByCostCentreCategory"] = value; }
        }

        public int BreakUpByCostCentre
        {
            get
            {
                if (HttpContext.Current.Session["BreakUpByCostCentre"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["BreakUpByCostCentre"];
            }
            set { HttpContext.Current.Session["BreakUpByCostCentre"] = value; }
        }

        public int IncludeDetailed
        {
            get
            {
                if (HttpContext.Current.Session["IncludeDetailed"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeDetailed"];
            }
            set { HttpContext.Current.Session["IncludeDetailed"] = value; }
        }

        public int IncludeJournal
        {
            get
            {
                if (HttpContext.Current.Session["IncludeJournal"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeJournal"];
            }
            set { HttpContext.Current.Session["IncludeJournal"] = value; }
        }
        public int IncludeInKind
        {
            get
            {
                if (HttpContext.Current.Session["IncludeInKind"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeInKind"];
            }
            set { HttpContext.Current.Session["IncludeInKind"] = value; }
        }
        public int IncludeLedgerGroupTotal
        {
            get
            {
                if (HttpContext.Current.Session["IncludeLedgerGroupTotal"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeLedgerGroupTotal"];
            }
            set { HttpContext.Current.Session["IncludeLedgerGroupTotal"] = value; }
        }
        public int IncludeLedgerGroup
        {
            get
            {
                if (HttpContext.Current.Session["IncludeLedgerGroup"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeLedgerGroup"];
            }
            set { HttpContext.Current.Session["IncludeLedgerGroup"] = value; }
        }
        public int IncludeCostCentre
        {
            get
            {
                if (HttpContext.Current.Session["IncludeCostCentre"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeCostCentre"];
            }
            set { HttpContext.Current.Session["IncludeCostCentre"] = value; }
        }
        public int ShowMonthTotal
        {
            get
            {
                if (HttpContext.Current.Session["ShowMonthTotal"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowMonthTotal"];
            }
            set { HttpContext.Current.Session["ShowMonthTotal"] = value; }
        }
        public int ShowDonorAddress
        {
            get
            {
                if (HttpContext.Current.Session["ShowDonorAddress"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowDonorAddress"];
            }
            set { HttpContext.Current.Session["ShowDonorAddress"] = value; }
        }
        public int ShowDonorCategory
        {
            get
            {
                if (HttpContext.Current.Session["ShowDonorCategory"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowDonorCategory"];
            }
            set { HttpContext.Current.Session["ShowDonorCategory"] = value; }
        }
        public int IncludeBankAccount
        {
            get
            {
                if (HttpContext.Current.Session["IncludeBankAccount"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeBankAccount"];
            }
            set { HttpContext.Current.Session["IncludeBankAccount"] = value; }
        }
        public int IncludeBankDetails
        {
            get
            {
                if (HttpContext.Current.Session["IncludeBankDetails"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeBankDetails"];
            }
            set { HttpContext.Current.Session["IncludeBankDetails"] = value; }
        }
        public int ShowDetailedBalance
        {
            get
            {
                if (HttpContext.Current.Session["ShowDetailedBalance"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowDetailedBalance"];
            }
            set { HttpContext.Current.Session["ShowDetailedBalance"] = value; }
        }
        public string Project
        {
            get
            {
                if (HttpContext.Current.Session["Project"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["Project"];
            }
            set { HttpContext.Current.Session["Project"] = value; }
        }
        public string ProjectId
        {
            get
            {
                if (HttpContext.Current.Session["ProjectId"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ProjectId"];
            }
            set { HttpContext.Current.Session["ProjectId"] = value; }
        }


        public string BankAccount
        {
            get
            {
                if (HttpContext.Current.Session["BankAccount"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BankAccount"];
            }
            set { HttpContext.Current.Session["BankAccount"] = value; }
        }
        public string InstituteName
        {
            get
            {
                if (HttpContext.Current.Session["InstituteName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["InstituteName"];
            }
            set { HttpContext.Current.Session["InstituteName"] = value; }
        }
        public string SocietyName
        {
            get
            {
                if (HttpContext.Current.Session["SocietyName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["SocietyName"];
            }
            set { HttpContext.Current.Session["SocietyName"] = value; }
        }
        public Int32 SelectedSocietyCount
        {
            get
            {
                if (HttpContext.Current.Session["SelectedSocietyCount"] == null)
                    return 0;
                else
                    return (Int32)HttpContext.Current.Session["SelectedSocietyCount"];
            }
            set { HttpContext.Current.Session["SelectedSocietyCount"] = value; }
        }

        public string ReportCodeType
        {
            get
            {
                if (HttpContext.Current.Session["ReportCodeType"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ReportCodeType"];
            }
            set
            {
                HttpContext.Current.Session["ReportCodeType"] = value;
            }
        }

        public string LegalAddress
        {
            get
            {
                if (HttpContext.Current.Session["LegalAddress"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["LegalAddress"];
            }
            set { HttpContext.Current.Session["LegalAddress"] = value; }
        }
        public string LedgalEntityId
        {
            get
            {
                if (HttpContext.Current.Session["LedgalEntityId"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["LedgalEntityId"];
            }
            set { HttpContext.Current.Session["LedgalEntityId"] = value; }
        }
        public int ShowTitleSocietyName
        {
            get
            {
                if (HttpContext.Current.Session["ShowTitleSocietyName"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowTitleSocietyName"];
            }
            set { HttpContext.Current.Session["ShowTitleSocietyName"] = value; }
        }

        public string FDAccountID
        {
            get
            {
                if (HttpContext.Current.Session["FDAccountID"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["FDAccountID"];
            }
            set { HttpContext.Current.Session["FDAccountID"] = value; }
        }
        public string FDAccount
        {
            get
            {
                if (HttpContext.Current.Session["FDAccount"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["FDAccount"];
            }
            set { HttpContext.Current.Session["FDAccount"] = value; }
        }
        public string BudgetId
        {
            get
            {
                if (HttpContext.Current.Session["BudgetId"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BudgetId"];
            }
            set { HttpContext.Current.Session["BudgetId"] = value; }
        }
        public string Budget
        {
            get
            {
                if (HttpContext.Current.Session["Budget"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["Budget"];
            }
            set { HttpContext.Current.Session["Budget"] = value; }
        }
        public string UnSelectedBudgetId
        {
            get;
            set;
        }
        public string UnSelectedBankAccountId
        {
            get;
            set;
        }
        public string BankAccountId
        {
            get
            {
                if (HttpContext.Current.Session["BankAccountId"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BankAccountId"];
            }
            set { HttpContext.Current.Session["BankAccountId"] = value; }
        }
        public string Ledger
        {
            get
            {
                if (HttpContext.Current.Session["Ledger"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["Ledger"];
            }
            set { HttpContext.Current.Session["Ledger"] = value; }
        }

        public string CongregationLedger
        {
            get
            {
                if (HttpContext.Current.Session["CongregationLedger"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["CongregationLedger"];
            }
            set { HttpContext.Current.Session["CongregationLedger"] = value; }
        }

        public string LedgerGroup
        {
            get
            {
                if (HttpContext.Current.Session["LedgerGroup"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["LedgerGroup"];
            }
            set { HttpContext.Current.Session["LedgerGroup"] = value; }
        }

        public string LedgerName
        {
            get
            {
                if (HttpContext.Current.Session["LedgerName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["LedgerName"];
            }
            set { HttpContext.Current.Session["LedgerName"] = value; }
        }
        public string LedgerGroupName
        {
            get
            {
                if (HttpContext.Current.Session["LedgerGroupName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["LedgerGroupName"];
            }
            set { HttpContext.Current.Session["LedgerGroupName"] = value; }
        }
        public string UnSelectedLedgerId { get; set; }
        public string CostCentre
        {
            get
            {
                if (HttpContext.Current.Session["CostCentre"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["CostCentre"];
            }
            set { HttpContext.Current.Session["CostCentre"] = value; }
        }
        public string Narration
        {
            get
            {
                if (HttpContext.Current.Session["Narration"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["Narration"];
            }
            set { HttpContext.Current.Session["Narration"] = value; }
        }
        public Dictionary<string, object> DrillDownProperties
        {
            get
            {
                if (HttpContext.Current.Session["DrillDownProperties"] == null)
                    return null;
                else
                    return (Dictionary<string, object>)HttpContext.Current.Session["DrillDownProperties"];
            }
            set { HttpContext.Current.Session["DrillDownProperties"] = value; }
        }

        public string ReportCriteria
        {
            get
            {
                if (HttpContext.Current.Session["ReportCriteria"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ReportCriteria"];
            }
            set { HttpContext.Current.Session["ReportCriteria"] = value; }
        }

        public string ProjectTitle
        {
            get
            {
                if (HttpContext.Current.Session["ProjectTitle"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ProjectTitle"];
            }
            set { HttpContext.Current.Session["ProjectTitle"] = value; }
        }
        public string CostCentreName
        {
            get
            {
                if (HttpContext.Current.Session["CostCentreName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["CostCentreName"];
            }
            set { HttpContext.Current.Session["CostCentreName"] = value; }
        }
        public string BudgetName
        {
            get
            {
                if (HttpContext.Current.Session["BudgetName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BudgetName"];
            }
            set { HttpContext.Current.Session["BudgetName"] = value; }
        }

        public string BudgetProject
        {
            get
            {
                if (HttpContext.Current.Session["BudgetProject"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BudgetProject"];
            }
            set { HttpContext.Current.Session["BudgetProject"] = value; }
        }

        public string BudgetDateRangeInMonths
        {
            get
            {
                if (HttpContext.Current.Session["BudgetDateRangeInMonths"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BudgetDateRangeInMonths"];
            }

            set { HttpContext.Current.Session["BudgetDateRangeInMonths"] = value; }


        }

        public string VoucherType
        {
            get
            {
                if (HttpContext.Current.Session["VoucherType"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["VoucherType"];
            }
            set { HttpContext.Current.Session["VoucherType"] = value; }
        }

        public int TitleAlignment
        {
            get
            {
                if (HttpContext.Current.Session["TitleAlignment"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["TitleAlignment"];
            }
            set { HttpContext.Current.Session["TitleAlignment"] = value; }
        }

        public int Count
        {
            get
            {
                if (HttpContext.Current.Session["Count"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["Count"];
            }
            set { HttpContext.Current.Session["Count"] = value; }
        }
        public string BankAccountName
        {
            get
            {
                if (HttpContext.Current.Session["BankAccountName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BankAccountName"];
            }
            set { HttpContext.Current.Session["BankAccountName"] = value; }
        }
        //string _reportdate = "";
        public string ReportDate
        {
            get
            {
                if (HttpContext.Current.Session["ReportDate"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ReportDate"];
            }
            set
            {
                HttpContext.Current.Session["ReportDate"] = value;
                ShowPrintDate = ((HttpContext.Current.Session["ReportDate"].ToString() == "") ? 0 : 1);
            }

        }

        public int ShowLedgerCode
        {
            get
            {
                if (HttpContext.Current.Session["ShowLedgerCode"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowLedgerCode"];
            }
            set { HttpContext.Current.Session["ShowLedgerCode"] = value; }
        }
        public int ShowGroupCode
        {
            get
            {
                if (HttpContext.Current.Session["ShowGroupCode"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowGroupCode"];
            }
            set { HttpContext.Current.Session["ShowGroupCode"] = value; }
        }
        public int SortByLedger
        {
            get
            {
                if (HttpContext.Current.Session["SortByLedger"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["SortByLedger"];
            }
            set { HttpContext.Current.Session["SortByLedger"] = value; }
        }
        public int SortByGroup
        {
            get
            {
                if (HttpContext.Current.Session["SortByGroup"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["SortByGroup"];
            }
            set { HttpContext.Current.Session["SortByGroup"] = value; }
        }
        public int IncludeNarration
        {
            get
            {
                // return 0;
                if (HttpContext.Current.Session["IncludeNarration"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["IncludeNarration"];
            }
            set { HttpContext.Current.Session["IncludeNarration"] = value; }
        }

        //On 25/09/2020, Show Province From/To Contribution
        public int ShowInterAccountDetails
        {
            get
            {
                if (HttpContext.Current.Session["ShowInterAccountDetails"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowInterAccountDetails"];
            }
            set { HttpContext.Current.Session["ShowInterAccountDetails"] = value; }
        }

        //On 25/09/2020, Show Province From/To Contribution
        public int ShowProvinceFromToContributionDetails
        {
            get
            {
                if (HttpContext.Current.Session["ShowProvinceFromToContributionDetails"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowProvinceFromToContributionDetails"];
            }
            set { HttpContext.Current.Session["ShowProvinceFromToContributionDetails"] = value; }
        }


        // on 8/06/2023, Show All Cash Only
        public int ShowAllCash
        {
            get
            {
                if (HttpContext.Current.Session["ShowAllCashTrans"] == null)
                {
                    return 0;
                }
                else
                    return (int)HttpContext.Current.Session["ShowAllCashTrans"];
            }
            set { HttpContext.Current.Session["ShowAllCashTrans"] = value; }
        }

        public int LedgerSummary
        {
            get
            {
                if (HttpContext.Current.Session["LedgerSummary"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["LedgerSummary"];
            }
            set { HttpContext.Current.Session["LedgerSummary"] = value; }
        }

        public int ShowHorizontalLine
        {
            get
            {
                if (HttpContext.Current.Session["ShowHorizontalLine"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowHorizontalLine"];
            }
            set { HttpContext.Current.Session["ShowHorizontalLine"] = value; }
        }

        public int ConsolidateStateMent
        {
            get
            {
                if (HttpContext.Current.Session["ConsolidateStateMent"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ConsolidateStateMent"];
            }
            set { HttpContext.Current.Session["ConsolidateStateMent"] = value; }
        }

        public int ShowVerticalLine
        {
            get
            {
                if (HttpContext.Current.Session["ShowVerticalLine"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowVerticalLine"];
            }
            set { HttpContext.Current.Session["ShowVerticalLine"] = value; }
        }
        public int ShowTitles
        {
            get
            {
                if (HttpContext.Current.Session["ShowTitles"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowTitles"];
            }
            set { HttpContext.Current.Session["ShowTitles"] = value; }
        }
        public int ShowLogo
        {
            get
            {
                if (HttpContext.Current.Session["ShowLogo"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowLogo"];
            }
            set { HttpContext.Current.Session["ShowLogo"] = value; }
        }
        public int ShowPageNumber
        {
            get
            {
                if (HttpContext.Current.Session["ShowPageNumber"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowPageNumber"];
            }
            set { HttpContext.Current.Session["ShowPageNumber"] = value; }
        }
        public int ShowPrintDate
        {
            get
            {
                if (HttpContext.Current.Session["ShowPrintDate"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowPrintDate"];
            }
            set { HttpContext.Current.Session["ShowPrintDate"] = value; }
        }
        public string SetWithForCode
        {
            get
            {
                if (HttpContext.Current.Session["SetWithForCode"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["SetWithForCode"];
            }
            set { HttpContext.Current.Session["SetWithForCode"] = value; }
        }
        public int RecordCount
        {
            get
            {
                if (HttpContext.Current.Session["RecordCount"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["RecordCount"];
            }
            set { HttpContext.Current.Session["RecordCount"] = value; }
        }
        public int HeaderInstituteSocietyName
        {
            get
            {
                if (HttpContext.Current.Session["HeaderInstituteSocietyName"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["HeaderInstituteSocietyName"];
            }
            set { HttpContext.Current.Session["HeaderInstituteSocietyName"] = value; }
        }

        public int SelectedProjectCount
        {
            get
            {
                if (HttpContext.Current.Session["SelectedProjectCount"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["SelectedProjectCount"];
            }
            set { HttpContext.Current.Session["SelectedProjectCount"] = value; }
        }
        public DataTable CashBankVoucher
        {
            get
            {
                if (HttpContext.Current.Session["CashBankVoucher"] == null)
                    return null;
                else
                    return (DataTable)HttpContext.Current.Session["CashBankVoucher"];
            }
            set { HttpContext.Current.Session["CashBankVoucher"] = value; }
        }
        public string BranchOffice
        {
            get
            {
                if (HttpContext.Current.Session["BranchOffice"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BranchOffice"];
            }
            set { HttpContext.Current.Session["BranchOffice"] = value; }
        }

        public string BranchOfficeCode
        {
            get
            {
                if (HttpContext.Current.Session["BranchOfficeCode"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BranchOfficeCode"];
            }
            set { HttpContext.Current.Session["BranchOfficeCode"] = value; }
        }

        public string BranchOfficeName
        {
            get
            {
                if (HttpContext.Current.Session["BranchOfficeName"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["BranchOfficeName"];
            }
            set { HttpContext.Current.Session["BranchOfficeName"] = value; }
        }

        public string Society
        {
            get
            {
                if (HttpContext.Current.Session["Society"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["Society"];
            }
            set { HttpContext.Current.Session["Society"] = value; }
        }

        public string ProjectCategory
        {
            get
            {
                if (HttpContext.Current.Session["ProjectCategory"] == null)
                    return string.Empty;
                else
                    return (string)HttpContext.Current.Session["ProjectCategory"];
            }
            set { HttpContext.Current.Session["ProjectCategory"] = value; }
        }

        public static DataTable dtLedgerEntity
        {
            get
            {
                if (HttpContext.Current.Session["dtLedgerEntity"] == null)
                    return null;
                else
                    return (DataTable)HttpContext.Current.Session["dtLedgerEntity"];
            }
            set { HttpContext.Current.Session["dtLedgerEntity"] = value; }
        }

        public bool EnableDrillDown
        {
            get
            {
                if (HttpContext.Current.Session["EnableDrillDown"] == null)
                    return true;
                else
                    return (bool)HttpContext.Current.Session["EnableDrillDown"];
            }
            set { HttpContext.Current.Session["EnableDrillDown"] = value; }
        }
        public DataTable dtCBJ = null;
        public DataTable CashBankJouranlByVoucher
        {
            get { return dtCBJ; }
            set { dtCBJ = value; }
        }
        public int CashBankProjectId { get; set; }
        public string PrintCashBankVoucherId { get; set; }

        public DateTime CashBankVoucherDate { get; set; }

        public List<int> enumUserRights = new List<int>();




        private void LoadReportSetting()
        {
            try
            {
                //To set the current directory to avoid taking reportsetting.xml from recent path
                if (HttpContext.Current.Session["ReportProperty"] == null)
                {
                    Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    DataTable dtReportSettingInfo = new DataTable();
                    dtReportSettingInfo = dtReportSettingSchema.Copy();
                    dtReportSettingInfo.ReadXml(HttpContext.Current.Server.MapPath("~/bin/" + reportSettingFile));
                    dvReportSettingInfo = dtReportSettingInfo.DefaultView;
                    dvReportSettingInfo.Sort = dtReportSettingSchema.ReportGroupOrderColumn.ColumnName + "," + dtReportSettingSchema.ReportOrderColumn.ColumnName;
                    HttpContext.Current.Session["ReportProperty"] = dvReportSettingInfo;
                }
                else
                {
                    dvReportSettingInfo = (DataView)HttpContext.Current.Session["ReportProperty"];
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }

        private void SetReportSettingInfo()
        {
            if (dvReportSettingInfo != null)
            {
                dvReportSettingInfo.RowFilter = dtReportSettingSchema.ReportIdColumn.ColumnName + " = '" + ReportId + "'";

                if (dvReportSettingInfo.Count > 0)
                {
                    DataRowView drvReportSettingInfo = dvReportSettingInfo[0];
                    ReportGroup = drvReportSettingInfo[dtReportSettingSchema.ReportGroupColumn.ColumnName].ToString();
                    ReportName = drvReportSettingInfo[dtReportSettingSchema.ReportNameColumn.ColumnName].ToString();
                    ReportTitle = drvReportSettingInfo[dtReportSettingSchema.ReportTitleColumn.ColumnName].ToString();
                    ReportDescription = drvReportSettingInfo[dtReportSettingSchema.ReportDescriptionColumn.ColumnName].ToString();
                    ReportAssembly = drvReportSettingInfo[dtReportSettingSchema.ReportAssemblyColumn.ColumnName].ToString();
                    if (string.IsNullOrEmpty(Society))
                    {
                        Society = drvReportSettingInfo[dtReportSettingSchema.CustomerIdColumn.ColumnName].ToString();
                    }
                    if (string.IsNullOrEmpty(BranchOffice))
                    {
                        BranchOffice = drvReportSettingInfo[dtReportSettingSchema.BranchCodeColumn.ColumnName].ToString();
                    }
                    if (string.IsNullOrEmpty(DateFrom))
                    {
                        DateFrom = drvReportSettingInfo[dtReportSettingSchema.DateFromColumn.ColumnName].ToString();
                    }
                    if (string.IsNullOrEmpty(DateTo))
                    {
                        DateTo = drvReportSettingInfo[dtReportSettingSchema.DateToColumn.ColumnName].ToString();
                    }
                    DateAsOn = drvReportSettingInfo[dtReportSettingSchema.DateAsOnColumn.ColumnName].ToString();
                    IncludeAllLedger = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeAllLedgerColumn.ColumnName].ToString());
                    if (FDRegisterStatus == 0)
                    {
                        FDRegisterStatus = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.FDRegisterStatusColumn.ColumnName].ToString());
                    }
                    ShowByLedger = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByLedgerColumn.ColumnName].ToString());
                    ShowByCostCentre = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByCostCentreColumn.ColumnName].ToString());
                    ShowByCostCentreCategory = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByCostCentreCategoryColumn.ColumnName].ToString());
                    BreakUpByCostCentre = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.BreakUpByCostCentreColumn.ColumnName].ToString());
                    ShowByLedgerGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByLedgerGroupColumn.ColumnName].ToString());
                    ShowDailyBalance = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowDailyBalanceColumn.ColumnName].ToString());
                    IncludeJournal = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeJournalColumn.ColumnName].ToString());
                    IncludeInKind = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeInKindColumn.ColumnName].ToString());
                    IncludeBankDetails = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeBankDetailsColumn.ColumnName].ToString());
                    ShowDetailedBalance = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowBankDetailsColumn.ColumnName].ToString());
                    IncludeLedgerGroupTotal = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeLedgerGroupTotalColumn.ColumnName].ToString());
                    IncludeLedgerGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeLedgerGroupColumn.ColumnName].ToString());
                    IncludeCostCentre = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeCostCentreColumn.ColumnName].ToString());
                    ShowMonthTotal = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowMonthTotalColumn.ColumnName].ToString());
                    ShowDonorAddress = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowDonorAddressColumn.ColumnName].ToString());
                    ShowDonorCategory = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowDonorCategoryColumn.ColumnName].ToString());
                    IncludeBankAccount = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeBankAccountColumn.ColumnName].ToString());
                    UnSelectedBankAccountId = drvReportSettingInfo[dtReportSettingSchema.UnSelectedAccountIdColumn.ColumnName].ToString();
                    if (string.IsNullOrEmpty(Project)) { Project = drvReportSettingInfo[dtReportSettingSchema.ProjectColumn.ColumnName].ToString(); }
                    if (string.IsNullOrEmpty(Ledger)) { Ledger = drvReportSettingInfo[dtReportSettingSchema.LedgerColumn.ColumnName].ToString(); }
                    if (string.IsNullOrEmpty(LedgerGroup)) { LedgerGroup = drvReportSettingInfo[dtReportSettingSchema.LedgerGroupColumn.ColumnName].ToString(); }
                    if (LedgerGroup == "") { LedgerGroup = "0"; }
                    UnSelectedLedgerId = drvReportSettingInfo[dtReportSettingSchema.UnSelectedLedgerIdColumn.ColumnName].ToString();
                    if (UnSelectedLedgerId == "") { UnSelectedLedgerId = "0"; }
                    if (string.IsNullOrEmpty(CostCentre))
                    {
                        CostCentre = drvReportSettingInfo[dtReportSettingSchema.CostCentreColumn.ColumnName].ToString();
                    }
                    Narration = drvReportSettingInfo[dtReportSettingSchema.NarrationColumn.ColumnName].ToString();

                    TitleAlignment = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.TitleAlignmentColumn.ColumnName].ToString());
                    ConsolidateStateMent = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ConsolidateStatementColumn.ColumnName].ToString());
                    ReportDate = drvReportSettingInfo[dtReportSettingSchema.ReportDateColumn.ColumnName].ToString();

                    ReportCodeType = drvReportSettingInfo[dtReportSettingSchema.ReportCodeTypeColumn.ColumnName].ToString();

                    ShowLedgerCode = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByLedgerCodeColumn.ColumnName].ToString());
                    ShowGroupCode = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowByGroupCodeColumn.ColumnName].ToString());
                    SortByLedger = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.SortByLedgerColumn.ColumnName].ToString());
                    SortByGroup = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.SortByGroupColumn.ColumnName].ToString());
                    IncludeNarration = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.IncludeNarrationColumn.ColumnName].ToString());
                    LedgerSummary = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowLedgerSummaryColumn.ColumnName].ToString());
                    ShowHorizontalLine = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.HorizontalLineColumn.ColumnName].ToString());
                    ShowVerticalLine = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.VerticalLineColumn.ColumnName].ToString());
                    ShowTitles = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowTitlesColumn.ColumnName].ToString());
                    if (ShowLogo == 0)
                        ShowLogo = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowLogoColumn.ColumnName].ToString());
                    if (ShowPageNumber == 0) ShowPageNumber = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowPageNumberColumn.ColumnName].ToString());
                    ShowPrintDate = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowPrintDateColumn.ColumnName].ToString());
                    SetWithForCode = drvReportSettingInfo[dtReportSettingSchema.SetWidthForCodeColumn.ColumnName].ToString();
                    //Don't Update and GET DrillDownProperties, it is dynamic properties for all the reports, 
                    //it will be used when user cliks drill-down in all the reports 
                    //DrillDownProperties= this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.DrillDownPropertiesColumn.ColumnName].ToString());
                    ReportCriteria = drvReportSettingInfo[dtReportSettingSchema.ReportCriteriaColumn.ColumnName].ToString();
                    ShowProjectsinFooter = this.NumberSet.ToInteger(drvReportSettingInfo[dtReportSettingSchema.ShowProjectsinFooterColumn.ColumnName].ToString());
                }

                dvReportSettingInfo.RowFilter = "";
            }
        }

        public ResultArgs SaveReportSetting()
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;

            if (dvReportSettingInfo != null && dvReportSettingInfo.Count > 0)
            {
                DataTable dtReportSettingInfo = dvReportSettingInfo.Table;
                dvReportSettingInfo.RowFilter = dtReportSettingSchema.ReportIdColumn.ColumnName + " = '" + ReportId + "'";

                if (dvReportSettingInfo.Count > 0)
                {
                    DataRow drReportSettingInfo = dvReportSettingInfo[0].Row;
                    drReportSettingInfo[dtReportSettingSchema.DateFromColumn.ColumnName] = DateFrom;
                    drReportSettingInfo[dtReportSettingSchema.DateToColumn.ColumnName] = DateTo;
                    drReportSettingInfo[dtReportSettingSchema.DateAsOnColumn.ColumnName] = DateAsOn;
                    drReportSettingInfo[dtReportSettingSchema.IncludeAllLedgerColumn.ColumnName] = IncludeAllLedger;
                    drReportSettingInfo[dtReportSettingSchema.FDRegisterStatusColumn.ColumnName] = FDRegisterStatus;
                    drReportSettingInfo[dtReportSettingSchema.ShowByLedgerColumn.ColumnName] = ShowByLedger;
                    drReportSettingInfo[dtReportSettingSchema.ShowByCostCentreColumn.ColumnName] = ShowByCostCentre;
                    drReportSettingInfo[dtReportSettingSchema.ShowByCostCentreCategoryColumn.ColumnName] = ShowByCostCentreCategory;
                    drReportSettingInfo[dtReportSettingSchema.BreakUpByCostCentreColumn.ColumnName] = BreakUpByCostCentre;
                    drReportSettingInfo[dtReportSettingSchema.ShowByLedgerGroupColumn.ColumnName] = ShowByLedgerGroup;
                    drReportSettingInfo[dtReportSettingSchema.ShowDailyBalanceColumn.ColumnName] = ShowDailyBalance;
                    drReportSettingInfo[dtReportSettingSchema.IncludeJournalColumn.ColumnName] = IncludeJournal;
                    drReportSettingInfo[dtReportSettingSchema.IncludeInKindColumn.ColumnName] = IncludeInKind;
                    drReportSettingInfo[dtReportSettingSchema.IncludeLedgerGroupTotalColumn.ColumnName] = IncludeLedgerGroupTotal;
                    drReportSettingInfo[dtReportSettingSchema.IncludeLedgerGroupColumn.ColumnName] = IncludeLedgerGroup;
                    drReportSettingInfo[dtReportSettingSchema.IncludeCostCentreColumn.ColumnName] = IncludeCostCentre;
                    drReportSettingInfo[dtReportSettingSchema.ShowBankDetailsColumn.ColumnName] = ShowDetailedBalance;
                    drReportSettingInfo[dtReportSettingSchema.ShowMonthTotalColumn.ColumnName] = ShowMonthTotal;
                    drReportSettingInfo[dtReportSettingSchema.ShowDonorAddressColumn.ColumnName] = ShowDonorAddress;
                    drReportSettingInfo[dtReportSettingSchema.ShowDonorCategoryColumn.ColumnName] = ShowDonorCategory;
                    drReportSettingInfo[dtReportSettingSchema.IncludeBankAccountColumn.ColumnName] = IncludeBankAccount;
                    drReportSettingInfo[dtReportSettingSchema.IncludeBankDetailsColumn.ColumnName] = IncludeBankDetails;
                    drReportSettingInfo[dtReportSettingSchema.ProjectColumn.ColumnName] = Project;
                    //drReportSettingInfo[dtReportSettingSchema.ProjectTitleColumn.ColumnName] = ProjectTitle;
                    //drReportSettingInfo[dtReportSettingSchema.BankAccountColumn.ColumnName] = BankAccount;
                    drReportSettingInfo[dtReportSettingSchema.UnSelectedAccountIdColumn.ColumnName] = UnSelectedBankAccountId;
                    drReportSettingInfo[dtReportSettingSchema.LedgerColumn.ColumnName] = Ledger;
                    drReportSettingInfo[dtReportSettingSchema.LedgerGroupColumn.ColumnName] = LedgerGroup;
                    drReportSettingInfo[dtReportSettingSchema.UnSelectedLedgerIdColumn.ColumnName] = UnSelectedLedgerId;
                    drReportSettingInfo[dtReportSettingSchema.CostCentreColumn.ColumnName] = CostCentre;
                    drReportSettingInfo[dtReportSettingSchema.BUDGET_IDColumn.ColumnName] = Budget;
                    drReportSettingInfo[dtReportSettingSchema.NarrationColumn.ColumnName] = Narration;
                    //Don't Update and GET DrillDownProperties, it is dynamic properties for all the reports, 
                    //it will be used when user cliks drill-down in all the reports 
                    //drReportSettingInfo[dtReportSettingSchema.DrillDownPropertiesColumn.ColumnName] = DrillDownProperties;
                    drReportSettingInfo[dtReportSettingSchema.BranchCodeColumn.ColumnName] = BranchOffice;
                    drReportSettingInfo[dtReportSettingSchema.CustomerIdColumn.ColumnName] = Society;

                    drReportSettingInfo[dtReportSettingSchema.VoucherTypeColumn.ColumnName] = VoucherType;
                    drReportSettingInfo[dtReportSettingSchema.TitleAlignmentColumn.ColumnName] = TitleAlignment;
                    drReportSettingInfo[dtReportSettingSchema.ConsolidateStatementColumn.ColumnName] = ConsolidateStateMent;
                    drReportSettingInfo[dtReportSettingSchema.ReportDateColumn.ColumnName] = ReportDate;

                    drReportSettingInfo[dtReportSettingSchema.ReportCodeTypeColumn.ColumnName] = ReportCodeType;

                    drReportSettingInfo[dtReportSettingSchema.ShowByLedgerCodeColumn.ColumnName] = ShowLedgerCode;
                    drReportSettingInfo[dtReportSettingSchema.ShowByGroupCodeColumn.ColumnName] = ShowGroupCode;
                    drReportSettingInfo[dtReportSettingSchema.SortByLedgerColumn.ColumnName] = SortByLedger;
                    drReportSettingInfo[dtReportSettingSchema.SortByGroupColumn.ColumnName] = SortByGroup;
                    drReportSettingInfo[dtReportSettingSchema.IncludeNarrationColumn.ColumnName] = IncludeNarration;
                    drReportSettingInfo[dtReportSettingSchema.ShowLedgerSummaryColumn.ColumnName] = LedgerSummary;
                    drReportSettingInfo[dtReportSettingSchema.HorizontalLineColumn.ColumnName] = ShowHorizontalLine;
                    drReportSettingInfo[dtReportSettingSchema.VerticalLineColumn.ColumnName] = ShowVerticalLine;
                    drReportSettingInfo[dtReportSettingSchema.ShowTitlesColumn.ColumnName] = ShowTitles;
                    drReportSettingInfo[dtReportSettingSchema.ShowLogoColumn.ColumnName] = ShowLogo;
                    drReportSettingInfo[dtReportSettingSchema.ShowPageNumberColumn.ColumnName] = ShowPageNumber;
                    drReportSettingInfo[dtReportSettingSchema.ShowPrintDateColumn.ColumnName] = ShowPrintDate;
                    drReportSettingInfo[dtReportSettingSchema.SetWidthForCodeColumn.ColumnName] = SetWithForCode;
                    drReportSettingInfo[dtReportSettingSchema.ShowHeaderInstituteSocietyNameColumn.ColumnName] = HeaderInstituteSocietyName;
                    drReportSettingInfo[dtReportSettingSchema.ShowProjectsinFooterColumn.ColumnName] = ShowProjectsinFooter;
                    drReportSettingInfo.AcceptChanges();
                }
            }

            HttpContext.Current.Session["ReportProperty"] = dvReportSettingInfo;
            LoadReportSetting();
            SetReportSettingInfo();
            return result;
        }

        public DataView ReportSettingInfo
        {
            get { return dvReportSettingInfo; }
        }

        public void ShowMessageBox(string Msg)
        {
            XtraMessageBox.Show(Msg, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public string GetMessage(string keyCode)
        {
            ResourceManager resourceManger = new ResourceManager("ACPP.Resources.Messages.Messages", Assembly.GetExecutingAssembly());
            string msg = "";
            try
            {
                msg = resourceManger.GetString(keyCode);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage("Resoure File is not available", false);
            }
            return msg;
        }
        public int ShowProjectsinFooter
        {
            get
            {
                if (HttpContext.Current.Session["ShowProjectsinFooter"] == null)
                    return 0;
                else
                    return (int)HttpContext.Current.Session["ShowProjectsinFooter"];
            }
            set { HttpContext.Current.Session["ShowProjectsinFooter"] = value; }
        }

        public bool IsSDBRomeReports
        {
            get
            {
                bool rtn = (ReportId == "RPT-062" || ReportId == "RPT-063" || ReportId == "RPT-065" || ReportId == "RPT-068" ||
                            ReportId == "RPT-077" || ReportId == "RPT-078" || ReportId == "RPT-079" ||
                            ReportId == "RPT-170" || ReportId == "RPT-171" || ReportId == "RPT-172" || ReportId == "RPT-175" ||
                            ReportId == "RPT-176" || ReportId == "RPT-179"); //|| ReportId == "RPT-178"
                return rtn;
            }
        }

    }
}
