namespace cn.justwin.fileInfoDal
{
    using cn.justwin.DAL;
    using cn.justwin.fileInfoModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class FileType
    {
        public int Add(FileTypeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into F_FileType(");
            builder.Append("TypeId,TypeName,TypeImg,TypeSuffix)");
            builder.Append(" values (");
            builder.Append("@TypeId,@TypeName,@TypeImg,@TypeSuffix)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.NVarChar, 0x40), new SqlParameter("@TypeName", SqlDbType.NVarChar, 0x40), new SqlParameter("@TypeImg", SqlDbType.NVarChar, 200), new SqlParameter("@TypeSuffix", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.TypeId;
            commandParameters[1].Value = model.TypeName;
            commandParameters[2].Value = model.TypeImg;
            commandParameters[3].Value = model.TypeSuffix;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from F_FileType ");
            builder.Append(" where TypeId=@TypeId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = TypeId;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public List<FileTypeModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeId,TypeName,TypeImg,TypeSuffix ");
            builder.Append(" FROM F_FileType ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<FileTypeModel> list = new List<FileTypeModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public FileTypeModel GetModel(string TypeId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TypeId,TypeName,TypeImg,TypeSuffix from F_FileType ");
            builder.Append(" where TypeId=@TypeId ");
            FileTypeModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@TypeId", TypeId) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public FileTypeModel ReaderBind(IDataReader dataReader)
        {
            return new FileTypeModel { TypeId = dataReader["TypeId"].ToString(), TypeName = dataReader["TypeName"].ToString(), TypeImg = dataReader["TypeImg"].ToString(), TypeSuffix = dataReader["TypeSuffix"].ToString() };
        }

        public int Update(FileTypeModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update F_FileType set ");
            builder.Append("TypeName=@TypeName,");
            builder.Append("TypeImg=@TypeImg,");
            builder.Append("TypeSuffix=@TypeSuffix");
            builder.Append(" where TypeId=@TypeId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TypeId", SqlDbType.NVarChar, 0x40), new SqlParameter("@TypeName", SqlDbType.NVarChar, 0x40), new SqlParameter("@TypeImg", SqlDbType.NVarChar, 200), new SqlParameter("@TypeSuffix", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.TypeId;
            commandParameters[1].Value = model.TypeName;
            commandParameters[2].Value = model.TypeImg;
            commandParameters[3].Value = model.TypeSuffix;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

