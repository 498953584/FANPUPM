namespace cn.justwin.VehiclesDAL
{
    using cn.justwin.DAL;
    using cn.justwin.VehiclesModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ReimbursementService
    {
        public bool Add(Reimbursement model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_Vehicle_Reimbursement(");
            builder.Append("Guid,UserName,VehicleGuid,Date,Destination,Tolls,Repairs,FuelCosts,MaintenanceCosts,Remark,ReimbursementCode)");
            builder.Append(" values (");
            builder.Append("@Guid,@UserName,@VehicleGuid,@Date,@Destination,@Tolls,@Repairs,@FuelCosts,@MaintenanceCosts,@Remark,@ReimbursementCode)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@VehicleGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Date", SqlDbType.DateTime), new SqlParameter("@Destination", SqlDbType.NVarChar), new SqlParameter("@Tolls", SqlDbType.Decimal, 9), new SqlParameter("@Repairs", SqlDbType.Decimal, 9), new SqlParameter("@FuelCosts", SqlDbType.Decimal, 9), new SqlParameter("@MaintenanceCosts", SqlDbType.Decimal, 9), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@ReimbursementCode", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = model.Guid;
            commandParameters[1].Value = model.UserName;
            commandParameters[2].Value = model.VehicleGuid;
            commandParameters[3].Value = model.Date;
            commandParameters[4].Value = model.Destination;
            commandParameters[5].Value = model.Tolls;
            commandParameters[6].Value = model.Repairs;
            commandParameters[7].Value = model.FuelCosts;
            commandParameters[8].Value = model.MaintenanceCosts;
            commandParameters[9].Value = model.Remark;
            commandParameters[10].Value = model.ReimbursementCode;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Delete(Guid Guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OA_Vehicle_Reimbursement ");
            builder.Append(" where Guid=@Guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = Guid;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool DeleteList(string Guidlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OA_Vehicle_Reimbursement ");
            builder.Append(" where Guid in (" + Guidlist + ")  ");
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }

        public bool Exists(Guid Guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_Vehicle_Reimbursement");
            builder.Append(" where Guid=@Guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = Guid;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters)) > 0);
        }

        public DataTable getAllList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  a.*,b.VehicleCode from OA_Vehicle_Reimbursement as a inner join OA_Vehicle_Main as b on b.guid=a.VehicleGuid ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by Date desc ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM OA_Vehicle_Reimbursement ");
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
            builder.Append(" * ");
            builder.Append(" FROM OA_Vehicle_Reimbursement ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public Reimbursement GetModel(Guid Guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 Guid,UserName,VehicleGuid,Date,Destination,Tolls,Repairs,FuelCosts,MaintenanceCosts,Remark,ReimbursementCode from OA_Vehicle_Reimbursement ");
            builder.Append(" where Guid=@Guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = Guid;
            Reimbursement reimbursement = new Reimbursement();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["Guid"].ToString() != "")
            {
                reimbursement.Guid = new Guid(table.Rows[0]["Guid"].ToString());
            }
            reimbursement.UserName = table.Rows[0]["UserName"].ToString();
            if (table.Rows[0]["VehicleGuid"].ToString() != "")
            {
                reimbursement.VehicleGuid = new Guid(table.Rows[0]["VehicleGuid"].ToString());
            }
            if (table.Rows[0]["Date"].ToString() != "")
            {
                reimbursement.Date = new DateTime?(DateTime.Parse(table.Rows[0]["Date"].ToString()));
            }
            reimbursement.Destination = table.Rows[0]["Destination"].ToString();
            if (table.Rows[0]["Tolls"].ToString() != "")
            {
                reimbursement.Tolls = new decimal?(decimal.Parse(table.Rows[0]["Tolls"].ToString()));
            }
            if (table.Rows[0]["Repairs"].ToString() != "")
            {
                reimbursement.Repairs = new decimal?(decimal.Parse(table.Rows[0]["Repairs"].ToString()));
            }
            if (table.Rows[0]["FuelCosts"].ToString() != "")
            {
                reimbursement.FuelCosts = new decimal?(decimal.Parse(table.Rows[0]["FuelCosts"].ToString()));
            }
            if (table.Rows[0]["MaintenanceCosts"].ToString() != "")
            {
                reimbursement.MaintenanceCosts = new decimal?(decimal.Parse(table.Rows[0]["MaintenanceCosts"].ToString()));
            }
            reimbursement.Remark = table.Rows[0]["Remark"].ToString();
            reimbursement.ReimbursementCode = table.Rows[0]["ReimbursementCode"].ToString();
            return reimbursement;
        }

        public bool Update(Reimbursement model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_Vehicle_Reimbursement set ");
            builder.Append("UserName=@UserName,");
            builder.Append("VehicleGuid=@VehicleGuid,");
            builder.Append("Date=@Date,");
            builder.Append("Destination=@Destination,");
            builder.Append("Tolls=@Tolls,");
            builder.Append("Repairs=@Repairs,");
            builder.Append("FuelCosts=@FuelCosts,");
            builder.Append("MaintenanceCosts=@MaintenanceCosts,");
            builder.Append("Remark=@Remark,");
            builder.Append("ReimbursementCode=@ReimbursementCode");
            builder.Append(" where Guid=@Guid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Guid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@VehicleGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@Date", SqlDbType.DateTime), new SqlParameter("@Destination", SqlDbType.NVarChar), new SqlParameter("@Tolls", SqlDbType.Decimal, 9), new SqlParameter("@Repairs", SqlDbType.Decimal, 9), new SqlParameter("@FuelCosts", SqlDbType.Decimal, 9), new SqlParameter("@MaintenanceCosts", SqlDbType.Decimal, 9), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@ReimbursementCode", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = model.Guid;
            commandParameters[1].Value = model.UserName;
            commandParameters[2].Value = model.VehicleGuid;
            commandParameters[3].Value = model.Date;
            commandParameters[4].Value = model.Destination;
            commandParameters[5].Value = model.Tolls;
            commandParameters[6].Value = model.Repairs;
            commandParameters[7].Value = model.FuelCosts;
            commandParameters[8].Value = model.MaintenanceCosts;
            commandParameters[9].Value = model.Remark;
            commandParameters[10].Value = model.ReimbursementCode;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

