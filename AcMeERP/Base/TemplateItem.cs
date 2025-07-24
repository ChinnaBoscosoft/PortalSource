using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;

/// <summary>
/// Summary description for TemplateItem
/// </summary>
/// 
namespace AcMeERP.Base
{
    // Create a template class to represent a dynamic template column.
    public class GridViewTemplateField : TemplateField
    {
        private ITemplate itemTemplate;

        public GridViewTemplateField(TemplateColumnCollection templateColumnCollection)
        {
            this.HeaderText = templateColumnCollection.HeaderText;
            this.itemTemplate = new GridViewTemplateItem(templateColumnCollection);
            base.ItemTemplate = this.itemTemplate;
        }
    }

    // Create a template class to represent a dynamic template column.
    public class GridViewTemplateItem : ITemplate
    {
        TemplateColumnCollection templateColumnCollection;

        #region Template Methods

        public GridViewTemplateItem()
        {

        }

        public GridViewTemplateItem(TemplateColumnCollection templateColumnCollection)
        {
            this.templateColumnCollection = templateColumnCollection;
        }

        #endregion

        #region Instantiate Template Columns

        public void InstantiateIn(System.Web.UI.Control container)
        {
            // Create the content for the different row types.
            switch (templateColumnCollection.ControlType)
            {
                case ControlType.Literal:
                    // Create the controls to put in the header section and set their properties.
                    Literal lc = new Literal();
                    lc.Text = this.templateColumnCollection.BoundField;
                    lc.DataBinding += new EventHandler(this.LiteralControl_DataBinding);

                    // Add the controls to the Controls collection of the container.
                    container.Controls.Add(lc);
                    break;
                case ControlType.Label:
                    // Create the controls to put in a data row section and set their properties.
                    Label lbc = new Label();
                    lbc.Text = this.templateColumnCollection.BoundField;

                    // To support data binding, register the event-handling methods
                    // to perform the data binding. Each control needs its own event
                    // handler.
                    lbc.DataBinding += new EventHandler(this.LabelControl_DataBinding);

                    // Add the controls to the Controls collection of the container.
                    container.Controls.Add(lbc);
                    break;
                case ControlType.ComboBox:
                    // Create the controls to put in a data row section and set their properties.
                    DropDownList cbo = new DropDownList();
                    cbo.Text = this.templateColumnCollection.BoundField;

                    // To support data binding, register the event-handling methods
                    // to perform the data binding. Each control needs its own event
                    // handler.
                    cbo.DataBinding += new EventHandler(this.DropDownControl_DataBinding);

                    // Add the controls to the Controls collection of the container.
                    container.Controls.Add(cbo);
                    break;

                case ControlType.ImageButton:
                    string imageUrl = this.templateColumnCollection.ImageUrl;
                    ImageButton ib = new ImageButton();

                    //set properties
                    ib.ID = "img" + this.templateColumnCollection.CommandMode.ToString();
                    ib.AlternateText = this.templateColumnCollection.CommandMode.ToString();
                    ib.CommandName = this.templateColumnCollection.CommandMode.ToString();
                    ib.ToolTip = this.templateColumnCollection.CommandMode.ToString();

                    if (imageUrl == "")
                    {
                        ib.SkinID = GetImageSkin(this.templateColumnCollection.CommandMode, ib.GetType());
                    }

                    ib.ImageUrl = imageUrl;

                    if (this.templateColumnCollection.CommandMode == CommandMode.Delete)
                    {
                        ib.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.Delete_Confirm + "');";
                    }

                    //Create Events
                    ib.Command += new CommandEventHandler(this.ImageButton_Command);
                    ib.DataBinding += new EventHandler(this.ImageButtonControl_DataBinding);

                    // Add the controls to the Controls collection of the container.
                    container.Controls.Add(ib);
                    break;
                case ControlType.LinkButton:
                    // Create the controls to put in a data row section and set their properties.
                    LinkButton lbtc = new LinkButton();

                    //lbtc.ID = "lbt" + this.templateColumnCollection.HeaderText.ToString();
                    lbtc.Text = this.templateColumnCollection.CommandMode.ToString();

                    lbtc.CommandName = this.templateColumnCollection.CommandMode.ToString();
                    lbtc.Command += new CommandEventHandler(LinkButton_Command);

                    if (this.templateColumnCollection.CommandMode == CommandMode.Delete)
                    {
                        lbtc.OnClientClick = "javascript:return confirm('" + MessageCatalog.Message.Delete_Confirm + "');";
                    }

                    //lbtc.Command +=new CommandEventHandler(lbtc_Command);
                    // To support data binding, register the event-handling methods
                    // to perform the data binding. Each control needs its own event handler.
                    lbtc.DataBinding += new EventHandler(this.LinkButtonControl_DataBinding);

                    // Add the controls to the Controls collection of the container.
                    container.Controls.Add(lbtc);
                    break;
                case ControlType.HyperLink:
                    // Create the controls to put in a data row section and set their properties.
                    HyperLink hlk = new HyperLink();

                    hlk.ID = "hlk" + this.templateColumnCollection.CommandMode.ToString();
                    hlk.SkinID = GetImageSkin(this.templateColumnCollection.CommandMode, hlk.GetType());
                    hlk.Text = this.templateColumnCollection.CommandMode.ToString();
                    hlk.Target = "_self";

                    if (this.templateColumnCollection.LinkUrlColumn != null && this.templateColumnCollection.LinkUrlColumn.ShowNewWindow)
                    {
                        hlk.Target = "_blank";
                    }

                    // To support data binding, register the event-handling methods
                    // to perform the data binding. Each control needs its own event
                    // handler.
                    hlk.DataBinding += new EventHandler(this.HyperLinkControl_DataBinding);

                    // Add the controls to the Controls collection of the container.
                    container.Controls.Add(hlk);
                    break;
                case ControlType.CheckBox:
                    // Create the controls to put in a data row section and set their properties.
                    CheckBox chk = new CheckBox();
                    chk.ID = "chk" + this.templateColumnCollection.CommandMode.ToString();
                    chk.ToolTip = this.templateColumnCollection.CommandMode.ToString();
                   // chk.Text = this.templateColumnCollection.BoundField;

                    // To support data binding, register the event-handling methods
                    // to perform the data binding. Each control needs its own event
                    // handler.
                    chk.DataBinding += new EventHandler(this.CheckBoxControl_DataBinding);

                    // Add the controls to the Controls collection of the container.
                    container.Controls.Add(chk);
                    break;
                default:
                    // Insert code to handle unexpected values.
                    break;
            }
        }

        #endregion

        #region Data Binding for Controls

        private void LiteralControl_DataBinding(Object sender, EventArgs e)
        {
            // Get the Label control to bind the value. The Label control
            // is contained in the object that raised the DataBinding 
            // event (the sender parameter).
            Literal lt = (Literal)sender;

            // Get the GridViewRow object that contains the Label control. 
            GridViewRow row = (GridViewRow)lt.NamingContainer;

            // Get the field value from the GridViewRow object and 
            // assign it to the Text property of the Label control.
            if (this.templateColumnCollection.BoundField != "")
            {
                lt.Text = DataBinder.Eval(row.DataItem, this.templateColumnCollection.BoundField).ToString();
            }
        }

        private void LabelControl_DataBinding(Object sender, EventArgs e)
        {
            // Get the Label control to bind the value. The Label control
            // is contained in the object that raised the DataBinding 
            // event (the sender parameter).
            Label lbl = (Label)sender;

            // Get the GridViewRow object that contains the Label control. 
            GridViewRow row = (GridViewRow)lbl.NamingContainer;

            // Get the field value from the GridViewRow object and 
            // assign it to the Text property of the Label control.
            if (this.templateColumnCollection.BoundField != "")
            {
                lbl.Text = DataBinder.Eval(row.DataItem, this.templateColumnCollection.BoundField).ToString();
            }
        }

        private void DropDownControl_DataBinding(Object sender, EventArgs e)
        {
            // Get the Label control to bind the value. The Label control
            // is contained in the object that raised the DataBinding 
            // event (the sender parameter).
            DropDownList cbo = (DropDownList)sender;

            // Get the GridViewRow object that contains the Label control. 
            GridViewRow row = (GridViewRow)cbo.NamingContainer;

            // Get the field value from the GridViewRow object and 
            // assign it to the Text property of the Label control.
            if (this.templateColumnCollection.BoundField != "")
            {
                cbo.DataSource = this.templateColumnCollection.BindSource;
                cbo.DataTextField = this.templateColumnCollection.TextField;
                cbo.DataValueField = this.templateColumnCollection.ValueField;
                cbo.DataBind();
            }
        }

        private void ImageButtonControl_DataBinding(Object sender, EventArgs e)
        {
            ImageButton ib = (ImageButton)sender;
            ib.CausesValidation = false;
            // Get the GridViewRow object that contains the Label control.
            GridViewRow row = (GridViewRow)ib.NamingContainer;
            string primaryRowIdField = "";

            // Get the field value from the GridViewRow object and 
            // assign it to the Text property of the Label control.
            if (this.templateColumnCollection.RowIdField != "")
            {
                primaryRowIdField = GetRowIdField(this.templateColumnCollection.RowIdField);
                ib.CommandArgument = DataBinder.Eval(row.DataItem, primaryRowIdField).ToString();
            }

            if (this.templateColumnCollection.ScriptColumn != null)
            {
                string arg = "";
                if (this.templateColumnCollection.ScriptColumn.ArgColumnName != "")
                {
                    arg = DataBinder.Eval(row.DataItem, this.templateColumnCollection.ScriptColumn.ArgColumnName).ToString();
                }

                string func = this.templateColumnCollection.ScriptColumn.FunctionName;
                ib.Attributes.Add("onclick", "javascript:" + func + "('" + arg + "');return false");
            }

            if ((this.templateColumnCollection.CommandMode == CommandMode.Edit) &&
                this.templateColumnCollection.LinkUrlColumn != null)
            {
                int height = this.templateColumnCollection.LinkUrlColumn.Height;
                int width = this.templateColumnCollection.LinkUrlColumn.Width;
                int left = this.templateColumnCollection.LinkUrlColumn.Left;
                int top = this.templateColumnCollection.LinkUrlColumn.Top;
                bool showModel = this.templateColumnCollection.LinkUrlColumn.ShowModelWindow;
                bool callReturn = this.templateColumnCollection.LinkUrlColumn.CallReturn;
                if (showModel == false) callReturn = false;

                string commandArgumentField = this.templateColumnCollection.RowIdField;

                if (commandArgumentField != "")
                {
                    string script = GetNavigateUrl(this.templateColumnCollection.LinkUrlColumn.LinkUrlPage, commandArgumentField, row, height, width, left, top, callReturn, showModel);
                    ib.OnClientClick = script;
                }
            }
        }

        private void LinkButtonControl_DataBinding(Object sender, EventArgs e)
        {
            LinkButton lbtn = (LinkButton)sender;

            // Get the GridViewRow object that contains the Label control.
            GridViewRow row = (GridViewRow)lbtn.NamingContainer;
            string primaryRowIdField = "";

            // Get the field value from the GridViewRow object and 
            // assign it to the Text property of the Label control.
            if (this.templateColumnCollection.BoundField != "")
            {
                lbtn.Text = DataBinder.Eval(row.DataItem, this.templateColumnCollection.BoundField).ToString();
            }

            if (this.templateColumnCollection.RowIdField != "")
            {
                primaryRowIdField = GetRowIdField(this.templateColumnCollection.RowIdField);
                lbtn.CommandArgument = DataBinder.Eval(row.DataItem, primaryRowIdField).ToString();
            }

            if ((this.templateColumnCollection.CommandMode == CommandMode.Edit && this.templateColumnCollection.LinkUrlColumn != null)  
                || (this.templateColumnCollection.CommandMode == CommandMode.Resend && this.templateColumnCollection.LinkUrlColumn != null) )
            {
                int height = this.templateColumnCollection.LinkUrlColumn.Height;
                int width = this.templateColumnCollection.LinkUrlColumn.Width;
                int left = this.templateColumnCollection.LinkUrlColumn.Left;
                int top = this.templateColumnCollection.LinkUrlColumn.Top;
                bool showModel = this.templateColumnCollection.LinkUrlColumn.ShowModelWindow;
                bool callReturn = this.templateColumnCollection.LinkUrlColumn.CallReturn;
                if (showModel == false) callReturn = false;

                string commandArgumentField = this.templateColumnCollection.RowIdField;

                if (commandArgumentField != "")
                {
                    string script = GetNavigateUrl(this.templateColumnCollection.LinkUrlColumn.LinkUrlPage, commandArgumentField, row, height, width, left, top, callReturn, showModel);
                    lbtn.OnClientClick = script;
                }
            }
        }

        private void HyperLinkControl_DataBinding(Object sender, EventArgs e)
        {
            HyperLink hlk = (HyperLink)sender;
            GridViewRow row = (GridViewRow)hlk.NamingContainer;
            string commandArgumentField = this.templateColumnCollection.RowIdField;
            LinkUrlColumn activeTemplateUrl = this.templateColumnCollection.LinkUrlColumn;

            //Template Column contains conditional and alternate url binding
            if (this.templateColumnCollection.IsAlternateUrlBind && this.templateColumnCollection.AlternateLinkUrlColumn != null &&
                this.templateColumnCollection.ConditionalField != "")
            {
                string conditionalValue = DataBinder.Eval(row.DataItem, this.templateColumnCollection.ConditionalField).ToString();
                
                if (conditionalValue.ToLower() != this.templateColumnCollection.ConditionalValue.ToLower())
                {
                    this.templateColumnCollection.LinkUrlColumn = this.templateColumnCollection.AlternateLinkUrlColumn;
                }
            }

            if (this.templateColumnCollection.LinkUrlColumn != null)
            {
                bool showModel = this.templateColumnCollection.LinkUrlColumn.ShowModelWindow;

                if (this.templateColumnCollection.BoundField != "")
                {
                    string url = GetUrl(this.templateColumnCollection.LinkUrlColumn.LinkUrlPage, this.templateColumnCollection.RowIdField, row);
                    hlk.NavigateUrl = url;
                    hlk.Text = DataBinder.Eval(row.DataItem, this.templateColumnCollection.BoundField).ToString();
                }
                else if (commandArgumentField != "" && !showModel)
                {
                    string url = GetUrl(this.templateColumnCollection.LinkUrlColumn.LinkUrlPage, this.templateColumnCollection.RowIdField, row);
                    hlk.NavigateUrl = url;
                }
                else if (commandArgumentField != "" && showModel)
                {
                    int height = this.templateColumnCollection.LinkUrlColumn.Height;
                    int width = this.templateColumnCollection.LinkUrlColumn.Width;
                    int left = this.templateColumnCollection.LinkUrlColumn.Left;
                    int top = this.templateColumnCollection.LinkUrlColumn.Top;
                    bool callReturn = this.templateColumnCollection.LinkUrlColumn.CallReturn;
                    if (showModel == false) callReturn = false;

                    string script = GetNavigateUrl(this.templateColumnCollection.LinkUrlColumn.LinkUrlPage, commandArgumentField, row, height, width, left, top, callReturn, showModel);
                    hlk.Attributes.Add("onclick", script);
                    hlk.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                }
            }

            //Reset the actual URL
            this.templateColumnCollection.LinkUrlColumn = activeTemplateUrl;
        }

        private void CheckBoxControl_DataBinding(Object sender, EventArgs e)
        {
            CheckBox chkbox = (CheckBox)sender;

            // Get the GridViewRow object that contains the Label control.
            GridViewRow row = (GridViewRow)chkbox.NamingContainer;
            string primaryRowIdField = "";

            // Get the field value from the GridViewRow object and 
            // assign it to the Text property of the Label control.
            if (this.templateColumnCollection.BoundField != "")
            {
               chkbox.Checked = Convert.ToBoolean(DataBinder.Eval(row.DataItem, this.templateColumnCollection.BoundField).ToString());
            }

            if (this.templateColumnCollection.RowIdField != "")
            {
                primaryRowIdField = GetRowIdField(this.templateColumnCollection.RowIdField);
                chkbox.ValidationGroup = DataBinder.Eval(row.DataItem, primaryRowIdField).ToString();
            }
        }

        #endregion

        #region Command Events

        private void LinkButton_Command(Object sender, CommandEventArgs e)
        {

        }

        private void ImageButton_Command(Object sender, CommandEventArgs e)
        {

        }

        private void SetTargetUrl(Control ctl)
        {

        }

        #endregion

        #region Skin Image

        public string GetImageSkin(CommandMode commandMode, Type ctlType)
        {
            string imageSkin = "";

            switch (commandMode)
            {
                case CommandMode.Add:
                    {
                        imageSkin = "add_hlk";
                        break;
                    }
                case CommandMode.Edit:
                    {
                        imageSkin = "edit_hlk";
                        break;
                    }
                case CommandMode.Delete:
                    {
                        imageSkin = "delete_ib";
                        break;
                    }
                case CommandMode.Apply:
                    {
                        imageSkin = "apply_ib";
                        break;
                    }
                case CommandMode.Cancel:
                    {
                        imageSkin = "cancel_ib";
                        break;
                    }
                case CommandMode.View:
                    {
                        imageSkin = "view_hlk";
                        if (ctlType == typeof(ImageButton))
                        {
                            imageSkin = "view_ib";
                        }
                        break;
                    }
                case CommandMode.Print:
                    {
                        imageSkin = "printview_hlk";
                        break;
                    }
                case CommandMode.License:
                    {
                        imageSkin = "license_hlk";
                        break;
                    }
                case CommandMode.Send:
                    {
                        imageSkin = "send_hlk";
                        break;
                    }
                case CommandMode.Email:
                    {
                        imageSkin = "email_ib";
                        break;
                    }
                case CommandMode.UserRight:
                    {
                        imageSkin = "userright_hlk";
                        break;
                    }
                case CommandMode.Download:
                    {
                        imageSkin = "download_ib";
                        break;
                    }
                case CommandMode.Status:
                    {
                        imageSkin = "deactivate_ib";
                        break;
                    }
                case CommandMode.DB:
                    {
                        imageSkin = "updatedb_ib";
                        break;
                    }
                case CommandMode.Reset:
                    {
                        imageSkin = "resetpwd_ib";
                        break;
                    }
                case CommandMode.Data:
                    {
                       imageSkin = "data_ib";
                       break;
                    }
                case CommandMode.Key:
                    {
                        imageSkin = "licensekey_ib";
                        break;
                    }
                case CommandMode.Resend:
                    {
                        imageSkin = "mailResend_hlk";
                        break;
                    }
            }

            return imageSkin;
        }

        #endregion

        #region Utility Methods

        //Construct Query string for single / multiple row Id delimited with @
        private string GetNavigateUrl(string page, string commandArguemntField, GridViewRow row,
            int height, int width, int left, int top, bool callReturn, bool showModel)
        {
            string[] aCommandArguemntField = commandArguemntField.Split(Delimiter.AtCap.ToCharArray());
            string rowIdField = commandArguemntField;

            for (int i = 0; i < aCommandArguemntField.Length; i++)
            {
                if (i == 0)
                {
                    rowIdField = aCommandArguemntField[i];
                }
                else
                {
                    page = ClientScriptSetMember.GetQueryString(page, "id" + i.ToString(), DataBinder.Eval(row.DataItem, aCommandArguemntField[i]).ToString());
                }
            }

            string script = ClientScriptSetMember.GetOpenWindowScript(page, DataBinder.Eval(row.DataItem, rowIdField).ToString(), height, width, left, top, callReturn, showModel);
            return script;
        }

        //Construct Query string for single / multiple row Id delimited with @
        private string GetUrl(string page, string commandArguemntField, GridViewRow row)
        {
            string url = page;
            string[] aCommandArguemntField = commandArguemntField.Split(Delimiter.AtCap.ToCharArray());
            string suffix = "";

            for (int i = 0; i < aCommandArguemntField.Length; i++)
            {
                if (i > 0) { suffix = i.ToString(); }
                url = ClientScriptSetMember.GetQueryString(url, "id" + suffix, DataBinder.Eval(row.DataItem, aCommandArguemntField[i]).ToString());
            }

            return url;
        }

        //Construct Query string for single / multiple row Id delimited with @
        private string GetRowIdField(string commandArguemntField)
        {
            string rowIdFld = commandArguemntField;
            string[] aCommandArguemntField = commandArguemntField.Split(Delimiter.AtCap.ToCharArray());

            if (aCommandArguemntField.Length > 0)
            {
                rowIdFld = aCommandArguemntField[0];
            }

            return rowIdFld;
        }

        #endregion
    }
}