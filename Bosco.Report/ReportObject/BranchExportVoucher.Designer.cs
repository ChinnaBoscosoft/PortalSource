namespace Bosco.Report.ReportObject
{
    partial class BranchExportVoucher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.xrTableValue = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrBranch = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrUploadedOn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVoucherFrom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrVoucherTo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLocation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrUploadedBy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrStatus = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTableHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrCapCode = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapBranch = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapUploadedOn = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapVoucherFrom = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapVoucherTo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapLocation = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapUloadBy = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrCapStatus = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableValue});
            this.Detail.Expanded = true;
            this.Detail.HeightF = 25F;
            this.Detail.Visible = true;
            // 
            // xrTableValue
            // 
            this.xrTableValue.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableValue.Name = "xrTableValue";
            this.xrTableValue.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTableValue.SizeF = new System.Drawing.SizeF(1136F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCode,
            this.xrBranch,
            this.xrUploadedOn,
            this.xrVoucherFrom,
            this.xrVoucherTo,
            this.xrLocation,
            this.xrUploadedBy,
            this.xrStatus});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrCode
            // 
            this.xrCode.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchExportVoucher.BRANCH_PART_CODE")});
            this.xrCode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrCode.Name = "xrCode";
            this.xrCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCode.StylePriority.UseBorderColor = false;
            this.xrCode.StylePriority.UseBorders = false;
            this.xrCode.StylePriority.UseFont = false;
            this.xrCode.StylePriority.UsePadding = false;
            this.xrCode.StylePriority.UseTextAlignment = false;
            this.xrCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrCode.Weight = 0.12345633160803959D;
            // 
            // xrBranch
            // 
            this.xrBranch.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrBranch.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrBranch.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchExportVoucher.BRANCH_OFFICE_NAME")});
            this.xrBranch.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrBranch.Name = "xrBranch";
            this.xrBranch.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrBranch.StylePriority.UseBorderColor = false;
            this.xrBranch.StylePriority.UseBorders = false;
            this.xrBranch.StylePriority.UseFont = false;
            this.xrBranch.StylePriority.UsePadding = false;
            this.xrBranch.StylePriority.UseTextAlignment = false;
            this.xrBranch.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrBranch.Weight = 0.7217446620920559D;
            // 
            // xrUploadedOn
            // 
            this.xrUploadedOn.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrUploadedOn.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrUploadedOn.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchExportVoucher.UPLOADED_ON")});
            this.xrUploadedOn.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrUploadedOn.Name = "xrUploadedOn";
            this.xrUploadedOn.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrUploadedOn.StylePriority.UseBorderColor = false;
            this.xrUploadedOn.StylePriority.UseBorders = false;
            this.xrUploadedOn.StylePriority.UseFont = false;
            this.xrUploadedOn.StylePriority.UsePadding = false;
            this.xrUploadedOn.StylePriority.UseTextAlignment = false;
            this.xrUploadedOn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrUploadedOn.Weight = 0.25071130556894494D;
            // 
            // xrVoucherFrom
            // 
            this.xrVoucherFrom.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrVoucherFrom.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrVoucherFrom.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchExportVoucher.VOUCHER_FROM")});
            this.xrVoucherFrom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrVoucherFrom.Name = "xrVoucherFrom";
            this.xrVoucherFrom.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrVoucherFrom.StylePriority.UseBorderColor = false;
            this.xrVoucherFrom.StylePriority.UseBorders = false;
            this.xrVoucherFrom.StylePriority.UseFont = false;
            this.xrVoucherFrom.StylePriority.UsePadding = false;
            this.xrVoucherFrom.Weight = 0.15194624641439447D;
            // 
            // xrVoucherTo
            // 
            this.xrVoucherTo.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrVoucherTo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrVoucherTo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchExportVoucher.VOUCHER_TO")});
            this.xrVoucherTo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrVoucherTo.Name = "xrVoucherTo";
            this.xrVoucherTo.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrVoucherTo.StylePriority.UseBackColor = false;
            this.xrVoucherTo.StylePriority.UseBorderColor = false;
            this.xrVoucherTo.StylePriority.UseBorders = false;
            this.xrVoucherTo.StylePriority.UseFont = false;
            this.xrVoucherTo.StylePriority.UsePadding = false;
            this.xrVoucherTo.Weight = 0.15194625450005511D;
            // 
            // xrLocation
            // 
            this.xrLocation.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrLocation.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLocation.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchExportVoucher.LOCATION")});
            this.xrLocation.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrLocation.Name = "xrLocation";
            this.xrLocation.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrLocation.StylePriority.UseBorderColor = false;
            this.xrLocation.StylePriority.UseBorders = false;
            this.xrLocation.StylePriority.UseFont = false;
            this.xrLocation.StylePriority.UsePadding = false;
            this.xrLocation.Weight = 0.38936227425965925D;
            // 
            // xrUploadedBy
            // 
            this.xrUploadedBy.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrUploadedBy.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrUploadedBy.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchExportVoucher.UPLOADED_BY")});
            this.xrUploadedBy.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrUploadedBy.Name = "xrUploadedBy";
            this.xrUploadedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrUploadedBy.StylePriority.UseBorderColor = false;
            this.xrUploadedBy.StylePriority.UseBorders = false;
            this.xrUploadedBy.StylePriority.UseFont = false;
            this.xrUploadedBy.StylePriority.UsePadding = false;
            this.xrUploadedBy.Weight = 0.21842273017290226D;
            // 
            // xrStatus
            // 
            this.xrStatus.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrStatus.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrStatus.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BranchExportVoucher.STATUS")});
            this.xrStatus.Font = new System.Drawing.Font("Tahoma", 9F);
            this.xrStatus.Name = "xrStatus";
            this.xrStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrStatus.StylePriority.UseBorderColor = false;
            this.xrStatus.StylePriority.UseBorders = false;
            this.xrStatus.StylePriority.UseFont = false;
            this.xrStatus.StylePriority.UsePadding = false;
            this.xrStatus.Weight = 0.15004695493072634D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableHeader});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrTableHeader
            // 
            this.xrTableHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableHeader.Name = "xrTableHeader";
            this.xrTableHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTableHeader.SizeF = new System.Drawing.SizeF(1136F, 25F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrCapCode,
            this.xrCapBranch,
            this.xrCapUploadedOn,
            this.xrCapVoucherFrom,
            this.xrCapVoucherTo,
            this.xrCapLocation,
            this.xrCapUloadBy,
            this.xrCapStatus});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrCapCode
            // 
            this.xrCapCode.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapCode.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapCode.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapCode.Name = "xrCapCode";
            this.xrCapCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapCode.StylePriority.UseBackColor = false;
            this.xrCapCode.StylePriority.UseBorderColor = false;
            this.xrCapCode.StylePriority.UseBorders = false;
            this.xrCapCode.StylePriority.UseFont = false;
            this.xrCapCode.StylePriority.UsePadding = false;
            this.xrCapCode.Text = "Code";
            this.xrCapCode.Weight = 0.12345621537067995D;
            // 
            // xrCapBranch
            // 
            this.xrCapBranch.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapBranch.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapBranch.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapBranch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapBranch.Name = "xrCapBranch";
            this.xrCapBranch.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapBranch.StylePriority.UseBackColor = false;
            this.xrCapBranch.StylePriority.UseBorderColor = false;
            this.xrCapBranch.StylePriority.UseBorders = false;
            this.xrCapBranch.StylePriority.UseFont = false;
            this.xrCapBranch.StylePriority.UsePadding = false;
            this.xrCapBranch.Text = "Branch";
            this.xrCapBranch.Weight = 0.72174401589801662D;
            // 
            // xrCapUploadedOn
            // 
            this.xrCapUploadedOn.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapUploadedOn.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapUploadedOn.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapUploadedOn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapUploadedOn.Name = "xrCapUploadedOn";
            this.xrCapUploadedOn.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapUploadedOn.StylePriority.UseBackColor = false;
            this.xrCapUploadedOn.StylePriority.UseBorderColor = false;
            this.xrCapUploadedOn.StylePriority.UseBorders = false;
            this.xrCapUploadedOn.StylePriority.UseFont = false;
            this.xrCapUploadedOn.StylePriority.UsePadding = false;
            this.xrCapUploadedOn.StylePriority.UseTextAlignment = false;
            this.xrCapUploadedOn.Text = "Uploaded On";
            this.xrCapUploadedOn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrCapUploadedOn.Weight = 0.25071107393244951D;
            // 
            // xrCapVoucherFrom
            // 
            this.xrCapVoucherFrom.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapVoucherFrom.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapVoucherFrom.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapVoucherFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapVoucherFrom.Name = "xrCapVoucherFrom";
            this.xrCapVoucherFrom.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapVoucherFrom.StylePriority.UseBackColor = false;
            this.xrCapVoucherFrom.StylePriority.UseBorderColor = false;
            this.xrCapVoucherFrom.StylePriority.UseBorders = false;
            this.xrCapVoucherFrom.StylePriority.UseFont = false;
            this.xrCapVoucherFrom.StylePriority.UsePadding = false;
            this.xrCapVoucherFrom.Text = "From";
            this.xrCapVoucherFrom.Weight = 0.15194610686108159D;
            // 
            // xrCapVoucherTo
            // 
            this.xrCapVoucherTo.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapVoucherTo.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapVoucherTo.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapVoucherTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapVoucherTo.Name = "xrCapVoucherTo";
            this.xrCapVoucherTo.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapVoucherTo.StylePriority.UseBackColor = false;
            this.xrCapVoucherTo.StylePriority.UseBorderColor = false;
            this.xrCapVoucherTo.StylePriority.UseBorders = false;
            this.xrCapVoucherTo.StylePriority.UseFont = false;
            this.xrCapVoucherTo.StylePriority.UsePadding = false;
            this.xrCapVoucherTo.Text = "To";
            this.xrCapVoucherTo.Weight = 0.15194610597761257D;
            // 
            // xrCapLocation
            // 
            this.xrCapLocation.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapLocation.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapLocation.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapLocation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapLocation.Name = "xrCapLocation";
            this.xrCapLocation.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapLocation.StylePriority.UseBackColor = false;
            this.xrCapLocation.StylePriority.UseBorderColor = false;
            this.xrCapLocation.StylePriority.UseBorders = false;
            this.xrCapLocation.StylePriority.UseFont = false;
            this.xrCapLocation.StylePriority.UsePadding = false;
            this.xrCapLocation.Text = "Location";
            this.xrCapLocation.Weight = 0.38936190865384657D;
            // 
            // xrCapUloadBy
            // 
            this.xrCapUloadBy.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapUloadBy.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapUloadBy.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapUloadBy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapUloadBy.Name = "xrCapUloadBy";
            this.xrCapUloadBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapUloadBy.StylePriority.UseBackColor = false;
            this.xrCapUloadBy.StylePriority.UseBorderColor = false;
            this.xrCapUloadBy.StylePriority.UseBorders = false;
            this.xrCapUloadBy.StylePriority.UseFont = false;
            this.xrCapUloadBy.StylePriority.UsePadding = false;
            this.xrCapUloadBy.Text = "Uploaded By";
            this.xrCapUloadBy.Weight = 0.21842253830852582D;
            // 
            // xrCapStatus
            // 
            this.xrCapStatus.BackColor = System.Drawing.Color.Gainsboro;
            this.xrCapStatus.BorderColor = System.Drawing.Color.DarkGray;
            this.xrCapStatus.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrCapStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrCapStatus.Name = "xrCapStatus";
            this.xrCapStatus.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrCapStatus.StylePriority.UseBackColor = false;
            this.xrCapStatus.StylePriority.UseBorderColor = false;
            this.xrCapStatus.StylePriority.UseBorders = false;
            this.xrCapStatus.StylePriority.UseFont = false;
            this.xrCapStatus.StylePriority.UsePadding = false;
            this.xrCapStatus.Text = "Status";
            this.xrCapStatus.Weight = 0.15004679646499428D;
            // 
            // BranchExportVoucher
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1});
            this.DataMember = "BranchDataStatus";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(22, 6, 61, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTableValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.XRTable xrTableValue;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrCode;
        private DevExpress.XtraReports.UI.XRTableCell xrBranch;
        private DevExpress.XtraReports.UI.XRTableCell xrUploadedOn;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRTableCell xrVoucherFrom;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrTableHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrCapCode;
        private DevExpress.XtraReports.UI.XRTableCell xrCapBranch;
        private DevExpress.XtraReports.UI.XRTableCell xrCapUploadedOn;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVoucherFrom;
        private DevExpress.XtraReports.UI.XRTableCell xrVoucherTo;
        private DevExpress.XtraReports.UI.XRTableCell xrCapVoucherTo;
        private DevExpress.XtraReports.UI.XRTableCell xrStatus;
        private DevExpress.XtraReports.UI.XRTableCell xrCapStatus;
        private DevExpress.XtraReports.UI.XRTableCell xrLocation;
        private DevExpress.XtraReports.UI.XRTableCell xrUploadedBy;
        private DevExpress.XtraReports.UI.XRTableCell xrCapLocation;
        private DevExpress.XtraReports.UI.XRTableCell xrCapUloadBy;
    }
}
