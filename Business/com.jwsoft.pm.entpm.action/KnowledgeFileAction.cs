namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class KnowledgeFileAction
    {
        public static int AddNewFile(KnowledgeFileModel kfm)
        {
            object obj2 = " insert into XPM_Basic_AnnexList values ('" + kfm.AnnexCode + "', ";
            object obj3 = string.Concat(new object[] { obj2, " '", kfm.ModuleID, "','", kfm.RecordCode, "','", kfm.AnnexType, "','", kfm.FileCode, "'," });
            object obj4 = string.Concat(new object[] { obj3, " '", kfm.AnnexName, "','", kfm.OriginalName, "','", kfm.FilePath, "','", kfm.FileSize, "'," });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, " '", kfm.AddDate, "','", kfm.State, "','", kfm.HumanCode, "','", kfm.Remark, "','", kfm.ProjectCode, "')" }));
        }

        public static int DleFile(string Code)
        {
            return publicDbOpClass.ExecSqlString(" delete from XPM_Basic_AnnexList where AnnexCode = '" + Code + "'");
        }

        public static DataTable GetFileList(Guid DatumCode)
        {
            string str = @" select *,replace(FilePath,'\','/')FilePath1 from XPM_Basic_AnnexList ";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where RecordCode = '", DatumCode, "' " }));
        }
    }
}

