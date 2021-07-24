using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_NewPersonRecordEdit : BasePage, IRequiresSessionState
{

	public OAOfficeResJoinDrawItemSetAction amAction
	{
		get
		{
			return new OAOfficeResJoinDrawItemSetAction();
		}
	}
	public OAOfficeResJoinDrawItemAction pmAction
	{
		get
		{
			return new OAOfficeResJoinDrawItemAction();
		}
	}
	public int PostLevel
	{
		get
		{
			object obj = this.ViewState["PostLevel"];
			if (obj != null)
			{
				return (int)this.ViewState["PostLevel"];
			}
			return -1;
		}
		set
		{
			this.ViewState["PostLevel"] = value;
		}
	}
	public string JoinUserCode
	{
		get
		{
			object obj = this.ViewState["JoinUserCode"];
			if (obj != null)
			{
				return (string)this.ViewState["JoinUserCode"];
			}
			return "";
		}
		set
		{
			this.ViewState["JoinUserCode"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["pl"] == null || base.Request["uc"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		if (base.Request["pl"].ToString() != "")
		{
			this.PostLevel = int.Parse(base.Request["pl"].ToString());
		}
		this.JoinUserCode = base.Request["uc"].ToString();
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList("a.PostLevel=" + this.PostLevel);
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView arg_1E_0 = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
		}
	}
	private ArrayList GetData()
	{
		ArrayList arrayList = new ArrayList();
		foreach (GridViewRow gridViewRow in this.GVBook.Rows)
		{
			OAOfficeResJoinDrawItem oAOfficeResJoinDrawItem = new OAOfficeResJoinDrawItem();
			oAOfficeResJoinDrawItem.DrawDate = DateTime.Now;
			oAOfficeResJoinDrawItem.DrawItemID = ((((HtmlInputHidden)gridViewRow.Cells[4].FindControl("HdnMatterialID")).Value.Trim() == "") ? 0 : int.Parse(((HtmlInputHidden)gridViewRow.Cells[4].FindControl("HdnMatterialID")).Value.Trim()));
			oAOfficeResJoinDrawItem.FactNumber = ((((HtmlInputText)gridViewRow.Cells[4].FindControl("factNum")).Value.Trim() == "") ? 0 : int.Parse(((HtmlInputText)gridViewRow.Cells[4].FindControl("factNum")).Value.Trim()));
			decimal d = (((HtmlInputHidden)gridViewRow.Cells[4].FindControl("HdnPlanFee")).Value.Trim() == "") ? 0m : Convert.ToDecimal(((HtmlInputHidden)gridViewRow.Cells[4].FindControl("HdnPlanFee")).Value.Trim());
			oAOfficeResJoinDrawItem.SumFee = oAOfficeResJoinDrawItem.FactNumber * d;
			oAOfficeResJoinDrawItem.UserCode = this.JoinUserCode;
			arrayList.Add(oAOfficeResJoinDrawItem);
		}
		return arrayList;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		ArrayList data = this.GetData();
		int num = this.pmAction.Add(data);
		if (num > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('保存成功!');returnValue=true;window.close();</script>");
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('没有相关数据可保存!');</script>");
	}
}


