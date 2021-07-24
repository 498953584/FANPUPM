namespace com.jwsoft.pm.entpm.action
{
    using ChineseToSpell;
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.sysManage.publicStringOperation;
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;

    public class userManageDb
    {
        private string _messageString = "";
        private Page _objPage;

        public int chkDlid(string dlid, string yhdm)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_login where v_dlid = '" + dlid + "' and v_yhdm <> '" + yhdm + "'").Rows.Count;
        }

        public bool comparePwd(string yhdm, string inpPwd)
        {
            string str = this.getUserPwd(yhdm);
            inpPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(inpPwd, "md5");
            return (str == inpPwd);
        }

        private static UserInfo ConvertUserDataTableToUserInfo(DataRow dr)
        {
            return new UserInfo { UserCode = dr["v_yhdm"].ToString(), UserDepartCode = dr["i_bmdm"].ToString(), UserDepartName = dr["v_bmmc"].ToString(), UserLoginID = dr["v_dlid"].ToString(), UserLoginPwd = dr["v_dlmm"].ToString(), UserName = dr["v_xm"].ToString(), UserWorkArea = (dr["v_bgfw"] == DBNull.Value) ? "" : dr["v_bgfw"].ToString() };
        }

        public DataTable depManagerList(string bmdm, string sfyx)
        {
            string str2 = "SELECT b.V_YHDM, a.v_xm FROM dbo.PT_yhmc a INNER JOIN dbo.PT_LOGIN b ON a.v_yhdm = b.V_YHDM ";
            return publicDbOpClass.DataTableQuary(str2 + " WHERE (a.i_bmdm = " + bmdm + ") AND (b.C_SFYX = '" + sfyx + "') AND (b.IsManager = '1') ORDER BY a.i_xh");
        }

        public string depName(string bmdm)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_d_bm where i_bmdm = " + bmdm);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["v_bmmc"].ToString();
            }
            return str;
        }

        public DataTable depUserList(string bmdm, string sfyx)
        {
            string str2 = "SELECT b.v_xm, b.v_yhdm,b.i_xh FROM dbo.PT_LOGIN a INNER JOIN dbo.PT_yhmc b ON a.V_YHDM = b.v_yhdm ";
            return publicDbOpClass.DataTableQuary(str2 + "WHERE (b.I_BMDM = " + bmdm + ") AND (a.C_SFYX = '" + sfyx + "') AND b.STATE !='2' ORDER BY b.IsChargeMan desc,b.I_XH");
        }

        public DataSet DepUserQuaryDs(string bmdm, string sfyx)
        {
            string sqlString = "";
            if (sfyx == "y")
            {
                sqlString = "select v_yhdm,i_bmdm,v_xm,case when c_sfyx='y' then '有效' when c_sfyx='n' then '无效' end as c_sfyx from pt_yhmc where (i_bmdm=" + bmdm + ")and(c_sfyx='" + sfyx + "') order by c_sfyx desc,v_xm asc";
            }
            else if (sfyx == "n")
            {
                sqlString = "select v_yhdm,i_bmdm,v_xm,case when c_sfyx='y' then '有效' when c_sfyx='n' then '无效' end as c_sfyx from pt_yhmc where (i_bmdm=" + bmdm + ")and(c_sfyx='" + sfyx + "') order by c_sfyx desc,v_xm asc";
            }
            else
            {
                sqlString = "select v_yhdm,i_bmdm,v_xm,case when c_sfyx='y' then '有效' when c_sfyx='n' then '无效' end as c_sfyx from pt_yhmc where i_bmdm=" + bmdm + " order by c_sfyx desc,v_xm asc";
            }
            return publicDbOpClass.DataSetQuary(sqlString);
        }

        public DataTable DepUserQuaryDt(string bmdm, string sfyx)
        {
            string sqlString = "";
            if (sfyx == "y")
            {
                sqlString = "select v_yhdm,i_bmdm,v_xm,case when c_sfyx='y' then '有效' when c_sfyx='n' then '无效' end as c_sfyx from pt_yhmc where (i_bmdm=" + bmdm + ")and(c_sfyx='" + sfyx + "') order by c_sfyx desc";
            }
            else if (sfyx == "n")
            {
                sqlString = "select v_yhdm,i_bmdm,v_xm,case when c_sfyx='y' then '有效' when c_sfyx='n' then '无效' end as c_sfyx from pt_yhmc where (i_bmdm=" + bmdm + ")and(c_sfyx='" + sfyx + "') order by c_sfyx desc";
            }
            else
            {
                sqlString = "select v_yhdm,i_bmdm,v_xm,case when c_sfyx='y' then '有效' when c_sfyx='n' then '无效' end as c_sfyx from pt_yhmc where i_bmdm=" + bmdm + " order by c_sfyx desc";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataSet DepUserQuaryForMail(string bmdm)
        {
            if (bmdm == "0")
            {
                bmdm = "1";
            }
            string str = "select pt_yhmc.v_xm,pt_yhmc.v_yhdm,pt_d_bm.v_bmbm from pt_yhmc,pt_d_bm where pt_yhmc.i_bmdm=pt_d_bm.i_bmdm ";
            return publicDbOpClass.DataSetQuary(str + " and pt_yhmc.i_bmdm=" + bmdm + " and pt_yhmc.c_sfyx='y' and state!=2 order by v_xm");
        }

        public static UserCollection GetAllUserLists()
        {
            StringBuilder builder = new StringBuilder();
            UserCollection users = new UserCollection();
            builder.Append(" SELECT b.v_yhdm, b.v_xm, c.V_DLID, c.V_DLMM, a.i_bmdm, a.V_BMMC,c.V_bgfw ");
            builder.Append(" FROM dbo.PT_d_bm a INNER JOIN ");
            builder.Append(" dbo.PT_yhmc b ON a.i_bmdm = b.i_bmdm INNER JOIN ");
            builder.Append("  dbo.PT_LOGIN c ON b.v_yhdm = c.V_YHDM ");
            using (DataTable table = publicDbOpClass.DataTableQuary(builder.ToString()))
            {
                foreach (DataRow row in table.Rows)
                {
                    users.Add(ConvertUserDataTableToUserInfo(row));
                }
                return users;
            }
        }

        public DataTable GetAuditPwd(string usercode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_login where v_yhdm= '" + usercode + "'");
        }

        public string GetAutographImage(string userCode)
        {
            string str = null;
            object obj2 = publicDbOpClass.ExecuteScalar("select AuditNameImagePath from pt_login where v_yhdm = '" + userCode + "'");
            if (obj2 != null)
            {
                str = obj2.ToString();
            }
            return str;
        }

        public static string GetCorpCode(string yhdm)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select CorpCode from PT_d_bm where i_bmdm =(select top 1 i_bmdm from pt_yhmc where v_yhdm = '" + yhdm + "')");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "";
        }

        public static UserInfo GetCurrentUserInfo()
        {
            string str = "";
            if (HttpContext.Current.Session["yhdm"] != null)
            {
                str = HttpContext.Current.Session["yhdm"].ToString();
                using (DataTable table = publicDbOpClass.DataTableQuary("select * from v_UserInfoList where v_yhdm = '" + str + "'"))
                {
                    if (table.Rows.Count != 0)
                    {
                        return ConvertUserDataTableToUserInfo(table.Rows[0]);
                    }
                    return null;
                }
            }
            return null;
        }

        public static UserInfo GetCurrentUserInfo(string strUserCode)
        {
            using (DataTable table = publicDbOpClass.DataTableQuary("select * from v_UserInfoList where v_yhdm = '" + strUserCode + "'"))
            {
                if (table.Rows.Count != 0)
                {
                    return ConvertUserDataTableToUserInfo(table.Rows[0]);
                }
                return null;
            }
        }

        public string GetDeptAllName(string strUserCode)
        {
            return Convert.ToString(publicDbOpClass.ExecuteScalar("select v_bmqc from pt_d_bm where i_bmdm = (select i_bmdm from pt_yhmc where v_yhdm='" + strUserCode + "')"));
        }

        public static string GetModuleName(string code)
        {
            string str = "";
            str = "select c_mkdm,v_mkmc from pt_mk where c_mkdm = '" + code + "' ";
            while (code.Length > 2)
            {
                code = code.Substring(0, code.Length - 2);
                str = str + " or c_mkdm = '" + code + "'";
            }
            DataTable table = publicDbOpClass.DataTableQuary(str + " order by c_mkdm asc ");
            string str2 = "";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (i != 0)
                {
                    str2 = str2 + "->" + table.Rows[i]["v_mkmc"].ToString();
                }
                else
                {
                    str2 = str2 + table.Rows[i]["v_mkmc"].ToString();
                }
            }
            return str2;
        }

        public DataTable getSearchResult(string strKey)
        {
            return publicDbOpClass.DataTableQuary("select * from v_UserInfoList where v_xm like '%" + strKey + "%' order by v_xm");
        }

        public DataSet GetUserByLetterDs(string strLetter)
        {
            return publicDbOpClass.DataSetQuary("select a.v_yhdm,a.v_xm from pt_txl_nbtxl a, pt_yhmc b where (a.v_yhdm = b.v_yhdm)and(upper(substring(a.v_hypy,1,1))='" + strLetter + "')and(a.c_bs='y')and(b.c_sfyx='y') order by a.v_xm");
        }

        public DataTable GetUserByLetterDt(string strLetter)
        {
            return publicDbOpClass.DataTableQuary("select a.v_yhdm,a.v_xm from pt_txl_nbtxl a, pt_yhmc b where (a.v_yhdm = b.v_yhdm)and(upper(substring(a.v_hypy,1,1))='" + strLetter + "')and(b.c_sfyx='y') order by a.v_xm");
        }

        public int GetUserDept(string strUserCode)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select i_bmdm from pt_yhmc where v_yhdm='" + strUserCode + "'"));
        }

        public DataTable GetUserDisplayMenu(string strUserCode)
        {
            string str = "select a.*,isnull(b.c_mkdm,0) as IsHave from pt_mk a inner join ";
            return publicDbOpClass.DataTableQuary((str + "(select * from pt_mk where charindex( ','+c_mkdm+',',(select ','+cast(v_cycd as varchar(4000))+',' from pt_login where v_yhdm = '" + strUserCode + "'),1) != 0  ) b ") + " on a.c_mkdm  = b.c_mkdm where a.i_ChildNum = 0");
        }

        public string getUserDlid(string yhdm)
        {
            return publicDbOpClass.DataTableQuary("select v_dlid from pt_login where v_yhdm = '" + yhdm + "'").Rows[0]["v_dlid"].ToString();
        }

        public DataTable GetUserImageList(string yhdm)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_login where v_yhdm = '" + yhdm + "'");
        }

        public DataTable GetUserInfo(string yhdm)
        {
            return publicDbOpClass.DataTableQuary("select  V_XM, V_DLID, I_BMDM,V_BGFW,V_XTQX,V_CYCD from pt_login,pt_yhmc where pt_login.v_yhdm = pt_yhmc.v_yhdm and pt_yhmc.v_yhdm = '" + yhdm + "'");
        }

        public static DataTable GetUserInfoByYhdm(string YHDMS)
        {
            if (YHDMS == "")
            {
                YHDMS = "''";
            }
            return publicDbOpClass.DataTableQuary(" select * from V_UserInfoList where v_yhdm in (" + YHDMS + ")");
        }

        public DataTable GetUserMenu(string strUserCode)
        {
            return publicDbOpClass.DataTableQuary(("select a.*,isnull(b.c_mkdm,0) as IsHave from (select pt_mk.* from pt_yhmc_privilege,pt_mk where pt_yhmc_privilege.c_mkdm = pt_mk.c_mkdm and v_yhdm = '" + strUserCode + "')a ") + " left join (select * from pt_mk where charindex( ','+c_mkdm+',',(select ','+cast(v_cycd as varchar(4000))+',' from pt_login where v_yhdm = '" + strUserCode + "'),1) != 0  ) b  on a.c_mkdm  = b.c_mkdm");
        }

        public string GetUserName(string strSenderCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select v_xm from pt_yhmc where v_yhdm = '" + strSenderCode + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "";
        }

        public static string GetUserOpModules(string userCode)
        {
            string str = ",";
            DataTable table = publicDbOpClass.DataTableQuary("SELECT b.C_MKDM FROM dbo.PT_MK a INNER JOIN dbo.PT_YHMC_Privilege b ON a.C_MKDM = b.C_MKDM WHERE (b.v_yhdm = '" + userCode + "') and (a.IsMaintainable = '1') AND (b.IsHaveOp = '0') order by a.i_xh,a.c_mkdm");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                str = str + table.Rows[i][0].ToString() + ",";
            }
            return str;
        }

        public int getUserParentDept(string yhdm)
        {
            return Convert.ToInt32(publicDbOpClass.DataTableQuary("select i_sjdm from pt_d_bm where i_bmdm = (select i_bmdm from pt_yhmc where v_yhdm = '" + yhdm + "')").Rows[0]["i_sjdm"].ToString());
        }

        public DataTable GetUserPurv(string userCode)
        {
            return publicDbOpClass.DataTableQuary("SELECT a.*, ISNULL(b.V_YHDM, 0) AS IsHave, ISNULL(b.IsHaveOp, 0) AS ISHaveOp,'1' as IsManagerFixed FROM dbo.PT_MK a LEFT OUTER JOIN dbo.PT_YHMC_Privilege b ON a.C_MKDM = b.C_MKDM AND b.V_YHDM = '" + userCode + "' order by a.i_xh,a.c_mkdm ");
        }

        public DataTable GetUserPurv(string userCode, string ManagerCode)
        {
            string str = "";
            str = "SELECT a.*, ISNULL(b.V_YHDM, 0) AS IsHave, ISNULL(b.IsHaveOp, 0) AS ISHaveOp, ISNULL(c.C_MKDM, 0) AS IsManagerFixed ";
            return publicDbOpClass.DataTableQuary((str + " FROM dbo.PT_MK a LEFT OUTER JOIN dbo.PT_YHMC_Privilege b ON a.C_MKDM = b.C_MKDM AND b.V_YHDM = '" + userCode + "'") + " LEFT OUTER JOIN dbo.PT_Manager_Privilege c ON a.C_MKDM = c.C_MKDM AND c.V_YHDM = '" + ManagerCode + "' order by a.i_xh,a.c_mkdm");
        }

        public string getUserPwd(string yhdm)
        {
            return publicDbOpClass.DataTableQuary("select v_dlmm from pt_login where v_yhdm = '" + yhdm + "'").Rows[0]["v_dlmm"].ToString();
        }

        public static string getUserSkin(string UserCode)
        {
            return publicDbOpClass.ExecuteScalar("select SkinID from pt_login where v_yhdm = '" + UserCode + "'").ToString();
        }

        public string getUserWorkRange(string yhdm)
        {
            return publicDbOpClass.DataTableQuary("select v_bgfw from pt_login where v_yhdm = '" + yhdm + "'").Rows[0]["v_bgfw"].ToString();
        }

        public static bool InsertRTXSynchronization(string UserCode, string Flag)
        {
            string sqlString = "";
            if (Flag == "y")
            {
                sqlString = "insert into pt_loginRtxInit (V_DLID,InitState,Dept) (select v_dlid,'0',v_bmqc from v_pt_AllUserInfoList where v_yhdm = '" + UserCode + "')";
            }
            else if (Flag == "n")
            {
                sqlString = "insert into pt_loginRtxInit (V_DLID,InitState,Dept) (select v_dlid,'2',v_bmqc from v_pt_AllUserInfoList where v_yhdm = '" + UserCode + "')";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool InsertRTXSynchronizationDept(string UserCode, string OldDept)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pt_loginRtxInit (V_DLID,InitState,Dept,PWD) (select v_dlid,'1','" + OldDept + ":'+v_bmqc,'' from v_pt_AllUserInfoList where v_yhdm = '" + UserCode + "')");
        }

        public bool InsertRTXSynchronizationPwd(string UserCode, string PassWord)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pt_loginRtxInit (V_DLID,InitState,Dept,PWD) (select v_dlid,'1',v_bmqc,'" + PassWord + "' from v_pt_AllUserInfoList where v_yhdm = '" + UserCode + "')");
        }

        public bool isHave(string mkdm, string yhdm)
        {
            if (publicDbOpClass.DataTableQuary("select v_xtqx from pt_login where v_yhdm = '" + yhdm + "'").Rows[0]["v_xtqx"].ToString().IndexOf(mkdm) == -1)
            {
                return false;
            }
            return true;
        }

        public static bool IsReturnLoginID(string code)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("select count(*) from PT_LOGIN where v_dlid = '" + code + "'");
            return ((obj2 != null) && (((int) obj2) != 0));
        }

        public string manageDept(string UserCode)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("select ControlDept from pt_login where v_yhdm = '" + UserCode + "'");
            if (table.Rows.Count != 0)
            {
                str = table.Rows[0]["ControlDept"].ToString();
            }
            return str;
        }

        public DataTable managePriv(string UserCode)
        {
            string str = "SELECT a.*, ISNULL(b.V_YHDM, 0) AS IsHave FROM dbo.PT_MK a LEFT OUTER JOIN ";
            return publicDbOpClass.DataTableQuary(str + " dbo.PT_Manager_Privilege b ON a.C_MKDM = b.C_MKDM AND b.V_YHDM = '" + UserCode + "'");
        }

        public bool managerDeptUpdate(string UserCode, string DeptCodes)
        {
            return publicDbOpClass.NonQuerySqlString("update pt_login set ControlDept = '" + DeptCodes + "' where v_yhdm = '" + UserCode + "'");
        }

        public bool managerUpdate(string UserCode, string moduleCode)
        {
            string[] sqlString = new string[] { " delete from PT_Manager_Privilege where V_YHDM = '" + UserCode + "'", "" };
            if (moduleCode.Trim().Length != 0)
            {
                sqlString[1] = " insert into PT_Manager_Privilege (V_YHDM,C_MKDM) select '" + UserCode + "',c_mkdm from pt_mk where c_mkdm in (" + moduleCode + ")";
            }
            return publicDbOpClass.ExecuteSQL(sqlString);
        }

        public bool updateRolePriv(string scopeCode, string moduleCode, string UserCode)
        {
            string[] sqlString = new string[] { " delete from PT_YHMC_Privilege where V_YHDM = '" + UserCode + "'", "", "" };
            if (moduleCode.Trim().Length != 0)
            {
                sqlString[1] = " insert into PT_YHMC_Privilege (V_YHDM,C_MKDM,IsBasic,IsHaveOp) select '" + UserCode + "',c_mkdm,IsBasic,'0' from pt_mk where c_mkdm in (" + moduleCode + ")";
            }
            if (scopeCode.Trim().Length != 0)
            {
                sqlString[2] = " update PT_YHMC_Privilege set IsHaveOp = '1' where V_YHDM = '" + UserCode + "' and C_MKDM in (" + scopeCode + ")";
            }
            return publicDbOpClass.ExecuteSQL(sqlString);
        }

        public bool updateUserAuditImg(string yhdm, string ANIP)
        {
            return publicDbOpClass.NonQuerySqlString(("update pt_login set AuditNameImagePath = '" + ANIP + "'") + " where v_yhdm = '" + yhdm + "'");
        }

        public bool updateUserAuditPwd(string yhdm, string password)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
            return publicDbOpClass.NonQuerySqlString(("update pt_login set AuditPwd = '" + str + "'") + " where v_yhdm = '" + yhdm + "'");
        }

        public bool updateUserAuditPwd(string yhdm, string password, string ANIP)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
            string str2 = "update pt_login set AuditPwd = '" + str + "'";
            if (ANIP != "")
            {
                str2 = str2 + " ,AuditNameImagePath = '" + ANIP + "'";
            }
            return publicDbOpClass.NonQuerySqlString(str2 + " where v_yhdm = '" + yhdm + "'");
        }

        public static int UpdateUserPriv(string UserCode)
        {
            return publicDbOpClass.ExecuteSQL("insert into PT_YHMC_Privilege (V_YHDM,C_MKDM,IsBasic,IsHaveOp) select '" + UserCode + "',c_mkdm,IsBasic,'0' from pt_mk WHERE IsBasic='1'");
        }

        public bool updateUserPwd(string dlid, string yhdm, string password)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
            publicDbOpClass.NonQuerySqlString("update pt_login set v_dlmm = '" + str + "' where v_yhdm = '" + yhdm + "'");
            return this.InsertRTXSynchronizationPwd(yhdm, password);
        }

        public static bool updateUserSkin(string UserCode, string SkinID)
        {
            return publicDbOpClass.NonQuerySqlString("update pt_login set SkinID = " + SkinID + " where v_yhdm = '" + UserCode + "'");
        }

        public bool userAdd(string dlid, string yhmc, string yhmm, string bmid, string zwbm, int yxdx, int yjsl, string jsdm)
        {
            int num;
            dlid = PublicClass.CheckString(dlid);
            yhmc = PublicClass.CheckString(yhmc);
            yhmm = PublicClass.CheckString(yhmm);
            yhmm = FormsAuthentication.HashPasswordForStoringInConfigFile(yhmm, "md5");
            new ChineseToSpell().WordToSpell(yhmc);
            bmid = bmid.PadLeft(3, '0');
            string str2 = publicDbOpClass.DataTableQuary("select max(v_yhdm)+1 as yhdm from pt_yhmc where v_yhdm like '" + bmid + "%'").Rows[0]["yhdm"].ToString();
            if (str2.Length == 0)
            {
                num = 1;
                str2 = bmid + "00001";
            }
            else
            {
                num = Convert.ToInt32(str2.Substring(str2.Length - 5, 5));
                str2 = bmid + str2.Substring(str2.Length - 5, 5);
            }
            DataTable table2 = new roleManageDb().roleQuary(jsdm);
            string str3 = table2.Rows[0]["v_bgfw"].ToString();
            string str4 = table2.Rows[0]["v_xtqx"].ToString();
            string sqlString = "begin ";
            object obj2 = sqlString;
            string str5 = string.Concat(new object[] { obj2, " insert into pt_yhmc (v_yhdm,v_xm,i_bmdm,c_sfyx,i_xh,i_dutyID) values ('", str2, "','", yhmc, "',", Convert.ToInt32(bmid), ",'y',", num, ",", Convert.ToInt32(zwbm), ")" }) + " insert into pt_login (v_dlid,v_yhdm,v_dlmm,v_bgfw,c_jsdm,v_xtqx,c_sfyx) values ('" + dlid + "',";
            sqlString = (str5 + "'" + str2 + "','" + yhmm + "','" + str3 + "','" + jsdm + "','" + str4 + "','y')") + " end;";
            bool flag = true;
            try
            {
                flag = publicDbOpClass.NonQuerySqlString(sqlString);
                if (flag)
                {
                    return true;
                }
                this._messageString = "添加新用户出错，可能是登录id已存在，操作失败！";
                flag = false;
            }
            catch (InvalidCastException exception)
            {
                this._messageString = exception.Message;
            }
            return flag;
        }

        public static bool UserAdd(Hashtable htPersonnel, string strLoginID)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile("easy", "md5");
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "insert into pt_login (v_dlid,v_yhdm,v_dlmm,v_bgfw,c_sfyx) values ('", strLoginID, "',", htPersonnel["v_yhdm"], ",'", str, "',',28,','y')" }));
        }

        public DataTable UserCompanyQuaryDt(string yhdm)
        {
            string str = this.userQuaryDt(yhdm).Rows[0]["i_bmdm"].ToString();
            return publicDbOpClass.DataTableQuary("select * from pt_d_bm where c_sfyx = 'y' and i_bmdm = " + Convert.ToInt32(str));
        }

        public static DataTable UserIsExist(string strLoginID)
        {
            return publicDbOpClass.DataTableQuary(" select * from pt_login where v_dlid = '" + strLoginID + "'");
        }

        public DataTable userLoginQuaryDt(string dlid)
        {
            dlid = PublicClass.CheckString(dlid);
            return publicDbOpClass.DataTableQuary("SELECT pt_login.*,pt_yhmc.v_xm FROM pt_login,pt_yhmc WHERE pt_login.v_dlid='" + dlid + "' and pt_login.v_yhdm=pt_yhmc.v_yhdm and pt_login.c_sfyx = 'y'");
        }

        public bool userMod(string yhdm, string yhmc, string mmFlag, string RoleCodes, string IsManager, string MailSpace, bool isChange)
        {
            string[] strArray5;
            yhmc = PublicClass.CheckString(yhmc);
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile("easy", "md5");
            string[] sqlString = new string[2];
            sqlString[0] = "update pt_login set RoleCodes = '" + RoleCodes + "',IsManager = '" + IsManager + "',MailSpace='" + MailSpace + "'";
            if (mmFlag == "y")
            {
                string[] strArray3;
                (strArray3 = sqlString)[0] = strArray3[0] + " ,v_dlmm = '" + str + "'";
            }
            if (isChange)
            {
                string[] strArray4;
                (strArray4 = sqlString)[0] = strArray4[0] + " ,AuditPwd = 'easy'";
            }
            (strArray5 = sqlString)[0] = strArray5[0] + " where v_yhdm = '" + yhdm + "'";
            sqlString[1] = " exec P_Plat_UpdUserPriv '" + yhdm + "','" + RoleCodes + "'";
            bool flag = true;
            try
            {
                flag = publicDbOpClass.ExecuteSQL(sqlString);
                if (flag)
                {
                    if (mmFlag == "y")
                    {
                        this.InsertRTXSynchronizationPwd(yhdm, "easy");
                    }
                    return true;
                }
                this._messageString = "修改用户出错，可能是登录id已存在，操作失败！";
                flag = false;
            }
            catch (InvalidCastException exception)
            {
                this._messageString = exception.Message;
            }
            return flag;
        }

        public bool userPrivilegeSet(string mkdm, string yhdm)
        {
            return publicDbOpClass.NonQuerySqlString("update pt_login set v_xtqx = '" + mkdm + "' where v_yhdm = '" + yhdm + "'");
        }

        public DataTable userQuaryDt(string yhdm)
        {
            string str = "SELECT b.i_bmdm AS i_bmdm, b.V_BMMC AS V_BMMC,b.v_bmqc as v_bmqc, a.v_xm AS v_xm, a.i_xh AS i_xh, a.I_DUTYID AS I_DUTYID, c.* ";
            return publicDbOpClass.DataTableQuary((str + " FROM dbo.PT_yhmc a INNER JOIN dbo.PT_d_bm b ON a.i_bmdm = b.i_bmdm INNER JOIN dbo.PT_LOGIN c ON a.v_yhdm = c.V_YHDM") + " and a.v_yhdm='" + yhdm + "'");
        }

        public bool userRigthMenuSet(string mkdm, string yhdm)
        {
            return publicDbOpClass.NonQuerySqlString("update pt_login set v_cycd = '" + mkdm + "' where v_yhdm = '" + yhdm + "'");
        }

        public bool userUpdate(string yhdm, string flag)
        {
            string str = "";
            str = "begin ";
            string str3 = str;
            string str4 = str3 + " update pt_yhmc set c_sfyx = '" + flag + "' where v_yhdm = '" + yhdm + "'";
            bool flag2 = publicDbOpClass.NonQuerySqlString((str4 + " update pt_login set c_sfyx = '" + flag + "' where v_yhdm = '" + yhdm + "'") + " end;");
            string str2 = "";
            if (flag == "y")
            {
                str2 = "还原用户";
            }
            else if (flag == "n")
            {
                str2 = "删除用户";
            }
            if (flag2)
            {
                return true;
            }
            this._messageString = str2 + "出错，可能是登录id已存在，操作失败！";
            return false;
        }

        public static object ValidatorLoginAccess(string userName, string password)
        {
            string str = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
            return publicDbOpClass.ExecuteScalar("select v_yhdm from pt_login where v_dlid = '" + userName + "'collate chinese_PRC_CS_AI and v_dlmm = '" + str + "' and c_sfyx = 'y'");
        }

        public string MessageString
        {
            get
            {
                return this._messageString;
            }
        }

        public Page ObjPage
        {
            get
            {
                return this._objPage;
            }
            set
            {
                this._objPage = value;
            }
        }
    }
}

