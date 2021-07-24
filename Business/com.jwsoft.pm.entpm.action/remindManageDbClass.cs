namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class remindManageDbClass
    {
        private string _messageString = "";

        public bool SysRemindAdd(string strModuleCode, string strUserCode, int iRecID, string strRemindPara)
        {
            string str = "";
            string str2 = "";
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_dbmk where c_mkdm=" + strModuleCode);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["v_tplj"].ToString();
                str2 = table.Rows[1]["v_dblj"].ToString() + strRemindPara;
            }
            else
            {
                this._messageString = "该模块不具有督办功能！";
                return false;
            }
            int num = 0;
            num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_dbsj", "i_dbsj_id")) + 1;
            num++;
            if (publicDbOpClass.NonQuerySqlString("insert into pt_dbsj(i_dbsj_id,i_xgid,v_yhdm,c_mkdm,dtm_dbsj,v_tplj,v_dblj) values(" + num.ToString() + "," + iRecID.ToString() + ",'" + strUserCode + "','" + strModuleCode + "',Sysdate,'" + str + "','" + str2 + "')"))
            {
                return true;
            }
            this._messageString = "设置督办失败！";
            return false;
        }

        public bool SysRemindDel(string strSysBulCode)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_dbsj where i_dbsj_id= " + Convert.ToInt32(strSysBulCode));
        }

        public bool SysRemindDel(int strEventId, string strTypeCode)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "delete from pt_dbsj where i_xgid = ", Convert.ToInt32(strEventId), " and v_lxbm = '", strTypeCode, "'" }));
        }

        public bool SysRemindDel(string strUserCode, string strModuleCode)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_dbsj where i_dbsj_id = (select min(i_dbsj_id) from pt_dbsj where (v_yhdm='" + strUserCode + "') and (c_mkdm='" + strModuleCode + "'))");
        }

        public DataTable SysRemindInfoQuery(string strSysRCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_dbsj where i_dbsj_id='" + strSysRCode + "'");
        }

        public DataTable SysRemindQuery(string strUserCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_dbsj where v_yhdm='" + strUserCode + "'");
        }

        public string MessageString
        {
            get
            {
                return this._messageString;
            }
        }
    }
}

