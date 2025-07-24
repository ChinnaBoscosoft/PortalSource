using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;
using Bosco.Utility;
using System.Runtime.InteropServices;

namespace Bosco.Model.UIModel.Software
{
   public class ClearLogSystem : SystemBase
   {
       ResultArgs resultArgs = null;

       #region Constructor

       public ClearLogSystem()
       {                  
       }

       #endregion

       #region Methods

       public ResultArgs DeleteLog(int clrstatus)
       {
           using (DataManager dataManager = new DataManager(SQLCommand.Software.DeleteLog,DataBaseType.Portal))
           {
               dataManager.Parameters.Add(this.AppSchema.Software.STATUSColumn, clrstatus);
               dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
               resultArgs = dataManager.UpdateData();
           }
           return resultArgs;
       }

       #endregion
   }
}
