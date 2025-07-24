/********************************************************************************************
 *                                              Class      :MappingSQL.cs
 *                                              Purpose    :All the queries Mapping 
 *                                              Author     : Carmel Raj M
 *********************************************************************************************/
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    public class MappingSQL : IDatabaseQuery
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

            if (sqlCommandName == typeof(SQLCommand.Mapping).FullName)
            {
                query = GetMappingSQL();
            }
            sqlType = this.sqlType;
            return query;
        }

        #endregion

        #region SQL Script
        private string GetMappingSQL()
        {
            string Query = "";
            SQLCommand.Mapping sqlCommandId = (SQLCommand.Mapping)(this.dataCommandArgs.SQLCommandId);
            switch (sqlCommandId)
            {
                #region Projecct SQL
                case SQLCommand.Mapping.FetchProjectforLookup:
                    {
                        Query = "SELECT " +
                                    "MP.PROJECT_ID," +
                                    "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE " +
                                "FROM " +
                                    " MASTER_PROJECT MP " +
                                    " INNER JOIN MASTER_DIVISION MD ON " +
                                    " MP.DIVISION_ID=MD.DIVISION_ID WHERE  MP.DELETE_FLAG<>1 ORDER BY MP.PROJECT ASC ";
                        break;
                    }

                case SQLCommand.Mapping.FetchProjects:
                    {
                        Query = "SELECT " +
                                  "MP.PROJECT_ID, PROJECT_CATEGORY_ID, MD.DIVISION,MP.PROJECT AS PROJECT_NAME, " +
                                  "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE,0 AS 'SELECT' " +
                              "FROM " +
                                  " MASTER_PROJECT MP " +
                                  " INNER JOIN MASTER_DIVISION MD ON " +
                                  " MP.DIVISION_ID=MD.DIVISION_ID AND MP.DELETE_FLAG<>1  "+
                                  " { INNER JOIN PROJECT_BRANCH PB ON PB.PROJECT_ID=MP.PROJECT_ID AND PB.BRANCH_ID IN(?BRANCH_OFFICE_ID) } " +
                                  " GROUP BY MP.PROJECT_ID ORDER BY MP.PROJECT ASC ";
                        break;

                    }
                case SQLCommand.Mapping.FetchProjectsByLoginUser:
                    {
                        Query = "SELECT " +
                                  "MP.PROJECT_ID,MD.DIVISION,MP.PROJECT AS PROJECT_NAME, " +
                                  "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE,0 AS 'SELECT' " +
                              "FROM " +
                                  " MASTER_PROJECT MP " +
                                  " INNER JOIN MASTER_DIVISION MD ON " +
                                  " MP.DIVISION_ID=MD.DIVISION_ID AND MP.DELETE_FLAG<>1  " +
                                  " INNER JOIN PROJECT_USER PU ON PU.PROJECT_ID=MP.PROJECT_ID AND PU.USER_ID IN(?USER_ID) " +
                                  " { INNER JOIN PROJECT_BRANCH PB ON PB.PROJECT_ID=MP.PROJECT_ID AND PB.BRANCH_ID IN(?BRANCH_OFFICE_ID) } " +
                                  " GROUP BY MP.PROJECT_ID ORDER BY MP.PROJECT ASC ";
                        break;

                    }
                case SQLCommand.Mapping.FetchProjectBySociety:
                    {
                        Query = "SELECT " +
                                  "MP.PROJECT_ID, PROJECT_CATEGORY_ID, MD.DIVISION, MP.PROJECT AS PROJECT_NAME, " +
                                  "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE,0 AS 'SELECT' " +
                              "FROM " +
                                  " MASTER_PROJECT MP " +
                                  " INNER JOIN MASTER_DIVISION MD ON MP.DIVISION_ID=MD.DIVISION_ID AND MP.DELETE_FLAG<>1 " +
                                  " INNER JOIN PROJECT_BRANCH PB ON PB.PROJECT_ID=MP.PROJECT_ID { AND PB.BRANCH_ID IN(?BRANCH_OFFICE_ID) } { AND MP.CUSTOMERID IN (?CUSTOMERID) } {AND MP.PROJECT_CATEGORY_ID IN (?PROJECT_CATEGORY_ID)} " +
                                  " GROUP BY MP.PROJECT_ID ORDER BY MP.PROJECT ASC ";
                        break;
                    }
                case SQLCommand.Mapping.FetchProjectIdByBranchLocation:
                    {
                        Query = @"SELECT IFNULL(PB.BRANCH_ID, 0) AS BRANCH_ID, GROUP_CONCAT(IFNULL(MP.PROJECT_ID,0)) AS PROJECT_ID 
                                  FROM MASTER_PROJECT MP
                                  INNER JOIN PROJECT_BRANCH PB ON PB.PROJECT_ID = MP.PROJECT_ID  AND PB.BRANCH_ID = ?BRANCH_ID AND PB.LOCATION_ID = ?LOCATION_ID
                                  GROUP BY PB.BRANCH_ID";
                        break;
                    }
                case SQLCommand.Mapping.FetchProjectBySocietyUser:
                    {
                        Query = "SELECT " +
                                  "MP.PROJECT_ID,MD.DIVISION, MP.PROJECT AS PROJECT_NAME, " +
                                  "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT ' ') AS  TRANS_MODE,0 AS 'SELECT' " +
                              "FROM " +
                                  " MASTER_PROJECT MP " +
                                  " INNER JOIN MASTER_DIVISION MD ON MP.DIVISION_ID=MD.DIVISION_ID AND MP.DELETE_FLAG<>1 " +
                                  " INNER JOIN PROJECT_BRANCH PB ON PB.PROJECT_ID=MP.PROJECT_ID { AND PB.BRANCH_ID IN(?BRANCH_OFFICE_ID) } { AND MP.CUSTOMERID IN (?CUSTOMERID) } " +
                                  " INNER JOIN PROJECT_USER PU ON PU.PROJECT_ID=MP.PROJECT_ID AND PU.USER_ID IN(?USER_ID) "+
                                  " GROUP BY MP.PROJECT_ID ORDER BY MP.PROJECT ASC ";
                        break;
                    }
                case SQLCommand.Mapping.FetchProjectForGridView:
                    {
                        Query = "SELECT " +
                                    "MP.PROJECT_ID," +
                                    "CONCAT(MP.PROJECT,CONCAT(' - ',MD.DIVISION)) AS 'PROJECT',(SELECT 'DR') AS  TRANS_MODE,(SELECT 0.00) AS AMOUNT " +
                                "FROM " +
                                    " MASTER_PROJECT MP " +
                                    " INNER JOIN MASTER_DIVISION MD ON " +
                                    " MP.DIVISION_ID=MD.DIVISION_ID WHERE MP.DATE_CLOSED IS NULL AND MP.DELETE_FLAG<>1 ORDER BY MP.PROJECT ASC ";
                        break;
                    }
                case SQLCommand.Mapping.LoadProjectMappingGrid:
                    {
                        Query = @"SELECT P.PROJECT_ID, PROJECT, PL.LEDGER_ID,
                                    IF(PL.LEDGER_ID IS NULL, 0,1) AS 'SELECT',(SELECT 'DR') AS  TRANS_MODE,
                                    IF(AMOUNT IS NULL,0.00,AMOUNT) AS AMOUNT ,LB.TRANS_FLAG
                                    FROM MASTER_PROJECT P
                                    LEFT JOIN PROJECT_LEDGER PL ON PL.PROJECT_ID = P. PROJECT_ID AND PL.LEDGER_ID=?LEDGER_ID
                                    LEFT JOIN LEDGER_BALANCE LB ON LB.PROJECT_ID=PL.PROJECT_ID
                                                                AND LB.LEDGER_ID=PL.LEDGER_ID
                                                                AND LB.trans_flag='OP'
                                     WHERE P.DELETE_FLAG<>1;";
                        break;
                    }
                case SQLCommand.Mapping.LoadProjectDonorGrid:
                    {
                        Query = @"SELECT P.PROJECT_ID, PROJECT, PD.DONOR_ID,IF(PD.DONOR_ID IS NULL, 0,1) AS 'SELECT'
                                    FROM MASTER_PROJECT P
                                    LEFT JOIN PROJECT_DONOR PD ON P.PROJECT_ID = PD.PROJECT_ID AND PD.DONOR_ID=?DONAUD_ID WHERE P.DELETE_FLAG<>1;";
                        break;
                    }
                case SQLCommand.Mapping.LoadProjectCostCentreGrid:
                    {
                        Query = @"SELECT P.PROJECT_ID, PROJECT, PCC.COST_CENTRE_ID,IF(PCC.COST_CENTRE_ID IS NULL, 0,1) AS 'SELECT',
                                (SELECT 'DR') AS  TRANS_MODE,IF(AMOUNT IS NULL,0.00,AMOUNT) AS AMOUNT
                                FROM MASTER_PROJECT P
                                LEFT JOIN PROJECT_COSTCENTRE PCC ON P.PROJECT_ID = PCC.PROJECT_ID
                                AND PCC.COST_CENTRE_ID=?COST_CENTRE_ID
                                WHERE P.DELETE_FLAG<>1;";
                        break;
                    }
                case SQLCommand.Mapping.LoadProjectFDLedgerGrid:
                    {
                        Query = "SELECT P.PROJECT_ID,\n" +
                                "       PROJECT,\n" +
                                "       PL.LEDGER_ID,\n" +
                                "       IF(PL.LEDGER_ID IS NULL, 0, 1) AS 'SELECT',\n" +
                                "       (SELECT 'DR') AS TRANS_MODE,\n" +
                            // "       IFNULL(SUM(AMOUNT), 0.00) AS AMOUNT\n" +
                                "       IFNULL(AMOUNT, 0.00) AS AMOUNT\n" +
                                "  FROM MASTER_PROJECT P\n" +
                                "  LEFT JOIN PROJECT_LEDGER PL\n" +
                                "    ON PL.PROJECT_ID = P. PROJECT_ID\n" +
                                "   AND PL.LEDGER_ID =?LEDGER_ID\n" +
                                "  LEFT JOIN FD_ACCOUNT FD\n" +
                                "    ON FD.PROJECT_ID = PL.PROJECT_ID\n" +
                                "   AND FD.LEDGER_ID = PL.LEDGER_ID\n" +
                                "   AND TRANS_TYPE = 'OP'\n" +
                                "   AND FD.STATUS = 1\n" +
                                " WHERE P.DELETE_FLAG <> 1"; // GROUP BY PROJECT_ID";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedProject:
                    {
                        Query = @"SELECT
                                        MP.PROJECT,
                                        PL.PROJECT_ID,
                                        PL.LEDGER_ID,
                                        ML.LEDGER_NAME,
                                        LG.LEDGER_GROUP,
                                        IFNULL(LB.AMOUNT, 0) AS AMOUNT,
                                        IF(IFNULL(LB.TRANS_MODE, '') = '',
                                            IF(LG.NATURE_ID IN (1, 4), 'CR', 'DR'),
                                            LB.TRANS_MODE) AS TRANS_MODE,
                                        CASE
                                            WHEN LEDGER_TYPE = 'GN' THEN
                                            'General'
                                            ELSE
                                            CASE
                                            WHEN LEDGER_TYPE = 'IK' THEN
                                                'In kind'
                                            END
                                        END 'GROUP',
                                        IFNULL(LB.LEDGER_ID, 0) AS UPDATE_MODE
                                    FROM
                                    MASTER_PROJECT MP INNER JOIN
                                    PROJECT_LEDGER AS PL ON MP.PROJECT_ID=PL.PROJECT_ID
                                    LEFT JOIN MASTER_LEDGER AS ML
                                    ON PL.LEDGER_ID = ML.LEDGER_ID
                                    LEFT JOIN MASTER_LEDGER_GROUP AS LG
                                    ON ML.GROUP_ID = LG.GROUP_ID
                                    LEFT JOIN LEDGER_BALANCE AS LB
                                    ON PL.PROJECT_ID = LB.PROJECT_ID
                                    AND PL.LEDGER_ID = LB.LEDGER_ID
                                    AND LB.TRANS_FLAG = 'OP'
                                    WHERE PL.LEDGER_ID = ?LEDGER_ID AND MP.DELETE_FLAG<>1";
                        break;
                    }
                case SQLCommand.Mapping.LedgerProjectMappingDelete:
                    {
                        Query = "DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.LoadLedgerByProId:
                    {
                        Query = @"SELECT ML.LEDGER_ID,
                                        IF(AMOUNT IS NULL, 0.00, AMOUNT) AS AMOUNT,
                                        IF(ML.GROUP_ID = 13, 1, IF(PL.LEDGER_ID IS NULL, 0, 1)) AS 'SELECT_TMP',
                                        ML.GROUP_ID,
                                        LEDGER_CODE,
                                        LEDGER_NAME,
                                        ML.SORT_ID,
                                        CASE
                                            WHEN LEDGER_SUB_TYPE = 'BK' THEN
                                            'Bank Accounts'
                                            ELSE
                                            CASE
                                            WHEN LEDGER_SUB_TYPE = 'FD' THEN
                                                'Fixed Deposit'
                                            ELSE
                                                LEDGER_GROUP
                                            END
                                        END AS 'TYPE',
                                        IF(MG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,
                                        CASE
                                            WHEN LEDGER_TYPE = 'GN' THEN
                                            'General'
                                            ELSE
                                            CASE
                                            WHEN LEDGER_TYPE = 'IK' THEN
                                                'In kind'
                                            END
                                        END 'GROUP',
                                        ML.BANK_ACCOUNT_ID
                                    FROM MASTER_LEDGER ML
                                    LEFT JOIN PROJECT_LEDGER PL
                                    ON PL.LEDGER_ID = ML.LEDGER_ID
                                    AND PL.PROJECT_ID = ?PROJECT_ID
                                    LEFT JOIN LEDGER_BALANCE LB
                                    ON LB.LEDGER_ID = ML.LEDGER_ID
                                    AND LB.PROJECT_ID = ?PROJECT_ID
                                    AND TRANS_FLAG = 'OP'
                                    LEFT JOIN MASTER_LEDGER_GROUP MG
                                    ON ML.GROUP_ID = MG.GROUP_ID
                                    WHERE STATUS = 0
                                    -- AND ML.LEDGER_ID NOT IN
                                      --   (SELECT LEDGER_ID
                                         --    FROM PROJECT_LEDGER
                                          --  WHERE LEDGER_ID IN
                                            --    (SELECT LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID = 14))
                                    ORDER BY ML.SORT_ID;";
                        break;
                    }
               
                    
                #endregion

                #region Ledger SQL
                case SQLCommand.Mapping.LoadLedgerFD:
                    {
                        Query = "SELECT LEDGER_ID," +
                                    "LEDGER_CODE," +
                                    "LEDGER_NAME," +
                                    "ML.SORT_ID, " +
                                    "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'BK' THEN " +
                                        "'Bank Accounts' " +
                                        "ELSE " +
                                        "CASE " +
                                        "WHEN LEDGER_SUB_TYPE = 'FD' THEN " +
                                            "'Fixed Deposit' " +
                                        "ELSE " +
                                            "LEDGER_GROUP " +
                                        "END " +
                                    "END AS 'TYPE', " +
                                    "IF(MG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE, " +
                                    "CASE " +
                                        "WHEN LEDGER_TYPE = 'GN' THEN " +
                                        "'General' " +
                                        "ELSE " +
                                        "CASE " +
                                        "WHEN LEDGER_TYPE = 'IK' THEN " +
                                            "'In kind' " +
                                        "END " +
                                    "END 'GROUP', " +
                                    "ML.BANK_ACCOUNT_ID " +
                                "FROM MASTER_LEDGER ML " +
                                "LEFT JOIN MASTER_LEDGER_GROUP MG " +
                                "ON ML.GROUP_ID = MG.GROUP_ID " +
                                "WHERE STATUS = 0 " +
                                    "AND ML.LEDGER_ID NOT IN " +
                                "(SELECT LEDGER_ID " +
                                "FROM PROJECT_LEDGER " +
                                "WHERE LEDGER_ID IN " +
                                        "(SELECT LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID = 14)) " +
                                "ORDER BY ML.SORT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.LoadAllLedgers:
                    {
                        Query = @"SELECT LEDGER_ID,ML.GROUP_ID,
                                     LEDGER_CODE,
                                     LEDGER_NAME,
                                     ML.SORT_ID,
                                     CASE
                                         WHEN LEDGER_SUB_TYPE = 'BK' THEN
                                         'Bank Accounts'
                                         ELSE
                                         CASE
                                         WHEN LEDGER_SUB_TYPE = 'FD' THEN
                                             'Fixed Deposit'
                                         ELSE
                                             LEDGER_GROUP
                                         END
                                     END AS 'TYPE',
                                     IF(MG.NATURE_ID IN (1, 4), 'CR', 'DR') AS TRANS_MODE,
                                     CASE
                                         WHEN LEDGER_TYPE = 'GN' THEN
'General'
                                         ELSE
                                         CASE
                                         WHEN LEDGER_TYPE = 'IK' THEN
                                             'In kind'
                                         END
                                     END 'GROUP',
                                     ML.BANK_ACCOUNT_ID
                                 FROM MASTER_LEDGER ML
                                 LEFT JOIN MASTER_LEDGER_GROUP MG
                                 ON ML.GROUP_ID = MG.GROUP_ID
                                 WHERE STATUS = 0 AND ML.LEDGER_ID NOT IN
                                (SELECT LEDGER_ID
                                FROM PROJECT_LEDGER
                                WHERE LEDGER_ID IN
                                        (SELECT LEDGER_ID FROM MASTER_LEDGER WHERE GROUP_ID = 14))
                                ORDER BY ML.SORT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.ProjectLedgerMappingDelete:
                    {
                        Query = "DELETE FROM PROJECT_LEDGER WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.UnMapProjectLedger:
                    {
                        Query = "DELETE FROM PROJECT_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.DeleteMappedLedgerBalance:
                    {
                        Query = "DELETE FROM LEDGER_BALANCE WHERE PROJECT_ID=?PROJECT_ID AND TRANS_FLAG='OP';";
                        break;
                    }

                //case SQLCommand.Mapping.MapLedgersToProject:
                //    {
                //        Query = "INSERT INTO PROJECT_LEDGER\n" +
                //                "  (PROJECT_ID, LEDGER_ID)\n" +
                //                "VALUES\n" +
                //                "  (?PROJECT_ID, ?LEDGER_ID) ON DUPLICATE KEY UPDATE PROJECT_ID = ?PROJECT_ID, LEDGER_ID = ?LEDGER_ID;";
                //        break;
                //    }
                case SQLCommand.Mapping.CheckLedgerMapped:
                    {
                        Query = "SELECT PROJECT_ID,LEDGER_ID FROM PROJECT_LEDGER WHERE PROJECT_ID = ?PROJECT_ID AND LEDGER_ID = ?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.CheckCostCentreMapped:
                    {
                        Query = "SELECT PROJECT_ID,COST_CENTRE_ID FROM PROJECT_COSTCENTRE WHERE PROJECT_ID = ?PROJECT_ID AND COST_CENTRE_ID = ?COST_CENTRE_ID;";
                        break;
                    }
                //case SQLCommand.Mapping.MapCostCentreToProject:
                //    {
                //        Query = "INSERT INTO PROJECT_COSTCENTRE\n" +
                //                "  (PROJECT_ID, COST_CENTRE_ID, AMOUNT,TRANS_MODE)\n" +
                //                "VALUES\n" +
                //                "  (?PROJECT_ID, ?COST_CENTRE_ID, 0, ?TRANS_MODE) ON DUPLICATE KEY UPDATE PROJECT_ID = ?PROJECT_ID, COST_CENTRE_ID = ?COST_CENTRE_ID;";
                //        break;
                //    }
                case SQLCommand.Mapping.FetchMappedLedgers:
                    {
                        Query = @"SELECT PL.LEDGER_ID, MP.PROJECT_ID,PROJECT,AMOUNT,TRANS_MODE
                                        FROM PROJECT_LEDGER PL
                                        LEFT JOIN MASTER_LEDGER ML ON ML.LEDGER_ID=PL.LEDGER_ID
                                        LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=PL.PROJECT_ID
                                        LEFT JOIN LEDGER_BALANCE LB ON LB.PROJECT_ID=PL.PROJECT_ID
                                                                    AND LB.LEDGER_ID=PL.LEDGER_ID
                                                                    AND TRANS_FLAG='OP'
                                        WHERE PL.LEDGER_ID=?LEDGER_ID ORDER BY PROJECT_ID;";
                        break;
                    }

                case SQLCommand.Mapping.FetchMappedFDByFDId:
                    {
                        Query = @"SELECT PL.PROJECT_ID,AMOUNT,TRANS_MODE
                                    FROM PROJECT_LEDGER PL
                                    LEFT JOIN LEDGER_BALANCE LB ON LB.LEDGER_ID=PL.LEDGER_ID AND LB.TRANS_FLAG='OP'
                                    WHERE PL.LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                #endregion

                #region Cost Center SQL
                case SQLCommand.Mapping.LoadAllCostCentre:
                    {
                        Query = "SELECT " +
                                    "COST_CENTRE_ID, " +
                                    "(SELECT '') AS TRANS_MODE," +
                                    "COST_CENTRE_NAME, " +
                                    "(SELECT 0.0) AS AMOUNT " +
                               "FROM MASTER_COST_CENTRE " +
                                " ORDER BY COST_CENTRE_NAME";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedCostCenter:
                    {
                        Query = "SELECT PC.COST_CENTRE_ID, COST_CENTRE_NAME,AMOUNT,TRANS_MODE " +
                                        "FROM PROJECT_COSTCENTRE AS PC " +
                                    "LEFT JOIN MASTER_COST_CENTRE AS MCC ON PC.COST_CENTRE_ID=MCC.COST_CENTRE_ID " +
                                  "WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.FetchMappedCostCenterByCostCenterId:
                    {
                        Query = @"SELECT PD.PROJECT_ID,PROJECT,AMOUNT,TRANS_MODE
                                    FROM PROJECT_COSTCENTRE PD
                                    LEFT JOIN MASTER_COST_CENTRE PCC ON PCC.COST_CENTRE_ID=PD.COST_CENTRE_ID
                                    LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=PD.PROJECT_ID
                                WHERE PD.COST_CENTRE_ID=?COST_CENTRE_ID ORDER BY PROJECT;";
                        break;
                    }

                case SQLCommand.Mapping.ProjectCostCentreMappingAdd:
                    {
                        Query = "INSERT INTO PROJECT_COSTCENTRE(PROJECT_ID,COST_CENTRE_ID,AMOUNT,TRANS_MODE) VALUES(?PROJECT_ID,?COST_CENTRE_ID,?AMOUNT,?TRANS_MODE)";
                        break;
                    }
                case SQLCommand.Mapping.DeleteProjectCostCenterMapping:
                    {
                        Query = "DELETE FROM PROJECT_COSTCENTRE WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.UnMapCostCentreByCCId:
                    {
                        Query = "DELETE FROM PROJECT_COSTCENTRE WHERE COST_CENTRE_ID=?COST_CENTRE_ID;";
                        break;
                    }
                #endregion

                #region Donor SQL
                case SQLCommand.Mapping.FetchDonorMapped:
                    {
                        Query = "SELECT DONAUD_ID,NAME,COUNTRY,MC.COUNTRY_ID," +
                                    "CASE WHEN TYPE=1 THEN 'Institutional' ELSE " +
                                    "CASE WHEN TYPE=2 THEN 'Individual ' END END AS TYPE " +
                                "FROM PROJECT_DONOR PD " +
                                    "INNER JOIN MASTER_DONAUD MD ON MD.DONAUD_ID=PD.DONOR_ID " +
                                    "INNER JOIN MASTER_COUNTRY MC ON MC.COUNTRY_ID=MD.COUNTRY_ID " +
                                "WHERE PROJECT_ID=?PROJECT_ID ORDER BY DONAUD_ID DESC";
                        break;
                    }

                case SQLCommand.Mapping.FetchMappedDonorByDonorId:
                    {
                        Query = @"SELECT PD.PROJECT_ID,PROJECT
                                    FROM PROJECT_DONOR PD
                                    LEFT JOIN MASTER_DONAUD MD ON MD.DONAUD_ID=PD.DONOR_ID
                                    LEFT JOIN MASTER_PROJECT MP ON MP.PROJECT_ID=PD.PROJECT_ID
                                    WHERE DONOR_ID=?DONAUD_ID ORDER BY PROJECT;";
                        break;
                    }
                case SQLCommand.Mapping.LoadAllDonor:
                    {
                        Query = "SELECT DONAUD_ID,NAME,COUNTRY," +
                                "CASE WHEN TYPE=1 THEN 'Institutional' ELSE " +
                                "CASE WHEN TYPE=2 THEN 'Individual ' END " +
                                "END AS TYPE " +
                                "FROM MASTER_DONAUD MD " +
                                      "INNER JOIN MASTER_COUNTRY MC ON MD.COUNTRY_ID=MC.COUNTRY_ID " +
                                "WHERE IDENTITYKEY=0;";
                        break;
                    }
                case SQLCommand.Mapping.DonorMap:
                    {
                        Query = "INSERT INTO PROJECT_DONOR(PROJECT_ID,DONOR_ID) VALUES(?PROJECT_ID,?DONAUD_ID);";
                        break;
                    }
                case SQLCommand.Mapping.DonorUnMap:
                    {
                        Query = "DELETE FROM PROJECT_DONOR WHERE PROJECT_ID=?PROJECT_ID;";
                        break;
                    }
                case SQLCommand.Mapping.DonorUnMapByDonorId:
                    {
                        Query = "DELETE FROM PROJECT_DONOR WHERE DONOR_ID=?DONAUD_ID;";
                        break;
                    }
                case SQLCommand.Mapping.DeleteGeneralateMapping:
                    {
                        Query = "DELETE FROM SUBSIDY_CONTRIBUTION_LEDGERS_MAPPING\n" +
                                " WHERE SUBSIDY_CONTRIBUTION_ID =?LEDGER_ID\n" +
                                "   AND IS_SUBSIDY =?IS_SUBSIDY_LEDGER";
                        break;
                    }
                case SQLCommand.Mapping.MapGeneralateLedgers:
                    {
                        Query = "INSERT INTO SUBSIDY_CONTRIBUTION_LEDGERS_MAPPING\n" +
                                "  (SUBSIDY_CONTRIBUTION_ID, MAPPING_LEDGER_ID, IS_SUBSIDY)\n" + 
                                "VALUES\n" +
                                "  (?LEDGER_ID, ?GENERALATE_MAPPING_LEDGERID, ?IS_SUBSIDY_LEDGER)";
                        break;
                    }
                case SQLCommand.Mapping.LoadMappedLedgers:
                    {
                        Query = "SELECT ML.LEDGER_ID,SM.MAPPING_LEDGER_ID AS GENERALATE_MAPPING_LEDGERID,\n" +
                                "       MG.GROUP_ID,\n" + 
                                "       ML.LEDGER_CODE AS 'Code',\n" + 
                                "       ML.LEDGER_NAME AS 'Name',\n" + 
                                "       CASE\n" + 
                                "         WHEN ML.LEDGER_SUB_TYPE = 'BK' THEN\n" + 
                                "          'Bank Accounts'\n" + 
                                "         ELSE\n" + 
                                "          CASE\n" + 
                                "            WHEN ML.LEDGER_SUB_TYPE = 'FD' THEN\n" + 
                                "             'Fixed Deposit'\n" + 
                                "            ELSE\n" + 
                                "             MG.LEDGER_GROUP\n" + 
                                "          END\n" + 
                                "       END AS 'Group',\n" + 
                                "       ML.LEDGER_TYPE,\n" + 
                                "       ML.LEDGER_SUB_TYPE,\n" + 
                                "       ML.BANK_ACCOUNT_ID, 0 as 'SELECT'\n" + 
                                "  FROM MASTER_LEDGER ML\n" + 
                                " INNER JOIN MASTER_LEDGER_GROUP MG\n" + 
                                "    ON ML.GROUP_ID = MG.GROUP_ID\n" + 
                                "   AND ML.STATUS = 0\n" + 
                                "   AND ML.IS_BRANCH_LEDGER = 0\n" + 
                                "  LEFT JOIN SUBSIDY_CONTRIBUTION_LEDGERS_MAPPING SM\n" + 
                                "    ON ML.LEDGER_ID = SM.MAPPING_LEDGER_ID\n" +
                                "   AND SUBSIDY_CONTRIBUTION_ID = ?LEDGER_ID WHERE NATURE_ID=?NATURE_ID\n" + 
                                " ORDER BY SM.MAPPING_LEDGER_ID DESC;";
                        break;
                    }
                case SQLCommand.Mapping.LoadMappedGeneralateLedgers:
                    {
                        Query= "SELECT SUBSIDY_CONTRIBUTION_ID,\n" +
                                "       MAPPING_LEDGER_ID AS LEDGER_ID,\n" + 
                                "       IS_SUBSIDY,\n" +
                                "       ML.LEDGER_NAME AS LEDGER,0 as 'SELECT'\n" + 
                                "  FROM SUBSIDY_CONTRIBUTION_LEDGERS_MAPPING SM\n" + 
                                "  LEFT JOIN MASTER_LEDGER ML\n" + 
                                "    ON SM.MAPPING_LEDGER_ID = ML.LEDGER_ID \n" + 
                                " WHERE ML.STATUS = 0;";
                        break;
                    }
                #endregion

                #region Common SQL
                case SQLCommand.Mapping.ProjectLedgerMappingAdd:
                    {
                        Query = "INSERT INTO PROJECT_LEDGER(PROJECT_ID,LEDGER_ID) VALUES(?PROJECT_ID,?LEDGER_ID);";
                        break;
                    }
                case SQLCommand.Mapping.BankIdByLedgerId:
                    {
                        Query = "SELECT BANK_ACCOUNT_ID FROM MASTER_LEDGER WHERE LEDGER_ID=?LEDGER_ID;";
                        break;
                    }
                case SQLCommand.Mapping.TransactionFixedDepositId:
                    {
                        Query = "SELECT BANK_ACCOUNT_ID FROM FD_REGISTERS WHERE TRANS_MODE='TR';";
                        break;
                    }
                #endregion
            }
            return Query;
        }
        #endregion
    }
}
