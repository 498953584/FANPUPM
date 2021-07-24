using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Salary2_UserSalaryBooks : NBasePage, IRequiresSessionState
{
	private string DepartmentId = string.Empty;
	private PtYhmcBll ptyhBll = new PtYhmcBll();
	private SASalaryBooksService saBooksService = new SASalaryBooksService();
	private System.Collections.Generic.List<SASalaryBooks> saBooks = new System.Collections.Generic.List<SASalaryBooks>();
	private SAUserSalaryBooksService saUserBookService = new SAUserSalaryBooksService();
	private SAPayoffService saPayoffService = new SAPayoffService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["department"]))
		{
			this.DepartmentId = base.Request["department"].ToString();
			this.saBooks = (
				from sbs in this.saBooksService
				where sbs.IsValid == true
				orderby sbs.InputDate
				select sbs).ToList<SASalaryBooks>();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindSaUserBooks(this.ddlSaUserBooks);
			this.BindSaUserBooks(this.ddlSaBooksSearch);
			this.BindGv();
		}
	}
	private void BindSaUserBooks(DropDownList ddl)
	{
		ddl.DataSource = this.saBooks;
		ddl.DataTextField = "Name";
		ddl.DataValueField = "Id";
		ddl.DataBind();
		ddl.Items.Insert(0, new ListItem("", ""));
	}
	private void BindGv()
	{
		this.AspNetPager1.RecordCount = this.ptyhBll.GetUserSaBooksCount(this.DepartmentId, this.txtUserCode.Text.Trim(), this.txtName.Text.Trim(), this.txtPost.Text.Trim(), this.ddlIsAssign.SelectedValue, this.ddlState.SelectedValue.Trim(), this.ddlSaBooksSearch.SelectedValue.Trim());
		DataTable userInfoSaBooks = this.ptyhBll.GetUserInfoSaBooks(this.DepartmentId, this.txtUserCode.Text.Trim(), this.txtName.Text.Trim(), this.txtPost.Text.Trim(), this.ddlIsAssign.SelectedValue, this.ddlState.SelectedValue.Trim(), this.ddlSaBooksSearch.SelectedValue.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwUserInfo.DataSource = userInfoSaBooks;
		this.gvwUserInfo.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvwUserInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwUserInfo.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["saUserBooksId"] = this.gvwUserInfo.DataKeys[e.Row.RowIndex].Values[1].ToString();
			string selectedValue = this.gvwUserInfo.DataKeys[e.Row.RowIndex].Values[2].ToString();
			System.Collections.Generic.List<SASalaryBooks> arg_CC_0 = (System.Collections.Generic.List<SASalaryBooks>)this.ViewState["saBooks"];
			DropDownList dropDownList = (DropDownList)e.Row.FindControl("ddlSaBooks");
			dropDownList.DataSource = this.saBooks;
			dropDownList.DataTextField = "Name";
			dropDownList.DataValueField = "Id";
			dropDownList.DataBind();
			dropDownList.Items.Insert(0, new ListItem("", ""));
			dropDownList.SelectedValue = selectedValue;
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
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
			foreach (string userId in list)
			{
				SAUserSalaryBooks sAUserSalaryBooks = (
					from su in this.saUserBookService
					where su.UserCode == userId
					select su).FirstOrDefault<SAUserSalaryBooks>();
				if (sAUserSalaryBooks != null)
				{
					this.saPayoffService.DelSaMonthNoPayoff(userId);
					if (!string.IsNullOrEmpty(this.hfldSelectedSaBooks.Value.Trim()))
					{
						sAUserSalaryBooks.BooksId = this.hfldSelectedSaBooks.Value;
						this.saUserBookService.Update(sAUserSalaryBooks);
					}
					else
					{
						this.saUserBookService.Delete(sAUserSalaryBooks);
					}
				}
				else
				{
					sAUserSalaryBooks = new SAUserSalaryBooks();
					sAUserSalaryBooks.Id = System.Guid.NewGuid().ToString();
					sAUserSalaryBooks.UserCode = userId;
					sAUserSalaryBooks.BooksId = this.hfldSelectedSaBooks.Value;
					this.saUserBookService.Add(sAUserSalaryBooks);
				}
			}
			base.RegisterShow("系统提示", "保存成功!");
			this.BindGv();
		}
		catch
		{
			base.RegisterShow("系统提示", "删除失败!");
		}
	}
}


