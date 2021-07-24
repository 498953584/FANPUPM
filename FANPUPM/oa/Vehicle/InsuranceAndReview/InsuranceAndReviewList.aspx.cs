using ASP;
using cn.justwin.BLL;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_InsuranceAndReview_InsuranceAndReviewList : NBasePage, IRequiresSessionState
{
	private InsuranceAndReviewBLL IARBLL = new InsuranceAndReviewBLL();
	private InsuranceAndReviewModel IARMODEL = new InsuranceAndReviewModel();
	private MainBLL MAINBLL = new MainBLL();

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		this.IARgridView.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			List<InsuranceAndReviewModel> modelList = this.IARBLL.GetModelList(" Type <2 GROUP BY OA_Vehicle_InsuranceAndReview.VehicleCode,Type,OA_Vehicle_InsuranceAndReview.Guid,Date,OA_Vehicle_InsuranceAndReview.VehicleCode,OA_Vehicle_InsuranceAndReview.code ORDER BY Date desc");
			this.initDataGrw(modelList);
		}
	}
	private List<InsuranceAndReviewModel> searchItem()
	{
		string startDate = string.IsNullOrEmpty(this.txtBeinTime.Text.Trim()) ? "" : this.txtBeinTime.Text.Trim();
		string endDate = string.IsNullOrEmpty(this.txtEndTime.Text.Trim()) ? "" : this.txtEndTime.Text.Trim();
		string code = string.IsNullOrEmpty(this.txtCode.Text.ToString().Trim()) ? "" : this.txtCode.Text.ToString().Trim();
		string guid = string.IsNullOrEmpty(this.txtVehicleCode.Text.Trim()) ? "" : this.txtVehicleCode.Text.Trim();
		string type = this.DropDownList1.SelectedValue.ToString();
		if (string.IsNullOrEmpty(this.txtVehicleCode.Text.Trim()))
		{
			guid = "";
		}
		string order = " ORDER BY Date desc";
		return this.IARBLL.GetModelList(startDate, endDate, code, guid, type, order);
	}
	private void initDataGrw(List<InsuranceAndReviewModel> _LIST)
	{
		this.IARgridView.DataSource = _LIST;
		this.IARgridView.DataBind();
	}
	public string getVehicleCode(string guid)
	{
		string result = string.Empty;
		if (guid.ToString() != "" && this.MAINBLL.Exists(new Guid(guid.ToString())))
		{
			result = this.MAINBLL.GetModel(new Guid(guid.ToString())).VehicleCode.ToString();
		}
		return result;
	}
	protected void IARgridViewType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.IARgridView.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.IARgridView.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		if (this.hfldIRA.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldIRA.Value);
		}
		else
		{
			list.Add(this.hfldIRA.Value);
		}
		if (list.Count <= 0)
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
			return;
		}
		string text = string.Empty;
		for (int i = 0; i < list.Count; i++)
		{
			text = text + "'" + list[i].ToString() + "',";
		}
		text = text.Substring(0, text.Length - 1);
		if (this.IARBLL.DeleteList(text))
		{
			base.RegisterScript("window.location = window.location;");
			return;
		}
		base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.initDataGrw(this.searchItem());
	}
	protected void IARgridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.IARgridView.PageIndex = e.NewPageIndex;
		List<InsuranceAndReviewModel> lIST = this.searchItem();
		this.initDataGrw(lIST);
	}
}


