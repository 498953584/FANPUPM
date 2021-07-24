namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Web;

    public class IntendancePhotoListAction
    {
        public int AddpHoteInfo(HttpPostedFile postedFile, IntendancePhotoList annexInfo)
        {
            FileUpload upload = new FileUpload {
                FileSize = 0x800000,
                UploadPath = "/UploadFiles/Image/PhotosCheckIn/"
            };
            if (upload.UploadValidator(postedFile))
            {
                annexInfo.PhotoName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1);
                string fileName = "";
                fileName = ((DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0')) + "_" + DateTime.Now.ToLongTimeString().Replace(":", "")) + annexInfo.PhotoName;
                annexInfo.PhotoName = fileName;
                annexInfo.PhotoPath = "/UploadFiles/Image/PhotosCheckIn/" + fileName.Substring(0, 6) + "/";
                string str2 = "";
                str2 = "insert into OPM_EPCM_IntendancePhotoList(noteId,InfoGuid,PhotoNumber,PhotoExplain,PhotoPath,PhotoName,OPyhdm,PhotoType)";
                object obj2 = str2;
                if (publicDbOpClass.ExecSqlString(string.Concat(new object[] { 
                    obj2, " values('", annexInfo.NoteId, "','", annexInfo.InfoGuid, "','", annexInfo.PhotoNumber.ToString(), "','", annexInfo.PhotoExplain.ToString(), "','", annexInfo.PhotoPath.ToString(), "','", annexInfo.PhotoName.ToString(), "','", annexInfo.UserCode.ToString(), "','", 
                    annexInfo.PhotoType, "')"
                 })) != 1)
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
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from OPM_EPCM_IntendancePhotoList  where InfoGuid= '" + InfoGuid + "'"))
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

        public int DelAnnex(IntendancePhotoList annexInfo)
        {
            FileUpload upload = new FileUpload();
            if (upload.Delete(annexInfo.PhotoPath + annexInfo.PhotoName))
            {
                return publicDbOpClass.ExecSqlString("delete from OPM_EPCM_IntendancePhotoList where noteId = '" + annexInfo.NoteId.ToString() + "'");
            }
            return 0;
        }

        public int DelPhotoList(string usercode)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from OPM_EPCM_IntendancePhotoList where OPyhdm= '" + usercode + "' and InfoGuid='00000000-0000-0000-0000-000000000000'"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (this.DelAnnex(this.GetSinglePhotoInfo(new Guid(table.Rows[i]["NoteId"].ToString()))) != 1)
                    {
                        return 0;
                    }
                    MakeThumbnail thumbnail = new MakeThumbnail();
                    if (thumbnail.DelThumbnai(table.Rows[i]["NoteId"].ToString()))
                    {
                        return 1;
                    }
                }
            }
            return 1;
        }

        public DataTable GetPhotoInfoList(string IntendanceGuid)
        {
            new IntendancePhotoList();
            return publicDbOpClass.DataTableQuary("select * from OPM_EPCM_IntendancePhotoList where InfoGuid in (select NoteId from OPM_EPCM_IntendanceInfo where IntendanceGuid='" + IntendanceGuid + "')");
        }

        private IntendancePhotoList GetPhotosInfoFromDataRow(DataRow dr)
        {
            return new IntendancePhotoList { InfoGuid = (Guid) dr["InfoGuid"], NoteId = (Guid) dr["NoteId"], PhotoNumber = dr["PhotoNumber"].ToString(), PhotoExplain = dr["PhotoExplain"].ToString(), PhotoPath = dr["PhotoPath"].ToString(), PhotoName = dr["PhotoName"].ToString() };
        }

        public IntendancePhotoList GetSinglePhotoInfo(Guid NoteID)
        {
            IntendancePhotoList list = new IntendancePhotoList();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from OPM_EPCM_IntendancePhotoList where NoteID = '" + NoteID + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    return this.GetPhotosInfoFromDataRow(table.Rows[0]);
                }
            }
            return list;
        }
    }
}

