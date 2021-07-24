namespace com.jwsoft.pm.entpm.action
{
    using cn.justwin.BLL;
    using com.jwsoft.pm.data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class Fund_Report
    {
        public DataTable GetIndirectInfo(int planYear, int planMonth, string prjGuid)
        {
            int num = 0;
            int num2 = 0;
            if (planMonth == 1)
            {
                num = planYear - 1;
                num2 = 12;
            }
            else
            {
                num = planYear;
                num2 = planMonth - 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from (select 0 as xh,*,");
            builder.Append(" case when agoAmount=0 then 0 else convert(decimal(18,2),(payAmount/agoAmount)) end as ratio,");
            builder.Append(" (payAmount-agoAmount) as balance ");
            builder.Append(" from (select * from (select ");
            builder.Append(" prjCBS.prjName,prjCBS.Name,");
            builder.Append(" isnull((select M.Amount");
            builder.Append(" from Bud_IndirectMonthBudget as M left join Bud_IndirectBudget as B on M.IndirectBudget=B.Id");
            builder.Append(string.Concat(new object[] { "\twhere B.CBSCode=prjCBS.CBSCode and B.ProjectId=prjCBS.PrjGuid  AND M.Year=", num, " AND M.Month=", num2 }));
            builder.Append("\t),0) as agoAmount, ");
            builder.Append("\tisnull((");
            builder.Append(" select sum(IDD.Amount)  from Bud_IndirectDiaryDetails as IDD inner join Bud_IndirectDiaryCost as IDC on IDD.InDiaryId=IDC.inDiaryId");
            builder.Append(" where IDC.flowState=1 and IDD.CBSCode=prjCBS.CBSCode  and IDC.ProjectId=CAST(prjCBS.PrjGuid as nvarchar(200)) ");
            builder.Append(string.Concat(new object[] { " and Year(IDC.InputDate)=", num, " and Month(IDC.InputDate)=", num2 }));
            builder.Append(" ),0)as payAmount,");
            builder.Append(" isnull((");
            builder.Append(" select M.Amount");
            builder.Append(" from Bud_IndirectMonthBudget as M left join Bud_IndirectBudget as B on M.IndirectBudget=B.Id");
            builder.Append(string.Concat(new object[] { " where B.CBSCode=prjCBS.CBSCode and B.ProjectId=prjCBS.PrjGuid  AND M.Year=", planYear, " AND M.Month=", planMonth }));
            builder.Append(" ),0) as NewAmount,");
            builder.Append(" prjCBS.PrjGuid,prjCBS.CBSCode");
            builder.Append(" from (");
            builder.Append(" SELECT prj.PrjGuid, prj.PrjName, km.Name, km.CBSCode");
            builder.Append("\tFROM dbo.PT_PrjInfo AS prj CROSS JOIN ");
            builder.Append("\t dbo.Bud_CostAccounting AS km where prj.isValid=1 ) as prjCBS ");
            builder.Append("\t) as info) as L ");
            builder.Append(" union");
            builder.Append(" select ");
            builder.Append(" 1,prjCBS1.prjName,'小计',0,0,0,prjCBS1.PrjGuid,'',0,0");
            builder.Append(" from  (SELECT  prj.PrjGuid, prj.PrjName, km.Name, km.CBSCode");
            builder.Append(" FROM  dbo.PT_PrjInfo AS prj CROSS JOIN dbo.Bud_CostAccounting AS km where isValid=1) as prjCBS1 ");
            builder.Append(" group by prjCBS1.PrjGuid,prjCBS1.prjName ");
            builder.Append(" ) as indirectInfo");
            builder.Append(" where 1=1");
            if (prjGuid.ToString() != "")
            {
                builder.Append(" and PrjGuid='" + prjGuid.ToString() + "'");
            }
            builder.Append(" order by PrjGuid,NewAmount desc,xh,CBSCode");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable getPayoutPlanInfo(string userCode, int planYear, int planMonth, string prjName, string contract, string planType)
        {
            int num = 0;
            int num2 = 0;
            if (planMonth == 1)
            {
                num = planYear - 1;
                num2 = 12;
            }
            else
            {
                num = planYear;
                num2 = planMonth - 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from (");
            builder.Append(" select 0 as xh,prjName,contractName,beforePlanMoney,planMoney,conPayMoney,");
            builder.Append(" convert(varchar(20),convert(decimal(18,3),case when beforePlanMoney=0 then 0 else (conPayMoney/beforePlanMoney)*100 end ))+'%' as ExecuteRatio,");
            builder.Append(" convert(decimal(18,3),(conPayMoney-beforePlanMoney) ) as ExecuteVariation,");
            builder.Append(" ReMark,prjGuid,contractid from (");
            builder.Append("SELECT * FROM (");
            builder.Append("select ptPrj.prjName,con.contractName,");
            builder.Append(" isnull((select planMoney from dbo.Fund_Plan_MonthDetail planM where planM.contractId=con.contractid and planM.MonthPlanID in ");
            builder.Append(string.Concat(new object[] { " (select MonthPlanID from dbo.Fund_Plan_MonthMain as pM where pM.PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", num, " and pM.PlanMonth=", num2, " and pM.PrjGuid=ptPrj.prjGuid) " }));
            builder.Append(" ),0) as beforePlanMoney,");
            if (planType == "payout")
            {
                builder.Append(" isnull((select sum(PaymentMoney) from Con_Payout_Payment conPay where  conPay.ContractID=con.contractid and conPay.FlowState=1 and ");
                builder.Append(" conPay.MonthPlanUID=");
                builder.Append("\t(select UID from dbo.Fund_Plan_MonthDetail planM where planM.contractId=con.contractid and planM.MonthPlanID in ");
                builder.Append(string.Concat(new object[] { "\t\t\t(select MonthPlanID from dbo.Fund_Plan_MonthMain as pM where pM.PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", num, " and pM.PlanMonth=", num2, " and pM.PrjGuid=ptPrj.prjGuid) " }));
                builder.Append("\t)),0) as conPayMoney,");
            }
            else
            {
                builder.Append(" isnull((select sum(CllectionPrice) from Con_Incomet_Payment conPay where  conPay.ContractID=con.contractid and  ");
                builder.Append(" conPay.MonthPlanUID=");
                builder.Append("\t(select UID from dbo.Fund_Plan_MonthDetail planM where planM.contractId=con.contractid and planM.MonthPlanID in ");
                builder.Append(string.Concat(new object[] { "\t\t\t(select MonthPlanID from dbo.Fund_Plan_MonthMain as pM where pM.PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", num, " and pM.PlanMonth=", num2, " and pM.PrjGuid=ptPrj.prjGuid) " }));
                builder.Append("\t)),0) as conPayMoney,");
            }
            builder.Append(" isnull((select planMoney from dbo.Fund_Plan_MonthDetail planM where planM.contractId=con.contractid and planM.MonthPlanID in ");
            builder.Append(" (select MonthPlanID from dbo.Fund_Plan_MonthMain as pM ");
            builder.Append(string.Concat(new object[] { " where pM.PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", planYear, " and pM.PlanMonth=", planMonth, " and pM.PrjGuid=ptPrj.prjGuid) " }));
            builder.Append(" ),0) as planMoney,");
            builder.Append(" (SELECT PD.Remark FROM dbo.Fund_Plan_MonthDetail PD LEFT JOIN Fund_Plan_MonthMain AS pM  ON PD.MonthPlanID=PM.MonthPlanID ");
            builder.Append(string.Concat(new object[] { " where PlanType='", planType, "' and  pM.FlowState=1 and pM.PlanYear=", planYear, " and pM.PlanMonth=", planMonth, " and pM.PrjGuid=ptPrj.prjGuid AND PD.ContractID=con.contractid" }));
            builder.Append(" ) as ReMark ,");
            builder.Append(" ptPrj.prjGuid,con.contractid");
            builder.Append(" from pt_prjInfo as ptPrj ");
            if (planType == "payout")
            {
                builder.Append(" left join Con_Payout_Contract as con on ptPrj.prjGuid=con.prjGuid");
            }
            else
            {
                builder.Append(" left join Con_Incomet_Contract as con on ptPrj.prjGuid=con.project");
            }
            builder.Append(" where isValid=1 ) as selInfo) as prj  ");
            builder.Append("   union");
            builder.Append(" select 1, ptPrj.prjName ,'小计' ,0,0,0,'',0 , '',ptPrj.prjGuid ,null from pt_prjInfo as ptPrj where isValid=1");
            builder.Append(" ) as allInfo ");
            builder.Append(" where 1=1  ");
            if (contract.ToString() != "")
            {
                builder.Append(" and ContractID='" + contract.ToString() + "'");
            }
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.Append(string.Format(" and prjName like '%{0}%'", prjName));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                StringBuilder builder2 = new StringBuilder();
                IList<string> busiDataId = PrivHelper.GetBusiDataId("project", userCode);
                if ((busiDataId != null) && (busiDataId.Count > 0))
                {
                    string[] strArray = null;
                    foreach (string str in busiDataId)
                    {
                        if (builder2.Length != 0)
                        {
                            builder2.Append(",");
                        }
                        if (str.Contains(","))
                        {
                            strArray = str.Split(new char[] { ',' });
                            for (int i = 0; i < (strArray.Length - 1); i++)
                            {
                                if (!string.IsNullOrEmpty(strArray[i]))
                                {
                                    builder2.Append(string.Format("'{0}'", strArray[i]));
                                }
                            }
                        }
                        builder2.Append(string.Format("'{0}'", str));
                    }
                    builder2.Insert(0, "(");
                    builder2.Insert(builder2.Length, ")");
                    // TODO 2015-7-28 @tao 修改：将下面原代码移动到判断内部
                    builder.Append(string.Format(" and allInfo.prjGuid In {0}", builder2.ToString()));
                }
                //原代码：builder.Append(string.Format(" and allInfo.prjGuid In {0}", builder2.ToString()));
            }
            builder.Append(" order by prjGuid,xh");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPlanInfo(string userCode, int planYear, int planMonth, string planType, string prjName)
        {
            int num = 0;
            int num2 = 0;
            if (planMonth == 1)
            {
                num = planYear - 1;
                num2 = 12;
            }
            else
            {
                num = planYear;
                num2 = planMonth - 1;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(" select *,");
            builder.Append(" convert(varchar(20),convert(decimal(18,3),case when planMoney=0 then 0 else (conPayMoney/planMoney)*100 end ))+'%' as ratio, ");
            builder.Append(string.Concat(new object[] { " (select Remark from dbo.Fund_Plan_MonthMain as pM where  PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", planYear, " and pM.PlanMonth=", planMonth, " and pM.PrjGuid=PlanInfo.prjGuid) as Remark" }));
            builder.Append(" from (");
            builder.Append(" select ");
            builder.Append(" ptPrj.prjName,");
            builder.Append(" isnull((select sum(planMoney) from Fund_Plan_MonthDetail as planDetail ");
            builder.Append("\t\t\twhere planDetail.MonthPlanID=(select MonthPlanID from dbo.Fund_Plan_MonthMain ");
            builder.Append("\t\t\t\t\twhere PrjGuid=ptPrj.PrjGuid and FlowState=1 and PlanType='" + planType + "'");
            builder.Append(string.Concat(new object[] { "\t\t\t\t\t\t\tand PlanYear=", num, " and PlanMonth=", num2, " " }));
            builder.Append("\t)),0) as planMoney,");
            if (planType == "payout")
            {
                builder.Append(" isnull(convert(decimal(18,3),(select sum(PaymentMoney) from Con_Payout_Payment conPay where conPay.FlowState=1 and ");
                builder.Append(" conPay.ContractID in ");
                builder.Append("\t\t(select ContractID from dbo.Fund_Plan_MonthDetail planM where planM.MonthPlanID = ");
                builder.Append(string.Concat(new object[] { "\t\t\t\t(select MonthPlanID from dbo.Fund_Plan_MonthMain as pM where PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", num, " and pM.PlanMonth=", num2, " and pM.PrjGuid=ptPrj.prjGuid)" }));
                builder.Append("\t\t)");
                builder.Append(" and  ");
                builder.Append("\tconPay.MonthPlanUID in");
                builder.Append("\t\t(select UID from dbo.Fund_Plan_MonthDetail planM where  planM.MonthPlanID in ");
                builder.Append(string.Concat(new object[] { "\t\t\t\t(select MonthPlanID from dbo.Fund_Plan_MonthMain as pM where PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", num, " and pM.PlanMonth=", num2, " and pM.PrjGuid=ptPrj.prjGuid) " }));
                builder.Append("\t\t)");
                builder.Append(" )),0) as conPayMoney,");
            }
            else
            {
                builder.Append(" isnull(convert(decimal(18,3),(select sum(CllectionPrice) from Con_Incomet_Payment conPay where ");
                builder.Append(" conPay.ContractID in ");
                builder.Append("\t\t(select ContractID from dbo.Fund_Plan_MonthDetail planM where planM.MonthPlanID = ");
                builder.Append(string.Concat(new object[] { "\t\t\t\t(select MonthPlanID from dbo.Fund_Plan_MonthMain as pM where PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", num, " and pM.PlanMonth=", num2, " and pM.PrjGuid=ptPrj.prjGuid)" }));
                builder.Append("\t\t)");
                builder.Append(" and  ");
                builder.Append("\tconPay.MonthPlanUID in");
                builder.Append("\t\t(select UID from dbo.Fund_Plan_MonthDetail planM where  planM.MonthPlanID in ");
                builder.Append(string.Concat(new object[] { "\t\t\t\t(select MonthPlanID from dbo.Fund_Plan_MonthMain as pM where PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", num, " and pM.PlanMonth=", num2, " and pM.PrjGuid=ptPrj.prjGuid) " }));
                builder.Append("\t\t)");
                builder.Append(" )),0) as conPayMoney,");
            }
            builder.Append(" isnull((select sum(planMoney) from Fund_Plan_MonthDetail as planDetail ");
            builder.Append("\twhere planDetail.MonthPlanID=");
            builder.Append(string.Concat(new object[] { "\t\t(select MonthPlanID from dbo.Fund_Plan_MonthMain as pM where  PlanType='", planType, "' and pM.FlowState=1 and pM.PlanYear=", planYear, " and pM.PlanMonth=", planMonth, " and pM.PrjGuid=ptPrj.prjGuid) " }));
            builder.Append("\t),0) as NewPlanMoney,");
            builder.Append(" ptPrj.prjGuid");
            builder.Append(" from  pt_prjInfo as ptPrj where isValid=1 ");
            if (!string.IsNullOrEmpty(userCode))
            {
                StringBuilder builder2 = new StringBuilder();
                IList<string> busiDataId = PrivHelper.GetBusiDataId("project", userCode);
                if ((busiDataId != null) && (busiDataId.Count > 0))
                {
                    string[] strArray = null;
                    foreach (string str in busiDataId)
                    {
                        if (builder2.Length != 0)
                        {
                            builder2.Append(",");
                        }
                        if (str.Contains(","))
                        {
                            strArray = str.Split(new char[] { ',' });
                            for (int i = 0; i < (strArray.Length - 1); i++)
                            {
                                if (!string.IsNullOrEmpty(strArray[i]))
                                {
                                    builder2.Append(string.Format("'{0}'", strArray[i]));
                                }
                            }
                        }
                        builder2.Append(string.Format("'{0}'", str));
                    }
                    builder2.Insert(0, "(");
                    builder2.Insert(builder2.Length, ")");
                    // TODO 2015-7-28 @tao 修改：将下面原代码移动到判断内部
                    builder.Append(string.Format(" and ptPrj.prjGuid In {0}", builder2.ToString()));
                }
                //原代码：builder.Append(string.Format(" and ptPrj.prjGuid In {0}", builder2.ToString()));
            }
            builder.Append(" ) as PlanInfo");
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.Append(string.Format(" where prjName like '%{0}%'", prjName));
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }
    }
}

