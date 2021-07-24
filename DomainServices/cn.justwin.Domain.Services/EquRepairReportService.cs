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

    public class EquRepairReportService : Repository<EquRepairReport>
    {
        public EquRepairReport GetById(string id)
        {
            return (from r in this
                where r.ReportId.Equals(id)
                select r).FirstOrDefault<EquRepairReport>();
        }

        public DataTable GetRepairCost(string equType, string startDate, string endDate, string equCode, string equName, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetRepairPlanCompleteCount(equType, startDate, endDate, equCode, equName);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            DataTable table = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND ReportDate>=@startDate ", new object[0]);
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND ReportDate<=@endDate ", new object[0]);
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n\t\t\t        --DECLARE @startDate nvarchar(50),@endDate nvarchar(50),@equType char(1),\r\n                    --@equCode nvarchar(50),@equName nvarchar(50),@pageSize int,@pageIndex int;\r\n                    --SET @startDate='2013-08-01';\r\n                    --SET @endDate='2013-10-01';\r\n                    --SET @equType='0';\r\n                    --SET @equCode='';\r\n                    --SET @equName='';\r\n                    --SET @pageSize=500;\r\n                    --SET @pageIndex=1;\r\n                    WITH cteTotalPrice AS\r\n                    (\r\n\t                    SELECT ReportId,SUM(Quantity*UnitPrice) AS TotalPrice FROM Equ_RepairStock GROUP BY ReportId\r\n                    )\r\n                    SELECT TOP(@pageSize) Num,ReportId,ReportDate,TotalPrice,EquipmentCode,ResourceName,Specification,\r\n                    RepairContent,RepairStartDate,RepairEndDate,RepairCost,RepairPerson,RepairType,OutSubContractor FROM \r\n                    (\r\n\t                    SELECT ROW_NUMBER() OVER(ORDER BY ReportDate DESC) Num,repairReport.Id ReportId,\r\n\t                    CONVERT(VARCHAR(10),ReportDate,120) AS ReportDate,TotalPrice,EquipmentCode,ResourceName,\r\n                        Specification,repairReport.RepairContent,repairReport.RepairStartDate,repairReport.RepairEndDate,\r\n\t                    (case when repairType='0' then '自行维修' when repairType='1' then '委外维修' end) RepairType,\r\n\t                    RepairPerson,\r\n\t                    (case when repairReport.contractId is null then RepairCost \r\n\t\t                      when repairReport.contractId is not null then ModifiedMoney end) RepairCost,\r\n\t                    OutSubContractor\r\n\t                    FROM Equ_RepairReport repairReport\r\n\t                    JOIN cteTotalPrice ON repairReport.Id = cteTotalPrice.ReportId\r\n\t                    JOIN Equ_RepairApply repairApply ON repairReport.ApplyId=repairApply.Id\r\n\t                    INNER JOIN Equ_Equipment equipment ON repairApply.EquipmentId=equipment.Id\r\n\t                    INNER JOIN Res_Resource res ON equipment.ResourceId=res.ResourceId\r\n\t                    LEFT JOIN Con_Payout_Contract payoutCon ON repairReport.ContractId=payoutCon.ContractId\r\n\t                    LEFT JOIN PT_yhmc yhmc ON repairReport.InputUser=yhmc.V_Yhdm\r\n\t                    WHERE repairReport.FlowState=1 AND repairReport.EquipmentType=@equType\r\n\t                    AND EquipmentCode LIKE '%'+@equCode+'%' AND ResourceName LIKE '%'+@equName+'%'";
            cmdText = cmdText + builder.ToString() + " ) RepairInfo WHERE Num>(@pageSize)*(@pageIndex-1)";
            list.Add(new SqlParameter("@equType", equType));
            list.Add(new SqlParameter("@equCode", equCode));
            list.Add(new SqlParameter("@equName", equName));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public DataTable GetRepairPlanComplete(string equType, string startDate, string endDate, string equCode, string equName, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetRepairPlanCompleteCount(equType, startDate, endDate, equCode, equName);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            DataTable table = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND ReportDate>=@startDate ", new object[0]);
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND ReportDate<=@endDate ", new object[0]);
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @startDate nvarchar(50),@endDate nvarchar(50),@equType char(1),\r\n\t\t\t--@equCode nvarchar(50),@equName nvarchar(50),@pageSize int,@pageIndex int;\r\n\t\t\t--SET @startDate='2013-08-13'\r\n\t\t\t--SET @endDate='2013-08-22'\r\n\t\t\t--SET @equType='0'\r\n\t\t\t--SET @equCode='测试'\r\n\t\t\t--SET @equName=''\r\n\t\t\t--SET @pageSize=500\r\n\t\t\t--SET @pageIndex=1\r\n\t\t\tSELECT TOP(@pageSize) Num,EquipmentCode,ResourceName,Specification,\r\n\t\t\tPlanRepairContent,PlanRepairStartDate,PlanRepairEndDate,RepairContent,RepairStartDate,\r\n\t\t\tRepairEndDate,Reason,V_XM FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY ReportDate DESC) Num, EquipmentCode,ResourceName,Specification,\r\n\t\t\t\tISNULL(repairPlan.RepairContent,'') PlanRepairContent,\r\n\t\t\t\trepairPlan.RepairStartDate PlanRepairStartDate,repairPlan.RepairEndDate PlanRepairEndDate,\r\n\t\t\t\trepairReport.RepairContent,repairReport.RepairStartDate,repairReport.RepairEndDate,\r\n\t\t\t\tReason,yhmc.V_XM,ReportDate\r\n\t\t\t\tFROM Equ_RepairReport repairReport\r\n\t\t\t\tJOIN Equ_RepairApply repairApply ON repairReport.ApplyId=repairApply.Id\r\n\t\t\t\tLEFT JOIN Equ_RepairPlan repairPlan ON RepairPlanId=repairPlan.Id\r\n\t\t\t\tINNER JOIN Equ_Equipment equipment ON repairApply.EquipmentId=equipment.Id\r\n\t\t\t\tINNER JOIN Res_Resource res ON equipment.ResourceId=res.ResourceId\r\n\t\t\t\tLEFT JOIN PT_yhmc yhmc ON repairReport.InputUser=yhmc.V_Yhdm\r\n\t\t\t\tWHERE repairReport.FlowState=1 AND repairReport.EquipmentType=@equType\r\n\t\t\t\tAND EquipmentCode LIKE '%'+@equCode+'%' AND ResourceName LIKE '%'+@equName+'%' ";
            cmdText = cmdText + builder.ToString() + " ) RepairInfo WHERE Num>(@pageSize)*(@pageIndex-1)";
            list.Add(new SqlParameter("@equType", equType));
            list.Add(new SqlParameter("@equCode", equCode));
            list.Add(new SqlParameter("@equName", equName));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public int GetRepairPlanCompleteCount(string equType, string startDate, string endDate, string equCode, string equName)
        {
            int num = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND ReportDate>=@startDate ", new object[0]);
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND ReportDate<=@endDate ", new object[0]);
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @startDate nvarchar(50),@endDate nvarchar(50),@equType char(1),\r\n\t\t\t--@equCode nvarchar(50),@equName nvarchar(50)\r\n\t\t\t--SET @startDate='2013-08-14'\r\n\t\t\t--SET @endDate='2013-08-21'\r\n\t\t\t--SET @equType='0'\r\n\t\t\t--SET @equCode=''\r\n\t\t\t--SET @equName=''\r\n\t\t\tSELECT COUNT(*) FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT EquipmentCode,ResourceName,Specification,ISNULL(repairPlan.RepairContent,'') PlanRepairContent,\r\n\t\t\t\trepairPlan.RepairStartDate PlanRepairStartDate,repairPlan.RepairEndDate PlanRepairEndDate,\r\n\t\t\t\trepairReport.RepairContent,repairReport.RepairStartDate,repairReport.RepairEndDate,\r\n\t\t\t\tReason,yhmc.V_XM,ReportDate\r\n\t\t\t\tFROM Equ_RepairReport repairReport\r\n\t\t\t\tJOIN Equ_RepairApply repairApply ON repairReport.ApplyId=repairApply.Id\r\n\t\t\t\tLEFT JOIN Equ_RepairPlan repairPlan ON  RepairPlanId=repairPlan.Id\r\n\t\t\t\tINNER JOIN Equ_Equipment equipment ON repairApply.EquipmentId=equipment.Id\r\n\t\t\t\tINNER JOIN Res_Resource res ON equipment.ResourceId=res.ResourceId\r\n\t\t\t\tLEFT JOIN PT_yhmc yhmc ON repairReport.InputUser=yhmc.V_Yhdm\r\n\t\t\t\tWHERE repairReport.FlowState=1 AND repairReport.EquipmentType=@equType\r\n\t\t\t\tAND EquipmentCode LIKE '%'+@equCode+'%' AND ResourceName LIKE '%'+@equName+'%'";
            cmdText = cmdText + builder.ToString() + ") RepairInfo ";
            list.Add(new SqlParameter("@equType", equType));
            list.Add(new SqlParameter("@equCode", equCode));
            list.Add(new SqlParameter("@equName", equName));
            DataTable table = base.ExecuteQuery(cmdText, list.ToArray());
            if (table != null)
            {
                num = Convert.ToInt32(table.Rows[0][0]);
            }
            return num;
        }

        public List<string> GetReportIdByApplyId(string id)
        {
            return (from r in this
                where r.ApplyId == id
                select r.ReportId).ToList<string>();
        }

        public DataTable GetRoadOutRepairCost(string startDate, string endDate, string equCode, string equName, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetRoadOutRepairCostCount(startDate, endDate, equCode, equName);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            DataTable table = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND ReportDate>=@startDate ", new object[0]);
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND ReportDate<=@endDate ", new object[0]);
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @startDate nvarchar(50),@endDate nvarchar(50),\r\n\t\t\t--@equCode nvarchar(50),@equName nvarchar(50),@pageSize int,@pageIndex int;\r\n\t\t\t--SET @startDate='2013-08-13'\r\n\t\t\t--SET @endDate='2013-08-22'\r\n\t\t\t--SET @equCode=''\r\n\t\t\t--SET @equName=''\r\n\t\t\t--SET @pageSize=500\r\n\t\t\t--SET @pageIndex=1\r\n\t\t\tSELECT TOP(@pageSize) Num,ReportId,ReportDate,EquipmentCode,ResourceName,Specification,DepName,\r\n\t\t\tFaultDescription,OutCompany,OutDepartment,RepairStartDate,RepairEndDate,LaborCost,StuffCost,\r\n\t\t\tRepairCost,AcceptorName,Note FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY ReportDate DESC) Num,repairReport.Id ReportId,\r\n\t\t\t\tReportDate,EquipmentCode,ResourceName,Specification,V_BMMC DepName,FaultDescription,\r\n\t\t\t\tOutCompany,OutDepartment,repairReport.RepairStartDate,repairReport.RepairEndDate,\r\n\t\t\t\tLaborCost,StuffCost,\r\n\t\t\t\t(CASE WHEN repairReport.contractId IS NULL THEN RepairCost \r\n\t\t\t\t\t  WHEN repairReport.contractId IS NOT NULL THEN ModifiedMoney END) RepairCost,\r\n\t\t\t\tv_xm AcceptorName,repairReport.Note\r\n\t\t\t\tFROM Equ_RepairReport repairReport\r\n\t\t\t\tJOIN Equ_RepairApply repairApply ON repairReport.ApplyId=repairApply.Id\r\n\t\t\t\tINNER JOIN Equ_Equipment equipment ON repairApply.EquipmentId=equipment.Id\r\n\t\t\t\tINNER JOIN Res_Resource res ON equipment.ResourceId=res.ResourceId\r\n\t\t\t\tLEFT JOIN Con_Payout_Contract payoutCon ON repairReport.ContractId=payoutCon.ContractId\r\n\t\t\t\tLEFT JOIN PT_yhmc yhmc ON repairReport.Acceptor=yhmc.V_Yhdm\r\n\t\t\t\tLEFT JOIN Pt_d_bm bm ON ApplyDepId=bm.i_bmdm\r\n\t\t\t\tWHERE repairReport.FlowState=1 AND repairReport.EquipmentType=1 AND repairApply.RepairType=1\r\n\t\t\t\tAND EquipmentCode LIKE '%'+@equCode+'%' AND ResourceName LIKE '%'+@equName+'%'  ";
            cmdText = cmdText + builder.ToString() + " ) RepairInfo WHERE Num>(@pageSize)*(@pageIndex-1)";
            list.Add(new SqlParameter("@equCode", equCode));
            list.Add(new SqlParameter("@equName", equName));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public int GetRoadOutRepairCostCount(string startDate, string endDate, string equCode, string equName)
        {
            int num = 0;
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND ReportDate>=@startDate ", new object[0]);
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND ReportDate<=@endDate ", new object[0]);
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @startDate nvarchar(50),@endDate nvarchar(50),\r\n\t\t\t--@equCode nvarchar(50),@equName nvarchar(50)\r\n\t\t\t--SET @startDate='2013-08-14'\r\n\t\t\t--SET @endDate='2013-08-21'\r\n\t\t\t--SET @equCode=''\r\n\t\t\t--SET @equName=''\r\n\t\t\tSELECT COUNT(*) FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT EquipmentCode,ResourceName,Specification,ISNULL(repairPlan.RepairContent,'') PlanRepairContent,\r\n\t\t\t\trepairPlan.RepairStartDate PlanRepairStartDate,repairPlan.RepairEndDate PlanRepairEndDate,\r\n\t\t\t\trepairReport.RepairContent,repairReport.RepairStartDate,repairReport.RepairEndDate,\r\n\t\t\t\tReason,yhmc.V_XM,ReportDate\r\n\t\t\t\tFROM Equ_RepairReport repairReport\r\n\t\t\t\tJOIN Equ_RepairApply repairApply ON repairReport.ApplyId=repairApply.Id\r\n\t\t\t\tLEFT JOIN Equ_RepairPlan repairPlan ON  RepairPlanId=repairPlan.Id\r\n\t\t\t\tINNER JOIN Equ_Equipment equipment ON repairApply.EquipmentId=equipment.Id\r\n\t\t\t\tINNER JOIN Res_Resource res ON equipment.ResourceId=res.ResourceId\r\n\t\t\t\tLEFT JOIN PT_yhmc yhmc ON repairReport.InputUser=yhmc.V_Yhdm\r\n\t\t\t\tWHERE repairReport.FlowState=1 AND repairApply.RepairType=1 AND repairReport.EquipmentType=1\r\n\t\t\t\tAND EquipmentCode LIKE '%'+@equCode+'%' AND ResourceName LIKE '%'+@equName+'%'";
            cmdText = cmdText + builder.ToString() + ") RepairInfo ";
            list.Add(new SqlParameter("@equCode", equCode));
            list.Add(new SqlParameter("@equName", equName));
            DataTable table = base.ExecuteQuery(cmdText, list.ToArray());
            if (table != null)
            {
                num = Convert.ToInt32(table.Rows[0][0]);
            }
            return num;
        }

        public DataTable GetRoadRepairPlanComplete(string startDate, string endDate, string equCode, string equName, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetRepairPlanCompleteCount("1", startDate, endDate, equCode, equName);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            DataTable table = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND ReportDate>=@startDate ", new object[0]);
                list.Add(new SqlParameter("@startDate", startDate));
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND ReportDate<=@endDate ", new object[0]);
                list.Add(new SqlParameter("@endDate", endDate));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @startDate nvarchar(50),@endDate nvarchar(50),@equType char(1),\r\n\t\t\t--@equCode nvarchar(50),@equName nvarchar(50),@pageSize int,@pageIndex int;\r\n\t\t\t--SET @startDate='2013-08-13'\r\n\t\t\t--SET @endDate='2013-08-22'\r\n\t\t\t--SET @equCode='测试'\r\n\t\t\t--SET @equName=''\r\n\t\t\t--SET @pageSize=500\r\n\t\t\t--SET @pageIndex=1\r\n\t\t\tSELECT TOP(@pageSize) Num,EquipmentCode,ResourceName,Specification,\r\n\t\t\tPlanRepairContent,PlanRepairStartDate,PlanRepairEndDate,RepairContent,RepairStartDate,\r\n\t\t\tRepairEndDate,RepairType,V_XM,Reason,Note FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY ReportDate DESC) Num, EquipmentCode,ResourceName,Specification,\r\n\t\t\t\tISNULL(repairPlan.RepairContent,'') PlanRepairContent,\r\n\t\t\t\trepairPlan.RepairStartDate PlanRepairStartDate,repairPlan.RepairEndDate PlanRepairEndDate,\r\n\t\t\t\trepairReport.RepairContent,repairReport.RepairStartDate,repairReport.RepairEndDate,\r\n\t\t\t\t(case when repairApply.repairType='0' then '自行维修' \r\n\t\t\t\t\t  when repairApply.repairType='1' then '委外维修' end) RepairType,\r\n\t\t\t\tReason,yhmc.V_XM,ReportDate,repairReport.Note\r\n\t\t\t\tFROM Equ_RepairReport repairReport\r\n\t\t\t\tJOIN Equ_RepairApply repairApply ON repairReport.ApplyId=repairApply.Id\r\n\t\t\t\tLEFT JOIN Equ_RepairPlan repairPlan ON RepairPlanId=repairPlan.Id\r\n\t\t\t\tINNER JOIN Equ_Equipment equipment ON repairApply.EquipmentId=equipment.Id\r\n\t\t\t\tINNER JOIN Res_Resource res ON equipment.ResourceId=res.ResourceId\r\n\t\t\t\tLEFT JOIN PT_yhmc yhmc ON repairReport.Acceptor=yhmc.V_Yhdm\r\n\t\t\t\tWHERE repairReport.FlowState=1 AND repairReport.EquipmentType=1\r\n\t\t\t\tAND EquipmentCode LIKE '%'+@equCode+'%' AND ResourceName LIKE '%'+@equName+'%' ";
            cmdText = cmdText + builder.ToString() + " ) RepairInfo WHERE Num>(@pageSize)*(@pageIndex-1)";
            list.Add(new SqlParameter("@equCode", equCode));
            list.Add(new SqlParameter("@equName", equName));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }
    }
}

