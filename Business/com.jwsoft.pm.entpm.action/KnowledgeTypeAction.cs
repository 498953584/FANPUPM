namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class KnowledgeTypeAction
    {
        public static int AddNewType(KnowledgeTypeModel ktm)
        {
            object obj2 = " insert into EPM_Datum_Class (TypeName,Remark,ParentId,IsValid,isDelete,IsVisible,IsFixup)values ('" + ktm.TypeName + "','" + ktm.Remark + "', ";
            string str2 = string.Concat(new object[] { obj2, " '", ktm.ParentId, "',", ktm.IsValid, ",", ktm.isDelete, ", " });
            return publicDbOpClass.ExecSqlString(str2 + " " + ktm.IsVisible + "," + ktm.IsFixup + ")");
        }

        public static int DelType(string TypeId)
        {
            return publicDbOpClass.ExecSqlString((("delete EPM_Datum_Data where TypeId= " + TypeId) + " delete from EPM_Datum_Class where TypeId = '" + TypeId + "'") + " delete from EPM_Datum_Class where ParentId = '" + TypeId + "'");
        }

        public static int GetClassCount(string Type, string Valid)
        {
            string str = " select count(1) from EPM_Datum_Class where";
            return (int) publicDbOpClass.ExecuteScalar(str + GetSqlWhere(Type, Valid));
        }

        public static KnowledgeTypeModel GetSingleType(string ID)
        {
            using (DataTable table = GetType(ID))
            {
                return GetTypeInfoFromDataRow(table.Rows[0]);
            }
        }

        private static string GetSqlWhere(string Type, string Valid)
        {
            string str = " (1=1) ";
            if (Type.Trim() != "")
            {
                str = str + " and ParentId = '" + Type + "' ";
            }
            else
            {
                str = str ?? "";
            }
            if (Valid.Trim() != "")
            {
                return (str + " and isValid = '" + Valid + "' ");
            }
            return (str ?? "");
        }

        public static DataTable GetType()
        {
            string sqlString = " select * from EPM_Datum_Class  WHERE TypeId<>1 order by TypeId asc";
            return publicDbOpClass.DataTableQuary(sqlString);
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

        public static DataTable GetTypeList(string Type, string Valid, int pageSize, int pageIndex)
        {
            string sqlWhere = GetSqlWhere(Type, Valid);
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

