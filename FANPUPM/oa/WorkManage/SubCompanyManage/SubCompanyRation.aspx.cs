using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_SubCompanyManage_SubCompanyRation : BasePage, IRequiresSessionState
{

	public OAOfficeResRationSetAction mcAction
	{
		get
		{
			return new OAOfficeResRationSetAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnSave.Attributes["onclick"] = "javascript:if(!confirm('确定保存本次设置吗?')) return false;";
	}
	protected void GVManager_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView arg_21_0 = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			HtmlInputText htmlInputText = (HtmlInputText)e.Row.Cells[3].FindControl("txtRation");
			htmlInputText.Attributes["onblur"] = "javascript:checkDecimal(this);";
		}
		switch (e.Row.RowType)
		{
		case DataControlRowType.Header:
		case DataControlRowType.DataRow:
			e.Row.Cells[5].Style["display"] = "none";
			break;
		case DataControlRowType.Footer:
			break;
		default:
			return;
		}
	}
	private ArrayList GetData()
	{
		ArrayList arrayList = new ArrayList();
		foreach (GridViewRow gridViewRow in this.GVManager.Rows)
		{
			arrayList.Add(new OAOfficeResRationSet
			{
				Ration = (((HtmlInputText)gridViewRow.Cells[3].FindControl("txtRation")).Value == "") ? 0m : Convert.ToDecimal(((HtmlInputText)gridViewRow.Cells[3].FindControl("txtRation")).Value),
				RationType = "1",
				RecordCode = gridViewRow.Cells[5].Text.Trim(),
				RecordDate = DateTime.Now,
				Remark = gridViewRow.Cells[4].Text.Replace("&nbsp;", "").Trim(),
				UserCode = this.Session["yhdm"].ToString()
			});
		}
		return arrayList;
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		ArrayList data = this.GetData();
		if (this.mcAction.Update(data) > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('保存成功!');</script>");
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('保存失败!');</script>");
	}
}


