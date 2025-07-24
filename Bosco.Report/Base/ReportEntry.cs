/*  Class Name      : ReportBase.cs
 *  Purpose         : Interface between Report viewer and Report Interface class to get report source
 *  Author          : CS
 *  Created on      : 21-Jul-2009
 */

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Bosco.Report;
using Bosco.Utility;
using Bosco.Model.UIModel;

namespace Bosco.Report.Base
{
    public class ReportEntry : IReport
    {
        private Form mdiParent = null;
        private CommonMember utilityMember = null;
        private ReportProperty objReportProperty = new ReportProperty();

        public ReportEntry()
        {

        }

        public ReportEntry(Form mdiParent)
        {
            this.mdiParent = mdiParent;
        }

        #region IReport Members

        public void ShowReport()
        {
          //  frmReportGallery fReportGallery = new frmReportGallery();

            if (this.mdiParent != null)
            {
              //  fReportGallery.MdiParent = this.mdiParent;
            }

           // fReportGallery.Show();
        }



        public void VoucherPrint(string VoucherId, string VoucherType, string ProjectName)
        {
            ReportBase report = null;
            try
            {
               
                using (LegalEntitySystem legalEntitySystem = new LegalEntitySystem())
                {
                        ResultArgs resultArgs= legalEntitySystem.FetchLegalEntity();
                        ReportProperty.dtLedgerEntity = resultArgs.DataSource.Table;
                }

                objReportProperty.ReportId = VoucherType;
                string reportAssemblyType = objReportProperty.ReportAssembly;
                report = UtilityMember.GetDynamicInstance(reportAssemblyType, null) as ReportBase;
                report.ReportId = VoucherType;
                objReportProperty.PrintCashBankVoucherId = VoucherId;
                objReportProperty.ProjectTitle = ProjectName;
                report.VoucherPrint();


            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
            finally { }
        }
        #endregion

        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

    }
}
