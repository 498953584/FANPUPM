using ASP;
using cn.justwin.BLL;
using cn.justwin.opm.BuildDiary;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OPM_BuildDiary_BuildDiary_MX : NBasePage, IRequiresSessionState
{
	private BuildDiary_mxAction mxAction = new BuildDiary_mxAction();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (!string.IsNullOrEmpty(base.Request["UID"]))
			{
				this.hfldBDID.Value = base.Request["UID"].ToString();
				this.BindGV(this.hfldBDID.Value);
			}
			if (!string.IsNullOrEmpty(base.Request["type"]) && base.Request["type"].ToString() == "view")
			{
				this.btnAdd.Style.Add("display", "none");
				this.btnDelete.Style.Add("display", "none");
				this.btnUpdate.Style.Add("display", "none");
			}
			if (!string.IsNullOrEmpty(base.Request["flowState"]))
			{
				this.hfldFlowstate.Value = base.Request["flowState"].ToString();
				return;
			}
			this.hfldFlowstate.Value = "";
		}
	}
	public void BindGV(string BDID)
	{
		this.gvBuildDiary_MX.DataSource = this.mxAction.GetList(BDID);
		this.gvBuildDiary_MX.DataBind();
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hdBuildDiary_MXID.Value))
		{
			string text;
			if (this.hdBuildDiary_MXID.Value.Contains("["))
			{
				text = this.hdBuildDiary_MXID.Value.Replace("[", "").Replace("]", "").Replace("\"", "'");
			}
			else
			{
				text = "'" + this.hdBuildDiary_MXID.Value + "'";
			}
			try
			{
				if (text != "" && this.mxAction.Delete(text) > 0)
				{
					this.BindGV(this.hfldBDID.Value);
				}
			}
			catch
			{
				base.RegisterScript("alert('系统提示：\\n\\n删除失败！')");
				this.BindGV(this.hfldBDID.Value);
			}
		}
	}
	protected void gvBuildDiary_MX_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBuildDiary_MX.DataKeys[e.Row.RowIndex]["UID"].ToString();
			e.Row.Attributes["guid"] = this.gvBuildDiary_MX.DataKeys[e.Row.RowIndex]["UID"].ToString();
			e.Row.Attributes["onclick"] = "clickRows('" + this.gvBuildDiary_MX.DataKeys[e.Row.RowIndex]["UID"].ToString() + "')";
		}
	}
}


