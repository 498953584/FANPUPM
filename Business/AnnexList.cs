using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public class AnnexList
{
    public int Add(AnnexListModel model)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("insert into XPM_Basic_AnnexList(");
        builder.Append("AnnexCode,ModuleID,RecordCode,AnnexType,FileCode,AnnexName,OriginalName,FilePath,FileSize,AddDate,State,HumanCode,Remark,ProjectUniqueCode)");
        builder.Append(" values (");
        builder.Append("@AnnexCode,@ModuleID,@RecordCode,@AnnexType,@FileCode,@AnnexName,@OriginalName,@FilePath,@FileSize,@AddDate,@State,@HumanCode,@Remark,@ProjectUniqueCode)");
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AnnexCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@ModuleID", SqlDbType.Int, 4), new SqlParameter("@RecordCode", SqlDbType.NVarChar, 0x24), new SqlParameter("@AnnexType", SqlDbType.Int, 4), new SqlParameter("@FileCode", SqlDbType.NVarChar, 50), new SqlParameter("@AnnexName", SqlDbType.NVarChar, 80), new SqlParameter("@OriginalName", SqlDbType.NVarChar, 100), new SqlParameter("@FilePath", SqlDbType.NVarChar, 100), new SqlParameter("@FileSize", SqlDbType.Int, 4), new SqlParameter("@AddDate", SqlDbType.DateTime), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@HumanCode", SqlDbType.VarChar, 10), new SqlParameter("@Remark", SqlDbType.VarChar, 200), new SqlParameter("@ProjectUniqueCode", SqlDbType.VarChar, 50) };
        commandParameters[0].Value = model.AnnexCode;
        commandParameters[1].Value = model.ModuleID;
        commandParameters[2].Value = model.RecordCode;
        commandParameters[3].Value = model.AnnexType;
        commandParameters[4].Value = model.FileCode;
        commandParameters[5].Value = model.AnnexName;
        commandParameters[6].Value = model.OriginalName;
        commandParameters[7].Value = model.FilePath;
        commandParameters[8].Value = model.FileSize;
        commandParameters[9].Value = model.AddDate;
        commandParameters[10].Value = model.State;
        commandParameters[11].Value = model.HumanCode;
        commandParameters[12].Value = model.Remark;
        commandParameters[13].Value = model.ProjectUniqueCode;
        return publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
    }

    public int Delete(Guid AnnexCode)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("delete from XPM_Basic_AnnexList ");
        builder.Append(" where AnnexCode=@AnnexCode ");
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AnnexCode", SqlDbType.UniqueIdentifier) };
        commandParameters[0].Value = AnnexCode;
        return publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
    }

    public List<AnnexListModel> GetListArray(string strWhere)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("select AnnexCode,ModuleID,RecordCode,AnnexType,FileCode,AnnexName,OriginalName,FilePath,FileSize,AddDate,State,HumanCode,Remark,ProjectUniqueCode ");
        builder.Append(" FROM XPM_Basic_AnnexList ");
        if (strWhere.Trim() != "")
        {
            builder.Append(strWhere);
        }
        List<AnnexListModel> list = new List<AnnexListModel>();
        using (IDataReader reader = publicDbOpClass.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
        {
            while (reader.Read())
            {
                list.Add(this.ReaderBind(reader));
            }
        }
        return list;
    }

    public AnnexListModel GetModel(Guid AnnexCode)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("select AnnexCode,ModuleID,RecordCode,AnnexType,FileCode,AnnexName,OriginalName,FilePath,FileSize,AddDate,State,HumanCode,Remark,ProjectUniqueCode from XPM_Basic_AnnexList ");
        builder.Append(" where AnnexCode=@AnnexCode ");
        AnnexListModel model = null;
        using (IDataReader reader = publicDbOpClass.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@AnnexCode", AnnexCode) }))
        {
            if (reader.Read())
            {
                model = this.ReaderBind(reader);
            }
        }
        return model;
    }

    public AnnexListModel ReaderBind(IDataReader dataReader)
    {
        AnnexListModel model = new AnnexListModel();
        object obj2 = dataReader["AnnexCode"];
        if ((obj2 != null) && (obj2 != DBNull.Value))
        {
            model.AnnexCode = new Guid(obj2.ToString());
        }
        obj2 = dataReader["ModuleID"];
        if ((obj2 != null) && (obj2 != DBNull.Value))
        {
            model.ModuleID = (int) obj2;
        }
        model.RecordCode = dataReader["RecordCode"].ToString();
        obj2 = dataReader["AnnexType"];
        if ((obj2 != null) && (obj2 != DBNull.Value))
        {
            model.AnnexType = (int) obj2;
        }
        model.FileCode = dataReader["FileCode"].ToString();
        model.AnnexName = dataReader["AnnexName"].ToString();
        model.OriginalName = dataReader["OriginalName"].ToString();
        model.FilePath = dataReader["FilePath"].ToString();
        obj2 = dataReader["FileSize"];
        if ((obj2 != null) && (obj2 != DBNull.Value))
        {
            model.FileSize = (int) obj2;
        }
        obj2 = dataReader["AddDate"];
        if ((obj2 != null) && (obj2 != DBNull.Value))
        {
            model.AddDate = (DateTime) obj2;
        }
        obj2 = dataReader["State"];
        if ((obj2 != null) && (obj2 != DBNull.Value))
        {
            model.State = (int) obj2;
        }
        model.HumanCode = dataReader["HumanCode"].ToString();
        model.Remark = dataReader["Remark"].ToString();
        model.ProjectUniqueCode = dataReader["ProjectUniqueCode"].ToString();
        return model;
    }

    public int Update(AnnexListModel model)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("update XPM_Basic_AnnexList set ");
        builder.Append("ModuleID=@ModuleID,");
        builder.Append("RecordCode=@RecordCode,");
        builder.Append("AnnexType=@AnnexType,");
        builder.Append("FileCode=@FileCode,");
        builder.Append("AnnexName=@AnnexName,");
        builder.Append("OriginalName=@OriginalName,");
        builder.Append("FilePath=@FilePath,");
        builder.Append("FileSize=@FileSize,");
        builder.Append("AddDate=@AddDate,");
        builder.Append("State=@State,");
        builder.Append("HumanCode=@HumanCode,");
        builder.Append("Remark=@Remark,");
        builder.Append("ProjectUniqueCode=@ProjectUniqueCode");
        builder.Append(" where AnnexCode=@AnnexCode ");
        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AnnexCode", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@ModuleID", SqlDbType.Int, 4), new SqlParameter("@RecordCode", SqlDbType.NVarChar, 0x24), new SqlParameter("@AnnexType", SqlDbType.Int, 4), new SqlParameter("@FileCode", SqlDbType.NVarChar, 50), new SqlParameter("@AnnexName", SqlDbType.NVarChar, 80), new SqlParameter("@OriginalName", SqlDbType.NVarChar, 100), new SqlParameter("@FilePath", SqlDbType.NVarChar, 100), new SqlParameter("@FileSize", SqlDbType.Int, 4), new SqlParameter("@AddDate", SqlDbType.DateTime), new SqlParameter("@State", SqlDbType.Int, 4), new SqlParameter("@HumanCode", SqlDbType.VarChar, 10), new SqlParameter("@Remark", SqlDbType.VarChar, 200), new SqlParameter("@ProjectUniqueCode", SqlDbType.VarChar, 50) };
        commandParameters[0].Value = model.AnnexCode;
        commandParameters[1].Value = model.ModuleID;
        commandParameters[2].Value = model.RecordCode;
        commandParameters[3].Value = model.AnnexType;
        commandParameters[4].Value = model.FileCode;
        commandParameters[5].Value = model.AnnexName;
        commandParameters[6].Value = model.OriginalName;
        commandParameters[7].Value = model.FilePath;
        commandParameters[8].Value = model.FileSize;
        commandParameters[9].Value = model.AddDate;
        commandParameters[10].Value = model.State;
        commandParameters[11].Value = model.HumanCode;
        commandParameters[12].Value = model.Remark;
        commandParameters[13].Value = model.ProjectUniqueCode;
        return publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
    }
}

