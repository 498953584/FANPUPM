using ASP;
using cn.justwin.BLL;
using cn.justwin.contractDAL;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_InsuranceAndReview_Edit : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string IARGuid = string.Empty;
	private PayoutContract payoutContract = new PayoutContract();
	private TypeAndStateBll typeBLL = new TypeAndStateBll();
	private MainBLL BLL = new MainBLL();
	private InsuranceAndReviewBLL IARBLL = new InsuranceAndReviewBLL();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["IARGuid"]))
		{
			this.IARGuid = base.Request["IARGuid"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.initData();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		InsuranceAndReviewModel insuranceAndReviewModel = new InsuranceAndReviewModel();
		insuranceAndReviewModel.VehicleCode = new Guid(this.hdnVehiclesCode.Value);
		insuranceAndReviewModel.Type = new int?(int.Parse(this.rado.SelectedValue));
		insuranceAndReviewModel.Date = this.txtDate.Text.Trim().ToString();
		insuranceAndReviewModel.code = this.txtcode.Text.ToString();
		insuranceAndReviewModel.Guid = new Guid(this.hideIARGUID.Value.ToString());
		if (this.IARBLL.Update(insuranceAndReviewModel))
		{
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_InsuranceAndReview' })");
		}
	}
	private void initData()
	{
		if (this.IARGuid != "")
		{
			InsuranceAndReviewModel model = this.IARBLL.GetModel(new Guid(this.IARGuid));
			if (model != null)
			{
				this.hideIARGUID.Value = model.Guid.ToString();
				this.txtcode.Text = model.code.ToString();
				this.txtcode.Enabled = false;
				this.txtDate.Text = model.Date.ToString().Substring(0, model.Date.ToString().LastIndexOf(" ") + 1);
				if (model.Type == 0)
				{
					this.rado.Items[0].Selected = true;
				}
				else
				{
					this.rado.Items[1].Selected = true;
				}
				if (model.VehicleCode.ToString() != "")
				{
					this.hdnVehiclesCode.Value = model.VehicleCode.ToString();
					if (this.BLL.Exists(new Guid(model.VehicleCode.ToString())))
					{
						this.txtVehicleCode.Text = this.BLL.GetModel(new Guid(model.VehicleCode.ToString())).VehicleCode;
					}
				}
			}
		}
	}
}


