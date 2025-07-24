using System;
using System.Web.UI.WebControls;
using Bosco.Utility;

namespace AcMeERP.WebControl
{
    public partial class DateControl : System.Web.UI.UserControl
    {
        public string DateCultureName = DateFormatInfo.DateCulture;
        public string FormatString = DateFormatInfo.DateFormat.ToLower();

        #region properties to read control values

        public bool DateFontBold
        {
            set
            {
                this.txtDate.Font.Bold = value;
                if (value == true)
                    txtDate.Width = new Unit(70);
            }
        }

        public bool TimeFontBold
        {
            set
            {
                this.txtTime.Font.Bold = value;
                if (value == true)
                    txtTime.Width = new Unit(71);
            }
        }

        //public string ValidationDateGroup
        //{
        //    set
        //    {
        //        rfvDate.ValidationGroup = value;

        //    }
        //    get { return rfvDate.ValidationGroup; }
        //}

        public bool EnableDateTime
        {
            set
            {
                this.DateTextBox.Enabled = value;
                this.imgDate.Visible = value;
            }
            get { return this.DateTextBox.Enabled; }
        }

        public string ValidationTimeGroup
        {
            set { rfvTime.ValidationGroup = value; }
            get { return rfvTime.ValidationGroup; }
        }

        public string InvalidDateMessage
        {
            set { this.hfDate.Value = value; }
            get { return this.hfDate.Value; }
        }

        public string InvalidTimeMessage
        {
            set { this.hfTime.Value = value; }
            get { return this.hfTime.Value; }
        }

        public bool ShowTime
        {
            set { this.txtTime.Visible = value; }
            get { return this.txtTime.Visible; }
        }

        public bool ShowAMPM
        {
            set { this.meTime.AcceptAMPM = value; }
            get { return this.meTime.AcceptAMPM; }
        }

        public bool AutoPostBack
        {
            set { txtDate.AutoPostBack = txtTime.AutoPostBack = value; }
        }

        public bool showCalender
        {
            set
            {
                this.imgDate.Visible = value;
                if (value == true)
                {
                    txtDate.Width = new Unit(70);
                    txtTime.Width = new Unit(77);
                }
                else
                {
                    txtDate.Width = new Unit(70);
                    txtTime.Width = new Unit(71);
                }
            }
            get { return this.imgDate.Visible; }
        }

        public bool showMandatory
        {
            set { this.lblStar.Visible = value; }
            get { return this.lblStar.Visible; }
        }

        //public string mandatoryDateMessage
        //{
        //    set
        //    {
        //        this.rfvDate.ErrorMessage = value;
        //        this.rfvDate.Visible = (value != "");
        //    }
        //}

        public string mandatoryTimeMessage
        {
            set
            {
                this.rfvTime.ErrorMessage = value;
                this.rfvTime.Visible = (value != "");
            }
        }

        public short TabIndex
        {
            set
            {
                short tbIndex = value;
                if (tbIndex == -1)
                    txtTime.TabIndex = txtDate.TabIndex = tbIndex;
                else
                {
                    txtDate.TabIndex = tbIndex;
                    txtTime.TabIndex = short.Parse((tbIndex + 1).ToString());
                }
            }
        }

        public string DateValue
        {
            set { txtDate.Text = value; }
            get { return txtDate.Text; }
        }

        public string TimeValue
        {
            set { txtTime.Text = value; }
            get { return txtTime.Text; }
        }

        /// <summary>
        /// Get Date Time
        /// </summary>
        public string DateTimeValue
        {
            get { return txtDate.Text + " " + txtTime.Text; }
        }

        public TextBox DateTextBox
        {
            get { return txtDate; }
        }

        public TextBox TimeTextBox
        {
            get { return txtTime; }
        }

        public Image HyperLinkDate
        {
            get { return imgDate; }
        }

        public Label LableMandatory
        {
            get { return lblStar; }
        }

        //public RequiredFieldValidator DateValidator
        //{
        //    get { return this.rfvDate; }
        //}

        public RequiredFieldValidator TimeValidator
        {
            get { return this.rfvTime; }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            this.hfDate.Value = this.hfDate.Value == "" ? "Invalid Date" : this.hfDate.Value;
            this.hfTime.Value = this.hfTime.Value == "" ? "Invalid Time" : this.hfTime.Value;
            this.meDate.CultureName = DateCultureName;
            this.meTime.CultureName = DateCultureName;
        }
    }
}