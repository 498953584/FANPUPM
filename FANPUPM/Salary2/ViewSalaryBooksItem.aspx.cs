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
public class SaBooksItemTemp
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

public partial class Salary2_ViewSalaryBooksItem : NBasePage, IRequiresSessionState
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
			this.BindGV();
		}
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
	protected void gvSalaryBooksItem_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvSalaryBooksItem.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["No"] = this.gvSalaryBooksItem.DataKeys[e.Row.RowIndex].Values[1].ToString();
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
		System.Collections.Generic.List<SaBooksItemTemp> list2 = new System.Collections.Generic.List<SaBooksItemTemp>();
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
				SaBooksItemTemp saBooksItemTemp = new SaBooksItemTemp();
				if (byId != null)
				{
					saBooksItemTemp.Id = "[" + booksItemId + "]";
					saBooksItemTemp.ItemName = "[" + byId.Name + "]";
					list2.Add(saBooksItemTemp);
				}
			}
		}
		foreach (SaBooksItemTemp current in list2)
		{
			formula = formula.Replace(current.Id, current.ItemName);
		}
		return formula;
	}
}


