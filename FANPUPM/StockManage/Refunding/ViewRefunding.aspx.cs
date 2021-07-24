using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Refunding_ViewRefunding : NBasePage, IRequiresSessionState
{
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private Treasury treasury = new Treasury();
	private BackStockBll backStockBll = new BackStockBll();
	private RefundingBll refundingBll = new RefundingBll();
	private OutStockBll outStockBll = new OutStockBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		RefundingModel refundingModel = null;
		if (base.Request.QueryString["id"] != null && base.Request.QueryString["ic"] == null)
		{
			refundingModel = this.refundingBll.GetModel(base.Request.QueryString["id"]);
		}
		else
		{
			if (base.Request.QueryString["ic"] != null)
			{
				refundingModel = this.refundingBll.GetModelByIc(base.Request.QueryString["ic"]);
			}
		}
		this.lblExplain.Text = refundingModel.explain;
		this.lblInTime.Text = Common2.GetTime(refundingModel.intime.ToString());
		this.lblPeople.Text = refundingModel.person;
		this.lblPPCode.Text = refundingModel.rcode;
		PrjInfoModel modelByPrjGuid = this.pTPrjInfoBll.GetModelByPrjGuid(refundingModel.procode);
		if (modelByPrjGuid != null)
		{
			this.lblProjectName.Text = modelByPrjGuid.PrjName;
		}
		else
		{
			DataTable tableByPrjGuid = this.pTPrjInfoBll.GetTableByPrjGuid(refundingModel.procode);
			if (tableByPrjGuid.Rows.Count > 0)
			{
				this.lblProjectName.Text = tableByPrjGuid.Rows[0]["prjName"].ToString().Trim();
			}
		}
		this.lblBllProducer.Text = refundingModel.person;
		this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
		this.hdnProjectCode.Value = refundingModel.procode;
		this.hdflowstate.Value = refundingModel.flowstate.ToString();
		this.hdGuid.Value = refundingModel.rid;
		this.lblTreasuryName.Text = this.treasury.GetModel(refundingModel.tcode).tname;
		this.ViewState["DataTable"] = this.backStockBll.GetTableByRcode(refundingModel.rcode);
		this.BindGv();
		this.FileLink1.MID = 1805;
		this.FileLink1.FID = this.hdGuid.Value;
		this.FileLink1.Type = 1;
	}
	public void BindGv()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		if (dataTable.Rows.Count == 0)
		{
			dataTable.Rows.Add(dataTable.NewRow());
			this.gvNeedNote.DataSource = dataTable;
			this.gvNeedNote.DataBind();
			this.gvNeedNote.HeaderRow.Cells[0].Visible = false;
			this.gvNeedNote.Rows[0].Visible = false;
			return;
		}
		this.gvNeedNote.DataSource = dataTable;
		this.gvNeedNote.DataBind();
		string total = Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
		GridViewUtility.AddTotalRow(this.gvNeedNote, total, 8);
	}
	protected void gvNeedNote_RowDataBound(object sender, GridViewRowEventArgs e)
	{
	}
	public string GetNumber(string num, string orcode, string sprice, string scode, string corp)
	{
		if (string.IsNullOrEmpty(num) || string.IsNullOrEmpty(sprice))
		{
			return "";
		}
		string numByParam = this.backStockBll.GetNumByParam(orcode, Convert.ToDecimal(sprice), scode, corp, this.lblPPCode.Text, "-2");
		if (string.IsNullOrEmpty(numByParam))
		{
			return num.ToString();
		}
		return Convert.ToString(Convert.ToDecimal(num) - Convert.ToDecimal(numByParam));
	}
}


