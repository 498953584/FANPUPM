namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Web.UI;

    public class PageHelper
    {
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
    }
}

