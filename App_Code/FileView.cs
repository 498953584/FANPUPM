using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Text;
using System.Web;

public class FileView
{
    public static string FilesBind(int moduleID, string recordCode)
    {
        StringBuilder builder = new StringBuilder();
        DataTable table = publicDbOpClass.DataTableQuary("select * from XPM_Basic_AnnexList where (RecordCode = '" + recordCode + "' and ModuleID = " + moduleID.ToString() + "  and state<>-1)");
        int count = table.Rows.Count;
        string str2 = string.Empty;
        builder.Append("<div   Style=\" word-break:break-all; width:90%;\">");
        for (int i = 0; i < table.Rows.Count; i++)
        {
            DataRow row = table.Rows[i];
            str2 = "<a href='/Common/DownLoad2.aspx?path=" + HttpUtility.UrlEncode(row["FilePath"].ToString() + row["AnnexName"].ToString()) + "&Name=" + HttpUtility.UrlEncode(row["OriginalName"].ToString()) + "'>" + row["OriginalName"].ToString() + "</a>";
            builder.Append(str2);
            if (((i + 1) % 3) == 0)
            {
                builder.Append("</br></br>");
            }
            else
            {
                builder.Append(",");
            }
        }
        string str3 = string.Empty;
        if (builder.Length > 2)
        {
            str3 = builder.ToString().Substring(0, builder.Length - 1);
        }
        return (str3 + "</div>");
    }
}

