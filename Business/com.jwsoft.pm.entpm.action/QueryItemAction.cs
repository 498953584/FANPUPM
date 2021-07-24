namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class QueryItemAction
    {
        public static DataTable CodeInfoList(string typeID)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + " select * from XPM_Basic_CodeList where (TypeID=" + typeID.ToString() + ") and (IsVisible=1) and(IsValid=1)");
        }

        public static DataTable GetMatCodeList(string strwhere)
        {
            string str = "";
            str = " select * from JT_MaterialCodeApply  ";
            return publicDbOpClass.DataTableQuary(str + strwhere);
        }

        public static DataTable GetReportList(string reportid, string strwhere)
        {
            string sqlString = "";
            sqlString = " select SelectSql,Sortby from Rep_Main where ReportID=" + reportid;
            string str2 = publicDbOpClass.DataTableQuary(sqlString).Rows[0][1].ToString();
            sqlString = publicDbOpClass.DataTableQuary(sqlString).Rows[0][0].ToString();
            return publicDbOpClass.DataTableQuary(sqlString + " " + strwhere + " " + str2);
        }

        public static DataTable GetReportLister(string reportid, string strwhere, string selectList)
        {
            return publicDbOpClass.DataTableQuary(("select * from (SELECT  dbo.EPM_V_Res_ResourceBasic.ResourceName AS 资源名称,  dbo.EPM_Stuff_MaterialInDetial.ResourceCode AS  资源编号,dbo.PT_PrjInfo.PrjName AS 项目名称,  dbo.EPM_Stuff_MaterialInDetial.UnitPrice AS 单价, dbo.EPM_Stuff_MaterialInDetial.Quantity AS 数量,dbo.EPM_Stuff_MaterialInDetial.SumPrice AS 小计, dbo.EPM_V_Res_ResourceBasic.UnitName AS 单位, convert(varchar(10),dbo.EPM_Stuff_MaterialIn.InDate,120) AS 入库日期  FROM    dbo.EPM_Stuff_MaterialIn INNER JOIN  dbo.EPM_Stuff_MaterialInDetial ON dbo.EPM_Stuff_MaterialIn.StockInCode = dbo.EPM_Stuff_MaterialInDetial.StockInCode INNER JOIN    dbo.EPM_V_Res_ResourceBasic ON dbo.EPM_Stuff_MaterialInDetial.ResourceCode = dbo.EPM_V_Res_ResourceBasic.ResourceCode INNER JOIN   dbo.PT_PrjInfo ON dbo.EPM_Stuff_MaterialIn.ProjectCode =dbo.PT_PrjInfo.PrjGuid where EPM_Stuff_MaterialIn.inaddr='" + selectList + "') a  ") + " " + strwhere);
        }

        public static DataTable GetReportPList(string reportid, string strwhere)
        {
            return publicDbOpClass.DataTableQuary(publicDbOpClass.DataTableQuary(" select SelectSql from Rep_Main where ReportID=" + reportid).Rows[0][0].ToString() + " " + strwhere);
        }

        public static DataRow GetReportRow(string reportid)
        {
            return publicDbOpClass.DataTableQuary(" select Title,headwidth,Header,Footer from Rep_Main where ReportID=" + reportid).Rows[0];
        }

        public static string GetReportTitle(string reportid)
        {
            string sqlString = "";
            sqlString = " select Title from Rep_Main where ReportID=" + reportid;
            try
            {
                return publicDbOpClass.DataTableQuary(sqlString).Rows[0][0].ToString();
            }
            catch
            {
                return "";
            }
        }

        public static int GetReportWidth(string reportid)
        {
            string sqlString = "";
            sqlString = " select headwidth from Rep_Main where ReportID=" + reportid;
            try
            {
                return Convert.ToInt32(publicDbOpClass.DataTableQuary(sqlString).Rows[0][0].ToString());
            }
            catch
            {
                return 0x5dc;
            }
        }

        public static DataTable QueryValidItems(int reportid)
        {
            return publicDbOpClass.DataTableQuary("select * from Rep_MainVar where ReportId=" + reportid.ToString() + " order by termid asc");
        }

        public static DataTable SetQuertItem()
        {
            DataTable table = new DataTable {
                TableName = "Rep_MainVar"
            };
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("ItemSign", typeof(string));
            table.Columns.Add("ItemValue", typeof(string));
            table.Columns.Add("DataType", typeof(int));
            table.Columns.Add("IsEmpty", typeof(bool));
            return table;
        }
    }
}

