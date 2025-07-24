/*  Class Name      : ArgumentCollection.cs
 *  Purpose         : Use it for passing typed parameter as collection
 *  Author          : CS
 *  Created on      : 10-Jul-2010
 */

using System;
using System.Collections.Generic;

namespace Bosco.Utility
{
    public class ArgumentCollection<T>
    {
        private List<T> dataItem = new List<T>();

        public void Add(T argument)
        {
            dataItem.Add(argument);
        }

        public T this[int index]
        {
            get { return dataItem[index]; }
        }

        public T GetItem(int index)
        {
            return dataItem[index];
        }

        public void Clear()
        {
            dataItem.Clear();
        }

        public int Count
        {
            get { return dataItem.Count; }
        }

        #region IEnumerable Members

        public IEnumerator<T> GetEnumerator()
        {
            return dataItem.GetEnumerator();
        }
        #endregion
    }
}
