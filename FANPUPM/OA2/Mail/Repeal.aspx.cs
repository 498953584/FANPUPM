using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class OA2_Mail_Repeal : NBasePage, IRequiresSessionState
{
	private MailService mailService = new MailService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = 50;
			this.BindGv();
		}
		this.hfldAdjunctPath.Value = ConfigHelper.Get("Mail");
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void gvEmail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvEmail.DataKeys[e.Row.RowIndex].Values[0].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["AnnexId"] = this.gvEmail.DataKeys[e.Row.RowIndex].Values[1].ToString();
			this.hfldAnnexId.Value = this.gvEmail.DataKeys[e.Row.RowIndex].Values[1].ToString();
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (this.hfldEmailID.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.hfldEmailID.Value);
			}
			else
			{
				list.Add(this.hfldEmailID.Value);
			}
			this.mailService.Delete(list);
			base.RegisterShow("系统提示", "删除成功");
			this.BindGv();
		}
		catch
		{
			base.RegisterShow("系统提示", "删除失败");
			this.BindGv();
		}
	}
	protected void btnRepeal_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (this.hfldEmailID.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.hfldEmailID.Value);
			}
			else
			{
				list.Add(this.hfldEmailID.Value);
			}
			this.mailService.Delete(list);
			base.RegisterShow("系统提示", "撤销成功");
			this.BindGv();
		}
		catch
		{
			base.RegisterShow("系统提示", "撤销失败");
			this.BindGv();
		}
	}
	private void BindGv()
	{
		string name = this.txtName.Text.Trim();
		string to = this.txtTo.Text.Trim();
		string content = this.txtContent.Text.Trim();
		System.DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartTime.Text.Trim()));
		}
		System.DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0));
		}
		this.AspNetPager1.RecordCount = this.mailService.GetCount(name, content, WebUtil.GetUserNames(base.UserCode), to, startDate, endDate, "R", new bool?(true), null);
		System.Collections.Generic.IList<Mail> dataSource = this.mailService.Query(name, content, WebUtil.GetUserNames(base.UserCode), to, startDate, endDate, "R", new bool?(true), null, new int?(this.AspNetPager1.PageSize), new int?(this.AspNetPager1.CurrentPageIndex));
		this.gvEmail.DataSource = dataSource;
		this.gvEmail.DataBind();
	}
}


