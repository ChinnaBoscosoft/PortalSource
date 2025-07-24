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
    #region String Set

    public class StringSetMember
    {
        public int GetStringItemCount(string stringList, string delimiter)
        {
            int length = 0;
            string[] aList = stringList.Split(delimiter.ToCharArray());
            if (aList.Length > 0) { length = aList.Length; }
            return length;
        }

        public string AddToStringList(string stringList, string addItem, string delimiter)
        {
            int listCount = 0;
            return AddToStringList(stringList, addItem, delimiter, out listCount);
        }

        public string AddToStringList(string stringList, string addItem, string delimiter, out int listCount)
        {
            string retValue = stringList;
            string[] aStringList = stringList.Split(delimiter.ToCharArray());

            listCount = 0;
            if (stringList != string.Empty) { listCount = aStringList.Length; }
            if (listCount < 0) { listCount = 0; }

            bool isFound = HasItemInStringList(stringList, addItem, delimiter);

            if (!isFound)
            {
                retValue += ((retValue != "") ? delimiter : "") + addItem;
                listCount++;
            }

            return retValue;
        }

        public string RemoveFromStringList(string stringList, string removeItem, string delimiter)
        {
            int listCount = 0;
            return RemoveFromStringList(stringList, removeItem, delimiter, out listCount);
        }

        public string RemoveFromStringList(string stringList, string removeItem, string delimiter, out int listCount)
        {
            string retValue = "";
            string[] aStringList = stringList.Split(delimiter.ToCharArray());
            listCount = 0;

            foreach (string item in aStringList)
            {
                if (item != removeItem)
                {
                    retValue += ((retValue != "") ? delimiter : "") + item;
                    listCount++;
                }
            }

            return retValue;
        }

        public bool HasItemInStringList(string stringList, string searchItem, string delimiter)
        {
            stringList = delimiter + stringList + delimiter;
            bool isFound = stringList.Contains(delimiter + searchItem + delimiter);
            return isFound;
        }

        public string JoinString(object[] value, string delimiter)
        {
            string val = "";

            foreach (object item in value)
            {
                if (item != null && item.ToString() != "")
                {
                    val += ((val != "") ? delimiter + " " : "") + item.ToString();
                }
            }

            return val;
        }

        /// <summary>
        /// purpose: to get valid string for Data view row filter
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>

        public string RowFilterText(string searchString)
        {
            string filterString = RowFilterText(searchString, true);
            return filterString;
        }

        /// <summary>
        /// purpose: to get valid string for Data view row filter
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="isNoLikeFilter"></param>
        /// <returns></returns>
        public string RowFilterText(string searchString, bool isLikeFilter)
        {
            string filterString = searchString;
            string replaceChar = "";

            if (isLikeFilter)
            {
                for (int i = 0; i < filterString.Length; i++)
                {
                    if (filterString[i] == '[' || filterString[i] == ']')
                    {
                        replaceChar += ((filterString[i] == '[') ? "[[]" : "[]]");
                    }
                    else
                    {
                        replaceChar += filterString[i];
                    }
                }
                filterString = replaceChar;
                filterString = filterString.Replace("*", "[*]");
                filterString = filterString.Replace("%", "[%]");
            }
            filterString = filterString.Replace("'", "''");

            return filterString;
        }
        public string ToSentenceCase(string Name)
        {
            Name = Name.Trim();
            if (!string.IsNullOrEmpty(Name.Trim()))
            {
                string FirstLetter = Name.Substring(0, 1).ToUpper();
                Name = Name.Remove(0, 1);
                Name = FirstLetter + Name;
            }
            return Name;
        }
        public string ToTitleCase(string Name)
        {
            return String.IsNullOrEmpty(Name) ? string.Empty : System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Name);
        }
    }

    #endregion
}
