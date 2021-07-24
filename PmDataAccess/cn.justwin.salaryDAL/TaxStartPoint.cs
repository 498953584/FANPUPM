namespace cn.justwin.salaryDAL
{
    using cn.justwin.DAL;
    using cn.justwin.salaryModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class TaxStartPoint
    {
        public int Add(TaxStartPointModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SaM_IncomeTax(");
            builder.Append("TaxRateID,TaxStartPoint,LowerLimit,UpperLimit,TaxRate,Deduct,AddDate)");
            builder.Append(" values (");
            builder.Append("@TaxRateID,@TaxStartPoint,@LowerLimit,@UpperLimit,@TaxRate,@Deduct,@AddDate)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TaxRateID", SqlDbType.NVarChar, 0x40), new SqlParameter("@TaxStartPoint", SqlDbType.Decimal, 9), new SqlParameter("@LowerLimit", SqlDbType.Decimal, 9), new SqlParameter("@UpperLimit", SqlDbType.Decimal, 9), new SqlParameter("@TaxRate", SqlDbType.Decimal, 9), new SqlParameter("@Deduct", SqlDbType.Decimal, 9), new SqlParameter("@AddDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.TaxRateID;
            commandParameters[1].Value = model.TaxStartPoint;
            commandParameters[2].Value = model.LowerLimit;
            commandParameters[3].Value = model.UpperLimit;
            commandParameters[4].Value = model.TaxRate;
            commandParameters[5].Value = model.Deduct;
            commandParameters[6].Value = model.AddDate;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string TaxRateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from SaM_IncomeTax ");
            builder.Append(" where TaxRateID=@TaxRateID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TaxRateID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = TaxRateID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(List<string> ItemId)
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
                        this.Delete(str);
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

        public object Exists(string TaxRateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from SaM_IncomeTax");
            builder.Append(" where TaxRateID=@TaxRateID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TaxRateID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = TaxRateID;
            return SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
        }

        public object GetInComeTaxStartPoint()
        {
            string cmdText = "select top 1 TaxStartPoint from SaM_IncomeTax ";
            return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TaxRateID,TaxStartPoint,LowerLimit,UpperLimit,TaxRate,Deduct,AddDate ");
            builder.Append(" FROM SaM_IncomeTax ");
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
            builder.Append(" TaxRateID,TaxStartPoint,LowerLimit,UpperLimit,TaxRate,Deduct,AddDate ");
            builder.Append(" FROM SaM_IncomeTax ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public TaxStartPointModel GetModel(string TaxRateID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 TaxRateID,TaxStartPoint,LowerLimit,UpperLimit,TaxRate,Deduct,AddDate from SaM_IncomeTax ");
            builder.Append(" where TaxRateID=@TaxRateID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TaxRateID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = TaxRateID;
            TaxStartPointModel model = new TaxStartPointModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            model.TaxRateID = table.Rows[0]["TaxRateID"].ToString();
            if (table.Rows[0]["TaxStartPoint"].ToString() != "")
            {
                model.TaxStartPoint = decimal.Parse(table.Rows[0]["TaxStartPoint"].ToString());
            }
            if (table.Rows[0]["LowerLimit"].ToString() != "")
            {
                model.LowerLimit = decimal.Parse(table.Rows[0]["LowerLimit"].ToString());
            }
            if (table.Rows[0]["UpperLimit"].ToString() != "")
            {
                model.UpperLimit = decimal.Parse(table.Rows[0]["UpperLimit"].ToString());
            }
            if (table.Rows[0]["TaxRate"].ToString() != "")
            {
                model.TaxRate = decimal.Parse(table.Rows[0]["TaxRate"].ToString());
            }
            if (table.Rows[0]["Deduct"].ToString() != "")
            {
                model.Deduct = new decimal?(decimal.Parse(table.Rows[0]["Deduct"].ToString()));
            }
            if (table.Rows[0]["AddDate"].ToString() != "")
            {
                model.AddDate = new DateTime?(DateTime.Parse(table.Rows[0]["AddDate"].ToString()));
            }
            return model;
        }

        public int SavePersonIncomeTaxStartPoint(decimal point)
        {
            string cmdText = " update SaM_IncomeTax set TaxStartPoint=@TaxStartPoint ";
            SqlParameter parameter = new SqlParameter("@TaxStartPoint", SqlDbType.Decimal) {
                Value = point
            };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public int Update(TaxStartPointModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update SaM_IncomeTax set ");
            builder.Append("TaxStartPoint=@TaxStartPoint,");
            builder.Append("LowerLimit=@LowerLimit,");
            builder.Append("UpperLimit=@UpperLimit,");
            builder.Append("TaxRate=@TaxRate,");
            builder.Append("Deduct=@Deduct,");
            builder.Append("AddDate=@AddDate");
            builder.Append(" where TaxRateID=@TaxRateID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TaxRateID", SqlDbType.NVarChar, 0x40), new SqlParameter("@TaxStartPoint", SqlDbType.Decimal, 9), new SqlParameter("@LowerLimit", SqlDbType.Decimal, 9), new SqlParameter("@UpperLimit", SqlDbType.Decimal, 9), new SqlParameter("@TaxRate", SqlDbType.Decimal, 9), new SqlParameter("@Deduct", SqlDbType.Decimal, 9), new SqlParameter("@AddDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.TaxRateID;
            commandParameters[1].Value = model.TaxStartPoint;
            commandParameters[2].Value = model.LowerLimit;
            commandParameters[3].Value = model.UpperLimit;
            commandParameters[4].Value = model.TaxRate;
            commandParameters[5].Value = model.Deduct;
            commandParameters[6].Value = model.AddDate;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

