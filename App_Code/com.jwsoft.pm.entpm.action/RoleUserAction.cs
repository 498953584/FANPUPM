namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class RoleUserAction
    {
        public DataTable AllDepNoRoleUser(string yhdm, string RoleCode)
        {
            return publicDbOpClass.DataTableQuary("SELECT    a.v_xm, a.v_yhdm, b.v_bmbm FROM PT_yhmc AS a INNER JOIN dbo.PT_d_bm AS b ON a.i_bmdm = b.i_bmdm   WHERE  a.v_xm like '%" + yhdm + "%' AND (a.c_sfyx = 'y') and  a.v_yhdm not in(select v_yhdm from pt_login where c_sfyx = 'y'and charindex('" + RoleCode + ",',RoleCodeS)>0 ) ORDER BY a.v_xm");
        }

        public DataSet DepNoRoleUser(string bmdm, string RoleCode)
        {
            if (bmdm == "0")
            {
                bmdm = "1";
            }
            return publicDbOpClass.DataSetQuary("SELECT    a.v_xm, a.v_yhdm, b.v_bmbm FROM PT_yhmc AS a INNER JOIN dbo.PT_d_bm AS b ON a.i_bmdm = b.i_bmdm   WHERE  (b.i_bmdm = '" + bmdm + "') AND (a.c_sfyx = 'y') and  a.v_yhdm not in(select v_yhdm from pt_login where c_sfyx = 'y'and charindex('" + RoleCode + ",',RoleCodeS)>0 ) ORDER BY a.v_xm");
        }

        public DataTable GetHumBYGroupID(string GroupID)
        {
            return publicDbOpClass.DataTableQuary("SELECT [v_yhdm],[v_xm],[i_bmdm] FROM [PT_yhmc] WHERE  v_yhdm IN  (select v_yhdm from PT_GroupHum  WHERE GroupID=" + GroupID + " ) order by v_xm ");
        }

        public DataTable GetHumListBYRoleID(string RoleCode, string strwhere)
        {
            return publicDbOpClass.DataTableQuary("  SELECT   v_xm, V_BMMC,v_yhdm ,'" + RoleCode + "' AS RoleCode  FROM v_UserInfoList   WHERE  v_yhdm in (select v_yhdm from pt_login where  charindex('" + RoleCode + "',RoleCodeS)>0 )   " + strwhere + "  order by i_bmdm, v_xm ");
        }

        public int RoleUserDel(string RoleCode, string v_yhdm)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("  select top 1 RoleCodes from  pt_login   WHERE  v_yhdm= " + v_yhdm);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0][0].ToString();
            }
            int num = 0;
            if (str.Length > 1)
            {
                num = publicDbOpClass.ExecuteSQL("  update pt_login set RoleCodes='" + str.Replace(RoleCode + ',', "") + "'  WHERE  v_yhdm= " + v_yhdm);
            }
            return num;
        }

        public int RoleUserUpdate(string RoleCode, string v_yhdm)
        {
            return publicDbOpClass.ExecuteSQL("  update pt_login set RoleCodes=isnull(RoleCodes,'')+'" + RoleCode + ",'  WHERE  charindex(v_yhdm,'" + v_yhdm + "')>0  ");
        }

        public int SaveOrder(int rolecode, int orderID)
        {
            return publicDbOpClass.ExecuteSQL(string.Concat(new object[] { "update pt_role set I_xh=", orderID, " where rolecode=", rolecode }));
        }
    }
}

