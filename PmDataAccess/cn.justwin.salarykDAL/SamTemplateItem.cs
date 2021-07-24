namespace cn.justwin.salarykDAL
{
    using cn.justwin.DAL;
    using cn.justwin.salaryDAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public static class SamTemplateItem
    {
        private static UserSalaryItem usi = new UserSalaryItem();

        public static int Add(SamTemplateItemModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SaM_TemplateItem(");
            builder.Append("ItemID,ItemName,Remark,ItemCode)");
            builder.Append(" values (");
            builder.Append("@ItemID,@ItemName,@Remark,@ItemCode)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ItemID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ItemName", SqlDbType.NVarChar, 0x20), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x400), new SqlParameter("@ItemCode", SqlDbType.NVarChar, 0x20) };
            commandParameters[0].Value = model.ItemID;
            commandParameters[1].Value = model.ItemName;
            commandParameters[2].Value = model.Remark;
            commandParameters[3].Value = model.ItemCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int Delete(string ItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_TemplateItem ");
            builder.Append(" where ItemID=@ItemID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ItemID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ItemID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int Delete(List<string> ItemId)
        {
            int num = 0;
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (string str in ItemId)
                    {
                        Delete(str);
                        num++;
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    num = 0;
                }
            }
            return num;
        }

        public static object Exists(string ItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SaM_TemplateItem");
            builder.Append(" where ItemID=@ItemID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ItemID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ItemID;
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ItemID,ItemName,Remark,ItemCode ");
            builder.Append(" FROM SaM_TemplateItem ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public static DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" ItemID,ItemName,Remark,ItemCode ");
            builder.Append(" FROM SaM_TemplateItem ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public static SamTemplateItemModel GetModel(string ItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 ItemID,ItemName,Remark,ItemCode from SaM_TemplateItem ");
            builder.Append(" where ItemID=@ItemID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ItemID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ItemID;
            SamTemplateItemModel model = new SamTemplateItemModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count > 0)
            {
                model.ItemID = table.Rows[0]["ItemID"].ToString();
                model.ItemName = table.Rows[0]["ItemName"].ToString();
                model.Remark = table.Rows[0]["Remark"].ToString();
                model.ItemCode = table.Rows[0]["ItemCode"].ToString();
                return model;
            }
            return null;
        }

        public static int Update(SamTemplateItemModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SaM_TemplateItem set ");
            builder.Append("ItemName=@ItemName,");
            builder.Append("Remark=@Remark,ItemCode=@ItemCode");
            builder.Append(" where ItemID=@ItemID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ItemID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ItemName", SqlDbType.NVarChar, 0x20), new SqlParameter("@Remark", SqlDbType.NVarChar, 0x400), new SqlParameter("@ItemCode", SqlDbType.NVarChar, 0x20) };
            commandParameters[0].Value = model.ItemID;
            commandParameters[1].Value = model.ItemName;
            commandParameters[2].Value = model.Remark;
            commandParameters[3].Value = model.ItemCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

