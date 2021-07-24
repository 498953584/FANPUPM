using ASP;
using cn.justwin.BLL;
using cn.justwin.Serialize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_RequirePlan_ProgressDetailEdit : NBasePage, IRequiresSessionState
{
	private string planId = string.Empty;
	private int year = System.DateTime.Now.Year;
	private int month = System.DateTime.Now.Month;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["planId"]))
		{
			this.planId = base.Request["planId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = System.Convert.ToInt32(base.Request["year"]);
		}
		if (!string.IsNullOrEmpty(base.Request["month"]))
		{
			this.month = System.Convert.ToInt32(base.Request["month"]);
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.gvProgressDetail.Rows.Count == 0)
		{
			base.RegisterScript("top.ui.show('请编制工程进度！')");
			return;
		}
		try
		{
			this.SaveStocks();
			string message = "top.ui.show('编制成功！');winclose('ProgressDetailEdit', 'RequirePlanList.aspx', true)";
			base.RegisterScript(message);
		}
		catch
		{
			base.RegisterScript("top.ui.show('保存失败！')");
		}
	}
	protected void btnBindEquType_Click(object sender, System.EventArgs e)
	{
		this.SelectedEquType(this.hfldEquTypeId.Value);
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (!this.hfldIdChecked.Value.Contains('['))
		{
			list.Add(this.hfldIdChecked.Value);
		}
		else
		{
			list = JsonHelper.GetListFromJson(this.hfldIdChecked.Value);
		}
		this.UpdateProgressDetailDataSource();
		this.DeleteResource(list);
		this.BindGV();
	}
	protected void gvProgressDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvProgressDetail.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	private void GetProgressDetails()
	{
	}
	private void SelectedEquType(string equTypeIds)
	{
		if (!string.IsNullOrEmpty(equTypeIds))
		{
			this.UpdateProgressDetailDataSource();
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(equTypeIds);
		}
	}
	private void UpdateProgressDetailDataSource()
	{
		object arg_10_0 = this.ViewState["ProgressDetail"];
	}
	private void DeleteResource(System.Collections.Generic.List<string> lstEquTypeID)
	{
		object arg_10_0 = this.ViewState["ProgressDetail"];
	}
	private void BindGV()
	{
	}
	private void SaveStocks()
	{
	}
}


