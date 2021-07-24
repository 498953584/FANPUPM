namespace cn.justwin.epm.datum
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class datumClassService
    {
        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeId,TypeName,Remark,ParentId,IsValid,isDelete,IsVisible,IsFixup ");
            builder.Append(" FROM EPM_Datum_Class ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetListByTable(string strWhere, string table)
        {
            table = "EPM_Datum_Criterion";
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM  ").Append(table);
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public datumClassModel GetModel(int TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TypeId,TypeName,Remark,ParentId,IsValid,isDelete,IsVisible,IsFixup from EPM_Datum_Class ");
            builder.Append(" where TypeId=@TypeId");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.Int, 4) };
            commandParameters[0].Value = TypeId;
            datumClassModel model = new datumClassModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if ((table.Rows[0]["TypeId"] != null) && (table.Rows[0]["TypeId"].ToString() != ""))
            {
                model.TypeId = int.Parse(table.Rows[0]["TypeId"].ToString());
            }
            if ((table.Rows[0]["TypeName"] != null) && (table.Rows[0]["TypeName"].ToString() != ""))
            {
                model.TypeName = table.Rows[0]["TypeName"].ToString();
            }
            if ((table.Rows[0]["Remark"] != null) && (table.Rows[0]["Remark"].ToString() != ""))
            {
                model.Remark = table.Rows[0]["Remark"].ToString();
            }
            if ((table.Rows[0]["ParentId"] != null) && (table.Rows[0]["ParentId"].ToString() != ""))
            {
                model.ParentId = int.Parse(table.Rows[0]["ParentId"].ToString());
            }
            if ((table.Rows[0]["IsValid"] != null) && (table.Rows[0]["IsValid"].ToString() != ""))
            {
                if ((table.Rows[0]["IsValid"].ToString() == "1") || (table.Rows[0]["IsValid"].ToString().ToLower() == "true"))
                {
                    model.IsValid = true;
                }
                else
                {
                    model.IsValid = false;
                }
            }
            if ((table.Rows[0]["isDelete"] != null) && (table.Rows[0]["isDelete"].ToString() != ""))
            {
                if ((table.Rows[0]["isDelete"].ToString() == "1") || (table.Rows[0]["isDelete"].ToString().ToLower() == "true"))
                {
                    model.isDelete = true;
                }
                else
                {
                    model.isDelete = false;
                }
            }
            if ((table.Rows[0]["IsVisible"] != null) && (table.Rows[0]["IsVisible"].ToString() != ""))
            {
                if ((table.Rows[0]["IsVisible"].ToString() == "1") || (table.Rows[0]["IsVisible"].ToString().ToLower() == "true"))
                {
                    model.IsVisible = true;
                }
                else
                {
                    model.IsVisible = false;
                }
            }
            if ((table.Rows[0]["IsFixup"] != null) && (table.Rows[0]["IsFixup"].ToString() != ""))
            {
                if ((table.Rows[0]["IsFixup"].ToString() == "1") || (table.Rows[0]["IsFixup"].ToString().ToLower() == "true"))
                {
                    model.IsFixup = true;
                    return model;
                }
                model.IsFixup = false;
            }
            return model;
        }
    }
}

