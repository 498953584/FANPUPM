namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class FundApplication
    {
        public bool delFundRecord(Guid GuidFlow)
        {
            return publicDbOpClass.NonQuerySqlString("delete from  EPM_Fund_Application where GuidFlow = '" + GuidFlow + "'");
        }

        public DataTable getFundSchedule()
        {
            string sqlString = "select * from EPM_V_Fund_Schedule";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public FundAppInfo getOnePrjAppRecord(Guid GuidFlow)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from  EPM_Fund_Application where GuidFlow = '" + GuidFlow + "'");
            return new FundAppInfo { FundAppDate = Convert.ToDateTime(table.Rows[0]["FundAppDate"]), FundNum = Convert.ToDecimal(table.Rows[0]["FundNum"]), Content = table.Rows[0]["Content"].ToString() };
        }

        public DataTable getPrjAppRecords(string PrjCode, string yhdm, string type, int pageSize, int pageIndex)
        {
            string strWhere = " (1=1) ";
            if (type == "Edit")
            {
                string str2 = strWhere;
                strWhere = str2 + " and PrjCode = '" + PrjCode + "' and FundAppUser = '" + yhdm + "'";
            }
            if (type == "Audit")
            {
                strWhere = (strWhere + " and AuditState<>1") + " and PrjCode = '" + PrjCode + "'";
            }
            if (type == "Select")
            {
                strWhere = strWhere + " and AuditState=1 and PrjCode = '" + PrjCode + "'";
            }
            return publicDbOpClass.GetRecordFromPage("EPM_Fund_Application", "Id", pageSize, pageIndex, 1, strWhere);
        }

        public int getPrjAppRecordsCount(string PrjCode, string yhdm, string type)
        {
            string sqlString = "";
            sqlString = " select Count(1) from EPM_Fund_Application where (1=1)";
            if (type == "Edit")
            {
                string str2 = sqlString;
                sqlString = str2 + " and PrjCode = '" + PrjCode + "' and FundAppUser = '" + yhdm + "'";
            }
            if (type == "Audit")
            {
                sqlString = (sqlString + " and AuditState<>1") + " and PrjCode = '" + PrjCode + "'";
            }
            if (type == "Select")
            {
                sqlString = sqlString + " and AuditState=1 and PrjCode = '" + PrjCode + "'";
            }
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString));
        }

        public bool insFundRecord(FundAppInfo fai)
        {
            string str = "";
            object obj2 = str + "insert into EPM_Fund_Application (PrjCode,FundAppUser,FundAppDate,FundNum,Content) values (";
            object obj3 = string.Concat(new object[] { obj2, "'", fai.PrjCode, "','", fai.FundAppUser, "','", fai.FundAppDate, "'," });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj3, "cast('", fai.FundNum, "' as decimal(18,2)),'", fai.Content, "')" }));
        }

        public bool updFundRecord(FundAppInfo fai)
        {
            object obj2 = string.Concat(new object[] { "update EPM_Fund_Application set FundNum = cast('", fai.FundNum, "' as decimal(18,2)),Content = '", fai.Content, "' where " });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj2, " GuidFlow = '", fai.GuidFlow, "'" }));
        }
    }
}

