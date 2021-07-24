using com.jwsoft.pm.data;
using System;
using System.Data;

public class PMPrjAction
{
    public static DataTable getDTByWhere(string where)
    {
        string sqlString = " select * from PT_PrjInfo where isvalid=1 ";
        if (!string.IsNullOrEmpty(where))
        {
            sqlString = sqlString + where;
        }
        return publicDbOpClass.DataTableQuary(sqlString);
    }

    public static void updatePrjCode(string code, string guid)
    {
        publicDbOpClass.ExecSqlString("update PT_PrjInfo set prjcode='" + code + "' where prjGuid='" + guid + "'");
    }
}

