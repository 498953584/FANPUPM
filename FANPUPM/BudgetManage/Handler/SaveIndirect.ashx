<%@ WebHandler Language="C#" Class="SaveIndirect" %>

using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections.Generic;
using System.Web;
public class SaveIndirect : IHttpHandler
{
	public class IndirectData
	{
		public string guid;
		public string prjId;
		public string inputUser;
		public string type;
		public List<SaveIndirect.CBSAccount> CBS;
		public string flowState;
	}
	public class CBSAccount
	{
		public string code;
		public string amount;
	}
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		context.Response.ContentType = "text/plain";
		string jsonString = context.Request.Form[0];
		SaveIndirect.IndirectData indirectData = JsonHelper.JsonDeserialize<SaveIndirect.IndirectData>(jsonString);
		if (indirectData != null)
		{
			string prjId = indirectData.prjId;
			string inputUser = indirectData.inputUser;
			string guid = indirectData.guid;
			if (indirectData.flowState == "-1")
			{
				IndirectBudget.InsertWF(prjId, inputUser, guid);
			}
			if (indirectData.type == "1")
			{
				bool flag = IndirectBudget.IsExistPrjId(prjId);
				if (flag)
				{
					this.UpdateBudget(indirectData.CBS, prjId);
					return;
				}
				this.AddBudget(indirectData.CBS, prjId);
				return;
			}
			else
			{
				if (indirectData.type == "2")
				{
					bool flag = OrganizationBudget.IsExistOrgan(prjId);
					if (flag)
					{
						this.UpdateOrganBudget(indirectData.CBS, prjId);
						return;
					}
					this.AddOrganBudget(indirectData.CBS, prjId);
				}
			}
		}
	}
	protected void AddBudget(List<SaveIndirect.CBSAccount> CBSAccounts, string prjId)
	{
		foreach (SaveIndirect.CBSAccount current in CBSAccounts)
		{
			this.AddBudget(current, prjId);
		}
	}
	protected void UpdateBudget(List<SaveIndirect.CBSAccount> CBSAccounts, string prjId)
	{
		foreach (SaveIndirect.CBSAccount current in CBSAccounts)
		{
			string code = current.code;
			decimal accountAmount = Convert.ToDecimal(current.amount);
			string arg_2A_0 = string.Empty;
			IndirectBudget byPrjIdAndCBSCode = IndirectBudget.GetByPrjIdAndCBSCode(prjId, code);
			if (byPrjIdAndCBSCode != null)
			{
				byPrjIdAndCBSCode.AccountAmount = accountAmount;
				byPrjIdAndCBSCode.Update(byPrjIdAndCBSCode);
			}
			else
			{
				this.AddBudget(current, prjId);
			}
		}
	}
	protected void AddBudget(SaveIndirect.CBSAccount CBSAccount, string prjId)
	{
		string code = CBSAccount.code;
		decimal num = Convert.ToDecimal(CBSAccount.amount);
		string empty = string.Empty;
		IndirectBudget indirectBudget = IndirectBudget.Create(Guid.NewGuid().ToString(), prjId, code, num, num, empty, DateTime.Now, string.Empty);
		indirectBudget.Add(indirectBudget);
	}
	protected void AddOrganBudget(List<SaveIndirect.CBSAccount> CBSAccounts, string organizationBudgetId)
	{
		foreach (SaveIndirect.CBSAccount current in CBSAccounts)
		{
			this.AddOrganBudget(current, organizationBudgetId);
		}
	}
	protected void UpdateOrganBudget(List<SaveIndirect.CBSAccount> CBSAccounts, string organizationBudgetId)
	{
		foreach (SaveIndirect.CBSAccount current in CBSAccounts)
		{
			string code = current.code;
			decimal accountAmount = Convert.ToDecimal(current.amount);
			OrganizationBudget byOrganAndCBSCode = OrganizationBudget.GetByOrganAndCBSCode(organizationBudgetId, code);
			if (byOrganAndCBSCode != null)
			{
				byOrganAndCBSCode.AccountAmount = accountAmount;
				byOrganAndCBSCode.Update(byOrganAndCBSCode);
			}
			else
			{
				this.AddOrganBudget(current, organizationBudgetId);
			}
		}
	}
	protected void AddOrganBudget(SaveIndirect.CBSAccount CBSAccount, string organizationBudgetId)
	{
		string id = Guid.NewGuid().ToString();
		string empty = string.Empty;
		string code = CBSAccount.code;
		string empty2 = string.Empty;
		decimal num = Convert.ToDecimal(CBSAccount.amount);
		OrganizationBudget organizationBudget = OrganizationBudget.Create(id, organizationBudgetId, code, num, num, empty2, DateTime.Now, empty);
		organizationBudget.Add(organizationBudget);
	}
}
