namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Web;

    public class AnnexAction
    {
        public int AddAnnex(HttpPostedFile postedFile, AnnexInfo annexInfo, AnnexModuleSettingsInfo settingsInfo)
        {
            FileUpload upload = new FileUpload {
                ExtName = settingsInfo.ExtName,
                FileSize = settingsInfo.FileSize,
                UploadPath = settingsInfo.FilePath + annexInfo.AnnexType.ToString() + "/"
            };
            if (upload.UploadValidator(postedFile))
            {
                annexInfo.OriginalName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1);
                string fileName = "";
                fileName = DateTime.Now.ToString("yyyyMMdd-HH-ss-ffff") + "." + upload.GetExtName(annexInfo.OriginalName);
                annexInfo.AnnexName = fileName;
                annexInfo.FilePath = settingsInfo.FilePath + annexInfo.AnnexType.ToString() + "/" + fileName.Substring(0, 6) + "/";
                annexInfo.FileSize = postedFile.ContentLength;
                string str2 = "";
                str2 = "insert into XPM_Basic_AnnexList(AnnexCode,ModuleID,RecordCode,AnnexType,FileCode,AnnexName,OriginalName,FilePath,FileSize,AddDate,State,HumanCode,Remark)";
                string str3 = str2;
                object obj2 = str3 + " values('" + annexInfo.AnnexCode.ToString() + "'," + annexInfo.ModuleID.ToString() + ",'" + annexInfo.RecordCode.ToString() + "'," + annexInfo.AnnexType.ToString() + ",'" + annexInfo.FileCode + "',";
                string str4 = string.Concat(new object[] { obj2, "'", annexInfo.AnnexName, "','", annexInfo.OriginalName, "','", annexInfo.FilePath, "',", annexInfo.FileSize, ",'", annexInfo.AddDate.ToShortDateString(), "'," });
                if (publicDbOpClass.ExecSqlString(str4 + "-1,'" + annexInfo.HumanCode + "','" + annexInfo.Remark + "') ") != 1)
                {
                    return -2;
                }
                if (upload.Upload(postedFile, fileName, true))
                {
                    return publicDbOpClass.ExecSqlString("update XPM_Basic_AnnexList set State = 1 where AnnexCode = '" + annexInfo.AnnexCode.ToString() + "'");
                }
            }
            return -1;
        }

        public int ClearAnnexList(string recordCode, int annexType, int moduleID, string humanCode)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexList where ((RecordCode = '" + recordCode + "' and State = -1)or(RecordCode = '')) and (AnnexType = " + annexType.ToString() + ") and (HumanCode = '" + humanCode + "') and (ModuleID = " + moduleID.ToString() + ")"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (this.DelAnnex(this.GetAnnexInfoFromDataRow(table.Rows[i])) != 1)
                    {
                        return 0;
                    }
                }
            }
            return 1;
        }

        public int DelAnnex(AnnexInfo annexInfo)
        {
            FileUpload upload = new FileUpload();
            if (upload.Delete(annexInfo.FilePath + annexInfo.AnnexName))
            {
                return publicDbOpClass.ExecSqlString("delete from XPM_Basic_AnnexList where AnnexCode = '" + annexInfo.AnnexCode.ToString() + "'");
            }
            return 0;
        }

        public int DelAnnexTemp(Guid annexCode)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.ExecSqlString(((str + " update XPM_Basic_AnnexList set State = -1 where AnnexCode = '" + annexCode.ToString() + "' and State = 1") + " update XPM_Basic_AnnexList set State = 2 where AnnexCode = '" + annexCode.ToString() + "' and State = 0") + " end");
        }

        private AnnexInfo GetAnnexInfoFromDataRow(DataRow dr)
        {
            return new AnnexInfo { AnnexCode = (Guid) dr["AnnexCode"], ModuleID = (int) dr["ModuleID"], RecordCode = dr["RecordCode"].ToString(), AnnexType = (int) dr["AnnexType"], FileCode = dr["FileCode"].ToString(), OriginalName = dr["OriginalName"].ToString(), AnnexName = dr["AnnexName"].ToString(), FileSize = (int) dr["FileSize"], FilePath = dr["FilePath"].ToString(), AddDate = (DateTime) dr["AddDate"], State = (int) dr["State"], Remark = dr["Remark"].ToString(), HumanCode = dr["HumanCode"].ToString() };
        }

        public ArrayList GetAnnexList(string recordCode, int annexType, int moduleID)
        {
            ArrayList list = new ArrayList();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexList where (((RecordCode = '" + recordCode + "') and ((State = 0) or (State = 1)))or((RecordCode = '')and(State <>-1))) and AnnexType = " + annexType.ToString() + " and (ModuleID = " + moduleID.ToString() + ")"))
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.GetAnnexInfoFromDataRow(table.Rows[i]));
                }
            }
            return list;
        }

        public DataTable GetFileList(string recordCode, int annexType, int moduleID)
        {
            new ArrayList();
            return publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexList where (((RecordCode = '" + recordCode + "') and ((State = 0) or (State = 1)))or((RecordCode = '')and(State <>-1))) and AnnexType = " + annexType.ToString() + " and (ModuleID = " + moduleID.ToString() + ")");
        }

        public AnnexInfo GetSingleAnnexInfo(Guid annexCode)
        {
            AnnexInfo info = new AnnexInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexList where AnnexCode = '" + annexCode.ToString() + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    return this.GetAnnexInfoFromDataRow(table.Rows[0]);
                }
            }
            return info;
        }

        public int RestoreAnnexList(string recordCode, int annexType, int moduleID, string humanCode)
        {
            string str = "";
            string str2 = str + " begin ";
            string str3 = str2 + " update XPM_Basic_AnnexList set State = -1 where RecordCode = '" + recordCode + "' and State = 1 and annexType = " + annexType.ToString() + " and moduleID = " + moduleID.ToString() + " and HumanCode = '" + humanCode + "'";
            return publicDbOpClass.ExecSqlString((str3 + " update XPM_Basic_AnnexList set State = 0 where RecordCode = '" + recordCode + "' and State = 2 and annexType = " + annexType.ToString() + " and moduleID = " + moduleID.ToString() + " and HumanCode = '" + humanCode + "'") + " end");
        }

        public int SynchronizeAnnexList(string recordCode, int annexType, int moduleID, string humanCode)
        {
            string str = "";
            str = " begin ";
            string str2 = str;
            string str3 = str2 + " update XPM_Basic_AnnexList set RecordCode = '" + recordCode + "' where RecordCode = '' and AnnexType = " + annexType.ToString() + " and ModuleID = " + moduleID.ToString() + " and HumanCode = '" + humanCode + "'";
            string str4 = str3 + " update XPM_Basic_AnnexList set State = 0 where RecordCode = '" + recordCode + "' and State = 1 and AnnexType = " + annexType.ToString() + " and ModuleID = " + moduleID.ToString() + " and HumanCode = '" + humanCode + "'";
            return publicDbOpClass.ExecSqlString((str4 + " update XPM_Basic_AnnexList set State = -1 where RecordCode = '" + recordCode + "' and State =2 and AnnexType = " + annexType.ToString() + " and ModuleID = " + moduleID.ToString() + " and HumanCode = '" + humanCode + "'") + " end");
        }
    }
}

