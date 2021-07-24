using com.jwsoft.pm.data;
using System;

public class person
{
    public static bool checkNameisExit(string pyname)
    {
        return (publicDbOpClass.DataTableQuary("select * from PT_LOGIN where V_DLID='" + pyname + "'").Rows.Count > 0);
    }
}

