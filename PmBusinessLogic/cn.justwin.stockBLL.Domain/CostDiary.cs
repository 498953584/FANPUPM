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

    public class CostDiary
    {
        private CostDiary()
        {
        }

        public void Add(CostDiary costDiary)
        {
            if (costDiary != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_IndirectDiaryCost cost = new Bud_IndirectDiaryCost {
                        InDiaryId = costDiary.Id,
                        Name = costDiary.Name,
                        IndireCode = costDiary.Code,
                        ProjectId = costDiary.PrjId,
                        Department = costDiary.Department,
                        IssuedBy = costDiary.IssuedBy,
                        InputUser = costDiary.InputUser,
                        InputDate = costDiary.InputDate,
                        FlowState = costDiary.FlowState
                    };
                    entities.AddToBud_IndirectDiaryCost(cost);
                    entities.SaveChanges();
                }
            }
        }

        public static CostDiary Create(string id, string prjId, string IndireCode, string name, string department, string issuedBy, string inputUser, DateTime inputDate)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("间接成本日记", "Id不能为空!!!");
            }
            if (string.IsNullOrEmpty(prjId))
            {
                throw new ArgumentNullException("间接成本日记", "PrjId不能为空!!!");
            }
            if (string.IsNullOrEmpty(IndireCode))
            {
                throw new ArgumentNullException("间接成本日记", "IndireCode不能为空!!!");
            }
            return new CostDiary { Id = id, Name = name, PrjId = prjId, Code = IndireCode, Department = department, InputDate = inputDate, InputUser = inputUser, IssuedBy = issuedBy, FlowState = -1 };
        }

        public static void Del(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_IndirectDiaryCost entity = (from m in entities.Bud_IndirectDiaryCost
                        where m.InDiaryId == id
                        select m).FirstOrDefault<Bud_IndirectDiaryCost>();
                    if (entity == null)
                    {
                        throw new Exception("找不到要删除的间接成本日记对象!!!");
                    }
                    entities.DeleteObject(entity);
                    entities.SaveChanges();
                }
            }
        }

        public static List<CostDiary> GetAll(string prjId, string userName, int pageSize, int pageNo)
        {
            List<CostDiary> list = new List<CostDiary>();
            if (string.IsNullOrEmpty(prjId))
            {
                return list;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return (from m in entities.Bud_IndirectDiaryCost
                        where m.ProjectId == prjId
                        select new CostDiary { Id = m.InDiaryId, Code = m.IndireCode, Name = m.Name, Department = m.Department, FlowState = m.FlowState, IssuedBy = m.IssuedBy, InputDate = m.InputDate, InputUser = m.InputUser, PrjId = m.ProjectId } into m
                        orderby m.InputDate descending
                        select m).Skip<CostDiary>(((pageNo - 1) * pageSize)).Take<CostDiary>(pageSize).ToList<CostDiary>();
                }
                return (from m in entities.Bud_IndirectDiaryCost
                    where (m.ProjectId == prjId) && (m.InputUser == userName)
                    select new CostDiary { Id = m.InDiaryId, Code = m.IndireCode, Name = m.Name, Department = m.Department, FlowState = m.FlowState, IssuedBy = m.IssuedBy, InputDate = m.InputDate, InputUser = m.InputUser, PrjId = m.ProjectId } into m
                    orderby m.InputDate descending
                    select m).Skip<CostDiary>(((pageNo - 1) * pageSize)).Take<CostDiary>(pageSize).ToList<CostDiary>();
            }
        }

        public static CostDiary GetById(string id)
        {
            CostDiary diary = null;
            if (string.IsNullOrEmpty(id))
            {
                return diary;
            }
            using (pm2Entities entities = new pm2Entities())
            {
                return (from m in entities.Bud_IndirectDiaryCost
                    where m.InDiaryId == id
                    select new CostDiary { Id = m.InDiaryId, Department = m.Department, Code = m.IndireCode, Name = m.Name, PrjId = m.ProjectId, IssuedBy = m.IssuedBy, FlowState = m.FlowState, InputDate = m.InputDate, InputUser = m.InputUser }).FirstOrDefault<CostDiary>();
            }
        }

        public static DataTable GetContrast(string InDiaryId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append(" --项目Id\r\n                                        DECLARE @PrjId VARCHAR(200)\r\n                                        SELECT @PrjId=ProjectId FROM Bud_IndirectDiaryCost  WHERE InDiaryId=@InDiaryId\r\n                                        --月份和年份\r\n                                        DECLARE @Year VARCHAR(10),@Month VARCHAR(10)\r\n                                        SELECT @Month=MONTH(InputDate),@Year=YEAR(InputDate) FROM Bud_IndirectDiaryCost  WHERE InDiaryId=@InDiaryId\r\n\r\n                                        SELECT ThisAmount.IndetailsCode AS Code,ThisAmount.Name,ThisAmount.CBSCode,ThisAmount.CBSName,ISNULL(ThisAmount,0) AS ThisAmount, ISNULL(ThisApplyAmount, 0) AS ThisApplyAmount, ISNULL(prjAmount,0) AS PrjAmount,\r\n                                        ISNULL(monthAmount,0) AS MonthAmount,ISNULL(prjAlreadyAmount,0) AS PrjAlreadyAmount,ISNULL(monthAlreadyAmount,0) AS MonthAlreadyAmount\r\n                                        FROM (--本次金额\r\n                                          SELECT Bud_IndirectDiaryDetails.IndetailsCode,Bud_IndirectDiaryDetails.CBSCode,Bud_IndirectDiaryDetails.Name,Bud_CostAccounting.Name AS CBSName,SUM(ISNULL(Amount,0)) AS ThisAmount\r\n                                          FROM Bud_IndirectDiaryDetails \r\n                                              LEFT JOIN Bud_CostAccounting  ON Bud_IndirectDiaryDetails.CBSCode=Bud_CostAccounting.CBSCode\r\n                                          WHERE InDiaryId=@InDiaryId\r\n                                          GROUP BY Bud_IndirectDiaryDetails.IndetailsCode,Bud_IndirectDiaryDetails.CBSCode,Bud_IndirectDiaryDetails.Name,Bud_CostAccounting.Name\r\n                                         ) AS ThisAmount\r\n                                        LEFT JOIN\r\n                                        (--预算申请金额\r\n                                          SELECT Bud_IndirectDiaryDetails.IndetailsCode,Bud_IndirectDiaryDetails.CBSCode,Bud_IndirectDiaryDetails.Name,Bud_CostAccounting.Name AS CBSName,SUM(ISNULL(Amount,0)) AS ThisApplyAmount\r\n                                          FROM Bud_IndirectDiaryDetails \r\n                                              LEFT JOIN Bud_CostAccounting  ON Bud_IndirectDiaryDetails.CBSCode=Bud_CostAccounting.CBSCode\r\n                                          WHERE InDiaryId=@InDiaryId AND ApplyDetailId <> ''\r\n                                          GROUP BY Bud_IndirectDiaryDetails.IndetailsCode,Bud_IndirectDiaryDetails.CBSCode,Bud_IndirectDiaryDetails.Name,Bud_CostAccounting.Name\r\n                                         ) AS ThisApplyAmount ON ThisAmount.CBSCode=ThisApplyAmount.CBSCode\r\n                                        LEFT JOIN \r\n                                        (--总预算金额\r\n                                          SELECT CBSCode, AccountAmount AS prjAmount FROM Bud_IndirectBudget \r\n                                          WHERE ProjectId=@PrjId AND State='2'\r\n                                         ) AS PrjAmount ON ThisAmount.CBSCode=PrjAmount.CBSCode\r\n                                        LEFT JOIN \r\n                                        (--月度预算金额\r\n                                        SELECT CBSCode,Amount AS monthAmount FROM Bud_IndirectBudget \r\n                                        LEFT JOIN Bud_IndirectMonthBudget ON Bud_IndirectBudget.Id=Bud_IndirectMonthBudget.IndirectBudget \r\n                                        WHERE ProjectId=@PrjId AND State='2' AND Year=@Year AND Month=@Month\r\n                                        ) AS MonthAmount ON ThisAmount.CBSCode=MonthAmount.CBSCode\r\n                                        LEFT JOIN\r\n                                        (--已发生金额\r\n                                        SELECT CBSCode,SUM(ISNULL(Amount,0)) AS prjAlreadyAmount FROM Bud_IndirectDiaryDetails AS Details\r\n                                        LEFT JOIN Bud_IndirectDiaryCost AS Diary ON Details.InDiaryId=Diary.InDiaryId\r\n                                        WHERE ProjectId=@PrjId AND FlowState='1'--更改为1已审核\r\n                                        GROUP BY CBSCode\r\n                                        )AS PrjAlreadyAmount ON ThisAmount.CBSCode=PrjAlreadyAmount.CBSCode\r\n                                        LEFT JOIN\r\n                                        (--月度已发生金额\r\n                                        SELECT CBSCode,SUM(ISNULL(Amount,0)) AS monthAlreadyAmount FROM Bud_IndirectDiaryDetails AS Details\r\n                                        LEFT JOIN Bud_IndirectDiaryCost AS Diary ON Details.InDiaryId=Diary.InDiaryId\r\n                                        WHERE ProjectId=@PrjId AND FlowState='1'--更改为1已审核\r\n                                            AND YEAR(InputDate)=@Year AND MONTH(InputDate)=@Month\r\n                                        GROUP BY CBSCode\r\n                                        )AS MonthAlreadyAmount ON ThisAmount.CBSCode=MonthAlreadyAmount.CBSCode\r\n                                        ORDER BY Code,CBSCode,Name ASC ");
            SqlParameter parameter = new SqlParameter("@InDiaryId", InDiaryId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static int GetCostcount(string indiaryCode)
        {
            int num = 0;
            using (pm2Entities entities = new pm2Entities())
            {
                if (!string.IsNullOrEmpty(indiaryCode))
                {
                    num = (from m in entities.Bud_IndirectDiaryCost
                        where m.IndireCode.Contains(indiaryCode)
                        select m.InDiaryId).Count<string>();
                }
            }
            return num;
        }

        public static int GetCount(string prjId, string userName)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return (from m in entities.Bud_IndirectDiaryCost
                        where m.ProjectId == prjId
                        select m.InDiaryId).Count<string>();
                }
                return (from m in entities.Bud_IndirectDiaryCost
                    where (m.ProjectId == prjId) && (m.InputUser == userName)
                    select m.InDiaryId).Count<string>();
            }
        }

        public static int GetCount(string prjId, string person, string startDate, string endDate, string userName, string deparment, string name, string flowState, decimal? TotalAmount, decimal? EndCash, string costType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                SELECT count(*) FROM dbo.Bud_IndirectDiaryCost AS Diary\r\n                INNER JOIN ( ");
            builder.AppendFormat("SELECT Diary.InDiaryId,SUM(ISNULL(Amount,0)) Amount FROM dbo.Bud_IndirectDiaryCost AS Diary LEFT JOIN Bud_IndirectDiaryDetails AS Details ON Diary.InDiaryId=Details.InDiaryId WHERE ProjectId='{0}'", prjId);
            builder.AppendFormat(" AND CostType = '{0}' ", costType);
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
                builder.AppendFormat("AND Diary.FlowState LIKE '%{0}%'", flowState);
            }
            builder.Append(" GROUP BY Diary.InDiaryId ");
            builder.Append(" ) DiaryDetails ON Diary.InDiaryId=DiaryDetails.InDiaryId ");
            if (!string.IsNullOrEmpty(TotalAmount.ToString()))
            {
                builder.AppendFormat("AND Amount >='{0}'", TotalAmount);
                if (!string.IsNullOrEmpty(EndCash.ToString()))
                {
                    builder.AppendFormat("AND Amount <='{0}'", EndCash);
                }
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()));
        }

        public static DataTable GetDiaries(string prjId, string person, string startDate, string endDate, string userName, string deparment, string name, string flowState, decimal? TotalAmount, decimal? EndCash, string costType, int pageSize, int pageNo)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n\t\t\t\tSELECT TOP({0})\r\n\t\t\t\t\tDiary.InDiaryId,Diary.Name,Department,InputDate,IssuedBy,FlowState,InputUser,SUM(ISNULL(Amount,0)) AS TotalAmount\r\n                    ,MonthBudgetAmount=\r\n\t                    (\r\n\t                    SELECT ISNULL(SUM(ISNULL(Amount,0)),0) AS MonthAmount FROM Bud_IndirectBudget AS IndirBudget\r\n\t                    LEFT JOIN Bud_IndirectMonthBudget AS IndirMonthBudget ON IndirBudget.Id=IndirMonthBudget.IndirectBudget\r\n\t                    WHERE State='2' AND ProjectId=@PrjId AND CBSCode IN\r\n\t                    (\r\n\t                    SELECT CBSCode  FROM Bud_IndirectDiaryDetails WHERE InDiaryId=Diary.InDiaryId\r\n\t                    )\r\n\t                    AND Year=YEAR(Diary.InputDate) AND Month=MONTH(Diary.InputDate)\r\n\t                    )\r\n                    ,MothDiaryAlreadyAmount=\r\n\t                    (\r\n\t                    SELECT ISNULL(SUM(ISNULL(Amount,0)),0) FROM Bud_IndirectDiaryCost AS DiaryCost\r\n\t                    LEFT JOIN Bud_IndirectDiaryDetails AS DiaryDetails ON DiaryCost.InDiaryId=DiaryDetails.InDiaryId\r\n\t                    WHERE DiaryCost.ProjectId=@PrjId AND DiaryCost.FlowState='1' --更改为已审核\r\n\t                    AND CBSCode  IN (\r\n\t                    SELECT CBSCode  FROM Bud_IndirectDiaryDetails WHERE InDiaryId=Diary.InDiaryId\r\n\t                    )\r\n\t                    AND YEAR(InputDate)=YEAR(Diary.InputDate) AND MONTH(InputDate)=MONTH(Diary.InputDate)\r\n\t                    )\r\n                FROM Bud_IndirectDiaryCost AS Diary\r\n                LEFT JOIN Bud_IndirectDiaryDetails AS Details ON Diary.InDiaryId=Details.InDiaryId\r\n                WHERE ProjectId=@PrjId \r\n\t\t\t\t\tAND Diary.InDiaryId NOT IN(SELECT TOP(({1}-1)*{2}) InDiaryId FROM Bud_IndirectDiaryCost WHERE ProjectId=@PrjId  ORDER BY InputDate,InDiaryId ASC)", pageSize, pageNo, pageSize);
            builder.AppendFormat(" AND CostType = '{0}' ", costType);
            if (!string.IsNullOrEmpty(person))
            {
                builder.AppendFormat(" AND IssuedBy LIKE '%{0}%' ", person);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat(" AND InputUser = '{0}'", userName);
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
                builder.AppendFormat("AND Diary.FlowState  LIKE '%{0}'", flowState);
            }
            if (!string.IsNullOrEmpty(TotalAmount.ToString()))
            {
                builder.AppendFormat("AND Amount >='{0}'", TotalAmount);
                if (!string.IsNullOrEmpty(EndCash.ToString()))
                {
                    builder.AppendFormat("AND Amount <='{0}'", EndCash);
                }
            }
            builder.Append(" GROUP BY Diary.InDiaryId,Diary.Name,Department,InputDate,IssuedBy,FlowState,InputUser ");
            builder.AppendFormat(" ORDER BY InputDate,Diary.InDiaryId ASC", new object[0]);
            SqlParameter parameter = new SqlParameter("@PrjId", prjId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static decimal GetDiariesTotal(string prjId, string person, string userName, string name, string flowState, string startDate, string endDate, string deparment, string costType, decimal? TotalAmount, decimal? EndCash)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n                SELECT ISNULL(SUM(Amount),0) AS SumTotal FROM Bud_IndirectDiaryDetails AS Details\r\n                JOIN Bud_IndirectDiaryCost AS Diary ON Details.InDiaryId = Diary.InDiaryId \r\n                WHERE ProjectId='{0}' AND CostType='{1}'\r\n            ", prjId, costType);
            if (!string.IsNullOrEmpty(person))
            {
                builder.AppendFormat("AND IssuedBy LIKE '%{0}%' ", person);
            }
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat("AND InputUser ='{0}' ", userName);
            }
            if (!string.IsNullOrEmpty(name))
            {
                builder.AppendFormat("AND Diary.Name LIKE '%{0}%'", name);
            }
            if (!string.IsNullOrEmpty(flowState))
            {
                builder.AppendFormat("AND Diary.FlowState ='{0}'", flowState);
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
            if (!string.IsNullOrEmpty(TotalAmount.ToString()))
            {
                builder.AppendFormat("AND Amount >='{0}'", TotalAmount);
                if (!string.IsNullOrEmpty(EndCash.ToString()))
                {
                    builder.AppendFormat("AND Amount <='{0}'", EndCash);
                }
            }
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString()));
        }

        public void Update(CostDiary costDiary)
        {
            if (costDiary != null)
            {
                using (pm2Entities entities = new pm2Entities())
                {
                    Bud_IndirectDiaryCost cost = (from m in entities.Bud_IndirectDiaryCost
                        where m.InDiaryId == costDiary.Id
                        select m).FirstOrDefault<Bud_IndirectDiaryCost>();
                    if (cost == null)
                    {
                        throw new Exception("找不到要修改的间接成本日记对象!!!");
                    }
                    cost.Name = costDiary.Name;
                    cost.IndireCode = costDiary.Code;
                    cost.Department = costDiary.Department;
                    cost.IssuedBy = costDiary.IssuedBy;
                    cost.InputDate = costDiary.InputDate;
                    cost.InputUser = costDiary.InputUser;
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
                    Bud_IndirectDiaryCost cost = (from m in entities.Bud_IndirectDiaryCost
                        where m.InDiaryId == id
                        select m).FirstOrDefault<Bud_IndirectDiaryCost>();
                    if (cost == null)
                    {
                        throw new Exception("找不到间接成本对象，无法进行修改流程状态!!!");
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
                return CostDiaryDetails.GetSumAmountByInDiaryId(this.Id);
            }
        }
    }
}

