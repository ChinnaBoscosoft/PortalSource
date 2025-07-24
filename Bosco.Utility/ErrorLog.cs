/*
 * File         :ErrorLog.cs
 * Purpose      :To have all the Errors Log
 * Created On   :03.06.2014
 * Reviewed On  :
 * Reviewed By  :
  */
using System;
using System.IO;

namespace Bosco.Utility
{
    /// <summary>
    /// Summary description for ErrorLog
    /// </summary>
    public class ErrorLog
    {

        #region Constructor
        public ErrorLog()
        {

        }
        #endregion

        /// <summary>
        /// This method is to write the Mobile Service Log for acme.erp portal
        /// </summary>
        /// <param name="sMessage"></param>
        private static void WriteMobileServiceLog(string sMessage)
        {
            try
            {
                StreamWriter sw = File.AppendText((PagePath.MobileServiceLogFilePath));
                sMessage = ("::" + sMessage);
                sw.WriteLine(sMessage);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void WriteMobileServiceError(string errMessage)
        {
            WriteMobileServiceLog("-----------------------------------------------------------------------------------------");
            WriteMobileServiceLog("Date      :" + DateTime.Now.ToShortDateString());
            WriteMobileServiceLog("Time      :" + DateTime.Now.ToShortTimeString());
            WriteMobileServiceLog("ErrorMessage :" + errMessage);
            WriteMobileServiceLog("-----------------------------------------------------------------------------------------");
        }
        /// <summary>
        /// This method is to write the passed error message and other details of error occurence
        /// </summary>
        /// <param name="sMessage">string - Error message</param>
        private static void WriteLog(string sMessage)
        {
            try
            {
                StreamWriter sw = File.AppendText((PagePath.ErrorLogFilePath));
                sMessage = ("::" + sMessage);
                sw.WriteLine(sMessage);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method used to write the exception passed an an arugement to targetted text file
        /// in a new line
        /// </summary>
        /// <param name="ex"></param>
        public void WriteError(string errMessage)
        {
            WriteLog("-----------------------------------------------------------------------------------------");
            WriteLog("Date      :" + DateTime.Now.ToShortDateString());
            WriteLog("Time      :" + DateTime.Now.ToShortTimeString());
            WriteLog("ErrorMessage :" + errMessage);
            WriteLog("-----------------------------------------------------------------------------------------");
        }
        public void WriteError(string unitname, string pagename, string errMessage, string Errorlineno)
        {
            WriteLog("-----------------------------------------------------------------------------------------");
            WriteLog("Date      :" + DateTime.Now.ToShortDateString());
            WriteLog("Time      :" + DateTime.Now.ToShortTimeString());
            WriteLog("ClassName :" + unitname);
            WriteLog("MethodName :" + pagename);
            WriteLog("ErrorMessage :" + errMessage);
            WriteLog("Line Number  :" + Errorlineno);
            WriteLog("-----------------------------------------------------------------------------------------");
        }
        public void WriteError(string unitname, string pagename, string errMessage,string queryname, string Errorlineno)
        {
            WriteLog("-----------------------------------------------------------------------------------------");
            WriteLog("Date      :" + DateTime.Now.ToShortDateString());
            WriteLog("Time      :" + DateTime.Now.ToShortTimeString());
            WriteLog("ClassName :" + unitname);
            WriteLog("MethodName :" + pagename);
            WriteLog("ErrorMessage :" + errMessage);
            WriteLog("Query :" + queryname);
            WriteLog("Line Number  :" + Errorlineno);
            WriteLog("-----------------------------------------------------------------------------------------");
        }
    }

}