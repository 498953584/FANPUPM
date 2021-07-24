using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Storage_Storage : NBasePage, IRequiresSessionState
{
	private readonly Storage storage = new Storage();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.gvwStorage.PageSize = NBasePage.pagesize;
			this.DataBindStorage(this.storage.GetTable(false));
		}
	}
	protected void gvwStorage_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwStorage.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwStorage.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["prjGuid"] = this.gvwStorage.DataKeys[e.Row.RowIndex].Values[2].ToString();
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldStorageCode.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldStorageCode.Value);
		}
		else
		{
			list.Add(this.hfldStorageCode.Value);
		}
		if (this.storage.Delete(list) == 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
			return;
		}
		this.DataBindStorage(this.GetSerachDataSource());
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.DataBindStorage(this.GetSerachDataSource());
	}
	private void DataBindStorage(DataTable dataSource)
	{
		GridViewUtility.DataBind(this.gvwStorage, dataSource);
	}
	private DataTable GetSerachDataSource()
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
		return this.storage.Select(startDate, endDate, this.txtStorage.Text.Trim(), this.txtTrea.Text, this.txtPeople.Text.Trim(), false);
	}
	protected void gvwStorage_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwStorage.PageIndex = e.NewPageIndex;
		this.DataBindStorage(this.storage.GetTable(false));
	}
}


