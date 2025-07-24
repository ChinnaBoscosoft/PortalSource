using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.DAO.Schema;

namespace Bosco.SQL
{
    class TDSSQL : IDatabaseQuery
    {
        #region ISQLServerQueryMembers

        DataCommandArguments dataCommandArgs;
        SQLType sqlType;
        public string GetQuery(DataCommandArguments dataCommandArgs, ref SQLType sqlType)
        {
            string query = "";
            this.dataCommandArgs = dataCommandArgs;
            this.sqlType = SQLType.SQLStatic;

            string sqlCommandName = dataCommandArgs.FullName;

            if (sqlCommandName == typeof(SQLCommand.TDS).FullName)
            {
                query = GetTDSSQL();
            }

            sqlType = this.sqlType;
            return query;
        }

        #endregion

        public string GetTDSSQL()
        {
            string query = "";
            SQLCommand.TDS SqlCommand = (SQLCommand.TDS)(this.dataCommandArgs.SQLCommandId);
            switch (SqlCommand)
            {
                case SQLCommand.TDS.FetchTDSSection:
                    {
                        query = "SELECT TDS_SECTION_ID,\n" +
                                "       CODE AS Code,\n" +
                                "       SECTION_NAME AS Name,\n" +
                                "       CASE\n" +
                                "         WHEN STATUS = 1 THEN\n" +
                                "          'Active'\n" +
                                "         ELSE\n" +
                                "          'Inactive'\n" +
                                "       END AS Status\n" +
                                "  FROM TDS_SECTION ORDER BY CODE ASC";
                        break;
                    }
                case SQLCommand.TDS.FetchNatureofPaymentsSection:
                    {
                        query = "SELECT TN.NATURE_PAY_ID,\n" +
                                "       TN.PAYMENT_CODE as Code,\n" +
                                "       TN.NAME as Name,\n" +
                                "       TS.SECTION_NAME as Section,\n" +
                                "       CASE\n" +
                                "         WHEN TN.STATUS = 1 THEN\n" +
                                "          'Active'\n" +
                                "         ELSE\n" +
                                "          'Inactive'\n" +
                                "       END AS Status\n" +
                                "  FROM TDS_NATURE_PAYMENT TN\n" +
                                "  LEFT JOIN TDS_SECTION TS\n" +
                                "    ON TN.TDS_SECTION_ID = TS.TDS_SECTION_ID";
                        break;
                    }
                case SQLCommand.TDS.FetchDeducteeTypes:
                    {
                        query = "   SELECT DEDUCTEE_TYPE_ID,NAME AS Name,\n" +
                                "       (CASE\n" +
                                "         WHEN RESIDENTIAL_STATUS = 0 THEN\n" +
                                "          \"Resident\"\n" +
                                "         ELSE\n" +
                                "          \"Non_Resident\"\n" +
                                "       END) AS \"Residential Status\",\n" +
                                "       (CASE\n" +
                                "         WHEN DEDUCTEE_TYPE = 0 THEN\n" +
                                "          \"Company\"\n" +
                                "         ELSE\n" +
                                "          \"Non Company\"\n" +
                                "       END) AS \"Deductee Status\",\n" +
                                "       (CASE\n" +
                                "         WHEN STATUS = 1 THEN\n" +
                                "          \"Active\"\n" +
                                "         ELSE\n" +
                                "          \"Inactive\"\n" +
                                "       END) AS Status\n" +
                                "  FROM TDS_DEDUCTEE_TYPE";
                        break;
                    }
                case SQLCommand.TDS.FetchDutyTax:
                    {
                        query = "SELECT TDS_DUTY_TAXTYPE_ID,\n" +
                               "       TAX_TYPE_NAME AS Name,\n" +
                               "       0 AS TDS_RATE,\n" +
                               "       0 AS TDS_EXEMPTION_LIMIT,\n" +
                               "       CASE\n" +
                               "         WHEN STATUS = 1 THEN\n" +
                               "          'Active'\n" +
                               "         ELSE\n" +
                               "          'Inactive'\n" +
                               "       END AS Status,\n" +
                               "       CASE\n" +
                               "         WHEN STATUS = 1 THEN\n" +
                               "          'true'\n" +
                               "         ELSE\n" +
                               "          'false'\n" +
                               "       END AS StatusValue\n" +
                               "  FROM TDS_DUTY_TAXTYPE";
                        break;
                    }
            }
            return query;
        }
    }
}
