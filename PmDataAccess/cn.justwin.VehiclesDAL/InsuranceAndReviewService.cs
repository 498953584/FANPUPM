namespace cn.justwin.VehiclesDAL
{
    using cn.justwin.DAL;
    using cn.justwin.VehiclesModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class InsuranceAndReviewService
    {
        public void Add(InsuranceAndReviewModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Vehicle_InsuranceAndReview(");
            builder.Append("Guid,Date,Type,VehicleCode,code)");
            builder.Append(" values (");
            builder.Append("@Guid,@Date,@Type,@VehicleCode,@code)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Date", SqlDbType.DateTime), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@VehicleCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@code", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = Guid.NewGuid();
            if ((model.Date.ToString() == "") || (model.Date == null))
            {
                commandParameters[1].Value = DBNull.Value;
            }
            else
            {
                commandParameters[1].Value = DateTime.Parse(model.Date);
            }
            commandParameters[2].Value = model.Type;
            commandParameters[3].Value = model.VehicleCode;
            commandParameters[4].Value = model.code;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(Guid Guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OA_Vehicle_InsuranceAndReview ");
            builder.Append(" where Guid=@Guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = Guid;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool DeleteList(string Guidlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OA_Vehicle_InsuranceAndReview ");
            builder.Append(" where Guid in (" + Guidlist + ")  ");
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }

        public bool Exists(Guid Guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Vehicle_InsuranceAndReview");
            builder.Append(" where Guid=@Guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = Guid;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select OA_Vehicle_InsuranceAndReview.Guid,OA_Vehicle_InsuranceAndReview.Date,OA_Vehicle_InsuranceAndReview.Type,OA_Vehicle_InsuranceAndReview.VehicleCode,OA_Vehicle_InsuranceAndReview.code ");
            builder.Append(" FROM OA_Vehicle_InsuranceAndReview ");
            builder.Append(" left join OA_Vehicle_Main on OA_Vehicle_InsuranceAndReview.VehicleCode=OA_Vehicle_Main.guid ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" Guid,Date,Type,VehicleCode,code ");
            builder.Append(" FROM OA_Vehicle_InsuranceAndReview ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public InsuranceAndReviewModel GetModel(Guid Guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Guid,Date,Type,VehicleCode,code from OA_Vehicle_InsuranceAndReview ");
            builder.Append(" where Guid=@Guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = Guid;
            InsuranceAndReviewModel model = new InsuranceAndReviewModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["Guid"].ToString() != "")
            {
                model.Guid = new Guid(table.Rows[0]["Guid"].ToString());
            }
            if (table.Rows[0]["Date"].ToString() != "")
            {
                model.Date = table.Rows[0]["Date"].ToString();
            }
            if (table.Rows[0]["Type"].ToString() != "")
            {
                model.Type = new int?(int.Parse(table.Rows[0]["Type"].ToString()));
            }
            if (table.Rows[0]["VehicleCode"].ToString() != "")
            {
                model.VehicleCode = new Guid(table.Rows[0]["VehicleCode"].ToString());
            }
            model.code = table.Rows[0]["code"].ToString();
            return model;
        }

        public bool Update(InsuranceAndReviewModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Vehicle_InsuranceAndReview set ");
            builder.Append("Date=@Date,");
            builder.Append("Type=@Type,");
            builder.Append("VehicleCode=@VehicleCode,");
            builder.Append("code=@code");
            builder.Append(" where Guid=@Guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Date", SqlDbType.DateTime), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@VehicleCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@code", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = model.Guid;
            if ((model.Date.ToString() == "") || (model.Date == null))
            {
                commandParameters[1].Value = DBNull.Value;
            }
            else
            {
                commandParameters[1].Value = DateTime.Parse(model.Date);
            }
            commandParameters[2].Value = model.Type;
            commandParameters[3].Value = model.VehicleCode;
            commandParameters[4].Value = model.code;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

