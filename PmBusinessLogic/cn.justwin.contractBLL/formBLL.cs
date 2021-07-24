namespace cn.justwin.contractBLL
{
    using cn.justwin.DAL;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class formBLL
    {
        public static DataTable ContractReport(string PrjId, string StartDate)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("with total as (\r\nselect prjGuid,prjName,StartDate,ContractCode,ContractName,InContractID,Party,TypeID,ISNULL(ContractPrice,0.000) ContractPrice ,\r\nISNULL(ChangePrice,0.000) ChangePrice ,ISNULL(ClearingPrice,0.000) ClearingPrice , ISNULL(CllectionPrice,0.000) CllectionPrice ,\r\nContractCode1,ContractName1,TypeID1,TypeName,\r\nISNULL(ContractMoney,0.000) ContractMoney ,ISNULL(ModifiedMoney,0.000)ModifiedMoney ,ISNULL(BalanceMoney,0.000)BalanceMoney ,\r\nISNULL(PaymentMoney,0.000) PaymentMoney,ISNULL(ApplyAmount,0.000) ApplyAmount ,ISNULL(ChangePrice,0.000)-ISNULL(ApplyAmount,0.000) AS Profit from (\r\nselect shouru.*,zhichu.InContractID AS InContractID,zhichu.ContractCode as ContractCode1,zhichu.ContractName as ContractName1,\r\nzhichu.TypeID AS TypeID1,\r\nzhichu.TypeName as TypeName,zhichu.ContractMoney as ContractMoney,zhichu.ModifiedMoney as ModifiedMoney,\r\nzhichu.BalanceMoney as BalanceMoney,zhichu.PaymentMoney as PaymentMoney FROM\r\n(\r\n--收入合同\r\nselect prjGuid,prjName,INCOMET.ContractID,ContractCode,ContractName,Party,TypeID,StartDate,ContractPrice,ChangePrice,\r\nClearingPrice,CllectionPrice,ApplyAmount from (\r\nselect prjGuid,prjName,Incomet.ContractID,ContractCode,ContractName,TypeID,Party,Prj.StartDate,ContractPrice, \r\nISNULL(sum(ChangePrice),0.000)+ContractPrice as ChangePrice  from Con_Incomet_Contract Incomet \r\nleft join PT_PrjInfo Prj on Incomet.Project=Prj.prjGuid \r\nleft join Con_Incomet_Modify InModify on InModify.ContractID=Incomet.ContractID where IsArchived='0'");
            string str = ConfigHelper.Get("IsIncomeContractApprove");
            if (str == "1")
            {
                builder.Append("  AND FlowState=1  ");
            }
            builder.Append(" group by prjGuid,prjName,Incomet.ContractID,ContractCode,ContractName,TypeID,Party,ContractPrice,Prj.StartDate ) as INCOMET\r\nLEFT JOIN ( SELECT Incomet.ContractID ,ISNULL(SUM(ClearingPrice),0.000) ClearingPrice  from Con_Incomet_Contract   Incomet\r\nleft join  Con_Incomet_Balance on Con_Incomet_Balance.ContractID=Incomet.ContractID  where IsArchived='0'\r\ngroup by Incomet.ContractID) AS CP ON CP.ContractID=INCOMET.ContractID \r\nLEFT JOIN (SELECT Incomet.ContractID ,ISNULL(SUM(CllectionPrice),0.000) CllectionPrice  from Con_Incomet_Contract   Incomet\r\nleft join  Con_Incomet_Payment on Con_Incomet_Payment.ContractID=Incomet.ContractID  where IsArchived='0'\r\ngroup by Incomet.ContractID) AS CLP ON CLP.ContractID=INCOMET.ContractID\r\nLEFT JOIN (select ContractId, ISNULL(SUM(PaymentAmount),0.000) as ApplyAmount from Con_Income_PaymentApply where FlowState=1\r\ngroup by ContractId ) AS Apply ON Apply.ContractId=INCOMET.ContractID--收入合同结束\r\n) as shouru\r\nleft join (\r\n--支出合同\r\nselect PAYOUT.ContractID,ContractCode,ContractName,TypeID,TypeName,InContractID,ContractMoney,ModifiedMoney ,ISNULL(BalanceMoney,0.000) BalanceMoney, ISNULL(PaymentMoney,0.000) PaymentMoney \r\nFROM (\r\nselect p.ContractID,ContractCode,ContractName,p.TypeID,TypeName,InContractID,ContractMoney,ModifiedMoney from Con_Payout_Contract as p \r\nleft join Con_ContractType as c on p.TypeID=c.TypeID ) AS PAYOUT\r\nleft join ( SELECT ContractID,sum(BalanceMoney) BalanceMoney from Con_Payout_Balance where FlowState=1 group by ContractID) AS BM \r\nON BM.ContractID=PAYOUT.ContractID\r\nLEFT JOIN (SELECT ContractID,sum(PaymentMoney) PaymentMoney from Con_Payout_Payment where FlowState=1 group by ContractID) AS PM \r\nON PM.ContractID=PAYOUT.ContractID --支出合同结束\r\n) as zhichu on zhichu.InContractID=shouru.ContractID ) as aa \r\n), Shouru as (\r\nSELECT prjGuid,prjName,sum(ContractPrice) ContractPrice,sum(ChangePrice) ChangePrice,sum(ClearingPrice) ClearingPrice,\r\nsum(CllectionPrice) CllectionPrice ,ISNULL(sum(ApplyAmount),0.000) ApplyAmount,sum(ChangePrice)-ISNULL(sum(ApplyAmount),0.000) as Profit from (\r\nselect prjGuid,prjName,INCOMET.ContractID,ContractCode,ContractName,Party,TypeID,StartDate,ContractPrice,ChangePrice,\r\nClearingPrice,CllectionPrice,ApplyAmount from (\r\nselect prjGuid,prjName,Incomet.ContractID,ContractCode,ContractName,TypeID,Party,Prj.StartDate,ContractPrice, \r\nISNULL(sum(ChangePrice),0.000)+ContractPrice as ChangePrice  from Con_Incomet_Contract Incomet \r\nleft join PT_PrjInfo Prj on Incomet.Project=Prj.prjGuid \r\nleft join Con_Incomet_Modify InModify on InModify.ContractID=Incomet.ContractID where IsArchived='0' ");
            if (str == "1")
            {
                builder.Append("  AND FlowState=1  ");
            }
            builder.Append("group by prjGuid,prjName,Incomet.ContractID,ContractCode,ContractName,TypeID,Party,ContractPrice,Prj.StartDate ) as INCOMET\r\nLEFT JOIN ( SELECT Incomet.ContractID ,ISNULL(SUM(ClearingPrice),0.000) ClearingPrice  from Con_Incomet_Contract   Incomet\r\nleft join  Con_Incomet_Balance on Con_Incomet_Balance.ContractID=Incomet.ContractID  where IsArchived='0'\r\ngroup by Incomet.ContractID) AS CP ON CP.ContractID=INCOMET.ContractID \r\nLEFT JOIN (SELECT Incomet.ContractID ,ISNULL(SUM(CllectionPrice),0.000) CllectionPrice  from Con_Incomet_Contract   Incomet\r\nleft join  Con_Incomet_Payment on Con_Incomet_Payment.ContractID=Incomet.ContractID  where IsArchived='0'\r\ngroup by Incomet.ContractID) AS CLP ON CLP.ContractID=INCOMET.ContractID \r\nLEFT JOIN (select ContractId, ISNULL(SUM(PaymentAmount),0.000) as ApplyAmount from Con_Income_PaymentApply  where FlowState=1\r\ngroup by ContractId ) AS Apply ON Apply.ContractId=INCOMET.ContractID ) as Shouru group by prjGuid,prjName)\r\n\r\nselect * from \r\n(\r\nselect prjGuid,prjName,StartDate,ContractCode,ContractName,Party,TypeID,ContractPrice,ChangePrice,ClearingPrice,CllectionPrice,ApplyAmount,Profit,\r\nContractCode1,ContractName1,TypeID1,TypeName,ContractMoney,ModifiedMoney,BalanceMoney,PaymentMoney,'0' as ContractChazhi,'0' as\r\n ChangeChazhi,'0' as ClearingChazhi,'0' as CllectionChazhi from total  \r\n where 1=1\r\nunion \r\nselect prjGuid,prjName,null,'小计','小计',null,null,ContractPrice,ChangePrice,ClearingPrice,CllectionPrice,ApplyAmount,Profit,'小计','小计',null,'小计',ContractMoney,ModifiedMoney, BalanceMoney,PaymentMoney,ContractPrice-ContractMoney as ContractChazhi,ChangePrice-ModifiedMoney as ChangeChazhi,ClearingPrice-BalanceMoney as ClearingChazhi,CllectionPrice-PaymentMoney as CllectionChazhi from (\r\nselect prjGuid,prjName,sum(ContractPrice) ContractPrice,sum(ChangePrice) ChangePrice,sum(ClearingPrice) ClearingPrice,sum(CllectionPrice) CllectionPrice,sum(ApplyAmount) ApplyAmount,sum(Profit) Profit,sum(ContractMoney) ContractMoney,sum(ModifiedMoney) ModifiedMoney,sum(BalanceMoney) BalanceMoney,sum(PaymentMoney) PaymentMoney from (\r\nselect prjGuid,prjName,ContractPrice,ChangePrice,ClearingPrice,CllectionPrice,ApplyAmount,Profit,'0' as ContractMoney,'0' as ModifiedMoney,'0' as BalanceMoney,'0' as PaymentMoney from Shouru\r\nunion \r\nselect prjGuid,prjName,'0' as ContractPrice,'0' as ChangePrice,'0' as ClearingPrice,'0' as CllectionPrice,'0' as ApplyAmount,'0' as Profit,sum(ContractMoney) ContractMoney,sum(ModifiedMoney) ModifiedMoney,sum(BalanceMoney) BalanceMoney,sum(PaymentMoney) PaymentMoney from total group by prjGuid,prjName\r\n) as xiaoji group by prjGuid,prjName) as xx\r\nunion\r\nselect null,'合计',null,'合计','合计',null,null,sum(ContractPrice) ContractPrice,sum(ChangePrice) ChangePrice,sum(ClearingPrice) ClearingPrice,sum(CllectionPrice) CllectionPrice,sum(ApplyAmount) ApplyAmount,sum(Profit) Profit,'合计','合计',null,'合计',sum(ContractMoney) ContractMoney,sum(ModifiedMoney) ModifiedMoney,sum(BalanceMoney) BalanceMoney,sum(PaymentMoney) PaymentMoney,sum(ContractPrice)-sum(ContractMoney) as ContractChazhi,sum(ChangePrice)-sum(ModifiedMoney) as ChangeChazhi,sum(ClearingPrice)-sum(BalanceMoney) as ClearingChazhi,sum(CllectionPrice)-sum(PaymentMoney) as CllectionChazhi from (\r\nselect prjGuid,prjName,sum(ContractPrice) ContractPrice,sum(ChangePrice) ChangePrice,sum(ClearingPrice) ClearingPrice,sum(CllectionPrice) CllectionPrice,sum(ApplyAmount) ApplyAmount,sum(Profit) Profit,sum(ContractMoney) ContractMoney,sum(ModifiedMoney) ModifiedMoney,sum(BalanceMoney) BalanceMoney,sum(PaymentMoney) PaymentMoney from (\r\nselect prjGuid,prjName,ContractPrice,ChangePrice,ClearingPrice,CllectionPrice,ApplyAmount,Profit,'0' as ContractMoney,'0' as ModifiedMoney,'0' as BalanceMoney,'0' as PaymentMoney from Shouru\r\nunion \r\nselect prjGuid,prjName,'0' as ContractPrice,'0' as ChangePrice,'0' as ClearingPrice,'0' as CllectionPrice,'0' as ApplyAmount,'0' as Profit,sum(ContractMoney) ContractMoney,sum(ModifiedMoney) ModifiedMoney,sum(BalanceMoney) BalanceMoney,sum(PaymentMoney) PaymentMoney from total group by prjGuid,prjName\r\n) as xiaoji group by prjGuid,prjName) as yy\r\n) AS a  where 1=1 ");
            if (!string.IsNullOrEmpty(PrjId))
            {
                builder.AppendFormat(" and prjName like '%{0}%' ", PrjId);
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                builder.AppendFormat(" and StartDate like '%{0}%' ", StartDate);
            }
            builder.Append(" order by prjGuid desc,StartDate desc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public decimal GetIncoBack(string incomeCode)
        {
            DataTable table = DBHelper.GetTable("select sum(CllectionPrice) from Con_Incomet_Payment where contractID='" + incomeCode + "'");
            decimal num = 0M;
            if (((table.Rows.Count > 0) && (table.Rows[0][0].ToString() != null)) && (table.Rows[0][0].ToString() != ""))
            {
                num = Convert.ToDecimal(table.Rows[0][0].ToString());
            }
            return num;
        }

        public decimal GetIncoBalance(string incomeCode)
        {
            DataTable table = DBHelper.GetTable("select sum(ClearingPrice) from Con_Incomet_Balance where contractID='" + incomeCode + "'");
            decimal num = 0M;
            if (((table.Rows.Count > 0) && (table.Rows[0][0].ToString() != null)) && (table.Rows[0][0].ToString() != ""))
            {
                num = Convert.ToDecimal(table.Rows[0][0].ToString());
            }
            return num;
        }

        public static int GetIncometPayoutConCount(string prjId, string conCode, string conName, string conType)
        {
            StringBuilder builder = new StringBuilder();
            string str = ConfigHelper.Get("IsIncomeContractApprove");
            List<SqlParameter> list = new List<SqlParameter>();
            if (str == "1")
            {
                builder.Append(" AND FlowState=1 ");
            }
            if (!string.IsNullOrEmpty(conCode))
            {
                builder.AppendFormat(" AND ContractCode LIKE '%'+@conCode+'%' ", new object[0]);
                SqlParameter item = new SqlParameter("@conCode", conCode);
                list.Add(item);
            }
            if (!string.IsNullOrEmpty(conName))
            {
                builder.Append(" AND ContractName LIKE '%'+@conName+'%' ");
                SqlParameter parameter2 = new SqlParameter("@conName", conName);
                list.Add(parameter2);
            }
            if (!string.IsNullOrEmpty(conType))
            {
                builder.Append(" AND TypeName LIKE '%'+@conType+'%' ");
                SqlParameter parameter3 = new SqlParameter("@conType", conType);
                list.Add(parameter3);
            }
            list.Add(new SqlParameter("@PrjGuid", prjId));
            StringBuilder builder2 = new StringBuilder();
            builder2.AppendFormat("\r\n            --DECLARE @PrjGuid NVARCHAR(50);\r\n            --SET @PrjGuid='A69601A0-8AAA-4D80-9609-CA32139D72E9'\t\r\n            SELECT COUNT(*) FROM \r\n            (\t\r\n\t            SELECT ContractCode,ContractName,TypeName,ContractPrice,FlowState,ContractId\r\n\t            ,case isFContract \r\n\t\t            when 1 then ContractId \r\n\t\t            when 0 then Fcode+ContractId \r\n\t            END AS Version\r\n\t            ,case isFContract \r\n\t\t            WHEN '1' THEN RegisterTime \r\n\t\t            WHEN '0' THEN ( SELECT RegisterTime FROM Con_Incomet_Contract  \r\n\t\t\t\t\t              WHERE ContractID = (IncometCon.FCode)) \r\n\t            END AS Date,isFContract,FCode\r\n\t            FROM Con_Incomet_Contract IncometCon INNER JOIN Con_ContractType ConType \r\n\t            ON IncometCon.TypeId=ConType.TypeId\r\n\t            WHERE Project=@PrjGuid {0}\r\n            ) Con", builder);
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder2.ToString(), list.ToArray()));
        }

        public static DataTable GetIncometPayoutConInfo(string prjId, string conCode, string conName, string conType, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = GetIncometPayoutConCount(prjId, conCode, conName, conType);
            }
            StringBuilder builder = new StringBuilder();
            string str = ConfigHelper.Get("IsIncomeContractApprove");
            List<SqlParameter> list = new List<SqlParameter>();
            if (str == "1")
            {
                builder.Append(" AND FlowState=1 ");
            }
            if (!string.IsNullOrEmpty(conCode))
            {
                builder.AppendFormat(" AND ContractCode LIKE '%'+@conCode+'%' ", new object[0]);
                SqlParameter item = new SqlParameter("@conCode", conCode);
                list.Add(item);
            }
            if (!string.IsNullOrEmpty(conName))
            {
                builder.Append(" AND ContractName LIKE '%'+@conName+'%' ");
                SqlParameter parameter2 = new SqlParameter("@conName", conName);
                list.Add(parameter2);
            }
            if (!string.IsNullOrEmpty(conType))
            {
                builder.Append(" AND TypeName LIKE '%'+@conType+'%' ");
                SqlParameter parameter3 = new SqlParameter("@conType", conType);
                list.Add(parameter3);
            }
            list.Add(new SqlParameter("@PrjGuid", prjId));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            list.Add(new SqlParameter("@pageSize", pageSize));
            StringBuilder builder2 = new StringBuilder();
            builder2.AppendFormat("\r\n            --DECLARE @PrjGuid NVARCHAR(50),@PageSize INT,@PageIndex INT;\r\n            --SET @PrjGuid='A69601A0-8AAA-4D80-9609-CA32139D72E9'\r\n            --SET @PageSize=150;\r\n            --SET @PageIndex=1;\r\n            WITH IncometCon AS\r\n            (\r\n\t            --项目下对应的的所有的收入合同\r\n\t            SELECT  Row_Number() OVER (ORDER BY Date DESC, Version ASC) AS Num,* FROM \r\n\t            (\r\n\t\t            SELECT ContractCode,ContractName,TypeName,ContractPrice,FlowState,ContractId\r\n\t\t            ,case isFContract \r\n\t\t\t            when 1 then ContractId \r\n\t\t\t            when 0 then Fcode+ContractId \r\n\t\t            END AS Version\r\n\t\t            ,case isFContract \r\n\t\t\t            WHEN '1' THEN RegisterTime \r\n\t\t\t            WHEN '0' THEN ( SELECT RegisterTime FROM Con_Incomet_Contract  \r\n\t\t\t\t\t\t              WHERE ContractID = (IncometCon.FCode)) \r\n\t\t            END AS Date,isFContract,FCode\r\n\t\t            FROM Con_Incomet_Contract IncometCon INNER JOIN Con_ContractType ConType \r\n\t\t            ON IncometCon.TypeId=ConType.TypeId\r\n\t\t            WHERE Project=@PrjGuid {0}\r\n\t            ) Con\r\n            ),IncometModiy AS\r\n            (\r\n\t            --收入合同最终金额\r\n\t            SELECT  ContractId,(ContractPrice+ISNULL(ChangePrice,0)) IncometContractMoney  FROM (\r\n\t\t            SELECT IncometCon.ContractId,ContractPrice,SUM(ChangePrice) ChangePrice FROM IncometCon \r\n\t\t            LEFT JOIN Con_Incomet_Modify IncometModify ON IncometCon.ContractId=IncometModify.ContractId \r\n\t\t            GROUP BY ContractPrice,IncometCon.ContractId\r\n\t            ) IncometConModify\r\n            ),IncometBalance AS\r\n            (\r\n\t            --收入合同结算\r\n\t            SELECT IncometCon.ContractId,ISNULL(SUM(ClearingPrice),0) IncometBalanceMoney FROM IncometCon \r\n\t            LEFT JOIN Con_Incomet_Balance IncometBalance ON IncometCon.ContractId=IncometBalance.ContractId \r\n\t            GROUP BY ContractPrice,IncometCon.ContractId\r\n            ),IncometPayment AS\r\n            (\r\n\t            --收入合同收款\r\n\t            SELECT IncometCon.ContractId,ISNULL(SUM(CllectionPrice),0) IncometPaymentMoney FROM IncometCon \r\n\t            LEFT JOIN Con_Incomet_Payment IncometPayment ON IncometCon.ContractId=IncometPayment.ContractId \r\n\t            GROUP BY ContractPrice,IncometCon.ContractId\r\n            ),IncometApply AS\r\n            (\r\n\t            --收入合同挂靠\r\n\t            SELECT IncometCon.ContractId,ISNULL(SUM(PaymentAmount),0) IncometApplyMoney FROM IncometCon \r\n\t            LEFT JOIN Con_Income_PaymentApply IncometApply ON IncometCon.ContractId=IncometApply.ContractId \r\n\t            AND IncometApply.FlowState=1\r\n\t            GROUP BY ContractPrice,IncometCon.ContractId \r\n            ),PayoutContract AS\r\n            (\r\n\t            --收入合同对应的支出合同\r\n\t            SELECT IncometCon.ContractId IncometConId,payoutContract.ContractId PayoutConId,ModifiedMoney FROM IncometCon \r\n\t            INNER JOIN Con_Payout_Contract payoutContract ON IncometCon.ContractId=payoutContract.InContractID \r\n\t            AND payoutContract.FlowState=1\r\n            ),PayoutModified AS\r\n            (\r\n\t            --支出合同对应的支出合同之和\r\n\t            select IncometConId,SUM(ModifiedMoney) PayoutModifiedMoney \r\n\t            from PayoutContract GROUP BY IncometConId\r\n            ),PayoutBalance AS\r\n            (\r\n\t            --支出合同对应的合同结算\r\n\t            SELECT IncometConId,ISNULL(SUM(BalanceMoney),0) PayoutBalanceMoney FROM PayoutContract \r\n\t            LEFT JOIN Con_Payout_Balance PayoutBalance ON PayoutConId=PayoutBalance.ContractId\r\n\t            AND PayoutBalance.FlowState=1 GROUP BY IncometConId\r\n            ),PayoutPayment AS\r\n            (\r\n\t            --支出合同对应的合同支付\r\n\t            SELECT IncometConId,ISNULL(SUM(PaymentMoney),0) PayoutPaymentMoney FROM PayoutContract \r\n\t            LEFT JOIN Con_Payout_Payment PayoutPayment ON PayoutConId=PayoutPayment.ContractId\r\n\t            AND PayoutPayment.FlowState=1 GROUP BY IncometConId\r\n            )\r\n            SELECT TOP (@pageSize) Convert(NVARCHAR(20),Num) Num,ContractId,ContractCode,\r\n            ContractName,TypeName,ContractPrice,InCometContractMoney,IncometBalanceMoney,IncometPaymentMoney,\r\n            IncometApplyMoney,PayoutModifiedMoney,PayoutBalanceMoney,PayoutPaymentMoney,\r\n            (InCometContractMoney-PayoutModifiedMoney) SpreadConMoney,(IncometBalanceMoney-PayoutBalanceMoney) SpreadBalanceMoney,\r\n\t\t    (IncometPaymentMoney-PayoutPaymentMoney) SpreadPaymentMoney,Date\r\n            FROM \r\n            (\r\n\t            SELECT Num,IncometCon.ContractId,ContractCode,ContractName,TypeName,FlowState,ContractPrice,InCometContractMoney,\r\n\t            ISNULL(IncometBalanceMoney,0) IncometBalanceMoney,\r\n\t            ISNULL(IncometPaymentMoney,0) IncometPaymentMoney,ISNULL(IncometApplyMoney,0) IncometApplyMoney,\r\n\t            ISNULL(PayoutModifiedMoney,0) PayoutModifiedMoney,ISNULL(PayoutBalanceMoney,0) PayoutBalanceMoney, \r\n\t            ISNULL(PayoutPaymentMoney,0) PayoutPaymentMoney,Date FROM IncometCon \r\n\t            LEFT JOIN IncometModiy ON IncometCon.ContractId=IncometModiy.ContractId\r\n\t            LEFT JOIN IncometBalance ON IncometCon.ContractId=IncometBalance.ContractId\r\n\t            LEFT JOIN IncometPayment ON IncometCon.ContractId=IncometPayment.ContractId\r\n\t            LEFT JOIN IncometApply ON IncometCon.ContractId=IncometApply.ContractId\r\n\t            LEFT JOIN PayoutModified ON IncometCon.ContractId=PayoutModified.IncometConId\r\n\t            LEFT JOIN PayoutBalance ON IncometCon.ContractId=PayoutBalance.IncometConId\r\n\t            LEFT JOIN PayoutPayment ON IncometCon.ContractId=PayoutPayment.IncometConId\r\n            ) IncometPayoutCon\r\n            WHERE Num > @pageSize * (@pageIndex - 1)  ", builder);
            DataTable table = new DataTable();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), list.ToArray());
        }

        public decimal GetPayoutBack(string payoutCode)
        {
            DataTable table = DBHelper.GetTable("select sum(PaymentMoney) from Con_Payout_Payment where FlowState=1 and contractId='" + payoutCode + "'");
            decimal num = 0M;
            if (((table.Rows.Count > 0) && (table.Rows[0][0].ToString() != null)) && (table.Rows[0][0].ToString() != ""))
            {
                num = Convert.ToDecimal(table.Rows[0][0].ToString());
            }
            return num;
        }

        public decimal GetPayoutBalance(string payoutCode)
        {
            DataTable table = DBHelper.GetTable("select sum(BalanceMoney) from Con_Payout_Balance where FlowState=1 and contractid='" + payoutCode + "'");
            decimal num = 0M;
            if (((table.Rows.Count > 0) && (table.Rows[0][0].ToString() != null)) && (table.Rows[0][0].ToString() != ""))
            {
                num = Convert.ToDecimal(table.Rows[0][0].ToString());
            }
            return num;
        }

        public static int GetPayoutConCount(string incometConId, string conCode, string conName, string conType)
        {
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(conCode))
            {
                builder.AppendFormat(" AND ContractCode LIKE '%'+@conCode+'%' ", new object[0]);
                SqlParameter item = new SqlParameter("@conCode", conCode);
                list.Add(item);
            }
            if (!string.IsNullOrEmpty(conName))
            {
                builder.Append(" AND ContractName LIKE '%'+@conName+'%' ");
                SqlParameter parameter2 = new SqlParameter("@conName", conName);
                list.Add(parameter2);
            }
            if (!string.IsNullOrEmpty(conType))
            {
                builder.Append(" AND TypeName LIKE '%'+@conType+'%' ");
                SqlParameter parameter3 = new SqlParameter("@conType", conType);
                list.Add(parameter3);
            }
            list.Add(new SqlParameter("@IncometConId", incometConId));
            StringBuilder builder2 = new StringBuilder();
            builder2.AppendFormat("\r\n            --DECLARE @IncometConId NVARCHAR(50),@PageSize INT,@PageIndex INT;\r\n            --SET @IncometConId='fa62253d-9536-437b-bcf6-0eadfc141316'\r\n            --SET @PageSize=150;\r\n            --SET @PageIndex=1;\r\n            SELECT COUNT(*) FROM \r\n            (\t\r\n\t\t            SELECT ContractId PayoutConId,ContractCode,ContractName,TypeName,ContractMoney,ModifiedMoney \r\n\t\t            ,case isMainContract \r\n\t\t\t\t            when 1 then ContractId \r\n\t\t\t\t            when 0 then MainContractId+ContractId \r\n\t\t\t            END AS Version\r\n\t\t\t            ,case isMainContract \r\n\t\t\t\t            WHEN '1' THEN payoutContract.InputDate \r\n\t\t\t\t            WHEN '0' THEN ( SELECT InputDate FROM Con_Payout_Contract  \r\n\t\t\t\t\t\t\t              WHERE ContractID = (payoutContract.MainContractId)) \r\n\t\t\t            END AS Date,isMainContract,MainContractId\r\n\t\t            FROM Con_Payout_Contract payoutContract\r\n\t\t            INNER JOIN Con_ContractType ConType ON payoutContract.TypeId=ConType.TypeId \r\n\t\t            WHERE InContractID=@IncometConId\r\n\t\t            AND FlowState=1 {0} \r\n            ) Con", builder);
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder2.ToString(), list.ToArray()));
        }

        public static DataTable GetPayoutConInfo(string incometConId, string conCode, string conName, string conType, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = GetPayoutConCount(incometConId, conCode, conName, conType);
            }
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(conCode))
            {
                builder.AppendFormat(" AND ContractCode LIKE '%'+@conCode+'%' ", new object[0]);
                SqlParameter item = new SqlParameter("@conCode", conCode);
                list.Add(item);
            }
            if (!string.IsNullOrEmpty(conName))
            {
                builder.Append(" AND ContractName LIKE '%'+@conName+'%' ");
                SqlParameter parameter2 = new SqlParameter("@conName", conName);
                list.Add(parameter2);
            }
            if (!string.IsNullOrEmpty(conType))
            {
                builder.Append(" AND TypeName LIKE '%'+@conType+'%' ");
                SqlParameter parameter3 = new SqlParameter("@conType", conType);
                list.Add(parameter3);
            }
            list.Add(new SqlParameter("@IncometConId", incometConId));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            list.Add(new SqlParameter("@pageSize", pageSize));
            StringBuilder builder2 = new StringBuilder();
            builder2.AppendFormat("\r\n            --DECLARE @IncometConId NVARCHAR(50),@PageSize INT,@PageIndex INT;\r\n            --SET @IncometConId='fa62253d-9536-437b-bcf6-0eadfc141316'\r\n            --SET @PageSize=150;\r\n            --SET @PageIndex=1;\r\n            WITH PayoutContract AS\r\n            (\r\n\t            --收入合同对应的支出合同\r\n\t            SELECT  Row_Number() OVER (ORDER BY Date DESC, Version ASC) AS Num,* FROM \r\n\t            (\r\n\t\t            SELECT ContractId PayoutConId,ContractCode,ContractName,TypeName,ContractMoney,ModifiedMoney \r\n\t\t            ,case isMainContract \r\n\t\t\t\t            when 1 then ContractId \r\n\t\t\t\t            when 0 then MainContractId+ContractId \r\n\t\t\t            END AS Version\r\n\t\t\t            ,case isMainContract \r\n\t\t\t\t            WHEN '1' THEN payoutContract.InputDate \r\n\t\t\t\t            WHEN '0' THEN ( SELECT InputDate FROM Con_Payout_Contract  \r\n\t\t\t\t\t\t\t              WHERE ContractID = (payoutContract.MainContractId)) \r\n\t\t\t            END AS Date,isMainContract,MainContractId\r\n\t\t            FROM Con_Payout_Contract payoutContract\r\n\t\t            INNER JOIN Con_ContractType ConType ON payoutContract.TypeId=ConType.TypeId \r\n\t\t            WHERE InContractID=@IncometConId\r\n\t\t            AND FlowState=1 {0}\r\n\t            )Con\t\r\n            ),PayoutBalance AS\r\n            (\r\n\t            --支出合同对应的合同结算\r\n\t            SELECT PayoutConId,ISNULL(SUM(BalanceMoney),0) PayoutBalanceMoney FROM PayoutContract \r\n\t            LEFT JOIN Con_Payout_Balance PayoutBalance ON PayoutConId=PayoutBalance.ContractId\r\n\t            AND PayoutBalance.FlowState=1 GROUP BY PayoutConId\r\n            ),PayoutPayment AS\r\n            (\r\n\t            --支出合同对应的合同支付\r\n\t            SELECT PayoutConId,ISNULL(SUM(PaymentMoney),0) PayoutPaymentMoney FROM PayoutContract \r\n\t            LEFT JOIN Con_Payout_Payment PayoutPayment ON PayoutConId=PayoutPayment.ContractId\r\n\t            AND PayoutPayment.FlowState=1 GROUP BY PayoutConId\r\n            )\r\n            SELECT TOP (@pageSize) Convert(NVARCHAR(20),Num) Num,PayoutConId,ContractCode,\r\n            ContractName,TypeName,ContractMoney,PayoutModifiedMoney,PayoutBalanceMoney,\r\n            PayoutPaymentMoney,Date\r\n            FROM \r\n            (\r\n\t            SELECT Num,PayoutContract.PayoutConId,ContractCode,ContractName,TypeName,ContractMoney,\r\n\t            ModifiedMoney PayoutModifiedMoney,ISNULL(PayoutBalanceMoney,0) PayoutBalanceMoney, \r\n\t            ISNULL(PayoutPaymentMoney,0) PayoutPaymentMoney,Date FROM PayoutContract\r\n\t            LEFT JOIN PayoutBalance ON PayoutContract.PayoutConId=PayoutBalance.PayoutConId\r\n\t            LEFT JOIN PayoutPayment ON PayoutContract.PayoutConId=PayoutPayment.PayoutConId\r\n            ) IncometPayoutCon\r\n            WHERE Num > @pageSize * (@pageIndex - 1)  ", builder);
            DataTable table = new DataTable();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), list.ToArray());
        }

        public static int GetPrjConCount(string prjCode, string prjName)
        {
            StringBuilder builder = new StringBuilder();
            string str = ConfigHelper.Get("IsIncomeContractApprove");
            List<SqlParameter> list = new List<SqlParameter>();
            if (str == "1")
            {
                builder.Append(" AND FlowState=1 ");
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                builder.AppendFormat(" AND PrjCode LIKE '%'+@prjCode+'%' ", new object[0]);
                SqlParameter item = new SqlParameter("@prjCode", prjCode);
                list.Add(item);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.Append(" AND PrjName LIKE '%'+@prjName+'%' ");
                SqlParameter parameter2 = new SqlParameter("@prjName", prjName);
                list.Add(parameter2);
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.AppendFormat("\r\n            SELECT COUNT(PrjGuid) FROM\r\n            (\r\n\t            SELECT PrjGuid FROM vProject \r\n\t            INNER JOIN Con_Incomet_Contract ON PrjGuid=Project \r\n\t            WHERE 1=1 {0}\r\n\t            GROUP BY PrjGuid\r\n            ) PrjCon ", builder);
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder2.ToString(), list.ToArray()));
        }

        public static DataTable GetPrjConInfo(string prjCode, string prjName, int pageIndex, int pageSize)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = GetPrjConCount(prjCode, prjName);
            }
            StringBuilder builder = new StringBuilder();
            string str = ConfigHelper.Get("IsIncomeContractApprove");
            List<SqlParameter> list = new List<SqlParameter>();
            if (str == "1")
            {
                builder.Append(" AND FlowState=1 ");
            }
            if (!string.IsNullOrEmpty(prjCode))
            {
                builder.AppendFormat(" AND PrjCode LIKE '%'+@prjCode+'%' ", new object[0]);
                SqlParameter item = new SqlParameter("@prjCode", prjCode);
                list.Add(item);
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.Append(" AND PrjName LIKE '%'+@prjName+'%' ");
                SqlParameter parameter2 = new SqlParameter("@prjName", prjName);
                list.Add(parameter2);
            }
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            list.Add(new SqlParameter("@pageSize", pageSize));
            StringBuilder builder2 = new StringBuilder();
            builder2.AppendFormat("\r\n            --DECLARE @PageSize INT,@PageIndex INT;\r\n            --SET @PageSize=150;\r\n            --SET @PageIndex=1;\r\n            WITH IncometCon AS\r\n            (\r\n\t            --项目下对应的的所有的收入合同\r\n\t            SELECT TypeCode,PrjGuid,PrjCode,PrjName,vProject.InputDate PrjInputDate,\r\n\t            ContractCode,ContractName,ContractPrice,FlowState,ContractId FROM vProject \r\n\t            INNER JOIN Con_Incomet_Contract ON PrjGuid=Project \r\n\t            WHERE 1=1 {0}\r\n            ),IncometModiy AS\r\n            (\r\n\t            --收入合同最终金额\r\n\t            SELECT  PrjGuid,ContractId,(ContractPrice+ISNULL(ChangePrice,0)) IncometContractMoney  FROM (\r\n\t\t            SELECT PrjGuid,IncometCon.ContractId,ContractPrice,SUM(ChangePrice) ChangePrice FROM IncometCon \r\n\t\t            LEFT JOIN Con_Incomet_Modify IncometModify ON IncometCon.ContractId=IncometModify.ContractId \r\n\t\t            GROUP BY PrjGuid,ContractPrice,IncometCon.ContractId\r\n\t            ) IncometConModify\r\n            ),IncometBalance AS\r\n            (\r\n\t            --收入合同结算\r\n\t            SELECT PrjGuid,IncometCon.ContractId,ISNULL(SUM(ClearingPrice),0) IncometBalanceMoney FROM IncometCon \r\n\t            LEFT JOIN Con_Incomet_Balance IncometBalance ON IncometCon.ContractId=IncometBalance.ContractId \r\n\t            GROUP BY PrjGuid,ContractPrice,IncometCon.ContractId\r\n            ),IncometPayment AS\r\n            (\r\n\t            --收入合同收款\r\n\t            SELECT PrjGuid,IncometCon.ContractId,ISNULL(SUM(CllectionPrice),0) IncometPaymentMoney FROM IncometCon \r\n\t            LEFT JOIN Con_Incomet_Payment IncometPayment ON IncometCon.ContractId=IncometPayment.ContractId \r\n\t            GROUP BY PrjGuid,ContractPrice,IncometCon.ContractId\r\n            ),IncometApply AS\r\n            (\r\n\t            --收入合同挂靠\r\n\t            SELECT PrjGuid,IncometCon.ContractId,ISNULL(SUM(PaymentAmount),0) IncometApplyMoney FROM IncometCon \r\n\t            LEFT JOIN Con_Income_PaymentApply IncometApply ON IncometCon.ContractId=IncometApply.ContractId \r\n\t            AND IncometApply.FlowState=1\r\n\t            GROUP BY PrjGuid,ContractPrice,IncometCon.ContractId \r\n            ),PayoutContract AS\r\n            (\r\n\t            --收入合同对应的支出合同\r\n\t            SELECT IncometCon.ContractId IncometConId,payoutContract.ContractId PayoutConId,ModifiedMoney FROM IncometCon \r\n\t            INNER JOIN Con_Payout_Contract payoutContract ON IncometCon.ContractId=payoutContract.InContractID \r\n\t            AND payoutContract.FlowState=1\r\n            ),PayoutModified AS\r\n            (\r\n\t            --支出合同对应的支出合同之和\r\n\t            select IncometConId,SUM(ModifiedMoney) PayoutModifiedMoney \r\n\t            from PayoutContract GROUP BY IncometConId\r\n            ),PayoutBalance AS\r\n            (\r\n\t            --支出合同对应的合同结算\r\n\t            SELECT IncometConId,ISNULL(SUM(BalanceMoney),0) PayoutBalanceMoney FROM PayoutContract \r\n\t            LEFT JOIN Con_Payout_Balance PayoutBalance ON PayoutConId=PayoutBalance.ContractId\r\n\t            AND PayoutBalance.FlowState=1 GROUP BY IncometConId\r\n            ),PayoutPayment AS\r\n            (\r\n\t            --支出合同对应的合同支付\r\n\t            SELECT IncometConId,ISNULL(SUM(PaymentMoney),0) PayoutPaymentMoney FROM PayoutContract \r\n\t            LEFT JOIN Con_Payout_Payment PayoutPayment ON PayoutConId=PayoutPayment.ContractId\r\n\t            AND PayoutPayment.FlowState=1 GROUP BY IncometConId\r\n            )\r\n            SELECT TOP (@pageSize) Convert(NVARCHAR(20),Num) Num,TypeCode,PrjGuid,PrjCode,PrjName,\r\n            InCometContractMoney,IncometBalanceMoney,IncometPaymentMoney,\r\n            IncometApplyMoney,PayoutModifiedMoney,PayoutBalanceMoney,PayoutPaymentMoney,\r\n            SpreadConMoney,SpreadBalanceMoney,SpreadPaymentMoney,UserDefined_Date  FROM (\r\n\t            SELECT Row_Number()over(ORDER BY userDefined_Date DESC,TypeCode ASC) as Num,*  FROM\r\n\t            (\r\n\t\t            SELECT TypeCode,PrjGuid,PrjCode,PrjName,InCometContractMoney,IncometBalanceMoney,IncometPaymentMoney,\r\n\t\t            IncometApplyMoney,PayoutModifiedMoney,PayoutBalanceMoney,PayoutPaymentMoney,\r\n\t\t            (InCometContractMoney-PayoutModifiedMoney) SpreadConMoney,\r\n\t\t            (IncometBalanceMoney-PayoutBalanceMoney) SpreadBalanceMoney,\r\n\t\t            (IncometPaymentMoney-PayoutPaymentMoney) SpreadPaymentMoney\r\n\t\t            ,CASE LEN(PrjConInfo.TypeCode)\r\n\t\t\t            WHEN '5' THEN PrjInputDate \r\n\t\t\t            ELSE (SELECT InputDate FROM vProject WHERE TypeCode = \r\n\t\t\t\t              LEFT(PrjConInfo.TypeCode,5) AND IsValid = '1')\r\n\t\t\t            END\r\n\t\t             AS UserDefined_Date\r\n\t\t            FROM (\r\n\t\t\t            SELECT TypeCode,PrjGuid,PrjCode,PrjName,PrjInputDate,\r\n\t\t\t            SUM(InCometContractMoney) InCometContractMoney,SUM(IncometBalanceMoney) IncometBalanceMoney,\r\n\t\t\t            SUM(IncometPaymentMoney) IncometPaymentMoney,SUM(IncometApplyMoney) IncometApplyMoney, \r\n\t\t\t            SUM(PayoutModifiedMoney) PayoutModifiedMoney,SUM(PayoutBalanceMoney) PayoutBalanceMoney,\r\n\t\t\t            SUM(PayoutPaymentMoney) PayoutPaymentMoney\r\n\t\t\t            FROM \r\n\t\t\t            (\r\n\t\t\t\t            SELECT TypeCode,IncometCon.PrjGuid,PrjCode,PrjName,PrjInputDate,ContractCode,\r\n\t\t\t\t            IncometCon.ContractName,IncometCon.FlowState,IncometModiy.InCometContractMoney,\r\n\t\t\t\t            ISNULL(IncometBalanceMoney,0) IncometBalanceMoney,\r\n\t\t\t\t            ISNULL(IncometPaymentMoney,0) IncometPaymentMoney,ISNULL(IncometApplyMoney,0) IncometApplyMoney,\r\n\t\t\t\t            ISNULL(PayoutModifiedMoney,0) PayoutModifiedMoney,ISNULL(PayoutBalanceMoney,0) PayoutBalanceMoney, \r\n\t\t\t\t            ISNULL(PayoutPaymentMoney,0) PayoutPaymentMoney FROM IncometCon \r\n\t\t\t\t            LEFT JOIN IncometModiy ON IncometCon.ContractId=IncometModiy.ContractId\r\n\t\t\t\t            LEFT JOIN IncometBalance ON IncometCon.ContractId=IncometBalance.ContractId\r\n\t\t\t\t            LEFT JOIN IncometPayment ON IncometCon.ContractId=IncometPayment.ContractId\r\n\t\t\t\t            LEFT JOIN IncometApply ON IncometCon.ContractId=IncometApply.ContractId\r\n\t\t\t\t            LEFT JOIN PayoutModified ON IncometCon.ContractId=PayoutModified.IncometConId\r\n\t\t\t\t            LEFT JOIN PayoutBalance ON IncometCon.ContractId=PayoutBalance.IncometConId\r\n\t\t\t\t            LEFT JOIN PayoutPayment ON IncometCon.ContractId=PayoutPayment.IncometConId\r\n\t\t\t            ) ConsInfo \r\n\t\t\t            GROUP BY TypeCode,PrjGuid,PrjCode,PrjName,PrjInputDate\r\n\t\t            ) PrjConInfo ) \r\n\t            PrjConInfo1\r\n            ) tab2 where Num > @pageSize * (@pageIndex - 1) ", builder);
            DataTable table = new DataTable();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), list.ToArray());
        }

        public static DataTable PaymentDetailReport(string PaymentCode, string PaymentPerson, string ContractName, string PrjId, string Audit)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT A.*,C.ContractName,P.PrjName FROM  Con_Income_PaymentApply AS A\r\nLEFT JOIN Con_Incomet_Contract AS C ON C.ContractID=A.ContractId\r\nLEFT JOIN PT_PrjInfo AS P ON P.PrjGuid = C.Project \r\nWHERE A.FlowState=1 ");
            builder.AppendLine();
            if (Audit == "1")
            {
                builder.Append(" AND C.FlowState=1 ");
            }
            if (!string.IsNullOrEmpty(PaymentCode))
            {
                builder.AppendFormat(" AND A.PaymentCode LIKE '%{0}%' ", PaymentCode);
            }
            if (!string.IsNullOrEmpty(PaymentPerson))
            {
                builder.AppendFormat(" AND A.PaymentPenson LIKE '%{0}%' ", PaymentPerson);
            }
            if (!string.IsNullOrEmpty(ContractName))
            {
                builder.AppendFormat("AND C.ContractName LIKE '%{0}%' ", ContractName);
            }
            if (!string.IsNullOrEmpty(PrjId))
            {
                builder.AppendFormat(" AND P.PrjName like '%{0}%'", PrjId);
            }
            builder.Append("  ORDER BY A.PaymentDate DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable PaymentDetailReport(string PaymentCode, string PaymentPerson, string ContractName, string PrjId, string Audit, int pageIndex, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append("SELECT Row_Number() OVER (ORDER BY PaymentDate DESC) as Num,* FROM ( \n");
            builder.Append("SELECT A.*,C.ContractName,P.PrjName FROM  Con_Income_PaymentApply AS A\r\nLEFT JOIN Con_Incomet_Contract AS C ON C.ContractID=A.ContractId\r\nLEFT JOIN PT_PrjInfo AS P ON P.PrjGuid = C.Project \r\nWHERE A.FlowState=1 ");
            builder.AppendLine();
            if (Audit == "1")
            {
                builder.Append(" AND C.FlowState=1 ");
            }
            if (!string.IsNullOrEmpty(PaymentCode))
            {
                builder.AppendFormat(" AND A.PaymentCode LIKE '%{0}%' ", PaymentCode);
            }
            if (!string.IsNullOrEmpty(PaymentPerson))
            {
                builder.AppendFormat(" AND A.PaymentPenson LIKE '%{0}%' ", PaymentPerson);
            }
            if (!string.IsNullOrEmpty(ContractName))
            {
                builder.AppendFormat("AND C.ContractName LIKE '%{0}%' ", ContractName);
            }
            if (!string.IsNullOrEmpty(PrjId))
            {
                builder.AppendFormat(" AND P.PrjName like '%{0}%'", PrjId);
            }
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable PaymentReport(string ContractCode, string ContractName, string TypeName, string PrjId, string Audit)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("--项目资金收支分析表\r\nWITH cteModify AS(    --变更累计\r\n\tSELECT M.ContractID, SUM(ChangePrice) AS ModifyAmount\r\n\tFROM Con_Incomet_Modify AS M\r\n\tGROUP BY M.ContractID\r\n), ctePay AS (    --收款累计\r\n\tSELECT P.ContractID, SUM(CllectionPrice) AS PayAmount\r\n\tFROM Con_Incomet_Payment AS P\r\n\tGROUP BY P.ContractID\r\n), cteApply AS(    --付款累计\r\n\tSELECT A.ContractID, SUM(PaymentAmount) AS ApplyAmount\r\n\tFROM Con_Income_PaymentApply AS A  WHERE FlowState=1\r\n\tGROUP BY A.ContractID\r\n)\r\nSELECT C.ContractID,C.ContractCode, C.ContractName,\r\n\tC.ContractPrice + ISNULL(cteModify.ModifyAmount, 0.0) AS ContractAmount, --合同金额\r\n\tISNULL(ctePay.PayAmount,0.000) PayAmount, --收款累计\r\n\tISNULL(cteApply.ApplyAmount,0.000) ApplyAmount, --付款累计\r\n\tISNULL(C.ContractPrice,0.000) + ISNULL(cteModify.ModifyAmount, 0.000) - ISNULL(ctePay.PayAmount,0.000) AS DiffAmount, --差额\r\n    T.TypeName, --合同类型\r\n\tP.PrjName --对应项目\r\nFROM   Con_Incomet_Contract AS C\r\nLEFT JOIN cteApply ON C.ContractID=cteApply.ContractID\r\nLEFT JOIN  cteModify ON cteModify.ContractID = C.ContractID\r\nLEFT JOIN ctePay ON C.ContractID = ctePay.ContractID\r\nLEFT JOIN Con_ContractType AS T ON T.TypeID = C.TypeID\r\nLEFT JOIN PT_PrjInfo AS P ON P.PrjGuid = C.Project WHERE 1=1 ");
            builder.AppendLine();
            if (Audit == "1")
            {
                builder.Append(" AND C.FlowState=1 ");
            }
            if (!string.IsNullOrEmpty(ContractCode))
            {
                builder.AppendFormat(" AND ContractCode like '%{0}%' ", ContractCode);
            }
            if (!string.IsNullOrEmpty(ContractName))
            {
                builder.AppendFormat(" AND ContractName like '%{0}%' ", ContractName);
            }
            if (!string.IsNullOrEmpty(TypeName))
            {
                builder.AppendFormat(" AND T.TypeName ='{0}'", TypeName);
            }
            if (!string.IsNullOrEmpty(PrjId))
            {
                builder.AppendFormat(" AND P.PrjName like '%{0}%'", PrjId);
            }
            builder.Append(" ORDER BY P.StartDate DESC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable PaymentReport(string ContractCode, string ContractName, string TypeName, string PrjId, string Audit, int pageIndex, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("--项目资金收支分析表\r\nWITH cteModify AS(    --变更累计\r\n\tSELECT M.ContractID, SUM(ChangePrice) AS ModifyAmount\r\n\tFROM Con_Incomet_Modify AS M\r\n\tGROUP BY M.ContractID\r\n), ctePay AS (    --收款累计\r\n\tSELECT P.ContractID, SUM(CllectionPrice) AS PayAmount\r\n\tFROM Con_Incomet_Payment AS P\r\n\tGROUP BY P.ContractID\r\n), cteApply AS(    --付款累计\r\n\tSELECT A.ContractID, SUM(PaymentAmount) AS ApplyAmount\r\n\tFROM Con_Income_PaymentApply AS A  WHERE FlowState=1\r\n\tGROUP BY A.ContractID\r\n)\r\nSELECT TOP (@pageSize)* FROM (\r\nSELECT Row_Number() OVER (ORDER BY StartDate DESC) as Num,* FROM (\r\nSELECT C.ContractID,C.ContractCode, C.ContractName,\r\n\tC.ContractPrice + ISNULL(cteModify.ModifyAmount, 0.0) AS ContractAmount, --合同金额\r\n\tISNULL(ctePay.PayAmount,0.000) PayAmount, --收款累计\r\n\tISNULL(cteApply.ApplyAmount,0.000) ApplyAmount, --付款累计\r\n\tISNULL(C.ContractPrice,0.000) + ISNULL(cteModify.ModifyAmount, 0.000) - ISNULL(ctePay.PayAmount,0.000) AS DiffAmount, --差额\r\n    T.TypeName, --合同类型\r\n\tP.PrjName, --对应项目\r\n    P.StartDate\r\nFROM   Con_Incomet_Contract AS C\r\nLEFT JOIN cteApply ON C.ContractID=cteApply.ContractID\r\nLEFT JOIN  cteModify ON cteModify.ContractID = C.ContractID\r\nLEFT JOIN ctePay ON C.ContractID = ctePay.ContractID\r\nLEFT JOIN Con_ContractType AS T ON T.TypeID = C.TypeID\r\nLEFT JOIN PT_PrjInfo AS P ON P.PrjGuid = C.Project WHERE 1=1 ");
            builder.AppendLine();
            if (Audit == "1")
            {
                builder.Append(" AND C.FlowState=1 ");
            }
            if (!string.IsNullOrEmpty(ContractCode))
            {
                builder.AppendFormat(" AND ContractCode like '%{0}%' ", ContractCode);
            }
            if (!string.IsNullOrEmpty(ContractName))
            {
                builder.AppendFormat(" AND ContractName like '%{0}%' ", ContractName);
            }
            if (!string.IsNullOrEmpty(TypeName))
            {
                builder.AppendFormat(" AND T.TypeName ='{0}'", TypeName);
            }
            if (!string.IsNullOrEmpty(PrjId))
            {
                builder.AppendFormat(" AND P.PrjName like '%{0}%'", PrjId);
            }
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pageSize", SqlDbType.Int) };
            commandParameters[0].Value = pageSize;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int SelIncoCount(string PrjGuid)
        {
            string sql = "";
            return DBHelper.GetTable(sql).Rows.Count;
        }

        public DataTable SelIncomet(string sqlWhere)
        {
            return DBHelper.GetTable("select ContractID,ContractCode,ContractName,ContractPrice,project ,prjName from Con_Incomet_Contract as c  inner join PT_PrjInfo as p on c.project=p.prjGuid where IsArchived='false' " + sqlWhere + " order by SignedTime desc");
        }

        public DataTable SelPayoutCon(string IncometCode)
        {
            return DBHelper.GetTable("select ContractID,ContractCode,ContractName,ContractMoney,ModifiedMoney ,TypeName,p.PrjGuid from Con_Payout_Contract as p inner join Con_ContractType as c on p.TypeID=c.TypeID  where InContractID='" + IncometCode + "'");
        }

        public DataTable SelPayoutPrj(string prjCode)
        {
            string str = "select ContractID,ContractCode,ContractName,ContractMoney,ModifiedMoney ,TypeName,p.PrjGuid";
            return DBHelper.GetTable(((str + " from Con_Payout_Contract as p inner join Con_ContractType as c on p.TypeID=c.TypeID  ") + "where p.PrjGuid='" + prjCode + "' and InContractID not in ") + "(select ContractID from Con_Incomet_Contract where IsArchived='false' and Project='" + prjCode + "')");
        }

        public DataTable SelPrj(string whereSel)
        {
            return DBHelper.GetTable("select prjGuid,prjName from dbo.PT_PrjInfo where prjGuid in (select project from Con_Incomet_Contract where IsArchived='false' ) " + whereSel);
        }
    }
}

