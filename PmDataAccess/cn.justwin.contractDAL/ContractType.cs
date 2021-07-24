namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ContractType
    {
        public void Add(SqlTransaction trans, ContractTypeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_ContractType(");
            builder.Append("TypeID,TypeCode,TypeName,UserCodes,Notes,InputPerson,InputDate,CBSCode,TypeShort)");
            builder.Append(" values (");
            builder.Append("@TypeID,@TypeCode,@TypeName,@UserCodes,@Notes,@InputPerson,@InputDate,@CBSCode,@TypeShort)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeID", SqlDbType.NVarChar, 0x40), new SqlParameter("@TypeCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@TypeName", SqlDbType.NVarChar, 0x80), new SqlParameter("@UserCodes", SqlDbType.NVarChar), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@CBSCode", SqlDbType.NVarChar, 200), new SqlParameter("@TypeShort", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = model.TypeID;
            commandParameters[1].Value = model.TypeCode;
            commandParameters[2].Value = model.TypeName;
            commandParameters[3].Value = model.UserCodes;
            commandParameters[4].Value = model.Notes;
            commandParameters[5].Value = model.InputPerson;
            commandParameters[6].Value = model.InputDate;
            if (string.IsNullOrEmpty(model.CBSCode))
            {
                commandParameters[7].Value = DBNull.Value;
            }
            else
            {
                commandParameters[7].Value = model.CBSCode;
            }
            commandParameters[8].Value = model.TypeShort;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public void Delete(string[] typeIDs)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    foreach (string str in typeIDs)
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

        public void Delete(string TypeID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_ContractType ");
            builder.Append(" where TypeID=@TypeID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = TypeID;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public DataTable GetCBSCode()
        {
            string str = " SELECT CBSCode,[Name] FROM Bud_CostAccounting WHERE LEN(CBSCode)=9 ORDER BY CBSCode ";
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), new SqlParameter[0]);
        }

        public static string GetCBSCodeName(string CBSCode)
        {
            if (!string.IsNullOrEmpty(CBSCode))
            {
                string cmdText = string.Format(" SELECT [Name] FROM Bud_CostAccounting WHERE CBSCode='{0}'", CBSCode);
                return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]).Rows[0][0].ToString();
            }
            return "";
        }

        public int GetCount(string typeCode, string typeName, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.AppendFormat(" AND UserCodes LIKE '%{0}%' ", userCode);
            }
            if (!string.IsNullOrEmpty(typeCode))
            {
                builder.AppendFormat(" AND TypeCode LIKE '%{0}%' ", typeCode);
            }
            if (!string.IsNullOrEmpty(typeName))
            {
                builder.AppendFormat(" AND TypeName LIKE '%{0}%' ", typeName);
            }
            string cmdText = "SELECT COUNT(*) FROM Con_ContractType WHERE 1 = 1 " + builder.ToString();
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]));
        }

        public int GetCount(string TypeCode, string TypeName, string userCode, bool? IsValid)
        {
            userCode = "\"" + userCode + "\"";
            string str = "";
            string str2 = "";
            if (IsValid == true)
            {
                str2 = " and (IsValid is null or IsValid='true') ";
            }
            else if (IsValid == false)
            {
                str2 = " and (IsValid='false') ";
            }
            if ((TypeCode == "") && (TypeName != ""))
            {
                str = string.Format(" and TypeName like '%{0}%' ", TypeName);
            }
            else if ((TypeCode != "") && (TypeName == ""))
            {
                str = string.Format(" and TypeCode like '%{0}%' ", TypeCode);
            }
            else if ((TypeCode != "") && (TypeName != ""))
            {
                str = string.Format(" and TypeCode like '%{0}%' and TypeName like '%{1}%' ", TypeCode, TypeName);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT count(*) FROM Con_ContractType where UserCodes like '%{0}%' {1} {2}", userCode, str2, str).AppendLine();
            return DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null));
        }

        private List<ContractTypeModel> GetList(IDataReader dr)
        {
            List<ContractTypeModel> list = new List<ContractTypeModel>();
            while (dr.Read())
            {
                list.Add(this.GetModel(dr));
            }
            return list;
        }

        public List<ContractTypeModel> GetList(string typeCode, string typeName, int pageIndex, int pageSize, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(userCode))
            {
                builder.AppendFormat(" AND UserCodes LIKE '%{0}%' \n", userCode);
            }
            if (!string.IsNullOrEmpty(typeCode))
            {
                builder.AppendFormat(" AND TypeCode LIKE '%{0}%' \n", typeCode);
            }
            if (!string.IsNullOrEmpty(typeName))
            {
                builder.AppendFormat(" AND TypeName LIKE '%{0}%' \n", typeName);
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("SELECT TOP (@PageSize) * FROM \n");
            builder2.Append("( \n");
            builder2.Append("\tSELECT Row_Number() OVER (ORDER BY InputDate DESC) as Num,TypeId,TypeCode, \n");
            builder2.Append("\t\tTypeName,UserCodes,Notes,InputPerson,InputDate,IsValid,CBSCode,TypeShort \n");
            builder2.Append("\tFROM Con_ContractType \n");
            builder2.Append("\tWHERE 1 = 1 \n").Append(builder);
            builder2.Append(") T \n");
            builder2.Append("WHERE Num > (@PageIndex-1)*@PageSize \n");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PageSize", pageSize), new SqlParameter("@PageIndex", pageIndex) };
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder2.ToString(), commandParameters))
            {
                return this.GetList(reader);
            }
        }

        public List<ContractTypeModel> GetList(string TypeCode, string TypeName, int pageIndex, int pageSize, string userCode, bool? IsValid)
        {
            userCode = "\"" + userCode + "\"";
            string str = "";
            string str2 = "";
            if (IsValid == true)
            {
                str2 = " and (IsValid is null or IsValid='true') ";
            }
            else if (IsValid == false)
            {
                str2 = " and (IsValid='false') ";
            }
            if ((TypeCode == "") && (TypeName != ""))
            {
                str = string.Format(" and TypeName like '%{0}%' ", TypeName);
            }
            else if ((TypeCode != "") && (TypeName == ""))
            {
                str = string.Format(" and TypeCode like '%{0}%' ", TypeCode);
            }
            else if ((TypeCode != "") && (TypeName != ""))
            {
                str = string.Format(" and TypeCode like '%{0}%' and TypeName like '%{1}%' ", TypeCode, TypeName);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT TOP ({0}) * FROM ", pageSize).AppendLine();
            builder.Append("(SELECT Row_Number()over(order by InputDate DESC) as Num,TypeId,TypeCode,TypeName,UserCodes,Notes,InputPerson,InputDate,IsValid,CBSCode,TypeShort FROM Con_ContractType ").AppendLine();
            builder.AppendFormat("where UserCodes like '%{0}%' {1} {2} ) ContractType where Num>{3}*({4}-1) ", new object[] { userCode, str2, str, pageSize, pageIndex }).AppendLine();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                return this.GetList(reader);
            }
        }

        public List<ContractTypeModel> GetListByCBSCode(string CBSCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Con_ContractType ");
            builder.Append(" WHERE CBSCode=@CBSCode");
            SqlParameter parameter = new SqlParameter("@CBSCode", CBSCode);
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter }))
            {
                return this.GetList(reader);
            }
        }

        private ContractTypeModel GetModel(IDataReader dr)
        {
            ContractTypeModel model = new ContractTypeModel {
                TypeID = DBHelper.GetString(dr["TypeID"]),
                TypeCode = DBHelper.GetString(dr["TypeCode"]),
                TypeName = DBHelper.GetString(dr["TypeName"]),
                UserCodes = DBHelper.GetString(dr["UserCodes"]),
                Notes = DBHelper.GetString(dr["Notes"]),
                InputPerson = DBHelper.GetString(dr["InputPerson"]),
                InputDate = new DateTime?(DBHelper.GetDateTime(dr["InputDate"])),
                CBSCode = DBHelper.GetString(dr["CBSCode"]),
                TypeShort = DBHelper.GetString(dr["TypeShort"])
            };
            if (dr["IsValid"] != null)
            {
                if (dr["IsValid"].ToString() == "False")
                {
                    model.IsValid = "无效";
                    return model;
                }
                model.IsValid = "有效";
                return model;
            }
            model.IsValid = "有效";
            return model;
        }

        public ContractTypeModel GetModel(string TypeID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM Con_ContractType ");
            builder.Append(" WHERE TypeID=@TypeID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = TypeID;
            ContractTypeModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), commandParameters))
            {
                while (reader.Read())
                {
                    model = this.GetModel(reader);
                }
                return model;
            }
        }

        public void Update(ContractTypeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_ContractType set ");
            builder.Append("TypeCode=@TypeCode,");
            builder.Append("TypeName=@TypeName,");
            builder.Append("UserCodes=@UserCodes,");
            builder.Append("Notes=@Notes,");
            builder.Append("InputPerson=@InputPerson,");
            builder.Append("InputDate=@InputDate,");
            builder.Append("CBSCode=@CBSCode,");
            builder.Append("TypeShort=@TypeShort");
            builder.Append(" where TypeID=@TypeID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeID", SqlDbType.NVarChar, 0x40), new SqlParameter("@TypeCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@TypeName", SqlDbType.NVarChar, 0x80), new SqlParameter("@UserCodes", SqlDbType.NVarChar), new SqlParameter("@Notes", SqlDbType.NVarChar), new SqlParameter("@InputPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InputDate", SqlDbType.DateTime), new SqlParameter("@CBSCode", SqlDbType.NVarChar, 200), new SqlParameter("@TypeShort", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = model.TypeID;
            commandParameters[1].Value = model.TypeCode;
            commandParameters[2].Value = model.TypeName;
            commandParameters[3].Value = model.UserCodes;
            commandParameters[4].Value = model.Notes;
            commandParameters[5].Value = model.InputPerson;
            commandParameters[6].Value = model.InputDate;
            if (!string.IsNullOrEmpty(model.CBSCode))
            {
                commandParameters[7].Value = model.CBSCode;
            }
            else
            {
                commandParameters[7].Value = DBNull.Value;
            }
            commandParameters[8].Value = model.TypeShort;
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void UpdateCBSCode(SqlTransaction trans, ContractTypeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_ContractType set ");
            builder.Append("CBSCode=@CBSCode");
            builder.Append(" where TypeID=@TypeID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeID", SqlDbType.NVarChar, 0x40), new SqlParameter("@CBSCode", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = model.TypeID;
            if (!string.IsNullOrEmpty(model.CBSCode))
            {
                commandParameters[1].Value = model.CBSCode;
            }
            else
            {
                commandParameters[1].Value = DBNull.Value;
            }
            if (trans != null)
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
        }

        public void UpdateTypeValid(string TypeIds, string IsValid)
        {
            if (IsValid == "toValid")
            {
                IsValid = "true";
            }
            else
            {
                IsValid = "false";
            }
            string cmdText = string.Format("update Con_ContractType set IsValid='{0}' where TypeId in({1})", IsValid, TypeIds);
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
        }

        public void UpdateValid(string[] TypeIds, string IsValid)
        {
            string str = "";
            if (TypeIds.Length > 0)
            {
                foreach (string str2 in TypeIds)
                {
                    str = str + "'" + str2 + "',";
                }
                str = str.Remove(str.Length - 1);
            }
            else
            {
                str = "''";
            }
            if (IsValid == "toValid")
            {
                IsValid = "true";
            }
            else
            {
                IsValid = "false";
            }
            string cmdText = string.Format("update Con_ContractType set IsValid='{0}' where TypeId in({1})", IsValid, str);
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, null);
        }
    }
}

