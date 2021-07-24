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

    public class FileInfo
    {
        public int Add(FileInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into F_FileInfo(");
            builder.Append("Id,FileName,FileSize,FileOwner,ParentId,FileType,UserCodes,CreateTime,FileNewName)");
            builder.Append(" values (");
            builder.Append("@Id,@FileName,@FileSize,@FileOwner,@ParentId,@FileType,@UserCodes,@CreateTime,@FileNewName)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileName", SqlDbType.NVarChar, -1), new SqlParameter("@FileSize", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileOwner", SqlDbType.NVarChar, 0x40), new SqlParameter("@ParentId", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileType", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCodes", SqlDbType.NVarChar, -1), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@FileNewName", SqlDbType.NVarChar, -1) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.FileName;
            commandParameters[2].Value = model.FileSize;
            commandParameters[3].Value = model.FileOwner;
            commandParameters[4].Value = model.ParentId;
            commandParameters[5].Value = model.FileType;
            commandParameters[6].Value = model.UserCodes;
            commandParameters[7].Value = model.CreateTime;
            commandParameters[8].Value = model.FileNewName;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(string Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from F_FileInfo ");
            builder.Append(" where Id=@Id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = Id;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void DelProjectAnnex(string id)
        {
            string cmdText = "DELETE FROM PT_Prj_Completed_Annex WHERE AnnexAddress=@Id";
            SqlParameter parameter = new SqlParameter("@Id", id);
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public static DataTable GetAllFiles(string id, string userCode, string startTime, string endTime, string fileName, double fileLowerSize, double fileHighSize, string fileOwner, bool isGetAll, int pageSize, int pageIndex)
        {
            DataTable table = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (isGetAll)
            {
                string publicSqlStr = new FileInfo().GetPublicSqlStr();
                builder.Append(publicSqlStr + "SELECT * FROM(SELECT ROW_NUMBER() OVER(ORDER BY CreateTime DESC) AS No,* FROM F_FileInfo WHERE ParentId IN(SELECT Id FROM cteLimitFolder  \r\nWHERE UserCodes LIKE '%'+@userCodes+'%' AND IsValid=0) AND FileType=0 AND IsValid=0 ");
                list.Add(new SqlParameter("@userCodes", userCode));
            }
            else
            {
                builder.Append("SELECT * FROM(SELECT ROW_NUMBER() OVER (ORDER BY CreateTime DESC) AS No,* FROM F_FileInfo WHERE ParentId=@id AND IsValid=0 AND FileType=0 ");
            }
            list.Add(new SqlParameter("@id", id));
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = fileName.Replace('*', '%');
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
            if (!string.IsNullOrEmpty(fileOwner))
            {
                builder.Append(" AND FileOwner IN (SELECT v_yhdm FROM pt_yhmc WHERE v_xm LIKE @fileOwner)");
                list.Add(new SqlParameter("@fileOwner", fileOwner.Trim()));
            }
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageSize * pageIndex;
            builder.Append(") AS Result WHERE No BETWEEN @start AND @end");
            list.Add(new SqlParameter("@start", num));
            list.Add(new SqlParameter("@end", num2));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public static int GetAllFilesCount(string id, string userCode, string startTime, string endTime, string fileName, double fileLowerSize, double fileHighSize, string fileOwner, bool isGetAll)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (isGetAll)
            {
                string publicSqlStr = new FileInfo().GetPublicSqlStr();
                builder.Append(publicSqlStr + "SELECT COUNT(*) FROM F_FileInfo WHERE ParentId IN(SELECT Id FROM cteLimitFolder  \r\nWHERE UserCodes LIKE '%'+@userCodes+'%' AND IsValid=0) AND FileType=0 AND IsValid=0 ");
                list.Add(new SqlParameter("@userCodes", userCode));
            }
            else
            {
                builder.Append("SELECT COUNT(*) FROM F_FileInfo WHERE ParentId=@id AND IsValid=0 AND FileType=0 ");
            }
            list.Add(new SqlParameter("@id", id));
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = fileName.Replace('*', '%');
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
            if (!string.IsNullOrEmpty(fileOwner))
            {
                builder.Append(" AND FileOwner IN (SELECT v_yhdm FROM pt_yhmc WHERE v_xm LIKE @fileOwner)");
                list.Add(new SqlParameter("@fileOwner", fileOwner.Trim()));
            }
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), list.ToArray()).ToString());
        }

        public static string GetAncestor(string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT AncestorInfo FROM F_FileInfo  WHERE Id=@Id ");
            SqlParameter parameter = new SqlParameter("@Id", id);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
            return ((obj2 == null) ? string.Empty : obj2.ToString());
        }

        public static DataTable GetAncestorInfo(string id)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("WITH AncestorInfo AS\r\n\t(\r\n\t\tSELECT Id,FileName,ParentId FROM F_FileInfo WHERE Id=@Id\r\n\t\tUNION ALL\r\n\t\tSELECT T1.Id,T1.FileName,T1.ParentId FROM F_FileInfo AS T1\r\n\t\tINNER JOIN AncestorInfo ON AncestorInfo.ParentId=T1.Id\r\n\t\tWHERE AncestorInfo.ParentId!=AncestorInfo.Id \r\n\t)\r\nSELECT  Id,FileName FROM AncestorInfo WHERE Id!=@Id ");
            SqlParameter parameter = new SqlParameter("@Id", id);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
        }

        public static DataTable GetChildren(string directoryId)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("WITH Children AS\r\n(\r\n\tSELECT Id,ParentId,FileNewName,FileType FROM F_FileInfo WHERE ParentId='{0}' AND IsValid=0\r\n\tUNION ALL\r\n    SELECT T1.Id,T1.ParentId,T1.FileNewName,T1.FileType FROM F_FileInfo AS T1\r\n\tINNER JOIN Children ON Children.Id=T1.ParentId\r\n)\r\nSELECT * FROM Children ", directoryId);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public static DataTable GetDirectChildren(string id)
        {
            DataTable table = new DataTable();
            string cmdText = "SELECT * FROM F_FileInfo WHERE ParentId=@Id";
            SqlParameter parameter = new SqlParameter("@Id", id);
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[] { parameter });
        }

        public DataTable GetFillTable(string parentId, string userCodes, string startTime, string endTime, string fileName, double fileStartSize, double fileEndSize, string fileOwner, string isLoadFile, string dparentId, string strWhere)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select * FROM F_FileInfo ");
            builder.Append(" where 1=1 ");
            builder.Append(" and ParentId!=Id ");
            builder.Append("  and parentId in(select id from f_fileInfo");
            builder.Append(" where usercodes like @userCodes and filetype!=0  and IsValid='false')");
            list.Add(new SqlParameter("@userCodes", '%' + userCodes + '%'));
            if (isLoadFile == "1")
            {
                builder.Append(" and fileType=0");
                builder.Append(" and id in (" + this.GetIdsByPid(dparentId) + ")");
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
            if (!string.IsNullOrEmpty(fileOwner))
            {
                builder.Append(" and fileOwner in (select v_yhdm from pt_yhmc where v_xm like @fileOwner)");
                list.Add(new SqlParameter("@fileOwner", '%' + fileOwner + '%'));
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

        public static int GetFlodersCount(string parentId)
        {
            string cmdText = "SELECT COUNT(*) FROM F_FileInfo WHERE ParentId!=Id AND ParentId=@Id AND IsValid=0 AND FileType=2";
            SqlParameter parameter = new SqlParameter("@Id", parentId);
            return int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[] { parameter }).ToString());
        }

        public static DataTable GetFolders(string parentId, int pageSize, int pageIndex)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM (\r\nSELECT ROW_NUMBER() OVER(ORDER BY CreateTime DESC) AS No,* FROM F_FileInfo\r\nWHERE ParentId!=Id AND ParentId=@Id AND IsValid=0 AND FileType=2\r\n) AS Result WHERE No BETWEEN @start AND @end");
            int num = ((pageIndex - 1) * pageSize) + 1;
            int num2 = pageSize * pageIndex;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", parentId), new SqlParameter("@start", num), new SqlParameter("@end", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static string GetId(string parentId, string fileName, string exceptId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Id FROM F_FileInfo WHERE ParentId=@ParentId AND IsValid=0 AND FileName=@FileName AND Id !=@exceptId");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ParentId", parentId), new SqlParameter("@FileName", fileName), new SqlParameter("@exceptId", exceptId) };
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
            return ((obj2 == null) ? string.Empty : obj2.ToString());
        }

        public string GetIdsByPid(string pid)
        {
            List<string> list = new List<string>();
            foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.StoredProcedure, "usp_GetFiles", new SqlParameter[] { new SqlParameter("@fileId", pid) }).Rows)
            {
                list.Add(row["id"].ToString());
            }
            return StringHelper.GetArrayToInStr(list.ToArray());
        }

        public List<FileInfoModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Id,FileName,FileSize,FileOwner,ParentId,FileType,UserCodes,CreateTime,FileNewName ");
            builder.Append(" FROM F_FileInfo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<FileInfoModel> list = new List<FileInfoModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public FileInfoModel GetModel(string Id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select Id,FileName,FileSize,FileOwner,ParentId,FileType,UserCodes,CreateTime,FileNewName from F_FileInfo ");
            builder.Append(" where Id=@Id");
            FileInfoModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@Id", Id) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        private string GetPublicSqlStr()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\nWITH cteLimitFolder AS\r\n(\r\n  SELECT Id,ParentId,IsValid,UserCodes\r\n\tFROM F_FileInfo WHERE Id=@Id\r\n  UNION  ALL\r\n  SELECT T1.Id, T1.ParentId, T1.IsValid ,T1.UserCodes\r\n\tFROM F_FileInfo AS T1\r\n  JOIN cteLimitFolder ON cteLimitFolder.Id=T1.ParentId\r\n  WHERE T1.Id!= T1.ParentId AND T1.FileType=2\r\n)\r\n");
            return builder.ToString();
        }

        public static DataTable GetRecycleInfo(string userCode, int pageSize, int pageIndex)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM (\r\nSELECT ROW_NUMBER() OVER(ORDER BY FileType DESC,CreateTime DESC) AS No,Id,FileName,FileNewName,FileOwner,FileType,UserCodes,CreateTime FROM F_FileInfo\r\nWHERE  IsValid=1 AND FileOwner LIKE '%'+@userCode+'%'\r\n) AS Resoult WHERE No BETWEEN @Start AND @End");
            int num = (pageSize * (pageIndex - 1)) + 1;
            int num2 = pageSize * pageIndex;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode), new SqlParameter("@Start", num), new SqlParameter("@End", num2) };
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static int GetRecycleRecord(string userCode)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM F_FileInfo\r\nWHERE  IsValid=1 AND FileOwner LIKE '%'+@userCode+'%'");
            SqlParameter parameter = new SqlParameter("@userCode", userCode);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[] { parameter });
            if (obj2 != null)
            {
                num = int.Parse(obj2.ToString());
            }
            return num;
        }

        public static int GetRepeatDirectoryCount(string inStr)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(inStr))
            {
                string str = inStr;
                if (inStr.Contains(","))
                {
                    str = inStr.Substring(0, inStr.IndexOf(','));
                }
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("DECLARE @RecoverFileName VARCHAR(4000)\r\nSELECT @RecoverFileName=''''+FileName+''','+ISNULL(@RecoverFileName,'')  FROM F_FileInfo \r\nWHERE IsValid='1' AND FileType !='0' AND Id IN({0})\r\nSET @RecoverFileName=LEFT(@RecoverFileName,LEN(@RecoverFileName)-1)\r\nEXEC('\r\nSELECT COUNT(FileName) FROM F_FileInfo  \r\nWHERE ParentId=(SELECT ParentId FROM F_FileInfo WHERE Id='{1}')\r\nAND IsValid=''0'' AND FileType !=''0'' AND FileName IN('+@RecoverFileName+')\r\n')", inStr, str);
                object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[0]);
                if (obj2 != null)
                {
                    int.TryParse(obj2.ToString(), out result);
                }
            }
            return result;
        }

        public static DataTable GetRepeatDirectoryId(string inStr)
        {
            DataTable table = new DataTable();
            if (string.IsNullOrEmpty(inStr))
            {
                return table;
            }
            string str = inStr;
            if (inStr.Contains(","))
            {
                str = inStr.Substring(0, inStr.IndexOf(','));
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("DECLARE @RecoverFileName VARCHAR(4000)\r\nSELECT @RecoverFileName=''''+FileName+''','+ISNULL(@RecoverFileName,'')  FROM F_FileInfo \r\nWHERE IsValid='1' AND FileType !='0' AND Id IN({0})\r\nSET @RecoverFileName=LEFT(@RecoverFileName,LEN(@RecoverFileName)-1)\r\nEXEC('\r\nSELECT Id,FileName FROM F_FileInfo  \r\nWHERE ParentId=(SELECT ParentId FROM F_FileInfo WHERE Id='{1}')\r\nAND IsValid=''0'' AND FileType !=''0'' AND FileName IN('+@RecoverFileName+')\r\n')", inStr, str);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public static bool isExist(string id, string fileName)
        {
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM F_FileInfo  WHERE Id=@Id AND FileName=@FileName AND IsValid=0");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", id), new SqlParameter("@FileName", fileName) };
            if (int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters).ToString()) > 0)
            {
                flag = true;
            }
            return flag;
        }

        public FileInfoModel ReaderBind(IDataReader dataReader)
        {
            FileInfoModel model = new FileInfoModel {
                Id = dataReader["Id"].ToString(),
                FileName = dataReader["FileName"].ToString(),
                FileSize = dataReader["FileSize"].ToString(),
                FileOwner = dataReader["FileOwner"].ToString(),
                ParentId = dataReader["ParentId"].ToString(),
                FileType = dataReader["FileType"].ToString(),
                UserCodes = dataReader["UserCodes"].ToString(),
                FileNewName = dataReader["FileNewName"].ToString()
            };
            object obj2 = dataReader["CreateTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.CreateTime = new DateTime?((DateTime) obj2);
            }
            return model;
        }

        public static void SetDelParentId(string parentId)
        {
            string cmdText = "UPDATE F_FileInfo SET ParentId=null WHERE ParentId=@ParentId AND IsValid=1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ParentId", parentId) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        public int Update(FileInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update F_FileInfo set ");
            builder.Append("FileName=@FileName,");
            builder.Append("FileSize=@FileSize,");
            builder.Append("FileOwner=@FileOwner,");
            builder.Append("ParentId=@ParentId,");
            builder.Append("FileType=@FileType,");
            builder.Append("UserCodes=@UserCodes,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("FileNewName=@FileNewName");
            builder.Append(" where Id=@Id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileName", SqlDbType.NVarChar, -1), new SqlParameter("@FileSize", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileOwner", SqlDbType.NVarChar, 0x40), new SqlParameter("@ParentId", SqlDbType.NVarChar, 0x40), new SqlParameter("@FileType", SqlDbType.NVarChar, 0x40), new SqlParameter("@UserCodes", SqlDbType.NVarChar, -1), new SqlParameter("@CreateTime", SqlDbType.DateTime), new SqlParameter("@FileNewName", SqlDbType.NVarChar) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.FileName;
            commandParameters[2].Value = model.FileSize;
            commandParameters[3].Value = model.FileOwner;
            commandParameters[4].Value = model.ParentId;
            commandParameters[5].Value = model.FileType;
            commandParameters[6].Value = model.UserCodes;
            commandParameters[7].Value = model.CreateTime;
            commandParameters[8].Value = model.FileNewName;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void UpdateIsValidAndAncestorInfo(bool isValid, string jsonAncestorInfo, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("Sql中的Id不能为空");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE F_FileInfo SET IsValid=@isValid,AncestorInfo=@jsonAncestorInfo  WHERE Id =@Id");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@isValid", isValid), new SqlParameter("@jsonAncestorInfo", jsonAncestorInfo), new SqlParameter("@Id", id) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void UpdateIsValidAndAncestorInfoAndParnentId(bool isValid, string jsonAncestorInfo, string id, string parentId)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("Sql中的Id不能为空");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("UPDATE F_FileInfo SET IsValid=@isValid,AncestorInfo=@jsonAncestorInfo,ParentId=@ParentId  WHERE Id =@Id");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@isValid", isValid), new SqlParameter("@jsonAncestorInfo", jsonAncestorInfo), new SqlParameter("@Id", id), new SqlParameter("@ParentId", parentId) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public static void UpdateParentId(string newParentId, string oldParentId)
        {
            string cmdText = "UPDATE F_FileInfo SET ParentId=@newParentId WHERE ParentId=@oldParentId";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@newParentId", newParentId), new SqlParameter("@oldParentId", oldParentId) };
            SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
    }
}

