namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Data;

    public class PersonnelAction
    {
        public static bool AddPersonnel(Hashtable htPersonnel)
        {
            bool flag = publicDbOpClass.Insert("[pt_yhmc]", htPersonnel);
            if (flag)
            {
                publicDbOpClass.ExecSqlString("insert into PT_YHMC_Privilege (V_YHDM,C_MKDM,IsBasic,IsHaveOp) select " + htPersonnel["v_yhdm"].ToString() + ",c_mkdm,IsBasic,'0' from pt_mk WHERE IsBasic='1'");
            }
            return flag;
        }

        public static string ClassName(int classid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT [ClassID], [ClassName], [ClassTypeCode] FROM [PT_SingleClasses] WHERE  ClassID = " + classid);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["ClassName"].ToString();
            }
            return "";
        }

        public static bool DelPerson(string userCode)
        {
            publicDbOpClass.ExecSqlString(((" begin" + " update pt_yhmc set State = '2',LeaveDate=GETDATE() where v_yhdm = '" + userCode + "'") + " update pt_login set c_sfyx = 'n' where v_yhdm = '" + userCode + "'") + " end ");
            return userManageDb.InsertRTXSynchronization(userCode, "n");
        }

        public static string getCorpName(string corpCode)
        {
            string str = "";
            object obj2 = publicDbOpClass.ExecuteScalar(" select corpName from PT_d_CorpCode where corpCode = '" + corpCode + "'");
            if (obj2 != null)
            {
                str = (string) obj2;
            }
            return str;
        }

        public static DataTable getDeptmentList()
        {
            string sqlString = "select * from pt_d_bm where c_sfyx='y' order by i_sjdm,i_xh";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static string getDeptName(int deptId)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("select * from pt_d_bm where i_bmdm = " + deptId.ToString());
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["v_bmmc"].ToString();
            }
            return str;
        }

        public static Hashtable GetDistinctRegister(string Field)
        {
            DataTable distinctRegister = GetDistinctRegister(Field, "PT_yhmc");
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < distinctRegister.Rows.Count; i++)
            {
                if (distinctRegister.Rows[i][0].ToString() != "")
                {
                    hashtable.Add(distinctRegister.Rows[i][0].ToString(), distinctRegister.Rows[i][0].ToString());
                }
            }
            return hashtable;
        }

        protected static DataTable GetDistinctRegister(string Field, string Table)
        {
            return publicDbOpClass.DataTableQuary(string.Format("select distinct {0} from {1}", Field, Table));
        }

        public static string getDutyName(int dutyId)
        {
            string str = "";
            object obj2 = publicDbOpClass.ExecuteScalar("select DutyName from pt_duty where i_dutyId = " + dutyId.ToString());
            if (obj2 != null)
            {
                str = (string) obj2;
            }
            return str;
        }

        public static ArrayList GetProEmploy(string Prjcode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select podepom,PrjManager from pt_prjinfo where PrjGuid='" + Prjcode + "'");
            ArrayList list = new ArrayList();
            string str2 = "";
            if (table.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(table.Rows[0]["podepom"].ToString()))
                {
                    str2 = str2 + table.Rows[0]["podepom"].ToString();
                }
                if (!string.IsNullOrEmpty(table.Rows[0]["PrjManager"].ToString()))
                {
                    str2 = str2 + "," + table.Rows[0]["PrjManager"].ToString().Substring(0, table.Rows[0]["PrjManager"].ToString().IndexOf('-'));
                }
                string[] strArray = str2.Substring(1).Split(new char[] { ',' });
                if (strArray.Length <= 0)
                {
                    return list;
                }
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (list.Count == 0)
                    {
                        list.Add(strArray[i]);
                        continue;
                    }
                    bool flag = false;
                    for (int j = 0; j < list.Count; j++)
                    {
                        strArray[i].ToString();
                        list[j].ToString();
                        if (strArray[i].ToString().Trim() == list[j].ToString().Trim())
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        list.Add(strArray[i]);
                    }
                }
            }
            return list;
        }

        public static DataTable InitProEmpoy(string Prjcode, string state)
        {
            ArrayList proEmploy = GetProEmploy(Prjcode);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("v_yhdm", typeof(string)));
            table.Columns.Add(new DataColumn("v_xm", typeof(string)));
            table.Columns.Add(new DataColumn("i_bmdm", typeof(string)));
            table.Columns.Add(new DataColumn("bmmc", typeof(string)));
            table.Columns.Add(new DataColumn("I_DUTYID", typeof(string)));
            table.Columns.Add(new DataColumn("DutyName", typeof(string)));
            table.Columns.Add(new DataColumn("OtherDepts", typeof(string)));
            table.Columns.Add(new DataColumn("OtherDutyIDs", typeof(string)));
            table.Columns.Add(new DataColumn("i_xh", typeof(string)));
            table.Columns.Add(new DataColumn("c_sfyx", typeof(string)));
            table.Columns.Add(new DataColumn("MobilePhoneCode", typeof(string)));
            table.Columns.Add(new DataColumn("RelationCorp", typeof(string)));
            table.Columns.Add(new DataColumn("EnterCorpDate", typeof(string)));
            table.Columns.Add(new DataColumn("Age", typeof(string)));
            table.Columns.Add(new DataColumn("State", typeof(string)));
            table.Columns.Add(new DataColumn("StateName", typeof(string)));
            for (int i = 0; i < proEmploy.Count; i++)
            {
                DataTable table2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "SELECT [v_yhdm], [v_xm], [i_bmdm],(select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc, [I_DUTYID],(select b.RoleTypeName from pt_d_role b where b.RoleTypeCode = (select c.DutyCode from pt_duty c where c.i_dutyId = a.i_dutyId)) as DutyName, [OtherDepts], [OtherDutyIDs], [i_xh], [c_sfyx], [MobilePhoneCode], [RelationCorp], [EnterCorpDate], [Age], [State],(case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName FROM [PT_yhmc] a WHERE a.State = ", state, " and ([v_yhdm] = '", proEmploy[i], "') order by v_xm" }));
                if (table2.Rows.Count > 0)
                {
                    DataRow row = table.NewRow();
                    row["v_yhdm"] = table2.Rows[0]["v_yhdm"];
                    row["v_xm"] = table2.Rows[0]["v_xm"];
                    row["i_bmdm"] = table2.Rows[0]["i_bmdm"];
                    row["bmmc"] = table2.Rows[0]["bmmc"];
                    row["I_DUTYID"] = table2.Rows[0]["I_DUTYID"];
                    row["DutyName"] = table2.Rows[0]["DutyName"];
                    row["OtherDepts"] = table2.Rows[0]["OtherDepts"];
                    row["OtherDutyIDs"] = table2.Rows[0]["OtherDutyIDs"];
                    row["i_xh"] = table2.Rows[0]["i_xh"];
                    row["c_sfyx"] = table2.Rows[0]["c_sfyx"];
                    row["MobilePhoneCode"] = table2.Rows[0]["MobilePhoneCode"];
                    row["RelationCorp"] = table2.Rows[0]["RelationCorp"];
                    row["EnterCorpDate"] = table2.Rows[0]["EnterCorpDate"];
                    row["Age"] = table2.Rows[0]["Age"];
                    row["State"] = table2.Rows[0]["State"];
                    row["StateName"] = table2.Rows[0]["StateName"];
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static int isExistData(string TableName, string userCode)
        {
            int num = -1;
            object obj2 = publicDbOpClass.ExecuteScalar("select count(*) as sl from " + TableName + " where UserCode = '" + userCode + "'");
            if (obj2 != null)
            {
                num = (int) obj2;
            }
            return num;
        }

        public static DataTable QueryDeptByCorpCode(string corpCode)
        {
            return publicDbOpClass.DataTableQuary(" Select i_bmdm,V_BMMC,i_sjdm from PT_d_bm where CorpCode = '" + corpCode + "' and c_sfyx = 'y'");
        }

        public static DataTable QueryPersonnelById(string userCode)
        {
            return publicDbOpClass.DataTableQuary(" Select * from pt_yhmc where v_yhdm = '" + userCode + "'");
        }

        public static DataTable QueryPersonnelchInfoById(string userCode)
        {
            return publicDbOpClass.DataTableQuary(string.Format("\r\n\t\t\t\tSelect *,\r\n\t\t\t\t\t(select b.v_bmmc from pt_d_bm b where b.i_bmdm = a.i_bmdm) as bmmc,\r\n\t\t\t\t\t(select c.DutyName from pt_duty c where c.i_dutyId = a.i_dutyId)\r\n\t\t\t\t\t as DutyName,\r\n\t\t\t\t\t(case c_sfyx when 'n' then '离职' else \r\n\t\t\t\t\t(case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end) \r\n\t\t\t\t\tend ) as StateName \r\n\t\t\t\tfrom pt_yhmc a \r\n\t\t\t\twhere v_yhdm = '{0}'\r\n\t\t\t", userCode));
        }

        public static bool ReDelPerson(string userCode)
        {
            publicDbOpClass.ExecSqlString(((((((" begin" + " DELETE FROM [pt_dbsj]   where v_yhdm = '" + userCode + "'  ") + " DELETE FROM [pt_xtrz]   where v_yhdm = '" + userCode + "'  ") + " DELETE FROM [PT_LOG]   where v_yhdm = '" + userCode + "'  ") + " DELETE FROM  [PT_YHMC_Privilege]   where v_yhdm = '" + userCode + "'   ") + " DELETE FROM pt_login   where v_yhdm = '" + userCode + "' ") + " DELETE FROM  pt_yhmc   where v_yhdm = '" + userCode + "' ") + " end ");
            return userManageDb.InsertRTXSynchronization(userCode, "n");
        }

        public static bool RePerson(string userCode)
        {
            publicDbOpClass.ExecSqlString(((" begin" + " update pt_yhmc set State = '1',LeaveDate=NULL where v_yhdm = '" + userCode + "'") + " update pt_login set c_sfyx = 'y' where v_yhdm = '" + userCode + "'") + " end ");
            return userManageDb.InsertRTXSynchronization(userCode, "y");
        }

        public static string[] setUserCode(string bmid)
        {
            string[] strArray = new string[2];
            bmid = bmid.PadLeft(3, '0');
            string str2 = publicDbOpClass.DataTableQuary("select max(v_yhdm)+1 as yhdm from pt_yhmc where v_yhdm like '" + bmid + "%'").Rows[0]["yhdm"].ToString();
            if (str2.Length == 0)
            {
                strArray[0] = "1";
                strArray[1] = bmid + "00001";
                return strArray;
            }
            strArray[0] = str2.Substring(str2.Length - 5, 5);
            strArray[1] = bmid + str2.Substring(str2.Length - 5, 5);
            return strArray;
        }

        public static bool UpdatePT_yhmc(string strv_yhdm, string stri_bmdm)
        {
            return publicDbOpClass.NonQuerySqlString("Update pt_yhmc set IsChargeMan='false' where IsChargeMan='True' and v_yhdm != " + strv_yhdm + "and i_bmdm =" + stri_bmdm);
        }

        public static bool UpdPersonnel(Hashtable htPersonnel, string where)
        {
            return publicDbOpClass.Update("[pt_yhmc]", htPersonnel, where);
        }
    }
}

