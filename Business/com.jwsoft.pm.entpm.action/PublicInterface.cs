namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class PublicInterface
    {
        private static string GetReceiverMobileNo(string yhdm)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select MobilePhoneCode from pt_yhmc where v_yhdm = '" + yhdm + "' and c_sfyx = 'y'");
            if ((table.Rows.Count > 0) && (table.Rows[0]["MobilePhoneCode"].ToString().Trim() != ""))
            {
                return table.Rows[0]["MobilePhoneCode"].ToString();
            }
            return "0";
        }

        private static DataTable getTxlxList(string LXBM)
        {
            return publicDbOpClass.DataTableQuary("select * from PT_D_TXLX where V_LXBM = '" + LXBM + "'");
        }

        public static bool InsertSysOpLog()
        {
            return true;
        }

        private static bool IsDays(DateTime dt1, DateTime dt2)
        {
            bool flag = false;
            if (Convert.ToDouble(publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select datediff(dd,'", dt1, "','", dt2, "') as dt" })).Rows[0]["dt"].ToString()) == 0.0)
            {
                flag = true;
            }
            return flag;
        }

        private static int PTDBSJAdd(PTDBSJ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_DBSJ(");
            builder.Append("I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.I_XGID + "',");
            builder.Append("'" + model.V_LXBM + "',");
            builder.Append("'" + model.V_YHDM + "',");
            builder.Append("'" + model.DTM_DBSJ + "',");
            builder.Append("'" + model.V_Content + "',");
            builder.Append("'" + model.V_TPLJ + "',");
            builder.Append("'" + model.V_DBLJ + "',");
            builder.Append("'" + model.C_OpenFlag + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static int PTDBSJDelete(string XGID, string LXBM)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_DBSJ ");
            builder.Append(" where I_XGID='" + XGID + "' and V_LXBM = '" + LXBM + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        private static int PTDBSJTodayAdd(PTDBSJ model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_DBSJ_Today(");
            builder.Append("I_XGID,V_LXBM,V_YHDM,DTM_DBSJ,V_Content,V_TPLJ,V_DBLJ,C_OpenFlag");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.I_XGID + "',");
            builder.Append("'" + model.V_LXBM + "',");
            builder.Append("'" + model.V_YHDM + "',");
            builder.Append("'" + model.DTM_DBSJ + "',");
            builder.Append("'" + model.V_Content + "',");
            builder.Append("'" + model.V_TPLJ + "',");
            builder.Append("'" + model.V_DBLJ + "',");
            builder.Append("'" + model.C_OpenFlag + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static int PTDBSJTodayDelete(string XGID, string LXBM)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_DBSJ_Today ");
            builder.Append(" where I_XGID='" + XGID + "' and V_LXBM = '" + LXBM + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static bool SendRTXMsg()
        {
            return true;
        }

        public static int SendSmsMsg(SMSLog model)
        {
            if (IsDays(DateTime.Now, model.SendTime))
            {
                string receiverMobileNo = GetReceiverMobileNo(model.ReceiveUser);
                if (receiverMobileNo != "0")
                {
                    SMSLogTodayAdd(model, receiverMobileNo);
                }
            }
            return SMSLogAdd(model);
        }

        public static int SendSysMsg(PTDBSJ model)
        {
            DataTable table = getTxlxList(model.V_LXBM);
            if (table.Rows.Count > 0)
            {
                model.V_TPLJ = table.Rows[0]["V_TPLJ"].ToString();
                if (model.V_LXBM != "014")
                {
                    model.V_DBLJ = table.Rows[0]["V_DBLJ"].ToString() + model.V_DBLJ;
                }
                model.C_OpenFlag = table.Rows[0]["C_OpenFlag"].ToString();
            }
            return PTDBSJAdd(model);
        }

        private static int SMSLogAdd(SMSLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_SMSLog(");
            builder.Append("I_XGID,V_LXBM,SendUser,ReceiveUser,Message,SendTime");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.I_XGID + "',");
            builder.Append("'" + model.V_LXBM + "',");
            builder.Append("'" + model.SendUser + "',");
            builder.Append("'" + model.ReceiveUser + "',");
            builder.Append("'" + model.Message + "',");
            builder.Append("'" + model.SendTime + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static int SMSLogDelete(string XGID, string LXBM)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_SMSLog ");
            builder.Append(" where I_XGID='" + XGID + "' and V_LXBM = '" + LXBM + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        private static int SMSLogTodayAdd(SMSLog model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into PT_SMSLog_Today(");
            builder.Append("I_XGID,V_LXBM,SendUser,ReceiveUser,Message,SendTime");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.I_XGID + "',");
            builder.Append("'" + model.V_LXBM + "',");
            builder.Append("'" + model.SendUser + "',");
            builder.Append("'" + model.ReceiveUser + "',");
            builder.Append("'" + model.Message + "',");
            builder.Append("'" + model.SendTime + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        private static int SMSLogTodayAdd(SMSLog model, string ReceiverMobileNo)
        {
            string str = "系统消息:" + model.Message;
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into SMS.dbo.OutBox(");
            builder.Append("ReceiverMobileNo,Msg,SendTimeNormal");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + ReceiverMobileNo + "',");
            builder.Append("'" + str + "',");
            builder.Append("'" + model.SendTime + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static int SMSLogTodayDelete(string XGID, string LXBM)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete PT_SMSLog_Today ");
            builder.Append(" where I_XGID='" + XGID + "' and V_LXBM = '" + LXBM + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

