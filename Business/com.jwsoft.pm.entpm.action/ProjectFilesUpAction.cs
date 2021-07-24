namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class ProjectFilesUpAction
    {
        public bool DeleteFilesClass(string strID)
        {
            return publicDbOpClass.NonQuerySqlString("DELETE FROM Prj_FilesClass WHERE Id_xh IN (" + strID + ")");
        }

        public DataTable FilesClassList(int Special)
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_FilesClass_v where IsValid = " + Special.ToString());
        }

        public ProjectFilesUpInfo FilesSavePath()
        {
            string sqlString = "SELECT FilePath FROM PT_AnnexSettings WHERE (ModuleCode = 'ProjectFiles')";
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                return this.GetSavePathDataRow(table.Rows[0]);
            }
        }

        public static string GetBaseClassIDs(string PrjCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar(string.Format("select ClassIDs from Prj_DocumentBaseInfo where PrjCode='{0}'", PrjCode));
            if (obj2 != null)
            {
                return obj2.ToString();
            }
            return "";
        }

        public static string GetClassName(string ClassID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select ClassName,remark from Prj_FilesClass where ClassID='" + ClassID + "'");
            if (table.Rows.Count <= 0)
            {
                return "";
            }
            string str2 = (table.Rows[0][1] == DBNull.Value) ? "" : table.Rows[0][1].ToString();
            if (str2.Length > 0)
            {
                return (table.Rows[0][0].ToString() + "(" + str2 + ")");
            }
            return table.Rows[0][0].ToString();
        }

        public int GetCount()
        {
            string sqlString = "select * from Prj_FilesClass_v";
            return int.Parse(publicDbOpClass.ExecuteScalar(sqlString).ToString());
        }

        public DataTable GetFileClassId_xh()
        {
            string sqlString = "SELECT max(Id_xh) FROM Prj_FilesClass";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetFileClassInformation(string strId)
        {
            return publicDbOpClass.DataTableQuary("SELECT * FROM Prj_FilesClass_v WHERE Id_xh='" + strId + "'");
        }

        public DataTable GetFileClassList(string ParentClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT row_number() over(order by Id_xh) as rowNumber,ClassID,ParentClassID,");
            builder.Append("ClassName,Remark,IsValid,Id_xh,ClassOpters,ParentName");
            builder.Append(" FROM Prj_FilesClass_v ");
            builder.Append("WHERE ParentClassID ='" + ParentClassID + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetFileParentClassID(string strClassID)
        {
            return publicDbOpClass.DataTableQuary("SELECT * FROM Prj_FilesClass_v WHERE ClassID='" + strClassID + "'");
        }

        public DataTable GetFilesClassCount(string strClassID)
        {
            return publicDbOpClass.DataTableQuary("SELECT * FROM Prj_FilesClass_v WHERE ParentClassID='" + strClassID + "'");
        }

        public static int GetHaveFileCount(string ClassID, string PrjCode)
        {
            return int.Parse(publicDbOpClass.ExecuteScalar(("select isnull(Count(1),0) from Prj_Files where ClassID='" + ClassID) + "' and PriCode='" + PrjCode + "'").ToString());
        }

        public static string GetPClassID(string ClassID)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("select ParentClassID from Prj_FilesClass where ClassID='" + ClassID + "'");
            if (obj2 != null)
            {
                return obj2.ToString();
            }
            return "";
        }

        public ProjectFilesUpInfo GetProjectFilseUpDataRow(DataRow dr)
        {
            return new ProjectFilesUpInfo { FilesID = dr["FilesID"].ToString(), PriCode = dr["PriCode"].ToString(), FilesName = dr["FilesName"].ToString(), ArchivingID = dr["ArchivingID"].ToString(), ArchivingCode = dr["ArchivingCode"].ToString(), JuanCode = dr["JuanCode"].ToString(), ArchivingName = dr["ArchivingName"].ToString(), IsArchiving = (bool) dr["IsArchiving"], SaveAddress = dr["SaveAddress"].ToString(), ImagingUrl = dr["ImagingPath"].ToString(),  ClassID = (Guid) dr["ClassID"], Remark = dr["Remark"].ToString() };
        }

        public int GetRecordCount(string strWhere)
        {
            string sqlString = "select * from Prj_Files ";
            if (strWhere.Length > 0)
            {
                sqlString = sqlString + " where " + strWhere;
            }
            return publicDbOpClass.DataTableQuary(sqlString).Rows.Count;
        }

        public ProjectFilesUpInfo GetSavePathDataRow(DataRow dr)
        {
            return new ProjectFilesUpInfo { SavePath = dr["FilePath"].ToString() };
        }

        public static DataTable GetTypeInfo(string classId)
        {
            string str = "";
            str = "select className,ClassId,ParentClassId,IsValid from Prj_FilesClass ";
            return publicDbOpClass.DataTableQuary((((str + " where ClassID='" + classId + "' and isvalid=1 union") + " select a.className,a.ClassId,a.parentClassid,isvalid from Prj_FilesClass a,Prj_DocumentBaseInfo b") + " where a.parentClassID='" + classId + "'") + " and  b.ClassIds like '%'+cast(a.ClassId as varchar(50))+'%' and isvalid=1" + " order by parentClassId asc");
        }

        public static DataTable GetTypeInfo(string PrjCode, string classId)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from Prj_f_GetAllByClass('" + classId + "','" + PrjCode + "')");
            DataTable table2 = publicDbOpClass.DataTableQuary("select className,ClassId,'' as PClassId,IsValid from Prj_FilesClass where ClassId='" + classId + "'");
            if (table2.Rows.Count > 0)
            {
                DataRow row = table.NewRow();
                row[0] = table2.Rows[0][0].ToString();
                row[1] = table2.Rows[0][1].ToString();
                row[2] = table2.Rows[0][2].ToString();
                row[3] = table2.Rows[0][3].ToString();
                table.Rows.InsertAt(row, 0);
            }
            table2.Dispose();
            return table;
        }

        public bool InsertFilesClass(string ClassID, string ParentClassID, string ClassName, string Remark, int IsValid, int Id_xh, string ClassOpters)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "INSERT INTO Prj_FilesClass VALUES('", ClassID, "','", ParentClassID, "','", ClassName, "','", Remark, "','", IsValid, "','", Id_xh, "','", ClassOpters, "')" }));
        }

        public DataTable ProjectFilesPage(int iPageSize, int iCurrentPage, int iOrder, string strWhere)
        {
            return publicDbOpClass.GetRecordFromPage("Prj_Files", "Id_xh", iPageSize, iCurrentPage, iOrder, strWhere);
        }

        public int ProjectfilesUp(ProjectFilesUpInfo pfui, string FilesID)
        {
            string str2 = "update Prj_Files set FilesName = '" + pfui.FilesName.ToString() + "',ArchivingID='" + pfui.ArchivingID.ToString() + "',";
            object obj2 = str2 + " ArchivingCode='" + pfui.ArchivingCode.ToString() + "',JuanCode = '" + pfui.JuanCode.ToString() + "',ArchivingName = '" + pfui.ArchivingName.ToString() + "', ";
            string str3 = string.Concat(new object[] { obj2, " SaveAddress = '", pfui.SaveAddress.ToString(), "', IsArchiving = ", pfui.IsArchiving ? 1 : 0, ",ClassID = '", pfui.ClassID.ToString(), "'" });
            return publicDbOpClass.ExecSqlString((str3 + ",Remark='" + pfui.Remark + "',ImagingPath='" + pfui.ImagingUrl + "' ") + "where FilesID = '" + FilesID + "'");
        }

        public int ProjectfilesUpInsert(ProjectFilesUpInfo pfui)
        {
            string str = "insert into Prj_Files (FilesID,FilesName,PriCode,ArchivingID,ArchivingCode,JuanCode,ArchivingName,IsArchiving,SaveAddress,ClassID,Remark,Actor,ScheduleCode,ImagingPath)";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { 
                obj2, "values('", pfui.FilesID, "','", pfui.FilesName, "','", pfui.PriCode, "','", pfui.ArchivingID, "','", pfui.ArchivingCode, "','", pfui.JuanCode, "','", pfui.ArchivingName, "',0,'", 
                pfui.SaveAddress, "','", pfui.ClassID, "','", pfui.Remark, "','", pfui.Actor, "','", pfui.ScheduleCode, "','", pfui.ImagingUrl, "') "
             }));
        }

        public int ProjectFilseUpDel(string FilesID)
        {
            return publicDbOpClass.ExecSqlString("delete Prj_Files where FilesID ='" + FilesID + "'");
        }

        public ProjectFilesUpInfo ProjectFilseUpSel(string FilesId)
        {
            string sqlString = "select * from Prj_Files ";
            if (FilesId != "")
            {
                sqlString = sqlString + " where FilesId = '" + FilesId + "'";
            }
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                return this.GetProjectFilseUpDataRow(table.Rows[0]);
            }
        }

        public static DataTable SecurityFilesClassList()
        {
            string sqlString = "select * from Prj_FilesClass where IsValid=1";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static bool SetBaseInfo(string PrjCode, string ClassIDs)
        {
            string sqlString = "";
            if (publicDbOpClass.GetRecordCount("Prj_DocumentBaseInfo", "PrjCode='" + PrjCode + "'") > 0)
            {
                sqlString = ("update Prj_DocumentBaseInfo set ClassIDs='" + ClassIDs) + "' where PrjCode='" + PrjCode + "'";
            }
            else
            {
                sqlString = string.Format("insert into Prj_DocumentBaseInfo(PrjCode,ClassIDs) values('{0}','{1}')", PrjCode, ClassIDs);
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool UpdateFilesClass(string ClassName, string Remark, string ClassOpters, string Id_xh)
        {
            return publicDbOpClass.NonQuerySqlString("UPDATE Prj_FilesClass SET ClassName='" + ClassName + "',Remark='" + Remark + "',ClassOpters='" + ClassOpters + "' WHERE Id_xh='" + Id_xh + "'");
        }
    }
}

