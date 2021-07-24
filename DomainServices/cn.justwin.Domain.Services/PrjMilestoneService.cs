namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public class PrjMilestoneService : Repository<PrjMilestone>
    {
        private PTPrjInfoService prjInfoSer = new PTPrjInfoService();
        private PTPrjInfoZTBService prjInfoZTbSer = new PTPrjInfoZTBService();
        private PrjMilestoneDetailService prjMilestoneDetailSer = new PrjMilestoneDetailService();
        private PTPrjInfoZTBDetailService prjZTbDetailSer = new PTPrjInfoZTBDetailService();
        private int state;

        public void AddMilestone()
        {
            DateTime MonthFristDay = DateTime.Today.AddDays((double) (1 - DateTime.Today.Day));
            DateTime MonthDay = DateTime.Today.AddDays((double) ((1 - DateTime.Today.Day) + 14));
            List<PrjMilestone> list = new List<PrjMilestone>();
            List<PrjMilestoneDetail> list2 = new List<PrjMilestoneDetail>();
            List<PrjMilestone> list3 = new List<PrjMilestone>();
            if (DateTime.Now.Day < MonthDay.Day)
            {
                list = this.LstMilestone(MonthFristDay);
                list2 = this.AddPrjMilestoneDetail(MonthFristDay);
                list3 = (from r in this
                    where r.RptDate == Convert.ToDateTime(MonthFristDay)
                    select r).ToList<PrjMilestone>();
            }
            else
            {
                list = this.LstMilestone(MonthDay);
                list2 = this.AddPrjMilestoneDetail(MonthDay);
                list3 = (from r in this
                    where r.RptDate == Convert.ToDateTime(MonthDay)
                    select r).ToList<PrjMilestone>();
            }
            if (list3.Count == 0)
            {
                if (list.Count > 0)
                {
                    using (List<PrjMilestone>.Enumerator enumerator = list.GetEnumerator())
                    {
                        PrjMilestone stone;
                        while (enumerator.MoveNext())
                        {
                            stone = enumerator.Current;
                            if ((((from p in this
                                where (p.RptDate == stone.RptDate) && (p.UserCode == stone.UserCode)
                                select p).Count<PrjMilestone>() > 0) ? 1 : 0) == 0)
                            {
                                base.Add(stone);
                            }
                        }
                    }
                }
                if (list2.Count > 0)
                {
                    using (List<PrjMilestoneDetail>.Enumerator enumerator2 = list2.GetEnumerator())
                    {
                        PrjMilestoneDetail stone;
                        while (enumerator2.MoveNext())
                        {
                            stone = enumerator2.Current;
                            if ((((from p in this.prjMilestoneDetailSer
                                where (p.RptDate == stone.RptDate) && (p.PrjCode == stone.PrjCode)
                                select p).Count<PrjMilestoneDetail>() > 0) ? 1 : 0) == 0)
                            {
                                this.prjMilestoneDetailSer.Add(stone);
                            }
                        }
                    }
                }
            }
        }

        public List<PrjMilestoneDetail> AddPrjMilestoneDetail(DateTime RptDate)
        {
            List<PrjMilestoneDetail> list = new List<PrjMilestoneDetail>();
            PTYhmcService source = new PTYhmcService();
            PTdbmService service2 = new PTdbmService();
            using (List<PTyhmc>.Enumerator enumerator = source.AsQueryable<PTyhmc>().ToList<PTyhmc>().GetEnumerator())
            {
                PTyhmc yhmc;
                while (enumerator.MoveNext())
                {
                    yhmc = enumerator.Current;
                    PTyhmc pTyhmc = (from r in source
                        where r.v_xm == yhmc.v_xm
                        select r).FirstOrDefault<PTyhmc>();
                    PTdbm tdbm = (from r in service2
                        where r.I_bmdm == pTyhmc.i_bmdm
                        select r).FirstOrDefault<PTdbm>();
                    foreach (KeyValuePair<int, List<string>> pair in this.PrjState(yhmc.v_xm))
                    {
                        foreach (string str in pair.Value)
                        {
                            PrjMilestoneDetail item = new PrjMilestoneDetail();
                            List<string> ids = new List<string> {
                                str.ToLower()
                            };
                            string format = "\r\n                                    SELECT PT_PrjInfo.PrjGuid,PT_PrjInfo.PrjCode,PT_PrjInfo.PrjName,PT_PrjInfo_ZTB_Detail.ProjPeopleName FROM  PT_PrjInfo  --WHERE PrjGuid IN( SELECT PrjGuid FROM  PT_PrjInfo_ZTB_Detail WHERE SetUpFlowState=1 AND ProjFlowSate=-1)\r\n                                    JOIN  PT_PrjInfo_ZTB_Detail ON  PT_PrjInfo.PrjGuid=PT_PrjInfo_ZTB_Detail.PrjGuid                                 \r\n                                    WHERE  PT_PrjInfo.PrjGuid='{0}'";
                            format = string.Format(format, str);
                            DataTable table = base.ExecuteQuery(format, new SqlParameter[0]);
                            if (table.Rows[0]["ProjPeopleName"].ToString() != string.Empty)
                            {
                                item.PrjCode = table.Rows[0]["PrjCode"].ToString();
                                item.Id = Guid.NewGuid().ToString();
                                item.PrjName = table.Rows[0]["PrjName"].ToString();
                                item.RptDate = RptDate;
                                item.UserName = table.Rows[0]["ProjPeopleName"].ToString();
                                item.UserCode = pTyhmc.v_yhdm;
                                item.DepCode = pTyhmc.i_bmdm;
                                item.DepName = tdbm.V_BMMC;
                                item.StoreAmount = this.GetprjCost(ids, 1.0, item.UserName);
                                switch (pair.Key)
                                {
                                    case 1:
                                        item.Stone1 = 1;
                                        break;

                                    case 2:
                                        item.Stone2 = 1;
                                        item.ForeCast = this.GetprjCost(ids, 0.05, item.UserName);
                                        break;

                                    case 4:
                                        item.Stone4 = 1;
                                        item.ForeCast = this.GetprjCost(ids, 0.2, item.UserName);
                                        break;

                                    case 5:
                                        item.Stone5 = 1;
                                        item.ForeCast = this.GetprjCost(ids, 0.6, item.UserName);
                                        break;

                                    case 6:
                                        item.Stone6 = 1;
                                        item.ForeCast = this.GetprjCost(ids, 0.8, item.UserName);
                                        break;

                                    case 7:
                                        item.Stone7 = 1;
                                        item.ForeCast = this.GetprjCost(ids, 0.9, item.UserName);
                                        break;

                                    case 8:
                                        item.Stone8 = 1;
                                        item.ForeCast = this.GetprjCost(ids, 1.0, item.UserName);
                                        break;

                                    case 9:
                                        item.ForeCast = this.GetprjCost(ids, 0.0, item.UserName);
                                        item.StoreAmount = this.GetprjCost(ids, 0.0, item.UserName);
                                        item.Stone9 = 1;
                                        break;
                                }
                                item.NextForeCast = item.StoreAmount - item.ForeCast;
                                if (item.StoreAmount != 0M)
                                {
                                    item.StoreSwitchRate = decimal.Divide(item.ForeCast, item.StoreAmount);
                                }
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public DataTable GetBudgetAnalysis(string userCode)
        {
            string str = ConfigHelper.Get("IsWBSRelevance");
            string str2 = ConfigHelper.Get("IsIncomeContractApprove");
            StringBuilder builder = new StringBuilder();
            if (str == "0")
            {
                builder.Append("\r\n                SELECT PART1.PrjGuid,PrjCode,PrjName,StartDate,EndDate,Tender,\r\n                ISNULL(IndirBud,0) AS IndirBud,ISNULL(IndirAcc,0) AS IndirAcc,\r\n                CONVERT(DECIMAL(18,3),ISNULL(Target,0)) AS Target,\r\n                CONVERT(DECIMAL(18,3),ISNULL(Reality,0)) AS Reality,\r\n                Convert(Nvarchar(30), cast(ISNULL((ISNULL(Reality,0)+ISNULL(IndirAcc,0))/ NULLIF(ISNULL(Target,0)+ISNULL(IndirBud,0),0),0)*100 as decimal(20,2)))+'%' PercentCompleted   \r\n                FROM \r\n                (\r\n                    SELECT * FROM\r\n                    (\r\n                        SELECT PrjGuid,PrjCode,PrjName,StartDate,EndDate,(SumContractPrice + ISNULL(SumChangePrice,0)) AS TENDER FROM\r\n                        (\r\n                            --统计收入合同金额\r\n                            SELECT PRJ.PrjGuid,PrjCode,PrjName,PRJ.StartDate,PRJ.EndDate,\r\n\t\t\t                SUM(ISNULL(ContractPrice,0)) AS SumContractPrice FROM PT_PrjInfo AS PRJ ");
                if (str2 == "0")
                {
                    builder.AppendFormat(" \r\n                                LEFT JOIN (SELECT * FROM Con_Incomet_Contract WHERE ConState!=4)AS INCON ON PRJ.PrjGuid=INCON.Project\r\n                                LEFT JOIN Pt_PrjInfo_ZTB_Detail detail ON  PRJ.PrjGuid=detail.PrjGuid \r\n                                WHERE IsValid=1 AND (CHARINDEX('{0}',PrjManager)>0  OR CHARINDEX('{1}',Podepom)>0)  \r\n\t\t\t                    AND SetUpFlowState=1 AND PrjState<>17 ", userCode, userCode);
                }
                else
                {
                    builder.AppendFormat(" \r\n                                LEFT JOIN (SELECT * FROM Con_Incomet_Contract WHERE ConState!=4  AND FlowState=1)AS INCON ON PRJ.PrjGuid=INCON.Project\r\n                                LEFT JOIN Pt_PrjInfo_ZTB_Detail detail ON  PRJ.PrjGuid=detail.PrjGuid \r\n                                WHERE IsValid=1 AND (CHARINDEX('{0}',PrjManager)>0  OR CHARINDEX('{1}',Podepom)>0)  \r\n\t\t\t                    AND SetUpFlowState=1 AND PrjState<>17 ", userCode, userCode);
                }
                builder.Append("\r\n                            GROUP BY PRJ.PrjGuid,PrjCode,PrjName,PRJ.StartDate,PRJ.EndDate\r\n                        ) AS CONPRICE\r\n                        LEFT JOIN (\r\n                            --统计项目的变更合同金额\r\n                            SELECT Project,SUM(ISNULL(ChangePrice,0)) AS SumChangePrice FROM Con_Incomet_Contract AS INCON \r\n                            LEFT JOIN Con_Incomet_Modify AS INMOD ON INCON.ContractID=INMOD.ContractID\r\n                            WHERE ConState!=4 AND FlowState=1 GROUP BY Project\r\n                        )AS CHANGEPRICE ON CONPRICE.PrjGuid=CHANGEPRICE.Project\r\n                    ) AS TENDER \r\n                    LEFT JOIN \r\n                    (\r\n\t\t                SELECT ProjectId,SUM(ISNULL(AccountAmount,0)) AS INDIRBUD FROM Bud_IndirectBudget \r\n\t\t                WHERE LEN(CBSCode)>6 AND State='2' GROUP BY ProjectId\r\n                    )AS INDIR ON CONVERT(NVARCHAR(50),TENDER.PrjGuid)=INDIR.ProjectId\r\n                    LEFT JOIN\r\n                    (\r\n\t\t                SELECT ProjectId AS ProjectId2,SUM(Amount) AS IndirAcc FROM Bud_IndirectDiaryCost AS Diary\r\n\t\t                LEFT JOIN  Bud_IndirectDiaryDetails AS DiaryDetails ON Diary.InDiaryId=DiaryDetails.InDiaryId\r\n\t\t                WHERE FlowState='1' AND CBSCode IS NOT NULL AND CBSCode <>'' GROUP BY ProjectId\r\n                    )AS DIARYS ON TENDER.PrjGuid=DIARYS.ProjectId2\r\n                )AS PART1 \r\n                LEFT JOIN \r\n                (\r\n\t                SELECT * FROM \r\n\t                (\r\n\t\t                SELECT PrjGuid,SUM(BudTotal) Target FROM\r\n\t\t                (\r\n\t\t\t                SELECT BudInfo.PrjId PrjGuid,BudInfo.TaskId,(Total+ISNUll(ModifyAmount,0)) BudTotal FROM \r\n\t\t\t                (\r\n\t\t\t\t                SELECT BudTask.PrjId,TaskId,ISNULL(BudTask.Total,0) Total,Version FROM Bud_Task AS BudTask\r\n\t\t\t\t                INNER JOIN vGetCurBudVersion on BudTask.PrjId=vGetCurBudVersion.PrjId \r\n\t\t\t\t                AND BudTask.Version=vGetCurBudVersion.CurVersion \r\n\t\t\t\t                WHERE len(OrderNumber)=3 AND TaskType=''\r\n\t\t\t                ) BudInfo \r\n\t\t\t                LEFT JOIN \r\n\t\t\t                (\r\n\t\t\t\t                --预算变更\r\n\t\t\t\t                SELECT PrjId,SUM(Total) ModifyAmount,modifyTask.TaskId FROM Bud_Modify modify\r\n\t\t\t\t                INNER JOIN Bud_ModifyTask modifyTask ON modify.ModifyId=modifyTask.ModifyId\r\n\t\t\t\t                WHERE FlowState=1 AND ModifyType=1 Group By PrjId,modifyTask.TaskId  \r\n\t\t\t                ) modifyInfo ON BudInfo.TaskId=modifyInfo.TaskId\r\n\t\t                ) Bud GROUP BY PrjGuid\r\n\t                ) AS TARGET \r\n\t                LEFT JOIN \r\n\t                (\r\n\t\t                --施工报量的工程量*目标成本的综合单价\r\n\t\t                SELECT ConsRep.PrjId,SUM(ISNULL(AccountingQuantity,0)*ISNULL(UnitPrice,0)) AS Reality \r\n\t\t                FROM Bud_ConsTask AS ConsTask\r\n\t\t                LEFT JOIN Bud_ConsReport AS ConsRep ON ConsTask.ConsReportId=ConsRep.ConsReportId\r\n\t\t                LEFT JOIN \r\n\t\t                (\r\n\t\t\t                --预算\r\n\t\t\t                SELECT PrjId,TaskId,ISNULL(Total/NULLIF(Quantity,0),0) UnitPrice FROM \r\n\t\t\t                (\r\n\t\t\t\t                SELECT budInfo.PrjId,budInfo.TaskId,(budInfo.Total+ISNULL(modifyInfo.Total,0)) Total,\r\n\t\t\t\t                (budInfo.Quantity+ISNULL(modifyInfo.Quantity,0)) Quantity FROM \r\n\t\t\t\t                (\r\n\t\t\t\t\t                --完整的预算\r\n\t\t\t\t\t                SELECT BudTask.PrjId,TaskId,ISNULL(BudTask.Total,0) Total,ISNULL(Quantity,0) Quantity\r\n\t\t\t\t\t                FROM Bud_Task AS BudTask \r\n\t\t\t\t\t                INNER JOIN vGetCurBudVersion on BudTask.PrjId=vGetCurBudVersion.PrjId \r\n\t\t\t\t\t                AND BudTask.Version=vGetCurBudVersion.CurVersion\r\n\t\t\t\t\t                UNION\r\n\t\t\t\t\t                SELECT PrjId,ModifyTaskId TaskId,Total,Quantity FROM Bud_Modify modify\r\n\t\t\t\t\t                JOIN Bud_ModifyTask modifyTask ON modify.modifyId=modifyTask.modifyId\r\n\t\t\t\t\t                WHERE flowState=1 AND ModifyType=0 \r\n\t\t\t\t                ) budInfo\r\n\t\t\t\t                LEFT JOIN \r\n\t\t\t\t                (\r\n\t\t\t                        --清单内的预算变更\r\n\t\t\t                        SELECT TaskId,PrjId,SUM(Quantity) Quantity,SUM(Total) Total FROM Bud_Modify modify\r\n\t\t\t                        JOIN Bud_ModifyTask modifyTask ON modify.ModifyId=modifyTask.ModifyId\r\n\t\t\t                        WHERE FlowState=1 AND ModifyType=1\r\n\t\t\t                        GROUP BY TaskId,PrjId\r\n\t\t\t\t                ) modifyInfo ON budInfo.TaskId=modifyInfo.TaskId\r\n\t\t\t                ) Bud \r\n\t\t                ) AS BudTask ON ConsTask.TaskId=BudTask.TaskId\r\n\t\t                WHERE flowState=1 GROUP BY ConsRep.PrjId\r\n\t                ) AS REALITY ON TARGET.PrjGuid=REALITY.PrjId\r\n                )AS PART2 ON PART1.PrjGuid=PART2.PrjGuid ORDER BY StartDate DESC ");
            }
            else
            {
                builder.Append("\r\n                SELECT Part1.PrjGuid,PrjCode,PrjName,StartDate,EndDate,Tender,\r\n                ISNULL(IndirBud,0) AS IndirBud,ISNULL(IndirAcc,0) AS IndirAcc,\r\n                CONVERT(DECIMAL(18,3),ISNULL(Target,0)) AS Target,\r\n                CONVERT(DECIMAL(18,3),ISNULL(Reality,0)) AS Reality,\r\n                Convert(Nvarchar(30), cast(ISNULL((ISNULL(Reality,0)+ISNULL(IndirAcc,0))/ NULLIF(ISNULL(Target,0)+ISNULL(IndirBud,0),0),0)*100 as decimal(20,2)))+'%' PercentCompleted    \r\n                FROM \r\n                (\r\n                    SELECT * FROM\r\n                    (\r\n                        SELECT PrjGuid,PrjCode,PrjName,StartDate,EndDate,(SumContractPrice + ISNULL(SumChangePrice,0)) AS Tender FROM\r\n                        (\r\n                            --统计收入合同金额\r\n                            SELECT PRJ.PrjGuid,PrjCode,PrjName,PRJ.StartDate,PRJ.EndDate,\r\n\t\t\t                SUM(ISNULL(ContractPrice,0)) AS SumContractPrice FROM PT_PrjInfo AS PRJ  ");
                if (str2 == "0")
                {
                    builder.AppendFormat(" \r\n                                LEFT JOIN (SELECT * FROM Con_Incomet_Contract WHERE ConState!=4)AS INCON ON PRJ.PrjGuid=INCON.Project\r\n                                LEFT JOIN Pt_PrjInfo_ZTB_Detail detail ON  PRJ.PrjGuid=detail.PrjGuid \r\n                                WHERE IsValid=1 AND (CHARINDEX('{0}',PrjManager)>0  OR CHARINDEX('{1}',Podepom)>0)  \r\n\t\t\t                    AND SetUpFlowState=1 AND PrjState<>17 ", userCode, userCode);
                }
                else
                {
                    builder.AppendFormat(" \r\n                                LEFT JOIN (SELECT * FROM Con_Incomet_Contract WHERE ConState!=4  AND FlowState=1)AS INCON ON PRJ.PrjGuid=INCON.Project\r\n                                LEFT JOIN Pt_PrjInfo_ZTB_Detail detail ON  PRJ.PrjGuid=detail.PrjGuid \r\n                                WHERE IsValid=1 AND (CHARINDEX('{0}',PrjManager)>0  OR CHARINDEX('{1}',Podepom)>0)  \r\n\t\t\t                    AND SetUpFlowState=1 AND PrjState<>17 ", userCode, userCode);
                }
                builder.Append(" GROUP BY PRJ.PrjGuid,PrjCode,PrjName,PRJ.StartDate,PRJ.EndDate\r\n                        ) AS ConPrice\r\n                        LEFT JOIN (\r\n                            --统计项目的变更合同金额\r\n                            SELECT Project,SUM(ISNULL(ChangePrice,0)) AS SumChangePrice FROM Con_Incomet_Contract AS INCON \r\n                            LEFT JOIN Con_Incomet_Modify AS INMOD ON INCON.ContractID=INMOD.ContractID\r\n                            WHERE ConState!=4 AND FlowState=1 GROUP BY Project\r\n                        )AS ChangePrice ON ConPrice.PrjGuid=ChangePrice.Project\r\n                    ) AS Tender \r\n                    LEFT JOIN \r\n                    (\r\n\t\t                --间接成本预算(间接成本预算发生费用)\r\n                        SELECT ProjectId,SUM(ISNULL(AccountAmount,0)) AS IndirBud FROM Bud_IndirectBudget \r\n                        WHERE LEN(CBSCode)>6 AND State='2' GROUP BY ProjectId\r\n                    )AS Indir ON CONVERT(NVARCHAR(50),Tender.PrjGuid)=Indir.ProjectId\r\n                    LEFT JOIN\r\n                    (\r\n\t\t                --间接成本日记账(间接成本实际发生费用)\r\n                        SELECT ProjectId AS ProjectId2,SUM(Amount) AS IndirAcc FROM Bud_IndirectDiaryCost AS Diary\r\n                        LEFT JOIN  Bud_IndirectDiaryDetails AS DiaryDetails ON Diary.InDiaryId=DiaryDetails.InDiaryId\r\n                        WHERE FlowState='1' AND CBSCode IS NOT NULL AND CBSCode <>'' GROUP BY ProjectId\r\n                    )AS Diarys ON Tender.PrjGuid=Diarys.ProjectId2\r\n                )AS Part1 \r\n                LEFT JOIN \r\n                (\r\n                    SELECT PrjGuid,(Target+ISNULL(ModifyAmount,0)) Target,Reality  FROM (\r\n\t\t                --原预算\r\n\t\t                SELECT Task.PrjId AS PrjGuid,\r\n\t\t                SUM(ISNULL(ResourceQuantity,0)*ISNULL(ResourcePrice,0)) AS Target \r\n\t\t                FROM Bud_Task AS Task \r\n\t\t                INNER JOIN vGetCurBudVersion ON Task.PrjId=vGetCurBudVersion.PrjId AND Version=CurVersion\r\n\t\t                LEFT JOIN Bud_TaskResource AS TaskRES ON Task.TaskId =TaskRES.TaskId\r\n\t\t                WHERE TaskType=''\r\n\t\t                GROUP BY Task.PrjId\r\n\t                )AS Target \r\n\t                LEFT JOIN \r\n\t                (\r\n\t\t                --预算变更\r\n\t\t                SELECT PrjId,SUM(ResourceQuantity*ResourcePrice) ModifyAmount FROM Bud_Modify modify\r\n\t\t                INNER JOIN Bud_ModifyTask modifyTask ON modify.ModifyId=modifyTask.ModifyId\r\n\t\t                INNER JOIN Bud_ModifyTaskRes modifyTaskRes ON modifyTask.ModifyTaskId=modifyTaskRes.ModifyTaskId\r\n\t\t                WHERE FlowState=1 Group By PrjId\t\t\t\r\n\t                ) ModifyAmount ON Target.PrjGuid=ModifyAmount.PrjId\r\n                    LEFT JOIN (\r\n\t\t                --施工报量\r\n\t\t                SELECT PrjId,SUM(ISNULL(CONSRES.AccountingQuantity,0)*ISNULL(CONSRES.UnitPrice,0)) AS Reality \r\n\t\t                FROM Bud_ConsTaskRes AS CONSRES\r\n\t\t                LEFT JOIN  Bud_ConsTask AS CONSTask ON  CONSRES.ConsTaskId=CONSTask.ConsTaskId\r\n\t\t                LEFT JOIN Bud_ConsReport AS CONSREP ON CONSTask.ConsReportId=CONSREP.ConsReportId\r\n\t\t                WHERE FlowState=1 GROUP BY PrjId\r\n\t                ) AS Reality ON Target.PrjGuid=Reality.PrjId\r\n                )AS Part2 ON Part1.PrjGuid=Part2.PrjGuid ORDER BY StartDate DESC ");
            }
            return base.ExecuteQuery(builder.ToString(), null);
        }

        public PrjMilestone GetById(string Id)
        {
            return (from p in this
                where p.Id == Id
                select p).FirstOrDefault<PrjMilestone>();
        }

        public List<BasicCodeList> GetCodeLst()
        {
            BasicCodeListService service = new BasicCodeListService();
            return (from p in service
                where p.TypeCode == "ProjectState"
                select p).ToList<BasicCodeList>();
        }

        public int getdepmentCount(string where)
        {
            string cmdText = string.Format(" \r\n                            WITH Prj_Info AS(\r\n\t                            SELECT prjMiles.*,yh.v_xm,yh.i_bmdm FROM (\r\n\t                            SELECT ROW_NUMBER() OVER(partition by UserCode \r\n\t                            order by RptDate desc) AS orde,* FROM Prj_Milestone) AS prjMiles\r\n                            LEFT JOIN PT_yhmc AS yh ON prjMiles.UserCode=yh.v_yhdm\r\n\t                            WHERE orde=1\r\n                            ),Prj_User_Info AS(\r\n\t                            SELECT PrjInfo.*,dm.V_BMMC,dm.v_bmqc FROM Prj_Info AS PrjInfo \r\n                            LEFT JOIN PT_d_bm AS dm\r\n\t                            ON PrjInfo.i_bmdm=dm.i_bmdm\r\n                            ),CRT_Miles AS(\r\n                            SELECT v_BMMC,i_bmdm,RptDate \r\n                            FROM Prj_User_Info group by i_bmdm,v_BMMC,RptDate\r\n                            ),CRT_dept_Miles AS(\r\n                            SELECT*FROM CRT_Miles WHERE 1=1 {0}\r\n                            )SELECT COUNT(*) AS Num FROM CRT_dept_Miles", where);
            DataTable table = base.ExecuteQuery(cmdText, null);
            int num = 0;
            foreach (DataRow row in table.Rows)
            {
                num = (row["Num"] != null) ? Convert.ToInt32(row["Num"]) : 0;
            }
            return num;
        }

        public DataTable GetdepmentprjMilestone(string sWhere, string where, int pageSize, int pageIndex)
        {
            DateTime time = DateTime.Today.AddDays((double) (1 - DateTime.Today.Day));
            DateTime time2 = DateTime.Today.AddDays((double) ((1 - DateTime.Today.Day) + 14));
            DateTime now = DateTime.Now;
            DateTime time4 = new DateTime();
            if (now.Day < time2.Day)
            {
                time4 = Convert.ToDateTime(time);
            }
            else
            {
                time4 = Convert.ToDateTime(time2);
            }
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageIndex * pageSize;
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@Start", num),
                new SqlParameter("@End", num2)
            };
            string cmdText = string.Format(" \r\n                            WITH Prj_Info AS(\r\n\t                            SELECT prjMiles.*,yh.v_xm,yh.i_bmdm FROM (\r\n\t                            SELECT ROW_NUMBER() OVER(partition by UserCode \r\n\t                            order by RptDate desc) AS orde,* FROM Prj_Milestone) AS prjMiles\r\n                            LEFT JOIN PT_yhmc AS yh ON prjMiles.UserCode=yh.v_yhdm\t                             \r\n                            WHERE 1=1 AND RptDate='{2}'{0}                           \r\n                            ),Prj_User_Info AS(\r\n\t                            SELECT PrjInfo.*,dm.V_BMMC,dm.v_bmqc FROM Prj_Info AS PrjInfo \r\n                            LEFT JOIN PT_d_bm AS dm\r\n\t                            ON PrjInfo.i_bmdm=dm.i_bmdm\r\n                            ),CRT_Miles AS(\r\n                            SELECT v_BMMC,Sum(StoreAmount) AS StoreAmount,SUM(ForeCast) AS ForeCast,\r\n                            SUM(NextForeCast)AS NextForeCast,SUM(Stone1) AS Stone1,SUM(Stone2)AS Stone2\r\n                            ,SUM(Stone3)AS Stone3,SUM(Stone4)AS Stone4,SUM(Stone5)AS Stone5,SUM(Stone6)AS Stone6\r\n                            ,SUM(Stone7) AS Stone7,SUM(Stone8) AS Stone8,SUM(Stone9)AS Stone9,i_bmdm  \r\n                            FROM Prj_User_Info group by i_bmdm,v_BMMC\r\n                            ),CRT_dept_Miles AS(\r\n                            SELECT ROW_NUMBER() OVER(ORDER BY i_bmdm )AS NUM, * FROM CRT_Miles WHERE 1=1 {1}\r\n                            )SELECT * FROM CRT_dept_Miles WHERE NUM BETWEEN @Start AND @End", sWhere, where, time4);
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public decimal GetprjCost(List<string> Ids, double percent, string userCode)
        {
            DataTable table = new DataTable();
            if (Ids.Count != 0)
            {
                string inParameterSql = base.GetInParameterSql(Ids);
                string cmdText = string.Format("\r\n                WITH CRT_PrjInfo AS(\r\n                    select ZTb.PrjGuid,PrjName,ISNULL(PrjCost,0)AS PrjCost from PT_PrjInfo_ZTB AS ZTb\r\n                    JOIN (SELECT *FROM  PT_PrjInfo_ZTB_Detail WHERE ProjPeopleName='{0}')AS ZTbDetail\r\n                    ON ZTb.PrjGuid=ZTbDetail.PrjGuid\r\n                    WHERE ProjFlowSate=1 AND PrjState<5\r\n                    UNION \r\n                    SELECT Info.PrjGuid,PrjName, ISNULL(PrjCost,0)AS PrjCost FROM PT_prjInfo AS Info\r\n                    JOIN (SELECT *FROM  PT_PrjInfo_ZTB_Detail WHERE ProjPeopleName='{0}') AS ZTbdetail\r\n                    ON Info.PrjGuid=ZTbdetail.PrjGuid\r\n                    WHERE PrjState>=5\r\n                ) \r\n                SELECT  SUM(PrjCost)  FROM CRT_PrjInfo \r\n                WHERE PrjGuid IN ({1})\t\r\n                ", userCode, inParameterSql);
                table = base.ExecuteQuery(cmdText, new SqlParameter[0]);
            }
            decimal num = 0M;
            try
            {
                if (table.Rows.Count > 0)
                {
                    num = Convert.ToDecimal(table.Rows[0][0]) * ((decimal) percent);
                }
            }
            catch
            {
            }
            return num;
        }

        public List<string> getPrjIds(string userCode)
        {
            List<string> list = new List<string>();
            List<string> ZTbIds = (from p in this.prjZTbDetailSer
                where (p.ProjPeopleName == userCode) && (p.ProjFlowSate == 1)
                select p.PrjGuid.ToString().ToLower()).ToList<string>();
            List<string> first = (from p in this.prjInfoZTbSer
                where ZTbIds.Contains(p.PrjGuid.ToString().ToLower())
                select p.PrjGuid.ToString().ToLower()).ToList<string>();
            List<string> LstZTbdetailIds = (from p in this.prjZTbDetailSer
                where ((p.ProjPeopleName == userCode) && (p.ProjFlowSate == -1)) && (p.SetUpFlowState == 1)
                select p.PrjGuid.ToString().ToLower()).ToList<string>();
            List<string> second = (from p in this.prjInfoSer
                where LstZTbdetailIds.Contains(p.PrjGuid.ToString())
                select p.PrjGuid.ToString().ToLower()).ToList<string>();
            return first.Union<string>(second).Distinct<string>().ToList<string>();
        }

        public List<string> GetPurchaseIds(List<string> prjIds)
        {
            List<string> list = new List<string>();
            string cmdText = string.Format("\r\n                            SELECT ConPayCon.PrjGuid,SUM(sprice)AS sprice FROM Sm_Purchase AS SmPurchase\r\n                            LEFT JOIN\r\n\t                            (SELECT pscode,SUM(sprice*number) AS sprice FROM Sm_Purchase_Stock\r\n\t                            Group by pscode) AS PurchStock\r\n                            ON SmPurchase.pcode=PurchStock.pscode\r\n                            JOIN Con_Payout_Contract AS ConPayCon\r\n                            ON ConPayCon.ContractID=SmPurchase.contract\r\n\t                            WHERE SmPurchase.FlowState=1\r\n\t                            Group by PrjGuid", new object[0]);
            DataTable table = base.ExecuteQuery(cmdText, null);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    string item = row["PrjGuid"].ToString().ToLower();
                    if (prjIds.Contains(item) && !list.Contains(row["PrjGuid"].ToString()))
                    {
                        list.Add(row["PrjGuid"].ToString());
                    }
                }
            }
            return list;
        }

        public int GetRptDateMileCount(string where)
        {
            string cmdText = string.Format(" WITH Prj_Info AS(\r\n\t                            SELECT prjMiles.*,yh.v_xm,yh.i_bmdm FROM \r\n\t                            Prj_Milestone AS prjMiles\r\n                            LEFT JOIN PT_yhmc AS yh ON prjMiles.UserCode=yh.v_yhdm\r\n                            ),Prj_User_Info AS(\r\n\t                            SELECT PrjInfo.*,dm.V_BMMC,dm.v_bmqc FROM Prj_Info AS PrjInfo \r\n                            LEFT JOIN PT_d_bm AS dm\r\n\t                            ON PrjInfo.i_bmdm=dm.i_bmdm\r\n                                WHERE 1=1  {0}\r\n                            ),CRT_Miles AS(\r\n                            SELECT RptDate FROM Prj_User_Info \r\n                            GROUP BY RptDate \r\n                            )SELECT COUNT(*) AS NUM FROM CRT_Miles", where);
            DataTable table = base.ExecuteQuery(cmdText, null);
            int num = 0;
            foreach (DataRow row in table.Rows)
            {
                num = (row["NUM"] != null) ? Convert.ToInt32(row["NUM"]) : 0;
            }
            return num;
        }

        public DataTable GetRptDateMileSouce(string where, int pageIndex, int pageSize)
        {
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageIndex * pageSize;
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@Start", num),
                new SqlParameter("@End", num2)
            };
            string cmdText = string.Format(" \r\n                            WITH Prj_Info AS(\r\n\t                            SELECT prjMiles.*,yh.v_xm,yh.i_bmdm FROM \r\n\t                            Prj_Milestone AS prjMiles\r\n                            LEFT JOIN PT_yhmc AS yh ON prjMiles.UserCode=yh.v_yhdm\r\n                            ),Prj_User_Info AS(\r\n\t                            SELECT PrjInfo.*,dm.V_BMMC,dm.v_bmqc FROM Prj_Info AS PrjInfo \r\n                            LEFT JOIN PT_d_bm AS dm\r\n\t                            ON PrjInfo.i_bmdm=dm.i_bmdm\r\n                                WHERE 1=1  {0}\r\n                            ),CRT_Miles AS(\r\n                            SELECT RptDate,SUM(StoreAmount)AS StoreAmount,SUM(ForeCast)AS ForeCast,\r\n                            SUM(NextForeCast)AS NextForeCast,SUM(Stone1)AS Stone1,SUM(Stone2)AS Stone2,\r\n                            SUM(Stone3)AS Stone3,SUM(Stone4)AS Stone4,SUM(Stone5)AS Stone5,SUM(Stone6)AS Stone6,\r\n                            SUM(Stone7) AS Stone7,SUM(Stone8)AS Stone8,SUM(Stone9)AS Stone9 FROM Prj_User_Info \r\n                            GROUP BY RptDate \r\n                            ),RPT_Miles AS(\r\n                            SELECT ROW_NUMBER() OVER(ORDER BY RptDate DESC)AS NUM,* FROM CRT_Miles\r\n                            )SELECT * FROM RPT_Miles WHERE NUM BETWEEN @Start AND @End ", where);
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public List<string> GetSmOutInfo(List<string> prjIds, double percent)
        {
            List<string> list = new List<string>();
            string cmdText = string.Format(" WITH CRT_SmOutInfo AS(\r\n                            --出库信息\r\n\t                            SELECT procode,SUM(sprice)AS Sumprice FROM Sm_OutReserve AS SmOut\r\n                            LEFT JOIN\r\n\t                            (SELECT orcode, SUM(sprice*number) AS sprice FROM Sm_Out_Stock \r\n\t                            GROUP BY orcode)AS OutStock\r\n                            ON Smout.orcode=OutStock.orcode\r\n\t                            WHERE FlowState=1\r\n\t                            group by procode\r\n                            ),CRT_Sm_PurchaseInfo AS (\r\n                            --采购单信息\r\n\t                            SELECT ConPayCon.PrjGuid,SUM(sprice)AS sprice FROM Sm_Purchase AS SmPurchase\r\n                            LEFT JOIN\r\n\t                            (SELECT pscode,SUM(sprice*number) AS sprice FROM Sm_Purchase_Stock\r\n\t                            Group by pscode) AS PurchStock\r\n                            ON SmPurchase.pcode=PurchStock.pscode\r\n                            JOIN Con_Payout_Contract AS ConPayCon\r\n                            ON ConPayCon.ContractID=SmPurchase.contract\r\n\t                            WHERE SmPurchase.FlowState=1\r\n\t                            Group by PrjGuid\r\n                            )SELECT procode,Sumprice,sprice FROM CRT_SmOutInfo\r\n                             JOIN CRT_Sm_PurchaseInfo\r\n                            ON CRT_SmOutInfo.procode=CRT_Sm_PurchaseInfo.PrjGuid\r\n                            WHERE 1=1 AND Sumprice>=sprice*{0}", percent);
            DataTable table = base.ExecuteQuery(cmdText, null);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (prjIds.Contains<object>(row["procode"]))
                    {
                        list.Add(row["procode"].ToString());
                    }
                }
            }
            return list;
        }

        public List<string> GetsmWPlanIds(string userCode, List<string> LstPrjId)
        {
            this.state = 2;
            SmWantplanService service = new SmWantplanService();
            List<string> prjIds = this.getPrjIds(userCode);
            List<string> source = service.SmWlanPlanIds(prjIds);
            List<string> list3 = new List<string>();
            if (source.Count<string>() > 0)
            {
                foreach (string str in source)
                {
                    if (!LstPrjId.Contains(str.ToLower()))
                    {
                        LstPrjId.Add(str.ToLower());
                        if (!list3.Contains(str.ToLower()))
                        {
                            list3.Add(str.ToLower());
                        }
                    }
                }
            }
            return list3;
        }

        public int GetUserPrjCount(string where)
        {
            int num = 0;
            string cmdText = string.Format("WITH Prj_Info AS(\r\n\t                            SELECT prjMiles.*,yh.v_xm,yh.i_bmdm FROM (\r\n\t                            SELECT ROW_NUMBER() OVER(partition by UserCode \r\n\t                            order by RptDate desc) AS orde,* FROM Prj_Milestone) AS prjMiles\r\n                            LEFT JOIN PT_yhmc AS yh ON prjMiles.UserCode=yh.v_yhdm\r\n\t                            WHERE orde=1\r\n                            ),Prj_User_Info AS(\r\n\t                            SELECT PrjInfo.*,dm.V_BMMC,dm.v_bmqc FROM Prj_Info AS PrjInfo \r\n                            LEFT JOIN PT_d_bm AS dm\r\n\t                            ON PrjInfo.i_bmdm=dm.i_bmdm\r\n                            ),CRT_Miles AS(\r\n                            SELECT COUNT(*) AS Num\r\n                                    FROM Prj_User_Info WHERE 1=1 {0}\r\n                            )SELECT * FROM CRT_Miles ", where);
            DataTable table = base.ExecuteQuery(cmdText, null);
            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    num = (row["Num"] != null) ? Convert.ToInt32(row["Num"]) : 0;
                }
            }
            return num;
        }

        public DataTable GetUserPrjSouce(string where, int pageSize, int pageIndex)
        {
            DateTime time = DateTime.Today.AddDays((double) (1 - DateTime.Today.Day));
            DateTime time2 = DateTime.Today.AddDays((double) ((1 - DateTime.Today.Day) + 14));
            DateTime now = DateTime.Now;
            DateTime time4 = new DateTime();
            if (now.Day < time2.Day)
            {
                time4 = Convert.ToDateTime(time);
            }
            else
            {
                time4 = Convert.ToDateTime(time2);
            }
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageIndex * pageSize;
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@Start", num),
                new SqlParameter("@End", num2)
            };
            string cmdText = string.Format(" WITH Prj_Info AS(\r\n\t                                      SELECT prjMiles.*,yh.v_xm,yh.i_bmdm FROM \r\n\t                                      Prj_Milestone AS prjMiles\r\n                                          LEFT JOIN PT_yhmc AS yh ON prjMiles.UserCode=yh.v_yhdm\r\n                                          WHERE RptDate='{1}'\r\n                                          ),Prj_User_Info AS(\r\n\t                                      SELECT PrjInfo.*,dm.V_BMMC,dm.v_bmqc FROM Prj_Info AS PrjInfo \r\n                                          LEFT JOIN PT_d_bm AS dm\r\n\t                                      ON PrjInfo.i_bmdm=dm.i_bmdm\r\n                                          WHERE 1=1 {0}\r\n                                          ),CRT_Miles AS(\r\n                                          SELECT v_xm,SUM(StoreAmount)AS StoreAmount,SUM(ForeCast)AS ForeCast,\r\n                                          SUM(NextForeCast)AS NextForeCast,SUM(Stone1)AS Stone1,SUM(Stone2)AS Stone2,\r\n                                          SUM(Stone3)AS Stone3,SUM(Stone4)AS Stone4,SUM(Stone5)AS Stone5,SUM(Stone6)AS Stone6,\r\n                                          SUM(Stone7) AS Stone7,SUM(Stone8)AS Stone8,SUM(Stone9)AS Stone9 FROM Prj_User_Info \r\n                                          GROUP BY v_xm \r\n                                          ),RPT_Miles AS(\r\n                                          SELECT ROW_NUMBER() OVER(ORDER BY v_xm DESC)AS NUM,* FROM CRT_Miles\r\n                                          )SELECT * FROM RPT_Miles   WHERE Num BETWEEN @Start AND @End", where, time4);
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public List<PrjMilestone> LstMilestone(DateTime RptDate)
        {
            List<PrjMilestone> list = new List<PrjMilestone>();
            PTYhmcService source = new PTYhmcService();
            foreach (PTyhmc tyhmc in source.AsQueryable<PTyhmc>().ToList<PTyhmc>())
            {
                if ((this.getPrjIds(tyhmc.v_xm).Count > 0) && (tyhmc.v_xm.Trim() != string.Empty))
                {
                    Dictionary<string, decimal> dictionary = this.PrjStateCount(tyhmc.v_xm);
                    PrjMilestone item = new PrjMilestone {
                        Id = Guid.NewGuid().ToString(),
                        RptDate = RptDate,
                        UserCode = tyhmc.v_yhdm,
                        Stone1 = Convert.ToInt32(dictionary["prjZTb"]),
                        Stone2 = Convert.ToInt32(dictionary["sampling"]),
                        Stone4 = Convert.ToInt32(dictionary["prjTend"]),
                        Stone5 = Convert.ToInt32(dictionary["prjBind"]),
                        Stone6 = Convert.ToInt32(dictionary["submitOrder"]),
                        Stone7 = Convert.ToInt32(dictionary["delivery"]),
                        Stone8 = Convert.ToInt32(dictionary["sale"]),
                        Stone9 = Convert.ToInt32(dictionary["giveUp"]),
                        StoreAmount = dictionary["SumprjCost"],
                        ForeCast = dictionary["yearPrjCost"],
                        NextForeCast = dictionary["nextPrjCost"]
                    };
                    if (item.StoreAmount != 0M)
                    {
                        item.StoreSwitchRate = decimal.Divide(item.ForeCast, item.StoreAmount);
                    }
                    else
                    {
                        item.StoreAmount = 0M;
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        public List<string> PrjBindIds(string userCode, List<string> LstPrjId)
        {
            this.state = 5;
            List<string> list = new List<string>();
            using (List<PTPrjInfoZTBDetail>.Enumerator enumerator = (from p in this.prjZTbDetailSer
                where (p.ProjPeopleName == userCode) && (p.SetUpFlowState == 1)
                select p).ToList<PTPrjInfoZTBDetail>().GetEnumerator())
            {
                PTPrjInfoZTBDetail item;
                while (enumerator.MoveNext())
                {
                    item = enumerator.Current;
                    if ((item.ProjFlowSate == 1) && (item.SetUpFlowState == 1))
                    {
                        if (((from p in this.prjInfoZTbSer
                            where ((p.PrjGuid == item.PrjGuid) && (p.BidFlowState == 1)) && (p.PrjState == 5)
                            select p).FirstOrDefault<PTPrjInfoZTB>() != null) && !LstPrjId.Contains(item.PrjGuid.ToString()))
                        {
                            LstPrjId.Add(item.PrjGuid.ToString());
                            if (!list.Contains(item.PrjGuid.ToString()))
                            {
                                list.Add(item.PrjGuid.ToString());
                            }
                        }
                    }
                    else if ((item.ProjFlowSate == -1) && (((item.SetUpFlowState == 1) && ((from p in this.prjInfoSer
                        where (p.PrjGuid == item.PrjGuid) && (p.PrjState >= 5)
                        select p).FirstOrDefault<PTPrjInfo>() != null)) && !LstPrjId.Contains(item.PrjGuid.ToString())))
                    {
                        LstPrjId.Add(item.PrjGuid.ToString());
                        if (!list.Contains(item.PrjGuid.ToString()))
                        {
                            list.Add(item.PrjGuid.ToString());
                        }
                    }
                }
            }
            return list;
        }

        public List<string> prjdeliveryIds(string userCode, List<string> LstPrjId)
        {
            this.state = 7;
            List<string> prjIds = this.getPrjIds(userCode);
            List<string> smOutInfo = this.GetSmOutInfo(prjIds, 0.3);
            List<string> list3 = new List<string>();
            foreach (string str in smOutInfo)
            {
                if (!LstPrjId.Contains(str.ToLower()))
                {
                    LstPrjId.Add(str.ToLower());
                    if (!list3.Contains(str.ToLower()))
                    {
                        list3.Add(str.ToLower());
                    }
                }
            }
            return list3;
        }

        public List<string> PrjgiveIds(string userCode, List<string> LstPrjId)
        {
            this.state = 9;
            List<string> list = new List<string>();
            BasicCodeList list3 = (from p in this.GetCodeLst()
                where p.ItemName == "落标"
                select p).FirstOrDefault<BasicCodeList>();
            int prjState = (list3 == null) ? 6 : list3.ItemCode;
            using (List<PTPrjInfoZTBDetail>.Enumerator enumerator = (from p in this.prjZTbDetailSer
                where p.ProjPeopleName == userCode
                select p).ToList<PTPrjInfoZTBDetail>().GetEnumerator())
            {
                PTPrjInfoZTBDetail item;
                while (enumerator.MoveNext())
                {
                    item = enumerator.Current;
                    if (((from p in this.prjInfoZTbSer
                        where (p.PrjGuid == item.PrjGuid) && ((p.GiveUpFlowState == 1) || ((p.PrjState == prjState) && (p.BidFlowState == 1)))
                        select p).FirstOrDefault<PTPrjInfoZTB>() != null) && !LstPrjId.Contains(item.PrjGuid.ToString()))
                    {
                        LstPrjId.Add(item.PrjGuid.ToString());
                        if (!list.Contains(item.PrjGuid.ToString()))
                        {
                            list.Add(item.PrjGuid.ToString());
                        }
                    }
                }
            }
            return list;
        }

        public List<string> PrjSaleIds(string userCode, List<string> LstPrjId)
        {
            this.state = 8;
            List<string> prjIds = this.getPrjIds(userCode);
            List<string> list2 = new List<string>();
            foreach (string str in this.GetSmOutInfo(prjIds, 0.6))
            {
                if (!LstPrjId.Contains(str.ToLower()))
                {
                    LstPrjId.Add(str.ToLower());
                    if (!list2.Contains(str.ToLower()))
                    {
                        list2.Add(str.ToLower());
                    }
                }
            }
            return list2;
        }

        public Dictionary<int, List<string>> PrjState(string userCode)
        {
            List<string> lstPrjId = new List<string>();
            Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
            List<string> list2 = this.PrjgiveIds(userCode, lstPrjId);
            dictionary.Add(this.state, list2);
            List<string> list3 = this.PrjSaleIds(userCode, lstPrjId);
            dictionary.Add(this.state, list3);
            List<string> list4 = this.prjdeliveryIds(userCode, lstPrjId);
            dictionary.Add(this.state, list4);
            List<string> list5 = this.PrjsubmitOrder(userCode, lstPrjId);
            dictionary.Add(this.state, list5);
            List<string> list6 = this.PrjBindIds(userCode, lstPrjId);
            dictionary.Add(this.state, list6);
            List<string> list7 = this.prjTendIds(userCode, lstPrjId);
            dictionary.Add(this.state, list7);
            List<string> smWPlanIds = this.GetsmWPlanIds(userCode, lstPrjId);
            dictionary.Add(this.state, smWPlanIds);
            List<string> list9 = this.PrjZTbIds(userCode, lstPrjId);
            dictionary.Add(this.state, list9);
            return dictionary;
        }

        public Dictionary<string, decimal> PrjStateCount(string userCode)
        {
            List<string> lstPrjId = new List<string>();
            List<string> ids = this.PrjgiveIds(userCode, lstPrjId);
            List<string> list3 = this.PrjSaleIds(userCode, lstPrjId);
            List<string> list4 = this.prjdeliveryIds(userCode, lstPrjId);
            List<string> list5 = this.PrjsubmitOrder(userCode, lstPrjId);
            List<string> list6 = this.PrjBindIds(userCode, lstPrjId);
            List<string> list7 = this.prjTendIds(userCode, lstPrjId);
            List<string> smWPlanIds = this.GetsmWPlanIds(userCode, lstPrjId);
            List<string> list9 = this.PrjZTbIds(userCode, lstPrjId);
            decimal num = this.GetprjCost(smWPlanIds, 0.05, userCode);
            decimal num2 = this.GetprjCost(list7, 0.2, userCode);
            decimal num3 = this.GetprjCost(list6, 0.6, userCode);
            decimal num4 = this.GetprjCost(list5, 0.8, userCode);
            decimal num5 = this.GetprjCost(list4, 0.9, userCode);
            decimal num6 = this.GetprjCost(list3, 1.0, userCode);
            decimal num7 = ((((((this.GetprjCost(list3, 1.0, userCode) + this.GetprjCost(list4, 1.0, userCode)) + this.GetprjCost(list5, 1.0, userCode)) + this.GetprjCost(list6, 1.0, userCode)) + this.GetprjCost(list7, 1.0, userCode)) + this.GetprjCost(smWPlanIds, 1.0, userCode)) + this.GetprjCost(list9, 1.0, userCode)) + this.GetprjCost(ids, 0.0, userCode);
            decimal num8 = ((((num + num2) + num3) + num4) + num5) + num6;
            Dictionary<string, decimal> dictionary = new Dictionary<string, decimal>();
            dictionary.Add("giveUp", ids.Count);
            dictionary.Add("sale", list3.Count);
            dictionary.Add("delivery", list4.Count);
            dictionary.Add("submitOrder", list5.Count);
            dictionary.Add("prjBind", list6.Count);
            dictionary.Add("prjTend", list7.Count);
            dictionary.Add("sampling", smWPlanIds.Count);
            dictionary.Add("prjZTb", list9.Count);
            dictionary.Add("SumprjCost", num7);
            dictionary.Add("yearPrjCost", num8);
            dictionary.Add("nextPrjCost", num7 - num8);
            return dictionary;
        }

        public List<string> PrjsubmitOrder(string userCode, List<string> LstPrjId)
        {
            this.state = 6;
            List<string> prjIds = this.getPrjIds(userCode);
            List<string> purchaseIds = this.GetPurchaseIds(prjIds);
            List<string> list3 = new List<string>();
            foreach (string str in purchaseIds)
            {
                if (!LstPrjId.Contains(str.ToLower()))
                {
                    LstPrjId.Add(str.ToLower());
                    if (!list3.Contains(str))
                    {
                        list3.Add(str);
                    }
                }
            }
            return list3;
        }

        public List<string> prjTendIds(string userCode, List<string> LstPrjId)
        {
            this.state = 4;
            List<string> list = new List<string>();
            BasicCodeList list3 = (from p in this.GetCodeLst()
                where p.ItemName == "投标"
                select p).FirstOrDefault<BasicCodeList>();
            int prjState = (list3 == null) ? 4 : list3.ItemCode;
            List<string> LstId = (from p in this.prjZTbDetailSer
                where p.ProjPeopleName == userCode
                select p.PrjGuid.ToString()).ToList<string>();
            foreach (PTPrjInfoZTB oztb in (from p in this.prjInfoZTbSer
                where LstId.Contains(p.PrjGuid.ToString()) && (p.PrjState == prjState)
                select p).ToList<PTPrjInfoZTB>())
            {
                if (!LstPrjId.Contains(oztb.PrjGuid.ToString()))
                {
                    LstPrjId.Add(oztb.PrjGuid.ToString());
                    if (!list.Contains(oztb.PrjGuid.ToString()))
                    {
                        list.Add(oztb.PrjGuid.ToString());
                    }
                }
            }
            return list;
        }

        public List<string> PrjZTbIds(string userCode, List<string> LstPrjId)
        {
            this.state = 1;
            List<string> list = new List<string>();
            List<string> ZTbIds = (from p in this.prjZTbDetailSer
                where (p.ProjPeopleName == userCode) && (p.ProjFlowSate == 1)
                select p.PrjGuid.ToString()).ToList<string>();
            foreach (string str in (from p in this.prjInfoZTbSer
                where ZTbIds.Contains(p.PrjGuid.ToString())
                select p.PrjGuid.ToString()).ToList<string>())
            {
                if (!LstPrjId.Contains(str.ToLower()))
                {
                    LstPrjId.Add(str);
                    if (!list.Contains(str))
                    {
                        list.Add(str);
                    }
                }
            }
            return list;
        }
    }
}

