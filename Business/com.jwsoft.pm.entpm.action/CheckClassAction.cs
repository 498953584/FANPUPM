namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class CheckClassAction
    {
        public static bool CheckClassInfoOp(CheckClassInfo objinfo, string OpType)
        {
            string sqlString = "";
            if (OpType == "Insert")
            {
                sqlString = "Insert into Prj_ItemInspectSort(ItemInspectSortName,Remark) values('" + objinfo.ItemInspectSortName + "','" + objinfo.Remark + "')";
            }
            else if (OpType == "Update")
            {
                sqlString = string.Concat(new object[] { "update Prj_ItemInspectSort set ItemInspectSortName='", objinfo.ItemInspectSortName, "',Remark='", objinfo.Remark, "' where SortID='", objinfo.SortID, "'" });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool Delete(string pk)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Prj_ItemInspectSort where SortID='" + pk + "'");
        }

        public static DataTable GetAllCheckClassCollections()
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_ItemInspectSort");
        }

        public static DataTable GetCheckClassCollections(string strwhere)
        {
            return publicDbOpClass.GetPageData(strwhere, "Prj_ItemInspectSort", "SortID asc");
        }

        public static int GetCheckClassCount()
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_ItemInspectSort ");
        }

        public static CheckClassInfo GetCheckClassInfo(string SortID)
        {
            CheckClassInfo info = new CheckClassInfo();
            DataTable table = publicDbOpClass.DataTableQuary("select * from Prj_ItemInspectSort where SortID=" + SortID);
            if (table.Rows[0]["SortID"].ToString() != "")
            {
                info.SortID = int.Parse(table.Rows[0]["SortID"].ToString());
            }
            info.ItemInspectSortName = table.Rows[0]["ItemInspectSortName"].ToString();
            info.Remark = table.Rows[0]["Remark"].ToString();
            return info;
        }
    }
}

