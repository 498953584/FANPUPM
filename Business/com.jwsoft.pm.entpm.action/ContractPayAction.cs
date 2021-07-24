namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ContractPayAction
    {
        public bool delContPayRecord(Guid GuidFlow)
        {
            return publicDbOpClass.NonQuerySqlString("delete from EPM_Fund_ContractPay where GuidFlow = '" + GuidFlow + "'");
        }

        public DataTable getContPayAuditbyIC(string ic)
        {
            string str = "";
            str = "SELECT (SELECT top 1 [ContractName]   FROM [EPM_Con_ContractMain] where [ContractCode]=a.ContractCode) as ContractName,a.*,b.ContractType, CASE WHEN b.SumPayMoney IS NULL THEN 0 ELSE b.SumPayMoney END AS SumPayMoney FROM EPM_Fund_ContractPay a INNER JOIN EPM_V_Fund_ContListForPay b ON a.ContractCode = b.ContractCode  ";
            return publicDbOpClass.DataTableQuary(str + " and a.AuditState<>1 and GuidFlow='" + ic + "' ");
        }

        public DataTable getContPayAuditRecords(string yhdm)
        {
            string str = "";
            str = "SELECT (SELECT top 1 [ContractName]   FROM [EPM_Con_ContractMain] where [ContractCode]=a.ContractCode) as ContractName,a.*,b.ContractType, CASE WHEN b.SumPayMoney IS NULL THEN 0 ELSE b.SumPayMoney END AS SumPayMoney FROM EPM_Fund_ContractPay a INNER JOIN EPM_V_Fund_ContListForPay b ON a.ContractCode = b.ContractCode  ";
            return publicDbOpClass.DataTableQuary(str + " and a.AuditState<>1 ");
        }

        public DataTable getContPayAuditRecords(string PrjCode, string yhdm)
        {
            string sqlString = "";
            if (PrjCode == "0000000000")
            {
                sqlString = "SELECT (SELECT top 1 [ContractName]   FROM [EPM_Con_ContractMain] where [ContractCode]=a.ContractCode) as ContractName, a.*,b.ContractType, CASE WHEN b.SumPayMoney IS NULL THEN 0 ELSE b.SumPayMoney END AS SumPayMoney FROM EPM_Fund_ContractPay a INNER JOIN EPM_V_Fund_ContListForPay b ON a.ContractCode = b.ContractCode  ";
                sqlString = sqlString + " and a.AuditState<>1";
            }
            else
            {
                sqlString = ("SELECT (SELECT top 1 [ContractName]   FROM [EPM_Con_ContractMain] where [ContractCode]=a.ContractCode) as ContractName, a.*,b.ContractType, CASE WHEN b.SumPayMoney IS NULL THEN 0 ELSE b.SumPayMoney END AS SumPayMoney FROM EPM_Fund_ContractPay a INNER JOIN EPM_V_Fund_ContListForPay b ON a.ContractCode = b.ContractCode where a.PrjCode = '" + PrjCode + "' ") + " and a.AuditState<>1";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable getContPayRecords(string PrjCode, string ContractCode, string yhdm)
        {
            string sqlString = "";
            sqlString = "select *  from EPM_Fund_ContractPay  where  (1=1) ";
            if (PrjCode != "")
            {
                sqlString = sqlString + " and PrjCode = '" + PrjCode + "'";
            }
            if (ContractCode != "")
            {
                sqlString = sqlString + " and ContractCode = '" + ContractCode + "'";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetContractInfo(string contCode)
        {
            return publicDbOpClass.DataTableQuary("select *  from EPM_V_Fund_ContListForPay where contractcode ='" + contCode + "'");
        }

        public int GetContractTypebyContCode(string contCode)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select ContractType from EPM_Con_ContractMain where contractcode='" + contCode + "'"));
        }

        public ContractPayInfo getOneContPayRecord(Guid GuidFlow)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from  EPM_Fund_ContractPay where GuidFlow = '" + GuidFlow + "'");
            return new ContractPayInfo { Reason = table.Rows[0]["Reason"].ToString(), ProductValue = Convert.ToDecimal(table.Rows[0]["ProductValue"].ToString()), PayPercent = Convert.ToDecimal(table.Rows[0]["PayPercent"].ToString()), PayMoney = Convert.ToDecimal(table.Rows[0]["PayMoney"].ToString()), PayMode = table.Rows[0]["PayMode"].ToString(), PayDate = Convert.ToDateTime(table.Rows[0]["PayDate"].ToString()), PayType = table.Rows[0]["PayType"].ToString(), PayCompany = Convert.ToInt32(table.Rows[0]["PayCompany"].ToString()), BankAccounts = table.Rows[0]["BankAccounts"].ToString(), AccountBank = table.Rows[0]["AccountBank"].ToString() };
        }

        public DataTable GetPaymentMainInfo(string keyID)
        {
            return publicDbOpClass.DataTableQuary(" select * from EPM_Fund_ContractPay where  guidflow ='" + keyID + "'");
        }

        public bool insContPayRecord(ContractPayInfo cpi)
        {
            string str = "";
            object obj2 = str + "insert into EPM_Fund_ContractPay (PrjCode,ContractCode,Reason,ProductValue,PayPercent,PayMoney,PayMode,PayDate,PayType,PayCompany,BankAccounts,AccountBank,ApplyUser) values (";
            object obj3 = string.Concat(new object[] { obj2, "'", cpi.PrjCode, "','", cpi.ContractCode, "','", cpi.Reason, "'," });
            object obj4 = string.Concat(new object[] { obj3, "cast('", cpi.ProductValue, "' as decimal(18,2)),cast('", cpi.PayPercent, "' as decimal(18,2)),cast('", cpi.PayMoney, "' as decimal(18,2)),'", cpi.PayMode, "'," });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj4, "'", cpi.PayDate, "','", cpi.PayType, "',", cpi.PayCompany, ",'", cpi.BankAccounts, "','", cpi.AccountBank, "','", cpi.ApplyUser, "')" }));
        }

        public bool updConfirmMoney(Guid GuidFlow, string ConfirmMoney)
        {
            object obj2 = "update EPM_Fund_ContractPay set ConfirmMoney = cast('" + ConfirmMoney + "' as decimal(18,2)) ";
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj2, " where GuidFlow = '", GuidFlow, "'" }));
        }

        public bool updContPayRecord(ContractPayInfo cpi)
        {
            object obj2 = "update EPM_Fund_ContractPay set Reason = '" + cpi.Reason + "',";
            object obj3 = string.Concat(new object[] { obj2, "ProductValue = cast('", cpi.ProductValue, "' as decimal(18,2))," });
            object obj4 = string.Concat(new object[] { obj3, "PayPercent = ", cpi.PayPercent, "," });
            object obj5 = (string.Concat(new object[] { obj4, "PayMoney = cast('", cpi.PayMoney, "' as decimal(18,2))," }) + "PayMode = '" + cpi.PayMode + "',") + "PayType = '" + cpi.PayType + "',";
            object obj6 = (string.Concat(new object[] { obj5, "PayCompany = ", cpi.PayCompany, "," }) + "BankAccounts = '" + cpi.BankAccounts + "',") + "AccountBank = '" + cpi.AccountBank + "'";
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj6, " where GuidFlow = '", cpi.GuidFlow, "'" }));
        }
    }
}

