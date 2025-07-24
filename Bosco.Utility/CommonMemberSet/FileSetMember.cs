/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;

namespace Bosco.Utility.CommonMemberSet
{
    #region General Set

    public class FileSetMember
    {
        public ResultArgs CreateDirectory(string path, string directoryName)
        {
            ResultArgs resultArgs = new ResultArgs();
            resultArgs.Success = true;

            string createPath = path + directoryName;

            if (Directory.Exists(path))
            {
                if (!Directory.Exists(createPath))
                {
                    try
                    {
                        Directory.CreateDirectory(createPath);
                    }
                    catch (Exception err)
                    {
                        resultArgs.Exception = err;
                    }
                }
            }

            return resultArgs;
        }
    }

    #endregion
}
