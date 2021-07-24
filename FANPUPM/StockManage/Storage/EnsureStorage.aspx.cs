using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Storage_EnsureStorage : NBasePage, IRequiresSessionState
{
	private Storage storage = new Storage();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.gvwStorage.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.DataBindStorage(this.Select());
		}
	}
	protected void gvwStorage_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwStorage.DataKeys[e.Row.RowIndex]["scode"].ToString();
			e.Row.Attributes["isfirst"] = this.gvwStorage.DataKeys[e.Row.RowIndex]["isfirst"].ToString();
		}
	}
	protected void gvwStorage_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwStorage.PageIndex = e.NewPageIndex;
		this.DataBindStorage(this.Select());
	}
	protected void btnEnsure_Click(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(this.hfldStorageCode.Value))
		{
			base.RegisterScript("top.ui.alert('请选择入库单');");
			return;
		}
		string value = this.hfldIsFirst.Value;
		if (!this.storage.UpdateInflag(this.hfldStorageCode.Value, value, WebUtil.GetUserNames(base.UserCode)))
		{
			base.RegisterScript("top.ui.alert('操作失败');");
			return;
		}
		base.RegisterScript("top.ui.alert('操作成功');");
		base.RegisterScript("location='EnsureStorage.aspx'");
	}
	private void DataBindStorage(DataTable table)
	{
		GridViewUtility.DataBind(this.gvwStorage, table);
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		DataTable table = this.Select();
		this.DataBindStorage(table);
	}
	private DataTable Select()
	{
		System.DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartTime.Text.Trim()));
		}
		System.DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1.0));
		}
		return this.storage.GetEnsureStorage(startDate, endDate, this.txtStorage.Text.Trim(), this.txtTrea.Text.Trim(), this.txtPeople.Text.Trim());
	}
}


