namespace cn.justwin.salaryDAL
{
    using cn.justwin.DAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class UserSalaryItem
    {
        public int Add(SqlTransaction trans, UserSalaryItemModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SaM_UserSalaryItem(");
            builder.Append("UserItemID,UserCode,ItemCode,ItemValue)");
            builder.Append(" values (");
            builder.Append("@UserItemID,@UserCode,@ItemCode,@ItemValue)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserItemID", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ItemCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ItemValue", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.UserItemID;
            commandParameters[1].Value = model.UserCode;
            commandParameters[2].Value = model.ItemCode;
            commandParameters[3].Value = model.ItemValue;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string UserItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_UserSalaryItem ");
            builder.Append(" where UserItemID=@UserItemID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserItemID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = UserItemID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByCode(string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_UserSalaryItem ");
            builder.Append(" where userCode=@userCode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = userCode;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<UserSalaryItemModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserItemID,UserCode,ItemCode,ItemValue ");
            builder.Append(" FROM SaM_UserSalaryItem ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<UserSalaryItemModel> list = new List<UserSalaryItemModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public UserSalaryItemModel GetModel(string UserItemID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select UserItemID,UserCode,ItemCode,ItemValue from SaM_UserSalaryItem ");
            builder.Append(" where UserItemID=@UserItemID ");
            UserSalaryItemModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@UserItemID", UserItemID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetTbByItmeCode(string userCode, string code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  p1.ItemName,p1.itemCode,p2.itemValue");
            builder.Append(" from Sam_TemplateItem as p1");
            builder.Append(" left join dbo.SaM_UserSalaryItem as p2 on p1.itemCode=p2.itemCode");
            builder.Append(" where p1.ItemCode in (" + code + ") and p2.userCode='" + userCode + "'");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public UserSalaryItemModel ReaderBind(IDataReader dataReader)
        {
            UserSalaryItemModel model = new UserSalaryItemModel {
                UserItemID = dataReader["UserItemID"].ToString(),
                UserCode = dataReader["UserCode"].ToString(),
                ItemCode = dataReader["ItemCode"].ToString()
            };
            object obj2 = dataReader["ItemValue"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ItemValue = new decimal?((decimal) obj2);
            }
            return model;
        }

        public int Update(UserSalaryItemModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SaM_UserSalaryItem set ");
            builder.Append("UserCode=@UserCode,");
            builder.Append("ItemCode=@ItemCode,");
            builder.Append("ItemValue=@ItemValue");
            builder.Append(" where UserItemID=@UserItemID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserItemID", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ItemCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ItemValue", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.UserItemID;
            commandParameters[1].Value = model.UserCode;
            commandParameters[2].Value = model.ItemCode;
            commandParameters[3].Value = model.ItemValue;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

