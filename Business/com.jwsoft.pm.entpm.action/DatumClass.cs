namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class DatumClass
    {
        public static int AddNewType(KnowledgeTypeModel ktm)
        {
            object obj2 = " insert into EPM_Datum_Class values ('" + ktm.TypeName + "','" + ktm.Remark + "', ";
            string str2 = string.Concat(new object[] { obj2, " '", ktm.ParentId, "',", ktm.IsValid, ",", ktm.isDelete, ", " });
            return publicDbOpClass.ExecSqlString(str2 + " " + ktm.IsVisible + "," + ktm.IsFixup + ")");
        }

        public static int DelType(string TypeId)
        {
            return publicDbOpClass.ExecSqlString((" delete from EPM_Datum_Class where TypeId = '" + TypeId + "'") + " delete from KnowledgeType where ParentId = '" + TypeId + "'");
        }

        public static int GetClassCount(string Class, string Type)
        {
            string str = " select count(1) from EPM_Datum_Class where";
            return (int) publicDbOpClass.ExecuteScalar(str + GetSqlWhere(Class, Type));
        }

        public static KnowledgeTypeModel GetSingleType(string ID)
        {
            using (DataTable table = GetType(ID))
            {
                return GetTypeInfoFromDataRow(table.Rows[0]);
            }
        }

        private static string GetSqlWhere(string Class, string Type)
        {
            string str = " (isValid = '1' and ParentId <>1 or TypeId ='" + Class + "')";
            if (Type.Trim() != "")
            {
                return (str + " and ParentId = '" + Type + "' ");
            }
            return (str ?? "");
        }

        public static DataTable GetType(string ID)
        {
            string str = "  ";
            if (ID.Trim() != "")
            {
                str = "  where TypeId = '" + ID + "'";
            }
            else
            {
                str = "";
            }
            string str2 = " select * from EPM_Datum_Class  ";
            return publicDbOpClass.DataTableQuary(str2 + str + " order by TypeId asc");
        }

        private static KnowledgeTypeModel GetTypeInfoFromDataRow(DataRow dr)
        {
            return new KnowledgeTypeModel { TypeId = (int) dr["TypeId"], TypeName = dr["TypeName"].ToString(), Remark = dr["Remark"].ToString(), isDelete = dr["isDelete"].ToString(), IsFixup = dr["IsFixup"].ToString(), IsValid = dr["IsValid"].ToString(), IsVisible = dr["IsVisible"].ToString() };
        }

        public static DataTable GetTypeList(string Class, string Type, int pageSize, int pageIndex)
        {
            string sqlWhere = GetSqlWhere(Class, Type);
            return publicDbOpClass.GetRecordFromPage("EPM_Datum_Class", "TypeId", pageSize, pageIndex, 0, sqlWhere);
        }

        public static int UpdateType(KnowledgeTypeModel ktm)
        {
            string str2 = " update EPM_Datum_Class set TypeName = '" + ktm.TypeName + "', ";
            string str3 = str2 + " Remark = '" + ktm.Remark + "',IsValid = " + ktm.IsValid + ", isDelete = " + ktm.isDelete + ", ";
            object obj2 = str3 + " IsVisible = " + ktm.IsVisible + ",IsFixup = " + ktm.IsFixup;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " where TypeId = '", ktm.TypeId, "'" }));
        }
    }
}

