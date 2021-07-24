namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AnnexSettings
    {
        public int Add(AnnexSettingsModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into XPM_Basic_AnnexSettings(");
            builder.Append("ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber,Remark)");
            builder.Append(" values (");
            builder.Append("@ModuleID,@ModuleCode,@ModuleName,@FileSize,@ExtName,@FilePath,@FileNumber,@Remark)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModuleID", SqlDbType.Int, 4), new SqlParameter("@ModuleCode", SqlDbType.VarChar, 50), new SqlParameter("@ModuleName", SqlDbType.NVarChar, 50), new SqlParameter("@FileSize", SqlDbType.Int, 4), new SqlParameter("@ExtName", SqlDbType.VarChar, 100), new SqlParameter("@FilePath", SqlDbType.VarChar, 100), new SqlParameter("@FileNumber", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = model.ModuleID;
            commandParameters[1].Value = model.ModuleCode;
            commandParameters[2].Value = model.ModuleName;
            commandParameters[3].Value = model.FileSize;
            commandParameters[4].Value = model.ExtName;
            commandParameters[5].Value = model.FilePath;
            commandParameters[6].Value = model.FileNumber;
            commandParameters[7].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(int ModuleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from XPM_Basic_AnnexSettings ");
            builder.Append(" where ModuleID=@ModuleID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModuleID", SqlDbType.Int, 4) };
            commandParameters[0].Value = ModuleID;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<AnnexSettingsModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber,Remark ");
            builder.Append(" FROM XPM_Basic_AnnexSettings ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<AnnexSettingsModel> list = new List<AnnexSettingsModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public AnnexSettingsModel GetModel(int ModuleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber,Remark from XPM_Basic_AnnexSettings ");
            builder.Append(" where ModuleID=@ModuleID ");
            AnnexSettingsModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ModuleID", ModuleID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public AnnexSettingsModel ReaderBind(IDataReader dataReader)
        {
            AnnexSettingsModel model = new AnnexSettingsModel();
            object obj2 = dataReader["ModuleID"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ModuleID = (int) obj2;
            }
            model.ModuleCode = dataReader["ModuleCode"].ToString();
            model.ModuleName = dataReader["ModuleName"].ToString();
            obj2 = dataReader["FileSize"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.FileSize = (int) obj2;
            }
            model.ExtName = dataReader["ExtName"].ToString();
            model.FilePath = dataReader["FilePath"].ToString();
            obj2 = dataReader["FileNumber"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.FileNumber = (int) obj2;
            }
            model.Remark = dataReader["Remark"].ToString();
            return model;
        }

        public int Update(AnnexSettingsModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update XPM_Basic_AnnexSettings set ");
            builder.Append("ModuleCode=@ModuleCode,");
            builder.Append("ModuleName=@ModuleName,");
            builder.Append("FileSize=@FileSize,");
            builder.Append("ExtName=@ExtName,");
            builder.Append("FilePath=@FilePath,");
            builder.Append("FileNumber=@FileNumber,");
            builder.Append("Remark=@Remark");
            builder.Append(" where ModuleID=@ModuleID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModuleID", SqlDbType.Int, 4), new SqlParameter("@ModuleCode", SqlDbType.VarChar, 50), new SqlParameter("@ModuleName", SqlDbType.NVarChar, 50), new SqlParameter("@FileSize", SqlDbType.Int, 4), new SqlParameter("@ExtName", SqlDbType.VarChar, 100), new SqlParameter("@FilePath", SqlDbType.VarChar, 100), new SqlParameter("@FileNumber", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = model.ModuleID;
            commandParameters[1].Value = model.ModuleCode;
            commandParameters[2].Value = model.ModuleName;
            commandParameters[3].Value = model.FileSize;
            commandParameters[4].Value = model.ExtName;
            commandParameters[5].Value = model.FilePath;
            commandParameters[6].Value = model.FileNumber;
            commandParameters[7].Value = model.Remark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

