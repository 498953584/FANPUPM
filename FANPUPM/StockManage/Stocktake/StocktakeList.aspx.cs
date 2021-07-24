using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Stocktake_StocktakeList : NBasePage, IRequiresSessionState
{
	private readonly Storage storage = new Storage();
	private readonly Treasury treasury = new Treasury();
	private readonly StocktakeBll stocktakeBll = new StocktakeBll();
	private readonly StocktakeDetailBll stocktakeDetailBll = new StocktakeDetailBll();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.gvwStocktake.PageSize = NBasePage.pagesize;
			this.hfldToday.Value = DateTime.Now.ToString("yyyy-M-d");
			this.treasury.ParseTreasuryList(this.tvTreasury, base.UserCode, true, true);
			if (base.Request["tCode"] != null)
			{
				string text = base.Request["tcode"];
				StringBuilder stringBuilder = new StringBuilder("0");
				for (int i = 0; i < text.Length / 3; i++)
				{
					stringBuilder.Append("/" + text.Substring(0, 3 * (i + 1)));
				}
				this.tvTreasury.FindNode(stringBuilder.ToString()).Select();
				this.hfSelectValue.Value = this.tvTreasury.SelectedValue;
				this.hfSelectName.Value = this.tvTreasury.SelectedNode.Text;
				this.BindGridView();
			}
		}
	}
	protected void gvwStocktake_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwStocktake.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwStocktake.DataKeys[e.Row.RowIndex].Value.ToString();
			string text = ((Label)e.Row.Cells[4].FindControl("lblStocktakeDate")).Text.Trim();
			if (text != "")
			{
				string str = text.Substring(0, 4);
				string str2 = text.Substring(4, 2);
				((Label)e.Row.Cells[4].FindControl("lblStocktakeDate")).Text = str + "年" + str2 + "月";
			}
		}
	}
	protected void BindGridView()
	{
		this.gvwStocktake.DataSource = this.stocktakeBll.GetByTreasuryCode(this.tvTreasury.SelectedValue);
		this.gvwStocktake.DataBind();
		this.hfldIsFirst.Value = this.stocktakeBll.IsFirst(this.tvTreasury.SelectedValue).ToString();
		this.hfldIsAdd.Value = this.stocktakeBll.IsAdd(this.tvTreasury.SelectedValue).ToString();
		StocktakeModel editModel = this.stocktakeBll.GetEditModel(this.tvTreasury.SelectedValue);
		if (editModel != null)
		{
			if (editModel.State == 1)
			{
				this.btnLock.Attributes.Remove("disabled");
				this.btnOverrule.Attributes.Remove("disabled");
			}
			else
			{
				this.btnLock.Attributes.Add("disabled", "disabled");
				this.btnOverrule.Attributes.Add("disabled", "disabled");
			}
			this.hfldState.Value = editModel.FlowState.ToString();
		}
		else
		{
			this.btnLock.Attributes.Add("disabled", "disabled");
			this.btnOverrule.Attributes.Add("disabled", "disabled");
		}
		StocktakeModel lastStocktakeModel = this.stocktakeBll.GetLastStocktakeModel(this.tvTreasury.SelectedValue);
		if (lastStocktakeModel == null)
		{
			this.hfldLastStocktakeDate.Value = "false";
			return;
		}
		string stocktakeDate = lastStocktakeModel.StocktakeDate;
		if (!(stocktakeDate != ""))
		{
			this.hfldLastStocktakeDate.Value = "false";
			return;
		}
		DateTime.Now.ToString("yyyyMM");
		if (DateTime.Now.ToString("yyyyMM") == stocktakeDate)
		{
			this.hfldLastStocktakeDate.Value = "true";
			return;
		}
		this.hfldLastStocktakeDate.Value = "false";
	}
	protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.hfSelectValue.Value = this.tvTreasury.SelectedValue;
		this.hfSelectName.Value = this.tvTreasury.SelectedNode.Text;
		this.BindGridView();
		this.hfldToday.Value = DateTime.Now.ToString("yyyy-M-d");
	}
	protected void gvwStorage_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwStocktake.PageIndex = e.NewPageIndex;
		this.BindGridView();
	}
	protected void btnLock_Click(object sender, EventArgs e)
	{
		StocktakeModel editModel = this.stocktakeBll.GetEditModel(this.tvTreasury.SelectedValue);
		List<StocktakeDetailModel> byStocktakeId = this.stocktakeDetailBll.GetByStocktakeId(editModel.Id);
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (StocktakeDetailModel current in byStocktakeId)
				{
					decimal num = current.StocktakeNum - current.BookNum;
					if (num < 0m)
					{
						num = current.BookNum - current.StocktakeNum;
						List<TreasuryStockModel> listArray = this.treasuryStockBll.GetListArray(string.Concat(new object[]
						{
							" where scode='",
							current.ResourceCode,
							"' and sprice=",
							current.Price,
							" and corp='",
							current.SupplierId,
							"' and tcode='",
							editModel.TreasuryCode,
							"'  order by intime asc"
						}));
						decimal d = 0m;
						foreach (TreasuryStockModel current2 in listArray)
						{
							d += current2.snumber;
						}
						using (List<TreasuryStockModel>.Enumerator enumerator3 = listArray.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								TreasuryStockModel current3 = enumerator3.Current;
								if (current3.snumber >= num)
								{
									current3.snumber -= num;
									if (current3.snumber == 0m)
									{
										this.stocktakeDetailBll.Delete(sqlTransaction, current3.tsid);
										break;
									}
									this.stocktakeDetailBll.Update(sqlTransaction, current3);
									break;
								}
								else
								{
									if (current3.snumber < num)
									{
										num -= current3.snumber;
										this.stocktakeDetailBll.Delete(sqlTransaction, current3.tsid);
									}
								}
							}
							continue;
						}
					}
					if (num > 0m)
					{
						TreasuryStockModel treasuryStockModel = new TreasuryStockModel();
						treasuryStockModel.tsid = Guid.NewGuid().ToString();
						treasuryStockModel.scode = current.ResourceCode;
						treasuryStockModel.tcode = this.tvTreasury.SelectedValue;
						treasuryStockModel.sprice = current.Price;
						treasuryStockModel.snumber = num;
						treasuryStockModel.isfirst = false;
						treasuryStockModel.corp = current.SupplierId;
						treasuryStockModel.incode = editModel.Code;
						treasuryStockModel.intime = DateTime.Today;
						treasuryStockModel.intype = 0;
						this.stocktakeDetailBll.AddTreasuryStock(sqlTransaction, treasuryStockModel);
					}
				}
				editModel.LockDate = DateTime.Now;
				editModel.State = 2;
				this.stocktakeBll.LockStocktake(sqlTransaction, editModel);
				sqlTransaction.Commit();
				base.RegisterScript("alert('系统提示：\\n\\n锁定成功！');");
				base.RegisterScript("location='StocktakeList.aspx?tCode=" + this.tvTreasury.SelectedValue + "';");
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				throw;
			}
		}
	}
	protected void btnOverrule_Click(object sender, EventArgs e)
	{
		StocktakeModel editModel = this.stocktakeBll.GetEditModel(this.tvTreasury.SelectedValue);
		editModel.State = 0;
		this.stocktakeBll.Update(null, editModel);
		base.RegisterScript("alert('系统提示：\\n\\n驳回成功！');");
		this.BindGridView();
	}
}


