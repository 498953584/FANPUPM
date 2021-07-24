namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public class EquShipRefuelApplyService : Repository<EquShipRefuelApply>
    {
        public int GetAnalysisCount(string startDate, string endDate, string prjName, string shipCode)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                    --DECLARE @startDate datetime,@endDate datetime,@prjName nvarchar(500),@equCode nvarchar(500);\r\n                    --SET @startDate = '2013-08-01';\r\n                    --SET @endDate = CONVERT(VARCHAR(10),GETDATE(),120);\r\n                    --SET @prjName = '';\r\n                    --SET @equCode = '';\r\n                    WITH cteBudOilWears AS\r\n                    (\r\n\t                    SELECT Id,Equ_Ship_BudOilWear.PrjId,PrjName,TaskName,ConstructionPlace,QuotaOilWear\r\n\t                    FROM Equ_Ship_BudOilWear JOIN PT_PrjInfo ON Equ_Ship_BudOilWear.PrjId = PT_PrjInfo.PrjGuid\r\n\t                    LEFT JOIN Bud_Task ON Equ_Ship_BudOilWear.TaskId = Bud_Task.TaskId\r\n\t                    WHERE FlowState = 1\r\n                    ),cteRefuelApplys AS\r\n                    (\r\n\t                    SELECT Equ_Ship_RefuelApply.Id,BudOilWearId,Equ_Ship_RefuelApply.EquipmentId,\r\n\t                    EquipmentCode,ApplyRefuelDate,ApplyQuantity,Sump,Reason \r\n\t                    FROM Equ_Ship_RefuelApply JOIN Equ_Equipment ON Equ_Ship_RefuelApply.EquipmentId = Equ_Equipment.Id\r\n\t                    WHERE FlowState = 1 ");
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.Append("AND ApplyRefuelDate>=@startDate ");
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.Append("AND ApplyRefuelDate<=@endDate ");
                list.Add(new SqlParameter("@endDate", endDate));
            }
            builder.Append("\r\n                    ),cteOil AS\r\n                    (\r\n\t                    SELECT PrjId,PrjName,TaskName,ConstructionPlace,QuotaOilWear,\r\n\t                    EquipmentId,EquipmentCode,ApplyRefuelDate,ApplyQuantity,Sump,Reason\r\n\t                    FROM cteBudOilWears JOIN cteRefuelApplys ON cteBudOilWears.Id = cteRefuelApplys.BudOilWearId\r\n                    ),cteQty AS\r\n                    (\r\n\t                    SELECT PrjId,Qty,EquipmentId FROM Equ_ShipElseReport\r\n\t                    UNION \r\n\t                    SELECT PrjId,Quantity,EquipmentId FROM Equ_ShipFlatReport\r\n\t                    UNION\r\n\t                    SELECT PrjId,Qty,EquipmentId FROM Equ_ShipGrapReport\r\n\t                    UNION \r\n\t                    SELECT PrjId,MudTotalQty,EquipmentId FROM Equ_ShipMudReport\r\n\t                    WHERE FlowState = 1\r\n                    )\r\n                    SELECT PrjName,EquipmentCode,ApplyRefuelDate\r\n                    FROM cteOil LEFT JOIN cteQty ON cteOil.PrjId = cteQty.PrjId AND cteOil.EquipmentId = cteQty.EquipmentId\r\n                    WHERE EquipmentCode LIKE '%'+@equCode+'%' ");
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.Append("AND PrjName LIKE '%'+@prjName+'%'");
                list.Add(new SqlParameter("@prjName", prjName));
            }
            list.Add(new SqlParameter("@equCode", shipCode));
            return (base.ExecuteQuery(builder.ToString(), list.ToArray()).Rows.Count + 1);
        }

        public DataTable GetAnalysisTable(string startDate, string endDate, string prjName, string shipCode, int pageSize, int pageNo)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetAnalysisCount(startDate, endDate, prjName, shipCode);
            }
            if (pageNo == 0)
            {
                pageNo = 1;
            }
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                    --DECLARE @startDate datetime,@endDate datetime,@prjName nvarchar(500),@equCode nvarchar(500),@pageSize int,@pageNo int;\r\n                    --SET @startDate = '2013-08-01';\r\n                    --SET @endDate = CONVERT(VARCHAR(10),GETDATE(),120);\r\n                    --SET @prjName = '';\r\n                    --SET @equCode = '';\r\n                    --SET @pageSize = 15;\r\n                    --SET @pageNo = 1;\r\n                    WITH cteBudOilWears AS\r\n                    (\r\n\t                    SELECT Id,Equ_Ship_BudOilWear.PrjId,PrjName,TaskName,ConstructionPlace,QuotaOilWear\r\n\t                    FROM Equ_Ship_BudOilWear JOIN PT_PrjInfo ON Equ_Ship_BudOilWear.PrjId = PT_PrjInfo.PrjGuid\r\n\t                    LEFT JOIN Bud_Task ON Equ_Ship_BudOilWear.TaskId = Bud_Task.TaskId\r\n\t                    WHERE FlowState = 1\r\n                    ),cteRefuelApplys AS\r\n                    (\r\n\t                    SELECT Equ_Ship_RefuelApply.Id,BudOilWearId,Equ_Ship_RefuelApply.EquipmentId,\r\n\t                    EquipmentCode,ApplyRefuelDate,ApplyQuantity,Sump,Reason \r\n\t                    FROM Equ_Ship_RefuelApply JOIN Equ_Equipment ON Equ_Ship_RefuelApply.EquipmentId = Equ_Equipment.Id\r\n\t                    WHERE FlowState = 1 ");
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.Append("AND ApplyRefuelDate>=@startDate ");
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.Append("AND ApplyRefuelDate<=@endDate ");
                list.Add(new SqlParameter("@endDate", endDate));
            }
            builder.Append("\r\n                    ),cteOil AS\r\n                    (\r\n\t                    SELECT PrjId,PrjName,TaskName,ConstructionPlace,QuotaOilWear,\r\n\t                    EquipmentId,EquipmentCode,ApplyRefuelDate,ApplyQuantity,Sump,Reason\r\n\t                    FROM cteBudOilWears JOIN cteRefuelApplys ON cteBudOilWears.Id = cteRefuelApplys.BudOilWearId\r\n                    ),cteQty AS\r\n                    (\r\n\t                    SELECT PrjId,Qty,EquipmentId FROM Equ_ShipElseReport\r\n\t                    UNION \r\n\t                    SELECT PrjId,Quantity,EquipmentId FROM Equ_ShipFlatReport\r\n\t                    UNION\r\n\t                    SELECT PrjId,Qty,EquipmentId FROM Equ_ShipGrapReport\r\n\t                    UNION \r\n\t                    SELECT PrjId,MudTotalQty,EquipmentId FROM Equ_ShipMudReport\r\n\t                    WHERE FlowState = 1\r\n                    )\r\n                    SELECT TOP(@pageSize) Num,PrjName,TaskName,ConstructionPlace,QuotaOilWear,ApplyRefuelDate,Qty,Sump,\r\n                    BudOilWear,EquipmentCode,ApplyQuantity,Diff,Reason\r\n                    FROM\r\n                    (\r\n\t                    SELECT ROW_NUMBER() OVER(ORDER BY PrjName,ApplyRefuelDate DESC) AS Num,cteOil.PrjId,PrjName,ISNULL(TaskName,'') AS TaskName,\r\n\t                    ConstructionPlace,QuotaOilWear,EquipmentCode,CONVERT(VARCHAR(10),ApplyRefuelDate,120) AS ApplyRefuelDate,\r\n\t                    ApplyQuantity,Sump,Reason,ISNULL(Qty,0) AS Qty,ISNULL((QuotaOilWear*Qty),0.00) AS BudOilWear,\r\n\t                    ISNULL((ApplyQuantity-QuotaOilWear*Qty),0.00) AS Diff\r\n\t                    FROM cteOil left JOIN cteQty ON cteOil.PrjId = cteQty.PrjId AND cteOil.EquipmentId = cteQty.EquipmentId\r\n\t                    WHERE EquipmentCode LIKE '%'+@equCode+'%' ");
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.Append("AND PrjName LIKE '%'+@prjName+'%'");
                list.Add(new SqlParameter("@prjName", prjName));
            }
            builder.Append("\r\n                    ) tab WHERE Num > (@pageNo - 1 ) * @pageSize ");
            list.Add(new SqlParameter("@equCode", shipCode));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageNo", pageNo));
            return base.ExecuteQuery(builder.ToString(), list.ToArray());
        }

        public int GetApplyCount(string startDate, string endDate, string shipCode)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string str = string.Empty;
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate>=@startDate AND ApplyRefuelDate<=@endDate";
                list.Add(new SqlParameter("@startDate", startDate));
                list.Add(new SqlParameter("@endDate", endDate));
            }
            if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate>=@startDate";
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate <=@endDate";
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n                    --DECLARE @startDate datetime,@endDate datetime,@equCode nvarchar(500);\r\n                    --SET @startDate = '2013-08-01';\r\n                    --SET @endDate = '2013-08-31';\r\n                    --SET @equCode = '001';\r\n                    WITH cteOperator AS\r\n                    (\r\n\t                    SELECT InstanceCode,Operator,v_xm AS OperatorName\r\n\t                    FROM WF_Instance JOIN WF_Instance_Main ON WF_Instance.ID = WF_Instance_Main.ID\r\n\t                    JOIN PT_yhmc ON WF_Instance.Operator = PT_yhmc.v_yhdm\r\n                    ),cteApply AS\r\n                    (\r\n\t                    SELECT Equ_Ship_RefuelApply.Id,EquipmentId,EquipmentCode,TotalRefuel,StockQuantity,ApplyQuantity,\r\n\t                    BudCompleteQuantity,ISNULL(CompletedQuantity/NULLIF(TotalConstructionDates,0),0) AS Ratio,\r\n\t                    Sump,ApplyRefuelPlace,ApplyRefuelDate,\r\n\t                    CASE WHEN IsEntrustPurchase=1 THEN '是' ELSE '否' END AS IsEntrustPurchase,ApplyMaster,\r\n\t                    ApplyQuantity AS RatifyQuantity\r\n\t                    FROM Equ_Ship_RefuelApply JOIN Equ_Equipment ON Equ_Ship_RefuelApply.EquipmentId = Equ_Equipment.Id\r\n\t                    WHERE FlowState = 1 ";
            cmdText = cmdText + str + ")\r\n                    SELECT EquipmentCode,TotalRefuel,StockQuantity,\r\n                    ApplyQuantity,BudCompleteQuantity,Ratio,Sump,ApplyRefuelPlace,ApplyRefuelDate,IsEntrustPurchase,\r\n                    ApplyMaster,RatifyQuantity,OperatorName\r\n                    FROM cteApply JOIN cteOperator ON cteApply.Id = cteOperator.InstanceCode\r\n                    WHERE EquipmentCode LIKE '%'+@equCode+'%'";
            list.Add(new SqlParameter("@equCode", shipCode));
            return (base.ExecuteQuery(cmdText, list.ToArray()).Rows.Count + 1);
        }

        public DataTable GetApplyTable(string startDate, string endDate, string shipCode, int pageSize, int pageNo)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetApplyCount(startDate, endDate, shipCode);
            }
            if (pageNo == 0)
            {
                pageNo = 1;
            }
            List<SqlParameter> list = new List<SqlParameter>();
            string str = string.Empty;
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate>=@startDate AND ApplyRefuelDate<=@endDate";
                list.Add(new SqlParameter("@startDate", startDate));
                list.Add(new SqlParameter("@endDate", endDate));
            }
            if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate>=@startDate";
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate <=@endDate";
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n                    --DECLARE @startDate datetime,@endDate datetime,@equCode nvarchar(500),@pageSize int,@pageNo int;\r\n                    --SET @startDate = '2013-08-01';\r\n                    --SET @endDate = '2013-08-31';\r\n                    --SET @equCode = '001';\r\n                    --SET @pageSize = 15;\r\n                    --SET @pageNo = 1;\r\n                    WITH cteOperator AS\r\n                    (\r\n\t                    SELECT InstanceCode,Operator,v_xm AS OperatorName\r\n\t                    FROM WF_Instance JOIN WF_Instance_Main ON WF_Instance.ID = WF_Instance_Main.ID\r\n\t                    JOIN PT_yhmc ON WF_Instance.Operator = PT_yhmc.v_yhdm\r\n                    ),cteApply AS\r\n                    (\r\n\t                    SELECT Equ_Ship_RefuelApply.Id,EquipmentId,EquipmentCode,TotalRefuel,StockQuantity,ApplyQuantity,\r\n\t                    BudCompleteQuantity,ISNULL(CompletedQuantity/NULLIF(TotalConstructionDates,0),0) AS Ratio,\r\n\t                    Sump,ApplyRefuelPlace,CONVERT(VARCHAR(10),ApplyRefuelDate,120) AS ApplyRefuelDate,\r\n\t                    CASE WHEN IsEntrustPurchase=1 THEN '是' ELSE '否' END AS IsEntrustPurchase,ApplyMaster,\r\n\t                    ApplyQuantity AS RatifyQuantity\r\n\t                    FROM Equ_Ship_RefuelApply JOIN Equ_Equipment ON Equ_Ship_RefuelApply.EquipmentId = Equ_Equipment.Id\r\n\t                    WHERE FlowState = 1 ";
            cmdText = cmdText + str + "\r\n                    )\r\n                    SELECT TOP(@pageSize) Num,EquipmentCode,TotalRefuel,StockQuantity,\r\n                    ApplyQuantity,BudCompleteQuantity,Ratio,Sump,ApplyRefuelPlace,ApplyRefuelDate,IsEntrustPurchase,\r\n                    ApplyMaster,RatifyQuantity,OperatorName\r\n                    FROM\r\n                    (\r\n\t                    SELECT ROW_NUMBER() OVER(ORDER BY ApplyRefuelDate DESC) AS Num,EquipmentCode,TotalRefuel,StockQuantity,\r\n\t                    ApplyQuantity,BudCompleteQuantity,Ratio,Sump,ApplyRefuelPlace,ApplyRefuelDate,IsEntrustPurchase,\r\n\t                    ApplyMaster,RatifyQuantity,OperatorName\r\n\t                    FROM cteApply JOIN cteOperator ON cteApply.Id = cteOperator.InstanceCode\r\n                        WHERE EquipmentCode LIKE '%'+@equCode+'%'\r\n                    )tab WHERE Num > (@pageNo - 1 ) * @pageSize";
            list.Add(new SqlParameter("@equCode", shipCode));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageNo", pageNo));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public EquShipRefuelApply GetById(string id)
        {
            return (from r in this
                where r.ApplyId.Equals(id)
                select r).FirstOrDefault<EquShipRefuelApply>();
        }

        public DataTable GetRefuelRecord(string startDate, string endDate, string shipCode, int pageSize, int pageNo)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetRefuelrecordCount(startDate, endDate, shipCode);
            }
            if (pageNo == 0)
            {
                pageNo = 1;
            }
            List<SqlParameter> list = new List<SqlParameter>();
            string str = string.Empty;
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate>=@startDate AND ApplyRefuelDate<=@endDate";
                list.Add(new SqlParameter("@startDate", startDate));
                list.Add(new SqlParameter("@endDate", endDate));
            }
            if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate>=@startDate";
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate <=@endDate";
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n                    --DECLARE @startDate datetime,@endDate datetime,@equCode nvarchar(500),@pageSize int,@pageNo int;\r\n                    --SET @startDate = '2013-08-01';\r\n                    --SET @endDate = '2013-08-31';\r\n                    --SET @equCode = '0';\r\n                    --SET @pageSize = 15;\r\n                    --SET @pageNo = 1;\r\n                    WITH cteApply AS\r\n                    (\r\n\t                    SELECT Equ_Ship_RefuelApply.Id,BudOilWearId,EquipmentId,EquipmentCode,CONVERT(VARCHAR(10),ApplyRefuelDate,120) AS ApplyRefuelDate,\r\n                        StockQuantity,ApplyQuantity,(StockQuantity+ApplyQuantity) AS AfterQuantity,OilsType,Fueler,\r\n\t                    ApplyRefuelPlace,FuelerOwner,LocaleLeader,v_xm AS LeaderName,Equ_Ship_RefuelApply.Note\r\n\t                    FROM Equ_Ship_RefuelApply JOIN Equ_Equipment ON Equ_Ship_RefuelApply.EquipmentId = Equ_Equipment.Id\r\n\t                    LEFT JOIN PT_yhmc ON Equ_Ship_RefuelApply.LocaleLeader = PT_yhmc.v_yhdm\r\n\t                    WHERE FlowState = 1 ";
            cmdText = cmdText + str + "\r\n                    ),ctePrj AS\r\n                    (\r\n\t                    SELECT Id, PrjName,TaskName\r\n\t                    FROM Equ_Ship_BudOilWear JOIN PT_PrjInfo ON Equ_Ship_BudOilWear.PrjId = PT_PrjInfo.PrjGuid\r\n\t                    LEFT JOIN Bud_Task ON Equ_Ship_BudOilWear.TaskId = Bud_Task.TaskId\r\n\t                    WHERE FlowState = 1\r\n                    ),cteLastDate AS\r\n                    (\r\n\t                    SELECT r1.Id,CONVERT(VARCHAR(10),MAX(r2.ApplyRefuelDate),120) as LastDate\r\n\t                    FROM Equ_Ship_RefuelApply r1 INNER JOIN Equ_Ship_RefuelApply r2\r\n\t                    ON r1.BudOilWearId = r2.BudOilWearId AND r1.EquipmentId = r2.EquipmentId\r\n\t                    WHERE r1.ApplyRefuelDate>r2.ApplyRefuelDate\r\n\t                    GROUP BY r1.Id\r\n                    )\r\n                    SELECT TOP(@pageSize) Num,EquipmentCode,ApplyRefuelDate,\r\n                    LastDate,StockQuantity,PrjName,TaskName,ApplyQuantity,AfterQuantity,OilsType,Fueler,ApplyRefuelPlace,\r\n                    FuelerOwner,LeaderName,Note\r\n                    FROM\r\n                    (\r\n\t                    SELECT ROW_NUMBER() OVER(ORDER BY ApplyRefuelDate DESC) AS Num,EquipmentCode,ApplyRefuelDate,\r\n\t                    LastDate,StockQuantity,PrjName,TaskName,ApplyQuantity,AfterQuantity,OilsType,Fueler,ApplyRefuelPlace,\r\n\t                    FuelerOwner,LeaderName,Note\r\n\t                    FROM cteApply JOIN ctePrj ON cteApply.BudOilWearId = ctePrj.Id\r\n\t                    LEFT JOIN cteLastDate ON cteApply.Id = cteLastDate.Id\r\n\t                    WHERE EquipmentCode LIKE '%'+@equCode+'%'\r\n                    )tab WHERE Num > (@pageNo - 1 ) * @pageSize";
            list.Add(new SqlParameter("@equCode", shipCode));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageNo", pageNo));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public int GetRefuelrecordCount(string startDate, string endDate, string shipCode)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            string str = string.Empty;
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate>=@startDate AND ApplyRefuelDate<=@endDate";
                list.Add(new SqlParameter("@startDate", startDate));
                list.Add(new SqlParameter("@endDate", endDate));
            }
            if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate>=@startDate";
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                str = "AND ApplyRefuelDate <=@endDate";
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n                    --DECLARE @startDate datetime,@endDate datetime,@equCode nvarchar(500);\r\n                    --SET @startDate = '2013-08-01';\r\n                    --SET @endDate = '2013-08-31';\r\n                    --SET @equCode = '0';\r\n                    WITH cteApply AS\r\n                    (\r\n\t                    SELECT Equ_Ship_RefuelApply.Id,BudOilWearId,EquipmentId,EquipmentCode,ApplyRefuelDate,StockQuantity,\r\n\t                    ApplyQuantity,(StockQuantity+ApplyQuantity) AS AfterQuantity,OilsType,Fueler,\r\n\t                    ApplyRefuelPlace,FuelerOwner,LocaleLeader,v_xm AS LeaderName,Equ_Ship_RefuelApply.Note\r\n\t                    FROM Equ_Ship_RefuelApply JOIN Equ_Equipment ON Equ_Ship_RefuelApply.EquipmentId = Equ_Equipment.Id\r\n\t                    LEFT JOIN PT_yhmc ON Equ_Ship_RefuelApply.LocaleLeader = PT_yhmc.v_yhdm\r\n\t                    WHERE FlowState = 1 ";
            cmdText = cmdText + str + "\r\n                    ),ctePrj AS\r\n                    (\r\n\t                    SELECT Id, PrjName,TaskName\r\n\t                    FROM Equ_Ship_BudOilWear JOIN PT_PrjInfo ON Equ_Ship_BudOilWear.PrjId = PT_PrjInfo.PrjGuid\r\n\t                    LEFT JOIN Bud_Task ON Equ_Ship_BudOilWear.TaskId = Bud_Task.TaskId\r\n\t                    WHERE FlowState = 1\r\n                    ),cteLastDate AS\r\n                    (\r\n\t                    SELECT r1.Id,MAX(r2.ApplyRefuelDate) as LastDate\r\n\t                    FROM Equ_Ship_RefuelApply r1 INNER JOIN Equ_Ship_RefuelApply r2\r\n\t                    ON r1.BudOilWearId = r2.BudOilWearId AND r1.EquipmentId = r2.EquipmentId\r\n\t                    WHERE r1.ApplyRefuelDate>r2.ApplyRefuelDate\r\n\t                    GROUP BY r1.Id\r\n                    )\r\n                    SELECT ROW_NUMBER() OVER(ORDER BY ApplyRefuelDate DESC) AS Num,EquipmentCode,ApplyRefuelDate,\r\n                    LastDate,StockQuantity,PrjName,TaskName,ApplyQuantity,AfterQuantity,OilsType,Fueler,ApplyRefuelPlace,\r\n                    FuelerOwner,LeaderName,Note\r\n                    FROM cteApply JOIN ctePrj ON cteApply.BudOilWearId = ctePrj.Id\r\n                    LEFT JOIN cteLastDate ON cteApply.Id = cteLastDate.Id\r\n                    WHERE EquipmentCode LIKE '%'+@equCode+'%'";
            list.Add(new SqlParameter("@equCode", shipCode));
            return (base.ExecuteQuery(cmdText, list.ToArray()).Rows.Count + 1);
        }
    }
}

