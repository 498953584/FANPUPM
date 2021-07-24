namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.sysManage.publicStringOperation;
    using System;
    using System.Data;

    public class loginLogDb
    {
        public bool delLoginInfo(string date)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_xtrz where dtm_dlsj < '" + date + "'");
        }

        public DataTable loginLogQuery()
        {
            string sqlString = "select pt_xtrz.*,pt_yhmc.v_xm from pt_xtrz,pt_yhmc where pt_xtrz.v_yhdm = pt_yhmc.v_yhdm order by i_rzid desc";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public void UserlLoginInfo(string ipdz, string yhdm)
        {
            ipdz = PublicClass.CheckString(ipdz);
            publicDbOpClass.NonQuerySqlString("insert into pt_xtrz (v_yhdm,v_dlip,dtm_dlsj) values('" + yhdm + "','" + ipdz + "',getdate())");
        }
    }
}

