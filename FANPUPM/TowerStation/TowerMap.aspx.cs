using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TowerStation_TowerMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGYSData();
            BindNetSystem();
        }
        
    }
    private void BindGYSData() {
        drop_gys.Items.Add(new ListItem("请选择", ""));
        DataTable dt = GetGYSInfos();
        foreach(DataRow dr in dt.Rows)
        { 
        drop_gys.Items.Add(new ListItem(dr["NetworkManufacturer"].ToString(), dr["NetworkManufacturer"].ToString()));
        }
    }
    private void BindNetSystem()
    {
        drop_networktype.Items.Clear();
        drop_networktype.Items.Add(new ListItem("请选择", ""));
        DataTable dt = GetNetSystem();
        foreach (DataRow dr in dt.Rows)
        {
            drop_networktype.Items.Add(new ListItem(dr["CodeName"].ToString(), dr["CodeId"].ToString()));
        }
    }
    private DataTable GetGYSInfos()
    {
        string sql = "select distinct NetworkManufacturer from TowerStationInfo";
        return publicDbOpClass.DataTableQuary(sql);
    }

    private DataTable GetNetSystem() {
        string sql = "select CodeId,CodeName from [dbo].[XPM_Basic_CodeList] where signCode2='NetworkSubsystem'";
        return publicDbOpClass.DataTableQuary(sql);

    }
}