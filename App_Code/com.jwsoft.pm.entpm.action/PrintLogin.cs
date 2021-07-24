namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class PrintLogin
    {
        public static DataTable PintLoginDT(string strWhere)
        {
            string str = "select pt_xtrz.*,pt_yhmc.v_xm from pt_xtrz,pt_yhmc where pt_xtrz.v_yhdm = pt_yhmc.v_yhdm ";
            if (strWhere != "")
            {
                str = str + strWhere;
            }
            return publicDbOpClass.DataTableQuary(str + "order by i_rzid desc");
        }
    }
}

