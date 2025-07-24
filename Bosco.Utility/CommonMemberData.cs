/*  Class Name      : CommonMemberData.cs
 *  Purpose         : Global member data
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Web;
using System.IO;
using System.ServiceProcess;
using System.Reflection;
using Microsoft.Win32;

namespace Bosco.Utility
{
    
    public abstract class Delimiter
    {
        public const string PipeLine = "|";
        public const string AtCap = "@";
        public const string Comma = ",";
        public const string Mew = "µ";
        public const string ECap = "ê";
        public const string NewLine = "lt;br/gt;";
        public const string NewLineTag = "<br/>";
        public const string Hyphen = "-";
        public const string forwardSlash = "/";
    }

    public abstract class SessionMemeber
    {
        public const string Id = "id";
        public const string UserInfo = "user_info";
    }

    public abstract class QueryStringMemeber
    {
        public const string ReturnUrl = "ru";
        public const string Id = "id";
        public const string Id1 = "id1";
        public const string Id2 = "id2";
        public const string Logout = "out";
        public const string Message = "msg";
        public const string ReportId = "rid";
        public const string DateFrom = "df";
        public const string DateTo = "dt";
        public const string LedgerId = "lgi";
        public const string GroupId = "gri";
        public const string ProjectId = "pri";
    }

    public abstract class DefaultItem
    {
        public const string Empty = "";
        public const string All = "All";
        public const string AllWithLine = "-------All-------";
        public const string AllWithLineArrow = "<-------All------->";
    }

    public abstract class Currency
    {
        public static string Name = "INR";
        public static string Symbol = "Rs.";
        public static string Format = "en-GB";
    }

    public abstract class NumberFormatInfo
    {
        public const string Currency = "C";
        public const string General = "G";
        public const string Number = "D";
        public const string Amount = "#########.00";
    }

    public abstract class DateFormatInfo
    {
        private static string dateCulture = "en-GB";
        private static string dateFormat = "dd/MM/yyyy";

        public static string DateCulture
        {
            set { dateCulture = value; }
            get { return dateCulture; }
        }

        public static string DateFormat
        {
            set { dateFormat = value; }
            get { return dateFormat; }
        }

        public static string DateAndTimeFormat
        {
            get { return DateFormat + " " + TimeFormat; }
        }

        public static string DateAndTimeFormatLong
        {
            get { return DateFormat + " " + TimeFormatLong; }
        }

        public static string DateAndTimeFormatShort
        {
            get { return DateFormat + " " + TimeFormat; }
        }

        public static string DateAndTime12Format
        {
            get { return DateFormat + " " + Time12Format; }
        }

        public static string DateFormatMYD
        {
            get { return "MM/yyyy/dd"; }
        }

        public static string DateFormatYMD
        {
            get { return "yyyy/MM/dd"; }
        }

        public static string TimeFormat
        {
            get { return "HH:mm:ss"; }
        }

        public static string TimeFormatLong
        {
            get { return "HH:mm:ss:fff"; }
        }

        public static string Time12Format
        {
            get { return "hh:mm:ss tt"; }
        }

        public static string Time12FormatHM
        {
            get { return "hh:mm tt"; }
        }

        public static string TimeFormatHour
        {
            get { return "HH"; }
        }

        public static string Time12FormatHour
        {
            get { return "hh"; }
        }

        public static string TimeFormatMinute
        {
            get { return "mm"; }
        }

        public static string TimeFormatSecond
        {
            get { return "ss"; }
        }

        public static string TimeFormatAMPM
        {
            get { return "tt"; }
        }

        public static string StartTime
        {
            get { return " 00:00:00"; }
        }

        public static string EndTime
        {
            get { return " 23:59:59"; }
        }

        public static string Start12Time
        {
            get { return " 00:00:00"; }
        }

        public static string End12Time
        {
            get { return " 11:59:59 PM"; }
        }

        public class MySQLFormat
        {
            //date format for SQL insert/update
            public const string DateAdd = "yyyy/MM/dd";
            public const string DateTimeAdd = "yyyy/MM/dd HH:mm:ss";

            //date format for SQL select statement
            public static string DateFormat = "yyyy/MM/dd";
            public static string DateFormat2 = "yyyy-MM-dd";
            public static string DateTimeLong = "yyyyMMdd_HHmmss";
            public static string DateTime = "ddMMyyyy";
            public const string DateAndTimeNoformatBegin = "yyyyMMdd" + "000000";
            public const string DateAndTimeNoformatEnd = "yyyyMMdd" + "235959";

            public static string DateAndTime
            {
                get { return DateFormat2 + " HH:mm:ss"; }
            }
        }
    }

    public class ApplicationTitle
    {
        public const string Name = "Acme.erp Portal";
    }
    public class PortalTitle
    {
        public const string PortalName = "ERP for Religious & NGOs";
    }
    public class AppFile
    {
        public const string MasterkeyName = "master.xml";
        public const string LicenseKeyName = "licensekey.xml";
        //dll Conversion
        //public const string LicenseKeyName = "Acme.erpLicense.acp";
    }
    public class PagePath
    {
        public static string ApplicationVirtualPath
        {
            get
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                       HttpContext.Current.Request.ApplicationPath;
            }
        }

        public static string ApplicationPhysicalPath
        {
            get { return System.Web.HttpContext.Current.Request.PhysicalApplicationPath; }
        }

        public static string UIStartupPath
        {
            get { return PagePath.ApplicationVirtualPath.EndsWith(Delimiter.forwardSlash.ToString()) ? 
                PagePath.ApplicationVirtualPath : (PagePath.ApplicationVirtualPath +Delimiter.forwardSlash.ToString()); }
        }

        public static string AppFilePath
        {
            get { return PagePath.ApplicationPhysicalPath + @"AppFile\"; }
        }
        public static string ReportPath
        {
            //get { return PagePath.UIStartupPath + @"Report/PortalReportCriteria.aspx"; }
            get { return PagePath.UIStartupPath + @"Report/ReportViewer.aspx"; }
        }

        public static string VoucherViewPath
        {
            get { return PagePath.UIStartupPath + @"Report/VoucherDetailView.aspx"; }
        }

        public static string MasterSettingFileName
        {
            get { return PagePath.AppFilePath + "master.xml"; }
        }

        public static string MultiMasterSettingFileName
        {
            get { return PagePath.AppFilePath + @"Masters\"; }
        }

        public static string LicenseKeyFileName
        {
            get { return PagePath.AppFilePath + "licensekey.xml"; }
        }

        public static string MultilicensekeySettingFileName
        {
            get { return PagePath.AppFilePath + @"Licenses\"; }
        }
        public static string AcMEERPMastersFilePath
        {
            get { return PagePath.AppFilePath + "AcMEERP_MastersTemplate.xlsx"; }
        }
        public static string AcMEERPAssetMastersFilePath
        {
            get { return PagePath.AppFilePath + "AcMEERP_Asset_Masters_Template.xlsx"; }
        }
        public static string AcMEERPBranchTemplateFilePath
        {
            get { return PagePath.AppFilePath + "BranchOfficeTemplate.xlsx"; }
        }
        public static string AcMEERPLatestVersionExePath
        {
           get { return PagePath.ApplicationPhysicalPath + @"Module\Software\Uploads\AcMEERPUpdater.exe"; }
        }
        public static string ErrorLogFilePath
        {
            get
            {
                //string path = "E:\\AcMEERP_Log";
                string path = PagePath.AppFilePath + "AcMEERP_Log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += "\\PortalLog.txt";
                return path;
            }
        }
        public static string MobileServiceLogFilePath
        {
            get
            {
                //string path = "E:\\AcMEERP_Log";
                string path = PagePath.AppFilePath + "AcMEERP_Log";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += "\\MobileServiceLog.txt";
                return path;
            }
        }

        public static string GetAcmeDSyncServicePath()
        {
            string servicePath = string.Empty;
            try
            {
                 ServiceController[] scServices;
                scServices = ServiceController.GetServices();

                foreach (ServiceController scAcMEDS in scServices)
                {
                    if (scAcMEDS.ServiceName.Equals("AcMEDS"))
                    {
                        using (RegistryKey regKey1 = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\" + scAcMEDS.ServiceName))
                        {
                            if (regKey1.GetValue("ImagePath") != null)
                                servicePath = regKey1.GetValue("ImagePath").ToString();
                        }
                        if (servicePath.IndexOf("AcMEDS.exe") > 0)
                            servicePath = servicePath.Substring(0, servicePath.IndexOf("AcMEDS.exe") - 1);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally { }
            return servicePath;
        }
    }

    public class LicenseUtility
    {
        public const string BranchKeyUniqueCode = "YYYYMM";
        public const int LicenseRunningNoLength = 4;
        public const string LicenseIdentificationNumberFormat = "YYYYMMDD";
        public const int LicenseIdentificationRunningNoLength = 6;
    }
}
