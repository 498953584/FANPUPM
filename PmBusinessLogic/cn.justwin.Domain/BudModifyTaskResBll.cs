namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using cn.justwin.Web;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BudModifyTaskResBll
    {
        private static string priceTypeId = ConfigHelper.Get("BudgetPriceType");

        public static DataTable GetResByModifyTaskId(string modifyTaskId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n                select ResourceCode,ResourceName,Res_Unit.[Name] as Unit,\r\n                Res_Resource.Specification,Res_Resource.Brand,ModelNumber,TechnicalParameter, \r\n                ResourcePrice as price,ResourceQuantity as number,(ResourcePrice*ResourceQuantity) total \r\n                from Bud_ModifyTaskRes INNER JOIN Res_Resource ON Bud_ModifyTaskRes.ResourceId=Res_Resource.ResourceId \r\n                LEFT JOIN Res_Unit ON Unit=UnitId \r\n                where ModifyTaskId=@ModifyTaskId ORDER BY ResourceCode\r\n            ");
            SqlParameter parameter = new SqlParameter("@ModifyTaskId", SqlDbType.NVarChar, 0x40) {
                Value = modifyTaskId
            };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static DataTable GetResourcesByModifyId(string modifyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("\r\n                select ModifyTaskId as Id,Bud_ModifyTaskRes.ResourceId,ResourceCode,\r\n                ResourceName,Res_Unit.[Name] as Unit,Res_Resource.Specification,ResourcePrice as price, \r\n                Res_Resource.Brand,ModelNumber,TechnicalParameter, ResourceQuantity as number,\r\n                 from Bud_ModifyTaskRes \r\n                INNER JOIN Res_Resource ON Bud_TaskResource.ResourceId=Res_Resource.ResourceId\r\n                LEFT JOIN Res_Unit ON Unit=UnitId \r\n                WHERE modifyId=@ModifyId\r\n                ORDER BY ResourceCode\r\n            ");
            SqlParameter parameter = new SqlParameter("@PriceType", modifyId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static DataTable showResForAdd(string IdList, string modifyTaskId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("\r\n                SELECT ModifyTaskResId as Id,Res_Resource.ResourceId,ResourceCode,ResourceName,\r\n                [Name] AS Unit,Specification, ISNULL(PriceValue,0.000) AS price, \r\n                Res_Resource.Brand,ModelNumber,TechnicalParameter,0.000 AS number,ISNULL(LossCoefficient,1.00) LossCoefficient FROM Res_Resource \r\n                LEFT JOIN Bud_ModifyTaskRes ON Res_Resource.ResourceId=Bud_ModifyTaskRes.ResourceId\r\n                and ModifyTaskId=@ModifyTaskId LEFT JOIN Res_Unit ON UnitId = Unit \r\n                LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId \r\n                AND PriceTypeId = @PriceType \r\n                LEFT JOIN Res_PriceType ON Res_PriceType.PriceTypeId = Res_Price.PriceTypeId \r\n                WHERE Res_Resource.ResourceId IN(\r\n            ");
            builder.Append(IdList);
            builder.Append(")  ORDER BY ResourceCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PriceType", SqlDbType.NVarChar, 0x40), new SqlParameter("@ModifyTaskId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = priceTypeId;
            commandParameters[1].Value = modifyTaskId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static DataTable showResForAddByModify(string IdList, string modifyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("\r\n                SELECT  ModifyTaskId as Id,Res_Resource.ResourceId,ResourceCode,ResourceName,\r\n                [Name] AS Unit,Specification, ISNULL(PriceValue,0.000) AS price, \r\n                Res_Resource.Brand,ModelNumber,TechnicalParameter,0.000 AS number,ISNULL(LossCoefficient,1.000)  LossCoefficient FROM Res_Resource \r\n                LEFT JOIN Bud_ModifyTaskRes ON Res_Resource.ResourceId=Bud_ModifyTaskRes.ResourceId \r\n                and modifyId=@ModifyId LEFT JOIN Res_Unit ON UnitId = Unit \r\n                LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId \r\n                AND PriceTypeId = @PriceType \r\n                LEFT JOIN Res_PriceType ON Res_PriceType.PriceTypeId = Res_Price.PriceTypeId \r\n                WHERE Res_Resource.ResourceId IN(\r\n            ");
            builder.Append(IdList);
            builder.Append(")  ORDER BY ResourceCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PriceType", SqlDbType.NVarChar, 0x40), new SqlParameter("@modifyId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = priceTypeId;
            commandParameters[1].Value = modifyId;
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

