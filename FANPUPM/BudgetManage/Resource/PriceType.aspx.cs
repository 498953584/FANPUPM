using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_PriceType : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			this.AspNetPager1.RecordCount = pm2Entities.Res_PriceType.Count<Res_PriceType>();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindPriceType();
		}
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldPriceTypeId.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldPriceTypeId.Value);
		}
		else
		{
			list.Add(this.hfldPriceTypeId.Value);
		}
		if (list.Count > 0)
		{
			try
			{
				this.DeletePriceType(list);
			}
			catch
			{
			}
		}
	}
	private void DeletePriceType(System.Collections.Generic.List<string> priceTypeIds)
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			foreach (string id in priceTypeIds)
			{
				Res_PriceType res_PriceType = (
					from pt in pm2Entities.Res_PriceType
					where pt.PriceTypeId == id
					select pt).FirstOrDefault<Res_PriceType>();
				if (res_PriceType != null)
				{
					if (this.IsUsedPriceType(res_PriceType))
					{
						base.RegisterScript("alert('系统提示:\\n\\n此价格类型已经使用，不能删除')");
						return;
					}
					pm2Entities.DeleteObject(res_PriceType);
				}
			}
			pm2Entities.SaveChanges();
		}
		this.DataBindPriceType();
	}
	private bool IsUsedPriceType(Res_PriceType priceType)
	{
		bool result;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			result = ((
				from p in pm2Entities.Res_Price
				where p.PriceTypeId == priceType.PriceTypeId
				select p).Count<Res_Price>() > 0);
		}
		return result;
	}
	protected void gvwPriceType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwPriceType.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindPriceType();
	}
	private void DataBindPriceType()
	{
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IQueryable<Res_PriceType> dataSource = (
				from t in pm2Entities.Res_PriceType
				orderby t.PriceTypeCode descending
				select t).Skip(NBasePage.pagesize * (currentPageIndex - 1)).Take(NBasePage.pagesize);
			this.gvwPriceType.DataSource = dataSource;
			this.gvwPriceType.DataBind();
		}
	}
}


