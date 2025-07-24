/* Class        : DatabaseHandler.cs
 * Purpose      : Reads the list of data object from app.config in App.DataAccess collection 
 *                and get the active data object assembly name
 *
 * Author       : CS
 * Created on   : 25-Jun-2010
 */

using System;
using System.Collections.Generic;
using System.Xml;

namespace Bosco.DAO.Configuration
{
    public class DatabaseHandler : System.Configuration.IConfigurationSectionHandler
    {
        Dictionary<string, string> dbItems = new Dictionary<string, string>();

        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            foreach (XmlNode node in section.ChildNodes)
            {
                dbItems[node.Attributes["id"].Value] = node.Attributes["type"].Value;
            }
            return this;
        }

        #endregion

        //Reads the list of data object from collection and get the active data object assembly name
        public string GetDatabaseItem(string dbType)
        {
            string db = "";
            if (dbItems.ContainsKey(dbType)) { db = dbItems[dbType]; }
            return db;
        }
    }
}
