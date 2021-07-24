<%@ WebHandler Language="C#" Class="ComputeByFormula" %>

using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class SaItemCost
{
    public string ItemId
    {
        get;
        set;
    }
    public decimal Cost
    {
        get;
        set;
    }
    public bool IsFormula
    {
        get;
        set;
    }
}
public class ComputeByFormula : IHttpHandler
{
	private SASalaryBooksItemService saBooksItemService = new SASalaryBooksItemService();
	private SAPersonalTaxService saTaxService = new SAPersonalTaxService();
	private SASalaryItemService saItemService = new SASalaryItemService();
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		context.Response.ContentType = "text/json";
		string s = string.Empty;
		if (!string.IsNullOrEmpty(context.Request["itemCost"]) && !string.IsNullOrEmpty(context.Request["booksId"]))
		{
			string value = context.Request["itemCost"];
			string booksId = context.Request["booksId"];
			List<SaItemCost> list = JsonConvert.DeserializeObject<List<SaItemCost>>(value);
			list = this.ComputeNewValue(booksId, list);
			s = JsonHelper.JsonSerializer<SaItemCost[]>(list.ToArray());
		}
		context.Response.Write(s);
	}
	public List<SaItemCost> ComputeNewValue(string booksId, List<SaItemCost> saItemlist)
	{
		try
		{
			foreach (SaItemCost saItemCost in saItemlist)
			{
				SASalaryBooksItem sASalaryBooksItem = (
					from sbi in this.saBooksItemService
					where sbi.BooksId == booksId && sbi.ItemId == saItemCost.ItemId
					select sbi).FirstOrDefault<SASalaryBooksItem>();
				if (sASalaryBooksItem != null)
				{
					if (sASalaryBooksItem.IsFormula)
					{
						decimal cost = 0m;
						if (!string.IsNullOrEmpty(sASalaryBooksItem.Formula))
						{
							string formula = sASalaryBooksItem.Formula;
							string expression = this.ReplaceFormula(booksId, formula, saItemlist, 30);
							try
							{
								cost = CalculatorHelper.Calc(expression);
							}
							catch (Exception ex)
							{
								if (ex.Message == "计算错误")
								{
									cost = 0m;
								}
							}
						}
						saItemCost.Cost = cost;
					}
					else
					{
						SASalaryItem byId = this.saItemService.GetById(saItemCost.ItemId);
						if (byId.Code == "TaxRate" || byId.Code == "Deduct")
						{
							saItemCost.Cost = this.ComputeTaxrateDeduct(booksId, saItemlist, byId.Code, 30);
						}
					}
				}
			}
		}
		catch (Exception)
		{
			throw new Exception("计算工资失败，请确认公式逻辑是否正确或者计算额度是否过大！");
		}
		return saItemlist;
	}
	private decimal GetItemCostVal(string saBooksItemId, List<SaItemCost> itemCostList)
	{
		decimal result = 0m;
		SASalaryBooksItem byId = this.saBooksItemService.GetById(saBooksItemId);
		itemCostList = (
			from ic in itemCostList
			where !ic.IsFormula
			select ic).ToList<SaItemCost>();
		foreach (SaItemCost current in itemCostList)
		{
			if (byId.ItemId == current.ItemId)
			{
				result = current.Cost;
				break;
			}
		}
		return result;
	}
	private string ReplaceFormula(string booksId, string formula, List<SaItemCost> itemCostList, int level)
	{
		if (level < 0)
		{
			throw new Exception("计算工资失败，请确认公式逻辑是否正确或者计算额度是否过大！");
		}
		if (formula.Contains("["))
		{
			string[] array = formula.Split(new char[]
			{
				'['
			});
			List<string> list = new List<string>();
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
			foreach (string current in list)
			{
				SASalaryBooksItem byId = this.saBooksItemService.GetById(current);
				if (byId.IsFormula)
				{
					if (string.IsNullOrEmpty(byId.Formula))
					{
						formula = formula.Replace("[" + byId.Id + "]", "(0.000)");
					}
					else
					{
						formula = formula.Replace("[" + byId.Id + "]", "(" + byId.Formula.ToString() + ")");
					}
				}
				else
				{
					SASalaryItem byId2 = this.saItemService.GetById(byId.ItemId);
					if (byId2.Code == "TaxRate" || byId2.Code == "Deduct")
					{
						decimal num = this.ComputeTaxrateDeduct(booksId, itemCostList, byId2.Code, --level);
						formula = formula.Replace("[" + byId.Id + "]", num.ToString("0.000"));
					}
					else
					{
						formula = formula.Replace("[" + byId.Id + "]", this.GetItemCostVal(byId.Id, itemCostList).ToString("0.000"));
					}
				}
			}
			if (formula.Contains("["))
			{
				formula = this.ReplaceFormula(booksId, formula, itemCostList, --level);
			}
		}
		return formula;
	}
	private decimal ComputeTaxrateDeduct(string booksId, List<SaItemCost> itemCostList, string saItemCode, int level)
	{
		decimal taxable = this.GetTaxable(booksId, itemCostList, level);
		return this.GetTaxrateDeduct(taxable, saItemCode);
	}
	private decimal GetTaxable(string booksId, List<SaItemCost> itemCostList, int level)
	{
		SASalaryItem sASalaryItem = (
			from sis in this.saItemService
			where sis.Code == "Taxable"
			select sis).FirstOrDefault<SASalaryItem>();
		decimal result = 0m;
		foreach (SaItemCost itemCost in itemCostList)
		{
			if (itemCost.ItemId == sASalaryItem.Id)
			{
				if (itemCost.IsFormula)
				{
					SASalaryBooksItem sASalaryBooksItem = (
						from sbi in this.saBooksItemService
						where sbi.BooksId == booksId && sbi.ItemId == itemCost.ItemId
						select sbi).FirstOrDefault<SASalaryBooksItem>();
					if (sASalaryBooksItem == null || string.IsNullOrEmpty(sASalaryBooksItem.Formula))
					{
						continue;
					}
					string text = sASalaryBooksItem.Formula;
					text = this.ReplaceFormula(booksId, text, itemCostList, --level);
					try
					{
						result = CalculatorHelper.Calc(text);
						continue;
					}
					catch (Exception ex)
					{
						if (ex.Message == "计算错误")
						{
							result = 0m;
						}
						continue;
					}
				}
				result = itemCost.Cost;
			}
		}
		return result;
	}
	private List<SaItemCost> GetNewSaItemCost(SaItemCost itemCost, List<SaItemCost> itemCostList)
	{
		foreach (SaItemCost current in itemCostList)
		{
			if (current.ItemId == itemCost.ItemId)
			{
				current.Cost = itemCost.Cost;
				break;
			}
		}
		return itemCostList;
	}
	private decimal GetTaxrateDeduct(decimal value, string saItemCode)
	{
		decimal result = 0m;
		if (saItemCode == "TaxRate")
		{
			result = this.saTaxService.GetTaxRate(value);
		}
		else
		{
			if (saItemCode == "Deduct")
			{
				result = this.saTaxService.GetDeduct(value);
			}
		}
		return result;
	}
}
