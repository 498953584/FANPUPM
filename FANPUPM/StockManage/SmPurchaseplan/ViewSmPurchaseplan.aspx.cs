using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_SmPurchaseplan_ViewSmPurchaseplan : NBasePage, IRequiresSessionState
{
	private SmPurchaseplanBll smPurchaseplanBll = new SmPurchaseplanBll();
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private SmPurchaseplanStockBll smPurchaseplanStockBll = new SmPurchaseplanStockBll();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.InitPage();
		}
	}
	public void InitPage()
	{
		SmPurchaseplanModel smPurchaseplanModel = null;
		if (base.Request.QueryString["id"] != null && base.Request.QueryString["ic"] == null)
		{
			smPurchaseplanModel = this.smPurchaseplanBll.GetModel(base.Request.QueryString["id"]);
		}
		else
		{
			if (base.Request.QueryString["ic"] != null)
			{
				smPurchaseplanModel = this.smPurchaseplanBll.GetModelByCid(base.Request.QueryString["ic"]);
			}
		}
		if (smPurchaseplanModel != null)
		{
			this.lblExplain.Text = smPurchaseplanModel.explain;
			this.lblInTime.Text = Common2.GetTime(smPurchaseplanModel.intime.ToString());
			this.lblPeople.Text = smPurchaseplanModel.person;
			this.lblBllProducer.Text = smPurchaseplanModel.person;
			this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
			this.lblPPCode.Text = smPurchaseplanModel.ppcode;
			PrjInfoModel modelByPrjGuid = this.pTPrjInfoBll.GetModelByPrjGuid(smPurchaseplanModel.Project);
			if (modelByPrjGuid != null)
			{
				this.lblProjectName.Text = modelByPrjGuid.PrjName;
			}
			else
			{
				DataTable dataTable = new DataTable();
				dataTable = this.pTPrjInfoBll.GetTableByPrjGuid(smPurchaseplanModel.Project);
				if (dataTable.Rows.Count > 0)
				{
					this.lblProjectName.Text = dataTable.Rows[0]["prjName"].ToString().Trim();
				}
			}
			this.hdflowstate.Value = smPurchaseplanModel.flowstate.ToString();
			this.hdGuid.Value = smPurchaseplanModel.ppid;
			string[] arrPpcode = new string[]
			{
				smPurchaseplanModel.ppcode
			};
			string project = smPurchaseplanModel.Project;
			this.ViewState["DataTable"] = this.smPurchaseplanStockBll.GetResourceByPpcodes(arrPpcode, project, this.hfldIsWBSRelevance.Value);
			this.BindGv();
			this.FileLink1.MID = 1756;
			this.FileLink1.FID = this.hdGuid.Value;
			this.FileLink1.Type = 1;
			return;
		}
		this.FileLink1.MID = 1756;
		this.FileLink1.FID = "";
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
	}
}


