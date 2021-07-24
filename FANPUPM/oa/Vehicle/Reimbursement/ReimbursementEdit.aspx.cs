using ASP;
using cn.justwin.BLL;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using com.jwsoft.web.WebControls;
using System;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_Reimbursement_ReimbursementEdit : NBasePage, IRequiresSessionState
{
	private Reimbursement rei;
	private ReimbursementBLL reiBll = new ReimbursementBLL();
	private MainBLL mainBll = new MainBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request.QueryString["type"]))
		{
			if (base.Request.QueryString["type"] == "add")
			{
				this.lblTitle.Text = "新增司机报销单";
				this.txtcode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
				this.txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.hdnGuid.Value = Guid.NewGuid().ToString();
				return;
			}
			if (base.Request.QueryString["type"] == "edit")
			{
				this.lblTitle.Text = "修改司机报销单";
				this.rei = this.reiBll.GetModel(new Guid(base.Request.QueryString["id"]));
				this.txtcode.Text = this.rei.ReimbursementCode;
				this.txtDate.Text = Convert.ToDateTime(this.rei.Date).ToString("yyyy-MM-dd");
				this.txtVehicleCode.Value = this.mainBll.GetCodeByguid(this.rei.VehicleGuid);
				this.txtDestination.Text = this.rei.Destination;
				this.txtPeople.Value = this.rei.UserName;
				this.txtFuelcosts.Text = this.rei.FuelCosts.ToString();
				this.txtMaintenancecosts.Text = this.rei.MaintenanceCosts.ToString();
				this.txtTolls.Text = this.rei.Tolls.ToString();
				this.txtRemark.Text = this.rei.Remark;
				this.txtRepairs.Text = this.rei.Repairs.ToString();
				this.hdnGuid.Value = this.rei.Guid.ToString();
				this.hdnVehicleGuid.Value = this.rei.VehicleGuid.ToString();
				return;
			}
			if (base.Request.QueryString["type"] == "view")
			{
				this.lblTitle.Text = "查看司机报销单";
				this.rei = this.reiBll.GetModel(new Guid(base.Request.QueryString["id"]));
				this.txtcode.Text = this.rei.ReimbursementCode;
				this.txtDate.Text = Convert.ToDateTime(this.rei.Date).ToString("yyyy-MM-dd");
				this.txtVehicleCode.Value = this.mainBll.GetCodeByguid(this.rei.VehicleGuid);
				this.txtDestination.Text = this.rei.Destination;
				this.txtPeople.Value = this.rei.UserName;
				this.txtFuelcosts.Text = this.rei.FuelCosts.ToString();
				this.txtMaintenancecosts.Text = this.rei.MaintenanceCosts.ToString();
				this.txtTolls.Text = this.rei.Tolls.ToString();
				this.txtRemark.Text = this.rei.Remark;
				this.txtRepairs.Text = this.rei.Repairs.ToString();
				this.txtcode.Enabled = false;
				this.txtDate.Enabled = false;
				this.txtDestination.Enabled = false;
				this.txtFuelcosts.Enabled = false;
				this.txtMaintenancecosts.Enabled = false;
				this.txtPeople.Disabled = true;
				this.txtRemark.Enabled = false;
				this.txtRepairs.Enabled = false;
				this.txtTolls.Enabled = false;
				this.txtVehicleCode.Disabled = true;
				this.btnSave.Enabled = false;
				this.Img1.Visible = false;
				this.imgPrj.Visible = false;
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		this.rei = new Reimbursement();
		this.rei.Guid = new Guid(this.hdnGuid.Value);
		this.rei.VehicleGuid = new Guid(this.hdnVehicleGuid.Value);
		this.rei.ReimbursementCode = this.txtcode.Text;
		this.txtDate.Text.ToString();
		this.rei.Date = new DateTime?(Convert.ToDateTime(this.txtDate.Text.ToString()));
		this.rei.Destination = this.txtDestination.Text;
		this.rei.UserName = this.txtPeople.Value;
		if (string.IsNullOrEmpty(this.txtFuelcosts.Text))
		{
			this.rei.FuelCosts = new decimal?(0.00m);
		}
		else
		{
			this.rei.FuelCosts = new decimal?(Convert.ToDecimal(this.txtFuelcosts.Text));
		}
		if (string.IsNullOrEmpty(this.txtMaintenancecosts.Text))
		{
			this.rei.MaintenanceCosts = new decimal?(0.00m);
		}
		else
		{
			this.rei.MaintenanceCosts = new decimal?(Convert.ToDecimal(this.txtMaintenancecosts.Text));
		}
		if (string.IsNullOrEmpty(this.txtTolls.Text))
		{
			this.rei.Tolls = new decimal?(0.00m);
		}
		else
		{
			this.rei.Tolls = new decimal?(Convert.ToDecimal(this.txtTolls.Text));
		}
		this.rei.Remark = this.txtRemark.Text;
		if (string.IsNullOrEmpty(this.txtRepairs.Text))
		{
			this.rei.Repairs = new decimal?(0.00m);
		}
		else
		{
			this.rei.Repairs = new decimal?(Convert.ToDecimal(this.txtRepairs.Text));
		}
		bool flag = false;
		if (this.reiBll.Exists(this.rei.Guid))
		{
			if (this.reiBll.Update(this.rei))
			{
				flag = true;
			}
		}
		else
		{
			if (this.reiBll.Add(this.rei))
			{
				flag = true;
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (flag)
		{
			stringBuilder.Append("top.ui.tabSuccess({ parentName: '_ReimbursementManage'}); \n");
		}
		else
		{
			stringBuilder.Append("top.ui.alert('保存失败');");
		}
		base.RegisterScript(stringBuilder.ToString());
	}
}


