/* Class    : SQLParser.cs
 *          : Parse and eliminate optional parameter from SQL statement
 * Created  : 23-Jul-2010
 * Author   : CS
 */

using System;
using System.Text.RegularExpressions;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace Bosco.DAO
{
    public class SQLParser : CommonMember
    {
        DataParameters dataParameters;
        string sqlStatement = "";
        string parameterDelimiter = "";

        public string ParseQuery(string sqlStatement, DataParameters dataParameters, string parameterDelimiter)
        {
            string resultSQL = "";

            this.sqlStatement = sqlStatement;
            this.dataParameters = dataParameters;
            this.parameterDelimiter = parameterDelimiter;
            
            resultSQL = ParseOptionalParameter();
            return resultSQL;
        }

        private string ParseOptionalParameter()
        {
            string resultSQL = sqlStatement;
            //pattern = @"\{[^\}]*?\s*?([\}]+?)";
            string pattern = @"\{[^\}]*([\}])";

            Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchEvaluator replaceCallback = new MatchEvaluator(MatchHandler);
            resultSQL = regEx.Replace(sqlStatement, replaceCallback);
            return resultSQL;
        }

        private string MatchHandler(Match stringMatch)
        {
            string parseString = "";
            string findString = "";
            string stringVal = "";

            //Parsing Query
            if (dataParameters != null)
            {
                foreach (IDataArguments itemValue in dataParameters)
                {
                    findString = itemValue.FieldName;

                    if (findString != "")
                    {
                        findString = parameterDelimiter + findString;
                        stringVal = stringMatch.Value;

                        if (stringVal.Contains(findString))
                        {
                            parseString = stringVal.Replace("{", "").Replace("}", "");
                        }
                    }
                }
            }

            return parseString;
        }
    }
}
