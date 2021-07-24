namespace cn.justwin.opm
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class ContractPaidAction
    {
        public string GetContractID(string contractCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select contractID from Con_Payout_Contract where contractCode='" + contractCode + "'");
            string str2 = string.Empty;
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            return str2;
        }

        public DataTable getContractPaid(string prjID, string contractcode, string paidState)
        {
            string sqlString = "select a.* ,b.ContractCode,b.ContractName from Con_Payout_Payment a,Con_Payout_Contract b  where a.ContractID=b.ContractID and a.FlowState='1' ";
            if ((prjID != "") && (prjID != "''"))
            {
                sqlString = sqlString + "  and b.prjguid in (" + prjID + ") ";
            }
            if (contractcode != "")
            {
                sqlString = sqlString + " and b.ContractCode like '%" + contractcode + "%'";
            }
            if (paidState == "1")
            {
                sqlString = sqlString + " and a.PaidMoney >0";
            }
            else if (paidState == "2")
            {
                sqlString = sqlString + " and (a.PaidMoney <=0 or a.PaidMoney is null) ";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable getContractPaidByID(string PaymentCodes)
        {
            string str = "select a.id ,b.ContractCode,b.ContractName , a.PaymentCode ,a.PaymentMoney ,a.PaymentPerson ,a.PaidMoney ,a.PaidUser  from Con_Payout_Payment a,Con_Payout_Contract b  where a.ContractID=b.ContractID ";
            return publicDbOpClass.DataTableQuary(str + "  and a.ID in (" + PaymentCodes + ") and a.FlowState='1'");
        }

        public DataTable GetTableDescription(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT a.MaintainID N'name', isnull(g.[value],'') AS N'value'");
            builder.Append(" FROM OPM_Maintain_Data a inner join OPM_Maintain d ");
            builder.Append(" on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' ");
            builder.Append(" left join sys.extended_properties   g ");
            builder.Append(" on a.id=g.major_id AND a.colid = g.minor_id ");
            builder.Append(" where d.name='" + tableName + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int UpdateContractPaid(string PaymentCode, string contractID, decimal paidMoney, string PaidUser)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update Con_Payout_Payment SET");
            builder.Append(" PaidMoney=");
            builder.Append(paidMoney + ",");
            builder.Append(" PaidUser=");
            builder.Append(PaidUser ?? "");
            builder.Append(" where ");
            builder.Append("contractID=");
            builder.Append("'" + contractID + "'");
            builder.Append("and");
            builder.Append(" PaymentCode=");
            builder.Append("'" + PaymentCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

