namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ProgSortAction
    {
        public static bool Delete(string pk)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Prj_ProgSort where ProgSortCode='" + pk + "'");
        }

        public static DataTable GetAllProgSortCollections()
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_ProgSort");
        }

        public static DataTable GetProgSortCollections(string strwhere)
        {
            return publicDbOpClass.GetPageData(strwhere, "Prj_ProgSort", "ProgSortCode desc");
        }

        public static int GetProgSortCount()
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from Prj_ProgSort ");
        }

        public static ProgSortInfo GetProgSortInfo(string ProgSortCode)
        {
            ProgSortInfo info = new ProgSortInfo();
            DataTable table = publicDbOpClass.DataTableQuary("select * from Prj_ProgSort where ProgSortCode=" + ProgSortCode);
            if (table.Rows[0]["ProgSortCode"].ToString() != "")
            {
                info.ProgSortCode = int.Parse(table.Rows[0]["ProgSortCode"].ToString());
            }
            info.ProgSortName = table.Rows[0]["ProgSortName"].ToString();
            info.Remark = table.Rows[0]["Remark"].ToString();
            return info;
        }

        public static bool ProgSortInfoOp(ProgSortInfo objinfo, string OpType)
        {
            string sqlString = "";
            if (OpType == "Insert")
            {
                sqlString = "Insert into Prj_ProgSort(ProgSortName,Remark) values('" + objinfo.ProgSortName + "','" + objinfo.Remark + "')";
            }
            else if (OpType == "Update")
            {
                sqlString = string.Concat(new object[] { "update Prj_ProgSort set ProgSortName='", objinfo.ProgSortName, "',Remark='", objinfo.Remark, "' where ProgSortCode='", objinfo.ProgSortCode, "'" });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }
    }
}

