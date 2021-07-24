namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class MaterialInitAction
    {
        public static DataTable GetResource(string strCategoryCode)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "select a.ResourceCode,a.ResourceName,a.CategoryCode,a.Specification,a.UnitName, b.Quantity,b.Price,b.ModifyDate from EPM_Stuff_Stock b left join v_Res_Resource a on a.ResourceCode=b.ResourceCode where a.CategoryCode='" + strCategoryCode + "'");
        }

        public static bool InitResource()
        {
            string str = "";
            return publicDbOpClass.NonQuerySqlString(str + " insert into EPM_Stuff_Stock select ResourceCode,CategoryCode,0,0 from EPM_Res_Resource where ResourceCode not in (select Resourcecode from EPM_Stuff_Stock)");
        }
    }
}

