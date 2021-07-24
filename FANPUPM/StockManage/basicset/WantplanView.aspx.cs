using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_basicset_WantplanView : NBasePage, IRequiresSessionState
{
	private string ic = string.Empty;
	private string wantplanCode = string.Empty;
	private string prjId = string.Empty;
	private MaterialWantPlan materialWant = new MaterialWantPlan();
	private MeterialPlanStock materialStock = new MeterialPlanStock();
	private readonly PTPrjInfoBll ptPrjInfo = new PTPrjInfoBll();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request.QueryString["ic"]))
		{
			this.ic = base.Request.QueryString["ic"];
			this.wantplanCode = this.materialWant.GetCodeByGuid(this.ic);
		}
		if (!string.IsNullOrEmpty(base.Request.QueryString["swcode"]))
		{
			this.wantplanCode = base.Request.QueryString["swcode"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.InitPage();
			base.Response.Cache.SetNoStore();
			base.Response.Clear();
			this.setControlsValue();
			this.FileLink1.MID = 1803;
			this.FileLink1.FID = this.hfldWantPlanGuid.Value;
			this.FileLink1.Type = 1;
		}
	}
	protected void setControlsValue()
	{
		MaterialWantPlanModel model = this.materialWant.GetModel(this.wantplanCode);
		this.hfldWantPlanGuid.Value = model.swid;
		this.lblCode.Text = model.swcode;
		if (!string.IsNullOrEmpty(model.procode))
		{
			this.prjId = model.procode;
			PrjInfoModel modelByPrjGuid = this.ptPrjInfo.GetModelByPrjGuid(model.procode);
			if (modelByPrjGuid != null)
			{
				this.lblPrjName.Text = modelByPrjGuid.PrjName;
			}
			else
			{
				DataTable tableByPrjGuid = this.ptPrjInfo.GetTableByPrjGuid(model.procode);
				if (tableByPrjGuid.Rows.Count > 0)
				{
					this.lblPrjName.Text = tableByPrjGuid.Rows[0]["prjName"].ToString().Trim();
				}
			}
		}
		this.lblPerson.Text = model.person;
		this.lblBllProducer.Text = model.person;
		this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
		this.dtxtApplyDate.Text = model.intime.Replace("0:00:00", "").Trim();
		this.txtRemark.Value = model.explain;
		this.lblRemark.Text = model.explain;
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
		DataTable dataTable = this.materialStock.showMaterialListForUpdate(this.wantplanCode);
		if (dataTable != null && dataTable.Rows.Count > 0)
		{
			this.gvSmWantPlanStock.DataSource = dataTable;
			this.gvSmWantPlanStock.DataBind();
		}
		this.DataBindDiff();
		this.DataBindTaskDiff();
	}
	public void DataBindTaskDiff()
	{
		DataTable budTaskDiff = this.materialStock.GetBudTaskDiff(this.wantplanCode, this.prjId);
		this.gvwTaskDiff.DataSource = budTaskDiff;
		this.gvwTaskDiff.DataBind();
	}
	private void DataBindDiff()
	{
		MaterialWantPlanModel model = this.materialWant.GetModel(this.wantplanCode);
		DataTable diff = this.materialWant.GetDiff(model, this.hfldIsWBSRelevance.Value);
		this.gvwDiff.DataSource = diff;
		this.gvwDiff.DataBind();
	}
	public void InitPage()
	{
		if (!string.IsNullOrEmpty(this.ic))
		{
			try
			{
				PTDbsjBll pTDbsjBll = new PTDbsjBll();
				PTDbsjModel modelByGID = pTDbsjBll.GetModelByGID(this.ic);
				pTDbsjBll.Delete(modelByGID.I_DBSJ_ID);
			}
			catch
			{
			}
		}
	}
	protected void gvwDiff_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1 && !string.IsNullOrEmpty(e.Row.Cells[6].Text))
		{
			try
			{
				string text = string.Format("{0:P}", System.Convert.ToDecimal(e.Row.Cells[6].Text));
				decimal value = System.Convert.ToDecimal(e.Row.Cells[6].Text);
				string alarmLevel = ResourceAlarm.GetAlarmLevel(System.Convert.ToDecimal(value));
				foreach (TableCell tableCell in e.Row.Cells)
				{
					tableCell.ForeColor = Common2.GetColorByLevel(alarmLevel);
				}
				e.Row.Cells[6].Text = text;
			}
			catch
			{
			}
		}
	}
}


