using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TowerStation_CTSearch : System.Web.UI.Page
{
    protected string HTML = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HTML = BindGYSNetSystemInfo();
        }
    }

    private string  BindGYSNetSystemInfo()
    {
        DataTable gys = GetGYSInfos();
        DataTable netsystem = GetNetSystem();
        StringBuilder html = new StringBuilder();
        foreach (DataRow g in gys.Rows)
        {
            foreach (DataRow net in netsystem.Rows)
            {
                html.Append("<div id='' class='flex-sty col50 flex-center'><div id = '' class='jianju'><span class='jianju'>" +
                    g["NetworkManufacturer"].ToString() + "</span><span class='jianju'>" + net["CodeName"].ToString() + "</span></div><div id = '' class='jianju jianju2'><span id = '' class='jianju'>塔间距</span><span id = '' class='jianju'>0--500m</span>" +
                    "<input data-info='" + g["NetworkManufacturer"].ToString() + "_" + net["CodeName"].ToString() + "' name = 'Checkbox' type='checkbox' /></div></div>");
            }
        }
        return html.ToString();

    }

    private DataTable GetGYSInfos()
    {
        string sql = "select distinct NetworkManufacturer from TowerStationInfo";
        return publicDbOpClass.DataTableQuary(sql);
    }

    private DataTable GetNetSystem()
    {
        string sql = "select CodeId,CodeName from [dbo].[XPM_Basic_CodeList] where signCode2='NetworkSubsystem'";
        return publicDbOpClass.DataTableQuary(sql);

    }
}