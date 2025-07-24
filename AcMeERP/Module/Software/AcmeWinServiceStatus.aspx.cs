using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceProcess;
using Bosco.Utility;
using System.IO;

namespace AcMeERP.Module.Software
{
    public partial class AcmeWinServiceStatus : Base.UIBase
    {
        #region Constant Values
        private const string AcMEDS = "AcMEDS";
        private const string STARTSERVICE = "Service is running...";
        private const string STOPSERVICE = "Service is not running...";
        public const int ACMEDS_SERVICE_TIMEOUT = 20000;
        private string AcmeDSynLog = string.Empty;
        #endregion

        #region Service Methods

        /// <summary>
        /// Start or Stop AcMEDS Service.
        /// </summary>
        /// <returns></returns>
        private void ProcessAcMEDSService()
        {
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(ACMEDS_SERVICE_TIMEOUT);
                ServiceController[] scServices;
                scServices = ServiceController.GetServices();

                foreach (ServiceController scAcMEDS in scServices)
                {
                    if (scAcMEDS.ServiceName.Equals(AcMEDS))
                    {
                        ServiceController scService = new ServiceController(AcMEDS);
                        
                        if (scService.Status == ServiceControllerStatus.Running || scService.Status == ServiceControllerStatus.Paused)
                        {
                            scService.Stop();
                            scService.Refresh();
                            scService.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                            btnServiceStatus.Text = "Start";
                            lblSerivceType.Text = STOPSERVICE;
                        }
                        else if (scService.Status == ServiceControllerStatus.Stopped)
                        {
                            scService.Start();
                            scService.Refresh();
                            scService.WaitForStatus(ServiceControllerStatus.Running, timeout);
                            btnServiceStatus.Text = "Stop";
                            lblSerivceType.Text = STARTSERVICE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + Environment.NewLine + ex.Source);
                this.Message = ex.Message;
            }
            finally { }
        }

        private void ReadandTruncateAcmeDsynLog(bool isAcmeDsyn)
        {
            string FilePath = PagePath.GetAcmeDSyncServicePath();
            if (Directory.Exists(FilePath))
            {
                FilePath = FilePath + "\\" + "DsynLog.txt";
                if (File.Exists(FilePath))
                {
                    if (!isAcmeDsyn)
                    {
                        using (FileStream fileStream = new FileStream(FilePath, FileMode.Truncate))
                        {
                            fileStream.Close();
                        }
                        txtServiceDescription.Text = string.Empty;
                    }
                    else
                    {
                        string line = string.Empty;

                        using (StreamReader fileStream = new StreamReader(FilePath))
                        {
                            while ((line = fileStream.ReadLine()) != null)
                            {
                                AcmeDSynLog += Environment.NewLine + line;
                            }
                            fileStream.Close();
                            txtServiceDescription.Text = AcmeDSynLog;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get service status by service name.
        /// </summary>
        /// <returns></returns>
        private string GetServiceStatus()
        {
            string serviceStatus = string.Empty;
            try
            {
                ServiceController[] services;
                services = ServiceController.GetServices();
                foreach (ServiceController scServices in services)
                {
                    if (scServices.ServiceName.Equals(AcMEDS))
                    {
                        ServiceController serviceController = new ServiceController(AcMEDS);
                        if (serviceController.Status == ServiceControllerStatus.Stopped)
                        {
                            serviceStatus = STOPSERVICE;
                            btnServiceStatus.Text = "Start";
                        }
                        else if (serviceController.Status == ServiceControllerStatus.Running)
                        {
                            serviceStatus = STARTSERVICE;
                            btnServiceStatus.Text = "Stop";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message + Environment.NewLine + ex.Source);
                this.Message = ex.Message;
            }
            finally { }
            return serviceStatus;
        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblSerivceType.Text = GetServiceStatus();
                ReadandTruncateAcmeDsynLog(true);
            }
        }

        protected void btnServiceStatus_Click(object sender, EventArgs e)
        {
            ProcessAcMEDSService();
            ReadandTruncateAcmeDsynLog(true);
        }

        protected void btnClearLog_Click(object sender, EventArgs e)
        {
            ReadandTruncateAcmeDsynLog(false);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            lblSerivceType.Text = GetServiceStatus();
            ReadandTruncateAcmeDsynLog(true);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            string navurl = this.GetPageUrlByName(URLPages.HomeLogin.ToString());
            Response.Redirect(navurl, false);
        }
        #endregion
    }
}