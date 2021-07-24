using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class OA2_Mail_Deleted : NBasePage, IRequiresSessionState
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
			System.Collections.Generic.List<Mail> list3 = new System.Collections.Generic.List<Mail>();
			if (this.DDLtBox.SelectedValue == "I")
			{
				for (int j = 0; j < list2.Count; j++)
				{
					list2[j].ToMailId = System.Guid.NewGuid().ToString().Trim();
					list2[j].MailType = "I";
					list2[j].IsValid = true;
					list2[j].IsReaded = false;
					list2[j].MailTo = base.UserCode;
					list2[j].MailFrom = base.UserCode;
					list2[j].AllMailTo = WebUtil.GetUserNames(base.UserCode);
					list2[j].AllMailToCode = base.UserCode;
					list2[j].AllCopyto = string.Empty;
					list2[j].AllCopytoCode = string.Empty;
					list2[j].InputDate = System.DateTime.Now.AddSeconds((double)j);
					list3.Add(new Mail
					{
						MailId = System.Guid.NewGuid().ToString().Trim(),
						ToMailId = list2[j].ToMailId,
						MailName = list2[j].MailName,
						MailFrom = base.UserCode,
						MailTo = base.UserCode,
						AllCopyto = string.Empty,
						AllCopytoCode = string.Empty,
						AllMailTo = WebUtil.GetUserNames(base.UserCode),
						AllMailToCode = base.UserCode,
						AnnexId = list2[j].AnnexId,
						InputDate = System.DateTime.Now.AddSeconds((double)j),
						IsReaded = false,
						IsValid = true,
						MailContent = list2[j].MailContent,
						MailType = "O"
					});
				}
				this.mailService.Update(list2);
				this.mailService.Add(list3);
			}
			else
			{
				for (int k = 0; k < list.Count; k++)
				{
					list2[k].MailType = "D";
					list2[k].IsValid = true;
					list2[k].MailFrom = base.UserCode;
					list2[k].InputDate = System.DateTime.Now.AddSeconds(1.0);
					list2[k].MailFrom = base.UserCode;
				}
				this.mailService.Update(list2);
			}
			base.RegisterShow("系统提示", "转移成功");
			this.BindGv();
		}
		catch
		{
			base.RegisterShow("系统提示", "转移失败");
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
		}
	}
	private void BindGv()
	{
		string strName = this.txtName.Text.Trim();
		string strContent = this.txtContent.Text.Trim();
		string strTo = this.txtTo.Text.Trim();
		string strForm = this.txtFrom.Text.Trim();
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
		IQueryable<Mail> source = this.mailService.AsQueryable<Mail>();
		source =
			from m in source
			where m.MailName.Contains(strName) && m.MailContent.Contains(strContent) && m.IsValid == false && ((m.MailType == "I" && m.MailTo == this.UserCode) || (m.MailType == "O" && m.MailFrom == this.UserCode))
			select m;
		source =
			from m in source
			where m.MailToYhmc.v_xm.Contains(strTo)
			select m;
		source =
			from m in source
			where m.MailFromYhmc.v_xm.Contains(strForm)
			select m;
		if (startDate.HasValue)
		{
			source =
				from m in source
				where m.InputDate >= startDate.Value
				select m;
		}
		if (endDate.HasValue)
		{
			source =
				from m in source
				where m.InputDate <= endDate.Value
				select m;
		}
		source =
			from m in source
			orderby m.InputDate descending
			select m;
		int recordCount = source.Count<Mail>();
		this.AspNetPager1.RecordCount = recordCount;
		int? num = new int?(this.AspNetPager1.PageSize);
		int? num2 = new int?(this.AspNetPager1.CurrentPageIndex);
		if (num.HasValue && num2.HasValue)
		{
			source = source.Skip((num2.Value - 1) * num.Value).Take(num.Value);
		}
		System.Collections.Generic.IList<Mail> dataSource = source.ToList<Mail>();
		this.gvEmail.DataSource = dataSource;
		this.gvEmail.DataBind();
	}
	private void BindDrop()
	{
		this.DDLtBox.Items.Clear();
		this.DDLtBox.Items.Add(new ListItem("草稿箱", "D"));
		this.DDLtBox.Items.Add(new ListItem("收件箱", "I"));
	}
}


