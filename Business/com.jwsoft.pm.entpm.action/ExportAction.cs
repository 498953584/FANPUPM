namespace com.jwsoft.pm.entpm.action
{
    using System;
    using System.Data;

    public class ExportAction
    {
        public static string StrExecl(DataTable dt)
        {
            string str = "";
            str = ((str + "<table border=\"1\" width = \"100%\">") + "<tr align=\"left\" >" + dt.TableName.ToString()) + "</tr>" + "<tr align=\"center\">";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str = (str + "<td><b>") + dt.Columns[i].ToString() + "</b></td>";
            }
            str = str + "</tr>";
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                str = str + "<tr align=\"center\">";
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    str = (str + "<td>") + dt.Rows[j][k].ToString() + " </td>";
                }
                str = str + "</tr>";
            }
            return (str + "</table>");
        }
    }
}

