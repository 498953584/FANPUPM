using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Stocktake_StocktakeView : NBasePage, IRequiresSessionState
{
	private string stocktakeId = string.Empty;
	private StocktakeBll StocktakeBll = new StocktakeBll();
	private StocktakeDetailBll stocktakeDetailBll = new StocktakeDetailBll();

	protected override void OnInit(EventArgs e)
	{
		if (base.Request["stocktakeId"] != null)
		{
			this.stocktakeId = base.Request["stocktakeId"];
		}
		if (base.Request["ic"] != null && string.IsNullOrWhiteSpace(this.stocktakeId))
		{
			this.stocktakeId = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			StocktakeModel stocktakeModel = new StocktakeModel();
			stocktakeModel = this.StocktakeBll.GetById(this.stocktakeId);
			this.lblCode.Text = stocktakeModel.Code;
			this.lblName.Text = stocktakeModel.Name;
			string stocktakeDate = stocktakeModel.StocktakeDate;
			string str = stocktakeDate.Substring(0, 4);
			string str2 = stocktakeDate.Substring(4, 2);
			this.lblStocktakeDate.Text = str + "年" + str2 + "月";
			this.lblInputDate.Text = stocktakeModel.InputDate.ToString("yyyy-MM-dd  HH:mm:ss");
			this.lblPerson.Text = stocktakeModel.InputUser;
			this.lblEndTime.Text = stocktakeModel.EndDate.ToString("yyyy-MM-dd");
			this.lblTName.Text = stocktakeModel.TreasuryName;
			this.lblExplain.Text = stocktakeModel.Note;
			if (!string.IsNullOrEmpty(stocktakeModel.BeginDate.ToString()))
			{
				this.lblStateTime.Text = stocktakeModel.BeginDate.ToString("yyyy-MM-dd");
			}
			List<StocktakeDetailModel> dataSource = new List<StocktakeDetailModel>();
			dataSource = this.stocktakeDetailBll.GetByStocktakeId(this.stocktakeId);
			this.gvwStocktake.DataSource = dataSource;
			this.gvwStocktake.DataBind();
		}
	}
}


