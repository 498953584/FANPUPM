namespace cn.justwin.stockBLL.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class OrganizationDiary
    {
        private OrganizationDiary()
        {
        }

        public void Add(OrganizationDiary orgDiary)
        {
            if (orgDiary != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    int num = Convert.ToInt32(orgDiary.PrjId);
                    Bud_OrgDiaryCost cost = new Bud_OrgDiaryCost {
                        OrgDiaryId = orgDiary.Id,
                        Name = orgDiary.Name,
                        OrgdiaryCode = orgDiary.Code,
                        OrgId = new int?(num),
                        Department = orgDiary.Department,
                        IssuedBy = orgDiary.IssuedBy,
                        InputUser = orgDiary.InputUser,
                        InputDate = orgDiary.InputDate,
                        FlowState = orgDiary.FlowState
                    };
                    entities.AddToBud_OrgDiaryCost(cost);
                    entities.SaveChanges();
                }
            }
        }

        public static OrganizationDiary Create(string id, string prjId, string OrgdiaryCode, string name, string department, string issuedBy, string inputUser, DateTime inputDate)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("组织机构日记", "Id不能为空!!!");
            }
            if (string.IsNullOrEmpty(prjId))
            {
                throw new ArgumentNullException("组织机构日记", "PrjId不能为空!!!");
            }
            return new OrganizationDiary { Id = id, Name = name, PrjId = prjId, Code = OrgdiaryCode, Department = department, InputDate = inputDate, InputUser = inputUser, IssuedBy = issuedBy, FlowState = -1 };
        }

        public static void Del(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_OrgDiaryCost entity = (from m in entities.Bud_OrgDiaryCost
                        where m.OrgDiaryId == id
                        select m).FirstOrDefault<Bud_OrgDiaryCost>();
                    if (entity == null)
                    {
                        throw new Exception("找不到要删除的组织机构日记对象！");
                    }
                    entities.DeleteObject(entity);
                    entities.SaveChanges();
                }
            }
        }

        public static List<OrganizationDiary> GetAll(string orgId, string userName, int pageSize, int pageNo)
        {
            List<OrganizationDiary> list = new List<OrganizationDiary>();
            if (string.IsNullOrEmpty(orgId))
            {
                return list;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                int orgIdInt = Convert.ToInt32(orgId);
                if (string.IsNullOrEmpty(userName))
                {
                    return (from m in entities.Bud_OrgDiaryCost
                        where m.OrgId == orgIdInt
                        select new OrganizationDiary { Id = m.OrgDiaryId, Code = m.OrgdiaryCode, Name = m.Name, Department = m.Department, FlowState = m.FlowState, IssuedBy = m.IssuedBy, InputDate = m.InputDate, InputUser = m.InputUser } into m
                        orderby m.InputDate descending
                        select m).Skip<OrganizationDiary>(((pageNo - 1) * pageSize)).Take<OrganizationDiary>(pageSize).ToList<OrganizationDiary>();
                }
                return (from m in entities.Bud_OrgDiaryCost
                    where (m.OrgId == orgIdInt) && (m.InputUser == userName)
                    select new OrganizationDiary { Id = m.OrgDiaryId, Code = m.OrgdiaryCode, Name = m.Name, Department = m.Department, FlowState = m.FlowState, IssuedBy = m.IssuedBy, InputDate = m.InputDate, InputUser = m.InputUser } into m
                    orderby m.InputDate descending
                    select m).Skip<OrganizationDiary>(((pageNo - 1) * pageSize)).Take<OrganizationDiary>(pageSize).ToList<OrganizationDiary>();
            }
        }

        public static OrganizationDiary GetById(string id)
        {
            OrganizationDiary diary = null;
            if (string.IsNullOrEmpty(id))
            {
                return diary;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_OrgDiaryCost
                    where m.OrgDiaryId == id
                    select new OrganizationDiary { Id = m.OrgDiaryId, Code = m.OrgdiaryCode, Department = m.Department, Name = m.Name, IssuedBy = m.IssuedBy, FlowState = m.FlowState, InputDate = m.InputDate, InputUser = m.InputUser }).FirstOrDefault<OrganizationDiary>();
            }
        }

        public static DataTable GetContrast(string orgDiaryId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append(" --项目Id\r\n DECLARE @OrgId VARCHAR(200)\r\nSELECT @OrgId=OrgId FROM Bud_OrgDiaryCost  WHERE OrgDiaryId=@orgDiaryId\r\n--月份和年份\r\nDECLARE @Year VARCHAR(10),@Month VARCHAR(10)\r\nSELECT @Month=MONTH(InputDate),@Year=YEAR(InputDate) FROM Bud_OrgDiaryCost  WHERE OrgDiaryId=@orgDiaryId\r\n\r\nSELECT ThisAmount.OrgdetailsCode AS Code,ThisAmount.Name,ThisAmount.CBSCode,CBSName,ISNULL(ThisAmount,0) AS ThisAmount,ISNULL(prjAmount,0) AS PrjAmount,\r\nISNULL(monthAmount,0) AS MonthAmount,ISNULL(prjAlreadyAmount,0) AS PrjAlreadyAmount,ISNULL(monthAlreadyAmount,0) AS MonthAlreadyAmount\r\nFROM (--本次金额\r\nSELECT Bud_OrgDiaryDetails.OrgdetailsCode,Bud_OrgDiaryDetails.CBSCode,Bud_OrgDiaryDetails.Name,Bud_CostAccounting.Name AS CBSName,SUM(ISNULL(Amount,0)) AS ThisAmount FROM Bud_OrgDiaryDetails \r\nLEFT JOIN Bud_CostAccounting  ON Bud_OrgDiaryDetails.CBSCode=Bud_CostAccounting.CBSCode\r\nWHERE OrgDiaryId=@orgDiaryId\r\nGROUP BY Bud_OrgDiaryDetails.OrgdetailsCode,Bud_OrgDiaryDetails.CBSCode,Bud_OrgDiaryDetails.Name,Bud_CostAccounting.Name\r\n) AS ThisAmount\r\nLEFT JOIN \r\n(--总预算金额\r\nSELECT CBSCode, AccountingAmount AS prjAmount FROM Bud_OrganizationBudget\r\nWHERE OrganizationBudgetId=@OrgId AND State='2'\r\n) AS PrjAmount ON ThisAmount.CBSCode=PrjAmount.CBSCode\r\nLEFT JOIN \r\n(--月度预算金额\r\nSELECT CBSCode,Amount AS monthAmount FROM Bud_OrganizationBudget \r\nLEFT JOIN Bud_OrganizationMonthBudget ON Bud_OrganizationBudget.Id=Bud_OrganizationMonthBudget.OrganizationBudget \r\nWHERE OrganizationBudgetId=@OrgId AND State='2' AND Year=@Year AND Month=@Month\r\n) AS MonthAmount ON ThisAmount.CBSCode=MonthAmount.CBSCode\r\nLEFT JOIN\r\n(--已发生金额\r\nSELECT CBSCode,SUM(ISNULL(Amount,0)) AS prjAlreadyAmount FROM Bud_OrgDiaryDetails AS Details\r\nLEFT JOIN Bud_OrgDiaryCost AS Diary ON Details.OrgDiaryId=Diary.OrgDiaryId\r\nWHERE OrgId=@OrgId AND FlowState='1'--更改为1已审核\r\nGROUP BY CBSCode\r\n)AS PrjAlreadyAmount ON ThisAmount.CBSCode=PrjAlreadyAmount.CBSCode\r\nLEFT JOIN\r\n(--月度已发生金额\r\nSELECT CBSCode,SUM(ISNULL(Amount,0)) AS monthAlreadyAmount FROM Bud_OrgDiaryDetails AS Details\r\nLEFT JOIN Bud_OrgDiaryCost AS Diary ON Details.OrgDiaryId=Diary.OrgDiaryId\r\nWHERE OrgId=@OrgId AND FlowState='1'--更改为1已审核\r\n    AND YEAR(Diary.InputDate)=@Year AND MONTH(Diary.InputDate)=@Month\r\nGROUP BY CBSCode\r\n)AS MonthAlreadyAmount ON ThisAmount.CBSCode=MonthAlreadyAmount.CBSCode\r\nORDER BY Code,CBSCode,Name desc");
            SqlParameter parameter = new SqlParameter("@orgDiaryId", orgDiaryId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static int GetCount(int orgId, string userName)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return (from m in entities.Bud_OrgDiaryCost
                        where m.OrgId == orgId
                        select m.OrgDiaryId).Count<string>();
                }
                return (from m in entities.Bud_OrgDiaryCost
                    where (m.OrgId == orgId) && (m.InputUser == userName)
                    select m.OrgDiaryId).Count<string>();
            }
        }

        public static int GetCount(int orgId, string person, string startDate, string endDate, bool isInputUser, string userName, string deparment, string name, string flowState, decimal? TotalAmount)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT count(*) FROM dbo.Bud_OrgDiaryCost AS Diary\r\n                INNER JOIN ( ");
            builder.AppendFormat("SELECT Diary.OrgDiaryId,SUM(ISNULL(Amount,0)) Amount FROM dbo.Bud_OrgDiaryCost Diary LEFT JOIN Bud_OrgDiaryDetails AS Details ON Diary.OrgDiaryId=Details.OrgDiaryId WHERE OrgId='{0}'", orgId);
            if (!string.IsNullOrEmpty(person))
            {
                builder.AppendFormat("AND IssuedBy LIKE '%{0}%' ", person);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat("AND InputUser = '{0}' ", userName);
            }
            if (!string.IsNullOrEmpty(deparment))
            {
                builder.AppendFormat(" AND Department LIKE '%{0}%' ", deparment);
            }
            if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat("AND inputDate<='{0}' ", endDate);
            }
            else if (string.IsNullOrEmpty(endDate) && !string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat("AND inputDate>='{0}' ", startDate);
            }
            else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat("AND inputDate>='{0}' AND inputDate<='{1}' ", startDate, endDate);
            }
            if (!string.IsNullOrEmpty(name))
            {
                builder.AppendFormat("AND Diary.Name LIKE '%{0}%'", name);
            }
            if (!string.IsNullOrEmpty(flowState))
            {
                builder.AppendFormat("AND Diary.FlowState ='{0}'", flowState);
            }
            builder.Append(" GROUP BY Diary.InputDate,Diary.OrgDiaryId");
            if (!string.IsNullOrEmpty(TotalAmount.ToString()))
            {
                builder.AppendFormat("HAVING SUM(ISNULL(Amount,0))={0}", TotalAmount);
            }
            builder.Append(" ) DiaryDetails ON Diary.OrgDiaryId=DiaryDetails.OrgDiaryId ");
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()));
        }

        public static DataTable GetDiaries(string prjId, string person, string startDate, string endDate, bool isInputUser, string userName, string deparment, string name, string flowState, decimal? TotalAmount, int pageSize, int pageNo)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n\t\t\t\tSELECT TOP ({0})--PageSize\r\n\t\t\t\tDiary.OrgDiaryId AS InDiaryId,Diary.Name,Department,InputDate,IssuedBy,FlowState,InputUser,SUM(ISNULL(Amount,0)) AS TotalAmount\r\n\t\t\t\t,MonthBudgetAmount=\r\n\t\t\t\t\t(\r\n\t\t\t\t\tSELECT ISNULL(SUM(ISNULL(Amount,0)),0) AS MonthAmount FROM Bud_OrganizationBudget AS IndirBudget\r\n\t\t\t\t\tLEFT JOIN Bud_OrganizationMonthBudget AS IndirMonthBudget ON IndirBudget.Id=IndirMonthBudget.OrganizationBudget\r\n\t\t\t\t\tWHERE State='2' AND OrganizationBudgetId=@OrgId AND CBSCode IN\r\n\t\t\t\t\t(\r\n\t\t\t\t\tSELECT CBSCode  FROM Bud_OrgDiaryDetails WHERE OrgDiaryId=Diary.OrgDiaryId\r\n\t\t\t\t\t)\r\n\t\t\t\t\tAND Year=YEAR(Diary.InputDate) AND Month=MONTH(Diary.InputDate)\r\n\t\t\t\t\t)\r\n\t\t\t\t,MothDiaryAlreadyAmount=\r\n\t\t\t\t\t(\r\n\t\t\t\t\tSELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Bud_OrgDiaryCost AS DiaryCost\r\n\t\t\t\t\tLEFT JOIN Bud_OrgDiaryDetails AS DiaryDetails ON DiaryCost.OrgDiaryId=DiaryDetails.OrgDiaryId\r\n\t\t\t\t\tWHERE DiaryCost.OrgId=@OrgId AND DiaryCost.FlowState='1' --更改为已审核\r\n\t\t\t\t\tAND CBSCode  IN (\r\n\t\t\t\t\tSELECT CBSCode  FROM Bud_OrgDiaryDetails WHERE OrgDiaryId=Diary.OrgDiaryId\r\n\t\t\t\t\t)\r\n\t\t\t\t\tAND YEAR(InputDate)=YEAR(Diary.InputDate) AND MONTH(InputDate)=MONTH(Diary.InputDate)\r\n\t\t\t\t\t)\r\n\t\t\t\tFROM Bud_OrgDiaryCost AS Diary\r\n\t\t\t\tLEFT JOIN Bud_OrgDiaryDetails AS Details ON Diary.OrgDiaryId=Details.OrgDiaryId\r\n\t\t\t\tWHERE OrgId=@OrgId\r\n\t\t\t\tAND Diary.OrgDiaryId NOT IN(SELECT  TOP (({1}-1)*{2}) OrgDiaryId FROM  Bud_OrgDiaryCost WHERE OrgId=@OrgId ORDER BY InputDate,OrgDiaryId DESC) ", pageSize, pageNo, pageSize);
            if (!string.IsNullOrEmpty(person))
            {
                builder.AppendFormat(" AND IssuedBy LIKE '%{0}%' ", person);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat(" AND InputUser = '{0}' ", userName);
            }
            if (!string.IsNullOrEmpty(deparment))
            {
                builder.AppendFormat(" AND Department LIKE '%{0}%' ", deparment);
            }
            if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat("AND inputDate<='{0}' ", endDate);
            }
            else if (string.IsNullOrEmpty(endDate) && !string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat("AND inputDate>='{0}' ", startDate);
            }
            else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat("AND inputDate>='{0}' AND inputDate<='{1}' ", startDate, endDate);
            }
            if (!string.IsNullOrEmpty(name))
            {
                builder.AppendFormat("AND Diary.Name LIKE '%{0}%'", name);
            }
            if (!string.IsNullOrEmpty(flowState))
            {
                builder.AppendFormat("AND Diary.FlowState ='{0}'", flowState);
            }
            builder.Append(" GROUP BY Diary.OrgDiaryId,Diary.Name,Department,InputDate,IssuedBy,FlowState,InputUser ");
            if (!string.IsNullOrEmpty(TotalAmount.ToString()))
            {
                builder.AppendFormat("HAVING SUM(ISNULL(Amount,0))={0}", TotalAmount);
            }
            builder.AppendFormat(" ORDER BY InputDate,Diary.OrgDiaryId DESC", new object[0]);
            SqlParameter parameter = new SqlParameter("@OrgId", prjId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static decimal GetDiariesTotal(string prjId, string person, string startDate, string endDate, bool isInputUser, string userName, string deparment)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT ISNULL(SUM(Amount),0) AS SumTotal FROM Bud_OrgDiaryDetails AS Details\r\nJOIN Bud_OrgDiaryCost AS Diary ON Details.OrgDiaryId = Diary.OrgDiaryId WHERE OrgId='{0}' ", prjId);
            if (!string.IsNullOrEmpty(person))
            {
                if (isInputUser)
                {
                    builder.AppendFormat("AND InputUser LIKE '%{0}%' ", person);
                }
                else
                {
                    builder.AppendFormat("AND IssuedBy LIKE '%{0}%' ", person);
                }
            }
            if (userName != "管理员")
            {
                builder.AppendFormat(" AND IssuedBy LIKE '%{0}%' ", userName);
            }
            if (!string.IsNullOrEmpty(deparment))
            {
                builder.AppendFormat(" AND Department LIKE '%{0}%' ", deparment);
            }
            if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat("AND inputDate<='{0}' ", endDate);
            }
            else if (string.IsNullOrEmpty(endDate) && !string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat("AND inputDate>='{0}' ", startDate);
            }
            else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat("AND inputDate>='{0}' AND inputDate<='{1}' ", startDate, endDate);
            }
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()));
        }

        public static DataTable getDtByWhere(string strwhere)
        {
            string cmdText = "select (select isnull(sum(isnull(PayOutMoney,0.00)),0.00) as PayMoney  from  Fund_Prj_Accoun_Payout where FloeState=1 and RPGuid=TAB.InDiaryId ) PayMoney,* from ( select a.*,(select sum(isnull(detail.Amount,0)) from dbo.Bud_IndirectDiaryDetails as detail where detail.IndiaryId=a.IndiaryId group by detail.IndiaryId) as Total from Bud_IndirectDiaryCost as a ) as TAB";
            if (!string.IsNullOrEmpty(strwhere))
            {
                cmdText = cmdText + " where " + strwhere;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public static int GetOrgainCostCount(string OrgCost)
        {
            int num = 0;
            using (pm2Entities entities = new pm2Entities())
            {
                if (!string.IsNullOrEmpty(OrgCost))
                {
                    num = (from m in entities.Bud_OrgDiaryCost
                        where m.OrgdiaryCode.Contains(OrgCost)
                        select m.OrgDiaryId).Count<string>();
                }
            }
            return num;
        }

        public void Update(OrganizationDiary orgDiary)
        {
            if (orgDiary != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_OrgDiaryCost cost = (from m in entities.Bud_OrgDiaryCost
                        where m.OrgDiaryId == orgDiary.Id
                        select m).FirstOrDefault<Bud_OrgDiaryCost>();
                    if (cost == null)
                    {
                        throw new Exception("找不到要修改的组织机构日记对象！");
                    }
                    cost.Name = orgDiary.Name;
                    cost.Department = orgDiary.Department;
                    cost.OrgdiaryCode = orgDiary.Code;
                    cost.IssuedBy = orgDiary.IssuedBy;
                    cost.InputDate = orgDiary.InputDate;
                    cost.InputUser = orgDiary.InputUser;
                    entities.SaveChanges();
                }
            }
        }

        public static void UpdateFlowState(string id, int state)
        {
            if (!string.IsNullOrEmpty(id))
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_OrgDiaryCost cost = (from m in entities.Bud_OrgDiaryCost
                        where m.OrgDiaryId == id
                        select m).FirstOrDefault<Bud_OrgDiaryCost>();
                    if (cost == null)
                    {
                        throw new Exception("找不到间组织机构日记对象，无法进行修改流程状态！");
                    }
                    cost.FlowState = state;
                    entities.SaveChanges();
                }
            }
        }

        public string Code { get; set; }

        public string Department { get; set; }

        public int FlowState { get; set; }

        public string Id { get; set; }

        public DateTime InputDate { get; set; }

        public string InputUser { get; set; }

        public string IssuedBy { get; set; }

        public string Name { get; set; }

        public string PrjId { get; set; }

        public decimal Total
        {
            get
            {
                return OrganizationDiaryDetails.GetSumAmountByInDiaryId(this.Id);
            }
        }
    }
}

