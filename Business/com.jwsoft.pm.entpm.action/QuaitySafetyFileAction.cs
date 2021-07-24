namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class QuaitySafetyFileAction
    {
        public static int AddNewFile(QualitySafeFileModel qsfm)
        {
            object obj2 = " insert into XPM_Basic_AnnexList values ('" + qsfm.AnnexCode + "', ";
            object obj3 = string.Concat(new object[] { obj2, " '", qsfm.ModuleID, "','", qsfm.RecordCode, "','", qsfm.AnnexType, "','", qsfm.FileCode, "'," });
            object obj4 = string.Concat(new object[] { obj3, " '", qsfm.AnnexName, "','", qsfm.OriginalName, "','", qsfm.FilePath, "','", qsfm.FileSize, "'," });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, " '", qsfm.AddDate, "','", qsfm.State, "','", qsfm.HumanCode, "','", qsfm.Remark, "','", qsfm.ProjectCode, "')" }));
        }

        public static int DleFile(string Code)
        {
            return publicDbOpClass.ExecSqlString(" delete from XPM_Basic_AnnexList where AnnexCode = '" + Code + "'");
        }

        public static DataTable GetFileList(Guid Code, string ID)
        {
            string str = @" select *,replace(FilePath,'\','/')FilePath1 from XPM_Basic_AnnexList ";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where RecordCode = '", Code, "' and ModuleID = '", ID, "' " }));
        }
    }
}

