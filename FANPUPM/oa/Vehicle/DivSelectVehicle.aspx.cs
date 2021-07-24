using ASP;
using cn.justwin.BLL;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_DivSelectVehicle : NBasePage, IRequiresSessionState
{
	protected MainModel mainModel;
	protected MainBLL mainBll = new MainBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvVehicle.PageSize = NBasePage.pagesize;
		if (!this.Page.IsPostBack)
		{
			this.ShowTaskList("", "", "");
		}
	}
	public void ShowTaskList(string code, string user, string where)
	{
		this.gvVehicle.DataSource = this.mainBll.getSelect(where);
		this.gvVehicle.DataBind();
	}
	protected override void OnInit(EventArgs e)
	{
		this.InitializeComponent();
		base.OnInit(e);
	}
	private void InitializeComponent()
	{
	}
	protected void SearchBt_Click(object sender, EventArgs e)
	{
		string text = "";
		if (!string.IsNullOrEmpty(this.txtVehicleCode.Text))
		{
			text = text + " and a.VehicleCode like '%" + this.txtVehicleCode.Text.Trim() + "%'";
		}
		if (!string.IsNullOrEmpty(this.txtVehicleUser.Text))
		{
			text = text + " and a.VehicleName like '%" + this.txtVehicleUser.Text.Trim() + "%'";
		}
		this.ShowTaskList("", "", text);
	}
	protected void gvVehicle_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"clickRow(this,'",
				dataRowView["guid"].ToString(),
				"','",
				dataRowView["vehicleCode"].ToString(),
				"');"
			});
			dataRowView["guid"].ToString();
			e.Row.Attributes["ondblclick"] = string.Concat(new string[]
			{
				"dbClickRow(this,'",
				dataRowView["guid"].ToString(),
				"','",
				dataRowView["vehicleCode"].ToString(),
				"')"
			});
		}
	}
	protected void gvVehicle_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvVehicle.PageIndex = e.NewPageIndex;
		this.ShowTaskList("", "", "");
	}
}


