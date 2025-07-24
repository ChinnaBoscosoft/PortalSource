using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Data;
using System.Globalization;
using System.Threading;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using System.Collections;

using System.Diagnostics;
using System.Net;
using System.IO.Compression;
using System.Data.OleDb;
namespace Bosco.Utility
{
    public class CommonMethod : IDisposable
    {

        #region Delcaration
        bool _bSuccess = false;
        #endregion

        #region Methods
        /// <summary>
        /// This is to get the list of Currency symbol for the all the country
        /// </summary>
        /// <returns></returns>
        public DataTable GetCurrencySymbolList()
        {
            DataTable dtCurrencySymbol = new DataTable();
            dtCurrencySymbol.Columns.Add(new DataColumn("Currency Symbol", typeof(string)));
            dtCurrencySymbol.Columns.Add(new DataColumn("Currency Code", typeof(string)));
            dtCurrencySymbol.Columns.Add(new DataColumn("Country Code", typeof(string)));
            dtCurrencySymbol.Columns.Add(new DataColumn("Country", typeof(string)));
            dtCurrencySymbol.Columns.Add(new DataColumn("Currency Name", typeof(string)));

            try
            {
                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

                foreach (CultureInfo culture in cultures)
                {
                    var regionInfo = new RegionInfo(culture.Name);
                    dtCurrencySymbol.Rows.Add(regionInfo.CurrencySymbol, regionInfo.ISOCurrencySymbol, regionInfo.ThreeLetterISORegionName, regionInfo.DisplayName, regionInfo.CurrencyEnglishName);
                }
            }
            catch (Exception ex)
            {

            }

            return dtCurrencySymbol;
        }

        /// <summary>
        /// To get the line number of exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string GetExceptionline(Exception ex)
        {
            if (ex != null && ex.StackTrace != null)
            {
                return ex.StackTrace.Substring(ex.StackTrace.LastIndexOf(':') + 5);
            }
            else
            {
                return string.Empty;
            }
        }

        #region DateTime Functions

        public static DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }
        #endregion

        #region Excel

        public static string GetConnectionString(string FileName)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            dictionary["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            dictionary["Extended Properties"] = "Excel 12.0 XML";
            dictionary["Data Source"] = PagePath.AppFilePath + FileName + ".xlsx";

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in dictionary)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }
            if (File.Exists(dictionary["Data Source"].ToString()))
            {
                File.Delete(dictionary["Data Source"].ToString());
            }
            return sb.ToString();
        }

        public static void WriteExcelFile(DataTable dtExcelSource, string fileName)
        {
            try
            {
                string connectionString = GetConnectionString(fileName);
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    string sql = string.Empty;

                    string tableName = "CREATE TABLE [" + dtExcelSource.TableName + "] (";

                    string columName = string.Empty;
                    if (dtExcelSource.Columns.Count > 0)
                    {
                        for (int i = 0; i < dtExcelSource.Columns.Count; i++)
                        {
                            columName += String.Format("[{0}] {1},",
                                dtExcelSource.Columns[i].ColumnName.ToString(), (dtExcelSource.Columns[i].DataType.ToString() == "System.Int32" ? "VARCHAR" : "VARCHAR"));
                        }
                    }
                    cmd.CommandText = tableName + columName.TrimEnd(',') + ")";
                    cmd.ExecuteNonQuery();

                    sql = "Insert into [" + dtExcelSource.TableName + "] (";
                    string sqlcolumName = string.Empty;
                    if (dtExcelSource.Columns.Count > 0)
                    {
                        for (int i = 0; i < dtExcelSource.Columns.Count; i++)
                        {
                            sqlcolumName += String.Format("[{0}],", dtExcelSource.Columns[i].ColumnName.ToString());// + dtExcelSource.Columns[i].ColumnName.ToString() + "', ";
                        }
                    }
                    sql += sqlcolumName.TrimEnd(' ').TrimEnd(',') + ") values(";
                    foreach (DataRow item in dtExcelSource.Rows)
                    {
                        string sqlNamestring = string.Empty;
                        if (dtExcelSource.Columns.Count > 0)
                        {
                            for (int i = 0; i < dtExcelSource.Columns.Count; i++)
                            {
                                sqlNamestring += String.Format("\"{0}\",", item[i].ToString());
                            }
                        }
                        cmd.CommandText = sql + sqlNamestring.TrimEnd(',') + ")";
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Import Browse Page Data to excel::" + ex.Message);
            }
        }
        #endregion

        #region Generate Random Password
        /// <summary>
        /// This method is to generate Random Password which is the combination of Alphabets and Numeric of 10 characters
        /// </summary>
        /// <returns>string</returns>
        public static string GetRandomPassword()
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyz0123456989@*#$".ToCharArray();
            string password = string.Empty;
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(1, chars.Length);
                //Don't Allow repeation of Characters
                if (!password.Contains(chars.GetValue(x).ToString()))
                    password += chars.GetValue(x);
                else
                    i--;
            }
            return password;
        }
        #endregion

        #region Generate Random Security Code
        /// <summary>
        /// This method is to generate Random Security Code which is the combination of  Numeric of 0-9(4 digits)
        /// </summary>
        /// <returns>string</returns>
        public static int GetSecurityKey()
        {
            char[] number = "0123456989".ToCharArray();
            string securityCode = string.Empty;
            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                int x = random.Next(1, number.Length);
                //Don't Allow repeation of numbers
                if (!securityCode.Contains(number.GetValue(x).ToString()))
                    securityCode += number.GetValue(x);
                else
                    i--;
            }
            return Convert.ToInt16(securityCode);
        }
        public static string GetMailTemplate(string Header, string content, string name)
        {
            string mailContent = string.Empty;
            if (!(string.IsNullOrEmpty(Header) && string.IsNullOrEmpty(content)))
            {
                mailContent = @"<html><body><div style=""width: 90%; border: 1px solid #28688E; font-family: Verdana,Calibri;font-size: 12px;"">
                            <div style=""width: 100%; background-color: #C4DAF9; height: 60px;"">
                            <div style=""float: left;"">
                            <img src=""http://acmeerp.org/App_Themes/MainTheme/images/logo.png"" alt=""Acme.erp"" width=""100%"" height=""50""></div>
                            <div style=""float: right;""><img src=""http://acmeerp.org/App_Themes/MainTheme/images/Boscosoft.png"" alt=""BoscoSoft"" width=""100%"" height=""50""></div></div>";

                mailContent += @"<div style=""width: 100%; background-color: #ffffff;""><div style=""padding: 5px;""><p><b>Dear " + name + " , </b></p></div>";
                mailContent += @"<div style=""padding: 5px;"">Welcome to Acme.erp Portal.<br /><p>" + Header + "</p></div>";
                mailContent += @"<div style=""text-align:left;background-color:#F2F6FD;border-top:solid 1px #BDD0F2;border-bottom:solid 1px #BDD0F2; padding:  25px;"">" + content + "</div>";

                mailContent += @"<div style=""padding: 5px;""><p><b>Regards,</b><br /><br />Acme.erp Team</p></div>
                              <div><p>This email can't receive replies, please do not reply to this email. For more information,visit Acme.erp website</p></div></div>
                              <div style=""width: 100%; background-color: #f1f1f1;""><div style=""text-align: center; font-weight: bold;"">You can reach us at</div><div style=""padding: 5px;"">
                              <div>&nbsp;&nbsp;Email:acmeerp@boscosofttech.com</div><div>&nbsp;&nbsp;https://www.acmeerp.org</div>
                              <div>&nbsp;&nbsp;</div></div></div></div></body></html>";

            }
            return mailContent;
        }
        public static string GetMailTemplate(string Header, string content, string name, bool IsBranchOffice)
        {
            string mailContent = string.Empty;
            if (!(string.IsNullOrEmpty(Header) && string.IsNullOrEmpty(content)))
            {
                mailContent = @"<html><body><div style=""width: 90%; border: 1px solid #28688E; font-family: Verdana,Calibri;font-size: 12px;"">
                            <div style=""width: 100%; background-color: #C4DAF9; height: 60px;"">
                            <div style=""float: left;"">
                            <img src=""https://acmeerp.org/App_Themes/MainTheme/images/logo.png"" alt=""Acme.erp"" width=""100%"" height=""50""></div>
                            <div style=""float: right;""><img src=""https://acmeerp.org/App_Themes/MainTheme/images/Boscosoft.png"" alt=""BoscoSoft"" width=""100%"" height=""50""></div></div>";

                mailContent += @"<div style=""width: 100%; background-color: #ffffff;""><div style=""padding: 5px;""><p><b>Dear " + name + " , </b></p></div>";
                mailContent += @"<div style=""padding: 5px;"">Welcome to Acme.erp Portal.<br /><p>" + Header + "</p></div>";
                mailContent += @"<div style=""text-align:left;background-color:#F2F6FD;border-top:solid 1px #BDD0F2;border-bottom:solid 1px #BDD0F2; padding:  25px;"">" + content + "</div>";

                mailContent += @"<div style=""padding: 5px;""><p><b>Regards,</b><br /><br />Acme.erp Team</p></div>
                              <div><p>This email can't receive replies, please do not reply to this email. For more information, 
                               contact your Head Office Admin. </p></div></div>
                              <div style=""width: 100%; background-color: #f1f1f1;""><div style=""text-align: center; font-weight: bold;"">You can reach us at</div><div style=""padding: 5px;"">
                              <div>&nbsp;&nbsp;Email:acmeerp@boscosofttech.com</div><div>&nbsp;&nbsp;https://www.acmeerp.org</div>
                              <div>&nbsp;&nbsp;</div></div></div></div></body></html>";

            }
            return mailContent;
        }
        #endregion

        #region Get First Value in Comma Separated String
        /// <summary>
        /// This method is to get first value from the comma separated string
        /// </summary>
        public static string GetFirstValue(string commaSeparatedString)
        {
            string returnString = commaSeparatedString;
            if ((!string.IsNullOrEmpty(commaSeparatedString) && commaSeparatedString.Contains(Delimiter.Comma)))
            {
                returnString = commaSeparatedString.Substring(0, commaSeparatedString.IndexOf(Delimiter.Comma));
            }
            return returnString;
        }
        #endregion

        #region Remove First Value in Comma Separated String
        /// <summary>
        /// This method is to get first value from the comma separated string
        /// </summary>
        public static string RemoveFirstValue(string commaSeparatedString)
        {
            string returnString = commaSeparatedString;
            if ((!string.IsNullOrEmpty(commaSeparatedString) && commaSeparatedString.Contains(Delimiter.Comma)))
            {
                returnString = commaSeparatedString.Remove(0, commaSeparatedString.IndexOf(Delimiter.Comma) + 1);
            }
            else
            {
                returnString = "";
            }
            return string.IsNullOrEmpty(returnString) ? "" : returnString;
        }
        #endregion
        #endregion

        #region FTP Upload and Download

        /// <summary>
        /// Checks connection to specified URL. 
        /// </summary>
        /// <param name="URL"></param>
        /// <returns><b>True</b> if there is connection. Else <b>false</b>.</returns>
        public static bool CheckConnection(string sDomainNameToPing)
        {
            bool _bSuccess = false;
            try
            {
                HttpWebRequest request = WebRequest.Create(sDomainNameToPing) as HttpWebRequest;
                request.Timeout = 15000;//15 sec?
                request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                new ErrorLog().WriteError("Internet connection is active");
                _bSuccess = response.StatusCode == HttpStatusCode.OK ? true : false;
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Origin of the error:CheckConnection() ", "FTP Error", ex.Message, "0");
            }
            return _bSuccess;
        }

        /// <summary>
        /// This method is to get the file Availability, file status.
        /// </summary>
        /// <returns></returns>
        public bool GetFileStatus(bool bSize, string sFTPURL, string sFtpAddress, string FileName, string sFtpUsername, string sFtpPassword)
        {
            _bSuccess = false;
            string sFileStatus = string.Empty;

            var request = (FtpWebRequest)WebRequest.Create(sFTPURL + sFtpAddress + FileName);
            request.Credentials = new NetworkCredential(sFtpUsername, sFtpPassword);
            if (bSize)
                request.Method = WebRequestMethods.Ftp.GetFileSize;
            else
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

            request.Proxy = new WebProxy();
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //if (bSize)
                //    //Common.ftpZipFileSize = response.ContentLength;
                //else
                //    //Common.ftpFileUploadedTime = response.LastModified;

                new ErrorLog().WriteError("File is found on the server");
                response.Close();
                _bSuccess = true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                sFileStatus = response.StatusCode.ToString();
                new ErrorLog().WriteError("Origin of the error:GetFileStatus() ", "FTP Error", ex.Message, "0");
            }
            return _bSuccess;
        }

        /// <summary>
        /// This method is to download the FTP Files 
        /// </summary>
        public static bool DownloadFTPFiles(string sFtpUsername, string sFtpPassword, string sFtpAddress, string FileToDownload)
        {
            bool _bSuccess = false;
            try
            {
                new ErrorLog().WriteError("File download is started");
                string LocalDirectory = @"D:\\FilePuller";
                FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(sFtpAddress + "/" + FileToDownload);
                requestFileDownload.Credentials = new NetworkCredential(sFtpUsername, sFtpPassword);
                requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                Stream responseStream = responseFileDownload.GetResponseStream();
                if (!Directory.Exists(LocalDirectory))
                {
                    Directory.CreateDirectory(LocalDirectory);
                }
                FileStream writeStream = new FileStream(LocalDirectory + "/" + FileToDownload, FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);

                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                responseStream.Close();
                writeStream.Close();
                requestFileDownload = null;
                _bSuccess = true;
                new ErrorLog().WriteError("File download is completed");
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError("Origin of the error:DownloadFTPFiles() ", "FTP Error", ex.Message, "0");
            }

            return _bSuccess;
        }

        private static FtpWebRequest FTPDetail(string FileName)
        {
            string uri = "";
            string serverIp = "66.85.163.170"; //Ftp server IP address
            string Username = "acmeerp"; // Ftp User name
            string Password = "Rt6yx00!"; // Ftp Password
            uri = "ftp://" + serverIp + "/Uploads/" + FileName;

            FtpWebRequest objFTP;
            objFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            objFTP.Credentials = new NetworkCredential(Username, Password);
            objFTP.UsePassive = true;
            objFTP.KeepAlive = false;
            objFTP.Proxy = null;
            objFTP.UseBinary = false;
            objFTP.Timeout = 90000;
            return objFTP;
        }

        public static bool UploadFile(Stream inputStream, string fileName)
        {

            string uri = "";
            string serverIp = "66.85.163.170"; //Ftp server IP address
            string Username = "acmeerp"; // Ftp User name
            string Password = "Rt6yx00!"; // Ftp Password
            uri = "ftp://" + serverIp + "/Uploads/" + fileName;

            FtpWebRequest rq = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            rq.Credentials = new NetworkCredential(Username, Password);
            rq.Method = WebRequestMethods.Ftp.UploadFile;
            Stream fs = inputStream;
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            System.IO.Stream ftpstream = rq.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();
            return true;
        }

        #endregion

        #region Compress Dataset
        /// <summary>
        /// This method compress Dataset to byte data.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static byte[] CompressData(DataSet ds)
        {
            byte[] data;
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    GZipStream zipStream = new GZipStream(memStream, CompressionMode.Compress);
                    ds.WriteXml(zipStream, XmlWriteMode.WriteSchema);
                    zipStream.Close();
                    data = memStream.ToArray();
                    memStream.Close();
                    new ErrorLog().WriteError("DataSet Compress Success.....");
                }
            }
            catch (Exception ex)
            {
                data = null;
                new ErrorLog().WriteError("Dataset Compress Error::" + ex.Message);
            }

            return data;
        }

        /// <summary>
        /// This Method Decompress byte to Dataset
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataSet DecompressData(byte[] data)
        {
            DataSet ds = new DataSet();
            try
            {
                using (MemoryStream memStream = new MemoryStream(data))
                {
                    GZipStream unzipStream = new GZipStream(memStream, CompressionMode.Decompress);

                    ds.ReadXml(unzipStream, XmlReadMode.ReadSchema);
                    unzipStream.Close();
                    memStream.Close();
                    new ErrorLog().WriteError("DataSet Decompress Success.....");
                }
            }
            catch (Exception ex)
            {
                ds = null;
                new ErrorLog().WriteError("Dataset Decompress Error::" + ex.Message);
            }
            return ds;
        }

        public static string GetFileSize(FileInfo f)
        {
            string[] fileUnits = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int fuCount = 0;
            long size = f.Length;

            while (size >= 1024)
            {
                fuCount++;
                size /= 1024;
            }

            return string.Format("{0} {1}", size, fileUnits[fuCount]);
        }
        #endregion

        void IDisposable.Dispose()
        {
            GC.Collect();
        }
    }
}
