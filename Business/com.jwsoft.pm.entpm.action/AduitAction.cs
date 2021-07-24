namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class AduitAction
    {
        public static bool AddToLog(string v_yhdm, string c_mkdm, string V_USERIP, string V_OPERATEINFO)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pt_Log(v_yhdm,c_mkdm,V_USERIP,V_OPERATEINFO) values('" + v_yhdm + "','" + c_mkdm + "','" + V_USERIP + "','" + V_OPERATEINFO + "')");
        }

        public static string getAduitMsg(string mid, string fid)
        {
            string sqlString = "select * from PT_AduitMsg where Fid='" + fid + "' and Mid='" + mid + "'";
            publicDbOpClass.DataTableQuary(sqlString);
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if (table.Rows.Count != 0)
            {
                return (table.Rows[0]["person"].ToString() + "----" + table.Rows[0]["Message"].ToString() + "----" + table.Rows[0]["AddTime"].ToString());
            }
            return "暂无审核信息";
        }

        public static string GetYhdmbyLog(string V_USERIP)
        {
            string str2 = "";
            DataTable table = publicDbOpClass.DataTableQuary("select V_OPERATEINFO from  pt_Log where c_mkdm='登录系统' and v_UserIP='" + V_USERIP + "' order by DT_OPERATEDATE desc ");
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            return str2;
        }

        public static string OkMessage()
        {
            return "审核过程不可逆，你确定要审核通过吗";
        }

        public static void setAduitMsg(string mid, string fid, string msg, string Opp, string isgree)
        {
            DateTime now = DateTime.Now;
            string str2 = "begin";
            object obj2 = str2 + " if exists(select 1 from PT_AduitMsg WHERE Fid='" + fid + "' AND Mid='" + mid + "' )";
            string str3 = string.Concat(new object[] { obj2, " UPDATE  PT_AduitMsg SET IsAgree='", isgree, "',person='", Opp, "',Message='", msg, "',AddTime='", now, "'  WHERE Fid='", fid, "' AND Mid='", mid, "'" }) + " else";
            publicDbOpClass.ExecuteSQL((str3 + " INSERT INTO PT_AduitMsg(Fid,Mid,IsAgree,person,Message) VALUES ('" + fid + "','" + mid + "' ,'" + isgree + "' ,'" + Opp + "','" + msg + "')") + " end");
        }

        public static string SetOK(string sqlwhere)
        {
            string str = "操作失败！";
            if (publicDbOpClass.ExecuteSQL(sqlwhere) >= 1)
            {
                str = "操作成功！";
            }
            return str;
        }

        public static string SetOkState(string AuditState)
        {
            switch (AuditState)
            {
                case "-1":
                    return "未提交#050505";

                case "0":
                    return "审核中#030310";

                case "1":
                    return "已审核#008B45";

                case "-2":
                    return "驳回#ff0000";

                case "-3":
                    return "重报#914229";
            }
            return "未提交#050505";
        }
    }
}

