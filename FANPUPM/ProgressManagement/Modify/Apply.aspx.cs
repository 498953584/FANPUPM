using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.ProgressManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ProgressManagement_Modify_Apply : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindPlans();
		this.AspNetPager1.PageSize = NBasePage.pagesize;
	}
	protected void BindPlans()
	{
		DataTable dataSource = null;
		if (!string.IsNullOrEmpty(this.prjId))
		{
			this.hfldPrjId.Value = this.prjId;
			this.hfldYear.Value = this.year;
			string userCode = base.UserCode;
			this.AspNetPager1.RecordCount = Progress.GetModifyPlansCount(this.prjId, userCode);
			dataSource = Progress.GetModifyPlans(this.prjId, userCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		}
		this.gvwPlans.DataSource = dataSource;
		this.gvwPlans.DataBind();
	}
	protected void gvwPlans_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes.Add("id", this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressVersionId"].ToString());
			e.Row.Attributes.Add("guid", this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressVersionId"].ToString());
			e.Row.Attributes.Add("progressId", this.gvwPlans.DataKeys[e.Row.RowIndex]["ProgressId"].ToString());
			e.Row.Attributes.Add("prjGuid", this.prjId);
		}
	}
	protected string GetState(string modifyFlowState)
	{
		string result = "<span style='color:#008B45;' state='7'>计划已审核</span>";
		if (!string.IsNullOrEmpty(modifyFlowState))
		{
			result = this.GetState(modifyFlowState, "调整");
		}
		return result;
	}
	public string GetState(string state, string stateText)
	{
		string result;
		if (state != null)
		{
			if (state == "-1")
			{
				result = "<span style='color:#050505;' state='-1'>" + stateText + "未提交</span>";
				return result;
			}
			if (state == "0")
			{
				result = "<span style='color:#030310;' state='0'>" + stateText + "审核中</span>";
				return result;
			}
			if (state == "1")
			{
				result = "<span style='color:#008B45;' state='1'>" + stateText + "已审核</span>";
				return result;
			}
			if (state == "-2")
			{
				result = "<span style='color:#ff0000;' state='-2'>" + stateText + "驳回</span>";
				return result;
			}
			if (state == "-3")
			{
				result = "<span style='color:#914229;' state='-3'>" + stateText + "重报</span>";
				return result;
			}
		}
		result = "<span style='color:#050505;'>" + stateText + "未审核</span>";
		return result;
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldChecked.Value;
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(value);
		}
		else
		{
			list.Add(value);
		}
		cn.justwin.BLL.ProgressManagement.Version.DelApply(list);
		this.BindPlans();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindPlans();
	}
}


