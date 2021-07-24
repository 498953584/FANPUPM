namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ResourceTypeAction
    {
        public void Add(ResourceType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Res_ResourceType(");
            builder.Append("ResourceTypeId,ParentId,ResourceTypeCode,ResourceTypeName,InputUser,InputDate,IsValid)");
            builder.Append(" values (");
            builder.Append("@ResourceTypeId,@ParentId,@ResourceTypeCode,@ResourceTypeName,@InputUser,@InputDate,@IsValid)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ResourceTypeId", SqlDbType.NVarChar, 500), new SqlParameter("@ParentId", SqlDbType.NVarChar, 500), new SqlParameter("@ResourceTypeCode", SqlDbType.NVarChar, 500), new SqlParameter("@ResourceTypeName", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@InputUser", SqlDbType.NVarChar, 20), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@IsValid", SqlDbType.Bit, 1) };
            commandParameters[0].Value = model.ResourceTypeId;
            commandParameters[1].Value = model.ParentId;
            commandParameters[2].Value = model.ResourceTypeCode;
            commandParameters[3].Value = model.ResourceTypeName;
            commandParameters[4].Value = model.InputUser;
            commandParameters[5].Value = model.InputDate;
            commandParameters[6].Value = model.IsValid;
            SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void Delete(string ResourceTypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Res_ResourceType ");
            builder.Append(" where ResourceTypeId=@ResourceTypeId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ResourceTypeId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ResourceTypeId;
            SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable getDetailResource(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select res.resourceid, res.resourcecode,res.resourceName,res.specification,(select resourceTypeName from Res_ResourceType where ResourceTypeId=res.resourceType) as resourceType,(select [name] from res_unit where unitid=res.Unit) as unit from res_resource as res ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
            if (table.Rows.Count > 0)
            {
                return table;
            }
            return null;
        }

        public DataTable GetList(string strwhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ResourceTypeId,ParentId,ResourceTypeCode,ResourceTypeName,InputUser,InputDate,IsValid ");
            builder.Append(" FROM Res_ResourceType ");
            if (strwhere.Trim() != "")
            {
                builder.Append(" where " + strwhere);
            }
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
            if (table.Rows.Count > 0)
            {
                return table;
            }
            return null;
        }

        public DataTable GetList(string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(" ResourceTypeId,ParentId,ResourceTypeCode,ResourceTypeName,InputUser,InputDate,IsValid ");
            builder.Append(" FROM Res_ResourceType ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                builder.Append(" order by " + filedOrder);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public ResourceType GetModel(string ResourceTypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ResourceTypeId,ParentId,ResourceTypeCode,ResourceTypeName,InputUser,InputDate,IsValid from Res_ResourceType ");
            builder.Append(" where ResourceTypeId=@ResourceTypeId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ResourceTypeId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ResourceTypeId;
            ResourceType type = new ResourceType();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            type.ResourceTypeId = table.Rows[0]["ResourceTypeId"].ToString();
            type.ParentId = table.Rows[0]["ParentId"].ToString();
            type.ResourceTypeCode = table.Rows[0]["ResourceTypeCode"].ToString();
            type.ResourceTypeName = table.Rows[0]["ResourceTypeName"].ToString();
            type.InputUser = table.Rows[0]["InputUser"].ToString();
            if (table.Rows[0]["InputDate"].ToString() != "")
            {
                type.InputDate = new DateTime?(DateTime.Parse(table.Rows[0]["InputDate"].ToString()));
            }
            if (table.Rows[0]["IsValid"].ToString() != "")
            {
                if ((table.Rows[0]["IsValid"].ToString() == "1") || (table.Rows[0]["IsValid"].ToString().ToLower() == "true"))
                {
                    type.IsValid = true;
                    return type;
                }
                type.IsValid = false;
            }
            return type;
        }

        public DataTable getreaourceList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select type.ResourceTypeCode,type.ResourceTypeName,res.* FROM Res_Resource as res inner join Res_ResourceType as type on res.ResourceType=type.ResourceTypeId ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
            if (table.Rows.Count > 0)
            {
                return table;
            }
            return null;
        }

        public void Update(ResourceType model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Res_ResourceType set ");
            builder.Append("ParentId=@ParentId,");
            builder.Append("ResourceTypeCode=@ResourceTypeCode,");
            builder.Append("ResourceTypeName=@ResourceTypeName,");
            builder.Append("InputUser=@InputUser,");
            builder.Append("InputDate=@InputDate,");
            builder.Append("IsValid=@IsValid");
            builder.Append(" where ResourceTypeId=@ResourceTypeId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ResourceTypeId", SqlDbType.NVarChar, 500), new SqlParameter("@ParentId", SqlDbType.NVarChar, 500), new SqlParameter("@ResourceTypeCode", SqlDbType.NVarChar, 500), new SqlParameter("@ResourceTypeName", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@InputUser", SqlDbType.NVarChar, 20), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@IsValid", SqlDbType.Bit, 1) };
            commandParameters[0].Value = model.ResourceTypeId;
            commandParameters[1].Value = model.ParentId;
            commandParameters[2].Value = model.ResourceTypeCode;
            commandParameters[3].Value = model.ResourceTypeName;
            commandParameters[4].Value = model.InputUser;
            commandParameters[5].Value = model.InputDate;
            commandParameters[6].Value = model.IsValid;
            SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

