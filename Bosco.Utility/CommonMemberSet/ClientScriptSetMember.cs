/*  Class Name      : CommonMember.cs
 *  Purpose         : Reusable member functions accessible to inherited class
 *  Author          : CS
 *  Created on      : 13-Jul-2010
 */

using System;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using Bosco.Utility;

namespace Bosco.Utility.CommonMemberSet
{
    #region Script Set

    public class ClientScriptSetMember
    {
        public static string GetQueryString(string url, string key, string val)
        {
            string retUrl = url + ((url.IndexOf("?") >= 0) ? "&" : "?") + key + "=" + val;
            return retUrl;
        }

        public static string GetOpenWindowScript(string urlPage, string id, int height, int width, int left, int top, bool returnVal, bool showModel)
        {
            string url = urlPage + ((urlPage.IndexOf("?") >= 0) ? "&" : "?") + "id=" + id;
            string script = "";
            if (showModel)
            {
                script = "javascript:openwindow('" + url + "', '" + height + "', '" + width + "', '" + left + "', '" + top + "');return " + returnVal.ToString().ToLower() + ";";
            }
            else
            {
                script = "javascript:openwindowModeless('" + url + "', '" + height + "', '" + width + "', '" + left + "', '" + top + "');return " + returnVal.ToString().ToLower() + ";";
            }
            return script;
        }

        public static String InvokePopupCal(System.Web.UI.WebControls.TextBox Field)
        {
            //Define character array to trim from language strings
            char[] TrimChars = { (char)',', (char)' ' };

            // Get culture array of month names and convert to string for
            // passing to the popup calendar
            String MonthNameString = "";

            foreach (String Month in DateTimeFormatInfo.CurrentInfo.MonthNames)
            {
                MonthNameString += Month + ",";
            }

            MonthNameString = MonthNameString.TrimEnd(TrimChars);

            // Get culture array of day names and convert to string for
            // passing to the popup calendar
            String DayNameString = "";

            foreach (String Day in DateTimeFormatInfo.CurrentInfo.DayNames)
            {
                DayNameString += Day.Substring(0, 3) + ",";
            }

            DayNameString = DayNameString.TrimEnd(TrimChars);

            // Get the short date pattern for the culture
            String FormatString = DateFormatInfo.DateFormat;

            return "javascript:popupCal('Cal','" + Field.ClientID + "','" + FormatString + "','" + MonthNameString + "','" + DayNameString + "')";
        }
    }

    #endregion
}
