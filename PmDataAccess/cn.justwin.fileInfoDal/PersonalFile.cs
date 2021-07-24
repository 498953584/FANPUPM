namespace cn.justwin.fileInfoDal
{
    using cn.justwin.DAL;
    using cn.justwin.fileInfoModel;
    using cn.justwin.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class PersonalFile
    {
        public int Add(PersonalFileModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into F_PersonalFile(");
            builder.Append("Id,FileName,FileNewName,FileSize,FileOwner,ParentId,FileType,CreateTime,ShareUsers)");
            builder.Append(" values (");
            builder.Append("@Id,@FileName,@FileNewName,@FileSize,@FileOwner,@ParentId,@FileType,@CreateTime,@ShareUsers)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileName", SqlDbType.NVarChar), new SqlParameter("@FileNewName", SqlDbType.NVarChar), new SqlParameter("@FileSize", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileOwner", SqlDbType.NVarChar, 0x40), new SqlParameter("@ParentId", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileType", SqlDbType.NVarChar, 0x40), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ShareUsers", SqlDbType.NVarChar) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.FileName;
            commandParameters[2].Value = model.FileNewName;
            commandParameters[3].Value = model.FileSize;
            commandParameters[4].Value = model.FileOwner;
            commandParameters[5].Value = model.ParentId;
            commandParameters[6].Value = model.FileType;
            commandParameters[7].Value = model.CreateTime;
            commandParameters[8].Value = model.ShareUsers;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from F_PersonalFile ");
            builder.Append(" where Id=@Id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = Id;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static DataTable GetAllFiles(string id, string startTime, string endTime, string fileName, double fileLowerSize, double fileHighSize, string userCode, bool isGetAll, bool isShare, int pageSize, int pageIndex)
        {
            DataTable table = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (isGetAll)
            {
                builder.Append(GetPublicSqlStr() + "SELECT * FROM(SELECT ROW_NUMBER() OVER (ORDER BY CreateTime DESC) AS No,* FROM Children WHERE  Id!=ParentId  AND FileType = 0  ");
            }
            else
            {
                builder.Append("SELECT * FROM(SELECT ROW_NUMBER() OVER (ORDER BY CreateTime DESC) AS No,* FROM F_PersonalFile WHERE ParentId=@id AND Id!=ParentId ");
            }
            if (isShare)
            {
                builder.Append(" AND ShareUsers LIKE '%'+@userCode+'%' ");
            }
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userCode", userCode));
            if (!string.IsNullOrEmpty(fileName))
            {
                builder.Append(" AND FileName LIKE '%'+@fileName+'%'");
                list.Add(new SqlParameter("@fileName", fileName.Trim()));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" AND CreateTime >= @startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" AND CreateTime <= @endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            if (fileLowerSize > 0.0)
            {
                builder.Append(" AND FileSize >=@fileLowerSize");
                list.Add(new SqlParameter("@fileLowerSize", fileLowerSize));
            }
            if (fileHighSize > 0.0)
            {
                builder.Append(" AND FileSize <=@fileHighSize");
                list.Add(new SqlParameter("@fileHighSize", fileHighSize));
            }
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageSize * pageIndex;
            builder.Append(") AS Result WHERE No BETWEEN @start AND @end");
            list.Add(new SqlParameter("@start", num));
            list.Add(new SqlParameter("@end", num2));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public static int GetAllFilesCount(string id, string startTime, string endTime, string fileName, double fileLowerSize, double fileHighSize, string userCode, bool isGetAll, bool isShare)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (isGetAll)
            {
                builder.Append(GetPublicSqlStr() + "SELECT COUNT(*) FROM Children WHERE Id!=ParentId AND FileType = 0  ");
            }
            else
            {
                builder.Append("SELECT COUNT(*) FROM F_PersonalFile WHERE ParentId=@id AND  Id!=ParentId ");
            }
            if (isShare)
            {
                builder.Append(" AND ShareUsers LIKE '%'+@userCode+'%' ");
            }
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userCode", userCode));
            if (!string.IsNullOrEmpty(fileName))
            {
                builder.Append(" AND FileName LIKE '%'+@fileName+'%'");
                list.Add(new SqlParameter("@fileName", fileName.Trim()));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" ND CreateTime >= @startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" AND CreateTime <= @endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            if (fileLowerSize > 0.0)
            {
                builder.Append(" AND FileSize >=@fileLowerSize");
                list.Add(new SqlParameter("@fileLowerSize", fileLowerSize));
            }
            if (fileHighSize > 0.0)
            {
                builder.Append(" AND FileSize <=@fileHighSize");
                list.Add(new SqlParameter("@fileHighSize", fileHighSize));
            }
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()).ToString());
        }

        public string GetIdsByPid(string pid)
        {
            List<string> list = new List<string>();
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "usp_GetPersonFiles", new SqlParameter[] { new SqlParameter("@fileId", pid) }).Rows)
            {
                list.Add(row["id"].ToString());
            }
            return StringHelper.GetArrayToInStr(list.ToArray());
        }

        public List<PersonalFileModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Id,FileName,FileNewName,FileSize,FileOwner,ParentId,FileType,CreateTime,ShareUsers ");
            builder.Append(" FROM F_PersonalFile ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<PersonalFileModel> list = new List<PersonalFileModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public PersonalFileModel GetModel(string Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Id,FileName,FileNewName,FileSize,FileOwner,ParentId,FileType,CreateTime,ShareUsers from F_PersonalFile ");
            builder.Append(" where Id=@Id ");
            PersonalFileModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@id", Id) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetPersonalFile(string parentId, string startTime, string endTime, string fileName, double fileStartSize, double fileEndSize, string fileOwner, string isLoadFile, string sparentId, string strWhere)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM F_PersonalFile ");
            builder.Append(" where 1=1 ");
            builder.Append(" and ParentId!=Id ");
            builder.Append(" and fileOwner=@fileOwner");
            list.Add(new SqlParameter("@fileOwner", fileOwner));
            if (isLoadFile == "1")
            {
                builder.Append(" and fileType=0");
                builder.Append(" and id in (" + this.GetIdsByPid(sparentId) + ")");
            }
            else
            {
                builder.Append(" and parentId=@parentId ");
                list.Add(new SqlParameter("@parentId", parentId));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and createTime>=@startTime ");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and createTime<=@endTime ");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            if (fileStartSize > 0.0)
            {
                builder.Append(" and fileSize >= @fileStartSize ");
                list.Add(new SqlParameter("@fileStartSize", fileStartSize));
            }
            if (fileEndSize > 0.0)
            {
                builder.Append(" and fileSize <= @fileEndSize ");
                list.Add(new SqlParameter("@fileEndSize", fileEndSize));
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = fileName.Replace('*', '%');
                builder.Append(" and fileName like @fileName ");
                list.Add(new SqlParameter("@fileName", '%' + fileName + '%'));
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append(strWhere);
            }
            builder.Append(" order by CreateTime desc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        private static string GetPublicSqlStr()
        {
            return "\r\nWITH Children AS\r\n(\r\n  SELECT Id,FileName,FileNewName,FileSize,FileOwner,ParentId,FileType,ShareUsers,CreateTime\r\n\tFROM F_PersonalFile WHERE Id=@id\r\n  UNION  ALL\r\n  SELECT T1.Id, T1.FileName, T1.FileNewName, T1.FileSize, T1.FileOwner, T1.ParentId, T1.FileType, T1.ShareUsers, T1.CreateTime\r\n\tFROM F_PersonalFile AS T1\r\n  JOIN Children ON Children.Id=T1.ParentId\r\n  WHERE T1.Id!= T1.ParentId \r\n)\r\n";
        }

        public static DataTable GetShareFolders(string ShareFolderIds, int pageSize, int pageIndex)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM (\r\nSELECT ROW_NUMBER() OVER (ORDER BY CreateTime DESC) AS No,* FROM F_PersonalFile WHERE Id IN({0}) \r\n)AS Result WHERE No BETWEEN @start AND @end\r\n", ShareFolderIds);
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageSize * pageIndex;
            if (string.IsNullOrEmpty(ShareFolderIds))
            {
                ShareFolderIds = "''";
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@start", num), new SqlParameter("@end", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int GetShareFolersCount(string ShareFolderIds)
        {
            string cmdText = "SELECT COUNT(*) FROM F_PersonalFile WHERE Id IN(" + ShareFolderIds + ") ";
            if (string.IsNullOrEmpty(ShareFolderIds))
            {
                ShareFolderIds = "''";
            }
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]).ToString());
        }

        public PersonalFileModel ReaderBind(IDataReader dataReader)
        {
            PersonalFileModel model = new PersonalFileModel {
                Id = dataReader["Id"].ToString(),
                FileName = dataReader["FileName"].ToString(),
                FileNewName = dataReader["FileNewName"].ToString(),
                FileSize = dataReader["FileSize"].ToString(),
                FileOwner = dataReader["FileOwner"].ToString(),
                ParentId = dataReader["ParentId"].ToString(),
                FileType = dataReader["FileType"].ToString(),
                ShareUsers = dataReader["ShareUsers"].ToString()
            };
            object obj2 = dataReader["CreateTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.CreateTime = new DateTime?((DateTime) obj2);
            }
            return model;
        }

        public int Update(PersonalFileModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update F_PersonalFile set ");
            builder.Append("FileName=@FileName,");
            builder.Append("FileNewName=@FileNewName,");
            builder.Append("FileSize=@FileSize,");
            builder.Append("FileOwner=@FileOwner,");
            builder.Append("ParentId=@ParentId,");
            builder.Append("FileType=@FileType,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("ShareUsers=@ShareUsers");
            builder.Append(" where Id=@Id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileName", SqlDbType.NVarChar), new SqlParameter("@FileNewName", SqlDbType.NVarChar), new SqlParameter("@FileSize", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileOwner", SqlDbType.NVarChar, 0x40), new SqlParameter("@ParentId", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileType", SqlDbType.NVarChar, 0x40), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@ShareUsers", SqlDbType.NVarChar) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.FileName;
            commandParameters[2].Value = model.FileNewName;
            commandParameters[3].Value = model.FileSize;
            commandParameters[4].Value = model.FileOwner;
            commandParameters[5].Value = model.ParentId;
            commandParameters[6].Value = model.FileType;
            commandParameters[7].Value = model.CreateTime;
            commandParameters[8].Value = model.ShareUsers;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void UpdateFolderShareUser(string id, string shareUser)
        {
            string str = string.Empty;
            SqlParameter parameter = new SqlParameter("@parentId", id);
            str = SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "uspGetChildrenFoler", new SqlParameter[] { parameter }).ToString();
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE F_PersonalFile SET ShareUsers='" + shareUser + "' WHERE Id IN(" + str + ")");
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }
    }
}

