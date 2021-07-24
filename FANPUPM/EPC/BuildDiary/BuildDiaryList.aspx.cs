using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.BuildDiary;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class OPM_BuildDiary_BuildDiaryList : NBasePage, IRequiresSessionState
{
	private BuildDiaryAction bdAction = new BuildDiaryAction();
	private bool isedit;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BuildDiaryPage.PageSize = NBasePage.pagesize;
			this.hfldSelectedPrj.Value = base.Request["PrjGuid"].ToString();
			if (base.Request["opType"] != null && base.Request["opType"].ToString() != "edit")
			{
				this.btnAdd.Style.Add("display", "none");
				this.btnDelete.Style.Add("display", "none");
				this.btnUpdate.Style.Add("display", "none");
				this.hfldType.Value = "view";
				this.isedit = false;
				this.WF1.Visible = false;
				this.HdnState.Value = base.Request["opType"].ToString();
			}
			else
			{
				this.hfldType.Value = "edit";
				this.isedit = true;
			}
			this.BindGV(this.hfldSelectedPrj.Value, null);
		}
	}
	public void BindGV(string pc, string flowState)
	{
		string creatorName = this.txtcreatorName.Text.Trim();
		string jobName = this.txtjobName.Text.Trim();
		string recordName = this.txtrecordName.Text.Trim();
		System.DateTime? startTime = null;
		if (!string.IsNullOrEmpty(this.txtstartTime.Text.Trim()))
		{
			startTime = new System.DateTime?(System.Convert.ToDateTime(this.txtstartTime.Text.Trim()));
		}
		System.DateTime? endTime = null;
		if (!string.IsNullOrEmpty(this.txtendTime.Text.Trim()))
		{
			endTime = new System.DateTime?(System.Convert.ToDateTime(this.txtendTime.Text.Trim()).AddDays(1.0));
		}
		int currentPageIndex = this.BuildDiaryPage.CurrentPageIndex;
		int count = this.bdAction.GetCount(pc, creatorName, jobName, recordName, startTime, endTime);
		DataTable buildListAll = this.bdAction.GetBuildListAll(base.UserCode, pc, flowState, creatorName, jobName, recordName, startTime, endTime, this.BuildDiaryPage.PageSize, currentPageIndex);
		this.BuildDiaryPage.RecordCount = count;
		this.gvBuildDiaryList.DataSource = buildListAll;
		this.gvBuildDiaryList.DataBind();
	}
	protected void BuildDiaryPage_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV(this.hfldSelectedPrj.Value, null);
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldGuid.Value))
		{
			string text = string.Empty;
			if (this.hfldGuid.Value.Contains("["))
			{
				text = this.hfldGuid.Value.Substring(1, this.hfldGuid.Value.Length - 2).Replace("\"", "'");
			}
			else
			{
				text = "'" + this.hfldGuid.Value + "'";
			}
			try
			{
				if (this.bdAction.ExistsBuildDiary(text).Rows.Count <= 0)
				{
					this.bdAction.Delete(text);
					this.BindGV(this.hfldSelectedPrj.Value, null);
				}
				else
				{
					base.RegisterScript("alert('系统提示：\\n\\n删除失败，请先删除施工日志明细中施工日志！')");
				}
			}
			catch
			{
				base.RegisterScript("alert('系统提示：\\n\\n请选择施工日志！')");
			}
		}
	}
	protected void btnDataBind_Click(object sender, System.EventArgs e)
	{
		this.BindGV(this.hfldSelectedPrj.Value, null);
	}
	protected void gvBuildDiaryList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBuildDiaryList.DataKeys[e.Row.RowIndex]["UID"].ToString();
			e.Row.Attributes["guid"] = this.gvBuildDiaryList.DataKeys[e.Row.RowIndex]["UID"].ToString();
			string text = ((HiddenField)e.Row.FindControl("hfldFlowState")).Value.Trim();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"clickRows('",
				this.gvBuildDiaryList.DataKeys[e.Row.RowIndex]["UID"].ToString(),
				"','",
				text,
				"')"
			});
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BuildDiaryPage.CurrentPageIndex = 1;
		this.BindGV(this.hfldSelectedPrj.Value, null);
	}
}


