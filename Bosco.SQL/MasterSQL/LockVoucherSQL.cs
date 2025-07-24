using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class LockVoucherSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.LockVoucher).FullName)
            {
                query = GetLockVoucherSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Lock Voucher details.
        /// </summary>
        /// <returns></returns>
        private string GetLockVoucherSQL()
        {
            string query = "";
            SQLCommand.LockVoucher sqlCommandId = (SQLCommand.LockVoucher)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.LockVoucher.Add:
                    {
                        query = "INSERT INTO MASTER_LOCK_TRANS ( " +
                               "BRANCH_ID, " +
                               "PROJECT_ID, " +
                               "DATE_FROM," +
                               "DATE_TO," +
                               "PASSWORD," +
                               "REASON," +
                               "PASSWORD_HINT," +
                               "LOCK_TYPE," +
                               "LOCK_BY_PORTAL ) VALUES( " +
                               "?BRANCH_ID, " +
                               "?PROJECT_ID, " +
                               "?DATE_FROM," +
                               "?DATE_TO," +
                               "?PASSWORD," +
                               "?REASON," +
                               "?PASSWORD_HINT," +
                               "?LOCK_TYPE," +
                               "?LOCK_BY_PORTAL)";
                        break;
                    }
                case SQLCommand.LockVoucher.Update:
                    {
                        query = "UPDATE MASTER_LOCK_TRANS SET " +
                                    "BRANCH_ID = ?BRANCH_ID, " +
                                    "PROJECT_ID =?PROJECT_ID, " +
                                    "DATE_FROM=?DATE_FROM, " +
                                    "DATE_TO=?DATE_TO, " +
                                    "PASSWORD=?PASSWORD," +
                                    "REASON=?REASON," +
                                    "PASSWORD_HINT=?PASSWORD_HINT, " +
                                    "LOCK_BY_PORTAL=?LOCK_BY_PORTAL " +
                                    "WHERE LOCK_TRANS_ID=?LOCK_TRANS_ID ";
                        break;
                    }
                case SQLCommand.LockVoucher.Delete:
                    {
                        query = "DELETE FROM MASTER_LOCK_TRANS WHERE LOCK_TRANS_ID =?LOCK_TRANS_ID";
                        break;
                    }
                case SQLCommand.LockVoucher.FetchBranchByProject:
                    {
                        query = "SELECT * FROM BRANCH_OFFICE BO INNER JOIN MASTER_LOCK_TRANS MLT " +
                                  " ON BO.BRANCH_OFFICE_ID = MLT.BRANCH_ID";
                        break;
                    }
                case SQLCommand.LockVoucher.FetchLockVoucher:
                    {
                        query = "SELECT MLT.LOCK_TRANS_ID,BO.BRANCH_OFFICE_NAME AS BranchOffice,\n" +
                        "       MP.PROJECT,\n" +
                        "       DATE_FORMAT(DATE_FROM,'%d/%m/%Y') AS DateFrom,\n" +
                        "       DATE_FORMAT(DATE_TO,'%d/%m/%Y') AS DateTo,\n" +
                        "       IF(LOCK_BY_PORTAL = 1, 'YES', 'NO') AS Locked\n" +
                        "  FROM MASTER_LOCK_TRANS MLT\n" +
                        " INNER JOIN BRANCH_OFFICE BO\n" +
                        "    ON MLT.BRANCH_ID = BO.BRANCH_OFFICE_ID\n" +
                        " INNER JOIN MASTER_PROJECT MP\n" +
                        "    ON MP.PROJECT_ID = MLT.PROJECT_ID;";
                        break;
                    }
                case SQLCommand.LockVoucher.FetchLockVoucherById:
                    {
                        query = " SELECT LOCK_TRANS_ID,\n" +
                             "BRANCH_ID, PROJECT_ID, DATE_FROM, DATE_TO, PASSWORD,\n" +
                             "REASON, PASSWORD_HINT, LOCK_BY_PORTAL\n" +
                             "FROM MASTER_LOCK_TRANS\n" +
                             "WHERE LOCK_TRANS_ID =?LOCK_TRANS_ID";
                        break;
                    }
                case SQLCommand.LockVoucher.FetchBranchLockVoucherGraceDays:
                    {
                        query = @"SELECT BGD.BRANCH_ID, BGD.LOCATION_ID, BO.BRANCH_OFFICE_NAME, BL.LOCATION_NAME,
                                BGD.ENFORCE_GRACE_DAYS, BGD.GRACE_DAYS, BGD.GRACE_TMP_DATE_FROM, GRACE_TMP_DATE_TO, GRACE_TMP_VALID_UPTO
                                FROM master_branch_voucher_grace_days BGD
                                LEFT JOIN BRANCH_OFFICE BO ON BO.BRANCH_OFFICE_ID = BGD.BRANCH_ID
                                LEFT JOIN BRANCH_LOCATION BL ON (BL.BRANCH_ID = BGD.BRANCH_ID OR BL.BRANCH_ID=0) AND BL.LOCATION_ID = BGD.LOCATION_ID";
                        break;
                    }
                case SQLCommand.LockVoucher.FetchBranchLockVoucherGraceDaysByBranchLocation:
                    {
                        query = @"SELECT BGD.BRANCH_ID, BGD.LOCATION_ID, BO.BRANCH_OFFICE_NAME, BL.LOCATION_NAME,
                                BGD.ENFORCE_GRACE_DAYS, BGD.GRACE_DAYS, BGD.GRACE_TMP_DATE_FROM, GRACE_TMP_DATE_TO, GRACE_TMP_VALID_UPTO
                                FROM master_branch_voucher_grace_days BGD
                                LEFT JOIN BRANCH_OFFICE BO ON BO.BRANCH_OFFICE_ID = BGD.BRANCH_ID
                                LEFT JOIN BRANCH_LOCATION BL ON (BL.BRANCH_ID = BGD.BRANCH_ID OR BL.BRANCH_ID=0) AND BL.LOCATION_ID = BGD.LOCATION_ID
                                WHERE BGD.BRANCH_ID = ?BRANCH_ID AND BGD.LOCATION_ID = ?LOCATION_ID";
                        break;
                    }
            }
            return query;
        }
        #endregion LockVoucher SQL
    }
}
