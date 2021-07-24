namespace com.jwsoft.pm.entpm.action
{
    using ChineseToSpell;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class AddressListDb
    {
        public bool cAddLinkman(string yhdm, string xm, int bmdm, string zw, string jtzz, string yzbm, string bgdh, string zzdh, string zdbs, string sj, string sjbs, string cz, string xb, string dzyx, string wlch, string bz, string xh)
        {
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_txl_nbtxl", "i_id")) + 1;
            
            string str = new ChineseToSpell().WordToSpell(xm);
            string str2 = "";
            object obj2 = str2 + "insert into pt_txl_nbtxl (i_id,v_xm,i_bmdm,v_zw,v_jtzz,v_yzbm,v_bgdh,v_zzdh,c_zdbs,v_sj,c_sjbs,v_cz,c_xb,v_dzyx,v_wlch,v_bz,v_yhdm,c_bs,v_hypy,i_xh) values ";
            object obj3 = string.Concat(new object[] { 
                obj2, " (", num, ",'", xm, "',", bmdm, ",'", zw, "','", jtzz, "','", yzbm, "','", bgdh, "','", 
                zzdh, "','", zdbs, "','", sj, "','", sjbs, "','", cz, "','", xb, "',"
             });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj3, "'", dzyx, "','", wlch, "','", bz, "','", yhdm, "','n','", str, "',", Convert.ToInt32(xh), ")" }));
        }

        public bool cDelLinkman(string id)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_txl_nbtxl where i_id = " + Convert.ToInt32(id));
        }

        public DataTable cGetDeptLinkman(int bmdm)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("select v_bmbm from pt_d_bm where i_bmdm = " + bmdm);
            return publicDbOpClass.DataTableQuary("select a.* from pt_txl_nbtxl a,pt_d_bm b where a.i_bmdm = b.i_bmdm and b.v_bmbm like '" + obj2.ToString() + "%' order by a.i_xh");
        }

        public int cGetDeptMaxSerial(int bmdm)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select i_xh from pt_txl_nbtxl where i_bmdm = " + bmdm + " order by i_xh desc");
            if (table.Rows.Count == 0)
            {
                return 0;
            }
            return Convert.ToInt32(table.Rows[0]["i_xh"].ToString());
        }

        public DataTable cGetLinkman(string id)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_txl_nbtxl where i_id = " + Convert.ToInt32(id));
        }

        public DataRow cGetLinkmanDetail(string yhdm)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_txl_nbtxl where v_yhdm = '" + yhdm + "' and c_bs = 'y'");
            if (table.Rows.Count == 0)
            {
                return null;
            }
            return table.Rows[0];
        }

        public string cGetLinkmanHandset(string yhdm)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_txl_nbtxl where v_yhdm = '" + yhdm + "' and c_bs = 'y'");
            if (table.Rows.Count == 0)
            {
                return null;
            }
            return table.Rows[0]["v_sj"].ToString();
        }

        public DataTable cSearchLinkman(string key, string field)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_txl_nbtxl where " + field + " like '%" + key + "%'");
        }

        public bool cUpdateLinkman(int id, string xm, string bmFlag, int bmdm, string zw, string jtzz, string yzbm, string bgdh, string zzdh, string zdbs, string sj, string sjbs, string cz, string xb, string dzyx, string wlch, string bz, string xhFlag, string xh)
        {
            string str = "";
            if (xh == "")
            {
                xh = "1";
            }
            if ((bmFlag == "y") || (xhFlag == "y"))
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, " update pt_txl_nbtxl set i_xh = i_xh+1 where i_bmdm = ", Convert.ToInt32(bmdm), " and i_xh >= ", Convert.ToInt32(xh), ";" });
            }
            object obj3 = str;
            object obj4 = string.Concat(new object[] { 
                obj3, "update pt_txl_nbtxl set v_xm='", xm, "',i_bmdm=", bmdm, ",v_zw='", zw, "',v_jtzz='", jtzz, "',v_yzbm='", yzbm, "',v_bgdh='", bgdh, "',v_zzdh='", zzdh, "',c_zdbs='", 
                zdbs, "',v_sj='", sj, "',c_sjbs='", sjbs, "',"
             });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj4, "v_cz='", cz, "',c_xb='", xb, "',v_dzyx='", dzyx, "',v_wlch='", wlch, "',v_bz='", bz, "',i_xh = ", Convert.ToInt32(xh), " where i_id = ", id }));
        }

        public bool cUpdateUserData(string yhdm, string zw, string jtzz, string yzbm, string bgdh, string zzdh, string zdbs, string sj, string sjbs, string cz, string xb, string dzyx, string wlch, string bz)
        {
            string sqlString = "";
            if (this.cGetLinkmanDetail(yhdm) != null)
            {
                string str4 = "update pt_txl_nbtxl set v_zw = '" + zw + "',v_jtzz = '" + jtzz + "',v_yzbm = '" + yzbm + "',v_bgdh = '" + bgdh + "',v_zzdh = '" + zzdh + "',c_zdbs = '" + zdbs + "',v_sj = '" + sj + "',";
                sqlString = str4 + "c_sjbs = '" + sjbs + "',v_cz = '" + cz + "',c_xb = '" + xb + "',v_dzyx = '" + dzyx + "',v_wlch = '" + wlch + "',v_bz = '" + bz + "' where v_yhdm = '" + yhdm + "' and c_bs = 'y'";
            }
            else
            {
                int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_txl_nbtxl", "i_id")) + 1;
                DataTable table = new userManageDb().userQuaryDt(yhdm);
                string word = table.Rows[0]["v_xm"].ToString();
                int num2 = Convert.ToInt32(table.Rows[0]["i_bmdm"].ToString());
                string str3 = new ChineseToSpell().WordToSpell(word);
                object obj2 = sqlString + "insert into pt_txl_nbtxl (i_id,v_xm,i_bmdm,v_zw,v_jtzz,v_yzbm,v_bgdh,v_zzdh,c_zdbs,v_sj,c_sjbs,v_cz,c_xb,v_dzyx,v_wlch,v_bz,v_yhdm,c_bs,v_hypy) values ";
                string str5 = string.Concat(new object[] { 
                    obj2, " (", num, ",'", word, "',", num2, ",'", zw, "','", jtzz, "','", yzbm, "','", bgdh, "','", 
                    zzdh, "','", zdbs, "','", sj, "','", sjbs, "','", cz, "','", xb, "',"
                 });
                sqlString = str5 + "'" + dzyx + "','" + wlch + "','" + bz + "','" + yhdm + "','y','" + str3 + "')";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
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

        public bool eAddCorpInfo(string gsmc, string dz, string yb)
        {
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_txl_wbgs", "i_id")) + 1;
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "insert into pt_txl_wbgs (i_id,v_mc,v_yb,v_dz) values (", num, ",'", gsmc, "','", yb, "','", dz, "')" }));
        }

        public bool eAddLinkman(string gsid, string xm, string zw, string jtzz, string yzbm, string bgdh, string zzdh, string sj, string cz, string xb, string dzyx, string wlch, string bz)
        {
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_txl_wbtxl", "i_id")) + 1;
            string str = "";
            object obj2 = str + "insert into pt_txl_wbtxl (i_id,i_gs_id,v_xm,v_zw,v_jtzz,v_yzbm,v_bgdh,v_zzdh,v_sj,v_cz,c_xb,v_dzyx,v_wlch,v_bz) values ";
            string str2 = string.Concat(new object[] { 
                obj2, " (", num, ",", Convert.ToInt32(gsid), ",'", xm, "','", zw, "','", jtzz, "','", yzbm, "','", bgdh, "','", 
                zzdh, "','", sj, "','", cz, "','", xb, "',"
             });
            return publicDbOpClass.NonQuerySqlString(str2 + "'" + dzyx + "','" + wlch + "','" + bz + "')");
        }

        public bool eDelCorpInfo(string id)
        {
            string str = "";
            return publicDbOpClass.NonQuerySqlString((((str + "begin ") + " delete from pt_txl_wbgs where i_id = " + id) + " delete from pt_txl_wbtxl where i_gs_id = " + id) + " end;");
        }

        public bool eDelLinkman(string id)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_txl_wbtxl where i_id = " + Convert.ToInt32(id));
        }

        public DataTable eGetCorp()
        {
            string sqlString = "select * from pt_txl_wbgs order by i_id";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable eGetCorpInfo(string id)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_txl_wbgs where i_id = " + Convert.ToInt32(id));
        }

        public DataTable eGetCorpLinkman(string id)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_txl_wbtxl where i_gs_id = " + Convert.ToInt32(id));
        }

        public DataTable eGetLinkman(string id)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_txl_wbtxl where i_id = " + Convert.ToInt32(id));
        }

        public DataTable eLastLinkmanQuary(int n)
        {
            return publicDbOpClass.DataTableQuary("select top " + n + " * from pt_txl_wbtxl order by i_id desc");
        }

        public bool eUpdateCorpInfo(string id, string gsmc, string dz, string yb)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update pt_txl_wbgs set v_mc = '", gsmc, "',v_yb='", yb, "',v_dz = '", dz, "' where i_id = ", Convert.ToInt32(id) }));
        }

        public bool eUpdateLinkman(int id, string xm, string zw, string jtzz, string yzbm, string bgdh, string zzdh, string sj, string cz, string xb, string dzyx, string wlch, string bz)
        {
            string str2 = "";
            object obj2 = str2 + "update pt_txl_wbtxl set v_xm='" + xm + "',v_zw='" + zw + "',v_jtzz='" + jtzz + "',v_yzbm='" + yzbm + "',v_bgdh='" + bgdh + "',v_zzdh='" + zzdh + "',v_sj='" + sj + "',";
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj2, "v_cz='", cz, "',c_xb='", xb, "',v_dzyx='", dzyx, "',v_wlch='", wlch, "',v_bz='", bz, "' where i_id = ", id }));
        }

        public DataSet getSubDepartment(int iDeptCode)
        {
            return publicDbOpClass.DataSetQuary("select * from pt_d_bm where (i_sjdm=" + iDeptCode.ToString() + ")and(c_sfyx='y') order by i_xh");
        }

        public int getUserParentDept(string yhdm)
        {
            return Convert.ToInt32(publicDbOpClass.DataTableQuary("select i_sjdm from pt_d_bm where i_bmdm = (select i_bmdm from pt_yhmc where v_yhdm = '" + yhdm + "')").Rows[0]["i_sjdm"].ToString());
        }

        public string manageDept(string yhdm)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("select v_bmdm from pt_manager where v_yhdm = '" + yhdm + "'");
            if (table.Rows.Count != 0)
            {
                str = table.Rows[0]["v_bmdm"].ToString();
            }
            return str;
        }

        public bool pAddGroup(string yhdm, string fzmc, string bz)
        {
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_txl_grfzlx", "i_id")) + 1;
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "insert into pt_txl_grfzlx (i_id,v_fzmc,v_yhdm,v_bz) values (", num, ",'", fzmc, "','", yhdm, "','", bz, "')" }));
        }

        public bool pAddLinkman(string yhdm, string xm, string sr, string jtzz, string yzbm, string dw, string bgdh, string zzdh, string sj, string xb, string dzyx, string wlch, string bz, string fzid)
        {
            int num = Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_txl_grtxl", "i_grtxl_id")) + 1;
            string str = "";
            object obj2 = str + "insert into pt_txl_grtxl (i_grtxl_id,v_yhdm,v_xm,dtm_sr,v_jtzz,c_yzbm,v_dw,v_bgdh,v_zzdh,v_sj,c_xb,v_dzyx,v_wlch,v_bz,i_fz_id) values ";
            object obj3 = string.Concat(new object[] { 
                obj2, " (", num, ",'", yhdm, "','", xm, "','", sr, "','", jtzz, "','", yzbm, "','", dw, "','", 
                bgdh, "','", zzdh, "','", sj, "','", xb, "',"
             });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj3, "'", dzyx, "','", wlch, "','", bz, "',", Convert.ToInt32(fzid), ")" }));
        }

        public bool pDelGroup(int id)
        {
            string str = "";
            return publicDbOpClass.NonQuerySqlString((((str + "begin ") + " delete from pt_txl_grfzlx where i_id = " + id) + " delete from pt_txl_grtxl where i_fz_id = " + id) + " end;");
        }

        public bool pDelLinkman(string id)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pt_txl_grtxl where i_grtxl_id = " + Convert.ToInt32(id));
        }

        public DataTable pGetGroup(string yhdm)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_txl_grfzlx where v_yhdm = '" + yhdm + "'");
        }

        public DataTable pGetGroupMember(int iGroup)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_txl_grtxl where i_fz_id = " + iGroup);
        }

        public DataTable pGetLinkman(string id)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_txl_grtxl where i_grtxl_id = " + Convert.ToInt32(id));
        }

        public DataTable pLastLinkmanQuary(int n, string yhdm)
        {
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select top ", n, " * from pt_txl_grtxl where v_yhdm = '", yhdm, "' order by i_grtxl_id desc" }));
        }

        public bool pUpdateGroup(int id, string fzmc, string bz)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update pt_txl_grfzlx set v_fzmc = '", fzmc, "',v_bz = '", bz, "' where i_id = ", id }));
        }

        public bool pUpdateLinkman(string id, string xm, string sr, string jtzz, string yzbm, string dw, string bgdh, string zzdh, string sj, string xb, string dzyx, string wlch, string bz, string fzid)
        {
            string str2 = "";
            object obj2 = str2 + "update pt_txl_grtxl set v_xm='" + xm + "',dtm_sr='" + sr + "',v_jtzz ='" + jtzz + "',c_yzbm='" + yzbm + "',v_dw='" + dw + "',v_bgdh='" + bgdh + "',";
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { 
                obj2, "v_zzdh = '", zzdh, "',v_sj = '", sj, "',c_xb = '", xb, "',v_dzyx='", dzyx, "',v_wlch = '", wlch, "',v_bz='", bz, "',i_fz_id=", Convert.ToInt32(fzid), " where i_grtxl_id = ", 
                Convert.ToInt32(id)
             }));
        }
    }
}

