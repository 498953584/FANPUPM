namespace cn.justwin.stockBLL.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class CostApply
    {
        private CostApply()
        {
        }

        public static DataTable GetContrast(string ApplyId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("  --项目Id\r\n                                        DECLARE @PrjId VARCHAR(200)\r\n                                        SELECT @PrjId=PrjId FROM Bud_PreReimburseApply  WHERE Id=@ApplyId\r\n                                --月份和年份\r\n                                        DECLARE @Year VARCHAR(10),@Month VARCHAR(10)\r\n                                        SELECT @Month=MONTH(ApplyDate),@Year=YEAR(ApplyDate) FROM Bud_PreReimburseApply  WHERE Id=@ApplyId\r\n\r\n                                 SELECT ThisAmount.Name,ThisAmount.CBSCode,CBSName,ISNULL(ThisAmount,0) AS ThisAmount,ISNULL(prjAmount,0) AS PrjAmount,\r\n                                        ISNULL(monthAmount,0) AS MonthAmount,ISNULL(prjAlreadyAmount,0) AS PrjAlreadyAmount\r\n                                        FROM (\r\n\r\n                                --本次金额\r\n\r\n                                SELECT Bud_PreReimburseApplyDetail.CBSCode,Bud_PreReimburseApplyDetail.Name,\r\n                                Bud_CostAccounting.Name AS CBSName,SUM(ISNULL(Cost,0)) AS ThisAmount FROM Bud_PreReimburseApplyDetail \r\n                                LEFT JOIN Bud_CostAccounting  ON Bud_PreReimburseApplyDetail.CBSCode=Bud_CostAccounting.CBSCode\r\n                                WHERE ApplyId=@ApplyId\r\n                                GROUP BY Bud_PreReimburseApplyDetail.CBSCode,\r\n                                Bud_PreReimburseApplyDetail.Name,Bud_CostAccounting.Name\r\n\r\n                                )AS ThisAmount\r\n                                LEFT JOIN \r\n                                (\r\n\r\n                                -----总预算金额\r\n                                SELECT CBSCode, AccountAmount AS prjAmount FROM Bud_IndirectBudget \r\n                                WHERE ProjectId=@PrjId AND State='2'\r\n                                )AS PrjAmount ON ThisAmount.CBSCode=PrjAmount.CBSCode\r\n\r\n                                LEFT JOIN\r\n                                 (\r\n                                --月度预算金额\r\n                                SELECT CBSCode,Amount AS monthAmount FROM Bud_IndirectBudget \r\n                                LEFT JOIN Bud_IndirectMonthBudget ON Bud_IndirectBudget.Id=Bud_IndirectMonthBudget.IndirectBudget \r\n                                WHERE ProjectId=@PrjId AND State='2' AND Year=@Year AND Month=@Month\r\n                                )AS MonthAmount ON ThisAmount.CBSCode=MonthAmount.CBSCode\r\n\r\n                                 LEFT JOIN\r\n                                (\r\n                                --已发生金额\r\n                                SELECT CBSCode,SUM(ISNULL(Cost,0)) AS prjAlreadyAmount FROM Bud_PreReimburseApplyDetail AS Details\r\n                                LEFT JOIN Bud_PreReimburseApply AS Diary ON Details.ApplyId=Diary.Id\r\n                                WHERE PrjId=@PrjId AND FlowState='1'--更改为1已审核\r\n                                GROUP BY CBSCode\r\n                                )AS PrjAlreadyAmount ON ThisAmount.CBSCode=PrjAlreadyAmount.CBSCode");
            SqlParameter parameter = new SqlParameter("@ApplyId", ApplyId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public virtual DateTime ApplyDate { get; set; }

        public virtual string Code { get; set; }

        public virtual string CostType { get; set; }

        public virtual int FlowState { get; set; }

        public virtual string Id { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string Name { get; set; }

        public virtual string PrjId { get; set; }

        public virtual string RptUser { get; set; }
    }
}

