using AccountManage.BLL;
using AccountManage.Model;
using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using com.jwsoft.pm.entpm;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_fund_Reqinfo_fund_ReqinfoView : NBasePage, IRequiresSessionState
{
	private string action = string.Empty;
	private string contractID = string.Empty;
	private fund_ReqinfoBll fund_ReqinfoBll = new fund_ReqinfoBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.contractID = base.Request["ic"].ToString().ToLower();
			this.InitUpdateAndQuery();
		}
	}
	private void InitUpdateAndQuery()
	{
		fund_ReqinfoModle model = this.fund_ReqinfoBll.GetModel(this.contractID);
		this.txtContractCode.Text = model.reqNum;
		this.txtContractMoney.Value = ((!model.amount.HasValue) ? string.Empty : model.amount.ToString());
		this.txtStartDate.Text = ((!model.reqDate.HasValue) ? string.Empty : Convert.ToDateTime(model.reqDate).ToShortDateString());
		this.txtEndDate.Text = ((!model.useDate.HasValue) ? string.Empty : Convert.ToDateTime(model.useDate).ToShortDateString());
		this.txtMainItem.Text = model.reqCause;
		this.txtNotes.Text = model.remark;
		this.hdfusercode.Value = ((model.reqPeopNum == null) ? string.Empty : model.reqPeopNum.ToString());
		this.txtUsercode.Value = PageHelper.QueryUser(this, this.hdfusercode.Value);
		this.DroType.SelectedValue = ((!model.IsContr.HasValue) ? "0" : model.IsContr.ToString());
		if (this.DroType.SelectedValue == "0")
		{
			this.HiddenField1.Value = ((model.PrjNum == null) ? string.Empty : model.PrjNum);
			this.txtContr.Value = this.GetContrName(model.PrjNum.Trim());
			this.Pro1.Visible = false;
			this.Pro2.Visible = false;
			this.Pro3.Visible = true;
			this.Pro4.Visible = true;
		}
		else
		{
			this.hdnProjectCode.Value = ((model.PrjNum == null) ? string.Empty : model.PrjNum);
			this.txtProject.Value = this.GetProName(model.PrjNum.Trim());
			this.Pro3.Visible = false;
			this.Pro4.Visible = false;
			this.Pro1.Visible = true;
			this.Pro2.Visible = true;
		}
		this.RadioButton1.Checked = model.isInterest;
		if (this.RadioButton1.Checked)
		{
			this.isshow.Visible = true;
			this.RadioButton3.Enabled = true;
			this.RadioButton4.Enabled = true;
			this.RadioButton2.Checked = false;
		}
		else
		{
			this.isshow.Visible = false;
			this.RadioButton3.Enabled = true;
			this.RadioButton4.Enabled = true;
		}
		this.RadioButton3.Checked = model.isDefault;
		if (this.RadioButton3.Checked)
		{
			this.RadioButton4.Checked = false;
		}
		else
		{
			this.RadioButton4.Checked = true;
			this.td2.Visible = true;
			this.td1.Visible = true;
		}
		this.TextBox1.Text = ((!model.InterestNum.HasValue) ? string.Empty : Convert.ToDecimal(model.InterestNum).ToString());
	}
	protected string GetProName(string pronum)
	{
		DataTable dataTable = SqlHelper.ExecuteQuery(CommandType.Text, "select *from dbo.PT_PrjInfo where PrjGuid='" + pronum.Trim() + "'", new SqlParameter[0]);
		if (dataTable.Rows.Count == 1)
		{
			return dataTable.Rows[0]["PrjName"].ToString();
		}
		return "错误项目代码";
	}
	protected string GetContrName(string contrnum)
	{
		PayoutContract payoutContract = new PayoutContract();
		PayoutContractModel model = payoutContract.GetModel(contrnum);
		if (model != null)
		{
			return contrnum = ((model.ContractName == null) ? "无" : model.ContractName);
		}
		return "无";
	}
}


