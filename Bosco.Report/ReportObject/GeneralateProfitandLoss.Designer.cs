namespace Bosco.Report.ReportObject
{
    partial class GeneralateProfitandLoss
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
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTableHeader = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.tcExpenditure = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcExpenditureYearTo = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcIncome = new DevExpress.XtraReports.UI.XRTableCell();
            this.tcIncomeYearTo = new DevExpress.XtraReports.UI.XRTableCell();
            this.reportSetting1 = new Bosco.Report.ReportSetting();
            this.xrsrincome = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrsrLoss = new DevExpress.XtraReports.UI.XRSubreport();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrtcNetresultnext = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrIncomenxtSum = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrName = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrExpencenxtSum = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrsrincome,
            this.xrsrLoss});
            this.Detail.Expanded = true;
            this.Detail.HeightF = 23F;
            this.Detail.Visible = true;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTableHeader});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTableHeader
            // 
            this.xrTableHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTableHeader.BorderColor = System.Drawing.Color.DarkGray;
            this.xrTableHeader.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTableHeader.Name = "xrTableHeader";
            this.xrTableHeader.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTableHeader.SizeF = new System.Drawing.SizeF(1060F, 25F);
            this.xrTableHeader.StylePriority.UseBackColor = false;
            this.xrTableHeader.StylePriority.UseBorderColor = false;
            this.xrTableHeader.StylePriority.UseBorders = false;
            this.xrTableHeader.StylePriority.UseFont = false;
            this.xrTableHeader.StylePriority.UseTextAlignment = false;
            this.xrTableHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tcExpenditure,
            this.tcExpenditureYearTo,
            this.tcIncome,
            this.tcIncomeYearTo});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // tcExpenditure
            // 
            this.tcExpenditure.Name = "tcExpenditure";
            this.tcExpenditure.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.tcExpenditure.StylePriority.UsePadding = false;
            this.tcExpenditure.Text = "Expenditure";
            this.tcExpenditure.Weight = 0.64954569945034879D;
            // 
            // tcExpenditureYearTo
            // 
            this.tcExpenditureYearTo.Name = "tcExpenditureYearTo";
            this.tcExpenditureYearTo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 5, 0, 0, 100F);
            this.tcExpenditureYearTo.StylePriority.UsePadding = false;
            this.tcExpenditureYearTo.StylePriority.UseTextAlignment = false;
            this.tcExpenditureYearTo.Text = "Amount\t";
            this.tcExpenditureYearTo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tcExpenditureYearTo.Weight = 0.36298141441816728D;
            // 
            // tcIncome
            // 
            this.tcIncome.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.tcIncome.Name = "tcIncome";
            this.tcIncome.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);
            this.tcIncome.StylePriority.UseBorders = false;
            this.tcIncome.StylePriority.UsePadding = false;
            this.tcIncome.StylePriority.UseTextAlignment = false;
            this.tcIncome.Text = "Income";
            this.tcIncome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.tcIncome.Weight = 0.64954566495274269D;
            // 
            // tcIncomeYearTo
            // 
            this.tcIncomeYearTo.Name = "tcIncomeYearTo";
            this.tcIncomeYearTo.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100F);
            this.tcIncomeYearTo.StylePriority.UsePadding = false;
            this.tcIncomeYearTo.StylePriority.UseTextAlignment = false;
            this.tcIncomeYearTo.Text = "Amount";
            this.tcIncomeYearTo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.tcIncomeYearTo.Weight = 0.36298140340032453D;
            // 
            // reportSetting1
            // 
            this.reportSetting1.DataSetName = "ReportSetting";
            this.reportSetting1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrsrincome
            // 
            this.xrsrincome.LocationFloat = new DevExpress.Utils.PointFloat(530.9997F, 0F);
            this.xrsrincome.Name = "xrsrincome";
            this.xrsrincome.ReportSource = new Bosco.Report.ReportObject.GeneralateProfit();
            this.xrsrincome.SizeF = new System.Drawing.SizeF(529.0001F, 23F);
            // 
            // xrsrLoss
            // 
            this.xrsrLoss.LocationFloat = new DevExpress.Utils.PointFloat(0.999705F, 0F);
            this.xrsrLoss.Name = "xrsrLoss";
            this.xrsrLoss.ReportSource = new Bosco.Report.ReportObject.GeneralateLoss();
            this.xrsrLoss.SizeF = new System.Drawing.SizeF(530F, 23F);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.xrTable1,
            this.xrTable2});
            this.ReportFooter.HeightF = 56.25F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable3
            // 
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(530.97F, 31.25F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable3.SizeF = new System.Drawing.SizeF(529.0298F, 25F);
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrtcNetresultnext});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "Net Result Activities";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 1.8819032173124386D;
            // 
            // xrtcNetresultnext
            // 
            this.xrtcNetresultnext.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrtcNetresultnext.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrtcNetresultnext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrtcNetresultnext.Name = "xrtcNetresultnext";
            this.xrtcNetresultnext.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrtcNetresultnext.StylePriority.UseBorderColor = false;
            this.xrtcNetresultnext.StylePriority.UseBorders = false;
            this.xrtcNetresultnext.StylePriority.UseFont = false;
            this.xrtcNetresultnext.StylePriority.UsePadding = false;
            this.xrtcNetresultnext.StylePriority.UseTextAlignment = false;
            this.xrtcNetresultnext.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrtcNetresultnext.Weight = 1.057781068045297D;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(530.9997F, 4.25F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable1.SizeF = new System.Drawing.SizeF(529.0002F, 25F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrIncomenxtSum});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 3, 3, 3, 100F);
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Total Revenue Income";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 1.8690913322316414D;
            // 
            // xrIncomenxtSum
            // 
            this.xrIncomenxtSum.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrIncomenxtSum.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrIncomenxtSum.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrIncomenxtSum.Name = "xrIncomenxtSum";
            this.xrIncomenxtSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrIncomenxtSum.StylePriority.UseBorderColor = false;
            this.xrIncomenxtSum.StylePriority.UseBorders = false;
            this.xrIncomenxtSum.StylePriority.UseFont = false;
            this.xrIncomenxtSum.StylePriority.UsePadding = false;
            this.xrIncomenxtSum.StylePriority.UseTextAlignment = false;
            this.xrIncomenxtSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrIncomenxtSum.Weight = 1.0506736526595024D;
            // 
            // xrTable2
            // 
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.999697F, 4.25F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(530F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrName,
            this.xrExpencenxtSum});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrName
            // 
            this.xrName.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrName.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrName.Name = "xrName";
            this.xrName.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 3, 3, 100F);
            this.xrName.StylePriority.UseBorderColor = false;
            this.xrName.StylePriority.UseBorders = false;
            this.xrName.StylePriority.UseFont = false;
            this.xrName.StylePriority.UsePadding = false;
            this.xrName.StylePriority.UseTextAlignment = false;
            this.xrName.Text = "Total Revenue Expenditure";
            this.xrName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrName.Weight = 1.8674573757862736D;
            // 
            // xrExpencenxtSum
            // 
            this.xrExpencenxtSum.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrExpencenxtSum.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrExpencenxtSum.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.xrExpencenxtSum.Name = "xrExpencenxtSum";
            this.xrExpencenxtSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 5, 3, 3, 100F);
            this.xrExpencenxtSum.StylePriority.UseBorderColor = false;
            this.xrExpencenxtSum.StylePriority.UseBorders = false;
            this.xrExpencenxtSum.StylePriority.UseFont = false;
            this.xrExpencenxtSum.StylePriority.UsePadding = false;
            this.xrExpencenxtSum.StylePriority.UseTextAlignment = false;
            this.xrExpencenxtSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrExpencenxtSum.Weight = 1.0521634742725181D;
            // 
            // GeneralateProfitandLoss
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.GroupHeader1,
            this.ReportFooter});
            this.DataMember = "ReportSetting";
            this.DataSource = this.reportSetting1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(48, 53, 61, 0);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.Version = "13.2";
            this.Controls.SetChildIndex(this.ReportFooter, 0);
            this.Controls.SetChildIndex(this.GroupHeader1, 0);
            this.Controls.SetChildIndex(this.Detail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.xrTableHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRTable xrTableHeader;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell tcExpenditure;
        private DevExpress.XtraReports.UI.XRTableCell tcExpenditureYearTo;
        private DevExpress.XtraReports.UI.XRTableCell tcIncome;
        private DevExpress.XtraReports.UI.XRTableCell tcIncomeYearTo;
        private ReportSetting reportSetting1;
        private DevExpress.XtraReports.UI.XRSubreport xrsrincome;
        private DevExpress.XtraReports.UI.XRSubreport xrsrLoss;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrName;
        private DevExpress.XtraReports.UI.XRTableCell xrExpencenxtSum;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrIncomenxtSum;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrtcNetresultnext;
    }
}
