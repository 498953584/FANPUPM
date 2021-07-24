using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_Completed_PrjCompleted : NBasePage, IRequiresSessionState
{
	private const int _firstEditCellIndex = 3;
	public string PrjGuid
	{
		get
		{
			object obj = this.ViewState["PrjGuid"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["PrjGuid"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string text = base.Request["PrjId"].ToString();
			this.PrjGuid = text;
			this.hfldProjectId.Value = text;
			this.lblPrjName.Text = ProjectInfo.GetProjectName(this.PrjGuid);
			PrjCompleted.AddCompleted(this.PrjGuid);
			this.gvComplete.DataSource = PrjCompleted.GetPrjComplete(this.PrjGuid);
			this.gvComplete.DataBind();
			return;
		}
		for (int i = 0; i < this.gvComplete.Rows.Count; i++)
		{
			string text2 = ((TextBox)this.gvComplete.Rows[i].FindControl("txtPrepareStatus")).Text;
			((Label)this.gvComplete.Rows[i].FindControl("lblPrepareStatus")).Text = StringUtility.ReplaceTxt(text2);
			string text3 = ((TextBox)this.gvComplete.Rows[i].FindControl("txtUncompletedTrans")).Text;
			((Label)this.gvComplete.Rows[i].FindControl("lblUncompletedTrans")).Text = StringUtility.ReplaceTxt(text3);
			string text4 = ((TextBox)this.gvComplete.Rows[i].FindControl("txtRectification")).Text;
			((Label)this.gvComplete.Rows[i].FindControl("lblRectification")).Text = StringUtility.ReplaceTxt(text4);
		}
	}
	protected void gvComplete_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string prjCompletedId = this.gvComplete.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["id"] = this.gvComplete.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["isAnnex"] = CompletedAnnex.ExistAnnexAddress(this.PrjGuid, prjCompletedId).ToString();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			for (int i = 0; i < this.gvComplete.Rows.Count; i++)
			{
				PrjCompleted prjCompleted = new PrjCompleted();
				prjCompleted.PrjGuid = new System.Guid(this.PrjGuid);
				prjCompleted.ID = System.Guid.NewGuid().ToString();
				prjCompleted.InputDate = System.DateTime.Now;
				prjCompleted.InputUser = base.UserCode;
				prjCompleted.PrepareStatus = ((TextBox)this.gvComplete.Rows[i].FindControl("txtPrepareStatus")).Text;
				prjCompleted.UncompletedTrans = ((TextBox)this.gvComplete.Rows[i].FindControl("txtUncompletedTrans")).Text;
				prjCompleted.Rectification = ((TextBox)this.gvComplete.Rows[i].FindControl("txtRectification")).Text;
				prjCompleted.PrjCompletedId = this.gvComplete.DataKeys[i].Value.ToString();
				prjCompleted.Update(prjCompleted);
			}
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_PrjCompletedList' });");
		}
		catch (System.Exception)
		{
			base.RegisterScript("top.ui.alert('保存失败');");
		}
	}
}


