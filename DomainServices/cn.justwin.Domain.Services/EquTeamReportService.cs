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

    public class EquTeamReportService : Repository<EquTeamReport>
    {
        public EquTeamReport GetById(string id)
        {
            return (from r in this
                where r.TeamID.Equals(id)
                select r).FirstOrDefault<EquTeamReport>();
        }

        public DataTable GetReports(string type, string prjName, string equipmentCode, string equipmentName, string startDate, string endDate, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetReportsCount(type, prjName, equipmentCode, equipmentName, startDate, endDate);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
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
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat(" AND PrjName LIKE '%'+@prjName+'%' ", new object[0]);
                list.Add(new SqlParameter("@prjName", prjName));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @type char(1), @prjName nvarchar(50),@equipmentName nvarchar(50),@equipmentCode nvarchar(50),\r\n\t\t\t--@startDate nvarchar(50),@endDate nvarchar(50),@pageSize int,@pageIndex int\r\n\t\t\t--SET @type='D'\r\n\t\t\t--SET @prjName=''\r\n\t\t\t--SET @equipmentCode=''\r\n\t\t\t--SET @equipmentName=''\r\n\t\t\t--SET @startDate=''\r\n\t\t\t--SET @endDate=''\r\n\t\t\t--SET @pageSize=50\r\n\t\t\t--SET @pageIndex=1\r\n\t\t\tSELECT TOP(@pageSize) Convert(NVARCHAR(2),Num) Num,ReportDate,ConstructionDate,\r\n\t\t\tPrjName,ConstructionPlace,EquipmentCode,ResourceName,StartDate,EndDate,\r\n\t\t\tConstructionTime,Quantity,Note FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY ReportDate) Num,ReportDate,ConstructionDate,\r\n\t\t\t\tPrjName,ConstructionPlace,EquipmentCode,ResourceName,report.StartDate,report.EndDate,\r\n\t\t\t\tConvert(DECIMAL(18,3),(MachineTeamCount*MachineTeamDuration)) ConstructionTime,Quantity,report.Note\r\n\t\t\t\tFROM Equ_ShipTeamReport report\r\n\t\t\t\tLEFT JOIN Pt_PrjInfo prjInfo ON report.PrjId=Convert(nvarchar(50),prjInfo.PrjGuid)\r\n\t\t\t\tINNER JOIN Equ_Equipment equipment ON report.EquipmentId=equipment.Id\r\n\t\t\t\tINNER JOIN Res_Resource res ON equipment.ResourceId=res.ResourceId\r\n\t\t\t\tWHERE Type=@type AND FlowState=1 AND EquipmentCode LIKE '%'+@equipmentCode+'%' \r\n\t\t\t\tAND ResourceName LIKE '%'+@equipmentName+'%'";
            cmdText = cmdText + builder.ToString() + " ) reportInfo WHERE Num>@pageSize*(@pageIndex-1) ";
            list.Add(new SqlParameter("@type", type));
            list.Add(new SqlParameter("@equipmentCode", equipmentCode));
            list.Add(new SqlParameter("@equipmentName", equipmentName));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public int GetReportsCount(string type, string prjName, string equipmentCode, string equipmentName, string startDate, string endDate)
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
            if (!string.IsNullOrEmpty(prjName))
            {
                builder.AppendFormat(" AND PrjName LIKE '%'+@prjName+'%' ", new object[0]);
                list.Add(new SqlParameter("@prjName", prjName));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @type char(1), @prjName nvarchar(50),@equipmentCode nvarchar(50),@equipmentName nvarchar(50),\r\n\t\t\t--@startDate nvarchar(50),@endDate nvarchar(50)\r\n\t\t\t--SET @type='D'\r\n\t\t\t--SET @prjName=''\r\n\t\t\t--SET @equipmentCode=''\r\n\t\t\t--SET @equipmentName=''\r\n\t\t\t--SET @startDate=''\r\n\t\t\t--SET @endDate=''\r\n\t\t\tSELECT COUNT(*) FROM \r\n\t\t\t(\r\n\t\t\t\tSELECT ReportDate,ConstructionDate,PrjName,ConstructionPlace,\r\n\t\t\t\tEquipmentCode,ResourceName,report.StartDate,report.EndDate,\r\n\t\t\t\t(MachineTeamCount*MachineTeamDuration) ConstructionTime,Quantity \r\n\t\t\t\tFROM Equ_ShipTeamReport report\r\n\t\t\t\tLEFT JOIN Pt_PrjInfo prjInfo ON report.PrjId=Convert(nvarchar(50),prjInfo.PrjGuid)\r\n\t\t\t\tINNER JOIN Equ_Equipment equipment ON report.EquipmentId=equipment.Id\r\n\t\t\t\tINNER JOIN Res_Resource res ON equipment.ResourceId=res.ResourceId\r\n\t\t\t\tWHERE Type=@type AND FlowState=1 AND EquipmentCode LIKE '%'+@equipmentCode+'%' \r\n\t\t\t\tAND ResourceName LIKE '%'+@equipmentName+'%'";
            cmdText = cmdText + builder.ToString() + ") reportInfo";
            list.Add(new SqlParameter("@type", type));
            list.Add(new SqlParameter("@equipmentCode", equipmentCode));
            list.Add(new SqlParameter("@equipmentName", equipmentName));
            DataTable table = base.ExecuteQuery(cmdText, list.ToArray());
            if (table != null)
            {
                num = Convert.ToInt32(table.Rows[0][0]);
            }
            return num;
        }
    }
}

