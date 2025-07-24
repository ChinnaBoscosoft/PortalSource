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

namespace AcMeERP.Base
{
    public class TemplateColumnCollection
    {
        private ControlType ctlType = ControlType.LinkButton;
        private CommandMode commandMode = CommandMode.Edit;
        private bool isAlternateUrlBind = false;
        private string rowIdField = "";
        private string boundField = "";
        private string headerText = "";
        private string imageUrl = "";

        private string textField = "";
        private string valueField = "";
        private string conditionalValue = "";
        private string conditionalField = "";

        private object bindSource = null;
        private LinkUrlColumn linkUrlColumn = null;
        private ScriptColumn scriptColumn = null;
        private LinkUrlColumn alternateLinkUrlColumn = null;

        public ControlType ControlType
        {
            get { return ctlType; }
            set { ctlType = value; }
        }

        public CommandMode CommandMode
        {
            get { return commandMode; }
            set { commandMode = value; }
        }

        public string RowIdField
        {
            get { return rowIdField; }
            set { rowIdField = value; }
        }

        public string BoundField
        {
            get { return boundField; }
            set { boundField = value; }
        }

        public string ConditionalField
        {
            get { return conditionalField; }
            set { conditionalField = value; }
        }

        public object BindSource
        {
            get { return bindSource; }
            set { bindSource = value; }
        }

        public string TextField
        {
            get { return textField; }
            set { textField = value; }
        }

        public string ValueField
        {
            get { return valueField; }
            set { valueField = value; }
        }

        public string ConditionalValue
        {
            get { return conditionalValue; }
            set { conditionalValue = value; }
        }

        public string HeaderText
        {
            get { return headerText; }
            set { headerText = value; }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        public LinkUrlColumn LinkUrlColumn
        {
            get { return linkUrlColumn; }
            set { linkUrlColumn = value; }
        }

        public LinkUrlColumn AlternateLinkUrlColumn
        {
            get { return alternateLinkUrlColumn; }
            set { alternateLinkUrlColumn = value; }
        }

        public ScriptColumn ScriptColumn
        {
            get { return scriptColumn; }
            set { scriptColumn = value; }
        }

        public bool IsAlternateUrlBind
        {
            get { return isAlternateUrlBind; }
            set { isAlternateUrlBind = value; }
        }

        public TemplateColumnCollection(ControlType ctlType, CommandMode commandMode,
               string rowIdField, string boundField, LinkUrlColumn linkUrlColumn,
               string imageUrl, string headerText, ScriptColumn scriptColumn)
        {
            this.ctlType = ctlType;
            this.commandMode = commandMode;
            this.rowIdField = rowIdField;
            this.boundField = boundField;
            this.linkUrlColumn = linkUrlColumn;
            this.headerText = headerText;
            this.imageUrl = imageUrl;
            this.scriptColumn = scriptColumn;
        }

        public TemplateColumnCollection(ControlType ctlType, CommandMode commandMode,
            string rowIdField, string conditionalField, string conditionalValue,
            LinkUrlColumn linkUrlColumn, LinkUrlColumn alternateLinkUrlColumn, string headerText)
        {
            this.ctlType = ctlType;
            this.commandMode = commandMode;
            this.rowIdField = rowIdField;
            this.conditionalField = conditionalField;
            this.headerText = headerText;
            this.isAlternateUrlBind = true;
            this.conditionalValue = conditionalValue;
            this.linkUrlColumn = linkUrlColumn;
            this.alternateLinkUrlColumn = alternateLinkUrlColumn;
        }
    }

    public class ScriptColumn
    {
        private string functionName = "";
        private string argColumnName = "";

        public ScriptColumn(string functionName, string argColumnName)
        {
            this.functionName = functionName;
            this.argColumnName = argColumnName;
        }

        public string FunctionName
        {
            get { return functionName; }
            set { functionName = value; }
        }

        public string ArgColumnName
        {
            get { return argColumnName; }
            set { argColumnName = value; }
        }
    }

    //object dataSource,string textField,string ValueField
    public class LinkUrlColumn
    {
        private string linkUrlPage = "";
        private string linkUrlCaption = "Add";
        private bool showNewWindow = false;
        private int height = 0;
        private int width = 0;
        private int left = 0;
        private int top = 0;
        private bool showModelWindow = false;
        private bool callReturn = false;

        public string LinkUrlPage
        {
            get { return linkUrlPage; }
            set { linkUrlPage = value; }
        }

        public bool ShowNewWindow
        {
            get { return showNewWindow; }
            set { showNewWindow = value; }
        }

        public bool ShowModelWindow
        {
            get { return showModelWindow; }
            set { showModelWindow = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        public bool CallReturn
        {
            get { return callReturn; }
            set { callReturn = value; }
        }

        public string LinkUrlCaption
        {
            get { return (linkUrlCaption.Trim() == "") ? "Add" : linkUrlCaption; }
            set { linkUrlCaption = value; }
        }

        public LinkUrlColumn(string linkUrlPage, string linkUrlCaption, bool showNewWindow)
        {
            this.linkUrlPage = linkUrlPage;
            this.linkUrlCaption = linkUrlCaption;
            this.showNewWindow = showNewWindow;
        }

        public LinkUrlColumn(string linkUrlPage, bool showNewWindow, int height, int width, int left, int top, bool modelWindow)
            : this(linkUrlPage, showNewWindow, height, width, left, top, modelWindow, false)
        {
        }

        public LinkUrlColumn(string linkUrlPage, bool showNewWindow, int height, int width, int left, int top, bool modelWindow, bool callReturn)
        {
            this.linkUrlPage = linkUrlPage;
            this.showNewWindow = showNewWindow;
            this.height = height;
            this.width = width;
            this.left = left;
            this.top = top;
            this.showModelWindow = modelWindow;
            this.callReturn = callReturn;
        }
    }
}