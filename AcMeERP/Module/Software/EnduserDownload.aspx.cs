/*****************************************************************************************************
 * Created by       : Chinna M
 * Created On       : 9th June 2014
 *  
 * Modified by      : 
 * Modified On      : 
 * Modified Purpose : 
 * 
 * Reviewed By      : 
 * Reviewed On      : 
 * 
 * Purpose          :This page allows the end user to download the latest build of acme.erp product, updater , Prerequiste files for the acme.erp product installation.
 *****************************************************************************************************/
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Data;

namespace AcMeERP.Module.Software
{
    public partial class EnduserDownload : Base.UIBase
    {
        #region Properties
        private string filename = "";
        private string contenttype = "";
        private int PageNumber
        {
            get
            {
                if (ViewState["pagenumber"] != null)
                {
                    return this.Member.NumberSet.ToInteger(ViewState["pagenumber"].ToString());
                }
                else
                    return 0;
            }
            set
            {
                ViewState["pagenumber"] = value;
            }

        }
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.PageTitle = "Download Software";
                this.CheckUserRights(RightsModule.Tools, RightsActivity.SoftwareDownload, base.LoginUser.LoginUserHeadOfficeCode == string.Empty ? DataBaseType.Portal : DataBaseType.HeadOffice);
                FillList();
                FillDataList();
                
            }
        }
              
        protected void lnkReadMe_Click(object sender, EventArgs e)
        {
            LinkButton btnRelease = (LinkButton)sender;
            string filename = btnRelease.CommandArgument.ToString();
            if (!(string.IsNullOrEmpty(filename)))
            {
                string filePath = Server.MapPath(CommonMember.DOWNLOAD_FOLDER + filename);
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    string text = File.ReadAllText(filePath);
                    if (!string.IsNullOrEmpty(text))
                    {
                        lblReadMe.Text = text;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Release", "javascript:showpopupbox(true)", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Release", "javascript:ShowDisplayPopUp()", true);
                    }
                }
            }
        }
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            PageNumber += 1;
            FillList();
        }
        protected void lnkPrevious_Click(object sender, EventArgs e)
        {
            PageNumber -= 1;
            FillList();
        }
        
        #endregion

        #region Methods
        private void FillList()
        {
            DataTable dtsoftware = null;
            using (SoftwareSystem softwareSystem = new SoftwareSystem())
            {
                int cnt = 0;
                softwareSystem.UPLOAD_TYPE =(int) FileUploadType.Build;
                ResultArgs resultArgs = softwareSystem.FetchSoftwareDetailsByType();

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtsoftware = resultArgs.DataSource.Table;
                    PagedDataSource PagedData = new PagedDataSource();
                    PagedData.DataSource = dtsoftware.DefaultView;
                    cnt = dtsoftware.Rows.Count;
                    PagedData.AllowPaging = true;
                    PagedData.PageSize = 5;
                    PagedData.CurrentPageIndex = PageNumber;
                    if (PageNumber == 0)
                    {
                        lnkPrevious.Visible = false;
                        lnkPreviousUp.Visible = false;
                    }
                    else
                    {
                        lnkPrevious.Visible = true;
                        lnkPreviousUp.Visible = true;
                    }
                    if (PagedData.PageCount == PageNumber + 1)
                    {
                        lnkNext.Visible = false;
                        lnkNextUp.Visible = false;
                    }
                    else
                    {
                        lnkNext.Visible = true;
                        lnkNextUp.Visible = true;
                    }
                    rpdownloads.DataSource = PagedData;
                    rpdownloads.DataBind();
                }
            }
        }

        private void FillDataList()
        {
            DataTable dtPrequisite = null;
            using (SoftwareSystem softwareSystem = new SoftwareSystem())
            {
                softwareSystem.UPLOAD_TYPE = (int)FileUploadType.Prerequisite;
                ResultArgs resultArgs = softwareSystem.FetchSoftwareDetailsByType();

                if (resultArgs.Success && resultArgs.RowsAffected>0)
                {
                    dtPrequisite = resultArgs.DataSource.Table;
                    dlPrerequisite.DataSource = dtPrequisite;
                    dlPrerequisite.DataBind();
                }
            }
        }

        private void DownLoadFile()
        {
            byte[] bytes;
            bytes = File.ReadAllBytes(Server.MapPath(CommonMember.DOWNLOAD_PATH + filename));
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contenttype;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename.Trim().Replace(" ", "").Trim());
            Response.BinaryWrite(bytes);
            Response.Flush();
            //Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        #endregion
    }
}