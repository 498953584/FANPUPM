namespace com.jwsoft.pm.entpm
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.action;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.web.WebControls;
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class PageHelper
    {
        public static void BindDropDownTree(DropDownTree ddv, BasicType bType)
        {
            int typeID = Convert.ToInt32(bType);
            BindDropDownTree(ddv, typeID);
        }

        public static void BindDropDownTree(DropDownTree ddv, int typeID)
        {
            CodingInfoCollection infos = new CodingAction().QueryCodeInfoList(typeID, ValidState.Valid);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("CodeID", typeof(int)));
            table.Columns.Add(new DataColumn("ParentCodeID", typeof(int)));
            table.Columns.Add(new DataColumn("CodeName", typeof(string)));
            int count = infos.Count;
            DataRow row = table.NewRow();
            row["CodeID"] = "9999";
            row["ParentCodeID"] = "0";
            row["CodeName"] = "全部".ToString();
            table.Rows.Add(row);
            for (int i = 0; i < count; i++)
            {
                DataRow row2 = table.NewRow();
                row2["CodeID"] = infos[i].CodeID.ToString();
                row2["ParentCodeID"] = infos[i].ParentCodeID.ToString();
                row2["CodeName"] = infos[i].CodeName;
                table.Rows.Add(row2);
            }
            ddv.LoadFromTable(table, false, "CodeID", "ParentCodeID", "CodeName", "CodeID");
        }

        public static void BindDropDownTree(DropDownTree ddv, int typeID, bool sel)
        {
            CodingInfoCollection infos = new CodingAction().QueryCodeInfoList(typeID, ValidState.Valid);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("CodeID", typeof(int)));
            table.Columns.Add(new DataColumn("ParentCodeID", typeof(int)));
            table.Columns.Add(new DataColumn("CodeName", typeof(string)));
            int count = infos.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow row = table.NewRow();
                row["CodeID"] = infos[i].CodeID.ToString();
                row["ParentCodeID"] = infos[i].ParentCodeID.ToString();
                row["CodeName"] = infos[i].CodeName;
                table.Rows.Add(row);
            }
            ddv.LoadFromTable(table, false, "CodeID", "ParentCodeID", "CodeName", "CodeID");
        }

        public static void BindDropDownTree(DropDownTree ddv, int typeID, int nodeID)
        {
            CodingInfoCollection infos = new CodingAction().QueryCodeInfoList(typeID, nodeID, ValidState.Valid);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("CodeID", typeof(int)));
            table.Columns.Add(new DataColumn("ParentCodeID", typeof(int)));
            table.Columns.Add(new DataColumn("CodeName", typeof(string)));
            int count = infos.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow row = table.NewRow();
                row["CodeID"] = infos[i].CodeID.ToString();
                row["ParentCodeID"] = infos[i].ParentCodeID.ToString();
                row["CodeName"] = infos[i].CodeName;
                table.Rows.Add(row);
            }
            ddv.LoadFromTable(table, false, "CodeID", "ParentCodeID", "CodeName", "CodeID");
        }

        public static void BindDropDownTree(DropDownTree ddv, int typeID, bool sel, string CodeID)
        {
            CodingInfoCollection infos = new CodingAction().QueryCodeInfoList(typeID, ValidState.Valid, CodeID);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("CodeID", typeof(int)));
            table.Columns.Add(new DataColumn("ParentCodeID", typeof(int)));
            table.Columns.Add(new DataColumn("CodeName", typeof(string)));
            int count = infos.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow row = table.NewRow();
                row["CodeID"] = infos[i].CodeID.ToString();
                row["ParentCodeID"] = infos[i].ParentCodeID.ToString();
                row["CodeName"] = infos[i].CodeName;
                table.Rows.Add(row);
            }
            ddv.LoadFromTable(table, false, "CodeID", "ParentCodeID", "CodeName", "CodeID");
        }

        public static string QueryBasicCode(Page obj, BasicType bType, int codeID)
        {
            string signCode = bType.ToString();
            int typeID = Convert.ToInt32(bType);
            return QueryBasicCode(obj, signCode, typeID, codeID);
        }

        public static string QueryBasicCode(Page obj, string signCode, int typeID, int codeID)
        {
            bool flag = true;
            Hashtable hashtable = new Hashtable();
            if (obj.Cache[signCode] == null)
            {
                flag = false;
            }
            else
            {
                hashtable = (Hashtable) obj.Cache[signCode];
                if (hashtable == null)
                {
                    flag = false;
                }
            }
            if (!flag)
            {
                CodingInfoCollection infos = new CodingAction().QueryCodeInfoList(typeID, ValidState.All);
                int count = infos.Count;
                for (int i = 0; i < count; i++)
                {
                    hashtable.Add(infos[i].CodeID.ToString(), infos[i].CodeName);
                }
            }
            obj.Cache[signCode] = hashtable;
            return hashtable[codeID.ToString()].ToString();
        }

        public static string QueryCorp(Page obj, int corpID)
        {
            try
            {
                bool flag = true;
                Hashtable hashtable = new Hashtable();
                if (obj.Cache["CORPLIST"] == null)
                {
                    flag = false;
                }
                else
                {
                    hashtable = (Hashtable) obj.Cache["CORPLIST"];
                    if (hashtable == null)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    ContactCorpCollection corps = new ContactCorpAction().QueryAllCorpList(ValidState.All);
                    int count = corps.Count;
                    for (int i = 0; i < count; i++)
                    {
                        hashtable.Add(corps[i].CorpID.ToString(), corps[i].CorpName);
                    }
                }
                obj.Cache["CORPLIST"] = hashtable;
                return hashtable[corpID.ToString()].ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string QueryDept(Page obj, int deptID)
        {
            bool flag = true;
            Hashtable hashtable = new Hashtable();
            if (obj.Cache["DEPTLIST"] == null)
            {
                flag = false;
            }
            else
            {
                hashtable = (Hashtable) obj.Cache["DEPTLIST"];
                if (hashtable == null)
                {
                    flag = false;
                }
            }
            if (!flag)
            {
                DeptCollection allDepartmentLists = new DeptManageDb().GetAllDepartmentLists();
                int count = allDepartmentLists.Count;
                for (int i = 0; i < count; i++)
                {
                    hashtable.Add(allDepartmentLists[i].DeptCode, allDepartmentLists[i].DeptName);
                }
            }
            obj.Cache["DEPTLIST"] = hashtable;
            try
            {
                return hashtable[deptID.ToString()].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string QueryUser(Page obj, string userCode)
        {
            if (string.IsNullOrEmpty(userCode))
            {
                return string.Empty;
            }
            string str = string.Empty;
            bool flag = true;
            Hashtable hashtable = new Hashtable();
            if (obj.Cache["USERLIST"] == null)
            {
                flag = false;
            }
            else
            {
                hashtable = (Hashtable) obj.Cache["USERLIST"];
                if (hashtable == null)
                {
                    flag = false;
                }
            }
            if (!flag)
            {
                UserCollection allUserLists = userManageDb.GetAllUserLists();
                int count = allUserLists.Count;
                for (int i = 0; i < count; i++)
                {
                    hashtable.Add(allUserLists[i].UserCode, allUserLists[i].UserName);
                }
            }
            obj.Cache["USERLIST"] = hashtable;
            try
            {
                str = hashtable[userCode].ToString();
            }
            catch (Exception)
            {
            }
            if (string.IsNullOrEmpty(str))
            {
                object obj2 = publicDbOpClass.ExecuteScalar(string.Format("SELECT v_xm FROM PT_yhmc WHERE v_yhdm = '{0}'", userCode));
                if ((obj2 != null) && (obj2 != DBNull.Value))
                {
                    str = obj2.ToString();
                }
            }
            return str;
        }

        public static void RefreshBasicCode(Page obj, BasicType bType)
        {
            string signCode = bType.ToString();
            int typeID = Convert.ToInt32(bType);
            RefreshBasicCode(obj, signCode, typeID);
        }

        public static void RefreshBasicCode(Page obj, string signCode, int typeID)
        {
            Hashtable hashtable = new Hashtable();
            CodingInfoCollection infos = new CodingAction().QueryCodeInfoList(typeID, ValidState.All);
            int count = infos.Count;
            for (int i = 0; i < count; i++)
            {
                hashtable.Add(infos[i].CodeID.ToString(), infos[i].CodeName);
            }
            obj.Cache[signCode] = hashtable;
        }

        public static void RefreshCorp(Page obj)
        {
            Hashtable hashtable = new Hashtable();
            ContactCorpCollection corps = new ContactCorpAction().QueryAllCorpList(ValidState.All);
            int count = corps.Count;
            for (int i = 0; i < count; i++)
            {
                hashtable.Add(corps[i].CorpID.ToString(), corps[i].CorpName);
            }
            obj.Cache["CORPLIST"] = hashtable;
        }

        public static void SetDropDownListSelected(DropDownList ddl, string selValue)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                ListItem item = ddl.Items[i];
                if (item.Value == selValue)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        public static void SetDropDownTreeSelected(DropDownTree ddv, string selValue)
        {
            bool flag = false;
            for (int i = 0; i < ddv.Items.Count; i++)
            {
                DropDownTreeItem item = ddv.Items[i];
                if (item.Value == selValue)
                {
                    flag = true;
                }
                else if (!flag && (item.Items.Count > 0))
                {
                    for (int j = 0; j < item.Items.Count; j++)
                    {
                        if (item.Items[j].Value == selValue)
                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    ddv.SelectedValue = selValue;
                    return;
                }
            }
        }
    }
}

