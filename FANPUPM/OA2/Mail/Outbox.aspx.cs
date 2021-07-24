using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class OA2_Outbox : NBasePage, IRequiresSessionState
{
	private MailService mailService = new MailService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = 50;
			this.BindDrop();
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
			System.Collections.Generic.List<Mail> list2 = new System.Collections.Generic.List<Mail>();
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(this.mailService.GetById(list[i].ToString()));
			}
			for (int j = 0; j < list2.Count; j++)
			{
				list2[j].IsValid = false;
				list2[j].InputDate = System.DateTime.Now.AddSeconds((double)j);
			}
			this.mailService.Update(list2);
			base.RegisterShow("系统提示", "删除成功");
			this.BindGv();
		}
		catch
		{
			base.RegisterShow("系统提示", "删除失败");
			this.BindGv();
		}
	}
	protected void gvEmail_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvEmail.DataKeys[e.Row.RowIndex].Values[0].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["AnnexId"] = this.gvEmail.DataKeys[e.Row.RowIndex].Values[1].ToString();
			this.hfldAnnexId.Value = this.gvEmail.DataKeys[e.Row.RowIndex].Values[1].ToString();
			if (this.gvEmail.DataKeys[e.Row.RowIndex].Values[3].ToString() == "True")
			{
				e.Row.Cells[4].Text = "已读";
				return;
			}
			e.Row.Cells[4].Text = "未读";
			e.Row.Cells[4].ForeColor = Color.FromName("#FF7D00");
			for (int i = 0; i < e.Row.Cells.Count; i++)
			{
				if (i == 2)
				{
					Label label = (Label)e.Row.FindControl("lbName");
					label.Font.Bold = true;
				}
				else
				{
					e.Row.Cells[i].Attributes.Add("style", "font-weight:bolder;");
				}
			}
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
			string msg = "撤回成功";
			for (int i = 0; i < list.Count; i++)
			{
				Mail byId = this.mailService.GetById(list[i].ToString());
				if (!byId.IsReaded)
				{
					byId.MailType = "R";
					byId.InputDate = System.DateTime.Now.AddSeconds((double)i);
					this.mailService.Update(byId);
					string strToMailId = byId.ToMailId;
					list[i].ToString();
					System.Collections.Generic.IEnumerable<Mail> source = this.mailService.AsEnumerable<Mail>();
					if (!string.IsNullOrEmpty(strToMailId))
					{
						source =
							from m in source
							where m.ToMailId.Contains(strToMailId) && m.MailType == "I"
							select m;
					}
					System.Collections.Generic.List<Mail> list2 = source.ToList<Mail>();
					if (list2.Count > 0)
					{
						this.mailService.Delete(list2[0].MailId);
					}
				}
				else
				{
					msg = "已读取邮件不能撤回!";
				}
			}
			base.RegisterShow("系统提示", msg);
			this.BindGv();
		}
		catch
		{
			base.RegisterShow("系统提示", "撤回失败");
			this.BindGv();
		}
	}
	protected void btnMove_Click(object sender, System.EventArgs e)
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
			System.Collections.Generic.List<Mail> list2 = new System.Collections.Generic.List<Mail>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(this.mailService.GetById(list[i].ToString()));
			}
			if (this.DDLtBox.SelectedValue == "I")
			{
				System.Collections.Generic.List<Mail> list3 = new System.Collections.Generic.List<Mail>();
				for (int j = 0; j < list2.Count; j++)
				{
					string toMailId = System.Guid.NewGuid().ToString();
					list2[j].ToMailId = toMailId;
					list2[j].MailType = "I";
					list2[j].IsValid = true;
					list2[j].IsReaded = false;
					list2[j].MailTo = base.UserCode;
					list2[j].MailFrom = base.UserCode;
					list2[j].AllMailTo = WebUtil.GetUserNames(base.UserCode);
					list2[j].AllMailToCode = base.UserCode;
					list2[j].AllCopyto = string.Empty;
					list2[j].AllCopytoCode = string.Empty;
					list2[j].InputDate = System.DateTime.Now;
					list2[j].ReadTime = null;
					list2[j].InputDate = System.DateTime.Now.AddSeconds((double)j);
					list3.Add(new Mail
					{
						MailId = System.Guid.NewGuid().ToString(),
						ToMailId = toMailId,
						MailName = list2[j].MailName,
						MailFrom = base.UserCode,
						MailTo = base.UserCode,
						AllMailToCode = base.UserCode,
						AllMailTo = WebUtil.GetUserNames(base.UserCode),
						AllCopytoCode = "",
						AllCopyto = "",
						MailContent = list2[j].MailContent,
						IsValid = true,
						IsReaded = false,
						MailType = "O",
						AnnexId = list2[j].AnnexId,
						InputDate = System.DateTime.Now.AddSeconds((double)j)
					});
				}
				this.mailService.Add(list3);
				this.mailService.Update(list2);
			}
			else
			{
				if (this.DDLtBox.SelectedValue == "D")
				{
					for (int k = 0; k < list.Count; k++)
					{
						list2[k].MailType = "D";
						list2[k].IsValid = true;
						list2[k].InputDate = System.DateTime.Now.AddSeconds((double)k);
					}
					this.mailService.Update(list2);
				}
				else
				{
					for (int l = 0; l < list.Count; l++)
					{
						list2[l].IsValid = false;
						list2[l].InputDate = System.DateTime.Now.AddSeconds((double)l);
					}
					this.mailService.Update(list2);
				}
			}
			base.RegisterShow("系统提示", "转移成功");
			this.BindGv();
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n转移失败！')");
			this.BindGv();
		}
	}
	private void BindGv()
	{
		string name = this.txtName.Text.Trim();
		string content = this.txtContent.Text.Trim();
		string to = this.txtTo.Text.Trim();
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
		this.AspNetPager1.RecordCount = this.mailService.GetCount(name, content, WebUtil.GetUserNames(base.UserCode), to, startDate, endDate, "O", new bool?(true), null);
		System.Collections.Generic.IList<Mail> dataSource = this.mailService.Query(name, content, WebUtil.GetUserNames(base.UserCode), to, startDate, endDate, "O", new bool?(true), null, new int?(this.AspNetPager1.PageSize), new int?(this.AspNetPager1.CurrentPageIndex));
		this.gvEmail.DataSource = dataSource;
		this.gvEmail.DataBind();
	}
	private void BindDrop()
	{
		this.DDLtBox.Items.Clear();
		this.DDLtBox.Items.Add(new ListItem("草稿箱", "D"));
		this.DDLtBox.Items.Add(new ListItem("已删除邮件", "L"));
		this.DDLtBox.Items.Add(new ListItem("收件箱", "I"));
	}
}


