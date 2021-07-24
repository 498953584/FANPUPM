namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class Fund_Prj_Accoun
    {
        public static DataTable CombineTheSameDatatable(DataTable DataTable1, DataTable DataTable2)
        {
            DataTable table = DataTable1.Clone();
            object[] array = new object[table.Columns.Count];
            for (int i = 0; i < DataTable1.Rows.Count; i++)
            {
                DataTable1.Rows[i].ItemArray.CopyTo(array, 0);
                table.Rows.Add(array);
            }
            for (int j = 0; j < DataTable2.Rows.Count; j++)
            {
                DataTable2.Rows[j].ItemArray.CopyTo(array, 0);
                table.Rows.Add(array);
            }
            return table;
        }

        public DataTable ContIncomeRZByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("select (select PrjName from PT_PrjInfo where PrjGuid=accin.PrjGuid)as PrjName, ");
                        builder2.Append("accin.[GetDate] AS OccurredDate,accin.inpeople,ISNULL(accin.INMoney ,0.00) AS INMoney,accin.GetMoney as Inm, ");
                        builder2.Append("(select ContractName from Con_Incomet_Contract where ContractID=(select  ContractID FROM Con_Incomet_Payment WHERE id=accin.ContractID)) as ContName,accin.remark ");
                        builder2.Append("from Fund_Prj_Accoun_Income as accin where accin.Subject=0 and accin.FlowState=1");
                        builder2.Append(" and accin.PrjGuid='" + str + "' ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }

        public DataTable getAccounSumInfo(string strwhere)
        {
            string str = "(InitialFundsum+OtherIncome+ContIncomeRZ+Loan) AS FundSource";
            string str2 = "isnull(Account.initialFund,0.00)+(select isnull(sum(INMoney),0.00) from Fund_Prj_Accoun_Income where Subject='1' And Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FlowState=1) AS InitialFundsum ";
            string str3 = "(select isnull(sum(INMoney),0.00) from Fund_Prj_Accoun_Income where Subject='2' And Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FlowState=1)   AS OtherIncome ";
            string str4 = "(select isnull(sum(INMoney),0.00) from Fund_Prj_Accoun_Income where Subject='0' And Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FlowState=1  ) as ContIncomeRZ ";
            string str5 = "(SELECT  isnull(sum(LoanFund),0) FROM  [Fund_Prj_Loan] where Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FlowState=1)AS Loan ";
            string str6 = "(ReturnLoanBJ+ReturnLoanOther+MustPaidCont+MustPaidOtherCost) AS FundPayout";
            string str7 = "(select isnull(sum(FR_Money),0.00) from  Fund_Prj_Loan_Return AS a INNER JOIN   Fund_Prj_Loan AS b ON a.LoanID = b.LoanID where Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FR_flowState=1) AS ReturnLoanBJ ";
            string str8 = "(select isnull(sum(FR_interest),0.00)+isnull(sum(FR_deduct),0.00)  from  Fund_Prj_Loan_Return AS a INNER JOIN   Fund_Prj_Loan AS b ON a.LoanID = b.LoanID where Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FR_flowState=1) AS ReturnLoanOther ";
            string str9 = "(select  isnull(sum(PaymentMoney),0) from  Con_Payout_Payment  a  INNER JOIN   Con_Payout_Contract b ON a.ContractID = b.ContractID  where  Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and a.FlowState=1 ) AS MustPaidCont ";
            string str10 = "(SELECT isnull(SUM(Amount),0) AS qtqk FROM Bud_IndirectDiaryDetails AS D  LEFT JOIN Bud_IndirectDiaryCost AS M ON D.InDiaryId=M.InDiaryId   WHERE account.PrjGuid like '%'+CONVERT(VARCHAR(50),ProjectId)+'%' AND FlowState='1') AS MustPaidOtherCost ";
            string str11 = "(InitialFundsum+OtherIncome+ContIncomeRZ+Loan)-(ReturnLoanBJ+ReturnLoanOther+MustPaidCont+MustPaidOtherCost)  ";
            string str12 = "(MustPaidCont-PaidCont) AS UnpaidCont   ";
            string str13 = "(MustPaidOtherCost-PaidOtherCost) AS  UnpaidOtherCost  ";
            string str14 = "(select isnull(sum(PayOutMoney),0.00) from Fund_Prj_Accoun_Payout where Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FloeState=1 and Type=0) AS PaidCont   ";
            string str15 = "(select isnull(sum(PayOutMoney),0.00) from Fund_Prj_Accoun_Payout where Account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FloeState=1 and Type=1) AS  PaidOtherCost  ";
            string str16 = str11 + "+ (MustPaidCont - PaidCont) + (MustPaidOtherCost - PaidOtherCost) AS  JE";
            string sqlString = "select *, " + str + " ," + str6 + " ," + str11 + " as UsableJE," + str12 + " ," + str13 + " ," + str16 + "  from (select account.*," + str2 + " ," + str3 + " ," + str4 + " ," + str5 + " ," + str7 + " ," + str8 + " ," + str9 + " ," + str10 + " ," + str14 + " ," + str15 + "   from Fund_Prj_Accoun as Account)as A where 1=1 ";
            if ((strwhere != null) && (strwhere != ""))
            {
                sqlString = sqlString + strwhere;
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public string getAccountByPrjGuid(string PrjGuid)
        {
            return publicDbOpClass.DataTableQuary("select Accountid from Fund_Prj_Accoun where PrjGuid like '%" + PrjGuid + "%'").Rows[0][0].ToString();
        }

        public DataTable GetPrjNameByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("SELECT ");
                        builder2.Append("ppi.PrjGuid,ppi.PrjName ");
                        builder2.Append("FROM PT_PrjInfo ppi ");
                        builder2.Append("WHERE  ppi.prjGuid='").Append(str).Append("' ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }

        public DataTable initialFundByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            new DataSet();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* , (select v_xm  FROM PT_yhmc where v_yhdm=fpa.createMan ) as v_xm FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count <= 0)
            {
                return table2;
            }
            if (table.Rows[0]["PrjGuid"] != null)
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("SELECT  1 as sxh, fpai.InDate AS OccurredDate").Append(" ");
                        builder2.Append(",ppi.PrjName");
                        builder2.Append(",py.v_xm").Append(" ");
                        builder2.Append(",fpai.Remark AS RemarkText").Append(" ");
                        builder2.Append(",ISNULL(INMoney ,0.00) INMoney").Append(" ");
                        builder2.Append("FROM   Fund_Prj_Accoun_Income fpai").Append(" ");
                        builder2.Append("LEFT JOIN PT_PrjInfo ppi").Append(" ");
                        builder2.Append("ON CONVERT(VARCHAR(64), ppi.PrjGuid) = CONVERT(VARCHAR(64),fpai.PrjGuid)").Append(" ");
                        builder2.Append("LEFT JOIN PT_yhmc py").Append(" ");
                        builder2.Append("ON  py.v_yhdm LIKE '%'+fpai.InPeople+'%'").Append(" ");
                        builder2.Append("  WHERE CONVERT(VARCHAR(64),fpai.PrjGuid) ='" + str + "'AND fpai.[Subject]=1 and fpai.FlowState=1 ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            if (table2.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                table2.Rows.Add(new object[] { 0, row["creatDate"], row["acountName"], row["v_xm"], "账户初始启动资金", row["initialFund"] });
            }
            else
            {
                table2 = publicDbOpClass.DataTableQuary(("SELECT 0 as sxh, creatDate as OccurredDate,acountName as PrjName ,(select v_xm  FROM PT_yhmc where v_yhdm=createMan ) as v_xm ,'账户初始启动资金' as RemarkText , initialFund as INMoney FROM Fund_Prj_Accoun WHERE AccountID='" + _AccountID + "'").ToString());
            }
            DataView defaultView = table2.DefaultView;
            defaultView.Sort = "sxh  asc";
            return defaultView.ToTable();
        }

        public DataTable LoanFundByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("SELECT fpl.LoanCode");
                        builder2.Append(",fpl.LoanDate AS OccurredDate ");
                        builder2.Append(",fpl.LoanFund ");
                        builder2.Append(",fpl.ReturnState ");
                        builder2.Append(",ppi.PrjName,ppi.PrjGuid ");
                        builder2.Append(",py.v_xm ,fpl.Remark ");
                        builder2.Append("FROM   Fund_Prj_Loan fpl ");
                        builder2.Append("LEFT JOIN PT_PrjInfo ppi ");
                        builder2.Append(" ON  ppi.PrjGuid = fpl.PrjGuid ");
                        builder2.Append("LEFT JOIN PT_yhmc py ON py.v_yhdm=fpl.LoanMan ");
                        builder2.Append("WHERE FlowState=1 AND fpl.PrjGuid ='").Append(str).Append("' ");
                        builder2.Append(" ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }

        public DataTable MustPaidContByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("SELECT cpp.PaymentMoney ,cpc.PrjGuid ,cpp.PaymentPerson AS v_xm ");
                        builder2.Append(",cpp.PaymentDate AS OccurredDate ,cpc.ContractName,ppi.PrjName,cpc.Notes ");
                        builder2.Append(" FROM Con_Payout_Payment cpp ");
                        builder2.Append(" LEFT JOIN Con_Payout_Contract cpc ON  cpc.ContractID = cpp.ContractID ");
                        builder2.Append(" LEFT JOIN PT_PrjInfo ppi ON ppi.PrjGuid=cpc.PrjGuid ");
                        builder2.Append(" WHERE cpp.FlowState = 1 AND  CONVERT(VARCHAR(64) ,cpc.PrjGuid) ='").Append(str).Append("' ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }

        public DataTable MustPaidOtherCostByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("SELECT bidd.Amount");
                        builder2.Append(" ,ppi.PrjName,ppi.PrjGuid,bidc.Name,bidc.InputUser,bidc.InputDate AS OccurredDate ");
                        builder2.Append(" FROM   Bud_IndirectDiaryDetails bidd ");
                        builder2.Append(" LEFT JOIN Bud_IndirectDiaryCost bidc ");
                        builder2.Append(" ON  bidd.InDiaryId = bidc.InDiaryId ");
                        builder2.Append(" LEFT JOIN PT_PrjInfo ppi ");
                        builder2.Append(" ON  ppi.PrjGuid = bidc.ProjectId ");
                        builder2.Append(" WHERE bidc.FlowState=1 AND  bidc.ProjectId ='").Append(str).Append("' ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }

        public DataTable OtherIncomeByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            new DataSet();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("SELECT fpai.InDate AS OccurredDate").Append(" ");
                        builder2.Append(",ppi.PrjName,ppi.PrjGuid");
                        builder2.Append(",py.v_xm").Append(" ");
                        builder2.Append(",fpai.Remark AS RemarkText").Append(" ");
                        builder2.Append(",ISNULL(INMoney ,0.00) INMoney").Append(" ");
                        builder2.Append("FROM   Fund_Prj_Accoun_Income fpai").Append(" ");
                        builder2.Append("LEFT JOIN PT_PrjInfo ppi").Append(" ");
                        builder2.Append("ON CONVERT(VARCHAR(64), ppi.PrjGuid) = CONVERT(VARCHAR(64),fpai.PrjGuid)").Append(" ");
                        builder2.Append("LEFT JOIN PT_yhmc py").Append(" ");
                        builder2.Append("ON  py.v_yhdm LIKE '%'+fpai.InPeople+'%'").Append(" ");
                        builder2.Append("  WHERE CONVERT(VARCHAR(64),fpai.PrjGuid) ='" + str + "'AND fpai.[Subject]=2 ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }

        public DataTable PaidContByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("select (select PrjName from PT_PrjInfo where PrjGuid=accout.PrjGuid)as PrjName,");
                        builder2.Append("(select ContractName from Con_PayOut_Contract where ContractID=(select  ContractID FROM Con_PayOut_Payment WHERE id=accout.RpGuid)) as ContName,");
                        builder2.Append("accout.PayOutTime AS OccurredDate,accout.PayOutPeople,isnull(accout.PayOutMoney,0.00) as PayOutMoney,accout.remark ");
                        builder2.Append("from Fund_Prj_Accoun_Payout as accout where accout.Type=0 and FloeState=1 and accout.PrjGuid='" + str + "' ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }

        public DataTable PaidOtherCostByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("SELECT ");
                        builder2.Append("ppi.PrjName,ppi.PrjGuid,fpap.PayOutTime AS OccurredDate ,fpap.PayOutPeople ,ISNULL(SUM(PayOutMoney) ,0.00) AS PayOutMoney,py.v_xm AS InputUser,bidc.Name ");
                        builder2.Append("FROM   Fund_Prj_Accoun_Payout fpap ");
                        builder2.Append(" LEFT JOIN Bud_IndirectDiaryCost bidc  ON bidc.ProjectId=fpap.prjGuid  AND bidc.InDiaryId=fpap.RPGuid ");
                        builder2.Append("LEFT JOIN PT_PrjInfo ppi ");
                        builder2.Append(" ON  fpap.prjGuid = ppi.PrjGuid  LEFT JOIN PT_yhmc py ON py.v_yhdm =fpap.PayOutPeople ");
                        builder2.Append("WHERE  fpap.prjGuid='").Append(str).Append("' AND fpap.[Type]=1 AND fpap.FloeState=1 ");
                        builder2.Append("GROUP BY bidc.Name,ppi.PrjName,ppi.PrjGuid ,PayOutPeople,fpap.PayOutTime ,py.v_xm ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }

        public DataTable ReturnLoanByAccountID(string _AccountID)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT fpa.* FROM Fund_Prj_Accoun fpa ");
            builder.Append("WHERE fpa.AccountID='");
            builder.Append(_AccountID);
            builder.Append("'");
            table = publicDbOpClass.DataTableQuary(builder.ToString());
            if ((table.Rows.Count > 0) && (table.Rows[0]["PrjGuid"] != null))
            {
                string[] strArray = table.Rows[0]["PrjGuid"].ToString().Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].ToString() != "")
                    {
                        string str = strArray[i].ToString().Replace("'", "");
                        StringBuilder builder2 = new StringBuilder();
                        builder2.Append("SELECT fplr.*,fplr.FR_data AS OccurredDate,py.v_xm,ppi.PrjName,fpl.LoanCode, ppi.PrjGuid ");
                        builder2.Append(" FROM Fund_Prj_Loan_Return fplr ");
                        builder2.Append(" LEFT JOIN Fund_Prj_Loan fpl ON  fpl.LoanID = fplr.LoanID ");
                        builder2.Append(" LEFT JOIN PT_PrjInfo ppi ON  ppi.PrjGuid = fpl.PrjGuid");
                        builder2.Append(" LEFT JOIN PT_yhmc py ON  py.v_yhdm LIKE '%'+fplr.FR_name+'%' ");
                        builder2.Append(" WHERE CONVERT(VARCHAR(64), fpl.PrjGuid) ='").Append(str).Append("'  AND fplr.FR_flowState = 1 ");
                        DataTable table3 = new DataTable();
                        table3 = publicDbOpClass.DataTableQuary(builder2.ToString());
                        if (table2.Rows.Count == 0)
                        {
                            table2 = table3.Clone();
                        }
                        table2 = CombineTheSameDatatable(table2, table3);
                    }
                }
            }
            return table2;
        }
    }
}

