namespace cn.justwin.resourceDAL
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    public class Resource
    {
        public DataTable GetAllResource()
        {
            string cmdText = "SELECT * FROM Res_Resource ";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
        }

        public IList<string> GetAllResourceTypeId(string resourceTypeName)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                IQueryable<string> queryable = from rt in entities.Res_ResourceType
                    where rt.ResourceTypeName == resourceTypeName
                    select rt.ResourceTypeId;
                IQueryable<string> queryable2 = queryable;
                using (IEnumerator<string> enumerator = queryable.GetEnumerator())
                {
                    string id;
                    while (enumerator.MoveNext())
                    {
                        id = enumerator.Current;
                        IQueryable<string> queryable3 = from rt in entities.Res_ResourceType
                            join r in entities.Res_ResourceType on rt.ResourceTypeId equals r.Res_ResourceType2.ResourceTypeId 
                            where rt.Res_ResourceType2.ResourceTypeId == id
                            select r.ResourceTypeId;
                        IQueryable<string> queryable4 = from rt in entities.Res_ResourceType
                            where rt.Res_ResourceType2.ResourceTypeId == id
                            select rt.ResourceTypeId;
                        queryable2 = queryable2.Union<string>(queryable3).Union<string>(queryable4);
                    }
                }
                return queryable2.ToList<string>();
            }
        }

        public int GetCount()
        {
            string str = "SELECT COUNT(1) FROM Res_Resource";
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, str.ToString(), new SqlParameter[0]));
        }

        public string GetPriceTypeNameCSV()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DECLARE @price AS nvarchar(max) \n");
            builder.Append("SELECT @price = ISNULL(@price + ',','') + PriceTypeName \n");
            builder.Append("FROM ( \n");
            builder.Append("\tSELECT DISTINCT PriceTypeName FROM Res_PriceType \n");
            builder.Append(") AS T \n");
            builder.Append("SELECT @price \n");
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[0]).ToString();
        }

        public DataTable GetResource(int pageSize, int pageIndex, string condition)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ROW_NUMBER() OVER(ORDER BY InputDate) AS Number,* FROM ").AppendLine();
            builder.Append("(").AppendLine();
            builder.Append("\tSELECT r.ResourceId,r.ResourceCode,r.ResourceName,r.Brand,").AppendLine();
            builder.Append("\t  r.TaxRate,r.Specification,r.ResourceType,r.InputDate,").AppendLine();
            builder.Append("\t  r.TechnicalParameter, r.Series, r.ModelNumber,r.Note,").AppendLine();
            builder.Append("\t  u.Name,p.PriceValue,pt.PriceTypeName,c.CorpName ").AppendLine();
            builder.Append("\tFROM Res_Price AS p").AppendLine();
            builder.Append("\tRIGHT JOIN Res_Resource AS r ON p.ResourceId = r.ResourceId").AppendLine();
            builder.Append("\tLEFT JOIN  Res_PriceType AS pt ON pt.PriceTypeId = p.PriceTypeId").AppendLine();
            builder.Append("\tLEFT JOIN Res_Unit AS u ON u.UnitId = r.Unit").AppendLine();
            builder.Append("\tLEFT JOIN XPM_Basic_ContactCorp AS c ON c.CorpID = r.SupplierId").AppendLine();
            builder.Append("\tGROUP BY r.ResourceId,r.ResourceCode,r.ResourceName,r.Brand,").AppendLine();
            builder.Append("\t  r.TaxRate,r.Specification,r.ResourceType,r.InputDate,").AppendLine();
            builder.Append("\t  r.Series, r.ModelNumber,r.Note,c.CorpName,").AppendLine();
            builder.Append("\t  r.TechnicalParameter,u.Name,p.PriceValue,pt.PriceTypeName").AppendLine();
            builder.Append(") rtt").AppendLine();
            builder.Append("WHERE 1 = 1 ").AppendLine();
            builder.Append(condition).AppendLine();
            return table;
        }

        public DataTable GetResource(string typeId, int pageSize, int pageIndex)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pageSize", pageSize), new SqlParameter("@pageIndex", pageIndex), new SqlParameter("@ResourceTypeId", typeId) };
            return SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "cpResource", commandParameters);
        }

        public string GetResourceName(string id)
        {
            string cmdText = "SELECT ResourceName FROM Res_Resource WHERE ResourceId = @ResourceId";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ResourceId", id) };
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters));
        }

        public string GetResTypeIdCSV(string resTypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("DECLARE @type AS nvarchar(max); \n");
            builder.Append("WITH cteResType AS \n");
            builder.Append("( \n");
            builder.Append("\tSELECT ResourceTypeId, ParentId, ResourceTypeCode, ResourceTypeName \n");
            builder.Append("\tFROM Res_ResourceType \n");
            if (string.IsNullOrEmpty(resTypeId))
            {
                builder.Append("\tWHERE ParentId IS NULL \n");
            }
            else
            {
                builder.AppendFormat("\tWHERE ResourceTypeId = '{0}' \n", resTypeId);
            }
            builder.Append("\tUNION ALL  \n");
            builder.Append("\tSELECT RT.ResourceTypeId, RT.ParentId, RT.ResourceTypeCode, RT.ResourceTypeName \n");
            builder.Append("\tFROM Res_ResourceType AS RT \n");
            builder.Append("\tINNER JOIN cteResType ON RT.ParentId = cteResType.ResourceTypeID \n");
            builder.Append(") \n");
            builder.Append("SELECT @type = ISNULL(@type + ',','') + '''' + ResourceTypeId + '''' \n");
            builder.Append("FROM ( \n");
            builder.Append("\tSELECT DISTINCT ResourceTypeId FROM cteResType \n");
            builder.Append(") AS T \n");
            builder.Append("SELECT @type \n");
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[0]).ToString();
        }

        public DataTable Query(IList<string> priceTypeNameList, string condition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ROW_NUMBER() OVER(ORDER BY InputDate) AS Number,* FROM ").AppendLine();
            builder.Append("(").AppendLine();
            builder.Append("    SELECT r.ResourceId,r.ResourceCode,r.ResourceName,r.Brand, ").AppendLine();
            builder.Append("    r.TaxRate,r.Specification,r.ResourceType,r.InputDate,").AppendLine();
            builder.Append("    r.TechnicalParameter, r.Series, r.ModelNumber,r.Note,").AppendLine();
            builder.Append("    u.Name,p.PriceValue,pt.PriceTypeName,c.CorpName,c.CorpID ").AppendLine();
            builder.Append("    FROM Res_Price AS p").AppendLine();
            builder.Append("    RIGHT JOIN Res_Resource AS r ON p.ResourceId = r.ResourceId").AppendLine();
            builder.Append("    LEFT JOIN  Res_PriceType AS pt ON pt.PriceTypeId = p.PriceTypeId").AppendLine();
            builder.Append("    LEFT JOIN Res_Unit AS u ON u.UnitId = r.Unit").AppendLine();
            builder.Append("    LEFT JOIN XPM_Basic_ContactCorp AS c ON c.CorpID = r.SupplierId").AppendLine();
            builder.Append("    WHERE 1=1 ").Append(condition).AppendLine();
            builder.Append(") rtt").AppendLine();
            string priceTypeNameCSV = this.GetPriceTypeNameCSV();
            if (!string.IsNullOrEmpty(priceTypeNameCSV))
            {
                builder.Append("PIVOT ").AppendLine();
                builder.Append("(").AppendLine();
                builder.AppendFormat("\tMAX(PriceValue) FOR PriceTypeName in ({0})", priceTypeNameCSV).AppendLine();
                builder.Append(") AS pvt").AppendLine();
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable Query(int pageSize, int pageIndex, string condition, string priceTypeCondition)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP({0}) * \n", pageSize);
            builder.Append("FROM ( \n");
            builder.Append("\tSELECT ROW_NUMBER() OVER(ORDER BY ResourceCode, ResourceType, InputDate) AS Number,* FROM  \n");
            builder.Append("\t( \n");
            builder.Append("\t\tSELECT r.ResourceId,r.ResourceCode,r.ResourceName,r.Brand, \n");
            builder.Append("\t\tr.TaxRate,r.Specification,r.ResourceType,r.InputDate, \n");
            builder.Append("\t\tr.TechnicalParameter, r.Series, r.ModelNumber,r.Note, \n");
            builder.Append("\t\tu.Name,p.PriceValue,pt.PriceTypeName,c.CorpName,c.CorpID \n");
            builder.Append("\t\tFROM Res_Price AS p \n");
            builder.Append("\t\tRIGHT JOIN Res_Resource AS r ON p.ResourceId = r.ResourceId \n");
            builder.Append("\t\tLEFT JOIN  Res_PriceType AS pt ON pt.PriceTypeId = p.PriceTypeId \n");
            builder.Append("\t\tLEFT JOIN Res_Unit AS u ON u.UnitId = r.Unit \n");
            builder.Append("\t\tLEFT JOIN XPM_Basic_ContactCorp AS c ON c.CorpID = r.SupplierId \n");
            builder.Append("\t\tWHERE 1=1 \n").Append(condition);
            builder.Append("\t) rtt \n");
            builder.Append("\tPIVOT \n");
            builder.Append("\t( \n");
            builder.AppendFormat("MAX(PriceValue) FOR PriceTypeName in ({0}) \n", this.GetPriceTypeNameCSV());
            builder.Append("\t) \n");
            builder.Append("\tAS pvt \n");
            builder.Append("\tWHERE 1 = 1 \n").Append(priceTypeCondition);
            builder.Append(") AS T2 \n");
            builder.AppendFormat("WHERE Number > ({0} - 1) * {1} \n", pageIndex, pageSize);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public int QueryCount(string condition, string priceCondition)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) \n");
            builder.Append("FROM ( \n");
            builder.Append("\tSELECT ROW_NUMBER() OVER(ORDER BY ResourceCode, ResourceType, InputDate) AS Number,* FROM  \n");
            builder.Append("\t( \n");
            builder.Append("\t\tSELECT r.ResourceId,r.ResourceCode,r.ResourceName,r.Brand, \n");
            builder.Append("\t\tr.TaxRate,r.Specification,r.ResourceType,r.InputDate, \n");
            builder.Append("\t\tr.TechnicalParameter, r.Series, r.ModelNumber,r.Note, \n");
            builder.Append("\t\tu.Name,p.PriceValue,pt.PriceTypeName,c.CorpName,c.CorpID \n");
            builder.Append("\t\tFROM Res_Price AS p \n");
            builder.Append("\t\tRIGHT JOIN Res_Resource AS r ON p.ResourceId = r.ResourceId \n");
            builder.Append("\t\tLEFT JOIN  Res_PriceType AS pt ON pt.PriceTypeId = p.PriceTypeId \n");
            builder.Append("\t\tLEFT JOIN Res_Unit AS u ON u.UnitId = r.Unit \n");
            builder.Append("\t\tLEFT JOIN XPM_Basic_ContactCorp AS c ON c.CorpID = r.SupplierId \n");
            builder.Append("\t\tWHERE 1=1 \n").Append(condition);
            builder.Append("\t) rtt \n");
            builder.Append("\tPIVOT \n");
            builder.Append("\t( \n");
            builder.AppendFormat("MAX(PriceValue) FOR PriceTypeName in ({0}) \n", this.GetPriceTypeNameCSV());
            builder.Append("\t) \n");
            builder.Append("\tAS pvt \n");
            builder.Append("\tWHERE 1 = 1 \n").Append(priceCondition);
            builder.Append(") AS T2 \n");
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[0]));
        }

        public DataTable Select(string typeId, int pageSize, int pageIndex, string condition)
        {
            DataTable table = new DataTable();
            if (string.IsNullOrEmpty(typeId))
            {
                return this.GetResource(pageIndex, pageIndex, condition);
            }
            DataView defaultView = this.GetResource(typeId, pageSize, pageIndex).DefaultView;
            defaultView.RowFilter = condition;
            return defaultView.ToTable();
        }
    }
}

