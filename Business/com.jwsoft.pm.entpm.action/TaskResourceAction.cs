namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class TaskResourceAction
    {
        public static bool DelResource(SLTaskResource res)
        {
            string str = "";
            return publicDbOpClass.NonQuerySqlString(((str + " begin ") + " delete from EPM_Task_Resource where NoteId = " + res.NoteID) + GetSQL(res.ProjectCode, res.TaskCode, res.WbsType) + " end ");
        }

        public DataTable GetScheduleResource(Guid projectCode, string TaskCode, string resourceStyle, int wbsType)
        {
            string str = "";
            object obj2 = str + " select b.NoteID,b.ResourceCode,c.ResourceName,c.Specification,c.UnitName,c.UnitID,b.Quantity,b.UnitPrice,b.Fee,b.Fee1,b.ResourceStyle " + " from EPM_Task_Resource b left outer join EPM_V_Res_ResourceBasic c on b.ResourceCode=c.ResourceCode and b.ResourceStyle=c.ResourceStyle ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where b.ProjectCode='", projectCode, "' and b.TaskCode='", TaskCode, "' and b.wbsType=", wbsType.ToString(), " and b.ResourceStyle in (", resourceStyle, ") " }));
        }

        private static string GetSQL(string ProjectCode, string TaskCode, int WbsType)
        {
            string str = string.Empty;
            if (WbsType == 1)
            {
                object obj2 = str;
                object obj3 = string.Concat(new object[] { obj2, " update EPM_Task_TaskList set SynthPrice=(select sum(isnull(Quantity,0.00)*isnull(UnitPrice,0.00)) as sumPrice from EPM_Task_Resource  where ProjectCode='", ProjectCode, "' and TaskCode='", TaskCode, "' and WbsType='", WbsType, "' and ResourceCode <> '')/dbo.f_GetNumFromTaskResource('", ProjectCode, "','", TaskCode, "'), " });
                string str2 = string.Concat(new object[] { obj3, " sumPrice=(select sum(isnull(Quantity,0.00)*isnull(UnitPrice,0.00)) as sumPrice from EPM_Task_Resource  where ProjectCode='", ProjectCode, "' and TaskCode='", TaskCode, "' and WbsType='", WbsType, "' and ResourceCode <> '')" });
                str = str2 + " where  ProjectCode='" + ProjectCode + "' and TaskCode='" + TaskCode + "'";
            }
            return str;
        }

        public static bool InsertResource(string projectCode, string taskCode, int wbsType, string pageSign, string userCode)
        {
            string str = "";
            str = " begin ";
            object obj2 = str;
            string str2 = string.Concat(new object[] { obj2, " insert into EPM_Task_Resource select '", projectCode, "','", taskCode, "','", Guid.Empty, "','',ResourceCode,0,Price,0,0,ResourceStyle,'',0,", wbsType.ToString(), ",ResourceName,UnitName,'0'" });
            string str3 = str2 + " from EPM_Res_TempResource where SessionCode='" + pageSign + "' and UserCode='" + userCode + "' and ResourceCode not in (select Resourcecode from EPM_Task_Resource where ProjectCode='" + projectCode + "' and TaskCode='" + taskCode + "' and wbsType=" + wbsType.ToString() + ")";
            object obj3 = str3 + " delete EPM_Res_TempResource where  SessionCode='" + pageSign + "' and UserCode='" + userCode + "'";
            object obj4 = string.Concat(new object[] { obj3, " update EPM_Task_TaskList set SynthPrice=(select sum(isnull(UnitPrice,0.00)) from EPM_Task_Resource where ProjectCode='", projectCode, "' and TaskCode='", taskCode, "' and WbsType='", wbsType, "')/dbo.f_GetNumFromTaskResource('", projectCode, "','", taskCode, "') " });
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj4, " where  ProjectCode='", projectCode, "' and TaskCode='", taskCode, "' and WbsType='", wbsType, "' " }) + " end ");
        }

        public static bool UpdResource(SLTaskResource res)
        {
            string sqlString = "";
            sqlString = " select UnitPrice,Quantity*UnitPrice as sumPrice from EPM_Task_Resource where NoteId = " + res.NoteID.ToString();
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            string str2 = string.Format("{0:F2}", Convert.ToDecimal(table.Rows[0][0].ToString()));
            string str3 = string.Format("{0:F2}", Convert.ToDecimal(table.Rows[0][1].ToString()));
            string str4 = string.Format("{0:F2}", Convert.ToDecimal(res.UnitPrice));
            string str5 = string.Format("{0:F2}", Convert.ToDecimal((decimal) (res.Quantity * res.UnitPrice)));
            if (str2 != str4)
            {
                DataTable table2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select TaskCode from EPM_Task_Resource where ResourceCode = '", res.ResourceCode, "' and projectcode = '", res.ProjectCode, "' and WbsType='", res.WbsType, "'" }));
                sqlString = " begin ";
                string str7 = sqlString;
                object obj2 = (str7 + " update EPM_Task_Resource set Quantity=" + res.Quantity.ToString() + ",UnitPrice=" + res.UnitPrice.ToString() + ",Fee1=" + res.Fee1.ToString() + ",Fee=" + res.Fee.ToString()) + " where NoteId = " + res.NoteID.ToString();
                sqlString = string.Concat(new object[] { obj2, " update EPM_Task_Resource set UnitPrice = ", res.UnitPrice.ToString(), " where ResourceCode = '", res.ResourceCode, "' and projectcode = '", res.ProjectCode, "' and WbsType='", res.WbsType, "'" });
                for (int i = 0; i < table2.Rows.Count; i++)
                {
                    sqlString = sqlString + GetSQL(res.ProjectCode, table2.Rows[i][0].ToString(), res.WbsType);
                }
                sqlString = sqlString + " end ";
            }
            if ((str2 == str4) && (str3 != str5))
            {
                sqlString = " begin ";
                string str8 = sqlString;
                sqlString = ((str8 + " update EPM_Task_Resource set Quantity=" + res.Quantity.ToString() + ",UnitPrice=" + res.UnitPrice.ToString() + ",Fee1=" + res.Fee1.ToString() + ",Fee=" + res.Fee.ToString()) + " where NoteId = " + res.NoteID.ToString()) + GetSQL(res.ProjectCode, res.TaskCode, res.WbsType) + " end ";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }
    }
}

