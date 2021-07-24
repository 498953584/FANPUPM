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
public class SaBooksItem
{
    public string Id
    {
        get;
        set;
    }
    public string ItemName
    {
        get;
        set;
    }
}

public partial class Salary2_SalaryBooksItem : NBasePage, IRequiresSessionState
{
	private string saBooksId = string.Empty;
	private SASalaryBooksItemService saBooksItemService = new SASalaryBooksItemService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["saBooksId"]))
		{
			this.saBooksId = base.Request["saBooksId"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindSaItem();
			this.BindGV();
			this.hfldSaBooks.Value = this.saBooksId;
		}
	}
	private void BindSaItem()
	{
		SASalaryItemService source = new SASalaryItemService();
		System.Collections.Generic.List<SASalaryItem> dataSource = (
			from si in source
			orderby si.No
			select si).ToList<SASalaryItem>();
		this.ddlSalaryItem.DataSource = dataSource;
		this.ddlSalaryItem.DataTextField = "Name";
		this.ddlSalaryItem.DataValueField = "Id";
		this.ddlSalaryItem.DataBind();
	}
	private void BindGV()
	{
		System.Collections.Generic.List<SASalaryBooksItem> dataSource = (
			from sbi in this.saBooksItemService
			where sbi.BooksId == this.saBooksId
			orderby sbi.No
			select sbi).ToList<SASalaryBooksItem>();
		this.gvSalaryBooksItem.DataSource = dataSource;
		this.gvSalaryBooksItem.DataBind();
	}
	protected void btnSaveData_Click(object sender, System.EventArgs e)
	{
		try
		{
			SASalaryBooksItem sASalaryBooksItem = new SASalaryBooksItem();
			if (this.hfldType.Value.Trim() == "add")
			{
				sASalaryBooksItem = this.GetModel(null);
				this.saBooksItemService.Add(sASalaryBooksItem);
			}
			else
			{
				sASalaryBooksItem = this.saBooksItemService.GetById(this.hfldCheckedId.Value.Trim());
				sASalaryBooksItem = this.GetModel(sASalaryBooksItem);
				this.saBooksItemService.Update(sASalaryBooksItem);
			}
			this.BindGV();
			base.RegisterShow("系统提示", "保存成功!");
			this.txtDefaultValue.Text = "0.000";
		}
		catch
		{
			base.RegisterShow("系统提示", "保存失败!");
		}
	}
	protected SASalaryBooksItem GetModel(SASalaryBooksItem model)
	{
		if (model == null)
		{
			model = new SASalaryBooksItem();
			model.Id = System.Guid.NewGuid().ToString();
			if ((
				from sbi in this.saBooksItemService
				where sbi.BooksId == this.saBooksId
				select sbi).Count<SASalaryBooksItem>() == 0)
			{
				model.No = 1;
			}
			else
			{
				int num = this.saBooksItemService.Max((SASalaryBooksItem sbi) => sbi.No);
				model.No = num + 1;
			}
		}
		model.BooksId = this.saBooksId;
		model.ItemId = this.ddlSalaryItem.SelectedValue.Trim();
		decimal value = 0m;
		if (!string.IsNullOrEmpty(this.txtDefaultValue.Text.Trim()))
		{
			value = System.Convert.ToDecimal(this.txtDefaultValue.Text.Trim());
		}
		model.DefaultValue = new decimal?(value);
		if (!string.IsNullOrEmpty(this.hfldFormula.Value.Trim()))
		{
			model.IsFormula = this.chkIsFormula.Checked;
		}
		else
		{
			model.IsFormula = false;
		}
		model.IsShow = this.chkIsDisplay.Checked;
		if (model.IsFormula)
		{
			model.Formula = this.hfldFormula.Value;
		}
		else
		{
			model.Formula = null;
		}
		return model;
	}
	protected void gvSalaryBooksItem_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvSalaryBooksItem.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["No"] = this.gvSalaryBooksItem.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["Num"] = ((HiddenField)e.Row.FindControl("hfldNum")).Value;
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
			foreach (string id in list)
			{
				int num = (
					from sbi in this.saBooksItemService
					where sbi.Formula.Contains(id)
					select sbi).Count<SASalaryBooksItem>();
				SASalaryBooksItem byId = this.saBooksItemService.GetById(id);
				if (num > 0)
				{
					string saItemName = this.GetSaItemName(byId.ItemId);
					stringBuilder.Append("名称为" + saItemName + "的明细在其他明细的公式中正在使用,不能执行删除！");
				}
				else
				{
					this.saBooksItemService.Delete(byId);
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
	protected string GetSaItemName(string saItemId)
	{
		SASalaryItemService sASalaryItemService = new SASalaryItemService();
		SASalaryItem byId = sASalaryItemService.GetById(saItemId);
		return byId.Name;
	}
	protected string ConvertFormula(string formula)
	{
		string[] array = formula.Split(new char[]
		{
			'['
		});
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array2[i];
			if (text.Contains(']'))
			{
				string item = text.Substring(0, text.IndexOf(']'));
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
		}
		System.Collections.Generic.List<SaBooksItem> list2 = new System.Collections.Generic.List<SaBooksItem>();
		SASalaryItemService sASalaryItemService = new SASalaryItemService();
		foreach (string booksItemId in list)
		{
			SASalaryBooksItem sASalaryBooksItem = (
				from sbi in this.saBooksItemService
				where sbi.Id == booksItemId
				select sbi).FirstOrDefault<SASalaryBooksItem>();
			if (sASalaryBooksItem != null)
			{
				SASalaryItem byId = sASalaryItemService.GetById(sASalaryBooksItem.ItemId);
				SaBooksItem saBooksItem = new SaBooksItem();
				if (byId != null)
				{
					saBooksItem.Id = "[" + booksItemId + "]";
					saBooksItem.ItemName = "[" + byId.Name + "]";
					list2.Add(saBooksItem);
				}
			}
		}
		foreach (SaBooksItem current in list2)
		{
			formula = formula.Replace(current.Id, current.ItemName);
		}
		return formula;
	}
}


