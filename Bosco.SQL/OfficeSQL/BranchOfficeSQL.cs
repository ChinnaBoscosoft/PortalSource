using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bosco.DAO.Data;
using Bosco.DAO;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class BranchOfficeSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.BranchOffice).FullName)
            {
                query = GetBranchOfficeSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        /// <summary>
        /// Purpose:To Perform the action of the Branch Office details.
        /// </summary>
        /// <returns></returns>
        private string GetBranchOfficeSQL()
        {
            string query = "";
            SQLCommand.BranchOffice sqlCommandId = (SQLCommand.BranchOffice)(this.dataCommandArgs.SQLCommandId);

            switch (sqlCommandId)
            {
                case SQLCommand.BranchOffice.Add:
                    {
                        query = "INSERT INTO BRANCH_OFFICE ( " +
                               "BRANCH_OFFICE_CODE, " +
                               "BRANCH_OFFICE_NAME, " +
                               "HEAD_OFFICE_CODE, " +
                               "CREATED_DATE," +
                               "CREATED_BY," +
                               "DEPLOYMENT_TYPE," +
                               "ADDRESS," +
                               "COUNTRY_ID," +
                               "PINCODE," +
                               "STATE_ID," +
                               "PHONE_NO," +
                               "MOBILE_NO," +
                               "BRANCH_EMAIL_ID," +
                               "BRANCH_PART_CODE," +
                               "THIRDPARTY_CODE," +
                               "STATUS," +
                               "MODIFIED_DATE," +
                               "MODIFIED_BY," +
                               "USER_CREATED_STATUS," +
                               "CITY, " +
                               "COUNTRY_CODE,BRANCH_KEY_CODE,IS_SUBBRANCH,ASSOCIATE_BRANCH_CODE,INCHARGE_NAME " +
                               " ) VALUES( " +
                               "?BRANCH_OFFICE_CODE, " +
                               "?BRANCH_OFFICE_NAME, " +
                               "?HEAD_OFFICE_CODE, " +
                               "?CREATED_DATE," +
                               "?CREATED_BY," +
                               "?DEPLOYMENT_TYPE," +
                               "?ADDRESS," +
                               "?COUNTRY_ID," +
                               "?PINCODE," +
                               "?STATE_ID," +
                               "?PHONE_NO," +
                               "?MOBILE_NO," +
                               "?BRANCH_EMAIL_ID," +
                               "?BRANCH_PART_CODE," +
                               "?THIRDPARTY_CODE," +
                               "?STATUS," +
                               "?MODIFIED_DATE," +
                               "?MODIFIED_BY," +
                               "?USER_CREATED_STATUS," +
                               "?CITY," +
                               "?COUNTRY_CODE," +
                               "?BRANCH_KEY_CODE," +
                               "?IS_SUBBRANCH," +
                               "?ASSOCIATE_BRANCH_CODE," +
                               "?INCHARGE_NAME" +
                               " )";
                        break;
                    }

                case SQLCommand.BranchOffice.Update:
                    {
                        query = "UPDATE BRANCH_OFFICE SET " +
                               "BRANCH_OFFICE_NAME=?BRANCH_OFFICE_NAME, " +
                               "HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE, " +
                               "BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE, " +
                               "DEPLOYMENT_TYPE=?DEPLOYMENT_TYPE," +
                               "ADDRESS=?ADDRESS," +
                               "COUNTRY_ID=?COUNTRY_ID," +
                               "STATE_ID=?STATE_ID," +
                               "ADDRESS=?ADDRESS," +
                               "PINCODE=?PINCODE," +
                               "PHONE_NO=?PHONE_NO," +
                               "MOBILE_NO=?MOBILE_NO," +
                               "COUNTRY_CODE=?COUNTRY_CODE," +
                               "BRANCH_EMAIL_ID=?BRANCH_EMAIL_ID," +
                               "BRANCH_PART_CODE=?BRANCH_PART_CODE," +
                               "THIRDPARTY_CODE=?THIRDPARTY_CODE," +
                               "MODIFIED_DATE=?MODIFIED_DATE," +
                               "MODIFIED_BY=?MODIFIED_BY,INCHARGE_NAME=?INCHARGE_NAME," +
                               "CITY=?CITY" +
                               " WHERE  BRANCH_OFFICE_CODE =?CODE";
                        break;
                    }

                case SQLCommand.BranchOffice.UpdateUserInfo:
                    {
                        query = "UPDATE USER_INFO " +
                                "SET " +
                                        "FIRSTNAME=?FIRSTNAME," +
                                        "ADDRESS=?ADDRESS," +
                                        "CONTACT_NO=?CONTACT_NO," +
                                        "CITY=?CITY," +
                                        "EMAIL_ID=?EMAIL_ID," +
                                        "BRANCH_CODE=?BRANCH_CODE " +
                                        " WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.BranchOffice.Delete:
                    {
                        query = "DELETE FROM  BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?CODE";
                        break;
                    }

                case SQLCommand.BranchOffice.DeleteLicenseByBranch:
                    {
                        query = "DELETE FROM BRANCH_LICENSE WHERE BRANCH_ID= ?BRANCH_OFFICE_ID;";
                        break;
                    }

                case SQLCommand.BranchOffice.Fetch:
                    {
                        query = @"SELECT BRANCH_PART_CODE ,THIRDPARTY_CODE, STATUS ,BRANCH_OFFICE_CODE,BRANCH_OFFICE_NAME,
                                  HEAD_OFFICE_CODE,DEPLOYMENT_TYPE,ADDRESS,COUNTRY_ID,STATE_ID,PINCODE,PHONE_NO,
                                  MOBILE_NO,COUNTRY_CODE,BRANCH_EMAIL_ID,CITY,USER_CREATED_STATUS,
                                   BRANCH_KEY_CODE,INCHARGE_NAME FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchByBranchCode:
                    {
                        query = @"SELECT BRANCH_PART_CODE , BRANCH_OFFICE_CODE,BRANCH_OFFICE_NAME,HEAD_OFFICE_CODE,DEPLOYMENT_TYPE,ADDRESS,COUNTRY_ID,STATE_ID,PINCODE,PHONE_NO,
                                  MOBILE_NO,COUNTRY_CODE,BRANCH_EMAIL_ID,CITY,USER_CREATED_STATUS,IS_SUBBRANCH FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE AND STATUS=2";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchByBranchCodeAvailable:
                    {
                        query = @"SELECT BRANCH_PART_CODE , BRANCH_OFFICE_CODE,BRANCH_OFFICE_NAME,HEAD_OFFICE_CODE,DEPLOYMENT_TYPE,ADDRESS,COUNTRY_ID,STATE_ID,PINCODE,PHONE_NO,
                                  MOBILE_NO,COUNTRY_CODE,BRANCH_EMAIL_ID,CITY,USER_CREATED_STATUS FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchAll:
                    {
                        //TRIM(BOTH ',' FROM concat_ws(',',address, Country,state,Pincode)) as Address, // and bo.STATUS=2
                        query = @"SELECT BRANCH_OFFICE_ID,Head_office_code as 'H.O Code',BRANCH_PART_CODE AS 'Code',BRANCH_office_name as 'Name',
                                     Case When Deployment_Type=0 then 'Standalone' else 'Client-Server' end as 'System Type',
                                CONCAT(cast(STATUS as char),',',BRANCH_OFFICE_CODE,',',HEAD_OFFICE_CODE,',',cast(IS_SUBBRANCH as char)) AS Status,
                                     case when Status=1 then 'Created' else case when status=2 then 'Activated' else 'DeActivated' end end as 'Office Status',
                                     IF(IS_SUBBRANCH=1,'Yes','No') AS 'Sub Branch?' from
                                     BRANCH_OFFICE BO
                                     INNER JOIN country c on c.country_id=BO.country_id
                                     INNER JOIN state s on s.state_id=BO.state_id { where BO.HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE }{ and BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE 
                                    OR ( ASSOCIATE_BRANCH_CODE=?BRANCH_OFFICE_CODE AND IS_SUBBRANCH=1) }
                                     group by BRANCH_office_id order by status ASC ,BRANCH_office_code asc";
                        break;
                    }

                case SQLCommand.BranchOffice.FetchAllBranchesByUser:
                    {
                        //TRIM(BOTH ',' FROM concat_ws(',',address, Country,state,Pincode)) as Address, // and bo.STATUS=2 // BRANCH_PART_CODE AS 'Code' chinna at 27.10.2022
                        query = @"SELECT BRANCH_OFFICE_ID,Head_office_code as 'H.O Code',BRANCH_PART_CODE AS 'Code',BRANCH_office_name as 'Name',  
                                     Case When Deployment_Type=0 then 'Standalone' else 'Client-Server' end as 'System Type',
                                CONCAT(cast(STATUS as char),',',BRANCH_OFFICE_CODE,',',HEAD_OFFICE_CODE,',',cast(IS_SUBBRANCH as char)) AS Status,
                                     case when Status=1 then 'Created' else case when status=2 then 'Activated' else 'DeActivated' end end as 'Office Status',
                                     IF(IS_SUBBRANCH=1,'Yes','No') AS 'Sub Branch?' from
                                     BRANCH_OFFICE BO
                                     INNER JOIN country c on c.country_id=BO.country_id
                                     INNER JOIN state s on s.state_id=BO.state_id INNER JOIN BRANCH_USER BU ON BU.BRANCH_ID = BO.BRANCH_OFFICE_ID AND BU.USER_ID =?USER_ID { where BO.HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE }{ and BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE 
                                    OR ( ASSOCIATE_BRANCH_CODE=?BRANCH_OFFICE_CODE AND IS_SUBBRANCH=1) }
                                     group by BRANCH_office_id order by status ASC ,BRANCH_office_code asc";
                        break;
                    }

                case SQLCommand.BranchOffice.BranchByProject:
                    {
                        query = "SELECT MLT.LOCK_TRANS_ID,\n" +
                                    "       MLT.BRANCH_ID,\n" +
                                    "       MLT.PROJECT_ID,\n" +
                                    "       MLT.DATE_FROM,\n" +
                                    "       MLT.DATE_TO,\n" +
                                    "       MLT.PASSWORD,\n" +
                                    "       MLT.REASON,\n" +
                                    "       MLT.PASSWORD_HINT,\n" +
                                    "       MLT.LOCK_BY_PORTAL,\n" +
                                    "       MLT.LOCK_TYPE,\n" +
                                    "       MP.PROJECT\n" +
                                    "  FROM BRANCH_OFFICE BO\n" +
                                    " INNER JOIN MASTER_LOCK_TRANS MLT\n" +
                                    "    ON BO.BRANCH_OFFICE_ID = MLT.BRANCH_ID\n" +
                                    " INNER JOIN MASTER_PROJECT MP\n" +
                                    "    ON MP.PROJECT_ID = MLT.PROJECT_ID\n" +
                                    " INNER JOIN BRANCH_OFFICE B\n" +
                                    "    ON B.BRANCH_OFFICE_ID = MLT.BRANCH_ID\n" +
                                    " WHERE B.BRANCH_OFFICE_CODE =?BRANCH_OFFICE_CODE\n" +
                                    "   AND B.HEAD_OFFICE_CODE =?HEAD_OFFICE_CODE;";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchActiveBranchs:
                    {
                        query = @" SELECT BRANCH_OFFICE_ID,HEAD_OFFICE_CODE,
                                                            BRANCH_OFFICE_CODE AS 'Branch Code',
                                                            BRANCH_office_name as 'Branch Name',
                                                            Case When Deployment_Type=0 then 'Standalone' else 'Client-Server' end as 'System Type',
                                                             trim(BOTH ',' FROM concat_ws(',',address, Country,state,Pincode)) as Address
                                                            FROM BRANCH_OFFICE BO
                                                            INNER JOIN country c on c.country_id=BO.country_id
                                                            INNER JOIN state s on s.state_id=BO.state_id
                                                            WHERE STATUS=2 AND IS_SUBBRANCH<>1
                                                            { AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE } { AND BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE }
                                                            GROUP BY BRANCH_office_id order by status ASC ,BRANCH_office_code ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchforKeyDownload:
                    {
                        query = @" SELECT BRANCH_OFFICE_ID,HEAD_OFFICE_CODE,L.LICENSE_ID,
                                    BRANCH_OFFICE_CODE AS 'Branch Code',
                                    BRANCH_office_name as 'Branch Name',
                                    Case When Deployment_Type=0 then 'Standalone' else 'Client-Server' end as 'System Type',
                                    trim(BOTH ',' FROM concat_ws(',',address,City, state,Country,Pincode)) as Address
                                    FROM BRANCH_OFFICE BO
                                    INNER JOIN country c on c.country_id=BO.country_id
                                    INNER JOIN state s on s.state_id=BO.state_id
                                    INNER JOIN BRANCH_LICENSE AS L ON L.BRANCH_ID=BO.BRANCH_OFFICE_ID
                                    WHERE STATUS=2
                                    { AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE } { AND BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE OR BO.ASSOCIATE_BRANCH_CODE=?BRANCH_OFFICE_CODE }
                                    GROUP BY BRANCH_office_id order by status ASC ,BRANCH_office_code ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchforKeyDownloadByUser:
                    {
                        query = @" SELECT BRANCH_OFFICE_ID,HEAD_OFFICE_CODE,L.LICENSE_ID,
                                    BRANCH_OFFICE_CODE AS 'Branch Code',
                                    BRANCH_office_name as 'Branch Name',
                                    Case When Deployment_Type=0 then 'Standalone' else 'Client-Server' end as 'System Type',
                                    trim(BOTH ',' FROM concat_ws(',',address,City, state,Country,Pincode)) as Address
                                    FROM BRANCH_OFFICE BO
                                    INNER JOIN country c on c.country_id=BO.country_id
                                    INNER JOIN state s on s.state_id=BO.state_id
                                    INNER JOIN BRANCH_LICENSE AS L ON L.BRANCH_ID=BO.BRANCH_OFFICE_ID
                                    WHERE STATUS=2
                                    AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE AND BO.BRANCH_OFFICE_CODE IN (?BRANCH_OFFICE_CODE)
                                    GROUP BY BRANCH_office_id order by status ASC ,BRANCH_office_code ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchforKeyDownloadByUserId:
                    {
                        //                        query = @" SELECT GROUP_CONCAT('''',BRANCH_OFFICE_CODE,'''') AS BRANCH_OFFICE_CODE  FROM BRANCH_OFFICE BO
                        //                                                          INNER JOIN BRANCH_USER BU ON BU.BRANCH_ID = BO.BRANCH_OFFICE_ID WHERE USER_ID = ?USER_ID";

                        //                        query = @" SELECT BRANCH_OFFICE_CODE AS BRANCH_OFFICE_CODE  FROM BRANCH_OFFICE BO
                        //                                 INNER JOIN BRANCH_USER BU ON BU.BRANCH_ID = BO.BRANCH_OFFICE_ID WHERE USER_ID = ?USER_ID";

                        query = @" SELECT GROUP_CONCAT(BRANCH_OFFICE_CODE) AS BRANCH_OFFICE_CODE  FROM BRANCH_OFFICE BO
                           INNER JOIN BRANCH_USER BU ON BU.BRANCH_ID = BO.BRANCH_OFFICE_ID WHERE USER_ID = ?USER_ID";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchforMasterDownload:
                    {
                        query = @" SELECT BRANCH_OFFICE_ID,HEAD_OFFICE_CODE,
                                  BRANCH_OFFICE_CODE AS 'Branch Code',
                                    BRANCH_office_name as 'Branch Name',
                                    Case When Deployment_Type=0 then 'Standalone' else 'Client-Server' end as 'System Type',
                                    TRIM(BOTH ',' FROM concat_ws(',',address,City, state,Country,Pincode)) as Address
                                    FROM BRANCH_OFFICE BO
                                    INNER JOIN country c on c.country_id=BO.country_id
                                    INNER JOIN state s on s.state_id=BO.state_id
                                    INNER JOIN
                                    (SELECT MP.PROJECT_ID,
                                            MP.PROJECT_CATEGORY_ID,PB.BRANCH_ID
                                            FROM MASTER_PROJECT AS MP
                                            INNER JOIN PROJECT_BRANCH AS PB
                                            ON PB.PROJECT_ID=MP.PROJECT_ID
                                            INNER JOIN PROJECT_CATEGORY_LEDGER AS PCL
                                            ON PCL.PROJECT_CATEGORY_ID=MP.PROJECT_CATEGORY_ID
                                            GROUP BY PB.BRANCH_ID) AS T
                                    ON T.BRANCH_ID=BO.BRANCH_OFFICE_ID
                                    WHERE STATUS=2 AND IS_SUBBRANCH<>1
                                    { AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE } { AND BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE }
                                    GROUP BY BRANCH_office_id order by status,BRANCH_office_code ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchforMasterDownloadByUser:
                    {
                        query = @" SELECT BRANCH_OFFICE_ID,HEAD_OFFICE_CODE,
                                  BRANCH_OFFICE_CODE AS 'Branch Code',
                                    BRANCH_office_name as 'Branch Name',
                                    Case When Deployment_Type=0 then 'Standalone' else 'Client-Server' end as 'System Type',
                                    TRIM(BOTH ',' FROM concat_ws(',',address,City, state,Country,Pincode)) as Address
                                    FROM BRANCH_OFFICE BO
                                    INNER JOIN country c on c.country_id=BO.country_id
                                    INNER JOIN state s on s.state_id=BO.state_id
                                    INNER JOIN
                                    (SELECT MP.PROJECT_ID,
                                            MP.PROJECT_CATEGORY_ID,PB.BRANCH_ID
                                            FROM MASTER_PROJECT AS MP
                                            INNER JOIN PROJECT_BRANCH AS PB
                                            ON PB.PROJECT_ID=MP.PROJECT_ID
                                            INNER JOIN PROJECT_CATEGORY_LEDGER AS PCL
                                            ON PCL.PROJECT_CATEGORY_ID=MP.PROJECT_CATEGORY_ID
                                            GROUP BY PB.BRANCH_ID) AS T
                                    ON T.BRANCH_ID=BO.BRANCH_OFFICE_ID
                                    INNER JOIN BRANCH_USER BU ON BU.BRANCH_ID = BO.BRANCH_OFFICE_ID AND BU.USER_ID =?USER_ID
                                    WHERE STATUS=2 AND IS_SUBBRANCH<>1
                                    { AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE } { AND BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE }
                                    GROUP BY BRANCH_office_id order by status,BRANCH_office_code ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchAllToExport:
                    {
                        query = "SELECT HO.HEAD_OFFICE_CODE AS 'Office Code',\n" +
                                "       HEAD_OFFICE_NAME    as 'Head office',\n" +
                                "       BRANCH_OFFICE_CODE  AS 'Branch Code',\n" +
                                "\n" +
                                "       BRANCH_OFFICE_NAME AS 'Branch Name',\n" +
                                "       CASE\n" +
                                "         WHEN DEPLOYMENT_TYPE = 0 THEN\n" +
                                "          'Standalone'\n" +
                                "         ELSE\n" +
                                "          'Client-Server'\n" +
                                "       END AS 'Deployment Type',\n" +
                                "       BO.ADDRESS AS 'Address',\n" +
                                "       COUNTRY AS 'Country',\n" +
                                "       STATE AS 'State',\n" +
                                "       BO.PINCODE AS 'Pincode',\n" +
                                "\n" +
                                "       BO.PHONE_NO AS 'Phone No',\n" +
                                "       BO.MOBILE_NO AS 'Mobile No',\n" +
                                "       CASE\n" +
                                "         when BO.Status = 1 then\n" +
                                "          'Created'\n" +
                                "         else\n" +
                                "          case\n" +
                                "            when BO.status = 2 then\n" +
                                "             'Activated'\n" +
                                "            else\n" +
                                "             'DeActivated'\n" +
                                "          end\n" +
                                "       end as 'Office Status'\n" +
                                "  from BRANCH_OFFICE BO\n" +
                                "  inner JOIN country c\n" +
                                "    on c.country_id = BO.country_id and bo.status=2\n" +
                                " INNER JOIN state s\n" +
                                "    on s.state_id = BO.state_id\n" +
                                "  left join head_office ho\n" +
                                "    on ho.head_office_code = bo.head_office_code\n" +
                                "\n" +
                                "       { where BO.HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE }{ and BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE }\n" +
                                " group by BRANCH_office_id\n" +
                                " order by BO.status ASC, BRANCH_office_code asc;";

                        break;
                    }
                case SQLCommand.BranchOffice.Status:
                    {
                        query = @"UPDATE BRANCH_OFFICE SET STATUS=?STATUS WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";

                        break;
                    }
                case SQLCommand.BranchOffice.FetchBOAUser:
                    {
                        query = @"SELECT BRANCH_OFFICE_CODE,BRANCH_PART_CODE,HEAD_OFFICE_CODE,BRANCH_EMAIL_ID,MOBILE_NO FROM 
                                 BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE;";

                        break;
                    }
                case SQLCommand.BranchOffice.FetchAllBranchOffice:
                    {
                        query = @"SELECT BO.BRANCH_OFFICE_CODE,BO.BRANCH_OFFICE_NAME,BO.CREATED_DATE,
                                BO.CREATED_BY,BO.STATUS FROM BRANCH_OFFICE BO INNER JOIN HEAD_OFFICE HO
                                ON HO.HEAD_OFFICE_CODE=BO.HEAD_OFFICE_CODE WHERE BO.STATUS=1";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchAllBranchOfficeByHeadOffice:
                    {
                        query = @"SELECT BO.BRANCH_OFFICE_CODE,BO.BRANCH_OFFICE_NAME,BO.CREATED_DATE,
                                BO.CREATED_BY,BO.STATUS FROM BRANCH_OFFICE BO INNER JOIN HEAD_OFFICE HO
                                ON HO.HEAD_OFFICE_CODE=BO.HEAD_OFFICE_CODE WHERE BO.STATUS=1 { AND BO.HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE } ";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchRenewalBranchOfficeByDays:
                    {
                        //Skip All SDB HOs, Bosco Demo and License generated before 01/04/2017
                        query = @"SELECT BOL.BRANCH_ID, BO.HEAD_OFFICE_CODE, HO.HEAD_OFFICE_NAME, BO.BRANCH_PART_CODE, BO.BRANCH_OFFICE_NAME, BOL.YEAR_FROM, BOL.YEAR_TO
                                   FROM BRANCH_LICENSE BOL
                                   INNER JOIN BRANCH_OFFICE BO ON BO.BRANCH_OFFICE_ID = BOL.BRANCH_ID
                                   INNER JOIN HEAD_OFFICE HO ON HO.HEAD_OFFICE_CODE = BO.HEAD_OFFICE_CODE
                                   INNER JOIN (SELECT BRANCH_ID, MAX(LICENSE_ID) AS LICENSE_ID
                                              FROM BRANCH_LICENSE WHERE IS_LICENSE_MODEL =1 AND KEY_GENERATED_DATE > '2017-04-01 00:00:00' 
                                              GROUP BY BRANCH_ID) BL ON BL.BRANCH_ID = BOL.BRANCH_ID AND BL.LICENSE_ID = BOL.LICENSE_ID
                                   WHERE BO.STATUS= 2 { AND BO.HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE }  
                                    AND BO.HEAD_OFFICE_CODE NOT IN ('boscos', 'sdbinb', 'sdbinc', 'sdbind', 'sdbing', 'sdbinh', 'sdbink', 'sdbinm','sdbmas', 'sdbinp', 'sdbins', 'sdbint','sdbtls')
                                    AND DATEDIFF(YEAR_TO, CURDATE()) <=30 
                                   ORDER BY HO.HEAD_OFFICE_CODE, BO.BRANCH_OFFICE_NAME, YEAR_TO DESC;";

                        //AND YEAR_TO BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 30 DAY) 
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchHistoryMoreThanOneSystemByBranch:
                    {
                        //GROUP BY BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE, LOCATION
                        query = @"SELECT T.HEAD_OFFICE_CODE, T.BRANCH_OFFICE_CODE, T.HEAD_OFFICE_NAME, T.BRANCH_OFFICE_NAME, T.IPs, 
                                    b.HEAD_OFFICE_CODE AS HEAD_OFFICE_CODE1, b.BRANCH_OFFICE_CODE AS BRANCH_OFFICE_CODE1, 
                                    b.LICENSE_KEY_NUMBER, b.LOCATION AS LOCATION_NAME, b.LOGGED_ON, REPLACE(b.REMARKS, ':','-') AS REMARKS 
                                    FROM (SELECT BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE, BRANCH_OFFICE_NAME, HEAD_OFFICE_NAME, LOCATION, COUNT(*) AS IPs
                                    FROM branch_logged_history
                                    WHERE 1=1 {AND HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE}
                                    GROUP BY HEAD_OFFICE_NAME, BRANCH_OFFICE_NAME, LOCATION
                                    HAVING COUNT(*) > 1) AS T
                                    LEFT JOIN branch_logged_history b ON b.HEAD_OFFICE_NAME = T.HEAD_OFFICE_NAME 
                                    AND b.BRANCH_OFFICE_NAME = T.BRANCH_OFFICE_NAME AND b.LOCATION = T.LOCATION";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchHistoryMoreThanOneSystemByBranchLocation:
                    {
                        query = @"SELECT T.HEAD_OFFICE_CODE, T.BRANCH_OFFICE_CODE, T.HEAD_OFFICE_NAME, T.BRANCH_OFFICE_NAME, T.IPs, 
                                    b.HEAD_OFFICE_CODE AS HEAD_OFFICE_CODE1, b.BRANCH_OFFICE_CODE AS BRANCH_OFFICE_CODE1, 
                                    b.LICENSE_KEY_NUMBER, b.LOCATION AS LOCATION_NAME, b.LOGGED_ON, REPLACE(b.REMARKS, ':','-') AS REMARKS 
                                    FROM (SELECT BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE, BRANCH_OFFICE_NAME, HEAD_OFFICE_NAME, COUNT(*) AS IPs
                                    FROM branch_logged_history
                                    WHERE 1=1 {AND HEAD_OFFICE_CODE = ?HEAD_OFFICE_CODE}
                                    GROUP BY BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE) AS T
                                    LEFT JOIN branch_logged_history b ON b.HEAD_OFFICE_CODE = T.HEAD_OFFICE_CODE 
                                    AND b.BRANCH_OFFICE_CODE = T.BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.BranchOffice.UpdateUserStatus:
                    {
                        query = @"UPDATE BRANCH_OFFICE SET USER_CREATED_STATUS=?USER_CREATED_STATUS WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";

                        break;
                    }
                case SQLCommand.BranchOffice.View:
                    {
                        query = "SELECT BRANCH_OFFICE_ID,ifnull(Address,'N/A') AS ADDRESS,ifnull(city,'N/A') AS CITY," +
                                "ifnull(BRANCH_OFFICE_CODE,'N/A') as BRANCH_OFFICE_CODE,ifnull(BRANCH_office_name,'N/A') AS BRANCH_office_name,IFNULL(head_office_CODE,'N/A') AS head_office_CODE," +
                                "DATE_FORMAT(CREATED_DATE,'%b %d %Y') as CREATED_DATE," +
                                "DATE_FORMAT(MODIFIED_DATE,'%b %d %Y') as MODIFIED_DATE," +
                                "Case When ifnull(Deployment_Type,0)=0 then 'Standalone' else 'Client-Server' end as 'Deployment Type'," +
                                "IFNULL(Country,'N/A') AS COUNTRY,IFNULL(state,'N/A') AS STATE," +
                                "case when ifnull(Status,0)=1 then 'Created' else case when ifnull(Status,0)=2 then 'Activated' else 'DeActivated' end end as Status," +
                                " ifnull(mobile_no,'N/A') AS MOBILE_NO,ifnull(PHONE_NO,'N/A') AS PHONE_NO,ifnull(BRANCH_EMAIL_ID,'N/A') AS BRANCH_EMAIL_ID," +
                                "Pincode from BRANCH_OFFICE BO" +
                                " INNER JOIN country c on c.country_id=BO.country_id" +
                                " INNER JOIN state s on s.state_id=BO.state_id where BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranch:
                    {
                        query = "SELECT BRANCH_OFFICE_ID, BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE, BRANCH_EMAIL_ID AS MAIL_ID, REPLACE(BRANCH_EMAIL_ID,',',', ') AS Email,\n" +
                                  "BRANCH_OFFICE_NAME, BRANCH_OFFICE_NAME AS CODE, 0 AS 'SELECT',CONCAT(BRANCH_OFFICE_NAME,'-',SUBSTRING(BRANCH_OFFICE_CODE, -6)) AS BNAME_CODE\n" +
                                  "FROM BRANCH_OFFICE WHERE STATUS=2 AND IS_SUBBRANCH<>1\n" +
                                  "{ AND BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE } ORDER BY BRANCH_OFFICE_NAME ";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchbyLocations:
                    {
                        //query = " SELECT BO.BRANCH_OFFICE_ID, IFNULL(BL.LOCATION_ID, 0) AS LOCATION_ID, BO.BRANCH_OFFICE_NAME as CODE,BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE,BRANCH_OFFICE_NAME AS CODE, bl.location_name,CONCAT(BRANCH_OFFICE_NAME,'-',SUBSTRING(BRANCH_OFFICE_CODE, -6),'-',bl.location_name) BNAME_CODE \n" +
                        //        " FROM BRANCH_OFFICE BO\n" +
                        //        " LEFT JOIN (SELECT pb.BRANCH_ID, pb.LOCATION_ID, bl.location_name\n" +
                        //          " FROM project_branch pb INNER JOIN branch_location bl ON (bl.BRANCH_ID = pb.BRANCH_ID OR bl.BRANCH_ID=0) AND bl.LOCATION_ID = pb.LOCATION_ID\n" +
                        //          " GROUP BY pb.BRANCH_ID, pb.LOCATION_ID) AS bl on bl.BRANCH_ID = BO.BRANCH_OFFICE_ID\n" +
                        //        " WHERE BO.STATUS = 2 AND IS_SUBBRANCH<>1 ORDER BY BO.BRANCH_OFFICE_NAME, bl.location_name";

                        query = @" SELECT
                            BO.BRANCH_OFFICE_ID, 
                            IFNULL(BL.LOCATION_ID, 0) AS LOCATION_ID, 
                            BO.BRANCH_OFFICE_NAME AS CODE,
                            BO.BRANCH_OFFICE_CODE,
                            BO.HEAD_OFFICE_CODE, 
                            BO.BRANCH_OFFICE_NAME AS CODE, 
                            BL.location_name,
                            IF(
                                BC.BranchCount = 1, 
                                CONCAT(BRANCH_OFFICE_NAME, '-', SUBSTRING(BRANCH_OFFICE_CODE, -6), '-', BL.location_name),
                                CONCAT(BRANCH_OFFICE_NAME, '-', SUBSTRING(BRANCH_OFFICE_CODE, -6))
                            ) AS BNAME_CODE
                        FROM BRANCH_OFFICE BO
                        LEFT JOIN (
                           
                            SELECT pb.BRANCH_ID, pb.LOCATION_ID, bl.location_name
                            FROM project_branch pb 
                            INNER JOIN branch_location bl 
                            ON (bl.BRANCH_ID = pb.BRANCH_ID OR bl.BRANCH_ID = 0)
                            AND bl.LOCATION_ID = pb.LOCATION_ID
                            GROUP BY pb.BRANCH_ID, pb.LOCATION_ID, bl.location_name
                        ) AS BL ON BL.BRANCH_ID = BO.BRANCH_OFFICE_ID
                        LEFT JOIN (
                            SELECT LOCATION_ID, COUNT(DISTINCT BRANCH_ID) AS BranchCount
                            FROM project_branch
                            GROUP BY LOCATION_ID
                        ) AS BC ON BL.LOCATION_ID = BC.LOCATION_ID
                        WHERE BO.STATUS = 2 
                        AND BO.IS_SUBBRANCH <> 1 
                        ORDER BY BO.BRANCH_OFFICE_NAME, BL.location_name";

                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchbyGracedays: // IFNULL(BGD.ENFORCE_GRACE_DAYS, 1) AS ENFORCE_GRACE_DAYS
                    {
                        query = @" SELECT BO.BRANCH_OFFICE_ID, IFNULL(BL.LOCATION_ID, 0) AS LOCATION_ID, BO.BRANCH_OFFICE_NAME, bl.location_name,
                                IF(BGD.ENFORCE_GRACE_DAYS=1, 'No', 'Yes') AS ENFORCE_GRACE_DAYS, IFNULL(BGD.GRACE_DAYS, 30) AS GRACE_DAYS, 
                                BGD.GRACE_TMP_DATE_FROM, GRACE_TMP_DATE_TO, GRACE_TMP_VALID_UPTO
                                FROM BRANCH_OFFICE BO
                                LEFT JOIN (SELECT pb.BRANCH_ID, pb.LOCATION_ID, bl.location_name
                                   FROM project_branch pb INNER JOIN branch_location bl ON (bl.BRANCH_ID = pb.BRANCH_ID OR bl.BRANCH_ID=0) AND bl.LOCATION_ID = pb.LOCATION_ID
                                   GROUP BY pb.BRANCH_ID, pb.LOCATION_ID) AS bl on bl.BRANCH_ID = BO.BRANCH_OFFICE_ID
                                LEFT JOIN master_branch_voucher_grace_days BGD on BGD.BRANCH_ID = BO.BRANCH_OFFICE_ID AND BGD.LOCATION_ID = BL.LOCATION_ID
                                WHERE BO.STATUS = 2 ORDER BY BO.BRANCH_OFFICE_NAME, bl.location_name";
                        break;
                    }
                case SQLCommand.BranchOffice.DeleteGraceDays:
                    {
                        query = @" DELETE FROM master_branch_voucher_grace_days";
                        break;
                    }
                case SQLCommand.BranchOffice.InsertGraceDays:
                    {
                        query = "INSERT INTO MASTER_BRANCH_VOUCHER_GRACE_DAYS(BRANCH_ID, LOCATION_ID, ENFORCE_GRACE_DAYS, GRACE_DAYS, GRACE_TMP_DATE_FROM, GRACE_TMP_DATE_TO, GRACE_TMP_VALID_UPTO)\n " +
                                " VALUES (?BRANCH_ID, ?LOCATION_ID, ?ENFORCE_GRACE_DAYS, ?GRACE_DAYS, ?GRACE_TMP_DATE_FROM, ?GRACE_TMP_DATE_TO, ?GRACE_TMP_VALID_UPTO)";

                        break;
                    }

                case SQLCommand.BranchOffice.FetchBranchByUser:
                    {
                        query = "SELECT BRANCH_OFFICE_ID, BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE, BRANCH_EMAIL_ID AS MAIL_ID, REPLACE(BRANCH_EMAIL_ID,',',', ') AS Email,\n" +
                                  "BRANCH_OFFICE_NAME, BRANCH_OFFICE_NAME AS CODE, 0 AS 'SELECT'\n" +
                                  "FROM BRANCH_OFFICE BO INNER JOIN BRANCH_USER BU ON BU.BRANCH_ID = BO.BRANCH_OFFICE_ID WHERE STATUS=2 AND IS_SUBBRANCH<>1 AND BU.USER_ID =?USER_ID\n" +
                                  "{ AND BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE } ORDER BY BRANCH_OFFICE_NAME ";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchByBudget:
                    {
                        query = "SELECT BO.BRANCH_OFFICE_ID, BO.BRANCH_OFFICE_CODE, BO.BRANCH_EMAIL_ID AS MAIL_ID,REPLACE(BO.BRANCH_EMAIL_ID,',',', ') AS Email,\n" +
                            "BO.BRANCH_OFFICE_NAME, BO.BRANCH_OFFICE_NAME AS CODE, 0 AS 'SELECT'\n" +
                            "FROM BRANCH_OFFICE BO\n" +
                            "INNER JOIN BUDGET_MASTER BM On BM.BRANCH_ID = BRANCH_OFFICE_ID\n" +
                            "WHERE BM.IS_ACTIVE =1 AND BO.STATUS=2 AND BO.IS_SUBBRANCH<>1 { AND BO.BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE }\n" +
                            "GROUP BY BO.BRANCH_OFFICE_ID ORDER BY BO.BRANCH_OFFICE_NAME";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchMailByBranch:
                    {
                        query = "SELECT HEAD_OFFICE_CODE, BRANCH_OFFICE_NAME AS BRANCH, BRANCH_EMAIL_ID AS MAIL_ID " +
                               "FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_ID = ?BRANCH_OFFICE_ID;";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchMailByBranchCode:
                    {
                        query = "SELECT HEAD_OFFICE_CODE, BRANCH_OFFICE_NAME AS BRANCH, BRANCH_EMAIL_ID AS MAIL_ID " +
                               "FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE = ?BRANCH_OFFICE_CODE;";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBudgetProject:
                    {
                        query = "SELECT GROUP_CONCAT(MP.PROJECT) AS PROJECT, GROUP_CONCAT(MP.PROJECT_ID) AS PROJECT_ID " +
                                    "FROM BUDGET_PROJECT BP " +
                                    "INNER JOIN MASTER_PROJECT MP ON MP.PROJECT_ID = BP.PROJECT_ID " +
                                    "WHERE BP.BUDGET_ID IN (BUDGET_ID)";
                        break;
                    }
                case SQLCommand.BranchOffice.DeleteMappedBranchtoProjects:
                    {
                        query = "DELETE FROM PROJECT_BRANCH WHERE BRANCH_ID=?BRANCH_OFFICE_ID";
                        break;
                    }
                case SQLCommand.BranchOffice.MapBranchtoProject:
                    {
                        query = "INSERT INTO PROJECT_BRANCH(PROJECT_ID,BRANCH_ID,LOCATION_ID)VALUES(?PROJECT_ID,?BRANCH_OFFICE_ID,?LOCATION_ID)";
                        break;
                    }
                case SQLCommand.BranchOffice.MapProjectToUser:
                    {
                        query = "INSERT INTO PROJECT_USER(PROJECT_ID,USER_ID,USER_TYPE)VALUES(?PROJECT_ID,?USER_ID,?USER_TYPE)";
                        break;
                    }
                case SQLCommand.BranchOffice.MapBranchToUser:
                    {
                        query = "INSERT INTO BRANCH_USER(BRANCH_ID,USER_ID,USER_TYPE)VALUES(?BRANCH_OFFICE_ID,?USER_ID,?USER_TYPE)";
                        break;
                    }
                case SQLCommand.BranchOffice.DeleteMappedUsertoProjects:
                    {
                        query = "DELETE FROM PROJECT_USER WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.BranchOffice.DeleteMappedUsertoBranch:
                    {
                        query = "DELETE FROM BRANCH_USER WHERE USER_ID=?USER_ID";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchProjectbyBranch:
                    {
                        //"       CONCAT(MP.PROJECT, CONCAT(' - ', MD.DIVISION)) AS 'PROJECT',\n" +
                        query = "SELECT MP.PROJECT_ID,\n" +
                                "      MP.PROJECT AS 'PROJECT',PC.PROJECT_CATOGORY_NAME AS PROJECT_CATEGORY,\n" +
                                "       (SELECT ' ') AS TRANS_MODE,\n" +
                                "       IF(PB.BRANCH_ID =?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT',TP.PROJECT_ID AS MAPPED_PROJECT,PB.LOCATION_ID,BL.LOCATION_NAME\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN MASTER_DIVISION AS MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                " LEFT JOIN MASTER_PROJECT_CATOGORY AS PC \n" +
                                "    ON MP.PROJECT_CATEGORY_ID=PC.PROJECT_CATOGORY_ID \n" +
                                "  LEFT JOIN PROJECT_BRANCH PB\n" +
                                "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                                "   AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID\n" +
                                "   LEFT JOIN BRANCH_LOCATION BL\n" +
                                "   ON BL.LOCATION_ID=PB.LOCATION_ID\n" +
                                " LEFT JOIN\n" +
                                " (SELECT PROJECT_ID,BRANCH_ID FROM LEDGER_BALANCE\n" +
                                " WHERE BRANCH_ID=?BRANCH_OFFICE_ID GROUP BY BRANCH_ID,PROJECT_ID ) AS TP\n" +
                                " ON MP.PROJECT_ID=TP.PROJECT_ID\n" +
                                " WHERE MP.DELETE_FLAG <> 1\n" +
                                " ORDER BY MP.PROJECT ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchProjectsforCombo:
                    {
                        query = "SELECT MP.PROJECT_ID,\n" +
                               "       MP.PROJECT AS 'PROJECT'\n" +
                              "      { ,IF(PB.BRANCH_ID =?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT' } \n" +
                               "  FROM MASTER_PROJECT MP\n" +
                               " INNER JOIN MASTER_DIVISION AS MD\n" +
                               "    ON MP.DIVISION_ID = MD.DIVISION_ID \n" +
                               "   INNER JOIN PROJECT_BRANCH PB\n" +
                               "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                               "  { AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID } \n" +
                               " WHERE MP.DELETE_FLAG <> 1\n" +
                               " ORDER BY MP.PROJECT ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBudgetforCombo:
                    {
                        query = "SELECT BM.BUDGET_ID,\n" +
                                "       BM.BUDGET_NAME\n" +
                                "      { ,IF(PB.BRANCH_ID = ?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT' } \n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN MASTER_DIVISION AS MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                " INNER JOIN PROJECT_BRANCH PB\n" +
                                "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                                " INNER JOIN BUDGET_PROJECT BP\n" +
                                "    ON BP.PROJECT_ID = PB.PROJECT_ID\n" +
                                " INNER JOIN BUDGET_MASTER BM\n" +
                                "    ON BM.BUDGET_ID = BP.BUDGET_ID\n" +
                                "  { AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID }\n" +
                                " {  AND PB.PROJECT_ID IN (?PROJECT_IDs) }\n" +
                                " WHERE MP.DELETE_FLAG <> 1 AND BM.DATE_FROM >= ?DATE_FROM AND  BM.DATE_TO <= ?DATE_TO\n" +
                                " GROUP BY BM.BUDGET_ID\n" +
                                " ORDER BY BM.DATE_FROM ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBudgetforCombosdbinm:
                    {
                        query = "SELECT BM.BUDGET_ID,\n" +
                                "       BM.BUDGET_NAME\n" +
                                "      { ,IF(PB.BRANCH_ID = ?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT' } \n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN MASTER_DIVISION AS MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                " INNER JOIN PROJECT_BRANCH PB\n" +
                                "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                                " INNER JOIN BUDGET_PROJECT BP\n" +
                                "    ON BP.PROJECT_ID = PB.PROJECT_ID\n" +
                                " INNER JOIN BUDGET_MASTER BM\n" +
                                "    ON BM.BUDGET_ID = BP.BUDGET_ID\n" +
                                "  { AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID }\n" +
                                "  { AND BM.BRANCH_ID = ?BRANCH_OFFICE_ID }\n" +
                                " {  AND PB.PROJECT_ID IN (?PROJECT_IDs) }\n" +
                                " WHERE MP.DELETE_FLAG <> 1 AND BM.DATE_FROM <=  ?DATE_TO AND  BM.DATE_TO >= ?DATE_FROM \n" +
                                " GROUP BY BM.BUDGET_ID\n" +
                                " ORDER BY BM.DATE_FROM ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBudgetforComboByTwoMonths:
                    {
                        query = "SELECT GROUP_CONCAT(BM.BUDGET_ID ORDER BY BM.DATE_FROM) AS BUDGET_ID, BM.BUDGET_NAME\n" +
                                "  { ,IF(PB.BRANCH_ID = ?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT' } \n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN MASTER_DIVISION AS MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                " INNER JOIN PROJECT_BRANCH PB\n" +
                                "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                                " INNER JOIN BUDGET_PROJECT BP\n" +
                                "    ON BP.PROJECT_ID = PB.PROJECT_ID\n" +
                                " INNER JOIN BUDGET_MASTER BM\n" +
                                "    ON BM.BUDGET_ID = BP.BUDGET_ID\n" +
                                "  { AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID }\n" +
                                " {  AND PB.PROJECT_ID IN (?PROJECT_ID) }\n" +
                                " WHERE MP.DELETE_FLAG <> 1\n" +
                                " GROUP BY BM.BUDGET_NAME HAVING COUNT(BM.BUDGET_ID) =2";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchProjectsForVoucherLock:
                    {
                        query = @"SELECT MP.PROJECT_ID,
                                       MLT.PROJECT_ID,
                                       MP.PROJECT AS 'PROJECT'
                                      {, IF(PB.BRANCH_ID = ?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT' }
                                  FROM MASTER_PROJECT MP
                                 INNER JOIN PROJECT_BRANCH PB
                                    ON MP.PROJECT_ID = PB.PROJECT_ID
                                 INNER JOIN MASTER_DIVISION AS MD
                                    ON MP.DIVISION_ID = MD.DIVISION_ID
                                  LEFT JOIN MASTER_LOCK_TRANS MLT
                                    ON MLT.PROJECT_ID = MP.PROJECT_ID
                                 WHERE { PB.BRANCH_ID = ?BRANCH_OFFICE_ID AND }
                                   MP.DELETE_FLAG <> 1 AND MLT.PROJECT_ID IS NULL
                                 ORDER BY MP.PROJECT ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchProjectsforComboByLoginUser:
                    {
                        query = "SELECT MP.PROJECT_ID,\n" +
                               "       MP.PROJECT AS 'PROJECT'\n" +
                              "      { ,IF(PB.BRANCH_ID =?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT' } \n" +
                               "  FROM MASTER_PROJECT MP\n" +
                               " INNER JOIN MASTER_DIVISION AS MD\n" +
                               "    ON MP.DIVISION_ID = MD.DIVISION_ID \n" +
                               "   INNER JOIN PROJECT_BRANCH PB\n" +
                               "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                               "  { AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID } \n" +
                               "  INNER JOIN PROJECT_USER PU \n" +
                               "  ON MP.PROJECT_ID = PU.PROJECT_ID\n" +
                               " AND PU.USER_ID =?USER_ID\n" +
                               " WHERE MP.DELETE_FLAG <> 1\n" +
                               " ORDER BY MP.PROJECT ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchProjectBudgetComboByLoginUser:
                    {
                        query = "SELECT BM.BUDGET_ID,\n" +
                               "      BM.BUDGET_NAME\n" +
                              "      { ,IF(PB.BRANCH_ID =?BRANCH_OFFICE_ID, 1, 0) AS 'SELECT' } \n" +
                               "  FROM MASTER_PROJECT MP\n" +
                               " INNER JOIN MASTER_DIVISION AS MD\n" +
                               "    ON MP.DIVISION_ID = MD.DIVISION_ID \n" +
                               "   INNER JOIN PROJECT_BRANCH PB\n" +
                               "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                               " INNER JOIN BUDGET_PROJECT BP\n" +
                               "    ON BP.PROJECT_ID = PB.PROJECT_ID\n" +
                               " INNER JOIN BUDGET_MASTER BM\n" +
                               "    ON BM.BUDGET_ID = BP.BUDGET_ID\n" +
                               "  { AND PB.BRANCH_ID = ?BRANCH_OFFICE_ID } \n" +
                                 " { AND PB.PROJECT_ID IN (?PROJECT_ID) }\n" +
                               "  INNER JOIN PROJECT_USER PU \n" +
                               "  ON MP.PROJECT_ID = PU.PROJECT_ID\n" +
                               " AND PU.USER_ID =?USER_ID\n" +
                               " WHERE MP.DELETE_FLAG <> 1 GROUP BY BM.BUDGET_ID\n" +
                               " ORDER BY BM.BUDGET_NAME ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchbyHeadOffice:
                    {
                        query = "SELECT BRANCH_OFFICE_ID,CONCAT(BRANCH_OFFICE_CODE,CONCAT(' - ',BRANCH_OFFICE_NAME)) AS BRANCH_OFFICE_CODE FROM BRANCH_OFFICE WHERE STATUS=2";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchSubBranches:
                    {

                        query = "SELECT BRANCH_OFFICE_CODE,BRANCH_OFFICE_NAME,HEAD_OFFICE_CODE,DEPLOYMENT_TYPE,ADDRESS,PINCODE,PHONE_NO, " +
                                  "MOBILE_NO,BRANCH_EMAIL_ID,STATUS,CITY,BRANCH_PART_CODE,COUNTRY_CODE,BRANCH_KEY_CODE,IS_SUBBRANCH, " +
                                  "ASSOCIATE_BRANCH_CODE,INCHARGE_NAME FROM BRANCH_OFFICE " +
                                  "WHERE IS_SUBBRANCH = 1 " +
                                  "AND STATUS = 2 " +
                                  "AND ASSOCIATE_BRANCH_CODE =?BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchByBranchPartCode:
                    {
                        query = "SELECT HEAD_OFFICE_CODE,BRANCH_OFFICE_CODE,BRANCH_OFFICE_NAME " +
                                      " FROM BRANCH_OFFICE WHERE STATUS=2 AND BRANCH_PART_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchUserId:
                    {
                        query = "SELECT USER_ID FROM user_info where BRANCH_CODE =?BRANCH_OFFICE_CODE ";
                        break;

                    }
                case SQLCommand.BranchOffice.FetchProjectbyHeadOfficeUsers:
                    {
                        query = "SELECT MP.PROJECT_ID,\n" +
                                "       MP.PROJECT AS 'PROJECT',\n" +
                                "       PC.PROJECT_CATOGORY_NAME AS PROJECT_CATEGORY,\n" +
                                "       BO.BRANCH_OFFICE_NAME AS BRANCH,\n" +
                                "       IF(PU.USER_ID = ?USER_ID, 1, 0) AS 'SELECT'\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN MASTER_DIVISION AS MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                "  LEFT JOIN MASTER_PROJECT_CATOGORY AS PC\n" +
                                "    ON MP.PROJECT_CATEGORY_ID = PC.PROJECT_CATOGORY_ID\n" +
                                " LEFT JOIN PROJECT_BRANCH PB\n" +
                                 "   ON MP.PROJECT_ID =PB.PROJECT_ID\n" +
                                  " LEFT JOIN BRANCH_OFFICE BO\n" +
                                  "  ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID\n" +
                                "  LEFT JOIN PROJECT_USER PU\n" +
                                "    ON PU.PROJECT_ID = MP.PROJECT_ID\n" +
                                "   AND PU.USER_ID =?USER_ID\n" +
                                " WHERE MP.DELETE_FLAG <> 1\n" +
                                " ORDER BY MP.PROJECT ASC";

                        break;
                    }
                case SQLCommand.BranchOffice.FetchProjectbyHeadOfficeUsersFilter:
                    {
                        query = "SELECT MP.PROJECT_ID,\n" +
                                "       MP.PROJECT AS 'PROJECT',\n" +
                                "       PC.PROJECT_CATOGORY_NAME AS PROJECT_CATEGORY,\n" +
                                "       BO.BRANCH_OFFICE_NAME AS BRANCH,\n" +
                                "       IF(PU.USER_ID = ?USER_ID, 1, 0) AS 'SELECT'\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN MASTER_DIVISION AS MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                "  LEFT JOIN MASTER_PROJECT_CATOGORY AS PC\n" +
                                "    ON MP.PROJECT_CATEGORY_ID = PC.PROJECT_CATOGORY_ID\n" +
                                " LEFT JOIN PROJECT_BRANCH PB\n" +
                                 "   ON MP.PROJECT_ID =PB.PROJECT_ID\n" +
                                  " LEFT JOIN BRANCH_OFFICE BO\n" +
                                  "  ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID\n" +
                                "  INNER JOIN PROJECT_USER PU\n" +
                                "    ON PU.PROJECT_ID = MP.PROJECT_ID\n" +
                                "   AND PU.USER_ID =?USER_ID\n" +
                                " WHERE MP.DELETE_FLAG <> 1\n" +
                                " ORDER BY MP.PROJECT ASC";

                        break;
                    }

                case SQLCommand.BranchOffice.FetchBranchbyHeadOfficeUsers:
                    {
                        query = "SELECT BO.BRANCH_OFFICE_ID,\n" +
                         "       BO.BRANCH_OFFICE_NAME AS 'BRANCH',\n" +
                         "       IF(PU.USER_ID = ?USER_ID, 1, 0) AS 'SELECT'\n" +
                         "  FROM BRANCH_OFFICE BO\n" +
                         "  LEFT JOIN BRANCH_USER PU\n" +
                         "    ON PU.BRANCH_ID = BO.BRANCH_OFFICE_ID\n" +
                         "   AND PU.USER_ID = ?USER_ID\n" +
                         " WHERE BO.STATUS = 2\n" +
                         " ORDER BY BO.BRANCH_OFFICE_ID ASC;";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchbyHeadOfficeUsersFilter:
                    {
                        query = "SELECT BO.BRANCH_OFFICE_ID,\n" +
                         "       BO.BRANCH_OFFICE_NAME AS 'BRANCH',\n" +
                         "       IF(PU.USER_ID = ?USER_ID, 1, 0) AS 'SELECT'\n" +
                         "  FROM BRANCH_OFFICE BO\n" +
                         "  INNER JOIN BRANCH_USER PU\n" +
                         "    ON PU.BRANCH_ID = BO.BRANCH_OFFICE_ID\n" +
                         "   AND PU.USER_ID = ?USER_ID\n" +
                         " WHERE BO.STATUS = 2\n" +
                         " ORDER BY BO.BRANCH_OFFICE_ID ASC;";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchProjectbyBranchOfficeUsers:
                    {
                        query = "SELECT MP.PROJECT_ID,\n" +
                                "       MP.PROJECT AS 'PROJECT',\n" +
                                "       PC.PROJECT_CATOGORY_NAME AS PROJECT_CATEGORY,\n" +
                                "       BO.BRANCH_OFFICE_NAME AS BRANCH,\n" +
                                "       IF(PU.USER_ID =?USER_ID, 1, 0) AS 'SELECT',\n" +
                                "       PB.LOCATION_ID,\n" +
                                "       BL.LOCATION_NAME\n" +
                                "  FROM MASTER_PROJECT MP\n" +
                                " INNER JOIN MASTER_DIVISION AS MD\n" +
                                "    ON MP.DIVISION_ID = MD.DIVISION_ID\n" +
                                "  LEFT JOIN MASTER_PROJECT_CATOGORY AS PC\n" +
                                "    ON MP.PROJECT_CATEGORY_ID = PC.PROJECT_CATOGORY_ID\n" +
                                " INNER JOIN PROJECT_BRANCH PB\n" +
                                "    ON MP.PROJECT_ID = PB.PROJECT_ID\n" +
                                "   AND PB.BRANCH_ID IN\n" +
                                "       (SELECT BRANCH_OFFICE_ID\n" +
                                "          FROM BRANCH_OFFICE\n" +
                                "         WHERE BRANCH_OFFICE_CODE =?BRANCH_OFFICE_CODE)\n" +
                                "  LEFT JOIN BRANCH_LOCATION BL\n" +
                                "    ON BL.LOCATION_ID = PB.LOCATION_ID\n" +
                                "  LEFT JOIN BRANCH_OFFICE BO\n" +
                                 "   ON BO.BRANCH_OFFICE_ID = PB.BRANCH_ID\n" +
                                "  LEFT JOIN PROJECT_USER PU\n" +
                                "    ON PU.PROJECT_ID = MP.PROJECT_ID\n" +
                                "   AND PU.USER_ID = ?USER_ID\n" +
                                " WHERE MP.DELETE_FLAG <> 1\n" +
                                " ORDER BY MP.PROJECT ASC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchbyBranchOfficeUsers:
                    {
                        query = "SELECT BO.BRANCH_OFFICE_ID,\n" +
                         "       BO.BRANCH_OFFICE_NAME AS 'BRANCH',\n" +
                         "       IF(PU.USER_ID = ?USER_ID, 1, 0) AS 'SELECT'\n" +
                         "  FROM BRANCH_OFFICE BO\n" +
                         "  LEFT JOIN BRANCH_USER PU\n" +
                         "    ON PU.BRANCH_ID = BO.BRANCH_OFFICE_ID\n" +
                         "   AND PU.USER_ID = ?USER_ID\n" +
                         " WHERE BO.BRANCH_OFFICE_CODE =?BRANCH_OFFICE_CODE AND BO.STATUS =2\n" +
                         " ORDER BY BO.BRANCH_OFFICE_ID ASC;";
                        break;
                    }

                case SQLCommand.BranchOffice.SendMessage:
                    {
                        query = "INSERT INTO HEAD_MESSAGE(DATE,SUBJECT,CONTENT,TYPE)VALUES(?DATE,?SUBJECT,?CONTENT,?TYPE)";
                        break;
                    }

                case SQLCommand.BranchOffice.AddMessage:
                    {
                        query = "INSERT INTO MESSAGE_BRANCH(MESSAGE_ID,BRANCH_ID)VALUES(?MESSAGE_ID,?BRANCH_ID)";
                        break;
                    }

                case SQLCommand.BranchOffice.FetchMessage:
                    {
                        query = "SELECT HM.ID,SUBJECT, CONTENT, TYPE, BRANCH_ID\n" +
                                "  FROM HEAD_MESSAGE HM\n" +
                                " INNER JOIN MESSAGE_BRANCH MB\n" +
                                "    ON HM.ID = MB.MESSAGE_ID\n" +
                                " WHERE HM.ID = ?ID;";
                        break;
                    }
                case SQLCommand.BranchOffice.UpdateMessage:
                    {
                        query = "UPDATE HEAD_MESSAGE SET SUBJECT=?SUBJECT,CONTENT=?CONTENT,TYPE=?TYPE,DATE=?DATE WHERE ID=?ID";
                        break;
                    }
                case SQLCommand.BranchOffice.DeleteMessageBranch:
                    {
                        query = "DELETE FROM MESSAGE_BRANCH WHERE MESSAGE_ID=?MESSAGE_ID";
                        break;
                    }
                case SQLCommand.BranchOffice.ViewMessageDetail:
                    {
                        query = "SELECT HM.ID,\n" +
                                "       DATE_FORMAT(DATE, '%d/%m/%Y') AS Date,\n" +
                                "       Subject,\n" +
                                "       Content,\n" +
                                "       CASE\n" +
                                "         WHEN TYPE = 1 THEN\n" +
                                "          'Email'\n" +
                                "         WHEN TYPE = 2 THEN\n" +
                                "          'BroadCast'\n" +
                                "         WHEN TYPE = 3 THEN\n" +
                                "          'Both'\n" +
                                "       END AS Type\n" +
                                "  FROM HEAD_MESSAGE HM\n" +
                                " INNER JOIN MESSAGE_BRANCH MB\n" +
                                "    ON HM.ID = MB.MESSAGE_ID\n" +
                                "    { WHERE MB.BRANCH_ID IN (SELECT BRANCH_OFFICE_ID FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE) } GROUP BY ID;";

                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchIdByBranchCode:
                    {
                        query = "SELECT BRANCH_OFFICE_ID FROM BRANCH_OFFICE WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE";
                        break;
                    }

                case SQLCommand.BranchOffice.FetchBranchLoggedInfoByHeadOfficeCode:
                    {
                        query = "SELECT BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE, BRANCH_OFFICE_NAME, HEAD_OFFICE_NAME, " +
                                "LOCATION, LOGGED_ON AS LOGGED_ON," + //DATE_FORMAT(LOGGED_ON, '%d/%m/%Y %r') AS LOGGED_ON
                                "LICENSE_KEY_NUMBER, REMARKS " +
                                "FROM BRANCH_LOGGED_HISTORY " +
                                "WHERE 1=1 {AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE } {AND DATE(LOGGED_ON)=?LOGGED_ON }";
                        //"ORDER BY HEAD_OFFICE_NAME, BRANCH_OFFICE_NAME, DATE(LOGGED_ON) DESC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchBranchLoggedInfoByHeadOfficeCodeByBranch:
                    {
                        query = "SELECT BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE, BRANCH_OFFICE_NAME, HEAD_OFFICE_NAME, " +
                                "LOCATION, LOGGED_ON AS LOGGED_ON," + //DATE_FORMAT(LOGGED_ON, '%d/%m/%Y %r') AS LOGGED_ON
                                "LICENSE_KEY_NUMBER, REMARKS " +
                                "FROM BRANCH_LOGGED_HISTORY " +
                                "WHERE 1=1 {AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE } { AND BRANCH_OFFICE_CODE IN (?BRANCH_OFFICE_CODE) } {AND DATE(LOGGED_ON)=?LOGGED_ON }";
                        //"ORDER BY HEAD_OFFICE_NAME, BRANCH_OFFICE_NAME, DATE(LOGGED_ON) DESC";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchHeadOfficewiseBranchOffice:
                    {
                        query = "select ho.HEAD_OFFICE_CODE,ho.HEAD_OFFICE_NAME,bo.BRANCH_OFFICE_NAME " +
                          " from head_office ho left join branch_office bo on ho.HEAD_OFFICE_CODE = bo.HEAD_OFFICE_CODE " +
                          " where ho.STATUS =2 AND bo.STATUS=2 order by ho.HEAD_OFFICE_NAME, bo.BRANCH_OFFICE_NAME";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchHeadOfficewiseBranchOfficeDetailed:
                    {
                        query = "select ho.HEAD_OFFICE_CODE,HEAD_OFFICE_NAME, " +
                                 " BO.BRANCH_OFFICE_CODE,bo.BRANCH_OFFICE_NAME, bo.CREATED_DATE as BRANCH_CREATED, BO.ADDRESS ,BO.INCHARGE_NAME,bo.MOBILE_NO, bo.BRANCH_EMAIL_ID " +
                                 " from head_office ho left join branch_office bo on ho.HEAD_OFFICE_CODE = bo.HEAD_OFFICE_CODE " +
                                 " where ho.STATUS =2 AND bo.STATUS=2 order by ho.HEAD_OFFICE_NAME, bo.BRANCH_OFFICE_NAME";
                        break;
                    }
                case SQLCommand.BranchOffice.FetchHeadOfficewiseBranchOfficeCount:
                    {
                        query = " select ho.HEAD_OFFICE_CODE,HEAD_OFFICE_NAME,count(BRANCH_OFFICE_NAME) as COUNT from head_office ho " +
                                 " left join branch_office bo on ho.HEAD_OFFICE_CODE = bo.HEAD_OFFICE_CODE " +
                                 " where ho.STATUS =2 AND bo.STATUS=2 group by HEAD_OFFICE_NAME order by ho.HEAD_OFFICE_NAME";
                        break;
                    }
                case SQLCommand.BranchOffice.IsExistsBranchLoggedInfo:
                    {
                        query = "SELECT BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE " +
                                "FROM BRANCH_LOGGED_HISTORY " +
                                "WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE AND LOCATION=?LOCATION AND REMARKS=?REMARKS";
                        break;
                    }
                case SQLCommand.BranchOffice.InsertBranchLoggedInfo:
                    {
                        query = "INSERT INTO BRANCH_LOGGED_HISTORY " +
                                "(BRANCH_OFFICE_CODE, HEAD_OFFICE_CODE, BRANCH_OFFICE_NAME, HEAD_OFFICE_NAME, LOCATION, LOGGED_ON,LICENSE_KEY_NUMBER, REMARKS) " +
                                "VALUES (?BRANCH_OFFICE_CODE, ?HEAD_OFFICE_CODE, ?BRANCH_OFFICE_NAME, ?HEAD_OFFICE_NAME, ?LOCATION, ?LOGGED_ON, ?LICENSE_KEY_NUMBER,?REMARKS)";
                        break;
                    }
                case SQLCommand.BranchOffice.UpdateBranchLoggedInfo:
                    {
                        query = "UPDATE BRANCH_LOGGED_HISTORY " +
                                "SET LOGGED_ON= ?LOGGED_ON, LICENSE_KEY_NUMBER=?LICENSE_KEY_NUMBER, REMARKS=?REMARKS " +
                                "WHERE BRANCH_OFFICE_CODE=?BRANCH_OFFICE_CODE AND HEAD_OFFICE_CODE=?HEAD_OFFICE_CODE AND LOCATION=?LOCATION AND REMARKS = ?REMARKS";
                        break;
                    }
            }

            return query;
        }
        #endregion BRANCH_Office SQL
    }
}
