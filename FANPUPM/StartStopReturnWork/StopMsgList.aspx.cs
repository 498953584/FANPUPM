using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StartStopReturnWork_StopMsgList : NBasePage, IRequiresSessionState
{
	private PTStopMsgService stopMsgServer = new PTStopMsgService();
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
			this.hfldYear.Value = this.year;
			this.hfldPrjId.Value = this.prjId;
			this.GetProjectInfo();
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		System.Collections.Generic.List<PTStopMsg> list = (
			from stop in this.stopMsgServer
			where stop.PrjGuid == this.hfldPrjId.Value
			select stop into list0
			orderby list0.InputDate descending
			select list0).ToList<PTStopMsg>();
		this.gvStopMsg.DataSource = list;
		this.gvStopMsg.DataBind();
		if (list.Count <= 0)
		{
			this.hfldIsAllowAdd.Value = "1";
			return;
		}
		PTStopMsg stopMsg = list.FirstOrDefault<PTStopMsg>();
		PTRetMsgService source = new PTRetMsgService();
		System.Collections.Generic.List<PTRetMsg> list2 = (
			from ret in source
			where ret.StopMsgId == stopMsg.StopMsgId && ret.FlowState.Value == 1
			select ret).ToList<PTRetMsg>();
		if (list2.Count > 0)
		{
			this.hfldIsAllowAdd.Value = "1";
			return;
		}
		this.hfldIsAllowAdd.Value = "0";
	}
	protected void gvStopMsg_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string value = this.gvStopMsg.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			e.Row.Attributes["prjGuid"] = this.hfldPrjId.Value;
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		try
		{
			string value = this.hfldStopMsgChecked.Value;
			PTStopMsg byId = this.stopMsgServer.GetById(value);
			this.stopMsgServer.Delete(byId);
			this.BindGv();
			base.RegisterShow("系统提示", "删除成功!");
		}
		catch
		{
			base.RegisterShow("系统提示", "删除失败!");
		}
	}
	protected void GetProjectInfo()
	{
		if (!string.IsNullOrEmpty(this.hfldPrjId.Value))
		{
			ProjectInfo byId = ProjectInfo.GetById(this.hfldPrjId.Value);
			if (byId != null)
			{
				this.lblProject.Text = byId.PrjName;
				Label expr_3D = this.lblProject;
				expr_3D.Text = expr_3D.Text + "(" + Common2.GetPrjState(byId.PrjState.ToString(), true) + ")";
				this.hfldPrjState.Value = byId.PrjState.ToString();
			}
		}
	}
}


