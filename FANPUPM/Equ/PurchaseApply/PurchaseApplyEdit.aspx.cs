using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using cn.justwin.Serialize;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_PurchaseApply_PurchaseApplyEdit : NBasePage, IRequiresSessionState
{
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
			this.BindApplyInfo();
			this.BindDetailInfo();
		}
	}
	private void BindApplyInfo()
	{
		
	}
	private void GetApplyDetail()
	{
	}
	private void BindDetailInfo()
	{
	}
	protected void gvwApplyDetail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwApplyDetail.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnAddDetail_Click(object sender, System.EventArgs e)
	{
		this.AddDetailClick();
	}
	private void AddDetailClick()
	{
		if (!string.IsNullOrEmpty(this.hfldResourceId.Value))
		{
			string value = this.hfldResourceId.Value;
			this.UpdateViewState();
			ISerializable serializable = new JsonSerializer();
			string[] array = serializable.Deserialize<string[]>(value);
			if (array != null)
			{
				new Resource();
				new EquEquipmentTypeService();
				new ResResourceService();
			}
		}
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
		object arg_10_0 = this.ViewState["ApplyDetail"];
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			this.txtApplyCode.Text.Trim();
		}
		catch
		{
			base.RegisterScript("top.ui.show('保存失败');");
		}
	}
	private void AddApply()
	{
	}
	private void UpdateApply()
	{
	}
	private void SaveDetail()
	{
		this.UpdateViewState();
	}
	private void UpdateViewState()
	{
		object arg_10_0 = this.ViewState["ApplyDetail"];
	}
}


