using ASP;
using cn.justwin.Ent_Ept_EquipmentsBLL;
using cn.justwin.stockBLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_EquipmentManagement_Check_CheckView : BasePage, IRequiresSessionState
{
	public ResourceTypeManage manage = new ResourceTypeManage();
	private EquipmentsBLL BLL = new EquipmentsBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		string text = base.Request.QueryString["Guid"];
		string lXBM = base.Request.QueryString["Type"];
		PublicInterface.PTDBSJDelete(text, lXBM);
		EquipmentInfo singleEquipmentInfo = EquipmentAction.GetSingleEquipmentInfo(new Guid(text));
		DataTable list = this.manage.GetList("ResourceTypeCode='" + singleEquipmentInfo.EquipmentType + "'");
		if (list.Rows.Count > 0)
		{
			this.lblType.Text = list.Rows[0]["ResourceTypeName"].ToString();
		}
		else
		{
			this.lblType.Text = "";
		}
		this.lblcode.Text = singleEquipmentInfo.EquipmentManCode.ToString();
		this.lblName.Text = singleEquipmentInfo.EquipmentName.ToString();
		this.lblSp.Text = singleEquipmentInfo.Spec.ToString();
		this.lbljingdu.Text = singleEquipmentInfo.ThePrecision.ToString();
		this.lblmader.Text = singleEquipmentInfo.Manufacturer.ToString();
		this.lblchuchang.Text = singleEquipmentInfo.FactoryCode.ToString();
		this.lblDatechu.Text = singleEquipmentInfo.FactoryDate.ToShortDateString();
		this.lblBugDate.Text = singleEquipmentInfo.PurchaseDate.ToShortDateString();
		this.lblzhouqi.Text = singleEquipmentInfo.CheckCycle.ToString();
		this.lblyear.Text = singleEquipmentInfo.ServiceYear.ToString();
		this.lblMoney.Text = singleEquipmentInfo.OriginalPrice.ToString();
		this.lblState.Text = singleEquipmentInfo.State.ToString();
		this.lblRemark.Text = singleEquipmentInfo.Remark;
		this.lblzeju.Text = singleEquipmentInfo.depreciation.ToString();
		this.lblProject.Text = this.BLL.projectName(singleEquipmentInfo.EquipmentUniqueCode.ToString());
		try
		{
			this.lbldanwei.Text = com.jwsoft.pm.entpm.PageHelper.QueryCorp(this, int.Parse(singleEquipmentInfo.ContactDept));
		}
		catch
		{
		}
	}
}


