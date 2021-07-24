namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class IncometModify
    {
        public int Add(IncometModifyModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Incomet_Modify(");
            builder.Append("ID,ContractID,ChangeCode,ChangeTime,ChangePrice,ChangeReason,Transactor,Annex,Remark,InputPerson,InputDate)");
            builder.Append(" values (");
            builder.Append("@ID,@ContractID,@ChangeCode,@ChangeTime,@ChangePrice,@ChangeReason,@Transactor,@Annex,@Remark,@InputPerson,@InputDate)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ChangeCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ChangeTime", SqlDbType.DateTime), new SqlParameter("@ChangePrice", SqlDbType.Decimal, 9), new SqlParameter("@ChangeReason", SqlDbType.NVarChar, 500), new SqlParameter("@Transactor", SqlDbType.NVarChar, 0x40), new SqlParameter("@Annex", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.ChangeCode;
            commandParameters[3].Value = model.ChangeTime;
            commandParameters[4].Value = model.ChangePrice;
            commandParameters[5].Value = model.ChangeReason;
            commandParameters[6].Value = model.Transactor;
            commandParameters[7].Value = model.Annex;
            commandParameters[8].Value = model.Remark;
            commandParameters[9].Value = model.InputPerson;
            commandParameters[10].Value = model.InputDate;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Incomet_Modify ");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ID;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public void DeleteSheet(string modifyId)
        {
            string cmdText = "DELETE Con_IncomeModify_Technology WHERE ConModifyId = @ConModifyId";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ConModifyId", modifyId) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        public List<IncometModifyModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ContractID,ChangeCode,ChangeTime,ChangePrice,ChangeReason,Transactor,Annex,Remark,InputPerson,InputDate ");
            builder.Append(" FROM Con_Incomet_Modify ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<IncometModifyModel> list = new List<IncometModifyModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public IncometModifyModel GetModel(string ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ContractID,ChangeCode,ChangeTime,ChangePrice,ChangeReason,Transactor,Annex,Remark,InputPerson,InputDate from Con_Incomet_Modify ");
            builder.Append(" where ID=@ID ");
            IncometModifyModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ID", ID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetSheet(string modifyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Con_IncomeModify_Technology").AppendLine();
            builder.Append("WHERE ConModifyId = @ConModifyId").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ConModifyId", modifyId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public string GetSheetNameById(int id)
        {
            string str = "SELECT ItemName FROM Prj_TechnologyManage WHERE ID = @Id";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", id) };
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, str.ToString(), commandParameters));
        }

        public void InsertSheet(string modifyId, string sheetId)
        {
            string cmdText = "INSERT INTO Con_IncomeModify_Technology(ConModifyId, TechnologyId) VALUES(@ConModifyId, @TechnologyId)";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ConModifyId", modifyId), new SqlParameter("@TechnologyId", sheetId) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        public bool IsExistsSheet(string modifyId, string sheetId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM Con_IncomeModify_Technology").AppendLine();
            builder.Append("WHERE ConModifyId = @ConModifyId").AppendLine();
            builder.Append("\tAND TechnologyId = @TechnologyId").AppendLine();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ConModifyId", modifyId), new SqlParameter("@TechnologyId", sheetId) };
            object obj2 = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            return (DBHelper.GetInt(obj2) > 0);
        }

        public DataTable PickSheetByModifyId(string modifyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TM.ID, PrjCode, SerialNumber, ItemName, TM.Remark, BigSort").AppendLine();
            builder.Append("FROM Prj_TechnologyManage AS TM").AppendLine();
            builder.Append("INNER JOIN Con_Incomet_Contract AS IC ON TM.PrjCode = IC.Project").AppendLine();
            builder.Append("INNER JOIN Con_Incomet_Modify AS IM on IC.ContractID = IM.ContractID").AppendLine();
            builder.Append("WHERE IM.ID = @ModifyId").AppendLine();
            builder.Append("\tAND BigSort IN (7, 8)").AppendLine();
            builder.Append("\tAND TM.ID NOT IN(SELECT TechnologyId FROM Con_IncomeModify_Technology)").AppendLine();
            builder.Append("  AND TM.FlowState=1 ").AppendLine();
            builder.Append("UNION").AppendLine();
            builder.Append("SELECT TM.ID, PrjCode, SerialNumber, ItemName, TM.Remark, BigSort").AppendLine();
            builder.Append("FROM Prj_TechnologyManage AS TM").AppendLine();
            builder.Append("INNER JOIN Con_IncomeModify_Technology AS IT on TM.ID = IT.TechnologyId").AppendLine();
            builder.Append("WHERE IT.ConModifyId = @ModifyId").AppendLine();
            builder.Append(" AND TM.FlowState=1 ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModifyId", modifyId) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable PickSheetByPrjGuid(string prjGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Prj_TechnologyManage\r\n");
            builder.Append("WHERE PrjCode = @PrjGuid\r\n");
            builder.Append(" AND (BigSort=7 OR BigSort=8 AND FlowState=1) \r\n");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjGuid", prjGuid) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public IncometModifyModel ReaderBind(IDataReader dataReader)
        {
            IncometModifyModel model = new IncometModifyModel {
                ID = dataReader["ID"].ToString(),
                ContractID = dataReader["ContractID"].ToString(),
                ChangeCode = dataReader["ChangeCode"].ToString()
            };
            object obj2 = dataReader["ChangeTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ChangeTime = new DateTime?((DateTime) obj2);
            }
            obj2 = dataReader["ChangePrice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ChangePrice = new decimal?((decimal) obj2);
            }
            model.ChangeReason = dataReader["ChangeReason"].ToString();
            model.Transactor = dataReader["Transactor"].ToString();
            model.Annex = dataReader["Annex"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            model.InputPerson = dataReader["InputPerson"].ToString();
            obj2 = dataReader["InputDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.InputDate = new DateTime?((DateTime) obj2);
            }
            return model;
        }

        public int Update(IncometModifyModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Incomet_Modify set ");
            builder.Append("ContractID=@ContractID,");
            builder.Append("ChangeCode=@ChangeCode,");
            builder.Append("ChangeTime=@ChangeTime,");
            builder.Append("ChangePrice=@ChangePrice,");
            builder.Append("ChangeReason=@ChangeReason,");
            builder.Append("Transactor=@Transactor,");
            builder.Append("Annex=@Annex,");
            builder.Append("Remark=@Remark,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate");
            builder.Append(" where ID=@ID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ChangeCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@ChangeTime", SqlDbType.DateTime), new SqlParameter("@ChangePrice", SqlDbType.Decimal, 9), new SqlParameter("@ChangeReason", SqlDbType.NVarChar, 500), new SqlParameter("@Transactor", SqlDbType.NVarChar, 0x40), new SqlParameter("@Annex", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.ID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.ChangeCode;
            commandParameters[3].Value = model.ChangeTime;
            commandParameters[4].Value = model.ChangePrice;
            commandParameters[5].Value = model.ChangeReason;
            commandParameters[6].Value = model.Transactor;
            commandParameters[7].Value = model.Annex;
            commandParameters[8].Value = model.Remark;
            commandParameters[9].Value = model.InputPerson;
            commandParameters[10].Value = model.InputDate;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

