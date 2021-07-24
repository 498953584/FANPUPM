using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_SmOutReserve_ViewReserve : NBasePage, IRequiresSessionState
{
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();
	private Treasury treasury = new Treasury();
	private OutReserveBll outReserveBll = new OutReserveBll();
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
		OutReserveModel model = null;
		if (base.Request.QueryString["id"] != null && base.Request.QueryString["ic"] == null)
		{
			model = this.outReserveBll.GetModel(base.Request.QueryString["id"]);
		}
		else
		{
			if (base.Request.QueryString["ic"] != null)
			{
				model = this.outReserveBll.GetModelByIc(base.Request.QueryString["ic"]);
			}
		}
		this.lblExplain.Text = model.explain;
		this.lblInTime.Text = Common2.GetTime(model.intime.ToString());
		this.lblPeople.Text = model.person;
		this.lblPPCode.Text = model.orcode;
		this.lblPickingPeople.Text = model.PickingPeople;
		PTdbmService source = new PTdbmService();
		PTdbm pTdbm = (
			from dbm in source
			where dbm.V_bmbm == model.PickingSector && dbm.C_sfyx == "y"
			select dbm).FirstOrDefault<PTdbm>();
		if (pTdbm != null)
		{
			this.lblPickingSector.Text = pTdbm.V_BMMC;
		}
		else
		{
			this.lblPickingSector.Text = model.PickingSector;
		}
		PrjInfoModel modelByPrjGuid = this.pTPrjInfoBll.GetModelByPrjGuid(model.procode);
		if (modelByPrjGuid != null)
		{
			this.lblProjectName.Text = modelByPrjGuid.PrjName;
		}
		else
		{
			DataTable tableByPrjGuid = this.pTPrjInfoBll.GetTableByPrjGuid(model.procode);
			if (tableByPrjGuid.Rows.Count > 0)
			{
				this.lblProjectName.Text = tableByPrjGuid.Rows[0]["prjName"].ToString().Trim();
			}
		}
		this.lblBllProducer.Text = model.person;
		this.lblPrintDate.Text = DateTime.Now.ToShortDateString();
		this.hdnProjectCode.Value = model.procode;
		this.hdGuid.Value = model.orid;
		this.hdflowstate.Value = model.flowstate.ToString();
		this.lblTerasuryName.Text = this.treasury.GetModel(model.tcode).tname;
		string equipmentId = model.EquipmentId;
		if (!string.IsNullOrEmpty(equipmentId))
		{
			EquEquipmentService equEquipmentService = new EquEquipmentService();
			EquEquipment byId = equEquipmentService.GetById(equipmentId);
			if (byId != null)
			{
				this.lblEquCode.Text = byId.EquCode;
			}
		}
		List<OutStockModel> listArray = this.outStockBll.GetListArray(" where orcode='" + model.orcode + "'");
		string text = "";
		foreach (OutStockModel current in listArray)
		{
			text = text + "'" + current.scode + "',";
		}
		if (text != "")
		{
			text = text.Substring(0, text.Length - 1);
		}
		this.ViewState["DataTable"] = this.outStockBll.GetTableByOrcode(model.orcode, model.tcode);
		this.BindGv();
		this.FileLink1.MID = 1804;
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
}


