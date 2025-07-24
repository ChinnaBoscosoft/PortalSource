using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;
using Bosco.Utility;

namespace Bosco.SQL
{
    public class LicenseSQL : IDatabaseQuery
    {
        #region ISQLServerQuery Members

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;

        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.License).FullName)
            {
                query = GetLicenseSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the license details.
        /// </summary>
        /// <returns></returns>
        private string GetLicenseSQL()
        {
            string query = "";
            SQLCommand.License sqlCommandId = (SQLCommand.License)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.License.Add:
                    {
                        query = "INSERT INTO BRANCH_LICENSE(BRANCH_ID,LICENSE_KEY_NUMBER, " +
                               "LICENSE_QUANTITY, LICENSE_COST, YEAR_FROM, YEAR_TO, " +
                               "INSTITUTE_NAME, IS_MULTILOCATION, APPROVE_BUDGET_BY_PORTAL, APPROVE_BUDGET_BY_EXCEL, IS_TWO_MONTH_BUDGET,AUTOMATIC_BACKUP_PORTAL," +
                               "ENABLE_PORTAL, KEY_GENERATED_DATE, MODULE_ITEM, CRISTO_PARISH_CODE, THIRDPARTY_MODE,THIRDPARTY_URL, LOGIN_URL, " +
                               "USER_ID, " +
                               "BRANCH_KEY_CODE,IS_LICENSE_MODEL,ACCESS_MULTI_DB,LOCK_MASTER,MAP_LEDGER,ALLOW_MULTI_CURRENCY,ATTACH_VOUCHERS_FILES, ENABLE_REPORTS) " +
                               "VALUES(?BRANCH_ID, ?LICENSE_KEY_NUMBER, ?LICENSE_QUANTITY, " +
                               "?LICENSE_COST, ?YEAR_FROM, ?YEAR_TO, " +
                               "?INSTITUTE_NAME, ?IS_MULTILOCATION, ?APPROVE_BUDGET_BY_PORTAL, ?APPROVE_BUDGET_BY_EXCEL, ?IS_TWO_MONTH_BUDGET,?AUTOMATIC_BACKUP_PORTAL, " +
                               "?ENABLE_PORTAL, ?KEY_GENERATED_DATE, ?MODULE_ITEM, ?CRISTO_PARISH_CODE, ?THIRDPARTY_MODE,?THIRDPARTY_URL, " +
                               "?LOGIN_URL, ?USER_ID,?BRANCH_KEY_CODE,?IS_LICENSE_MODEL,?ACCESS_MULTI_DB,?LOCK_MASTER,?MAP_LEDGER,?ALLOW_MULTI_CURRENCY,?ATTACH_VOUCHERS_FILES,?ENABLE_REPORTS)";
                        break;
                    }
                case SQLCommand.License.Update:
                    {
                        query = "UPDATE BRANCH_LICENSE SET " +
                               "LICENSE_QUANTITY=?LICENSE_QUANTITY, LICENSE_COST=?LICENSE_COST, YEAR_FROM=?YEAR_FROM, YEAR_TO=?YEAR_TO, " +
                               "INSTITUTE_NAME=?INSTITUTE_NAME, IS_MULTILOCATION=?IS_MULTILOCATION, APPROVE_BUDGET_BY_PORTAL =?APPROVE_BUDGET_BY_PORTAL, APPROVE_BUDGET_BY_EXCEL =?APPROVE_BUDGET_BY_EXCEL," +
                               "IS_TWO_MONTH_BUDGET =?IS_TWO_MONTH_BUDGET, AUTOMATIC_BACKUP_PORTAL=?AUTOMATIC_BACKUP_PORTAL, " +
                               "ENABLE_PORTAL=?ENABLE_PORTAL, KEY_GENERATED_DATE=?KEY_GENERATED_DATE, MODULE_ITEM=?MODULE_ITEM, ENABLE_REPORTS=?ENABLE_REPORTS " +
                               "LOGIN_URL=?LOGIN_URL,LOCK_MASTER=?LOCK_MASTER,MAP_LEDGER=?MAP_LEDGER,ALLOW_MULTI_CURRENCY=?ALLOW_MULTI_CURRENCY, ATTACH_VOUCHERS_FILES=?ATTACH_VOUCHERS_FILES WHERE LICENSE_KEY_NUMBER=?LICENSE_KEY_NUMBER";
                        break;
                    }
                case SQLCommand.License.Delete:
                    {
                        query = "DELETE FROM BRANCH_LICENSE WHERE LICNESE_KEY_NUMBER = ?LICNESE_KEY_NUMBER";
                        break;
                    }
                case SQLCommand.License.NewBranchUniqueCodeFetch:
                    {
                        query = "SELECT CAST(CONCAT(?MATCH_VALUE, LPAD(IFNULL(MAX(SUBSTRING(BRANCH_KEY_CODE,?MATCH_LENGTH)+1),1),?RUNNING_NUMBER,'0')) AS CHAR) " +
                                "FROM BRANCH_OFFICE WHERE BRANCH_KEY_CODE LIKE ?LIKE_VALUE";

                        break;
                    }
                case SQLCommand.License.NewLicenseIdentificationNumberFetch:
                    {
                        query = "SELECT CAST(CONCAT(?MATCH_VALUE, LPAD(IFNULL(MAX(SUBSTRING(LICENSE_KEY_NUMBER,?MATCH_LENGTH)+1),1),?RUNNING_NUMBER,'0')) AS CHAR) " +
                                "FROM BRANCH_LICENSE WHERE LICENSE_KEY_NUMBER LIKE ?LIKE_VALUE";

                        break;
                    }
                case SQLCommand.License.NewLCBranchEnableRequestIdentificationNumberFetch:
                    {
                        query = "SELECT CAST(CONCAT(?MATCH_VALUE, LPAD(IFNULL(MAX(SUBSTRING(LC_BRANCH_REQUEST_CODE,?MATCH_LENGTH)+1),1),?RUNNING_NUMBER,'0')) AS CHAR) " +
                                "FROM LC_BRANCH_ENABLE_TRACK_MODULES WHERE LC_BRANCH_REQUEST_CODE LIKE ?LIKE_VALUE AND LC_BRANCH_OFFICE_CODE=?LC_BRANCH_OFFICE_CODE";

                        break;
                    }
                case SQLCommand.License.IsLicenseNoExist:
                    {
                        query = "SELECT  " +
                                    "COUNT(*) " +
                                    "FROM BRANCH_LICENSE " +
                                "WHERE LICENSE_KEY_NUMBER=?LICENSE__KEY_NUMBER";
                        break;
                    }
                case SQLCommand.License.LicenseDetailsByLicenseIdFetch:
                    {
                        query = "SELECT HO.HEAD_OFFICE_NAME,BO.HEAD_OFFICE_CODE,BO.BRANCH_OFFICE_CODE,BO.BRANCH_OFFICE_NAME, " +
                                "CASE WHEN BO.DEPLOYMENT_TYPE=0 THEN 'Standalone' ELSE 'Client/Server' END AS DEPLOYMENT_TYPE, " +
                                "BO.ADDRESS,C.COUNTRY,S.STATE,BO.PINCODE, " +
                                "BO.PHONE_NO,CONCAT(BO.COUNTRY_CODE,'',BO.MOBILE_NO) AS MOBILE_NO, " +
                                "BO.BRANCH_EMAIL_ID,BO.CITY AS PLACE,LC.LICENSE_KEY_NUMBER,CAST(LC.LICENSE_QUANTITY AS CHAR) AS LICENSE_QUANTITY, " +
                                "CAST(LC.LICENSE_COST AS CHAR) AS LICENSE_COST,CAST(LC.YEAR_FROM AS CHAR) AS YEAR_FROM, " +
                                "CAST(LC.YEAR_TO AS CHAR) AS YEAR_TO,LC.INSTITUTE_NAME AS InstituteName,'' AS SocietyName,  " +
                                "CAST(LC.IS_MULTILOCATION AS CHAR) AS IS_MULTILOCATION,CAST(LC.ENABLE_PORTAL AS CHAR) AS ENABLE_PORTAL, " +
                                "CAST(LC.KEY_GENERATED_DATE AS CHAR) AS KEY_GENERATED_DATE, " +
                                "LC.MODULE_ITEM,LC.ENABLE_REPORTS, LC.LOGIN_URL, " +
                                "LC.BRANCH_KEY_CODE,CAST(LC.IS_LICENSE_MODEL AS CHAR) AS IS_LICENSE_MODEL, " +
                                "CAST(LC.ACCESS_MULTI_DB AS CHAR) AS AccessToMultiDB, CAST(LC.APPROVE_BUDGET_BY_PORTAL AS CHAR) AS APPROVE_BUDGET_BY_PORTAL, CAST(LC.APPROVE_BUDGET_BY_EXCEL AS CHAR) AS APPROVE_BUDGET_BY_EXCEL," +
                                "CAST(LC.IS_TWO_MONTH_BUDGET AS CHAR) AS IS_TWO_MONTH_BUDGET,CAST(LC.AUTOMATIC_BACKUP_PORTAL AS CHAR) AS AUTOMATIC_BACKUP_PORTAL, " +
                                "CAST(LC.LOCK_MASTER AS CHAR) AS LOCK_MASTER, CAST(LC.MAP_LEDGER AS CHAR) AS MAP_LEDGER  " +
                                "FROM BRANCH_LICENSE LC " +
                                "INNER JOIN BRANCH_OFFICE BO " +
                                "ON LC.BRANCH_ID=BO.BRANCH_OFFICE_ID " +
                                "INNER JOIN HEAD_OFFICE HO " +
                                "ON HO.HEAD_OFFICE_CODE=BO.HEAD_OFFICE_CODE " +
                                "INNER JOIN COUNTRY C " +
                                "ON C.COUNTRY_ID=BO.COUNTRY_ID " +
                                "INNER JOIN STATE S " +
                                "ON S.STATE_ID=BO.STATE_ID " +
                                "WHERE LC.LICENSE_ID=?LICENSE_ID";
                        break;
                    }
                case SQLCommand.License.LicenseDetailsByBranchCodeFetch:
                    {
                        query = "SELECT HO.HEAD_OFFICE_NAME,IF(BO.IS_SUBBRANCH=1,BO.ASSOCIATE_BRANCH_CODE, " +
                                "BO.HEAD_OFFICE_CODE) AS   HEAD_OFFICE_CODE,BO.BRANCH_OFFICE_CODE,BO.BRANCH_OFFICE_NAME, " +
                                "CASE WHEN BO.DEPLOYMENT_TYPE=0 THEN 'Standalone' ELSE 'ClientServer' END AS DEPLOYMENT_TYPE, " +
                                "BO.ADDRESS,C.COUNTRY,S.STATE,BO.PINCODE,CRISTO_PARISH_CODE,BO.THIRDPARTY_CODE,LC.THIRDPARTY_MODE,LC.THIRDPARTY_URL, " +
                                "BO.PHONE_NO  AS PHONE,CONCAT(BO.COUNTRY_CODE,'',BO.MOBILE_NO) AS MOBILE_NO, " +
                                "BO.BRANCH_EMAIL_ID  AS EMAIL,BO.CITY AS PLACE,LC.LICENSE_KEY_NUMBER,CAST(LC.LICENSE_QUANTITY AS CHAR) AS NoOfNodes, " +
                                "CAST(LC.LICENSE_COST AS CHAR) AS LICENSE_COST,CAST(LC.YEAR_FROM AS CHAR) AS YEAR_FROM, " +
                                "CAST(LC.YEAR_TO AS CHAR) AS YEAR_TO,LC.INSTITUTE_NAME AS InstituteName,'' AS SocietyName, " +
                                "CAST(LC.IS_MULTILOCATION AS CHAR) AS IS_MULTILOCATION,CAST(LC.ENABLE_PORTAL AS CHAR) AS ENABLE_PORTAL, " +
                                "CAST(LC.KEY_GENERATED_DATE AS CHAR) AS KEY_GENERATED_DATE, " +
                                "LC.MODULE_ITEM AS NoOfModules,LC.ENABLE_REPORTS, LC.LOGIN_URL AS URL, " +
                                "LC.BRANCH_KEY_CODE,CAST(LC.IS_LICENSE_MODEL AS CHAR) AS IS_LICENSE_MODEL, " +
                                "CAST(LC.ACCESS_MULTI_DB AS CHAR) AS AccessToMultiDB, " +
                                "CAST(LC.APPROVE_BUDGET_BY_PORTAL AS CHAR) AS APPROVE_BUDGET_BY_PORTAL, " +
                                "CAST(LC.APPROVE_BUDGET_BY_EXCEL AS CHAR) AS APPROVE_BUDGET_BY_EXCEL, " +
                                "CAST(LC.IS_TWO_MONTH_BUDGET AS CHAR) AS IS_TWO_MONTH_BUDGET, " +
                                "CAST(LC.AUTOMATIC_BACKUP_PORTAL AS CHAR) AS AUTOMATIC_BACKUP_PORTAL, " +
                                "'' AS FAX,'Primary' AS LOCATION, " +
                                "'' AS CONTACTPERSON, CAST(LC.LOCK_MASTER AS CHAR) AS LOCK_MASTER, CAST(LC.MAP_LEDGER AS CHAR) AS MAP_LEDGER, CAST(LC.ALLOW_MULTI_CURRENCY AS CHAR) AS ALLOW_MULTI_CURRENCY, CAST(LC.ATTACH_VOUCHERS_FILES AS CHAR) AS ATTACH_VOUCHERS_FILES " +
                                " FROM BRANCH_LICENSE LC " +
                                "INNER JOIN BRANCH_OFFICE BO " +
                                "ON LC.BRANCH_ID=BO.BRANCH_OFFICE_ID " +
                                "INNER JOIN HEAD_OFFICE HO " +
                                "ON HO.HEAD_OFFICE_CODE=BO.HEAD_OFFICE_CODE " +
                                "INNER JOIN COUNTRY C " +
                                "ON C.COUNTRY_ID=BO.COUNTRY_ID " +
                                "INNER JOIN STATE S " +
                                "ON S.STATE_ID=BO.STATE_ID " +
                                "WHERE BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE ORDER BY LC.LICENSE_ID DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.License.LicenseDetailsByBranchIdFetch:
                    {
                        query = "SELECT HO.HEAD_OFFICE_NAME,BO.HEAD_OFFICE_CODE,BO.BRANCH_OFFICE_CODE,BO.BRANCH_OFFICE_NAME, " +
                                "CASE WHEN BO.DEPLOYMENT_TYPE=0 THEN 'Standalone' ELSE 'Client/Server' END AS DEPLOYMENT_TYPE, BO.THIRDPARTY_CODE, " +
                                "BO.ADDRESS,C.COUNTRY,S.STATE,BO.PINCODE,CRISTO_PARISH_CODE,LC.THIRDPARTY_MODE, LC.THIRDPARTY_URL, " +
                                "BO.PHONE_NO AS PHONE,CONCAT(BO.COUNTRY_CODE,'',BO.MOBILE_NO) AS MOBILE_NO, " +
                                "BO.BRANCH_EMAIL_ID,BO.CITY AS PLACE,LC.LICENSE_KEY_NUMBER,CAST(LC.LICENSE_QUANTITY AS CHAR) AS LICENSE_QUANTITY, " +
                                "CAST(LC.LICENSE_COST AS CHAR) AS LICENSE_COST,CAST(LC.YEAR_FROM AS CHAR) AS YEAR_FROM, " +
                                "CAST(LC.YEAR_TO AS CHAR) AS YEAR_TO,LC.INSTITUTE_NAME AS InstituteName,'' AS SocietyName,  " +
                                "CAST(LC.IS_MULTILOCATION AS CHAR) AS IS_MULTILOCATION,CAST(LC.ENABLE_PORTAL AS CHAR) AS ENABLE_PORTAL, " +
                                "CAST(LC.KEY_GENERATED_DATE AS CHAR) AS KEY_GENERATED_DATE, " +
                                "LC.MODULE_ITEM,LC.ENABLE_REPORTS,LC.LOGIN_URL, " +
                                 "LC.BRANCH_KEY_CODE,CAST(LC.IS_LICENSE_MODEL AS CHAR) AS IS_LICENSE_MODEL, " +
                                "CAST(LC.ACCESS_MULTI_DB AS CHAR) AS AccessToMultiDB,CAST(LC.LOCK_MASTER AS CHAR) AS LOCK_MASTER, CAST(LC.MAP_LEDGER AS CHAR) AS MAP_LEDGER, CAST(LC.ALLOW_MULTI_CURRENCY AS CHAR) AS ALLOW_MULTI_CURRENCY,CAST(LC.ATTACH_VOUCHERS_FILES AS CHAR) AS ATTACH_VOUCHERS_FILES, YEAR_TO AS YEAR_TO, " +
                                "CAST(LC.APPROVE_BUDGET_BY_PORTAL AS CHAR) AS APPROVE_BUDGET_BY_PORTAL,\n" +
                                "CAST(LC.APPROVE_BUDGET_BY_EXCEL AS CHAR) AS APPROVE_BUDGET_BY_EXCEL,\n" +
                                "CAST(LC.IS_TWO_MONTH_BUDGET AS CHAR) AS IS_TWO_MONTH_BUDGET, \n" +
                                "CAST(LC.AUTOMATIC_BACKUP_PORTAL AS CHAR) AS AUTOMATIC_BACKUP_PORTAL \n" +
                                "FROM BRANCH_LICENSE LC " +
                                "INNER JOIN BRANCH_OFFICE BO " +
                                "ON LC.BRANCH_ID=BO.BRANCH_OFFICE_ID " +
                                "INNER JOIN HEAD_OFFICE HO " +
                                "ON HO.HEAD_OFFICE_CODE=BO.HEAD_OFFICE_CODE " +
                                "INNER JOIN COUNTRY C " +
                                "ON C.COUNTRY_ID=BO.COUNTRY_ID " +
                                "INNER JOIN STATE S " +
                                "ON S.STATE_ID=BO.STATE_ID " +
                                "WHERE BO.BRANCH_OFFICE_ID=?BRANCH_OFFICE_ID ORDER BY LC.LICENSE_ID DESC LIMIT 1";
                        break;
                    }
                case SQLCommand.License.FetchLCBranchClientEnableModuleRequests:
                    {
                        query = "SELECT BLCD.LC_BRANCH_REQUEST_CODE, BLCD.LC_BRANCH_LICENSE_KEY_NUMBER, BLCD.LC_HEAD_OFFICE_CODE, BLCD.LC_BRANCH_OFFICE_CODE, \n" +
                                "IFNULL(BO.BRANCH_OFFICE_NAME, '') AS LC_BRANCH_OFFICE_NAME, BLCD.LC_BRANCH_LOCATION,\n" +
                                "BLCD.LC_BRANCH_CLIENT_IP_ADDRESS, BLCD.LC_BRANCH_CLIENT_MAC_ADDRESS, \n" +
                                "IFNULL(BLCD.LC_BRANCH_RECEIPT_MODULE_STATUS, 0 ) AS LC_BRANCH_RECEIPT_MODULE_STATUS, \n" +
                                "(CASE WHEN BLCD.LC_BRANCH_RECEIPT_MODULE_STATUS=1 THEN '" + LCBranchModuleStatus.Requested.ToString() + "'\n" +
                                "WHEN BLCD.LC_BRANCH_RECEIPT_MODULE_STATUS=2 THEN '" + LCBranchModuleStatus.Approved.ToString() + "'\n" +
                                "ELSE '" + LCBranchModuleStatus.Disabled.ToString() + "'\n" +
                                "END) AS LC_BRANCH_RECEIPT_MODULE_STATUS_NAME,\n" +
                                "BLCD.LC_BRANCH_REQUESTED_ON, BLCD.LC_BRANCH_REQUESTED_BY, BLCD.PORTAL_UPDATED_ON, BLCD.PORTAL_UPDATED_BY,\n" +
                                "BO.DEPLOYMENT_TYPE,\n" +
                                "(CASE WHEN BO.DEPLOYMENT_TYPE =0 THEN '" + DeploymentType.Standalone.ToString() + "'\n" +
                                " ELSE '" + DeploymentType.ClientServer.ToString() + "' END) AS DEPLOYMENT_TYPE_NAME\n" +
                                "FROM LC_BRANCH_ENABLE_TRACK_MODULES BLCD\n" +
                                "LEFT JOIN BRANCH_OFFICE BO ON BO.BRANCH_OFFICE_CODE = BLCD.LC_BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.License.FetchLCBranchClientEnableModuleRequestsByBranch:
                    {
                        query = "SELECT BLCD.LC_BRANCH_REQUEST_CODE, BLCD.LC_BRANCH_LICENSE_KEY_NUMBER, BLCD.LC_HEAD_OFFICE_CODE,  \n" +
                                "BLCD.LC_BRANCH_OFFICE_CODE, BLCD.LC_BRANCH_LOCATION,\n" +
                                "BLCD.LC_BRANCH_CLIENT_IP_ADDRESS, BLCD.LC_BRANCH_CLIENT_MAC_ADDRESS, BLCD.LC_BRANCH_RECEIPT_MODULE_STATUS,\n" +
                                "BLCD.LC_BRANCH_REQUESTED_ON, BLCD.LC_BRANCH_REQUESTED_BY, BLCD.PORTAL_UPDATED_ON, BLCD.PORTAL_UPDATED_BY\n" +
                                "FROM LC_BRANCH_ENABLE_TRACK_MODULES BLCD\n" +
                                "WHERE BLCD.LC_HEAD_OFFICE_CODE=?LC_HEAD_OFFICE_CODE AND BLCD.LC_BRANCH_OFFICE_CODE=?LC_BRANCH_OFFICE_CODE AND BLCD.LC_BRANCH_LOCATION=?LC_BRANCH_LOCATION";
                        break;
                    }
                case SQLCommand.License.IsExistsLCBranchClientEnableModuleRequestsByBranchRequestCode:
                    {
                        query = "SELECT BLCD.LC_BRANCH_REQUEST_CODE FROM LC_BRANCH_ENABLE_TRACK_MODULES BLCD\n" +
                                "WHERE LC_BRANCH_REQUEST_CODE=?LC_BRANCH_REQUEST_CODE AND BLCD.LC_HEAD_OFFICE_CODE=?LC_HEAD_OFFICE_CODE AND \n" +
                                "BLCD.LC_BRANCH_OFFICE_CODE=?LC_BRANCH_OFFICE_CODE AND BLCD.LC_BRANCH_LOCATION=?LC_BRANCH_LOCATION";
                        break;
                    }
                case SQLCommand.License.RequestLCBranchClientEnableModuleRequests:
                    {
                        query = "INSERT INTO LC_BRANCH_ENABLE_TRACK_MODULES\n" +
                                "(LC_BRANCH_REQUEST_CODE, LC_BRANCH_LICENSE_KEY_NUMBER, LC_BRANCH_OFFICE_CODE, LC_HEAD_OFFICE_CODE,\n" +
                                "LC_BRANCH_LOCATION, LC_BRANCH_CLIENT_IP_ADDRESS, LC_BRANCH_CLIENT_MAC_ADDRESS,\n" +
                                "LC_BRANCH_REQUESTED_ON, LC_BRANCH_REQUESTED_BY, PORTAL_UPDATED_ON, PORTAL_UPDATED_BY, LC_BRANCH_RECEIPT_MODULE_STATUS, REMARKS)\n" +
                                "VALUES (?LC_BRANCH_REQUEST_CODE, ?LC_BRANCH_LICENSE_KEY_NUMBER, ?LC_BRANCH_OFFICE_CODE, ?LC_HEAD_OFFICE_CODE,\n" +
                                "?LC_BRANCH_LOCATION, ?LC_BRANCH_CLIENT_IP_ADDRESS, ?LC_BRANCH_CLIENT_MAC_ADDRESS,\n" +
                                "?LC_BRANCH_REQUESTED_ON, ?LC_BRANCH_REQUESTED_BY, ?PORTAL_UPDATED_ON, ?PORTAL_UPDATED_BY, " + (Int32)LCBranchModuleStatus.Requested + ", ?REMARKS)";
                        break;
                    }
                case SQLCommand.License.UpdateLCBranchReceiptModuleStatus:
                    {
                        query = "UPDATE LC_BRANCH_ENABLE_TRACK_MODULES\n" +
                                "SET LC_BRANCH_RECEIPT_MODULE_STATUS=?LC_BRANCH_RECEIPT_MODULE_STATUS\n" +
                                "WHERE LC_BRANCH_REQUEST_CODE= ?LC_BRANCH_REQUEST_CODE AND LC_HEAD_OFFICE_CODE=?LC_HEAD_OFFICE_CODE AND \n" +
                                "LC_BRANCH_OFFICE_CODE=?LC_BRANCH_OFFICE_CODE AND LC_BRANCH_LOCATION=?LC_BRANCH_LOCATION";
                        break;
                    }
                case SQLCommand.License.DeleteAllLCBranchRequests:
                    {
                        query = "DELETE FROM LC_BRANCH_ENABLE_TRACK_MODULES";
                        break;
                    }
                case SQLCommand.License.DeleteLCBranchRequestsByBranch:
                    {
                        query = "DELETE FROM LC_BRANCH_ENABLE_TRACK_MODULES WHERE LC_HEAD_OFFICE_CODE=?LC_HEAD_OFFICE_CODE AND LC_BRANCH_OFFICE_CODE=?LC_BRANCH_OFFICE_CODE";
                        break;
                    }
            }
            return query;
        }
        #endregion License SQL
    }
}
