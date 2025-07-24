using System;
using System.Collections.Generic;
using System.Text;

namespace Bosco.Utility
{
    public class ExceptionLogger : ExceptionHandler
    {
        public ExceptionLogger()
        {
            ExceptionHandler.ExceptionLog = this;
        }

        public override void WirteLog(ExceptionHandler exceptionHandler)
        {
            //base.WirteLog(exceptionHandler);
        }
    }
}
