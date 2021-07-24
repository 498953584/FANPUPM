namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class NewsAction
    {
        public bool addNewsType(string ParentCode, string TypeName)
        {
            string newClassCode = this.GetNewClassCode(ParentCode);
            return publicDbOpClass.NonQuerySqlString("insert into Web_NewsCategories (c_xwlxdm,c_xwlxmc,c_parentid) values ('" + newClassCode + "','" + TypeName + "','" + ParentCode + "')");
        }

        public static bool asdf()
        {
            string sqlString = "SELECT V_YHDM FROM PT_LOGIN where v_yhdm not in (select distinct(v_yhdm) from PT_YHMC_Privilege) ";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                publicDbOpClass.DataTableQuary("insert into PT_YHMC_Privilege (V_YHDM, C_MKDM, IsBasic, IsHaveOp) select '" + table.Rows[i]["v_yhdm"].ToString() + "', C_MKDM, IsBasic, IsHaveOp FROM   PT_YHMC_Privilege WHERE  (V_YHDM = '00000003')");
            }
            return true;
        }

        public bool delNewsType(string TypeCode)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Web_NewsCategories where c_xwlxdm = '" + TypeCode + "'");
        }

        public int EquipDel(int LinkId, string LinkType)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "delete Web_FriendLink where LinkID = ", LinkId, " and LinkType='", LinkType, "'" }));
        }

        public DataTable EquipSel(string LinkType)
        {
            return publicDbOpClass.DataTableQuary("select * from Web_FriendLink where LinkType='" + LinkType + "'");
        }

        public DataTable getAllNewsType()
        {
            string sqlString = "select c_xwlxdm,c_xwlxmc,c_parentid from Web_NewsCategories";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetNewCate()
        {
            string sqlString = "select * from Web_NewsCategories ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        private string GetNewClassCode(string ParentCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("select max(c_xwlxdm) from Web_NewsCategories where c_parentid = '" + ParentCode + "'");
            if (obj2 != DBNull.Value)
            {
                return Convert.ToString((int) (int.Parse((string) obj2) + 1)).PadLeft(ParentCode.Length + 2, '0');
            }
            return (ParentCode + "01");
        }

        public DataTable GetNewsInfo(int xwid)
        {
            return publicDbOpClass.DataTableQuary("select * from Web_News where i_xw_id = " + xwid + " and c_sfyx = 'y'");
        }

        public DataTable GetnewsInfoLxwm(string code)
        {
            return publicDbOpClass.DataTableQuary("select top 1 * from Web_News where c_xwlxdm = '" + code + "' and c_sfyx = 'y' order by dtm_fbsj desc");
        }

        public string getNewsTypeName(string TypeCode)
        {
            object obj2 = publicDbOpClass.ExecuteScalar("select c_xwlxmc from Web_NewsCategories where c_xwlxdm = '" + TypeCode + "'");
            if (obj2 == null)
            {
                return "";
            }
            return obj2.ToString();
        }

        public DataTable GetPageData(string strWhere)
        {
            return publicDbOpClass.DataTableQuary(" select * from Web_News " + strWhere + " ");
        }

        public DataTable getPicNews(string TypeCode)
        {
            return publicDbOpClass.DataTableQuary("select * from Web_FriendLink where LinkType = '" + TypeCode + "' order by LinkID desc");
        }

        public int NewsAdd(News nw)
        {
            string str = "";
            if ((nw.c_xwlxdm != "001") && (nw.c_ttbj == "y"))
            {
                str = "update Web_News set c_ttbj = 'n' where c_xwlxdm = '" + nw.c_xwlxdm + "'";
            }
            object obj2 = str + " insert into Web_News(c_xwlxdm,txt_xwnr,v_xwbt,c_ttbj,dtm_fbsj,c_sfyx,imageURL) values ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "('", nw.c_xwlxdm, "','", nw.txt_xwnr, "','", nw.v_xwbt, "','", nw.c_ttbj, "','", nw.dtm_fbsj, "','", nw.c_sfyx, "','", nw.imageURL, "')" }));
        }

        public int NewsAdd(News nw, string xwlxdm)
        {
            string str2 = ("begin" + " if exists(select * from Web_News where c_xwlxdm = '" + xwlxdm + "')") + " begin " + " update Web_News";
            string str3 = str2 + " set txt_xwnr='" + nw.txt_xwnr + "',v_xwbt='" + nw.v_xwbt + "',";
            object obj2 = (((str3 + " c_ttbj='" + nw.c_ttbj + "',imageURL='" + nw.imageURL + "'") + " where c_xwlxdm = " + nw.c_xwlxdm) + " end" + " else") + " begin" + " insert into Web_News(c_xwlxdm,txt_xwnr,v_xwbt,c_ttbj,dtm_fbsj,c_sfyx,imageURL) values ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " ('", nw.c_xwlxdm, "','", nw.txt_xwnr, "','", nw.v_xwbt, "','", nw.c_ttbj, "','", nw.dtm_fbsj, "','", nw.c_sfyx, "','", nw.imageURL, "')" }) + " end" + " end");
        }

        public int NewsDel(int xwid)
        {
            new News();
            return publicDbOpClass.ExecSqlString("update Web_News set c_sfyx = 'n' where i_xw_id = " + xwid + " ");
        }

        public DataTable NewsSel(string dm)
        {
            return publicDbOpClass.DataTableQuary("select * from Web_News where c_xwlxdm = '" + dm + "' and c_sfyx = 'y' order by dtm_fbsj desc");
        }

        public DataTable NewsSelTT(string dm)
        {
            return publicDbOpClass.DataTableQuary("select * from Web_News where c_xwlxdm = '" + dm + "' and c_sfyx = 'y' and  c_ttbj='y' order by dtm_fbsj desc");
        }

        public int NewsUpdate(News nw)
        {
            string str = "";
            if (nw.c_ttbj == "y")
            {
                str = "update Web_News set c_ttbj = 'n' where c_xwlxdm = '" + nw.c_xwlxdm + "' ";
            }
            string str2 = str + " update Web_News";
            string str3 = str2 + " set txt_xwnr='" + nw.txt_xwnr + "',v_xwbt='" + nw.v_xwbt + "',";
            return publicDbOpClass.ExecSqlString((str3 + " c_ttbj='" + nw.c_ttbj + "',imageURL='" + nw.imageURL + "'") + " where i_xw_id = " + nw.i_xw_id);
        }

        public DataTable StaffInfo(string LinkType)
        {
            return publicDbOpClass.DataTableQuary("select * from Web_FriendLink where LinkType = '" + LinkType + "'");
        }

        public DataTable StaffInfo(string LinkType, string strWhere)
        {
            return publicDbOpClass.DataTableQuary(("select * from Web_FriendLink where LinkType = '" + LinkType + "'") + strWhere);
        }

        public int StaffInfoAdd(string Linktype, string imageurl)
        {
            string str = "begin";
            return publicDbOpClass.ExecSqlString((((((str + " if exists(select * from Web_FriendLink where LinkType = '4')" + " begin ") + " update Web_FriendLink set LinkLogo = '" + imageurl + "' where LinkType = '4'") + " end ") + " else begin" + " insert into Web_FriendLink (LinkName,LinkUrl,LinkLogo,LinkType)") + " values ('员工风采','','" + imageurl + "','4')") + " end" + " end");
        }

        public int StaffInfoAdd(string bt, string imageurl, string LinkType)
        {
            string str = "";
            string str2 = str + " insert into Web_FriendLink (LinkName,LinkUrl,LinkLogo,LinkType)";
            return publicDbOpClass.ExecSqlString(str2 + " values ('" + bt + "','','" + imageurl + "','" + LinkType + "')");
        }

        public bool updNewsType(string TypeCode, string TypeName)
        {
            return publicDbOpClass.NonQuerySqlString("update Web_NewsCategories set c_xwlxmc = '" + TypeName + "' where c_xwlxdm = '" + TypeCode + "'");
        }
    }
}

