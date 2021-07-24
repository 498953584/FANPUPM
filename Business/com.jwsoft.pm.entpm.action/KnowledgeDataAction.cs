namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class KnowledgeDataAction
    {
        public static int AddNewData(KnowledgeDataModel kdm)
        {
            object obj2 = " insert into EPM_Datum_Data (KnowledgeCode,DatumName,Author,Viscera,AddDate,UpdateDate,AuditingDate,IsValid,IsAuditing,TypeId,ProjectCode) values ('" + kdm.KnowledgeCode + "', ";
            object obj3 = string.Concat(new object[] { obj2, " '", kdm.DatumName, "','", kdm.Author, "','", kdm.Viscera, "','", kdm.AddDate, "', " });
            object obj4 = string.Concat(new object[] { obj3, " '", kdm.UpdateDate, "','", kdm.AuditingDate, "','", kdm.IsValid, "'," });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, " '", kdm.IsAuditing, "','", kdm.TypeId, "','", kdm.ProjectCode, "')" }));
        }

        public int DatumAffirm(string KnowledgeCode, string date, string yhdm, string info)
        {
            string str2 = " update EPM_Datum_Data set AffirmDateTime = '" + date + "',";
            return publicDbOpClass.ExecSqlString((str2 + " Affirmyhdm='" + yhdm + "',AffirmInfo='" + info + "',AffirmState=1") + " where KnowledgeCode = '" + KnowledgeCode + "' ");
        }

        public static int DelData(Guid Code)
        {
            object obj2 = " delete from XPM_Basic_AnnexList  where RecordCode = '" + Code + "'";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " delete from EPM_Datum_Data where KnowledgeCode = '", Code, "'" }));
        }

        public void DelFiles(Guid KnowledgeCode)
        {
            string str = " select replace(FilePath,'','/') as FilePath,AnnexCode from XPM_Basic_AnnexList ";
            object obj2 = str;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where RecordCode = '", KnowledgeCode, "'  " }));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (KnowledgeFileUp.DelFile(table.Rows[0][0].ToString()) != "3")
                {
                    KnowledgeFileAction.DleFile(table.Rows[0][1].ToString());
                }
            }
        }

        public static int GetDatumCount(string TypeId, string IsValid)
        {
            string str = " select Count(1) from EPM_Datum_Data where ";
            return (int) publicDbOpClass.ExecuteScalar(str + GetSqlWhere(TypeId, IsValid));
        }

        public static int GetDatumCount(string TypeId, string IsValid, string SqlWhere)
        {
            string str = " select Count(1) from EPM_Datum_Data where ";
            return (int) publicDbOpClass.ExecuteScalar(str + GetSqlWhere(TypeId, IsValid) + SqlWhere);
        }

        public static DataTable GetDatumList(string TypeId, string IsValid, int pageSize, int pageIndex)
        {
            string sqlWhere = GetSqlWhere(TypeId, IsValid);
            return publicDbOpClass.GetRecordFromPage("EPM_Datum_Data", "AddDate", pageSize, pageIndex, 1, sqlWhere);
        }

        public static DataTable GetDatumList(string TypeId, string IsValid, int pageSize, int pageIndex, string SqlWhere)
        {
            string strWhere = GetSqlWhere(TypeId, IsValid) + SqlWhere;
            return publicDbOpClass.GetRecordFromPage("EPM_Datum_Data", "AddDate", pageSize, pageIndex, 1, strWhere);
        }

        private static KnowledgeDataModel GetKnowledgeInfoFromDataRow(DataRow dr)
        {
            return new KnowledgeDataModel { KnowledgeCode = new Guid(dr["KnowledgeCode"].ToString()), DatumName = dr["DatumName"].ToString(), Author = dr["Author"].ToString(), Viscera = dr["Viscera"].ToString(), AddDate = (DateTime) dr["AddDate"], IsValid = dr["IsValid"].ToString(), UpdateDate = (DateTime) dr["UpdateDate"], AuditingDate = (DateTime) dr["AuditingDate"], IsAuditing = dr["IsAuditing"].ToString(), TypeId = (int) dr["TypeId"], ProjectCode = new Guid(dr["ProjectCode"].ToString()), AffirmDateTime = (dr["AffirmDateTime"] == DBNull.Value) ? DateTime.Now : ((DateTime) dr["AffirmDateTime"]), AffirmInfo = dr["AffirmInfo"].ToString() };
        }

        public static KnowledgeDataModel GetSingleValue(Guid Code)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_Datum_Data where KnowledgeCode = '" + Code + "'"))
            {
                return GetKnowledgeInfoFromDataRow(table.Rows[0]);
            }
        }

        public static string GetSqlWhere(string TypeId, string IsValid)
        {
            string str = "(1=1) ";
            if (TypeId.Trim() == "")
            {
                str = str ?? "";
            }
            else
            {
                str = str + " and TypeId = '" + TypeId + "'";
            }
            if (IsValid.Trim() == "")
            {
                return (str ?? "");
            }
            return (str + " and IsValid = '" + IsValid + "'");
        }

        public static int UpdateData(KnowledgeDataModel kdm)
        {
            object obj2 = " update EPM_Datum_Data set DatumName = '" + kdm.DatumName + "',Author = '" + kdm.Author + "',";
            object obj3 = string.Concat(new object[] { obj2, " Viscera = '", kdm.Viscera, "',UpdateDate = '", kdm.UpdateDate, "',IsValid = '", kdm.IsValid, "'," }) + "TypeId = " + kdm.TypeId;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, " where KnowledgeCode = '", kdm.KnowledgeCode, "' " }));
        }
    }
}

