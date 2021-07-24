namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class workLogDb
    {
        private DateTime currentDate = DateTime.Today;

        public string chineseWeek(string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "Sunday":
                    return "星期日";

                case "Monday":
                    return "星期一";

                case "Tuesday":
                    return "星期二";

                case "Wednesday":
                    return "星期三";

                case "Thursday":
                    return "星期四";

                case "Friday":
                    return "星期五";

                case "Saturday":
                    return "星期六";
            }
            return "";
        }

        public bool dateAdd(string startStr, string endStr)
        {
            string str4;
            string sqlString = "select id from pt_gzrz_kq";
            DataTable table = new DataTable();
            if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
            {
                string str2;
                if (endStr.Length > 0)
                {
                    str2 = "update pt_gzrz_kq set dtm_kq = '" + startStr + "',dtm_kqe = '" + endStr + "'";
                }
                else
                {
                    str2 = "update pt_gzrz_kq set dtm_kq = '" + startStr + "' , dtm_kqe=null";
                }
                str4 = str2;
            }
            else
            {
                string str;
                int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_gzrz_kq", "id")) + 1;
                if (endStr.Length > 0)
                {
                    str = string.Concat(new object[] { "insert into pt_gzrz_kq (dtm_kq,dtm_kqe,id) values ('", startStr, "','", endStr, "',", num, ")" });
                }
                else
                {
                    str = string.Concat(new object[] { "insert into pt_gzrz_kq (dtm_kq,id) values ('", startStr, "',", num, ")" });
                }
                str4 = str;
            }
            return publicDbOpClass.NonQuerySqlString(str4);
        }

        public DataTable getDepTable(int depId)
        {
            DataTable table = new DataTable();
            return publicDbOpClass.DataTableQuary("select pt_yhmc.v_yhdm,pt_yhmc.v_xm from pt_yhmc where pt_yhmc.c_sfyx = 'y' and pt_yhmc.i_bmdm = " + depId + " order by pt_yhmc.i_xh asc");
        }

        public int getLogNum(string startStr, string endStr, string yhdm, char availability)
        {
            int num = 0;
            DataTable table = new DataTable();
            table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select count(i_gzrz_id) as logNum  from pt_gzrz_nr where v_yhdm = '", yhdm, "' and dtm_zxsj >= '", startStr, "' and dtm_zxsj <= '", endStr, "' and c_bs ='", availability, "'" }));
            if (table.Rows.Count > 0)
            {
                num = Convert.ToInt32(table.Rows[0]["logNum"].ToString());
            }
            return num;
        }

        public DataTable getTable()
        {
            DataTable table = new DataTable();
            string sqlString = "select * from pt_gzrz_kq where id=1";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable getTable(int gzrzId)
        {
            DataTable table = new DataTable();
            return publicDbOpClass.DataTableQuary("select pt_gzrz_nr.*,v_xm from pt_gzrz_nr,pt_yhmc where i_gzrz_id = " + gzrzId + " and pt_gzrz_nr.v_yhdm=pt_yhmc.v_yhdm");
        }

        public DataTable getTable(string yhdm)
        {
            DataTable table = new DataTable();
            return publicDbOpClass.DataTableQuary("select * from pt_gzrz_nr where v_yhdm='" + yhdm + "' order by dtm_zxsj desc");
        }

        public DataTable getTable(string yhdm, DateTime date)
        {
            DataTable table = new DataTable();
            return publicDbOpClass.DataTableQuary("select pt_gzrz_nr.*, v_xm from pt_gzrz_nr,pt_yhmc where pt_gzrz_nr.v_yhdm='" + yhdm + "' and dtm_zxsj = '" + date.ToString("d") + "' and pt_gzrz_nr.v_yhdm=pt_yhmc.v_yhdm");
        }

        public DataTable getTable(string yhdm, string start, string end)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_gzrz_nr where v_yhdm = '" + yhdm + "'and dtm_zxsj >= '" + start + "'and dtm_zxsj <= '" + end + "' order by dtm_zxsj desc");
        }

        public DataTable getTempTable(string yhdm)
        {
            DataTable table = new DataTable();
            string str = this.currentDate.ToString("d");
            return publicDbOpClass.DataTableQuary("select * from pt_gzrz_temp where v_yhdm = '" + yhdm + "' and dtm_zxsj = '" + str + "'");
        }

        public bool logDel(int id)
        {
            return publicDbOpClass.NonQuerySqlString("update pt_gzrz_nr set c_bs = 'n' where i_gzrz_id = " + id);
        }

        public bool logIsExist(string yhdm, string tableName)
        {
            bool flag = false;
            string sqlString = "select i_gzrz_id from " + tableName + " where v_yhdm='" + yhdm + "' and dtm_zxsj='" + this.currentDate.ToString("d") + "'";
            DataTable table = new DataTable();
            if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        public bool workLogAdd(string weatherStr, string logContentStr, string yhdm)
        {
            string str = this.currentDate.ToString("d");
            string str2 = "";
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_gzrz_nr", "i_gzrz_id")) + 1;
            object obj2 = str2 + " begin";
            string str3 = string.Concat(new object[] { obj2, " insert into pt_gzrz_nr (i_gzrz_id,dtm_zxsj,dtm_xgsj,txt_rznr,v_yhdm,c_tq,c_bs) values (", num, ",'", str, "','", str, "','", logContentStr, "','", yhdm, "','", weatherStr, "','y')" });
            return publicDbOpClass.NonQuerySqlString((str3 + " delete pt_gzrz_temp where v_yhdm = '" + yhdm + "' and dtm_zxsj <= '" + str + "'") + " end");
        }

        public bool workLogAddTemp(string weatherStr, string logContentStr, string yhdm)
        {
            string str = this.currentDate.ToString("d");
            if (this.logIsExist(yhdm, "pt_gzrz_nr"))
            {
                return false;
            }
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_gzrz_temp", "i_gzrz_id")) + 1;
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "insert into pt_gzrz_temp (i_gzrz_id,dtm_zxsj,dtm_xgsj,txt_rznr,v_yhdm,c_tq,c_bs) values (", num, ",'", str, "','", str, "','", logContentStr, "','", yhdm, "','", weatherStr, "','y')" }));
        }

        public bool workLogTempUpdate(string weatherStr, string logContentStr, string yhdm)
        {
            string str = this.currentDate.ToString("d");
            return publicDbOpClass.NonQuerySqlString("update pt_gzrz_temp set c_tq='" + weatherStr + "',txt_rznr='" + logContentStr + "' where v_yhdm='" + yhdm + "' and dtm_zxsj='" + str + "'");
        }

        public bool workLogUpdate(string weatherStr, string logContentStr, int gzrzId)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update pt_gzrz_nr set txt_rznr='", logContentStr, "',c_tq='", weatherStr, "' where i_gzrz_id = ", gzrzId }));
        }

        public bool workLogUpdate(string weatherStr, string logContentStr, string yhdm)
        {
            DateTime today = DateTime.Today;
            return publicDbOpClass.NonQuerySqlString("update pt_gzrz_nr set txt_rznr='" + logContentStr + "',c_tq='" + weatherStr + "' where v_yhdm='" + yhdm + "' and dtm_zxsj = '" + today.ToString("d") + "'");
        }
    }
}

