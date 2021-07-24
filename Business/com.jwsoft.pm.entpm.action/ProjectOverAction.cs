namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ProjectOverAction
    {
        public static int AddDocument(ProjectDocumentInfo pdi)
        {
            string str2 = "insert into EPM_Prj_DocumentBase values('" + pdi.DocumentCode.ToString() + "','" + pdi.ProjectCode.ToString() + "',";
            string str3 = str2 + "'" + pdi.FileCode + "','" + pdi.RollCode + "'," + pdi.ClassCode.ToString() + ",'" + pdi.Storage + "','" + pdi.DocumentName + "',";
            return publicDbOpClass.ExecSqlString(str3 + "'" + pdi.Remark + "'," + pdi.State.ToString() + ",'" + pdi.AddDate.ToShortDateString() + "')");
        }

        public static int DelDocument(Guid docCode)
        {
            return publicDbOpClass.ExecSqlString("delete from EPM_Prj_DocumentBase where DocumentCode='" + docCode.ToString() + "'");
        }

        private static ProjectDocumentInfo GetDocumentInfoFromDataRow(DataRow dr)
        {
            return new ProjectDocumentInfo { DocumentCode = (Guid) dr["DocumentCode"], ProjectCode = (Guid) dr["ProjectCode"], DocumentName = dr["DocumentName"].ToString(), FileCode = dr["FileCode"].ToString(), RollCode = dr["RollCode"].ToString(), Storage = dr["Storage"].ToString(), State = (int) dr["State"], Remark = dr["Remark"].ToString(), ClassCode = (int) dr["ClassCode"], AddDate = (DateTime) dr["AddDate"] };
        }

        public static ProjectDocumentInfo GetOneDocumentInfo(Guid docCode)
        {
            ProjectDocumentInfo documentInfoFromDataRow = new ProjectDocumentInfo();
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Prj_DocumentBase where DocumentCode='" + docCode.ToString() + "'"))
            {
                if (table.Rows.Count == 1)
                {
                    documentInfoFromDataRow = GetDocumentInfoFromDataRow(table.Rows[0]);
                }
            }
            return documentInfoFromDataRow;
        }

        public static string GetSqlWhere(string projectCode, int term, string inputText, int classCode)
        {
            string str = "(ProjectCode = '" + projectCode + "')";
            switch (term)
            {
                case 1:
                    str = str + " and (DocumentName like '%" + inputText + "%')";
                    break;

                case 2:
                    str = str + " and (FileCode like '%" + inputText + "%')";
                    break;

                case 3:
                    str = str + " and (RollCode like '%" + inputText + "%')";
                    break;

                case 4:
                    str = str + " and (Storage like '%" + inputText + "%')";
                    break;
            }
            if (classCode != 0x270f)
            {
                str = str + " and (ClassCode = " + classCode.ToString() + ")";
            }
            return str;
        }

        public static DataTable QueryProjectDocumentList(string sqlWhere)
        {
            return publicDbOpClass.GetPageData(sqlWhere, "EPM_V_Prj_DocumentList", "AddDate desc");
        }

        public static int UpdDocument(ProjectDocumentInfo pdi)
        {
            string str2 = " update EPM_Prj_DocumentBase set FileCode = '" + pdi.FileCode + "',RollCode = '" + pdi.RollCode + "',";
            string str3 = str2 + "ClassCode=" + pdi.ClassCode.ToString() + ",Storage='" + pdi.Storage + "',DocumentName='" + pdi.DocumentName + "',";
            return publicDbOpClass.ExecSqlString(str3 + "Remark='" + pdi.Remark + "' where DocumentCode = '" + pdi.DocumentCode.ToString() + "'");
        }
    }
}

