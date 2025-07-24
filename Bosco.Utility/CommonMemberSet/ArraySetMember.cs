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
    #region Array Set

    public class ArraySetMember
    {
        public bool HasArrayValue(string[] arrayList, string findValue)
        {
            bool found = false;

            found = Array.Exists<string>(arrayList, new Predicate<string>(delegate(string arrayItem)
                    {
                        return arrayItem == findValue;
                    }));

            return found;
        }
    }

    #endregion
}
