namespace cn.justwin.opm.PM.prj_plan
{
    using cn.justwin.opm;
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.action;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web;

    public class PrjInfoPhotoAction
    {
        public int AddpHoteInfo(HttpPostedFile postedFile, PrjInfoPhotoModel annexInfo)
        {
            FileUpload upload = new FileUpload {
                FileSize = 0x800000,
                UploadPath = "/UploadFiles/Image/PrjPhoto/"
            };
            if (upload.UploadValidator(postedFile))
            {
                annexInfo.PhotoName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1);
                string fileName = "";
                fileName = ((DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0')) + "_" + DateTime.Now.ToLongTimeString().Replace(":", "")) + annexInfo.PhotoName;
                annexInfo.PhotoName = fileName;
                annexInfo.PhotoPath = "/UploadFiles/Image/PrjPhoto/" + fileName.Substring(0, 6) + "/";
                HttpContext.Current.Session["PrjInfo-photo-success"] = annexInfo;
                StringBuilder builder = new StringBuilder();
                builder.Append("insert into PT_PrjInfo_PhotoList(");
                builder.Append("UID,PrjGuid,PhotoCode,PhotoPath,PhotoName,PhotoType,I_xh,IsValid,AddUser,AddTime,Remark)");
                builder.Append(" values (");
                builder.Append("@UID,@PrjGuid,@PhotoCode,@PhotoPath,@PhotoName,@PhotoType,@I_xh,@IsValid,@AddUser,@AddTime,@Remark)");
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PrjGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PhotoCode", SqlDbType.VarChar, 50), new SqlParameter("@PhotoPath", SqlDbType.VarChar, 200), new SqlParameter("@PhotoName", SqlDbType.VarChar, 200), new SqlParameter("@PhotoType", SqlDbType.Int, 4), new SqlParameter("@I_xh", SqlDbType.Int, 4), new SqlParameter("@IsValid", SqlDbType.Char, 1), new SqlParameter("@AddUser", SqlDbType.VarChar, 50), new SqlParameter("@AddTime", SqlDbType.DateTime), new SqlParameter("@Remark", SqlDbType.VarChar) };
                commandParameters[0].Value = annexInfo.UID;
                commandParameters[1].Value = annexInfo.PrjGuid;
                commandParameters[2].Value = (annexInfo.PhotoCode == null) ? "" : annexInfo.PhotoCode;
                commandParameters[3].Value = annexInfo.PhotoPath;
                commandParameters[4].Value = annexInfo.PhotoName;
                commandParameters[5].Value = annexInfo.PhotoType;
                commandParameters[6].Value = !annexInfo.I_xh.HasValue ? 0 : annexInfo.I_xh;
                commandParameters[7].Value = annexInfo.IsValid;
                commandParameters[8].Value = annexInfo.AddUser;
                commandParameters[9].Value = DateTime.Now;
                commandParameters[10].Value = annexInfo.Remark;
                if (publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) != 1)
                {
                    return -2;
                }
                if (upload.Upload(postedFile, fileName, true))
                {
                    return 1;
                }
            }
            return -1;
        }

        public int ClearPhotosList(string InfoGuid)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from PT_PrjInfo_PhotoList  where PrjgUID= '" + InfoGuid + "'"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (this.DelAnnex(this.GetSinglePhotoInfo(new Guid(table.Rows[i]["NoteId"].ToString()))) != 1)
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }

        public int DelAllPhotos(string uid)
        {
            string sql = " delete from PT_PrjInfo_PhotoList where uid in (@uid) ;delete from XPM_Basic_Thumbnai where  ThumbnaiCode in (@uid)";
            return publicDbOpClass.ExecuteNonQuery(CommandType.Text, PrjInfo_Action.GetTranscationStr(sql), new SqlParameter[] { new SqlParameter("@uid", uid) });
        }

        public int DelAllPhotosPrj(string prjguid)
        {
            string sql = " DELETE FROM PT_PrjInfo_PhotoList WHERE PrjGuid=@uid; DELETE FROM XPM_Basic_Thumbnai WHERE ThumbnaiCode IN  (SELECT ppipl.UID FROM PT_PrjInfo_PhotoList ppipl WHERE ppipl.PrjGuid=@uid)";
            return publicDbOpClass.ExecuteNonQuery(CommandType.Text, PrjInfo_Action.GetTranscationStr(sql), new SqlParameter[] { new SqlParameter("@uid", prjguid) });
        }

        public int DelAnnex(PrjInfoPhotoModel annexInfo)
        {
            FileUpload upload = new FileUpload();
            if (upload.Delete(annexInfo.PhotoPath + annexInfo.PhotoName))
            {
                return publicDbOpClass.ExecSqlString("delete from PT_PrjInfo_PhotoList where uid = '" + annexInfo.UID.ToString() + "'");
            }
            return 0;
        }

        public int DelPhotoList(string usercode)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from PT_PrjInfo_PhotoList where AddUser= '" + usercode + "' and isvalid='0'"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (this.DelAnnex(this.GetSinglePhotoInfo(new Guid(table.Rows[i]["UID"].ToString()))) != 1)
                    {
                        return 0;
                    }
                    MakeThumbnail thumbnail = new MakeThumbnail();
                    if (thumbnail.DelThumbnai(table.Rows[i]["UID"].ToString()))
                    {
                        return 1;
                    }
                }
            }
            return 1;
        }

        public DataTable GetPhotoInfoList(string IntendanceGuid)
        {
            new PrjInfoPhotoModel();
            return publicDbOpClass.DataTableQuary("select * from PT_PrjInfo_PhotoList where UID = '" + IntendanceGuid + "' order by PhotoType");
        }

        private PrjInfoPhotoModel GetPhotosInfoFromDataRow(DataRow dr)
        {
            return new PrjInfoPhotoModel { UID = (Guid) dr["UID"], PrjGuid = (Guid) dr["PrjGuid"], PhotoCode = dr["PhotoCode"].ToString(), Remark = dr["Remark"].ToString(), PhotoType = int.Parse(dr["PhotoType"].ToString()), PhotoPath = dr["PhotoPath"].ToString(), PhotoName = dr["PhotoName"].ToString(), IsValid = dr["IsValid"].ToString() };
        }

        public PrjInfoPhotoModel GetSinglePhotoInfo(Guid NoteID)
        {
            PrjInfoPhotoModel model = new PrjInfoPhotoModel();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from PT_PrjInfo_PhotoList where UID = '" + NoteID + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    return this.GetPhotosInfoFromDataRow(table.Rows[0]);
                }
            }
            return model;
        }

        public DataTable GetThumbnail(string prjid)
        {
            return publicDbOpClass.ExecuteDataTable(CommandType.Text, " SELECT PT_PrjInfo_PhotoList.*,(XPM_Basic_Thumbnai.ThumbnaImgPath+XPM_Basic_Thumbnai.ThumbnaName) AS tpath FROM XPM_Basic_Thumbnai,PT_PrjInfo_PhotoList WHERE XPM_Basic_Thumbnai.ThumbnaiCode=PT_PrjInfo_PhotoList.UID AND PT_PrjInfo_PhotoList.PrjGuid='" + prjid + "' and PT_PrjInfo_PhotoList.IsValid='1' order by PT_PrjInfo_PhotoList.PhotoType ", new List<SqlParameter>().ToArray());
        }

        public bool UpdatePhotosList(string deluidstr, string changeuidstr)
        {
            if ((changeuidstr == "") && (deluidstr == ""))
            {
                return true;
            }
            StringBuilder builder = new StringBuilder();
            if (deluidstr != "")
            {
                builder.Append("  delete from PT_PrjInfo_PhotoList where uid in (" + deluidstr + ") ;");
                builder.Append(" delete from XPM_Basic_Thumbnai where  ThumbnaiCode in (" + deluidstr + ") ;  ");
            }
            if (changeuidstr != "")
            {
                builder.Append(" update  PT_PrjInfo_PhotoList set IsValid='1'  where uid in (" + changeuidstr + ")");
            }
            return (publicDbOpClass.ExecSqlString(PrjInfo_Action.GetTranscationStr(builder.ToString())) > 0);
        }
    }
}

