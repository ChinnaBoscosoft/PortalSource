using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AcMeERP.Base;
using Bosco.Utility;

namespace AcMeERP.WebControl
{
    public partial class Filter : System.Web.UI.UserControl
    {
        #region Declartion
        CommonMember Member = new CommonMember();
        #endregion

        #region Properties

        private object dataSource = null;
       
        public object DataSource
        {
            set
            {
                this.dataSource  = value;                             
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void imgFilterGo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Age", typeof(int));

                dt.Rows.Add("amal", 20);
                dt.Rows.Add("raj", 10);

                dataSource= dt.DefaultView;


               
                DataView dv = dataSource as DataView;

                dv.RowFilter = "Name='" + txtSearch.Text + "'";
             
              


            }
            catch (Exception ex)
            {
                new ErrorLog().WriteError(ex.Message);
            }
        }

        #endregion

        #region Methods


        #endregion
    }
}