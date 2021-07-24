namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Web;

    public class MailManage
    {
        private string _extName = "";
        private int _fileNumber;
        private int _fileSize;
        private string _messageString = "";
        private string _uploadPath = "";

        public bool AddlatelyLinkman(string UserCode, string[] linkMansCode, string[] LinkMansName)
        {
            string sqlString = "";
            for (int i = 0; i < (linkMansCode.Length - 1); i++)
            {
                switch (publicDbOpClass.DataTableQuary("select sum(sendcount) from PT_PAL_LIST where palyhdm= '" + linkMansCode[i] + "' and v_yhdm='" + UserCode + "'").Rows[0][0].ToString())
                {
                    case null:
                    case "":
                    {
                        object obj2 = sqlString;
                        sqlString = string.Concat(new object[] { obj2, " insert into PT_PAL_LIST (V_YHDM,palyhdm,palName,sendcount,lastsendTime) values ('", UserCode, "','", linkMansCode[i].Trim().ToString(), "','", LinkMansName[i].Trim().ToString(), "','1','", DateTime.Now, "')" });
                        break;
                    }
                    default:
                    {
                        object obj3 = sqlString;
                        sqlString = string.Concat(new object[] { obj3, " update  PT_PAL_LIST set sendcount=sendcount+1, lastsendTime='", DateTime.Now, "' where V_YHDM='", UserCode, "' and palyhdm='", linkMansCode[i], "'" });
                        break;
                    }
                }
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool AddLinkman(string UserCode, string LinkMans, Guid groudId)
        {
            string[] strArray = LinkMans.Split(new char[] { ',' });
            string sqlString = "";
            for (int i = 0; i < (strArray.Length - 1); i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { ':' });
                string str2 = sqlString;
                object obj2 = str2 + " delete from pt_dzyj_cylxr where UserCode = '" + UserCode + "' and RelationUserCode = '" + strArray2[1] + "'";
                sqlString = string.Concat(new object[] { obj2, " insert into pt_dzyj_cylxr (UserCode,RelationUserCode,groupId) values ('", UserCode, "','", strArray2[1], "','", groudId, "')" });
            }
            return ((sqlString != "") && publicDbOpClass.NonQuerySqlString(sqlString));
        }

        public bool ClearTempAnnex()
        {
            string sqlString = "";
            sqlString = "Truncate table pt_dzyj_Tempfj ";
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool DelLinkman(string UserCode, string LinkMan)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_dzyj_cylxr where UserCode = '" + UserCode + "' and RelationUserCode = '" + LinkMan + "'");
        }

        public bool DelMail(int iMailID, string strUserCode)
        {
            string sqlString = "";
            sqlString = "select i_fsrs from pt_dzyj_sj where i_sjid = " + iMailID.ToString();
            if (publicDbOpClass.ExecuteScalar(sqlString) == DBNull.Value)
            {
                return true;
            }
            if (Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString)) <= 1)
            {
                sqlString = "";
                DataTable mailAnnex = this.GetMailAnnex(iMailID);
                FileUpload upload = new FileUpload();
                if (mailAnnex.Rows.Count > 0)
                {
                    for (int i = 0; i < mailAnnex.Rows.Count; i++)
                    {
                        if (this.IsDel(iMailID, mailAnnex.Rows[i]["v_fjlj"].ToString(), mailAnnex.Rows[i]["v_lmc"].ToString()) && !upload.Delete(mailAnnex.Rows[i]["v_fjlj"].ToString() + mailAnnex.Rows[i]["v_lmc"].ToString()))
                        {
                            return false;
                        }
                        sqlString = sqlString + " delete from pt_dzyj_fj where (i_sjid=" + iMailID.ToString() + ")";
                    }
                }
                sqlString = "update pt_dzyj_yj set c_xbs='n',c_sfyx = 'h' where  i_sjid=" + iMailID.ToString() + ";";
            }
            else
            {
                sqlString = "update pt_dzyj_yj set c_xbs='n',c_sfyx = 'h' where (i_sjid=" + iMailID.ToString() + ")and((v_jsrdm='" + strUserCode + "')or(Len(v_jsrdm)<>8));";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool DelTempAnnex(int iAnnexID, bool isDel)
        {
            string sqlString = "";
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_dzyj_Tempfj where i_fj_id = " + iAnnexID.ToString());
            if (table.Rows.Count <= 0)
            {
                return true;
            }
            if (table.Rows[0]["i_IsSecond"].ToString() == "0")
            {
                if (this.IsDel(table.Rows[0]["v_AnnexPath"].ToString(), table.Rows[0]["v_AnnexName"].ToString()))
                {
                    string strFileName = table.Rows[0]["v_AnnexPath"].ToString() + table.Rows[0]["v_AnnexName"].ToString();
                    FileUpload upload = new FileUpload();
                    if (!upload.Delete(strFileName))
                    {
                        this._messageString = "删除文件出错!";
                        return false;
                    }
                }
                return publicDbOpClass.NonQuerySqlString("delete from pt_dzyj_Tempfj where i_fj_id = " + iAnnexID.ToString());
            }
            if (isDel)
            {
                sqlString = "delete from pt_dzyj_Tempfj where i_fj_id = " + iAnnexID.ToString();
            }
            else
            {
                sqlString = "update pt_dzyj_Tempfj set i_IsDel=1 where i_fj_id = " + iAnnexID.ToString();
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public int getAllAnnexSize(string UserCode)
        {
            string sqlString = "SELECT SUM(a.I_FJDX) AS AllAnnexSize FROM dbo.PT_DZYJ_SJ a INNER JOIN dbo.PT_DZYJ_YJ b ON a.I_SJID = b.I_SJID WHERE (b.V_JSRDM = '" + UserCode + "') AND (b.C_SFYX = 's')";
            if (publicDbOpClass.ExecuteScalar(sqlString).ToString() != "")
            {
                return (int) publicDbOpClass.ExecuteScalar(sqlString);
            }
            return 0;
        }

        public DataTable GetConsignee(int iMailID)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_dzyj_yj where i_sjid = " + iMailID.ToString());
        }

        public int GetDiskSpace(string strSenderCode)
        {
            int num = 0;
            string sqlString = "select sum(a.i_FjSize) from pt_dzyj_fj a,pt_dzyj_yj b, pt_dzyj_sj c where (a.i_sjid=b.i_sjid)and(b.i_sjid=c.i_sjid)and(c.v_fxrdm='" + strSenderCode + "')and(Len(b.v_jsrdm)<>8)";
            if (publicDbOpClass.ExecuteScalar(sqlString) != DBNull.Value)
            {
                num += Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString));
            }
            sqlString = "select sum(a.i_FjSize) from pt_dzyj_fj a,pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strSenderCode + "')";
            if (publicDbOpClass.ExecuteScalar(sqlString) != DBNull.Value)
            {
                num += Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString));
            }
            return num;
        }

        public DataTable GetDraftMail(string strConsigneeCode)
        {
            return publicDbOpClass.DataTableQuary("select a.i_sjid,a.i_sysid,i_fjdx,a.v_zt,a.dtm_sjsj,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj, b.v_jsrxm, b.c_xbs from pt_dzyj_sj a,pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(a.v_fxrdm='" + strConsigneeCode + "')and(b.c_sfyx='c') order by a.i_sjid desc");
        }

        public int GetDraftMailNumber(string strUserCode)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(a.v_fxrdm='" + strUserCode + "')and(b.c_sfyx='c')"));
        }

        public int GetDraftMailSize(string strUserCode)
        {
            int num = 0;
            string sqlString = "";
            sqlString = "select sum(a.i_fjSize) from pt_dzyj_fj a,pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strUserCode + "')and(b.c_sfyx='c')";
            if (publicDbOpClass.ExecuteScalar(sqlString) != DBNull.Value)
            {
                num += Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString));
            }
            return num;
        }

        private int GetFileNumber(string strSenderCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select count(1) as iNumber from pt_dzyj_Tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0) and (I_ISSECOND=0)");
            if (Convert.ToInt32(table.Rows[0]["iNumber"].ToString()) > 0)
            {
                return int.Parse(table.Rows[0]["iNumber"].ToString());
            }
            return 0;
        }

        public DataTable GetInMail(string strConsigneeCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select a.i_sjid,a.i_sysid,a.v_fxrdm,i_fjdx,a.I_FJSL,a.v_fxrxm,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,a.i_MailType,b.c_xbs,b.dtm_fssj from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strConsigneeCode + "')and(b.c_xbs='y')and(b.c_sfyx='s')and(a.i_MailType=0)and(dtm_fssj>(select dt_SecretaryDate from pt_dzyj_UserSet where v_UserCode='" + strConsigneeCode + "')) order by a.i_sjid desc");
            DataTable table2 = publicDbOpClass.DataTableQuary("select a.i_sjid,a.i_sysid,a.v_fxrdm,a.v_fxrxm,a.I_FJSL,i_fjdx,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,a.i_MailType,b.c_xbs,b.dtm_fssj from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strConsigneeCode + "')and(b.c_xbs<>'y')and(b.c_sfyx='s')and(a.i_MailType=0)and(dtm_fssj>(select dt_SecretaryDate from pt_dzyj_UserSet where v_UserCode='" + strConsigneeCode + "')) order by a.i_sjid desc");
            if (table.Rows.Count <= 0)
            {
                return table2;
            }
            object[] itemArray = new object[8];
            foreach (DataRow row2 in table2.Rows)
            {
                DataRow row = table.NewRow();
                itemArray = row2.ItemArray;
                row.ItemArray = itemArray;
                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable GetInMail(string strConsigneeCode, int iMailType)
        {
            DataTable table;
            DataTable table2;
            if (iMailType == -1)
            {
                table = publicDbOpClass.DataTableQuary("select d.v_bmmc, a.i_sjid,a.i_sysid,a.v_fxrdm,a.v_fxrxm,a.I_FJSL,a.i_fjdx,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,a.i_MailType,b.c_xbs,b.dtm_fssj from  pt_dzyj_sj a inner join  pt_dzyj_yj b on  a.i_sjid=b.i_sjid inner join dbo.PT_yhmc c on a.v_fxrdm=c.v_yhdm inner join dbo.PT_d_bm d on c.i_bmdm=d.i_bmdm where (b.v_jsrdm='" + strConsigneeCode + "')and(b.c_xbs='y')and(b.c_sfyx='s') order by a.i_sjid desc");
                table2 = publicDbOpClass.DataTableQuary("select d.v_bmmc, a.i_sjid,a.i_sysid,a.v_fxrdm,a.v_fxrxm,a.I_FJSL,a.i_fjdx,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,a.i_MailType,b.c_xbs,b.dtm_fssj from pt_dzyj_sj a inner join  pt_dzyj_yj b on  a.i_sjid=b.i_sjid inner join dbo.PT_yhmc c on a.v_fxrdm=c.v_yhdm inner join dbo.PT_d_bm d on c.i_bmdm=d.i_bmdm where (b.v_jsrdm='" + strConsigneeCode + "')and(b.c_xbs<>'y')and(b.c_sfyx='s') order by a.i_sjid desc");
            }
            else
            {
                table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select d.v_bmmc, a.i_sjid,a.i_sysid,a.v_fxrdm,a.I_FJSL,a.v_fxrxm,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,a.i_MailType,b.c_xbs,b.dtm_fssj from pt_dzyj_sj a inner join  pt_dzyj_yj b on  a.i_sjid=b.i_sjid inner join dbo.PT_yhmc c on a.v_fxrdm=c.v_yhdm inner join dbo.PT_d_bm d on c.i_bmdm=d.i_bmdm where (b.v_jsrdm='", strConsigneeCode, "')and(b.c_xbs='y')and(b.c_sfyx='s')and(a.i_MailType=", iMailType, ") order by a.i_sjid desc" }));
                table2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select d.v_bmmc, a.i_sjid,a.i_sysid,a.v_fxrdm,a.I_FJSL,a.v_fxrxm,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,a.i_MailType,b.c_xbs,b.dtm_fssj from pt_dzyj_sj a inner join  pt_dzyj_yj b on  a.i_sjid=b.i_sjid inner join dbo.PT_yhmc c on a.v_fxrdm=c.v_yhdm inner join dbo.PT_d_bm d on c.i_bmdm=d.i_bmdm where (b.v_jsrdm='", strConsigneeCode, "')and(b.c_xbs<>'y')and(b.c_sfyx='s')and(a.i_MailType=", iMailType, ") order by a.i_sjid desc" }));
            }
            if (table.Rows.Count <= 0)
            {
                return table2;
            }
            object[] itemArray = new object[8];
            foreach (DataRow row2 in table2.Rows)
            {
                DataRow row = table.NewRow();
                itemArray = row2.ItemArray;
                row.ItemArray = itemArray;
                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable GetInMailis(string strConsigneeCode, int iMailType)
        {
            return publicDbOpClass.DataTableQuary("select a.i_sjid,a.i_sysid,a.v_fxrdm,a.v_fxrxm,a.I_FJSL,a.i_fjdx,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,a.i_MailType,b.c_xbs,b.dtm_fssj from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strConsigneeCode + "')and(b.c_xbs<>'y')and(b.c_sfyx in ('s','l','h')) order by a.i_sjid desc");
        }

        public DataTable GetInMailiss(string strConsigneeCode, int iMailType)
        {
            string sqlString = "";
            sqlString = "select a.i_sjid,a.i_sysid,a.v_fxrdm,a.v_fxrxm,b.V_JSRXM,a.I_FJSL,a.i_fjdx,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,a.i_MailType,b.c_xbs,b.dtm_fssj from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid) and(b.c_xbs<>'y')and(b.c_sfyx in ('s','l','h')) order by a.i_sjid desc";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int GetInMailNumber(string strUserCode)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strUserCode + "')and(b.c_sfyx='s')"));
        }

        public int GetInMailSize(string strUserCode)
        {
            int num = 0;
            string sqlString = "";
            sqlString = "select sum(a.i_fjSize) from pt_dzyj_fj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strUserCode + "')and(b.c_sfyx='s')";
            if (publicDbOpClass.ExecuteScalar(sqlString) != DBNull.Value)
            {
                num += Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString));
            }
            return num;
        }

        public DataTable getlatelyLinkman(string ym)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "select top 20 * from PT_PAL_LIST where V_YHDM='" + ym + "' order by sendcount desc ");
        }

        public DataTable GetMailAnnex(int iMailID)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_dzyj_fj where i_sjid = " + iMailID.ToString());
        }

        public int GetMailNumber(string strUserCode)
        {
            return (((this.GetInMailNumber(strUserCode) + this.GetOutMailNumber(strUserCode)) + this.GetDraftMailNumber(strUserCode)) + this.GetTrashMailNumber(strUserCode));
        }

        public DataTable GetNewMail(string strConsigneeCode)
        {
            return publicDbOpClass.DataTableQuary("select a.i_sjid,a.v_fxrdm,a.v_fxrxm,a.v_zt,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj,a.i_fjdx,b.c_xbs,b.dtm_fssj from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strConsigneeCode + "')and(b.c_xbs='y')and(b.c_sfyx='s') order by a.i_sjid desc");
        }

        public int GetNewMailNumber(string strUserCode)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(b.v_jsrdm='" + strUserCode + "')and(b.c_xbs='y')and(b.c_sfyx='s')"));
        }

        public DataTable GetOneMail(int iMailID, string strConsigneeCode)
        {
            return publicDbOpClass.DataTableQuary("select a.i_SysID,a.v_fxrdm,a.i_fjdx,a.v_fxrxm,a.v_zt,a.dtm_sjsj,a.txt_zw,a.i_fjsl,a.i_MailType,b.v_jsrdm,b.v_jsrxm,V_SJR,V_CSR from pt_dzyj_sj a ,pt_dzyj_yj b where (a.i_sjid = b.i_sjid) and (a.i_sjid=" + iMailID.ToString() + ")");
        }

        public DataTable GetOutMail(string strConsigneeCode)
        {
            return publicDbOpClass.DataTableQuary("select a.i_sjid,a.i_sysid,a.i_fjdx,a.I_FJSL,a.v_zt,a.dtm_sjsj,                  case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj, b.v_jsrxm, b.c_xbs from pt_dzyj_sj a inner join pt_dzyj_yj b on (a.i_sjid=b.i_sjid) inner join dbo.PT_yhmc c on a.v_fxrdm=c.v_yhdm inner join dbo.PT_d_bm d on c.i_bmdm=d.i_bmdm   WHERE (a.v_fxrdm='" + strConsigneeCode + "')and(b.c_sfyx='d') order by a.i_sjid desc");
        }

        public int GetOutMailNumber(string strUserCode)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(a.v_fxrdm='" + strUserCode + "')and(b.c_sfyx='d')"));
        }

        public int GetOutMailSize(string strUserCode)
        {
            int num = 0;
            string sqlString = "";
            sqlString = "select sum(a.i_fjSize) from pt_dzyj_fj a,pt_dzyj_sj b,pt_dzyj_yj c where (a.i_sjid=b.i_sjid)and(b.i_sjid=c.i_sjid)and(b.v_fxrdm='" + strUserCode + "')and(c.c_sfyx='d')";
            if (publicDbOpClass.ExecuteScalar(sqlString) != DBNull.Value)
            {
                num += Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString));
            }
            return num;
        }

        public int getPageSize(string UserCode)
        {
            return (int) publicDbOpClass.ExecuteScalar("select MailPageSize from PT_LOGIN where v_yhdm = '" + UserCode + "'");
        }

        private PTDBSJ getPTDBSJ(string xgid, string Mes, string jsyhdm)
        {
            return new PTDBSJ { V_LXBM = "008", I_XGID = xgid, DTM_DBSJ = DateTime.Now, V_Content = Mes, V_DBLJ = "?rid=" + xgid, V_YHDM = jsyhdm.Replace("0:", "").Trim() };
        }

        public DataTable GetTempAnnex(string strSenderCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_dzyj_Tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0) and (I_ISSECOND=0)");
        }

        public DataTable GetTrashMail(string strConsigneeCode)
        {
            return publicDbOpClass.DataTableQuary("select a.i_sjid,a.i_sysid,a.i_fjdx,a.I_FJSL,a.v_zt,a.dtm_sjsj,case when a.i_fjsl>0 then 'y' when a.i_fjsl=0 then 'n' end as c_fj, b.v_jsrxm, b.c_xbs from pt_dzyj_sj a,pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(((a.v_fxrdm='" + strConsigneeCode + "')and(len(b.v_jsrdm)<>8)) or (b.v_jsrdm='" + strConsigneeCode + "'))and(b.c_sfyx='l') order by a.i_sjid desc");
        }

        public int GetTrashMailNumber(string strUserCode)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) from pt_dzyj_sj a, pt_dzyj_yj b where (a.i_sjid=b.i_sjid)and(((a.v_fxrdm='" + strUserCode + "')and(len(b.v_jsrdm)<>8)) or (b.v_jsrdm='" + strUserCode + "'))and(b.c_sfyx='l')"));
        }

        public int GetTrashMailSize(string strUserCode)
        {
            int num = 0;
            string sqlString = "select sum(a.i_fjSize) from pt_dzyj_fj a,pt_dzyj_sj b,pt_dzyj_yj c where (a.i_sjid=b.i_sjid)and(b.i_sjid=c.i_sjid)and(((b.v_fxrdm='" + strUserCode + "')and(len(c.v_jsrdm)<>8)) or (c.v_jsrdm='" + strUserCode + "'))and(c.c_sfyx='l')";
            if (publicDbOpClass.ExecuteScalar(sqlString) != DBNull.Value)
            {
                num += Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString));
            }
            return num;
        }

        private bool GetUploadSet()
        {
            string sqlString = "";
            sqlString = "select * from pt_dzyj_set";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if (table.Rows.Count > 0)
            {
                this._fileSize = 0x7fffffff;
                this._fileNumber = int.Parse(table.Rows[0]["i_FileNum"].ToString());
                this._uploadPath = table.Rows[0]["v_AnnexPath"].ToString();
                this._extName = table.Rows[0]["v_ExtName"].ToString();
                return true;
            }
            this._messageString = "邮件上传设置不正确！";
            return false;
        }

        public int getUserMailSpace(string UserCode)
        {
            return (int) publicDbOpClass.ExecuteScalar("select MailSpace from pt_login where v_yhdm = '" + UserCode + "'");
        }

        public DataTable GetUserSet(string strUserCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_dzyj_UserSet where v_UserCode='" + strUserCode + "'");
        }

        public bool isAllowSize(string strUserCode)
        {
            return true;
        }

        public bool IsDel(string strAnnexPath, string strAnnexName)
        {
            if (Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) as iFileNumber from pt_dzyj_fj where (v_fjlj='" + strAnnexPath + "')and(v_lmc='" + strAnnexName + "')")) > 0)
            {
                return false;
            }
            return true;
        }

        public bool IsDel(int iMailID, string strAnnexPath, string strAnnexName)
        {
            if (Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) as iFileNumber from pt_dzyj_fj where (i_sjid <> " + iMailID.ToString() + ")and(v_fjlj='" + strAnnexPath + "')and(v_lmc='" + strAnnexName + "')")) > 0)
            {
                return false;
            }
            return true;
        }

        public bool MoveToDraft(int iMailID, int iSysID, string strUserCode, string strMailBox)
        {
            bool flag = false;
            int num = 0;
            string str2 = "";
            DataTable table = publicDbOpClass.DataTableQuary("select v_jsrdm,v_jsrxm from pt_dzyj_yj where (i_sjid=" + iMailID.ToString() + ")and(v_jsrdm='" + strUserCode + "')");
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0]["v_jsrxm"].ToString();
            }
            string str3 = strMailBox.ToLower();
            if (str3 != null)
            {
                if (!(str3 == "d"))
                {
                    if (str3 != "s")
                    {
                        if (str3 == "l")
                        {
                            num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
                            string str6 = " insert into pt_dzyj_sj select " + num.ToString() + "," + iSysID.ToString() + ",'" + strUserCode + "','" + str2 + "',dtm_sjsj,v_zt,txt_zw,i_fjsl,i_fsrs,i_fjdx,i_MailType,V_SJR,V_CSR from pt_dzyj_sj where i_sjid=" + iMailID.ToString() + ";";
                            string str7 = str6 + " insert into pt_dzyj_yj select " + num.ToString() + "," + iSysID.ToString() + ",'" + iSysID.ToString() + ":" + strUserCode + "!','" + str2 + ",',dtm_fssj,'n','c' from pt_dzyj_yj where (i_sjid=" + iMailID.ToString() + ")and((v_jsrdm='" + strUserCode + "')or(Len(v_jsrdm)<>8));";
                            if (publicDbOpClass.NonQuerySqlString(str7 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_fjlj,v_lmc,i_fjSize,v_OrgPath from pt_dzyj_fj where i_sjid=" + iMailID.ToString() + ";"))
                            {
                                flag = this.DelMail(iMailID, strUserCode);
                            }
                        }
                        return flag;
                    }
                }
                else
                {
                    return publicDbOpClass.NonQuerySqlString("update pt_dzyj_yj set c_xbs='n',c_sfyx='c' where i_sjid=" + iMailID.ToString());
                }
                num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
                string str4 = " insert into pt_dzyj_sj select " + num.ToString() + "," + iSysID.ToString() + ",'" + strUserCode + "','" + str2 + "',dtm_sjsj,v_zt,txt_zw,i_fjsl,i_fsrs,i_fjdx,i_MailType ,V_SJR,V_CSR from pt_dzyj_sj where i_sjid=" + iMailID.ToString() + ";";
                string str5 = str4 + " insert into pt_dzyj_yj select " + num.ToString() + "," + iSysID.ToString() + ",'" + iSysID.ToString() + ":" + strUserCode + "!','" + str2 + ",',dtm_fssj,'n','c' from pt_dzyj_yj where (i_sjid=" + iMailID.ToString() + ")and(v_jsrdm='" + strUserCode + "');";
                if (publicDbOpClass.NonQuerySqlString(str5 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_fjlj,v_lmc,i_fjSize,v_OrgPath from pt_dzyj_fj where i_sjid=" + iMailID.ToString() + ";"))
                {
                    flag = this.DelMail(iMailID, strUserCode);
                }
            }
            return flag;
        }

        public bool MoveToInBox(int iMailID, int iSysID, string strUserCode, string strMailBox)
        {
            string sqlString = "";
            string userName = "";
            userName = new userManageDb().GetUserName(strUserCode);
            string str3 = strMailBox.ToLower();
            if (str3 != null)
            {
                if (!(str3 == "c") && !(str3 == "d"))
                {
                    if (str3 == "l")
                    {
                        sqlString = string.Concat(new object[] { "update pt_dzyj_yj set c_xbs='n',v_jsrdm='", strUserCode, "',v_jsrxm='", userName, "',c_sfyx='s' where (i_sjid=", iMailID, ")and((v_jsrdm='", strUserCode, "')or(Len(v_jsrdm)<>8))" });
                    }
                }
                else
                {
                    sqlString = "update pt_dzyj_yj set c_xbs='n',i_SysID=" + iSysID.ToString() + ",v_jsrdm='" + strUserCode + "',v_jsrxm='" + userName + "',c_sfyx='s' where i_sjid=" + iMailID.ToString();
                }
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool MoveToOutBox(int iMailID, int iSysID, string strUserCode, string strMailBox)
        {
            bool flag = false;
            int num = 0;
            string userName = "";
            userName = new userManageDb().GetUserName(strUserCode);
            string str3 = strMailBox.ToLower();
            if (str3 != null)
            {
                if (!(str3 == "c"))
                {
                    if (str3 != "s")
                    {
                        if (str3 == "l")
                        {
                            num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
                            string str6 = " insert into pt_dzyj_sj select " + num.ToString() + "," + iSysID.ToString() + ",'" + strUserCode + "','" + userName + "',dtm_sjsj,v_zt,txt_zw,i_fjsl,i_fsrs,i_fjdx,i_MailType,V_SJR,V_CSR from pt_dzyj_sj where i_sjid=" + iMailID.ToString() + ";";
                            string str7 = str6 + " insert into pt_dzyj_yj select " + num.ToString() + ",i_SysID,'" + iSysID.ToString() + ":" + strUserCode + "!','" + userName + ",',DTM_FSSJ,'n','d' from pt_dzyj_yj where (i_sjid=" + iMailID.ToString() + ")and((v_jsrdm='" + strUserCode + "')or(Len(v_jsrdm)<>8));";
                            if (publicDbOpClass.NonQuerySqlString(str7 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_fjlj,v_lmc,i_fjSize,v_OrgPath from pt_dzyj_fj where i_sjid=" + iMailID.ToString() + ";"))
                            {
                                flag = this.DelMail(iMailID, strUserCode);
                            }
                        }
                        return flag;
                    }
                }
                else
                {
                    return publicDbOpClass.NonQuerySqlString("update pt_dzyj_yj set c_xbs='n',c_sfyx='d' where i_sjid=" + iMailID.ToString());
                }
                num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
                string str4 = "insert into pt_dzyj_sj select " + num.ToString() + "," + iSysID.ToString() + ",'" + strUserCode + "','" + userName + "',dtm_sjsj,v_zt,txt_zw,i_fjsl,i_fsrs,i_fjdx,i_MailType,V_SJR,V_CSR  from pt_dzyj_sj where i_sjid =" + iMailID.ToString() + ";";
                string str5 = str4 + "insert into pt_dzyj_yj select " + num.ToString() + "," + iSysID.ToString() + ",'" + iSysID.ToString() + ":" + strUserCode + "!','" + userName + ",',DTM_FSSJ,'n','d' from pt_dzyj_yj where (i_sjid=" + iMailID.ToString() + ")and(v_jsrdm='" + strUserCode + "');";
                if (publicDbOpClass.NonQuerySqlString(str5 + "insert into pt_dzyj_fj select " + num.ToString() + ",v_fjlj,v_lmc,i_fjSize,v_OrgPath from pt_dzyj_fj where i_sjid = " + iMailID.ToString() + ";"))
                {
                    flag = this.DelMail(iMailID, strUserCode);
                }
            }
            return flag;
        }

        public bool MoveToRecycle(int iMailID, string strUserCode)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update pt_dzyj_yj set c_xbs='n',c_sfyx = 'l' where (i_sjid=", iMailID, ")and((v_jsrdm='", strUserCode, "')or(Len(v_jsrdm)<>8));" }));
        }

        public bool ReadMail(int iMailID, string strUserCode)
        {
            string str2 = "";
            return publicDbOpClass.NonQuerySqlString(str2 + " update pt_dzyj_yj set c_xbs='n' where (c_xbs='y')and(i_sjid=" + iMailID.ToString() + " ) and v_jsrdm='" + strUserCode + "';");
        }

        public bool RecdeskTookip(string sjid)
        {
            string str = "begin ";
            return publicDbOpClass.NonQuerySqlString(((str + " Delete from PT_DBSJ_Today where I_XGID='" + sjid + "' and  V_LXBM='008'") + " Delete from PT_DBSJ where I_XGID='" + sjid + "' and  V_LXBM='008' ") + " end ");
        }

        public bool ReclaimMain(int MailID)
        {
            return publicDbOpClass.NonQuerySqlString(" delete from pt_dzyj_yj where i_yj_id = " + MailID);
        }

        public bool ReEditAnnex(int iMailID, string strSenderCode)
        {
            int num = 0;
            publicDbOpClass.NonQuerySqlString("delete from pt_dzyj_tempfj where v_UserCode='" + strSenderCode + "'");
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_dzyj_fj where i_sjid = " + iMailID.ToString());
            if (table.Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_Tempfj", "i_fj_id").ToString()) + 1;
                        publicDbOpClass.NonQuerySqlString(" insert into pt_dzyj_Tempfj values(" + num.ToString() + ",'" + strSenderCode + "','" + row["v_fjlj"].ToString() + "','" + row["v_Lmc"].ToString() + "'," + row["i_fjSize"].ToString() + ",0,1)");
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public bool SaveToDraft(int iSysID, string strSenderCode, string strSenderName, string strTitle, string strContent, string strUserCode, string strUserName, int iMailType)
        {
            int num = 0;
            string str = "";
            num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
            string str3 = str;
            string str4 = str3 + " insert into pt_dzyj_sj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strSenderCode + "','" + strSenderName + "',getdate(),'" + strTitle + "','" + strContent + "',0,1,0," + iMailType.ToString() + ",'','');";
            string str5 = str4 + " insert into pt_dzyj_yj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strUserCode + "','" + strUserName + "',getdate(),'c','c');";
            str = str5 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_AnnexPath,v_AnnexName,i_AnnexSize,'' from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);";
            if (Convert.ToInt32(publicDbOpClass.ExecuteScalar(" select Count(1) from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0)")) > 0)
            {
                string str6 = str;
                str = str6 + " update pt_dzyj_sj set i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid = " + num.ToString() + "),i_fsrs=(select count(1) from pt_dzyj_yj where i_sjid=" + num.ToString() + "),i_fjdx=(select sum(i_fjsize) from pt_dzyj_fj where i_sjid=" + num.ToString() + ") where i_sjid=" + num.ToString() + ";";
            }
            else
            {
                string str7 = str;
                str = str7 + " update pt_dzyj_sj set i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid = " + num.ToString() + "),i_fsrs=(select count(1) from pt_dzyj_yj where i_sjid=" + num.ToString() + ") where i_sjid=" + num.ToString() + ";";
            }
            return publicDbOpClass.NonQuerySqlString(str + " update pt_dzyj_Tempfj set i_IsSecond=1 where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);");
        }

        public bool SaveToDraft(int iSysID, string strSenderCode, string strSenderName, string strTitle, string strContent, string strUserCode, string strUserName, int iMailType, string SJR, string FJR)
        {
            int num = 0;
            string str = "";
            num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
            string str3 = str;
            string str4 = str3 + " insert into pt_dzyj_sj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strSenderCode + "','" + strSenderName + "',getdate(),'" + strTitle + "','" + strContent + "',0,1,0," + iMailType.ToString() + ",'" + SJR + "','" + FJR + "');";
            string str5 = str4 + " insert into pt_dzyj_yj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strUserCode + "','" + strUserName + "',getdate(),'c','c');";
            str = str5 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_AnnexPath,v_AnnexName,i_AnnexSize,'' from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0) and (i_IsSecond=1)";
            if (Convert.ToInt32(publicDbOpClass.ExecuteScalar(" select Count(1) from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0)")) > 0)
            {
                string str6 = str;
                str = str6 + " update pt_dzyj_sj set i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid = " + num.ToString() + "),i_fsrs=(select count(1) from pt_dzyj_yj where i_sjid=" + num.ToString() + "),i_fjdx=(select sum(i_fjsize) from pt_dzyj_fj where i_sjid=" + num.ToString() + ") where i_sjid=" + num.ToString() + ";";
            }
            else
            {
                string str7 = str;
                str = str7 + " update pt_dzyj_sj set i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid = " + num.ToString() + "),i_fsrs=(select count(1) from pt_dzyj_yj where i_sjid=" + num.ToString() + ") where i_sjid=" + num.ToString() + ";";
            }
            return publicDbOpClass.NonQuerySqlString(str + " update pt_dzyj_Tempfj set i_IsSecond=1 where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);");
        }

        public bool SaveToOut(int iSysID, string strSenderCode, string strSenderName, string strTitle, string strContent, string strUserCode, string strUserName, int iMailType)
        {
            int num = 0;
            string str = "";
            num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
            string str3 = str;
            string str4 = str3 + " insert into pt_dzyj_sj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strSenderCode + "','" + strSenderName + "',getdate(),'" + strTitle + "','" + strContent + "',0,1,0," + iMailType.ToString() + ");";
            string str5 = str4 + " insert into pt_dzyj_yj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strUserCode + "','" + strUserName + "',getdate(),'p','d');";
            str = str5 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_AnnexPath,v_AnnexName,i_AnnexSize,'' from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);";
            if (Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "') and (i_IsDel=0)")) > 0)
            {
                string str6 = str;
                str = str6 + " update pt_dzyj_sj set i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid = " + num.ToString() + "),i_fsrs=(select count(1) from pt_dzyj_yj where i_sjid=" + num.ToString() + "),i_fjdx = (select sum(i_fjSize) from pt_dzyj_fj where i_sjid=" + num.ToString() + ") where i_sjid = " + num.ToString() + ";";
            }
            else
            {
                string str7 = str;
                str = str7 + " update pt_dzyj_sj set i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid = " + num.ToString() + "),i_fsrs=(select count(1) from pt_dzyj_yj where i_sjid=" + num.ToString() + ") where i_sjid = " + num.ToString() + ";";
            }
            return publicDbOpClass.NonQuerySqlString(str + " update pt_dzyj_Tempfj set i_IsSecond=1 where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);");
        }

        public bool SaveToOut(int iSysID, string strSenderCode, string strSenderName, string strTitle, string strContent, string strUserCode, string strUserName, int iMailType, string SJR, string FJR)
        {
            int num = 0;
            string str = "";
            num = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_sj", "i_sjid")) + 1;
            string str3 = str;
            string str4 = str3 + " insert into pt_dzyj_sj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strSenderCode + "','" + strSenderName + "',getdate(),'" + strTitle + "','" + strContent + "',0,1,0," + iMailType.ToString() + ",'" + SJR + "','" + FJR + "');";
            object obj2 = str4 + " insert into pt_dzyj_yj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strUserCode + "','" + strUserName + "',getdate(),'p','d');";
            str = string.Concat(new object[] { obj2, " insert into pt_dzyj_fj select ", num.ToString(), ",v_fjlj,v_lmc,I_fjsize,v_orgpath from pt_dzyj_fj where I_sjid=", num - 1, " ;" });
            if (Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "') and (i_IsDel=0)")) > 0)
            {
                string str5 = str;
                str = str5 + " update pt_dzyj_sj set i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid = " + num.ToString() + "),i_fsrs=(select count(1) from pt_dzyj_yj where i_sjid=" + num.ToString() + "),i_fjdx = (select sum(i_fjSize) from pt_dzyj_fj where i_sjid=" + num.ToString() + ") where i_sjid = " + num.ToString() + ";";
            }
            else
            {
                string str6 = str;
                str = str6 + " update pt_dzyj_sj set i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid = " + num.ToString() + "),i_fsrs=(select count(1) from pt_dzyj_yj where i_sjid=" + num.ToString() + ") where i_sjid = " + num.ToString() + ";";
            }
            return publicDbOpClass.NonQuerySqlString(str + " update pt_dzyj_Tempfj set i_IsSecond=1 where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);");
        }

        public DataTable SelLinkman(string UserCode)
        {
            return publicDbOpClass.DataTableQuary("select v_xm,v_yhdm from v_dzyj_linkman where UserCode = '" + UserCode + "' order by v_xm");
        }

        public bool SendMail(int iSysID, string strSenderCode, string strSenderName, DateTime dtSend, string strTitle, string strContent, int iSms, string[] aryConsigneeCode, string[] aryConsigneeName, int iMailType)
        {
            string str = "";
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
            string str3 = string.Concat(new object[] { 
                " insert into pt_dzyj_sj values(", num.ToString(), ",", iSysID.ToString(), ",'", strSenderCode, "','", strSenderName, "','", dtSend.ToString(), "','", strTitle, "','", strContent, "',0,0,0,", iMailType, 
                ",'','');"
             });
            str = str3 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_AnnexPath,v_AnnexName,i_AnnexSize,'' from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);";
            for (int i = 0; i < (aryConsigneeCode.Length - 1); i++)
            {
                string[] strArray = aryConsigneeCode[i].ToString().Split(new char[] { ':' });
                if (iSysID == int.Parse(strArray[0].ToString()))
                {
                    string str4 = str;
                    str = str4 + " insert into pt_dzyj_yj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strArray[1].ToString() + "','" + aryConsigneeName[0].ToString() + "','" + dtSend.ToString() + "','y','s');";
                }
                if (iSms == 1)
                {
                    SMSLog model = new SMSLog {
                        SendUser = HttpContext.Current.Session["yhdm"].ToString(),
                        SendTime = DateTime.Now,
                        ReceiveUser = strArray[1].ToString(),
                        Message = userManageDb.GetCurrentUserInfo().UserName + "给您发送了一封邮件:" + strTitle,
                        V_LXBM = "008",
                        I_XGID = num.ToString()
                    };
                    PublicInterface.SendSmsMsg(model);
                }
            }
            string str5 = str;
            return publicDbOpClass.NonQuerySqlString((str5 + "update pt_dzyj_sj set i_fjsl=" + num3.ToString() + ",i_fsrs=" + num2.ToString() + ",i_fjdx=" + num4.ToString() + " where i_sjid=" + num.ToString() + ";") + " update pt_dzyj_Tempfj set i_IsSecond=1 where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);");
        }

        public bool SendMail(int iSysID, string strSenderCode, string strSenderName, DateTime dtSend, string strTitle, string strContent, int iSms, string[] aryConsigneeCode, string[] aryConsigneeName, int iMailType, string SJR, string FJR, int Rtx)
        {
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
            string str3 = string.Concat(new object[] { 
                " insert into pt_dzyj_sj values(", num.ToString(), ",", iSysID.ToString(), ",'", strSenderCode, "','", strSenderName, "','", dtSend.ToString(), "','", strTitle, "','", strContent, "',0,0,0,", iMailType, 
                ",'", SJR, "','", FJR, "');"
             });
            sqlString = (str3 + " insert into pt_dzyj_fj select " + num.ToString() + ",v_AnnexPath,v_AnnexName,i_AnnexSize,'' from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0) and (I_ISSECOND=0);") + "Truncate table pt_dzyj_Tempfj;";
            for (int i = 0; i < (aryConsigneeCode.Length - 1); i++)
            {
                string[] strArray = aryConsigneeCode[i].ToString().Split(new char[] { ':' });
                if (iSysID == int.Parse(strArray[0].ToString()))
                {
                    string str4 = sqlString;
                    sqlString = str4 + " insert into pt_dzyj_yj values(" + num.ToString() + "," + iSysID.ToString() + ",'" + strArray[1].ToString() + "','" + aryConsigneeName[i].ToString() + "','" + dtSend.ToString() + "','y','s');";
                }
                if (iSms == 1)
                {
                    SMSLog model = new SMSLog {
                        SendUser = HttpContext.Current.Session["yhdm"].ToString(),
                        SendTime = DateTime.Now,
                        ReceiveUser = strArray[1].ToString(),
                        Message = userManageDb.GetCurrentUserInfo().UserName + "给您发送了一封邮件:" + strTitle,
                        V_LXBM = "008",
                        I_XGID = num.ToString()
                    };
                    PublicInterface.SendSmsMsg(model);
                }
            }
            string str5 = sqlString;
            sqlString = (str5 + "update pt_dzyj_sj set i_fjsl=" + num3.ToString() + ",i_fsrs=" + num2.ToString() + ",i_fjdx=" + num4.ToString() + " where i_sjid=" + num.ToString() + ";") + " update pt_dzyj_Tempfj set i_IsSecond=1 where (v_UserCode='" + strSenderCode + "')and(i_IsDel=0);";
            if (Rtx == 1)
            {
                for (int j = 0; j < (aryConsigneeCode.Length - 1); j++)
                {
                    PublicInterface.SendSysMsg(this.getPTDBSJ(num.ToString(), strTitle + "-" + userManageDb.GetCurrentUserInfo().UserName, aryConsigneeCode[j].ToString()));
                }
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool SetMailState(int iMailID, string strUserCode, string strMailState)
        {
            string sqlString = "";
            if (strMailState == "z")
            {
                object obj2 = sqlString;
                string str2 = string.Concat(new object[] { obj2, " update pt_dzyj_yj set c_xbs = 'z' where ((c_xbs<>'z')and(c_xbs<>'r'))and(i_sjid=", iMailID, ")and(v_jsrdm='", strUserCode, "');" });
                sqlString = str2 + " update pt_dzyj_yj set c_xbs = 'b' where (c_xbs='r')and(i_sjid=" + iMailID.ToString() + ")and(v_jsrdm='" + strUserCode + "');";
            }
            if (strMailState == "r")
            {
                object obj3 = sqlString;
                string str3 = string.Concat(new object[] { obj3, " update pt_dzyj_yj set c_xbs = 'r' where ((c_xbs<>'r')and(c_xbs<>'z'))and(i_sjid=", iMailID, ")and(v_jsrdm='", strUserCode, "');" });
                sqlString = str3 + " update pt_dzyj_yj set c_xbs = 'b' where (c_xbs='z')and(i_sjid=" + iMailID.ToString() + ")and(v_jsrdm='" + strUserCode + "');";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool setPageSize(string UserCode, int PageSize)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update pt_login set MailPageSize = ", PageSize, " where v_yhdm = '", UserCode, "'" }));
        }

        public bool UpdDraft(string strSenderCode, int iDraftID, string strTitle, string strContent, string strConsigneeCode, string strConsigneeName, int iMailType)
        {
            string strAnnexPath = "";
            string strAnnexName = "";
            string sqlString = "";
            sqlString = "select * from pt_dzyj_tempfj where (v_userCode='" + strSenderCode + "')and(i_IsDel=1)";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    strAnnexPath = table.Rows[i]["v_AnnexPath"].ToString();
                    strAnnexName = table.Rows[i]["v_AnnexName"].ToString();
                    if (this.IsDel(iDraftID, strAnnexPath, strAnnexName))
                    {
                        FileUpload upload = new FileUpload();
                        if (!upload.Delete(strAnnexPath + strAnnexName))
                        {
                            return false;
                        }
                        string str4 = sqlString;
                        sqlString = (str4 + " delete from pt_dzyj_fj where (i_sjid=" + iDraftID.ToString() + ")and(v_fjlj='" + strAnnexPath + "')and(v_Lmc='" + strAnnexName + "');") + " delete from pt_dzyj_tempfj where (i_fj_id=" + table.Rows[i]["i_fj_id"].ToString() + ");";
                    }
                    else
                    {
                        string str5 = sqlString;
                        sqlString = (str5 + " delete from pt_dzyj_fj where (i_sjid=" + iDraftID.ToString() + ")and(v_fjlj='" + strAnnexPath + "')and(v_Lmc='" + strAnnexName + "');") + " delete from pt_dzyj_tempfj where (i_fj_id=" + table.Rows[i]["i_fj_id"].ToString() + ");";
                    }
                }
            }
            string str6 = sqlString;
            string str7 = (str6 + " insert into pt_dzyj_fj select " + iDraftID.ToString() + ",v_AnnexPath,v_AnnexName,i_AnnexSize,'' from pt_dzyj_tempfj where (v_UserCode='" + strSenderCode + "')and(i_IsSecond=0);") + " update pt_dzyj_Tempfj set i_IsSecond = 1 where v_UserCode='" + strSenderCode + "';";
            string str8 = str7 + " update pt_dzyj_sj set dtm_sjsj=getdate(),v_zt='" + strTitle + "',txt_zw='" + strContent + "',i_fjsl = (select count(1) from pt_dzyj_fj where i_sjid=" + iDraftID.ToString() + "),i_MailType=" + iMailType.ToString() + ",i_fjdx = isnull((select sum(i_fjsize) from pt_dzyj_fj where i_sjid=" + iDraftID.ToString() + "),0) where i_sjid=" + iDraftID.ToString() + ";";
            return publicDbOpClass.NonQuerySqlString(str8 + " update pt_dzyj_yj set v_jsrdm='" + strConsigneeCode + "',v_jsrxm='" + strConsigneeName + "',dtm_fssj=getdate() where i_sjid=" + iDraftID.ToString() + ";");
        }

        public bool UpLoadAnnex(HttpPostedFile postedFile, string strSenderCode)
        {
            string str = "";
            string fileName = "";
            int num = 0;
            if (this.GetUploadSet())
            {
                FileUpload upload = new FileUpload {
                    ExtName = this._extName,
                    FileSize = this._fileSize,
                    UploadPath = this._uploadPath
                };
                num = this._fileNumber;
                if (this.GetFileNumber(strSenderCode) < num)
                {
                    fileName = ((DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0')) + "_I_" + DateTime.Now.ToLongTimeString().Replace(":", "")) + "-" + postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1);
                    str = this._uploadPath + fileName.Substring(0, 6) + "/";
                    if (upload.Upload(postedFile, fileName, true))
                    {
                        int num3 = int.Parse(publicDbOpClass.QuaryMaxid("pt_dzyj_tempfj", "i_fj_id")) + 1;
                        if (!publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "insert into pt_dzyj_tempfj values(", num3.ToString(), ",'", strSenderCode, "','", str, "','", fileName, "',", postedFile.ContentLength, ",0,0)" })))
                        {
                            this._messageString = "向数据库插入记录出错！";
                            return false;
                        }
                        return true;
                    }
                    this._messageString = upload.MessageString;
                    return false;
                }
                this._messageString = "上传文件的数量超出限制";
            }
            return false;
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

