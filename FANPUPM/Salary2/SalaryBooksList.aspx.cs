using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Salary2_SalaryBooksList : NBasePage, IRequiresSessionState
{
	private SASalaryBooksService saBookService = new SASalaryBooksService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGV();
		}
	}
	private void BindGV()
	{
		System.Collections.Generic.List<SASalaryBooks> dataSource = (
			from sbs in this.saBookService
			orderby sbs.InputDate
			select sbs).ToList<SASalaryBooks>();
		this.gvSalaryBooks.DataSource = dataSource;
		this.gvSalaryBooks.DataBind();
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
			foreach (string current in list)
			{
				this.saBookService.DelSaMonthNoPayoff(current);
				SASalaryBooks byId = this.saBookService.GetById(current);
				this.saBookService.Delete(byId);
			}
			base.RegisterShow("系统提示", "删除成功!");
			this.BindGV();
		}
		catch
		{
			base.RegisterShow("系统提示", "删除失败!");
		}
	}
	protected void gvSalaryBooks_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvSalaryBooks.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void btnSaveData_Click(object sender, System.EventArgs e)
	{
		try
		{
			string name = this.txtSABookName.Text.Trim();
			string note = this.txtNote.Text.Trim();
			bool @checked = this.chkIsValid.Checked;
			if (string.IsNullOrEmpty(name))
			{
				base.RegisterShow("系统提示", "名称不能为空,保存失败!");
				this.BindGV();
			}
			else
			{
				int num = (
					from si in this.saBookService
					where si.Name == name
					select si).Count<SASalaryBooks>();
				if (this.hfldType.Value.Trim() == "add")
				{
					if (num > 0)
					{
						base.RegisterShow("系统提示", "名称已经存在,保存失败!");
						return;
					}
					SASalaryBooks sASalaryBooks = new SASalaryBooks();
					sASalaryBooks.Id = System.Guid.NewGuid().ToString();
					sASalaryBooks.Name = name;
					sASalaryBooks.IsValid = @checked;
					sASalaryBooks.Note = note;
					sASalaryBooks.InputUser = base.UserCode;
					sASalaryBooks.InputDate = System.DateTime.Now;
					this.saBookService.Add(sASalaryBooks);
				}
				else
				{
					SASalaryBooks byId = this.saBookService.GetById(this.hfldCheckedId.Value.Trim());
					if (byId.Name != name && num > 0)
					{
						base.RegisterShow("系统提示", "名称已经存在,保存失败!");
						return;
					}
					byId.Name = name;
					byId.Note = note;
					byId.IsValid = @checked;
					this.saBookService.Update(byId);
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
}


