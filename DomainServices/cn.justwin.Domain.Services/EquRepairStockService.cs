namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class EquRepairStockService : Repository<EquRepairStock>
    {
        public void DelStockByReportId(string reportId)
        {
            string sql = string.Format(" DELETE Equ_RepairStock WHERE ReportId='{0}' ", reportId);
            base.ExcuteSql(sql);
        }

        public EquRepairStock GetById(string id)
        {
            return (from r in this
                where r.StockId.Equals(id)
                select r).FirstOrDefault<EquRepairStock>();
        }

        public DataTable GetRepairStock(string reportId, string resCode, string resName, int pageSize, int pageIndex)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageSize == 0)
            {
                pageSize = this.GetRepairStocksCount(reportId, resCode, resName);
            }
            string cmdText = "\r\n\t\t\t\t--DECLARE @reportId NVARCHAR(50),@resourceCode nvarchar(50),@resourceName nvarchar(50),\r\n\t\t\t\t--@pageSize int,@pageIndex int\r\n\t\t\t\t--set @reportId='d4d02406-4d47-4862-be38-0bdf0f965a87'\r\n\t\t\t\t--set @resourceCode=''\r\n\t\t\t\t--set @resourceName=''\r\n\t\t\t\t--set @pageSize=50\r\n\t\t\t\t--set @pageIndex=1\r\n\t\t\t\tSELECT TOP(@pageSize) Num,ID,ReportId,ResourceId,ReceiveDate,ResourceCode,ResourceName,Specification,\r\n\t\t\t\tBrand,ModelNumber,TechnicalParameter,Name,Quantity,UnitPrice,Total,ReceivePerson,ReceivePersonName,\r\n\t\t\t\tCorpId,CorpName FROM\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT ROW_NUMBER() OVER(ORDER BY ResourceCode)\tNum,stock.ID,ReportId,stock.ResourceId,\r\n\t\t\t\t\tres.ResourceCode,ResourceName,Specification,res.Brand,ModelNumber,TechnicalParameter,Res_Unit.Name,\r\n\t\t\t\t\tQuantity,UnitPrice,stock.Quantity*stock.UnitPrice Total,ReceivePerson,V_XM ReceivePersonName,\r\n\t\t\t\t\tReceiveDate,stock.CorpId,CorpName FROM Equ_RepairStock stock\r\n\t\t\t\t\tJOIN Res_Resource res on stock.ResourceId = res.ResourceId  \r\n\t\t\t\t\tLEFT JOIN Res_Unit on Res_Unit.UnitID = res.Unit \r\n\t\t\t\t\tLEFT JOIN XPM_Basic_ContactCorp on stock.CorpId = XPM_Basic_ContactCorp.CorpID\r\n\t\t\t\t\tLEFT JOIN PT_yhmc yhmc ON stock.ReceivePerson=yhmc.V_Yhdm\r\n\t\t\t\t\tWHERE ReportId=@reportId\r\n\t\t\t\t\tAND ResourceCode LIKE '%'+@resourceCode+'%' AND ResourceName LIKE '%'+@resourceName+'%' \r\n\t\t\t\t) stocksInfo WHERE Num>@pageSize*(@pageIndex-1) ";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@reportId", reportId), new SqlParameter("@resourceCode", resCode), new SqlParameter("@resourceName", resName), new SqlParameter("@pageSize", pageSize), new SqlParameter("@pageIndex", pageIndex) };
            return base.ExecuteQuery(cmdText, parameters);
        }

        public int GetRepairStocksCount(string reportId, string resCode, string resName)
        {
            int num = 0;
            string cmdText = "\r\n\t\t\t\t--DECLARE @reportId NVARCHAR(50),@resourceCode nvarchar(50),@resourceName nvarchar(50)\r\n\t\t\t\t--set @reportId='d4d02406-4d47-4862-be38-0bdf0f965a87'\r\n\t\t\t\t--set @resourceCode=''\r\n\t\t\t\t--set @resourceName=''\r\n\t\t\t\tSELECT COUNT(*) FROM \r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT stock.ID,ReportId,stock.ResourceId,res.ResourceCode,ResourceName,Specification,res.Brand,\r\n\t\t\t\t\tModelNumber,TechnicalParameter,Res_Unit.Name,Quantity,UnitPrice,stock.Quantity*stock.UnitPrice Total,\r\n\t\t\t\t\tReceivePerson,V_XM ReceivePersonName,ReceiveDate,stock.CorpId,CorpName FROM Equ_RepairStock stock\r\n\t\t\t\t\tJOIN Res_Resource res on stock.ResourceId = res.ResourceId  \r\n\t\t\t\t\tLEFT JOIN Res_Unit on Res_Unit.UnitID = res.Unit \r\n\t\t\t\t\tLEFT JOIN XPM_Basic_ContactCorp on stock.CorpId = XPM_Basic_ContactCorp.CorpID\r\n\t\t\t\t\tLEFT JOIN PT_yhmc yhmc ON stock.ReceivePerson=yhmc.V_Yhdm\r\n\t\t\t\t\tWHERE ReportId=@reportId \r\n\t\t\t\t\tAND ResourceCode LIKE '%'+@resourceCode+'%' AND ResourceName LIKE '%'+@resourceName+'%' \r\n\t\t\t\t) stocksInfo ";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@reportId", reportId), new SqlParameter("@resourceCode", resCode), new SqlParameter("@resourceName", resName) };
            DataTable table = base.ExecuteQuery(cmdText, parameters);
            if (table != null)
            {
                num = Convert.ToInt32(table.Rows[0][0]);
            }
            return num;
        }
    }
}

