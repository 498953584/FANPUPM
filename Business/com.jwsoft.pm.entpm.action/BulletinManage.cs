namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Data;

    public class BulletinManage
    {
        private string _messageString = "";

        public int AddBulletin(int iSysID, int iReleaseDept, string strReleaseDept, string strReleaseCode, string strReleaseUser, string strTitle, string strContent, DateTime dtReleaseTime, DateTime dtExpriesDate, int iReleaseBound, string strToSystem)
        {
            string str = "";
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pt_Bulletin_Main", "i_BulletinID")) + 1;
            string str2 = str;
            string str3 = str2 + " insert into pt_Bulletin_Main values(" + num.ToString() + "," + iSysID.ToString() + "," + num.ToString() + "," + iReleaseDept.ToString() + ",'" + strReleaseDept + "','" + strReleaseCode + "','" + strReleaseUser + "','" + strTitle + "','" + strContent + "','" + dtReleaseTime.ToString() + "','" + dtExpriesDate.ToString() + "'," + iReleaseBound.ToString() + ",0,0);";
            str = str3 + " insert into pt_Bulletin_Annex select " + num.ToString() + "," + iSysID.ToString() + ",0,v_AnnexPath,v_AnnexName,i_AnnexSize,0 from pt_TempAnnex where (v_UserCode = '" + strReleaseCode + "')and(i_IsDel=0);";
            string[] strArray = strToSystem.Split(new char[] { ',' });
            for (int i = 0; i < (strArray.Length - 1); i++)
            {
                string str4 = str;
                str = str4 + " insert into pt_Bulletin_ToSys values(" + num.ToString() + "," + strArray[i].ToString() + ",1,0,1);";
            }
            string str5 = str;
            if (publicDbOpClass.NonQuerySqlString((str5 + " update pt_Bulletin_ToSys set i_IsUpdate=0,i_IsSucess=1,i_IsDel=0 where (i_BulletinID = " + num.ToString() + ")and(i_ToSysID=" + iSysID.ToString() + ");") + "update pt_TempAnnex set i_IsSecond = 1 where v_UserCode='" + strReleaseCode + "';"))
            {
                return num;
            }
            return 0;
        }

        public static bool AddBulletinInfo(Hashtable bulletinInfo)
        {
            return publicDbOpClass.Insert("[PT_BULLETIN_MAIN]", bulletinInfo);
        }

        public bool DelBulletin(int iBulletinID)
        {
            FileUpload upload = new FileUpload();
            string str2 = "";
            string str3 = "";
            foreach (DataRow row in publicDbOpClass.DataTableQuary("select * from pt_Bulletin_Annex where i_BulletinID=" + iBulletinID.ToString()).Rows)
            {
                str2 = row["v_AnnexPath"].ToString();
                str3 = row["v_AnnexName"].ToString();
                if (!upload.Delete(str2 + str3))
                {
                    return false;
                }
            }
            return publicDbOpClass.NonQuerySqlString(((" delete from pt_Bulletin_Annex where i_BulletinID = " + iBulletinID.ToString() + ";") + " delete from pt_Bulletin_ToSys where i_BulletinID = " + iBulletinID.ToString() + ";") + " delete from pt_Bulletin_Main where i_BulletinID = " + iBulletinID.ToString() + ";");
        }

        public static bool DelBulletinInfo(Guid bulletinId)
        {
            return publicDbOpClass.NonQuerySqlString(("Delete from XPM_Basic_AnnexList where RecordCode = '" + bulletinId.ToString() + "' ") + " Delete from PT_BULLETIN_MAIN where I_BULLETINID='" + bulletinId.ToString() + "'");
        }

        public DataTable GetExteriorBulletin()
        {
            return publicDbOpClass.DataTableQuary("select i_BulletinID,i_ReleaseBound,v_ReleaseDept,v_ReleaseUser,v_Title,v_Content,dtm_ReleaseTime from pt_Bulletin_Main where ('" + DateTime.Today.ToShortDateString() + "'<=dtm_ExpriesDate)and(i_IsSucess=1) and I_RELEASEBOUND = 1 order by dtm_ReleaseTime desc");
        }

        public DataTable GetInValidBulletin(int iDeptCode, int iSysID)
        {
            string sqlString = "";
            if (iDeptCode == 0)
            {
                sqlString = string.Concat(new object[] { "select i_BulletinID,i_SysID,i_ReleaseBound,v_ReleaseDept,v_ReleaseUser,v_Title,dtm_ReleaseTime from pt_Bulletin_Main where (dtm_ExpriesDate<'", DateTime.Today.ToShortDateString(), "')and((i_SysID=", iSysID.ToString(), ")or((i_SysID<>", iSysID, ")and(i_IsSucess=1))) order by dtm_ReleaseTime desc" });
            }
            else
            {
                sqlString = string.Concat(new object[] { "select i_BulletinID,i_SysID,i_ReleaseBound,v_ReleaseDept,v_ReleaseUser,v_Title,dtm_ReleaseTime from pt_Bulletin_Main where (dtm_ExpriesDate<'", DateTime.Today.ToShortDateString(), "')and((i_SysID=", iSysID.ToString(), ")or((i_SysID<>", iSysID, ")and(i_IsSucess=1)))and(((i_ReleaseDept=", iDeptCode, ")and(i_ReleaseBound=0))or(i_ReleaseBound=1)or(i_ReleaseBound=2)or(i_ReleaseBound=3)) order by dtm_ReleaseTime desc" });
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetOneBulletin(int iBulletinID)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_Bulletin_Main where (i_BulletinID=" + iBulletinID.ToString() + ")");
        }

        public static DataTable GetTodayBulletin()
        {
            string sqlString = "select * from PT_BULLETIN_MAIN where DTM_EXPRIESDATE  > getdate() and AuditState = 1 and I_RELEASEBOUND = 2";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetTodayBulletin(int iReleaseDept)
        {
            string str;
            object obj2 = publicDbOpClass.ExecuteScalar("select v_bmbm from pt_d_bm where i_bmdm = " + iReleaseDept + " and c_sfyx = 'y'");
            if (obj2.ToString() == "")
            {
                str = " select top 6 *   from v_bulletin_list where AuditState = 1 and I_RELEASEBOUND = 1 ";
                str = (str + " and datediff(dd,DTM_EXPRIESDATE,getdate())<= 0 ") + " and  DeptRange = ''" + " order by  DTM_RELEASETIME desc";
            }
            else
            {
                str = " select top 6  * from v_bulletin_list where AuditState = 1 and I_RELEASEBOUND = 1 ";
                str = ((str + " and datediff(dd,DTM_EXPRIESDATE,getdate())<= 0 ") + " and charindex('," + obj2.ToString().Substring(0, 2) + "',','+DeptRange+',')>0 or DeptRange = ''") + " order by  DTM_RELEASETIME desc";
            }
            return publicDbOpClass.DataTableQuary(str);
        }

        public DataTable GetValidBulletin(int iDeptCode, int iSysID)
        {
            string sqlString = "";
            if (iDeptCode == 0)
            {
                sqlString = string.Concat(new object[] { "select i_BulletinID,i_SysID,i_ReleaseBound,v_ReleaseDept,v_ReleaseUser,v_Title,v_Content,dtm_ReleaseTime from pt_Bulletin_Main where ('", DateTime.Today.ToShortDateString(), "'<=dtm_ExpriesDate)and((i_SysID=", iSysID.ToString(), ")or((i_SysID<>", iSysID, ")and(i_IsSucess=1))) order by dtm_ReleaseTime desc" });
            }
            else
            {
                sqlString = string.Concat(new object[] { "select i_BulletinID,i_SysID,i_ReleaseBound,v_ReleaseDept,v_ReleaseUser,v_Title,v_Content,dtm_ReleaseTime from pt_Bulletin_Main where ('", DateTime.Today.ToShortDateString(), "'<=dtm_ExpriesDate)and((i_SysID=", iSysID.ToString(), ")or((i_SysID<>", iSysID, ")and(i_IsSucess=1)))and(((i_ReleaseDept=", iDeptCode, ")and(i_ReleaseBound=0))or(i_ReleaseBound=1)or(i_ReleaseBound=2)or(i_ReleaseBound=3)) order by dtm_ReleaseTime desc" });
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryOneBulletin(Guid bulletinId)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_Bulletin_Main where I_BULLETINID = '" + bulletinId.ToString() + "'");
        }

        public bool UpdateBulletin(int iBulletinID, int iSysID, string strReleaseCode, string strReleaseUser, string strTitle, string strContent, DateTime dtExpiresDate)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_TempAnnex where v_UserCode = '" + strReleaseCode + "'");
            FileUpload upload = new FileUpload();
            str = (" update pt_Bulletin_Main set v_ReleaseUser='" + strReleaseUser + "',v_Title='" + strTitle + "',v_Content='" + strContent + "',dtm_ExpriesDate='" + dtExpiresDate.ToString() + "',i_IsSucess = 0 where i_BulletinID=" + iBulletinID.ToString() + ";") + " update pt_Bulletin_ToSys set i_IsUpdate=1,i_IsSucess=0 where i_BulletinID = " + iBulletinID.ToString() + ";";
            foreach (DataRow row in table.Rows)
            {
                if ((row["i_IsSecond"].ToString() == "1") && (row["i_IsDel"].ToString() == "1"))
                {
                    upload.Delete(row["v_AnnexPath"].ToString() + row["v_AnnexName"].ToString());
                    str = str + " delete from pt_TempAnnex where i_AnnexID = " + row["i_AnnexID"].ToString() + ";";
                    string str2 = str;
                    str = str2 + " delete from pt_Bulletin_Annex where (i_BulletinID=" + iBulletinID.ToString() + ")and(v_AnnexPath='" + row["v_AnnexPath"].ToString() + "')and(v_AnnexName='" + row["v_AnnexName"].ToString() + "');";
                }
                else if (row["i_IsSecond"].ToString() == "0")
                {
                    string str3 = str;
                    str = str3 + " insert into pt_Bulletin_Annex values(" + iBulletinID.ToString() + "," + iSysID.ToString() + ",0,'" + row["v_AnnexPath"].ToString() + "','" + row["v_AnnexName"].ToString() + "'," + row["i_AnnexSize"].ToString() + ",0);";
                    str = str + " update pt_TempAnnex set i_IsSecond=1,i_IsDel=0 where i_AnnexID=" + row["i_AnnexID"].ToString() + ";";
                }
            }
            string str4 = str;
            return publicDbOpClass.NonQuerySqlString(str4 + " update pt_Bulletin_ToSys set i_IsUpdate = 0,i_IsSucess = 1 where (i_BulletinID = " + iBulletinID.ToString() + ")and(i_ToSysID=" + iSysID.ToString() + ");");
        }

        public static bool UpdBulletinInfo(Hashtable bulletinInfo, string where)
        {
            return publicDbOpClass.Update("[PT_BULLETIN_MAIN]", bulletinInfo, where);
        }

        public string MessageString
        {
            get
            {
                return this._messageString;
            }
            set
            {
                this._messageString = value;
            }
        }
    }
}

