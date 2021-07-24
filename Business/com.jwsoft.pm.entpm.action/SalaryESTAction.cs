namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Collections;
    using System.Data;

    public class SalaryESTAction
    {
        public DataTable SalaryESCount(int tempid, DateTime begindatetime, DateTime endbegin, string bmbm)
        {
            string str = "";
            if (bmbm != "")
            {
                str = publicDbOpClass.DataTableQuary("select v_bmbm from pt_d_bm where i_bmdm = " + bmbm).Rows[0][0].ToString();
            }
            else
            {
                str = bmbm;
            }
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { " exec P_HR_Salary_WageCount ", tempid, ",'", begindatetime.ToShortDateString(), "','", endbegin.ToShortDateString(), "','", str, "'" }));
        }

        public int SalaryESDelete(int tempid, string UserCode, int Year, int Month)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " delete HR_Salary_ES_", tempid, " where UserCode = '", UserCode, "' and year(PaymasterDate)=", Year, " and month(PaymasterDate) = ", Month }));
        }

        public DataTable SalaryESDT(int tempid)
        {
            string str = " begin";
            object obj2 = str + " select columnName as  ItemName,'0' as IsEdit ,columnName,0 as TheOrder,1 as IsDisplay";
            object obj3 = string.Concat(new object[] { obj2, " from v_FieldType where tableName = 'HR_Salary_ES_", tempid, "' and columnName not like 'F%' and columnName <> 'PaymasterDate' and columnName <> 'RecordID' union" });
            object obj4 = string.Concat(new object[] { obj3, " select (select ItemName from HR_Salary_TempletItem where TempletID = ", tempid, " and ItemID = replace(columnName,'F','')) as ItemName," });
            object obj5 = string.Concat(new object[] { obj4, " (select IsEdit from HR_Salary_TempletItem where TempletID = ", tempid, " and ItemID = replace(columnName,'F','')) as IsEdit,columnName," });
            object obj6 = string.Concat(new object[] { obj5, "(select TheOrder from HR_Salary_TempletItem where TempletID = ", tempid, " and ItemID = replace(columnName,'F','')) as TheOrder ," });
            object obj7 = string.Concat(new object[] { obj6, " (select IsDisplay from HR_Salary_TempletItem where TempletID = ", tempid, " and ItemID = replace(columnName,'F','')) as IsDisplay " });
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj7, " from v_FieldType where tableName = 'HR_Salary_ES_", tempid, "' and columnName like 'F%' order by TheOrder asc" }) + " end");
        }

        public DataTable SalaryESInfo(int tempid, Guid RecordID)
        {
            object obj2 = (" " + " select ( select ") + " (select distinct a.RoletypeName from pt_d_role a,pt_duty b where a.RoleTypeCode = b.dutycode and i_dutyid = pt_yhmc.i_dutyid ) " + " from pt_yhmc where v_yhdm = a.UserCode";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " )as RoleTypeName,* from HR_Salary_ES_", tempid, " as a where RecordID = '", RecordID, "'" }));
        }

        public DataTable SalaryESInfo(int tempid, string depcode, int Year, int Month)
        {
            object obj2 = (" " + " select ( select ") + " (select distinct a.RoletypeName from pt_d_role a,pt_duty b where a.RoleTypeCode = b.dutycode and i_dutyid = pt_yhmc.i_dutyid ) " + " from pt_yhmc where v_yhdm = a.UserCode";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " )as RoleTypeName,* from HR_Salary_ES_", tempid, " as a where v_bmbm like'", depcode, "%' and year(PaymasterDate)=", Year, " and month(PaymasterDate) = ", Month }));
        }

        public int SalaryESTAdd(int tempid, string depcode)
        {
            object obj2 = (" insert into HR_Salary_EST_" + tempid + "(UserCode,v_bmbm)") + " SELECT [v_yhdm],[v_bmbm]  FROM [v_UserInfoList]  a left join PT_SingleClasses b on a.ClassID = b.ClassID ";
            object obj3 = string.Concat(new object[] { obj2, " where b.ClassCode = (select ClassCode from HR_Salary_Templet where TempletID = ", tempid, ")" });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, " and a.[v_yhdm] not in (select UserCode from HR_Salary_EST_", tempid, ") " }) + " and v_bmbm = '" + depcode + "';select @@IDENTITY");
        }

        public int SalaryESTDelete(int tempid, string UserCode)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " delete HR_Salary_EST_", tempid, " where UserCode = '", UserCode, "'" }));
        }

        public DataTable SalaryESTDT(int tempid)
        {
            string str = " begin";
            object obj2 = str + " select columnName as  ItemName,'0' as IsEdit ,columnName,0 as TheOrder,1 as IsDisplay";
            object obj3 = string.Concat(new object[] { obj2, " from v_FieldType where tableName = 'HR_Salary_EST_", tempid, "' and columnName not like 'F%' union" });
            object obj4 = string.Concat(new object[] { obj3, " select (select ItemName from HR_Salary_TempletItem where TempletID = ", tempid, " and ItemID = replace(columnName,'F','')) as ItemName," });
            object obj5 = string.Concat(new object[] { obj4, " (select IsEdit from HR_Salary_TempletItem where  TempletID = ", tempid, " and ItemID = replace(columnName,'F','')) as IsEdit,columnName," });
            object obj6 = string.Concat(new object[] { obj5, "(select TheOrder from HR_Salary_TempletItem where TempletID = ", tempid, " and ItemID = replace(columnName,'F','')) as TheOrder ," });
            object obj7 = string.Concat(new object[] { obj6, " (select IsDisplay from HR_Salary_TempletItem where TempletID = ", tempid, " and ItemID = replace(columnName,'F','')) as IsDisplay " });
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj7, " from v_FieldType where tableName = 'HR_Salary_EST_", tempid, "' and columnName like 'F%' order by TheOrder asc" }) + " end");
        }

        public DataTable SalaryESTInfo(int tempid, string depcode)
        {
            object obj2 = (" " + " select ( select ") + " (select distinct a.RoletypeName from pt_d_role a,pt_duty b where a.RoleTypeCode = b.dutycode and i_dutyid = pt_yhmc.i_dutyid ) " + " from pt_yhmc where v_yhdm = a.UserCode";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " )as RoleTypeName,* from HR_Salary_EST_", tempid, " as a where v_bmbm ='", depcode, "'" }));
        }

        public int SalaryESTUpdate(int tempid, ArrayList AL)
        {
            string sqlString = "";
            for (int i = 0; i < AL.Count; i++)
            {
                string str2 = AL[i].ToString();
                object obj2 = sqlString;
                sqlString = string.Concat(new object[] { obj2, "update HR_Salary_EST_", tempid, " set ", str2 });
            }
            return publicDbOpClass.ExecSqlString(sqlString);
        }

        public int SalaryESUpdate(int tempid, ArrayList AL)
        {
            string sqlString = "";
            for (int i = 0; i < AL.Count; i++)
            {
                string str2 = AL[i].ToString();
                object obj2 = sqlString;
                sqlString = string.Concat(new object[] { obj2, "update HR_Salary_ES_", tempid, " set ", str2 });
            }
            return publicDbOpClass.ExecSqlString(sqlString);
        }
    }
}

