namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class ResourceBindAction
    {
        public DataTable ResourceBindList(string ResourceName, int ResourceStyle)
        {
            string sqlString = "";
            sqlString = sqlString + "select * from v_Res_Resource where ResourceName like '%" + ResourceName + "%'";
            if (ResourceStyle > 0)
            {
                object obj2 = sqlString;
                sqlString = string.Concat(new object[] { obj2, " and ResourceStyle ='", ResourceStyle, "'" });
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable ResourceBindList(string ResourceName, string ResourceCode)
        {
            string sqlString = "";
            sqlString = sqlString + "select * from v_Res_Resource where 1=1 ";
            if (ResourceName != "")
            {
                sqlString = sqlString + " and ResourceName like '%" + ResourceName + "%'";
            }
            if (ResourceCode != "")
            {
                sqlString = sqlString + " and ResourceCode like'%" + ResourceCode + "%'";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable ResourceList(Guid PrjCode)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, "select ResourceCode,ResourceName,ResourceUnit from EPM_Task_Resource where WbsType = 1 and ProjectCode = '", PrjCode, "' and ResourceCode = ''" }) + " group by ResourceCode,ResourceName,ResourceUnit");
        }

        public int RsourceCodeUp(int noteid, string resourcecode, string resourcestyle)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, "update EPM_Task_Resource set ResourceCode = '", resourcecode, "', ResourceStyle = '", resourcestyle, "' where NoteID = ", noteid }));
        }

        public int RsourceCodeUp(string resourcename, string resourcecode, string resourcestyle)
        {
            string str2 = "";
            return publicDbOpClass.ExecSqlString(str2 + "update EPM_Task_Resource set ResourceCode = '" + resourcecode + "', ResourceStyle = '" + resourcestyle + "' where ResourceName = '" + resourcename + "'");
        }
    }
}

