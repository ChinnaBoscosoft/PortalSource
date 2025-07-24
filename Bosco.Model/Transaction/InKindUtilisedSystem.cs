using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using System.Data;

namespace Bosco.Model.Transaction
{
    public class InKindUtilisedSystem : SystemBase
    {
        #region Constructor
        public InKindUtilisedSystem()
        {

        }
        public InKindUtilisedSystem(int InkindId)
        {

        }
        #endregion

        #region Properties
        public int ProjectId { get; set; }
        public int InKindArticleId { get; set; }
        public int PurposeId { get; set; }
        public int DonorId { get; set; }
        public int InKindTransId { get; set; }
        public DateTime InKindTransDate { get; set; }
        public int SequenceNo { get; set; }
        public int LedgerId { get; set; }
        public int InKindQuantity { get; set; }
        public int InKindValue { get; set; }
        public int InKindUnit { get; set; }
        public int ReceivedInformation { get; set; }
        public string BankAccountNo { get; set; }
        public string ChequeNo { get; set; }
        public string Narration { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        #endregion

        #region Methods

        #endregion

        #region Private Methods

        #endregion

    }
}
