namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.sysManage.publicStringOperation;
    using System;
    using System.Data;
    using System.Linq;
    using System.Text;

    public class SystemModule
    {
        public static bool AddModule(string mkdm, string mkmc, string cdlj, int xh, string img, string strBasic, string strMaintainable, string helpPath)
        {
            mkmc = PublicClass.CheckString(mkmc);
            cdlj = PublicClass.CheckString(cdlj);
            string str = (Convert.ToInt32(publicDbOpClass.QuaryMaxid("pt_mk", "I_ID")) + 1).ToString();
            string str2 = "";
            str2 = " begin ";
            object obj2 = str2;
            string str3 = string.Concat(new object[] { 
                obj2, " insert into pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) values ('", mkdm, "','", mkmc, "','", cdlj, "','y',", xh, ",", str, ",'", img, "',0,'", strBasic, "','", 
                strMaintainable, "','", helpPath, "')"
             });
            return publicDbOpClass.NonQuerySqlString((str3 + " update pt_mk set i_childNum = (select count(1) from pt_mk where (c_mkdm like '" + mkdm.Substring(0, mkdm.Length - 2) + "%') and (len(c_mkdm)= " + mkdm.Length.ToString() + ")) where c_mkdm = '" + mkdm.Substring(0, mkdm.Length - 2) + "'") + " end");
        }

        public static bool DelModule(string mkdm)
        {
            string str2 = ((("begin " + " delete from PT_Role_Privilege where ModuleCode = '" + mkdm + "'") + " delete from PT_YHMC_Privilege where c_mkdm = '" + mkdm + "'") + " delete from PT_Manager_Privilege where C_MKDM = '" + mkdm + "'") + " delete from pt_mk where c_mkdm='" + mkdm + "'";
            return publicDbOpClass.NonQuerySqlString((str2 + " update pt_mk set i_childNum = (select count(1) from pt_mk where (c_mkdm like '" + mkdm.Substring(0, mkdm.Length - 2) + "%') and (len(c_mkdm)= " + mkdm.Length.ToString() + ")) where c_mkdm = '" + mkdm.Substring(0, mkdm.Length - 2) + "'") + " end");
        }

        public static bool DelRights(string strTable, string strPrimaryKey, string strMainKey, string mkdm)
        {
            bool flag = true;
            string sqlString = "";
            foreach (DataRow row in publicDbOpClass.DataTableQuary("select " + strPrimaryKey + "," + strMainKey + " from " + strTable + " where CHARINDEX('," + mkdm + ",'," + strMainKey + ") > 0").Rows)
            {
                string str2 = row[strMainKey].ToString();
                if (str2.IndexOf("," + mkdm + ",") == 0)
                {
                    str2 = str2.Replace("," + mkdm + ",", ",");
                }
                sqlString = "update " + strTable + " set " + strMainKey + " = '" + str2 + "' where " + strPrimaryKey + " = '" + row[strPrimaryKey].ToString() + "'";
                flag &= publicDbOpClass.NonQuerySqlString(sqlString);
            }
            return flag;
        }

        public static DataTable GetAllSubModule(string parentModuleCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_mk where c_mkdm like '" + parentModuleCode + "%'");
        }

        public static DataTable GetFirstModule()
        {
            string sqlString = "";
            sqlString = "select * from pt_mk where len(c_mkdm)=2";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable GetModuleList(string mkdm)
        {
            DataTable table = new DataTable();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("\r\n\t\t\t\tWITH ctePriv AS\r\n\t\t\t\t(\r\n\t\t\t\t\tSELECT PT_MK.C_MKDM,\r\n\t\t\t\t\t\tSTUFF((\r\n\t\t\t\t\t\t\tSELECT ',' + Y.v_xm FROM PT_YHMC_Privilege AS P1\r\n\t\t\t\t\t\t\tLEFT JOIN PT_yhmc Y ON P1.V_YHDM = Y.V_YHDM\r\n\t\t\t\t\t\t\tWHERE P1.C_MKDM  = PT_MK.C_MKDM\r\n\t\t\t\t\t\t\tFOR XML PATH('')\r\n\t\t\t\t\t\t\t\t) , 1 , 1 , '') AS UserName,\r\n\t\t\t\t\t\tSTUFF((\r\n\t\t\t\t\t\t\tSELECT ',' + R.Name FROM Priv_BusiDataRole AS P1\r\n\t\t\t\t\t\t\tLEFT JOIN Priv_Role AS R ON P1.RoleId = R.Id\r\n\t\t\t\t\t\t\tWHERE P1.BusiDataId  = PT_MK.C_MKDM\r\n\t\t\t\t\t\t\tORDER BY R.No\r\n\t\t\t\t\t\t\tFOR XML PATH('')\r\n\t\t\t\t\t\t\t) , 1 , 1 , '') AS Role\r\n\t\t\t\t\tFROM PT_MK \r\n\t\t\t\t\tLEFT JOIN PT_YHMC_Privilege AS P2 ON PT_MK.C_MKDM = P2.C_MKDM\r\n\t\t\t\t\tGROUP BY PT_MK.C_MKDM, P2.C_MKDM\r\n\t\t\t\t)\r\n\t\t\t\tSELECT PT_MK.*, ctePriv.UserName, ctePriv.Role\r\n\t\t\t\tFROM PT_MK\r\n\t\t\t\tLEFT JOIN ctePriv ON ctePriv.C_MKDM = PT_MK.C_MKDM\r\n\t\t\t\tWHERE   PT_MK.C_MKDM LIKE '{0}%'\r\n\t\t\t\tORDER BY  PT_MK.C_MKDM\r\n\t\t\t", mkdm);
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public static string[] GetModulesLimitDetail(string mkdm)
        {
            string[] strArray = new string[2];
            string str = string.Empty;
            string str2 = string.Empty;
            DataTable table = publicDbOpClass.DataTableQuary("SELECT DISTINCT  P.V_YHDM,P.C_MKDM,Y.v_xm FROM PT_YHMC_Privilege P\r\n                LEFT JOIN PT_yhmc  Y\r\n                ON P.V_YHDM=Y.v_yhdm   WHERE  C_MKDM='" + mkdm + "'");
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    str = str + table.Rows[i][0].ToString() + ",";
                    str2 = str2 + table.Rows[i][2].ToString() + ",";
                }
            }
            strArray[0] = str;
            strArray[1] = str2;
            return strArray;
        }

        public static DataTable GetOneModule(string mkdm)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_mk where c_mkdm = '" + mkdm + "'");
        }

        public static DataTable GetUserPurviewModules(string UserId, string ptver)
        {
            return myxml.Getmenu(UserId, ptver);
        }

        public static void UpdateModuleLimit(string mkdm, string codes)
        {
            publicDbOpClass.ExecuteSQL("DELETE FROM PT_YHMC_Privilege WHERE C_MKDM LIKE '" + mkdm + "%' ");
            if (!string.IsNullOrEmpty(codes))
            {
                DataTable table = publicDbOpClass.DataTableQuary(" SELECT C_MKDM FROM PT_MK  WHERE C_MKDM LIKE '" + mkdm + "%'  ");
                for (int i = 1; i < (mkdm.Length / 2); i++)
                {
                    DataRow row = table.NewRow();
                    row[0] = mkdm.Substring(0, i * 2);
                    table.Rows.Add(row);
                }
                StringBuilder builder = new StringBuilder();
                builder.Append("BEGIN \n");
                string[] strArray = (from c in codes.Split(new char[] { ',' })
                    where c.Length == 8
                    select c).ToArray<string>();
                for (int j = 0; j < strArray.Length; j++)
                {
                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        builder.AppendFormat(" IF (SELECT COUNT(*) FROM PT_YHMC_Privilege WHERE V_YHDM = '{0}' AND C_MKDM = '{1}') = 0 \n", strArray[j], table.Rows[k][0].ToString());
                        builder.AppendFormat(" INSERT INTO PT_YHMC_Privilege (V_YHDM,C_MKDM,IsBasic,IsHaveOp) VALUES('{0}','{1}','0','0') \n", strArray[j], table.Rows[k][0].ToString());
                    }
                }
                builder.Append("END \n");
                publicDbOpClass.ExecuteSQL(builder.ToString());
            }
        }

        public static bool UpdModule(string mkdm, string mkmc, string cdlj, int xh, string img, string strBasic, string strMaintainable, string helpPath)
        {
            string str = "";
            mkmc = PublicClass.CheckString(mkmc);
            cdlj = PublicClass.CheckString(cdlj);
            str = " begin ";
            string str2 = str;
            return publicDbOpClass.NonQuerySqlString((((str2 + " update pt_mk set v_mkmc='" + mkmc + "',v_cdlj='" + cdlj + "',i_xh=" + xh.ToString() + ",v_img='" + img + "',IsBasic='" + strBasic + "',IsMaintainable='" + strMaintainable + "',helpPath='" + helpPath + "' where c_mkdm='" + mkdm + "' ") + " update pt_role_privilege set IsBasic = '" + strBasic + "' ") + " update pt_yhmc_privilege set IsBasic = '" + strBasic + "' ") + " end ");
        }
    }
}

