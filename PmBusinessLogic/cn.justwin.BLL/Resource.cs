namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using cn.justwin.resourceDAL;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public class Resource
    {
        private static string BudgetPriceType = ConfigHelper.Get("BudgetPriceType");
        private static cn.justwin.resourceDAL.Resource resource = new cn.justwin.resourceDAL.Resource();

        public DataTable GetAllResource()
        {
            return resource.GetAllResource();
        }

        public IList<string> GetAllResourceTypeId(string resourceTypeName)
        {
            cn.justwin.resourceDAL.Resource resource = new cn.justwin.resourceDAL.Resource();
            return resource.GetAllResourceTypeId(resourceTypeName);
        }

        private string GetConditionString(string resTypeId, string codes, string names, string specification, string technicalParameter, string brand)
        {
            string resTypeIdCSV = this.GetResTypeIdCSV(resTypeId);
            string str2 = DBHelper.GetQuerySql("ResourceCode", codes) + DBHelper.GetQuerySql("ResourceName", names) + DBHelper.GetQuerySql("Specification", specification) + DBHelper.GetQuerySql("TechnicalParameter", technicalParameter) + DBHelper.GetQuerySql("r.Brand", brand);
            if (resTypeIdCSV.Length > 0)
            {
                str2 = str2 + string.Format(" AND ResourceType IN ({0})", resTypeIdCSV);
            }
            return str2;
        }

        public int GetCount()
        {
            cn.justwin.resourceDAL.Resource resource = new cn.justwin.resourceDAL.Resource();
            return resource.GetCount();
        }

        private string GetPriceConditionString(string priceType, string minPrice, string maxPrice)
        {
            if (string.IsNullOrEmpty(priceType))
            {
                return string.Empty;
            }
            decimal num = 0M;
            decimal num2 = 999999999999999M;
            if (!string.IsNullOrEmpty(minPrice))
            {
                try
                {
                    num = Convert.ToDecimal(minPrice);
                }
                catch
                {
                    throw new Exception("非法数字。");
                }
            }
            if (!string.IsNullOrEmpty(maxPrice))
            {
                try
                {
                    num2 = Convert.ToDecimal(maxPrice);
                }
                catch
                {
                    throw new Exception("非法数字。");
                }
            }
            return string.Format(" AND {0} BETWEEN {1} AND {2} \n", priceType, num, num2);
        }

        public string GetPriceTypeNameCSV()
        {
            cn.justwin.resourceDAL.Resource resource = new cn.justwin.resourceDAL.Resource();
            return resource.GetPriceTypeNameCSV();
        }

        public IList<string> GetPriceTypeNameList(string userCode)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from pt in entities.Res_PriceType
                    where pt.UserCodes.Contains(userCode)
                    select pt.PriceTypeName).ToList<string>();
            }
        }

        public DataTable GetResource(string[] resources)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n                SELECT Res_Resource.ResourceId, ResourceCode, ResourceName, Specification, Name ,corpId,corpName,Res_Resource.brand,\r\n                ModelNumber,TechnicalParameter FROM Res_Resource\r\n                LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit \r\n                LEFT JOIN XPM_Basic_ContactCorp ON Res_Resource.SupplierId=XPM_Basic_ContactCorp.corpId\r\n                LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId AND PriceTypeId = '{1}'\r\n                WHERE Res_Resource.ResourceId IN({0}) ORDER BY ResourceCode DESC", DBHelper.GetInParameterSql(resources), BudgetPriceType);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public DataTable GetResource(string[] resources, string ppcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ResourceId, ResourceCode, ResourceName, Specification, [NAME],Sm_Purchaseplan_Stock.arrivalDate ").AppendLine();
            builder.Append("FROM Res_Resource ").AppendLine();
            builder.Append("LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit ").AppendLine();
            builder.Append("RIGHT JOIN Sm_Purchaseplan_Stock ON Sm_Purchaseplan_Stock.scode = Res_Resource.ResourceCode ").AppendLine();
            builder.AppendFormat("WHERE ResourceId IN ({0}) ", DBHelper.GetInParameterSql(resources)).AppendLine();
            builder.Append("AND Sm_Purchaseplan_Stock.ppcode IN(" + ppcode + ") ").AppendLine();
            builder.Append(" ORDER BY ResourceCode ASC ").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public DataTable GetResource(string typeId, int pageSize, int pageIndex, string code, string name)
        {
            string querySql = DBHelper.GetQuerySql("ResourceCode", code);
            string str2 = DBHelper.GetQuerySql("ResourceName", name);
            string cmdText = string.Format("WITH cte_Resource AS\r\n                            (\r\n\t                            SELECT * FROM Res_ResourceType WHERE ResourceTypeId = @typeId\r\n\t                            UNION ALL\r\n\t                            SELECT Res_ResourceType.* FROM cte_Resource \r\n\t                            INNER JOIN Res_ResourceType ON cte_Resource.ResourceTypeId = Res_ResourceType.ParentId\r\n                            )\r\n                            SELECT TOP(@pageSize) * FROM\r\n                            (\r\n\t                            SELECT  ROW_NUMBER() OVER (ORDER BY ResourceCode  DESC) AS row_number,Res_Resource.*,Name,ISNULL(PriceValue,0) price\r\n\t                            ,ISNULL(CorpName,'') CorpName FROM Res_Resource \r\n\t                            LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit\r\n\t                            INNER JOIN cte_Resource ON cte_Resource.ResourceTypeId = Res_Resource.ResourceType\r\n                                LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId AND PriceTypeId = @priceTypeId \r\n                                left join XPM_Basic_ContactCorp on Res_Resource.SupplierId = XPM_Basic_ContactCorp.CorpID \r\n                                WHERE 1 = 1 {0} {1}\r\n                            ) AS rt\r\n                            WHERE row_number > (@pageIndex - 1) * @pageSize\r\n                            ", querySql, str2);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeId", typeId), new SqlParameter("@priceTypeId", BudgetPriceType), new SqlParameter("@pageSize", pageSize), new SqlParameter("@pageIndex", pageIndex) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetResourceByPpcodes(string[] resources, string ppcode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ResourceId, ResourceCode, ResourceName, Specification, [NAME]").AppendLine();
            builder.Append(",Brand,ModelNumber,TechnicalParameter ").AppendLine();
            builder.Append("FROM Res_Resource ").AppendLine();
            builder.Append("LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit ").AppendLine();
            builder.Append("RIGHT JOIN Sm_Purchaseplan_Stock ON Sm_Purchaseplan_Stock.scode = Res_Resource.ResourceCode ").AppendLine();
            builder.AppendFormat("WHERE ResourceId IN ({0}) ", DBHelper.GetInParameterSql(resources)).AppendLine();
            builder.Append("AND Sm_Purchaseplan_Stock.ppcode IN(" + ppcode + ") ").AppendLine();
            builder.Append("GROUP BY ResourceId, ResourceCode, ResourceName, Specification, [NAME] ").AppendLine();
            builder.Append(",Brand,ModelNumber,TechnicalParameter ORDER BY ResourceCode DESC ").AppendLine();
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public DataTable GetResourceByRerourceName(int pageSize, int pageIndex, string name)
        {
            string cmdText = string.Format("SELECT TOP(@pageSize) * FROM\r\n                            (\r\n                                SELECT  ROW_NUMBER() OVER (ORDER BY len(ResourceCode) ASC) AS row_number,Res_Resource.*,Name,ISNULL(PriceValue,0) price,corpName\r\n                                FROM Res_Resource \r\n                                LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit\r\n                                LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId AND PriceTypeId = @BudgetPriceType\r\n                                LEFT JOIN XPM_Basic_ContactCorp ON Res_Resource.SupplierId=XPM_Basic_ContactCorp.corpId\r\n                                WHERE ResourceName = @queryName\r\n                            ) AS rt\r\n                            WHERE row_number > (@pageIndex - 1) * @pageSize", new object[0]);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@queryName", name), new SqlParameter("@pageSize", pageSize), new SqlParameter("@pageIndex", pageIndex), new SqlParameter("@BudgetPriceType", BudgetPriceType) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetResourceByRerourceType(string typeId, int pageSize, int pageIndex, string code, string name)
        {
            string querySql = DBHelper.GetQuerySql("ResourceCode", code);
            string str2 = DBHelper.GetQuerySql("ResourceName", name);
            string cmdText = string.Format("SELECT TOP(@pageSize) * FROM\r\n                            (\r\n                                SELECT  ROW_NUMBER() OVER (ORDER BY ResourceCode  DESC) AS row_number,Res_Resource.*,Name,ISNULL(PriceValue,0) price\r\n\t                            ,ISNULL(CorpName,'') CorpName FROM Res_Resource \r\n                                LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit\r\n\t                            INNER JOIN Res_ResourceType ON Res_ResourceType.ResourceTypeId = Res_Resource.ResourceType\r\n                                LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId AND PriceTypeId = @priceTypeId \r\n                                left join XPM_Basic_ContactCorp on Res_Resource.SupplierId = XPM_Basic_ContactCorp.CorpID \r\n                                WHERE ResourceType = @typeId {0} {1}\r\n                            ) AS rt\r\n                            WHERE row_number > (@pageIndex - 1) * @pageSize  ", querySql, str2);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeId", typeId), new SqlParameter("@priceTypeId", BudgetPriceType), new SqlParameter("@pageSize", pageSize), new SqlParameter("@pageIndex", pageIndex) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetResourceColumn()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from (SELECT c.column_id, c.name,ep.value FROM sys.columns AS c");
            builder.Append(" JOIN sys.tables AS t ON c.object_id = t.object_id");
            builder.Append(" JOIN sys.extended_properties AS ep ON c.column_id = ep.minor_id");
            builder.Append(" WHERE t.name = 'Res_Resource' AND ep.major_id = t.object_id) as a");
            builder.Append(" where a.name!='ResourceId'");
            builder.Append(" union select ROW_NUMBER() OVER(ORDER BY PriceTypeCode) + 100,");
            builder.Append(" PriceTypeCode as name, PriceTypeName as value from Res_PriceType");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public int GetResourceCount(string name)
        {
            string cmdText = string.Format("SELECT COUNT(*)\r\n                            FROM Res_Resource WHERE ResourceName = @queryName", new object[0]);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@queryName", name) };
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public int GetResourceCount(string typeId, string code, string name)
        {
            string querySql = DBHelper.GetQuerySql("ResourceCode", code);
            string str2 = DBHelper.GetQuerySql("ResourceName", name);
            string cmdText = string.Format("WITH cte_Resource AS\r\n                            (\r\n\t                            SELECT * FROM Res_ResourceType WHERE ResourceTypeId = @typeId\r\n\t                            UNION ALL\r\n\t                            SELECT Res_ResourceType.* FROM cte_Resource \r\n\t                            INNER JOIN Res_ResourceType ON cte_Resource.ResourceTypeId = Res_ResourceType.ParentId\r\n                            )\r\n                            SELECT COUNT(*)\r\n                            FROM Res_Resource \r\n                            INNER JOIN cte_Resource ON cte_Resource.ResourceTypeId = Res_Resource.ResourceType\r\n                            WHERE 1 = 1 {0} {1}", querySql, str2);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@typeId", typeId) };
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public int GetResourceCoutByResourceType(string typeId, string code, string name)
        {
            string querySql = DBHelper.GetQuerySql("ResourceCode", code);
            string str2 = DBHelper.GetQuerySql("ResourceName", name);
            string cmdText = string.Format("SELECT COUNT(*) FROM Res_Resource WHERE ResourceType = '{0}' {1} {2}", typeId, querySql, str2);
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText));
        }

        public DataTable GetResourceInitialize(string[] resources)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n                SELECT Res_Resource.ResourceId, ResourceCode, ResourceName, Specification, Name ,corpId,corpName,Res_Resource.brand,\r\n                ModelNumber,TechnicalParameter,ISNULL(PriceValue,0) Price,1 Number,(ISNULL(PriceValue,0)*1) Total FROM Res_Resource\r\n                LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit \r\n                LEFT JOIN XPM_Basic_ContactCorp ON Res_Resource.SupplierId=XPM_Basic_ContactCorp.corpId\r\n                LEFT JOIN Res_Price ON Res_Price.ResourceId = Res_Resource.ResourceId AND PriceTypeId = '{1}'\r\n                WHERE Res_Resource.ResourceId IN({0}) ORDER BY ResourceCode DESC", DBHelper.GetInParameterSql(resources), BudgetPriceType);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public string GetResourceName(string id)
        {
            return resource.GetResourceName(id);
        }

        public DataTable GetResourceNew(string[] resources)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n                SELECT ResourceId, ResourceCode, ResourceName, Specification, Name,Res_Resource.Brand,ModelNumber,TechnicalParameter \r\n                    FROM Res_Resource\r\n                LEFT JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit \r\n                LEFT JOIN XPM_Basic_ContactCorp ON Res_Resource.SupplierId=XPM_Basic_ContactCorp.corpId\r\n                WHERE ResourceId IN({0}) ORDER BY ResourceCode DESC", DBHelper.GetInParameterSql(resources));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString());
        }

        public static DataTable GetResType()
        {
            return SqlHelper.ExecuteQuery(CommandType.Text, " SELECT * FROM Res_ResourceType ORDER BY ResourceTypeCode");
        }

        public string GetResTypeIdCSV(string resTypeId)
        {
            return resource.GetResTypeIdCSV(resTypeId);
        }

        public DataTable Query(int pageSize, int pageIndex, string condition, string priceTypeCondition)
        {
            cn.justwin.resourceDAL.Resource resource = new cn.justwin.resourceDAL.Resource();
            return resource.Query(pageSize, pageIndex, condition, priceTypeCondition);
        }

        private DataTable Query(string userCode, string typeName, string codes, string names, string specification, string technicalParameter, string brand)
        {
            IList<string> allResourceTypeId = this.GetAllResourceTypeId(typeName);
            string condition = DBHelper.GetQuerySql("ResourceCode", codes) + DBHelper.GetQuerySql("ResourceName", names) + DBHelper.GetQuerySql("Specification", specification) + DBHelper.GetQuerySql("TechnicalParameter", technicalParameter) + DBHelper.GetQuerySql("r.Brand", brand);
            if (allResourceTypeId.Count > 0)
            {
                condition = condition + string.Format(" AND ResourceType IN ({0})", DBHelper.GetInParameterSql(allResourceTypeId.ToArray<string>()));
            }
            cn.justwin.resourceDAL.Resource resource = new cn.justwin.resourceDAL.Resource();
            IList<string> priceTypeNameList = this.GetPriceTypeNameList(userCode);
            return resource.Query(priceTypeNameList, condition);
        }

        public DataTable Query(string resTypeId, string codes, string names, string specification, string technicalParameter, string brand, string priceType, string minPrice, string maxPrice, int pageSize, int pageIndex)
        {
            cn.justwin.resourceDAL.Resource resource = new cn.justwin.resourceDAL.Resource();
            string condition = this.GetConditionString(resTypeId, codes, names, specification, technicalParameter, brand);
            string priceTypeCondition = this.GetPriceConditionString(priceType, minPrice, maxPrice);
            return resource.Query(pageSize, pageIndex, condition, priceTypeCondition);
        }

        public int QueryCount(string resTypeId, string codes, string names, string specification, string technicalParameter, string brand, string priceType, string minPrice, string maxPrice)
        {
            cn.justwin.resourceDAL.Resource resource = new cn.justwin.resourceDAL.Resource();
            string condition = this.GetConditionString(resTypeId, codes, names, specification, technicalParameter, brand);
            string priceCondition = this.GetPriceConditionString(priceType, minPrice, maxPrice);
            return resource.QueryCount(condition, priceCondition);
        }

        public DataTable Select(string userCode, string typeId, int pageSize, int pageIndex, string codes, string names, string specification, string technicalParameter, string brand)
        {
            string condition = DBHelper.GetQuerySql("ResourceCode", codes) + DBHelper.GetQuerySql("ResourceName", names) + DBHelper.GetQuerySql("Specification", specification) + DBHelper.GetQuerySql("TechnicalParameter", technicalParameter) + DBHelper.GetQuerySql("Brand", brand);
            if (condition.Length >= 4)
            {
                condition = condition.Remove(0, 4);
            }
            DataTable table = new cn.justwin.resourceDAL.Resource().Select(typeId, pageSize, pageIndex, condition);
            if (!string.IsNullOrEmpty(userCode))
            {
                try
                {
                    using (pm2Entities entities = new pm2Entities())
                    {
                        foreach (var type in from pt in entities.Res_PriceType select new { PriceTypeName = pt.PriceTypeName, UserCodes = pt.UserCodes })
                        {
                            if (!JsonHelper.GetListFromJson(type.UserCodes).Contains(userCode))
                            {
                                table.Columns.Remove(type.PriceTypeName);
                            }
                        }
                        return table;
                    }
                }
                catch
                {
                }
            }
            return table;
        }
    }
}

