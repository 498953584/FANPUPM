using com.jwsoft.pm.data;
using System;
using System.Text;

public class SummModelAcion
{
    public static string getInertStr(SummModel sm)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("insert into pm_workplan_weeksummary ( wkpdetailsid,wkpsmcontents,wkppercent,wkprecorddate,wkpid) values('");
        builder.Append(string.Concat(new object[] { sm.WkpDetailsId, "','", sm.WkpSmContents, "','", sm.WkpPercent, "','", sm.WkpRecordDate, "','", sm.WkpId, "')" }));
        return builder.ToString();
    }

    public static string getUpdateStr(SummModel sm, Guid wkpdetailsid)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(string.Concat(new object[] { "update pm_workplan_weeksummary set wkpsmcontents='", sm.WkpSmContents, "',wkppercent='", sm.WkpPercent, "',wkprecorddate='", sm.WkpRecordDate, "'" }));
        builder.Append(" where wkpdetailsid='" + wkpdetailsid + "'");
        return builder.ToString();
    }

    public static bool InsertResult(SummModel sm)
    {
        return publicDbOpClass.NonQuerySqlString(getInertStr(sm));
    }

    public static bool UpdateResult(SummModel sm)
    {
        return publicDbOpClass.NonQuerySqlString(getUpdateStr(sm, sm.WkpDetailsId));
    }
}

