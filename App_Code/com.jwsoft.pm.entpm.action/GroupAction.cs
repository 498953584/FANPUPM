namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;

    public class GroupAction
    {
        public void Addtop(string I_BULLETINID, string usercode)
        {
            publicDbOpClass.ExecSqlString("INSERT INTO [PT_BULLETIN_Top]([I_BULLETINID],[V_YHDM]) VALUES( '" + I_BULLETINID + "' ,'" + usercode + "') ");
        }

        public DataSet GetAllGroupSet(string UserCode)
        {
            string str = " ";
            if (UserCode != "00000000")
            {
                str = " WHERE addByUser= '" + UserCode + "'  or addByUser='00000000' or Groupid=1 ";
            }
            else
            {
                str = " WHERE addByUser= '" + UserCode + "' or Groupid=1 ";
            }
            return publicDbOpClass.DataSetQuary("select * from PT_Group   " + str + " order by Groupid ");
        }

        public static string GetBulletinLook(string UserCode, string I_BULLETINID)
        {
            string str = "未阅";
            if (publicDbOpClass.DataTableQuary(" SELECT * FROM [PT_BULLETIN_MAIN_ReturnInfo] where [I_BULLETINID]='" + I_BULLETINID + "' and  [ReturnPersonCode]='" + UserCode + "' ").Rows.Count > 0)
            {
                str = "已阅";
            }
            return str;
        }

        public DataTable GetBulletinSend(string UserCode, string strwhere)
        {
            string str = " WHERE V_RELUSERCODE= '" + UserCode + "'   " + strwhere;
            return publicDbOpClass.DataTableQuary("select  *,ROW_NUMBER() OVER(ORDER BY DTM_RELEASETIME desc) as xh   FROM (" + this.GetStr_v() + ") a    " + str + "  order by xh ");
        }

        public DataTable GetBulletinTo(string UserCode, string strwhere)
        {
            string str = " WHERE  (charindex('" + UserCode + "',V_humCode)<>0 or charindex('" + UserCode + "',V_humCode1)<>0 )   " + strwhere;
            return publicDbOpClass.DataTableQuary("select  *,ROW_NUMBER() OVER(ORDER BY DTM_RELEASETIME desc) as xh   FROM (" + this.GetStr_v() + ") a    " + str + "  order by xh ");
        }

        public DataTable GetBulletinTo1(string UserCode, string strwhere)
        {
            string str = " WHERE datediff(dd,DTM_EXPRIESDATE,getdate())<= 0  and datediff(dd,DTM_RELEASETIME,getdate())= 0   and   (charindex('" + UserCode + "',V_humCode)<>0 or charindex('" + UserCode + "',V_humCode1)<>0 )   and [I_BULLETINID] not in (select [I_BULLETINID] from PT_BULLETIN_top where v_yhdm = '" + UserCode + "' )  and [I_BULLETINID] not in (select [I_BULLETINID] from PT_BULLETIN_MAIN_ReturnInfo where ReturnPersonCode = '" + UserCode + "' )";
            return publicDbOpClass.DataTableQuary("select top 1 *   FROM (" + this.GetStr_v() + ") a    " + str + "  order by DTM_RELEASETIME desc ");
        }

        public DataTable GetBulletinToBrowse(string UserCode, string strwhere)
        {
            string str = " WHERE datediff(dd,DTM_EXPRIESDATE,getdate())<= 0  and   (charindex('" + UserCode + "',V_humCode)<>0 or charindex('" + UserCode + "',V_humCode1)<>0 ) ";
            return publicDbOpClass.DataTableQuary("select top 5 *   FROM (" + this.GetStr_v() + ") a    " + str + "  order by DTM_RELEASETIME desc ");
        }

        public DataTable GetBulletinToNOLooklist(string UserCode)
        {
            string str = " WHERE datediff(dd,DTM_EXPRIESDATE,getdate())<= 0  and   (charindex('" + UserCode + "',V_humCode)>0 or charindex('" + UserCode + "',V_humCode1)>0 )   and [I_BULLETINID] not in (select [I_BULLETINID] from PT_BULLETIN_MAIN_ReturnInfo where ReturnPersonCode = '" + UserCode + "' )";
            return publicDbOpClass.DataTableQuary("select  *   FROM (" + this.GetStr_v() + ") a   " + str + "   order by DTM_RELEASETIME desc ");
        }

        public DataTable GetBulletinToNOLooklist(string UserCode, string strwhere)
        {
            string str = " WHERE datediff(dd,DTM_EXPRIESDATE,getdate())<= 0  and   (charindex('" + UserCode + "',V_humCode)>0 or charindex('" + UserCode + "',V_humCode1)>0 )   and [I_BULLETINID] not in (select [I_BULLETINID] from PT_BULLETIN_top where v_yhdm = '" + UserCode + "' )  and [I_BULLETINID] not in (select [I_BULLETINID] from PT_BULLETIN_MAIN_ReturnInfo where ReturnPersonCode = '" + UserCode + "' )";
            return publicDbOpClass.DataTableQuary("select  *   FROM (" + this.GetStr_v() + ") a    " + str + "  order by DTM_RELEASETIME desc ");
        }

        public string Getgroupadduser(string GroupID)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary(" SELECT [addByUser] FROM [PT_Group] where [GroupID]=" + GroupID + "  ");
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["addByUser"].ToString();
            }
            return str;
        }

        public DataTable GetgroupList(string UserCode, string strwhere)
        {
            string str = " ";
            if (UserCode != "00000000")
            {
                str = " WHERE addByUser= '" + UserCode + "'   " + strwhere;
            }
            else
            {
                str = " WHERE GroupID<>1";
            }
            return publicDbOpClass.DataTableQuary(" select  * , (SELECT count(*) FROM [PT_GroupHum] where [GroupID]=[PT_Group].GroupID) as usernum, (SELECT  [v_xm] FROM [PT_yhmc] where [v_yhdm]=addByUser ) as v_xm  FROM [PT_Group]    " + str + "  order by GroupID ");
        }

        public DataTable GetHumBYGroupID(string GroupID)
        {
            return publicDbOpClass.DataTableQuary("SELECT [v_yhdm],[v_xm],[i_bmdm] FROM [PT_yhmc] WHERE  v_yhdm IN  (select v_yhdm from PT_GroupHum  WHERE GroupID=" + GroupID + " ) order by v_xm ");
        }

        public DataTable GetHumListBYGroupID(string GroupID, string strwhere)
        {
            return publicDbOpClass.DataTableQuary("  SELECT     b.v_xm, b.V_BMMC, a.GroupID, a.v_yhdm  FROM v_UserInfoList AS b RIGHT OUTER JOIN   PT_GroupHum AS a ON b.v_yhdm = a.v_yhdm   WHERE  a.GroupID=" + GroupID + "  " + strwhere + "  order by b.i_bmdm,v_xm ");
        }

        public DataTable GetLookinTo(string strwhere, string I_BULLETINID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT [V_humCode],[V_humCode1] FROM  [PT_BULLETIN_MAIN] where [I_BULLETINID]=  '" + I_BULLETINID + "'  ");
            string str2 = "";
            string str3 = "";
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
                str3 = table.Rows[0][1].ToString();
            }
            string str5 = ("  select * from ( SELECT (select top 1 V_BMMC from pt_d_bm where i_bmdm =ReturnDept) as [ReturnDept],  (select top 1 v_xm from pt_yhmc where v_yhdm = ReturnPersonCode ) as [ReturnPersonCode], '' as tel, convert(varchar(10),[ReturnInfoTime],120) as ReturnInfo,0 as xh FROM [PT_BULLETIN_MAIN_ReturnInfo]  where I_BULLETINID = '" + I_BULLETINID + "'  ") + " union ";
            return publicDbOpClass.DataTableQuary(str5 + "SELECT [V_BMMC], [v_xm],MobilePhoneCode,'短信催看' as ReturnInfo,1 as xh   FROM [v_opm_UserInfoList] where [c_sfyx]='y' and  ( charindex([v_yhdm],'" + str2 + "')<>0 or charindex([v_yhdm],'" + str3 + "')<>0 ) and  [v_yhdm] not in (SELECT ReturnPersonCode FROM [PT_BULLETIN_MAIN_ReturnInfo]  where I_BULLETINID = '" + I_BULLETINID + "') ) a  " + strwhere + "  order by xh  ");
        }

        public string GetStr_v()
        {
            string str = "SELECT     a.I_BULLETINID, a.CorpCode, b.CorpName, a.V_RELUSERCODE, a.V_RELEASEUSER, a.V_TITLE, a.V_CONTENT, a.URL, a.DTM_RELEASETIME, ";
            return (str + " a.DTM_EXPRIESDATE, a.I_RELEASEBOUND, a.DeptRange, a.AuditState, CONVERT(varchar(10), a.DTM_RELEASETIME, 20) AS rq, a.V_humCode, " + "a.V_humCode1, a.V_FromBMMC FROM  dbo.PT_BULLETIN_MAIN AS a INNER JOIN  dbo.PT_d_CorpCode AS b ON a.CorpCode = b.CorpCode");
        }

        public string getStrSend(int i)
        {
            switch (i)
            {
                case 0:
                    return "发送成功!";

                case 1:
                    return "没有发送信息权限即用户名密码错误!";

                case 2:
                    return "服务器或系统错误!";

                case 3:
                    return "用户名为空!";

                case 4:
                    return "密码为空!";

                case 5:
                    return "信息内容为空!";

                case 6:
                    return "手机号码为空!";

                case 7:
                    return "手机号码格式错误!";

                case 8:
                    return "手机号码数量大于100个，请重新分批发送!";
            }
            return "";
        }

        public int GroupAdd(string GroupName, string UserCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO [PT_Group]([GroupName],[addByUser])");
            builder.Append(" values (");
            builder.Append("'" + GroupName + "','" + UserCode + "'");
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != null)
            {
                return Convert.ToInt32(obj2);
            }
            return 0;
        }

        public int GroupDel(string GroupID)
        {
            return publicDbOpClass.ExecuteSQL(" begin  DELETE FROM [PT_Group] WHERE  GroupID= " + GroupID + " DELETE FROM [PT_Grouphum] WHERE  GroupID= " + GroupID + " end ");
        }

        public int GrouphumDel(string GroupID, string v_yhdm)
        {
            return publicDbOpClass.ExecuteSQL(" begin  DELETE FROM [PT_Grouphum] WHERE  GroupID= " + GroupID + "  and v_yhdm ='" + v_yhdm + "'   end ");
        }

        public int GroupItemAdd(string GroupID, string splitCode)
        {
            return publicDbOpClass.ExecuteSQL(" INSERT INTO [PT_GroupHum]([v_yhdm],[GroupID]) select v_yhdm, " + GroupID + " from pt_yhmc where c_sfyx='y' and  charindex(v_yhdm,'" + splitCode + "')<>0 ");
        }

        public int GroupItemAdd1(string GroupID, string splitCode)
        {
            return publicDbOpClass.ExecuteSQL(" INSERT INTO [PT_GroupHum]([v_yhdm],[GroupID]) select v_yhdm, " + GroupID + " from pt_yhmc where c_sfyx='y' and  charindex(v_yhdm,'" + splitCode + "')<>0 and  v_yhdm not in (select v_yhdm from  PT_GroupHum where GroupID=" + GroupID + ") ");
        }

        public int GroupUpdate(string GroupID, string splitCode)
        {
            return publicDbOpClass.ExecuteSQL(" begin  DELETE FROM [PT_GroupHum] WHERE  GroupID= " + GroupID + " INSERT INTO [PT_GroupHum]([v_yhdm],[GroupID]) select v_yhdm, " + GroupID + " from pt_yhmc where c_sfyx='y' and  charindex(v_yhdm,'" + splitCode + "')<>0  end ");
        }

        public string SetGroupSend(string splitCode, string sendtxt, string StrFrom)
        {
            string str = "";
            DataTable table = new DataTable();
            table = publicDbOpClass.DataTableQuary("SELECT [V_BMMC], [v_xm],tel, FROM [v_UserInfoList] where [c_sfyx]='y' and  ( charindex([v_yhdm],'" + splitCode + "')<>0 )");
            if (table.Rows.Count > 0)
            {
                string str3 = "";
                foreach (DataRow row in table.Rows)
                {
                    string t = row["tel"].ToString();
                    if (t == "")
                    {
                        str = str + row["v_xm"].ToString() + "手机号为空,";
                    }
                    else if ((t != "") && !this.telverify(t))
                    {
                        str = str + row["v_xm"].ToString() + "手机号未通过验证,";
                    }
                    else
                    {
                        str3 = str3 + row["tel"].ToString() + ",";
                    }
                }
                if (str3.Length > 10)
                {
                    int i = this.SetSendTo(str3.TrimEnd(new char[] { ',' }), sendtxt, StrFrom);
                    if (i != 0)
                    {
                        str = str + this.getStrSend(i);
                    }
                }
            }
            return str;
        }

        public int SetGroupSendTo(string splitCode, string sendtxt, string StrFrom)
        {
            DataTable table = new DataTable();
            table = publicDbOpClass.DataTableQuary("SELECT [V_BMMC], [v_xm],MobilePhoneCode FROM [v_opm_UserInfoList] where [c_sfyx]='y' and  ( charindex([v_yhdm],'" + splitCode + "')<>0 )");
            if (table.Rows.Count > 0)
            {
                string str2 = "";
                foreach (DataRow row in table.Rows)
                {
                    string t = row["MobilePhoneCode"].ToString();
                    if ((t != "") && ((t == "") || this.telverify(t)))
                    {
                        str2 = str2 + row["MobilePhoneCode"].ToString() + ",";
                    }
                }
                if (str2.Length > 10)
                {
                    str2 = str2.TrimEnd(new char[] { ',' });
                    string[] strArray = str2.Split(new char[] { ',' });
                    if (strArray.Length <= 100)
                    {
                        this.SetSendTo(str2.TrimEnd(new char[] { ',' }), sendtxt, StrFrom);
                    }
                    else
                    {
                        string str4 = "";
                        string str5 = "";
                        string str6 = "";
                        for (int i = 0; i <= (strArray.Length - 1); i++)
                        {
                            if (i <= 0x63)
                            {
                                str4 = str4 + strArray[i].ToString() + ",";
                            }
                            else if ((i > 0x63) && (i <= 0xc6))
                            {
                                str5 = str5 + strArray[i].ToString() + ",";
                            }
                            else if (i > 0xc6)
                            {
                                str6 = str6 + strArray[i].ToString() + ",";
                            }
                        }
                        this.SetSendTo(str4.TrimEnd(new char[] { ',' }), sendtxt, StrFrom);
                        if (str5.Length > 10)
                        {
                            Thread.Sleep(0x3e8);
                            this.SetSendTo(str5.TrimEnd(new char[] { ',' }), sendtxt, StrFrom);
                        }
                        if (str6.Length > 10)
                        {
                            Thread.Sleep(0x3e8);
                            this.SetSendTo(str6.TrimEnd(new char[] { ',' }), sendtxt, StrFrom);
                        }
                    }
                }
            }
            return 0;
        }

        public int SetSendTo(string tel, string sendtxt, string StrFrom)
        {
            return 5;
        }

        public string SetSendtxt(string I_BULLETINID)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("SELECT top 1 [V_TITLE],[V_CONTENT]  FROM  [PT_BULLETIN_MAIN] where [I_BULLETINID]=  '" + I_BULLETINID + "'");
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["V_TITLE"].ToString();
            }
            return str;
        }

        public bool telverify(string t)
        {
            bool flag = false;
            string pattern = @"(^1[3-8]\d{9}$)";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(t))
            {
                flag = true;
            }
            return flag;
        }
    }
}

