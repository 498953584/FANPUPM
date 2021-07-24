namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class IncomentPaymentApply
    {
        public void Add(IncomentPaymentApplyModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Income_PaymentApply(");
            builder.Append("PaymentId,PaymentCode,ContractId,PaymentPenson,PaymentAmount,PaymentDate,PaymentMode,FlowState,Notes,InputPerson,InputDate,ContainPending)");
            builder.Append(" values (");
            builder.Append("@PaymentId,@PaymentCode,@ContractId,@PaymentPenson,@PaymentAmount,@PaymentDate,@PaymentMode,@FlowState,@Notes,@InputPerson,@InputDate,@ContainPending)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PaymentId", SqlDbType.NVarChar, 0x40), new SqlParameter("@PaymentCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractId", SqlDbType.NVarChar, 0x40), new SqlParameter("@PaymentPenson", SqlDbType.NVarChar, 0x40), new SqlParameter("@PaymentAmount", SqlDbType.Decimal, 9), new SqlParameter("@PaymentDate", SqlDbType.DateTime), new SqlParameter("@PaymentMode", SqlDbType.NVarChar, 20), new SqlParameter("@FlowState", SqlDbType.Int), new SqlParameter("@Notes", SqlDbType.Text), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@ContainPending", SqlDbType.Bit) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.Code;
            commandParameters[2].Value = model.ContractId;
            commandParameters[3].Value = model.PaymentPenson;
            commandParameters[4].Value = model.PaymentAmount;
            commandParameters[5].Value = model.PaymentDate;
            commandParameters[6].Value = model.PaymentMode;
            commandParameters[7].Value = model.FlowState;
            commandParameters[8].Value = model.Notes;
            commandParameters[9].Value = model.InputPerson;
            commandParameters[10].Value = model.InputDate;
            commandParameters[11].Value = model.ContainPending;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void Delete(List<string> paymentIds)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in paymentIds)
                    {
                        this.Delete(str, trans);
                    }
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new Exception("删除失败");
                }
            }
        }

        public void Delete(string id, SqlTransaction trans)
        {
            string str = "DELETE Con_Income_PaymentApply WHERE PaymentId=@PaymentId";
            SqlParameter parameter = new SqlParameter("@PaymentId", SqlDbType.NVarChar, 0x40) {
                Value = id
            };
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
            }
        }

        public List<IncomentPaymentApplyModel> GetByContractId(string contractId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT PaymentId,PaymentCode,ContractId,PaymentPenson,PaymentAmount,PaymentDate,PaymentMode,FlowState,Notes,InputPerson,InputDate,ContainPending ");
            builder.Append("FROM Con_Income_PaymentApply WHERE ContractId=@ContractId");
            SqlParameter parameter = new SqlParameter("@ContractId", SqlDbType.NVarChar, 0x40) {
                Value = contractId
            };
            List<IncomentPaymentApplyModel> list = new List<IncomentPaymentApplyModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter }))
            {
                while (reader.Read())
                {
                    IncomentPaymentApplyModel item = new IncomentPaymentApplyModel {
                        Id = reader["PaymentId"].ToString(),
                        Code = reader["PaymentCode"].ToString(),
                        ContractId = reader["ContractId"].ToString(),
                        PaymentPenson = reader["PaymentPenson"].ToString(),
                        PaymentAmount = Convert.ToDecimal(reader["PaymentAmount"].ToString()),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"].ToString()),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        FlowState = Convert.ToInt32(reader["FlowState"]),
                        Notes = reader["Notes"].ToString(),
                        InputPerson = reader["InputPerson"].ToString(),
                        InputDate = Convert.ToDateTime(reader["InputDate"].ToString()),
                        ContainPending = Convert.ToBoolean(reader["ContainPending"].ToString())
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public IncomentPaymentApplyModel GetById(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT PaymentId,PaymentCode,ContractId,PaymentPenson,PaymentAmount,PaymentDate,PaymentMode,FlowState,Notes,InputPerson,InputDate,ContainPending ");
            builder.Append("FROM Con_Income_PaymentApply WHERE PaymentId=@PaymentId");
            SqlParameter parameter = new SqlParameter("@PaymentId", SqlDbType.NVarChar, 0x40) {
                Value = id
            };
            IncomentPaymentApplyModel model = new IncomentPaymentApplyModel();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter }))
            {
                if (reader.Read())
                {
                    model.Id = reader["PaymentId"].ToString();
                    model.Code = reader["PaymentCode"].ToString();
                    model.ContractId = reader["ContractId"].ToString();
                    model.PaymentPenson = reader["PaymentPenson"].ToString();
                    model.PaymentAmount = Convert.ToDecimal(reader["PaymentAmount"].ToString());
                    model.PaymentDate = Convert.ToDateTime(reader["PaymentDate"].ToString());
                    model.PaymentMode = reader["PaymentMode"].ToString();
                    model.FlowState = Convert.ToInt32(reader["FlowState"]);
                    model.Notes = reader["Notes"].ToString();
                    model.InputPerson = reader["InputPerson"].ToString();
                    model.InputDate = Convert.ToDateTime(reader["InputDate"].ToString());
                    model.ContainPending = Convert.ToBoolean(reader["ContainPending"].ToString());
                }
            }
            return model;
        }

        public List<IncomentPaymentApplyModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT PaymentId,PaymentCode,Con_Income_PaymentApply.ContractId,PaymentPenson,PaymentAmount,PaymentDate,PaymentMode,Con_Income_PaymentApply.FlowState,Notes,InputPerson,InputDate");
            builder.Append(",ContainPending FROM Con_Income_PaymentApply ");
            builder.Append("LEFT JOIN Con_Incomet_Contract ON Con_Incomet_Contract.ContractID = Con_Income_PaymentApply.ContractID ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append(" WHERE ").Append(strWhere);
            }
            List<IncomentPaymentApplyModel> list = new List<IncomentPaymentApplyModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    IncomentPaymentApplyModel item = new IncomentPaymentApplyModel {
                        Id = reader["PaymentId"].ToString(),
                        Code = reader["PaymentCode"].ToString(),
                        ContractId = reader["ContractId"].ToString(),
                        PaymentPenson = reader["PaymentPenson"].ToString(),
                        PaymentAmount = Convert.ToDecimal(reader["PaymentAmount"].ToString()),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"].ToString()),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        FlowState = Convert.ToInt32(reader["FlowState"]),
                        Notes = reader["Notes"].ToString(),
                        InputPerson = reader["InputPerson"].ToString(),
                        InputDate = Convert.ToDateTime(reader["InputDate"].ToString()),
                        ContainPending = Convert.ToBoolean(reader["ContainPending"].ToString())
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        public bool IsExists(string PaymentCode, string ContractID)
        {
            string cmdText = "SELECT COUNT(1) FROM Con_Income_PaymentApply WHERE PaymentCode=@PaymentCode AND ContractID=@ContractID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PaymentCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = PaymentCode;
            commandParameters[1].Value = ContractID;
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, commandParameters)) > 0);
        }

        public void Update(IncomentPaymentApplyModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE Con_Income_PaymentApply SET PaymentCode=@PaymentCode,PaymentPenson=@PaymentPenson,PaymentAmount=@PaymentAmount,PaymentDate=@PaymentDate,");
            builder.Append("PaymentMode=@PaymentMode,FlowState=@FlowState,Notes=@Notes,ContainPending=@ContainPending WHERE PaymentId=@PaymentId");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PaymentCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@PaymentPenson", SqlDbType.NVarChar, 0x40), new SqlParameter("@PaymentAmount", SqlDbType.Decimal, 9), new SqlParameter("@PaymentDate", SqlDbType.DateTime), new SqlParameter("@PaymentMode", SqlDbType.NVarChar, 20), new SqlParameter("@FlowState", SqlDbType.Int), new SqlParameter("@Notes", SqlDbType.Text), new SqlParameter("@ContainPending", SqlDbType.Bit), new SqlParameter("@PaymentId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.Code;
            commandParameters[1].Value = model.PaymentPenson;
            commandParameters[2].Value = model.PaymentAmount;
            commandParameters[3].Value = model.PaymentDate;
            commandParameters[4].Value = model.PaymentMode;
            commandParameters[5].Value = model.FlowState;
            commandParameters[6].Value = model.Notes;
            commandParameters[7].Value = model.ContainPending;
            commandParameters[8].Value = model.Id;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

