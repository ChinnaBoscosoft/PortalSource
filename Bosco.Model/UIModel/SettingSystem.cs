using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Configuration;

using Bosco.DAO.Schema;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Text.RegularExpressions;

namespace Bosco.Model
{
    public class SettingSystem : SystemBase
    {
        #region Declaration
        ResultArgs resultArgs = null;
        #endregion

        public SettingSystem()
        {
        }

        public ResultArgs SaveSettingDetails(DataTable dvSetting)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.InsertUpdate))
            {
                for (int i = 0; i < dvSetting.Rows.Count; i++)
                {
                    dataManager.Parameters.Add(this.AppSchema.Settings.SETTING_NAMEColumn, dvSetting.Rows[i][1]);
                    dataManager.Parameters.Add(this.AppSchema.Settings.VALUEColumn, dvSetting.Rows[i][2]);

                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                    {
                        //Keep the setting Info into session
                        this.SettingInfo = dvSetting.DefaultView;
                    }
                    else
                    {
                        this.SettingInfo = null;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs FetchSettingDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Setting.Fetch))
            {
                resultArgs = dataManager.FetchData(DataSource.DataView);
            }
            return resultArgs;
        }

        public string GetHeadOfficeDBConnection(string headOfficeCode)
        {
            string hoConnectionString = "";

            using (DataManager dataManager = new DataManager(SQLCommand.HeadOffice.FetchDatabase))
            {
                dataManager.Parameters.Add(this.AppSchema.HeadOffice.HEAD_OFFICE_CODEColumn, headOfficeCode);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                DataTable dtHOConnection = resultArgs.DataSource.Table;

                if (resultArgs.Success && dtHOConnection != null && dtHOConnection.Rows.Count == 1)
                {
                    hoConnectionString = dtHOConnection.Rows[0][this.AppSchema.HeadOffice.DB_CONNECTIONColumn.ColumnName].ToString();
                }
            }

            return hoConnectionString;
        }

        public ResultArgs SendEmail(string toEmailId, string subject, string message)
        {
            return SendEmail(toEmailId, "", subject, message);
        }

        public ResultArgs SendEmail(string toEmailId, string toCCEmailId, string subject, string message)
        {
            //string fromEmailId = this.LoginUserEmailId;
            string fromEmailId = string.Empty;
            if (string.IsNullOrEmpty(fromEmailId)) { fromEmailId = "Acme.erp <" + ConfigurationManager.AppSettings["DefaultSenderEmailId"] + ">"; }
            return SendEmail(fromEmailId, toEmailId, toCCEmailId, subject, message, "");
        }

        public ResultArgs SendEmail(string fromEmailId, string toEmailId, string subject, string message, string attachmentFile)
        {
            return SendEmail(fromEmailId, toEmailId, "", subject, message, attachmentFile);
        }

        public ResultArgs SendEmail(string fromEmailId, string toEmailId, string toCCEmailId,
            string subject, string message, string attachmentFile)
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = true;

            string mailFrom = fromEmailId;
            string mailTo = toEmailId;
            string mailToCC = toCCEmailId;

            if (string.IsNullOrEmpty(subject)) { subject = "Mail from AcMEERP"; }

            if (!string.IsNullOrEmpty(mailFrom) && !string.IsNullOrEmpty(mailTo))
            {
                try
                {
                    MailMessage mail = new MailMessage();

                    mail.Headers.Add("MIME-Version", "1.0");
                    mail.Headers.Add("Content-Type", "text/html; charset=iso-8859-1");
                    mail.Headers.Add("Content-Type", "image/png; name=logo.png");
                    mail.Headers.Add("Content-Type", "image/png; name=Picture1.png");
                    mail.Headers.Add("Mailed-by", "acmeerp.org");
                    mail.Headers.Add("Signed-by", "acmeerp.org");

                   
                    mail.From = new MailAddress(mailFrom);
                    mail.ReplyTo = new MailAddress("Acme.erp <support@acmeerp.org>");
                    mail.To.Add(new MailAddress(mailTo));
                    mail.Subject = subject;
                    mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    mail.Priority = MailPriority.High;

                    AlternateView plainView = AlternateView.CreateAlternateViewFromString
                    (Regex.Replace(message, @"<(.|\n)*?>", string.Empty), null, "text/plain");
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(message, null, "text/html");
                    mail.AlternateViews.Add(plainView);
                    mail.AlternateViews.Add(htmlView);

                  
                    if (toCCEmailId != "")
                    {
                        mail.CC.Add(toCCEmailId);
                    }

                    if (attachmentFile != "")
                    {
                        Attachment attachment = new Attachment(attachmentFile);
                        mail.Attachments.Add(attachment);
                    }

                    mail.IsBodyHtml = true;

                    //Send the message.
                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Timeout = 30000;
                    smtpClient.Send(mail);
                }
                catch (Exception err)
                {
                    resultArgs.Message = err.Message;
                    new ErrorLog().WriteError("SettingSystem", "SendEmail", err.Message, new CommonMethod().GetExceptionline(err));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(mailFrom))
                {
                    resultArgs.Message = "Sender Email Id not found";
                }
                else
                {
                    resultArgs.Message = "Receiver Email Id not found";
                }
            }

            return resultArgs;
        }

        public ResultArgs SendSMS(string mobileNo, string message)
        {
            //string smsDomainName = ConfigurationManager.AppSettings["SMSDomainName"];
            //string mailTo = mobileNo + "@" + smsDomainName;
            ResultArgs resultArgs = new ResultArgs();

            if (!string.IsNullOrEmpty(mobileNo))
            {
                resultArgs = ConnectSMSWebService(mobileNo, message);
            }
            else
            {
                resultArgs.Message = "Receiver Mobile Number not found";
            }

            return resultArgs;
        }

        private ResultArgs ConnectSMSWebService(string mobileNo, string smsText)
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = true;

            //string sWebServiceURL = "vmsrefsms521/web_services/SmsEasy.asmx";
            string ULN = ConfigurationManager.AppSettings["SMS_ULN"];
            string GID = ConfigurationManager.AppSettings["SMS_GID"];
            string mobile_no = "+91" + mobileNo; //Country code should be read from Global Setting
            StringBuilder sbResponse = new StringBuilder();

            try
            {
                StringBuilder sbParameterXML = new StringBuilder(string.Empty);
                StringBuilder sbRequestXML = new StringBuilder(string.Empty);
                string url = "http://vmsrefsms521/web_services/SmsEasy.asmx?op=SendSMS";

                //Input Parameters
                sbParameterXML.Append("<SendSMS xmlns=\"http://smseasy.net/\">");
                sbParameterXML.Append("<ULN>" + ULN + "</ULN>");
                sbParameterXML.Append("<GID>" + GID + "</GID>");
                sbParameterXML.Append("<numbers_list>" + mobile_no + "</numbers_list>");
                sbParameterXML.Append("<message_text>" + smsText + "</message_text>");
                sbParameterXML.Append("</SendSMS>");

                //Generate request soal xml along with input parameters
                sbRequestXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sbRequestXML.Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">");
                sbRequestXML.Append("<soap:Body>");
                sbRequestXML.Append(sbParameterXML.ToString());
                sbRequestXML.Append("</soap:Body>");
                sbRequestXML.Append("</soap:Envelope>");

                Byte[] byteArray = Encoding.UTF8.GetBytes(sbRequestXML.ToString());
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.ContentLength = byteArray.Length;
                Stream webStream = webRequest.GetRequestStream();
                webStream.Write(byteArray, 0, byteArray.Length);
                webStream.Close();

                WebResponse webResponse = webRequest.GetResponse();
                webStream = webResponse.GetResponseStream();

                using (StreamReader reader = new StreamReader(webStream, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sbResponse.Append(line);
                    }
                }

                webStream.Close();
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.Message;
                new ErrorLog().WriteError("SettingSystem", "ConnectSMSWebService", ex.Message, new CommonMethod().GetExceptionline(ex));
            }

            return resultArgs;
        }
    }
}
