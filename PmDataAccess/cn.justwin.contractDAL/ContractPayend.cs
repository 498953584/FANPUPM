namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ContractPayend
    {
        public int Add(ContractPayendModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_ContractPayend(");
            builder.Append("PayendID,ContractID,BWasPerson,ProvisionMatter,ProjectCondition,OtherExplain,Annex,InPerson,InTime,ModifyState,WasPerson,PayendTopics)");
            builder.Append(" values (");
            builder.Append("@PayendID,@ContractID,@BWasPerson,@ProvisionMatter,@ProjectCondition,@OtherExplain,@Annex,@InPerson,@InTime,@ModifyState,@WasPerson,@PayendTopics)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PayendID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@BWasPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@ProvisionMatter", SqlDbType.NVarChar), new SqlParameter("@ProjectCondition", SqlDbType.NVarChar), new SqlParameter("@OtherExplain", SqlDbType.NVarChar), new SqlParameter("@Annex", SqlDbType.NVarChar, 200), new SqlParameter("@InPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InTime", SqlDbType.DateTime), new SqlParameter("@ModifyState", SqlDbType.NVarChar, 5), new SqlParameter("@WasPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayendTopics", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = model.PayendID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.BWasPerson;
            commandParameters[3].Value = model.ProvisionMatter;
            commandParameters[4].Value = model.ProjectCondition;
            commandParameters[5].Value = model.OtherExplain;
            commandParameters[6].Value = model.Annex;
            commandParameters[7].Value = model.InPerson;
            commandParameters[8].Value = model.InTime;
            commandParameters[9].Value = model.ModifyState;
            commandParameters[10].Value = model.WasPerson;
            commandParameters[11].Value = model.PayendTopics;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string PayendID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_ContractPayend ");
            builder.Append(" where PayendID=@PayendID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PayendID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = PayendID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByWhere(string where)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_ContractPayend ");
            builder.Append(where);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<ContractPayendModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PayendID,ContractID,BWasPerson,ProvisionMatter,ProjectCondition,OtherExplain,Annex,InPerson,InTime,ModifyState,WasPerson,PayendTopics ");
            builder.Append(" FROM Con_ContractPayend ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<ContractPayendModel> list = new List<ContractPayendModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public ContractPayendModel GetModel(string PayendID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select PayendID,ContractID,BWasPerson,ProvisionMatter,ProjectCondition,OtherExplain,Annex,InPerson,InTime,ModifyState,WasPerson,PayendTopics from Con_ContractPayend ");
            builder.Append(" where PayendID=@PayendID ");
            ContractPayendModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@PayendID", PayendID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public ContractPayendModel ReaderBind(IDataReader dataReader)
        {
            ContractPayendModel model = new ContractPayendModel {
                PayendID = dataReader["PayendID"].ToString(),
                ContractID = dataReader["ContractID"].ToString(),
                BWasPerson = dataReader["BWasPerson"].ToString(),
                ProvisionMatter = dataReader["ProvisionMatter"].ToString(),
                ProjectCondition = dataReader["ProjectCondition"].ToString(),
                OtherExplain = dataReader["OtherExplain"].ToString(),
                Annex = dataReader["Annex"].ToString(),
                InPerson = dataReader["InPerson"].ToString()
            };
            object obj2 = dataReader["InTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.InTime = new DateTime?((DateTime) obj2);
            }
            model.ModifyState = dataReader["ModifyState"].ToString();
            model.WasPerson = dataReader["WasPerson"].ToString();
            model.PayendTopics = dataReader["PayendTopics"].ToString();
            return model;
        }

        public int Update(ContractPayendModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_ContractPayend set ");
            builder.Append("ContractID=@ContractID,");
            builder.Append("BWasPerson=@BWasPerson,");
            builder.Append("ProvisionMatter=@ProvisionMatter,");
            builder.Append("ProjectCondition=@ProjectCondition,");
            builder.Append("OtherExplain=@OtherExplain,");
            builder.Append("Annex=@Annex,");
            builder.Append("InPerson=@InPerson,");
            builder.Append("InTime=@InTime,");
            builder.Append("ModifyState=@ModifyState,");
            builder.Append("WasPerson=@WasPerson,");
            builder.Append("PayendTopics=@PayendTopics");
            builder.Append(" where PayendID=@PayendID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PayendID", SqlDbType.NVarChar, 0x40), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@BWasPerson", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@ProvisionMatter", SqlDbType.NVarChar), new SqlParameter("@ProjectCondition", SqlDbType.NVarChar), new SqlParameter("@OtherExplain", SqlDbType.NVarChar), new SqlParameter("@Annex", SqlDbType.NVarChar, 200), new SqlParameter("@InPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@InTime", SqlDbType.DateTime), new SqlParameter("@ModifyState", SqlDbType.NVarChar, 5), new SqlParameter("@WasPerson", SqlDbType.NVarChar, 0x40), new SqlParameter("@PayendTopics", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = model.PayendID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.BWasPerson;
            commandParameters[3].Value = model.ProvisionMatter;
            commandParameters[4].Value = model.ProjectCondition;
            commandParameters[5].Value = model.OtherExplain;
            commandParameters[6].Value = model.Annex;
            commandParameters[7].Value = model.InPerson;
            commandParameters[8].Value = model.InTime;
            commandParameters[9].Value = model.ModifyState;
            commandParameters[10].Value = model.WasPerson;
            commandParameters[11].Value = model.PayendTopics;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

