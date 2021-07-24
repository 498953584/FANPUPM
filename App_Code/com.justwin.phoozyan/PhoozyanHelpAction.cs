namespace com.justwin.phoozyan
{
    using cn.justwin.SMS;
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.action;
    using com.jwsoft.pm.entpm.model;
    using System;

    public class PhoozyanHelpAction
    {
        private void getPTDBSJ(string xgid, string Mes, string jsyhdm)
        {
            PTDBSJ model = new PTDBSJ {
                V_LXBM = "015",
                I_XGID = xgid,
                DTM_DBSJ = DateTime.Now,
                V_Content = Mes,
                V_DBLJ = "",
                V_YHDM = jsyhdm
            };
            PublicInterface.SendSysMsg(model);
        }

        public string RePhoneCode(string userCode)
        {
            return publicDbOpClass.ExecuteScalar("select mobilephonecode from pt_yhmc where V_yhdm='" + userCode + "'").ToString();
        }

        public string SendMail(int iSysID, string strSenderCode, string strSenderName, DateTime dtSend, string strTitle, string strContent, int iSms, string[] aryConsigneeCode, string[] aryConsigneeName, int iMailType, string SJR, string FJR, int Rtx)
        {
            string str = "";
            string sqlString = "";
            int num = 0;
            num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            num3 = Convert.ToInt32(publicDbOpClass.ExecuteScalar(" select Count(1) from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0)"));
            if (num3 > 0)
            {
                num4 = Convert.ToInt32(publicDbOpClass.ExecuteScalar("select sum(i_AnnexSize) from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "') and (i_IsDel=0)"));
            }
            num2 = aryConsigneeCode.Length - 1;
            string str9 = string.Concat(new object[] { 
                " insert into pt_dzyj_sj values(", num.ToString(), ",", iSysID.ToString(), ",'", strSenderCode, "','", strSenderName, "','", dtSend.ToString(), "','", strTitle, "','", strContent, "',0,0,0,", iMailType, 
                ",'", SJR, "','", FJR, "');"
             });
            sqlString = str9 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_AnnexPath,v_AnnexName,i_AnnexSize,'' from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0) and (I_ISSECOND=0);";
            for (int i = 0; i < (aryConsigneeCode.Length - 1); i++)
            {
                string[] strArray = aryConsigneeCode[i].ToString().Split(new char[] { ':' });
                if (iSysID == int.Parse(strArray[0].ToString()))
                {
                    string str10 = sqlString;
                    sqlString = str10 + " insert into pt_dzyj_yj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strArray[1].ToString() + "','" + aryConsigneeName[i].ToString() + "','" + dtSend.ToString() + "','y','s');";
                }
                if (iSms == 1)
                {
                    string userName = userManageDb.GetCurrentUserInfo().UserName;
                    string mbNo = "";
                    string msg = "";
                    string str8 = publicDbOpClass.DataTableQuary("select v_xm from pt_yhmc where v_yhdm='" + strArray[1] + "'").Rows[0]["V_xm"].ToString();
                    msg = "奇云短信:" + str8 + "您好！" + userName + "给您发送了一封邮件,请尽快查看!";
                    mbNo = publicDbOpClass.ExecuteScalar("select mobilephonecode from pt_yhmc where v_yhdm='" + strArray[1] + "'").ToString();
                    if (mbNo != "")
                    {
                        new cn.justwin.SMS.SMS().Send("", mbNo, msg, "", "", "");
                    }
                    else
                    {
                        str = str + str8 + " ";
                    }
                }
            }
            string str11 = sqlString;
            sqlString = (str11 + "update pt_dzyj_sj set i_fjsl=" + num3.ToString() + ",i_fsrs=" + num2.ToString() + ",i_fjdx=" + num4.ToString() + " where i_sjid=" + num.ToString() + ";") + " update pt_dzyj_Tempfj set i_IsSecond=1 where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);";
            if (Rtx == 1)
            {
                for (int j = 0; j < (aryConsigneeCode.Length - 1); j++)
                {
                    this.getPTDBSJ(num.ToString(), strTitle + "-" + userManageDb.GetCurrentUserInfo().UserName, aryConsigneeCode[j].ToString());
                }
            }
            if (!publicDbOpClass.NonQuerySqlString(sqlString))
            {
                return "2";
            }
            if (str == "")
            {
                return "1";
            }
            return str;
        }
    }
}

