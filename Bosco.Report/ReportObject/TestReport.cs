using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Bosco.Report.ReportObject
{
    public partial class TestReport : Bosco.Report.Base.ReportHeaderBase
    {
        public TestReport()
        {
            InitializeComponent();
            
            this.ReportTitle = "Monthly Abstract Payments for the duration or Month of";
            this.ReportSubTitle = "Consolidated Statement";    
         
            
        }


    }
}
