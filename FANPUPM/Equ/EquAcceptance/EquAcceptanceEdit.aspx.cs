using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_EquAcceptance_EquAcceptanceEdit : NBasePage, IRequiresSessionState
{
	private readonly PurchaseStock purchaseStock = new PurchaseStock();
	private string action = string.Empty;
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"];
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"];
		}
		if (string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = System.Guid.NewGuid().ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindAcceptance();
			this.BindDetailInfo();
		}
	}
	private void BindAcceptance()
	{
	}
	private void GetDetail()
	{
	}
	private void BindDetailInfo()
	{
	}
	protected void gvwDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwDetail.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnAddDetail_Click(object sender, System.EventArgs e)
	{
		try
		{
			this.AddDetail();
		}
		catch (System.Exception)
		{
		}
	}
	private void AddDetail()
	{
		string.IsNullOrEmpty(this.txtPurchaseCode.Text.Trim());
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldDetailID.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldDetailID.Value);
		}
		else
		{
			list.Add(this.hfldDetailID.Value.Trim());
		}
		this.UpdateViewState();
		this.DeleteDetail(list);
		this.BindDetailInfo();
	}
	private void DeleteDetail(System.Collections.Generic.List<string> detailIdList)
	{
		object arg_10_0 = this.ViewState["AcceptanceDetail"];
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
	}
	private void SaveDetail()
	{
		this.UpdateViewState();
	}
	private void UpdateViewState()
	{
		object arg_10_0 = this.ViewState["AcceptanceDetail"];
	}
	protected string GetCorpName(object corpId)
	{
		string result = string.Empty;
		if (corpId != null)
		{
			XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
			XPMBasicContactCorp byId = xPMBasicContactCorpService.GetById(System.Convert.ToInt32(corpId));
			if (byId != null)
			{
				result = byId.CorpName;
			}
		}
		return result;
	}
}


