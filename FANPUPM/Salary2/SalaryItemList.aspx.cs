using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Salary2_SalaryItemList : NBasePage, IRequiresSessionState
{
	private SASalaryItemService ItemService = new SASalaryItemService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGV();
		}
	}
	private void BindGV()
	{
		System.Collections.Generic.List<SASalaryItem> dataSource = (
			from si in this.ItemService
			orderby si.No
			select si).ToList<SASalaryItem>();
		this.gvSalaryItem.DataSource = dataSource;
		this.gvSalaryItem.DataBind();
	}
	protected void gvSalaryItem_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvSalaryItem.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["No"] = this.gvSalaryItem.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["IsAllowDel"] = this.gvSalaryItem.DataKeys[e.Row.RowIndex].Values[2].ToString();
			e.Row.Attributes["Num"] = ((HiddenField)e.Row.FindControl("hfldNum")).Value;
		}
	}
	protected void btnSaveData_Click(object sender, System.EventArgs e)
	{
		try
		{
			string name = this.txtSalaryName.Text.Trim();
			string note = this.txtNote.Text.Trim();
			if (string.IsNullOrEmpty(name))
			{
				base.RegisterShow("系统提示", "名称不能为空,保存失败!");
				this.BindGV();
			}
			else
			{
				int num = (
					from si in this.ItemService
					where si.Name == name
					select si).Count<SASalaryItem>();
				if (this.hfldType.Value.Trim() == "add")
				{
					if (num > 0)
					{
						base.RegisterShow("系统提示", "名称已经存在,保存失败!");
						return;
					}
					SASalaryItem sASalaryItem = new SASalaryItem();
					int num2 = this.ItemService.Max((SASalaryItem si) => si.No);
					sASalaryItem.Id = System.Guid.NewGuid().ToString();
					sASalaryItem.No = num2 + 1;
					sASalaryItem.Name = name;
					sASalaryItem.IsAllowDel = true;
					sASalaryItem.Note = note;
					this.ItemService.Add(sASalaryItem);
				}
				else
				{
					SASalaryItem byId = this.ItemService.GetById(this.hfldCheckedId.Value.Trim());
					if (byId.Name != name && num > 0)
					{
						base.RegisterShow("系统提示", "名称已经存在,保存失败!");
						return;
					}
					byId.Name = name;
					byId.Note = note;
					this.ItemService.Update(byId);
				}
				this.BindGV();
				base.RegisterShow("系统提示", "保存成功!");
			}
		}
		catch
		{
			base.RegisterShow("系统提示", "保存失败!");
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldCheckedId.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldCheckedId.Value);
		}
		else
		{
			list.Add(this.hfldCheckedId.Value);
		}
		try
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			SASalaryBooksItemService source = new SASalaryBooksItemService();
			SAMonthSalaryService source2 = new SAMonthSalaryService();
			foreach (string id in list)
			{
				SASalaryItem byId = this.ItemService.GetById(id);
				int num = (
					from sbi in source
					where sbi.ItemId == id
					select sbi).Count<SASalaryBooksItem>();
				int num2 = (
					from sms in source2
					where sms.ItemId == id
					select sms).Count<SAMonthSalary>();
				if (num == 0 && num2 == 0)
				{
					this.ItemService.Delete(byId);
				}
				else
				{
					stringBuilder.Append("名称为" + byId.Name + "的工资项在");
					if (num > 0)
					{
						stringBuilder.Append("帐套");
					}
					if (num > 0 && num2 > 0)
					{
						stringBuilder.Append("和");
					}
					if (num2 > 0)
					{
						stringBuilder.Append("工资表");
					}
					stringBuilder.Append("中正在使用,不能执行删除!");
				}
			}
			if (!string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				base.RegisterShow("系统提示", stringBuilder.ToString());
			}
			else
			{
				base.RegisterShow("系统提示", "删除成功!");
			}
			this.BindGV();
		}
		catch
		{
			base.RegisterShow("系统提示", "删除失败!");
		}
	}
}


