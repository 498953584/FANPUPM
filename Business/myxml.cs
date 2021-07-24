using com.jwsoft.pm.data;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;

public class myxml
{
    public static bool deladmindelInfo(string date)
    {
        return publicDbOpClass.NonQuerySqlString("delete from PT_LOG_DEL where 删除时间 < '" + date + "'");
    }

    public static string Getcs(string xmlpath)
    {
        string sqlString = "select   ptcs from PT_D_CS  where id=1";
        string str2 = publicDbOpClass.DataTableQuary(sqlString).Rows[0][0].ToString();
        publicDbOpClass.DataTableQuary("update PT_D_CS set ptcs=" + (Convert.ToInt32(str2) + 1) + " where id=1");
        return str2;
    }

    public static DataTable Getmenu(string UserId, string ptver)
    {
        string sqlString = string.Format("\r\n\t\t\tSELECT PT_MK.* \r\n\t\t\tFROM PT_MK \r\n\t\t\tWHERE C_MKDM IN (\r\n\t\t\t\tSELECT C_MKDM FROM PT_YHMC_Privilege WHERE V_YHDM = '{0}' \r\n\t\t\t\tUNION\r\n\t\t\t\tSELECT BusiDataId FROM Priv_BusiDataRole AS DR\r\n\t\t\t\tLEFT JOIN Priv_UserRole R ON DR.RoleId = R.RoleId\r\n\t\t\t\tWHERE DR.TableName = 'PT_MK'\r\n\t\t\t\t\tAND R.UserCode = '{0}'\r\n\t\t\t)\r\n\t\t\tAND Isdisplay=1 order by PT_MK.i_xh,PT_MK.c_mkdm\r\n\t\t", UserId);
        if (ptver == "test")
        {
            sqlString = "SELECT PT_MK.* FROM PT_MK INNER JOIN PT_YHMC_Privilege ON PT_MK.C_MKDM = PT_YHMC_Privilege.C_MKDM where PT_YHMC_Privilege.V_YHDM = '" + UserId + "' and Isdisplay=1  and PT_YHMC_Privilege.c_mkdm not like '38%' and PT_MK.c_mkdm not like '38%'  order by PT_MK.i_xh,PT_MK.c_mkdm";
        }
        return publicDbOpClass.DataTableQuary(sqlString);
    }

    public static DataTable GetPrjTree(string UserId)
    {
        string path = @"d:\XML\" + UserId;
        DataTable table = new DataTable();
        if (File.Exists(path + @"\PrjTree.xml"))
        {
            table.ReadXml(path + @"\PrjTree.xml");
            return table;
        }
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        table = publicDbOpClass.DataTableQuary("SELECT PT_MK.* FROM PT_MK INNER JOIN PT_YHMC_Privilege ON PT_MK.C_MKDM = PT_YHMC_Privilege.C_MKDM where PT_YHMC_Privilege.V_YHDM = '" + UserId + "' order by PT_MK.i_xh,PT_MK.c_mkdm");
        table.TableName = "a";
        table.WriteXml(path + @"\PrjTree.xml", XmlWriteMode.WriteSchema);
        return table;
    }

    public static bool GetTwoPWD(string TwoPWD)
    {
        bool flag = false;
        string path = HttpContext.Current.Request.MapPath("XML/TwoPWD.xml");
        DataTable table = new DataTable();
        if (File.Exists(path))
        {
            table.ReadXml(path);
            string str2 = FormsAuthentication.HashPasswordForStoringInConfigFile(TwoPWD, "md5");
            if ((table.Rows.Count > 0) && (table.Rows[0][0].ToString() == str2))
            {
                flag = true;
            }
            return flag;
        }
        string str3 = FormsAuthentication.HashPasswordForStoringInConfigFile(TwoPWD, "md5");
        if (FormsAuthentication.HashPasswordForStoringInConfigFile("easypm", "md5") == str3)
        {
            flag = true;
        }
        return flag;
    }

    public static DataTable GetTwoPWDlog()
    {
        string sqlString = "select *,isnull((SELECT v_xm FROM  PT_yhmc where v_yhdm=[PT_LOG_DEL].[删除人]),[删除人]) as yhmc , ([业务模块]+[记录名称]) as ywmk  from  [PT_LOG_DEL] order by [删除时间]";
        return publicDbOpClass.DataTableQuary(sqlString);
    }

    public static int Setmenu(string UserId)
    {
        try
        {
            string path = @"d:\XML\" + UserId;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DataTable table = publicDbOpClass.DataTableQuary("SELECT PT_MK.* FROM PT_MK INNER JOIN PT_YHMC_Privilege ON PT_MK.C_MKDM = PT_YHMC_Privilege.C_MKDM where PT_YHMC_Privilege.V_YHDM = '" + UserId + "' and Isdisplay=1 order by PT_MK.i_xh,PT_MK.c_mkdm");
            table.TableName = "a";
            table.WriteXml(path + @"\PT_MK.xml", XmlWriteMode.WriteSchema);
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    public static int SetPrjTree(string UserId)
    {
        try
        {
            string path = @"d:\XML\" + UserId;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DataTable table = publicDbOpClass.DataTableQuary("SELECT PT_MK.* FROM PT_MK INNER JOIN PT_YHMC_Privilege ON PT_MK.C_MKDM = PT_YHMC_Privilege.C_MKDM where PT_YHMC_Privilege.V_YHDM = '" + UserId + "' order by PT_MK.i_xh,PT_MK.c_mkdm");
            table.TableName = "a";
            table.WriteXml(path + @"\PrjTree.xml", XmlWriteMode.WriteSchema);
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    public static bool SetTwoPWD(string TwoPWD)
    {
        try
        {
            string path = HttpContext.Current.Request.MapPath("XML");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string str2 = FormsAuthentication.HashPasswordForStoringInConfigFile(TwoPWD, "md5");
            DataTable table = new DataTable("TwoPWD");
            table.Columns.Add("TP", typeof(string));
            table.Rows.Add(new object[] { str2 });
            FileInfo info = new FileInfo(path + @"\TwoPWD.xml");
            if (info.Attributes.ToString().IndexOf("ReadOnly") != -1)
            {
                info.Attributes = FileAttributes.Normal;
            }
            table.WriteXml(path + @"\TwoPWD.xml", XmlWriteMode.WriteSchema);
            info.Attributes = FileAttributes.ReadOnly;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool SetTwoPWDlog(string user, string userIP, string BusinessName, string fid, string fname)
    {
        try
        {
            string str = "INSERT INTO [PT_LOG_DEL]([删除时间],[删除人],[登陆IP],[业务模块],[记录ID],[记录名称])  VALUES ";
            object obj2 = str;
            publicDbOpClass.ExecuteSQL(string.Concat(new object[] { obj2, " ('", DateTime.Now, "','", user, "','", userIP, "',  '", BusinessName, "', '", fid, "', '", fname, "')" }));
            return true;
        }
        catch
        {
            return false;
        }
    }
}

