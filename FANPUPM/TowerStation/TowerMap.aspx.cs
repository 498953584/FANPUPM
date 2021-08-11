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
    private DataTable GetGYSInfos()
    {
        string sql = "select distinct NetworkManufacturer from TowerStationInfo";
        return publicDbOpClass.DataTableQuary(sql);
    }
}