namespace com.jwsoft.pm.entpm.action
{
    using cn.justwin.DAL;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BulletionAction
    {
        public static void AddBulletionLog(string bulletionId, string usercode)
        {
            publicDbOpClass.ExecuteSQL(string.Concat(new object[] { "INSERT INTO PT_BULLETIN_LOG(BulletinId,UserCode,InputDate) VALUES ('", bulletionId, "','", usercode, "','", DateTime.Now, "')" }));
        }

        public static DataTable GetBulletionList(string title, string startDate, string endDate, string range, string person, string flowstate, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT * FROM (\r\n                 SELECT ROW_NUMBER () OVER(ORDER BY DTM_RELEASETIME DESC) AS NUM,  I_BULLETINID AS ID,CorpCode,V_RELEASEUSER AS UserName,V_TITLE,DTM_RELEASETIME AS Date,\r\nI_RELEASEBOUND AS Rang,AuditState,(case when I_RELEASEBOUND = 1 then '集团内部' when I_RELEASEBOUND = 2 then '外部' end) as RangName FROM PT_BULLETIN_MAIN WHERE 1=1 ");
            if (!string.IsNullOrEmpty(title))
            {
                builder.AppendFormat(" AND V_TITLE LIKE '%{0}%' ", title);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND DTM_RELEASETIME >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND DTM_RELEASETIME <'{0}' ", Convert.ToDateTime(endDate).AddDays(1.0));
            }
            if (!string.IsNullOrEmpty(range))
            {
                builder.AppendFormat(" AND I_RELEASEBOUND ='{0}' ", range);
            }
            if (!string.IsNullOrEmpty(person))
            {
                builder.AppendFormat(" AND V_RELEASEUSER LIKE '%{0}%' ", person);
            }
            if (!string.IsNullOrEmpty(flowstate))
            {
                builder.AppendFormat(" AND AuditState ='{0}' ", flowstate);
            }
            builder.AppendFormat(") AS A  WHERE A.NUM BETWEEN ({0}-1)*{1}+1 AND {2}*{3} ", new object[] { pageNo, pageSize, pageNo, pageSize });
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static DataTable GetBulletionUserList(string bulletinId, string userName, string startDate, string endDate, int pageNo, int pageSize)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM (\r\nSELECT ROW_NUMBER() OVER (ORDER BY MinDate DESC) AS NUM, * FROM (\r\nSELECT  DISTINCT  A.UserCode, P.v_xm AS UserName,(SELECT TOP 1 InputDate FROM PT_BULLETIN_LOG B WHERE B.UserCode=A.UserCode \r\nAND B.BulletinId='{0}' ORDER BY InputDate ASC  ) AS MinDate\r\nFROM PT_BULLETIN_LOG A \r\nLEFT JOIN PT_yhmc P ON A.UserCode=P.v_yhdm\r\nWHERE A.BulletinId='{1}' ) AS Total WHERE 1=1 ", bulletinId, bulletinId);
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat(" AND  UserName LIKE '%{0}%' ", userName);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND  MinDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND MinDate <'{0}' ", Convert.ToDateTime(endDate).AddDays(1.0));
            }
            builder.AppendFormat(") AS T WHERE T.NUM BETWEEN ({0}-1)*{1}+1 AND {2}*{3} ", new object[] { pageNo, pageSize, pageNo, pageSize });
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static int GetCountBulletion(string title, string startDate, string endDate, string range, string person, string flowstate)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT COUNT(*) FROM (\r\n                 SELECT ROW_NUMBER () OVER(ORDER BY DTM_RELEASETIME DESC) AS NUM,  I_BULLETINID AS ID,CorpCode,V_RELEASEUSER AS UserName,V_TITLE,DTM_RELEASETIME AS Date,\r\nI_RELEASEBOUND AS Rang,AuditState,(case when I_RELEASEBOUND = 1 then '集团内部' when I_RELEASEBOUND = 2 then '外部' end) as RangName FROM PT_BULLETIN_MAIN WHERE 1=1 ");
            if (!string.IsNullOrEmpty(title))
            {
                builder.AppendFormat(" AND V_TITLE LIKE '%{0}%' ", title);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND DTM_RELEASETIME >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND DTM_RELEASETIME <'{0}' ", Convert.ToDateTime(endDate).AddDays(1.0));
            }
            if (!string.IsNullOrEmpty(range))
            {
                builder.AppendFormat(" AND I_RELEASEBOUND ='{0}' ", range);
            }
            if (!string.IsNullOrEmpty(person))
            {
                builder.AppendFormat(" AND V_RELEASEUSER LIKE '%{0}%' ", person);
            }
            if (!string.IsNullOrEmpty(flowstate))
            {
                builder.AppendFormat(" AND AuditState ='{0}' ", flowstate);
            }
            builder.Append(") AS A ");
            return int.Parse(publicDbOpClass.DataTableQuary(builder.ToString()).Rows[0][0].ToString());
        }

        public static int GetCountUserList(string bulletinId, string userName, string startDate, string endDate)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT COUNT(*) FROM (\r\nSELECT  DISTINCT  A.UserCode, P.v_xm AS UserName,(SELECT TOP 1 InputDate FROM PT_BULLETIN_LOG B WHERE B.UserCode=A.UserCode ORDER BY InputDate ASC  ) AS MinDate\r\nFROM PT_BULLETIN_LOG A \r\nLEFT JOIN PT_yhmc P ON A.UserCode=P.v_yhdm\r\nWHERE A.BulletinId='{0}' \r\n) AS Total WHERE 1=1 ", bulletinId);
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat(" AND  UserName LIKE '%{0}%' ", userName);
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                builder.AppendFormat(" AND  MinDate >= '{0}' ", startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                builder.AppendFormat(" AND MinDate <'{0}' ", Convert.ToDateTime(endDate).AddDays(1.0));
            }
            return int.Parse(publicDbOpClass.DataTableQuary(builder.ToString()).Rows[0][0].ToString());
        }

        public static string getv_bmbm(string usercode)
        {
            string str = "";
            DataSet set = publicDbOpClass.DataSetQuary("select a.v_bmbm from pt_d_bm as a inner join PT_yhmc as b on a.i_bmdm=b.i_bmdm where b.v_yhdm='" + usercode + "'");
            if (set.Tables[0].Rows.Count > 0)
            {
                str = set.Tables[0].Rows[0][0].ToString();
            }
            return str;
        }

        public static string ReturnCropName(string CropCode)
        {
            string str = "";
            string[] strArray = CropCode.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                DataTable table = publicDbOpClass.DataTableQuary("select V_BMMC from pt_d_bm where V_bmbm='" + strArray[i].ToString() + "' and C_sfyx='y'");
                if (table.Rows.Count > 0)
                {
                    if ((i == (strArray.Length - 1)) || (strArray.Length == 1))
                    {
                        str = str + table.Rows[0][0].ToString();
                    }
                    else
                    {
                        str = str + table.Rows[0][0].ToString() + ",";
                    }
                }
            }
            return str;
        }

        public static DataTable ReturnTable(string userCode, string condition)
        {
            DataTable table = new DataTable();
            if (userCode == "00000000")
            {
                string str = "\r\n\t\t\t\t\tSELECT [I_BULLETINID], [CorpCode], [V_RELUSERCODE], [V_RELEASEUSER], [V_TITLE],\r\n\t\t\t\t\t\t[V_CONTENT], [URL], [DTM_RELEASETIME], [DTM_EXPRIESDATE], [I_RELEASEBOUND], \r\n\t\t\t\t\t\t(case when I_RELEASEBOUND = 1 then '集团内部' when I_RELEASEBOUND = 2 then '外部' end) as RELEASEBOUND, \r\n\t\t\t\t\t\t[DeptRange], [AuditState], [rq] \r\n\t\t\t\t\tFROM [v_bulletin_list] \r\n\t\t\t\t\tWHERE 1 = 1\r\n\t\t\t\t";
                return publicDbOpClass.DataTableQuary(str + condition + " ORDER BY DTM_RELEASETIME desc ");
            }
            string cmdText = "\r\n\t\t\t\t\tSELECT PT_d_bm.v_bmbm,\r\n\t\t\t\t\t\tv.[I_BULLETINID], v.[CorpCode], v.[V_RELUSERCODE], v.[V_RELEASEUSER], v.[V_TITLE],\r\n\t\t\t\t\t\tv.[V_CONTENT], v.[URL], v.[DTM_RELEASETIME], v.[DTM_EXPRIESDATE], v.[I_RELEASEBOUND], \r\n\t\t\t\t\t\t(case when v.I_RELEASEBOUND = 1 then '集团内部' when v.I_RELEASEBOUND = 2 then '外部' end) as RELEASEBOUND, \r\n\t\t\t\t\t\tv.[DeptRange], v.[AuditState], v.[rq] \r\n\t\t\t\t\tFROM [v_bulletin_list] v\r\n\t\t\t\t\tLEFT JOIN PT_Yhmc ON v_yhdm = @userCode\r\n\t\t\t\t\tLEFT JOIN PT_d_bm ON PT_Yhmc.i_bmdm = PT_d_bm.i_bmdm\r\n\t\t\t\t\tWHERE ((',' + v.[CorpCode] + ',') LIKE ('%,' + PT_d_bm.v_bmbm + ',%')\r\n\t\t\t\t\t\t\tOR v.V_RELUSERCODE = @userCode)\r\n\t\t\t\t";
            cmdText = cmdText + condition + " ORDER BY DTM_RELEASETIME desc ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@userCode", userCode) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public static DataTable ReturnTable(string userCode, string condition, int count)
        {
            DataTable table = new DataTable();
            if (userCode == "00000000")
            {
                return publicDbOpClass.DataTableQuary(string.Format("\r\n\t\t\t\t\tSELECT TOP({0}) [I_BULLETINID], [CorpCode], [V_RELUSERCODE], [V_RELEASEUSER], [V_TITLE],\r\n\t\t\t\t\t\t[V_CONTENT], [URL], [DTM_RELEASETIME], [DTM_EXPRIESDATE], [I_RELEASEBOUND], \r\n\t\t\t\t\t\t(case when I_RELEASEBOUND = 1 then '集团内部' when I_RELEASEBOUND = 2 then '外部' end) as RELEASEBOUND, \r\n\t\t\t\t\t\t[DeptRange], [AuditState], [rq] \r\n\t\t\t\t\tFROM [v_bulletin_list] \r\n\t\t\t\t\tWHERE 1 = 1\r\n\t\t\t\t", count) + condition + " ORDER BY DTM_RELEASETIME desc ");
            }
            string cmdText = "\r\n\t\t\t\t\tSELECT TOP(@count) PT_d_bm.v_bmbm,\r\n\t\t\t\t\t\tv.[I_BULLETINID], v.[CorpCode], v.[V_RELUSERCODE], v.[V_RELEASEUSER], v.[V_TITLE],\r\n\t\t\t\t\t\tv.[V_CONTENT], v.[URL], v.[DTM_RELEASETIME], v.[DTM_EXPRIESDATE], v.[I_RELEASEBOUND], \r\n\t\t\t\t\t\t(case when v.I_RELEASEBOUND = 1 then '集团内部' when v.I_RELEASEBOUND = 2 then '外部' end) as RELEASEBOUND, \r\n\t\t\t\t\t\tv.[DeptRange], v.[AuditState], v.[rq] \r\n\t\t\t\t\tFROM [v_bulletin_list] v\r\n\t\t\t\t\tLEFT JOIN PT_Yhmc ON v_yhdm = @userCode\r\n\t\t\t\t\tLEFT JOIN PT_d_bm ON PT_Yhmc.i_bmdm = PT_d_bm.i_bmdm\r\n\t\t\t\t\tWHERE ((',' + v.[CorpCode] + ',') LIKE ('%,' + PT_d_bm.v_bmbm + ',%')\r\n\t\t\t\t\t\t\tOR v.V_RELUSERCODE = @userCode)\r\n\t\t\t\t";
            cmdText = cmdText + condition + " ORDER BY DTM_RELEASETIME desc ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@count", count), new SqlParameter("@userCode", userCode) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }
    }
}

