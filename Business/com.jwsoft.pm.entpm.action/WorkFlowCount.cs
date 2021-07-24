namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class WorkFlowCount
    {
        public string GetAduitUserName(int tempid, Guid InstanceCode)
        {
            object obj2 = ("" + " select * from WF_Instance n left join WF_Instance_Main m on n.ID =m.ID ") + " where TheOrder in (  " + " select isnull(Max(TheOrder),0) from WF_Instance a left join WF_Instance_Main b on a.ID =b.ID  ";
            object obj3 = string.Concat(new object[] { obj2, " where b.TemplateID = ", tempid, " " });
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj3, " ) and  m.TemplateID = ", tempid, "  and m.InstanceCode = '", InstanceCode, "'" }));
            if (table.Rows.Count <= 0)
            {
                return "";
            }
            userManageDb db = new userManageDb();
            string str2 = "";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string userName = db.GetUserName(table.Rows[i]["Operator"].ToString());
                if (userName != "")
                {
                    str2 = str2 + userName + ",";
                }
            }
            return str2;
        }

        public string UseDegree(int tempid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select count(*) from WF_Instance_Main where TemplateID = " + tempid);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "0";
        }
    }
}

