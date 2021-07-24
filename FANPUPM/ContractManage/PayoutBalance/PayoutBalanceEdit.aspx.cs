using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutBalance_PayoutBalanceEdit : NBasePage, IRequiresSessionState
{
	private string contractID = string.Empty;
	protected PayoutContract payoutContract = new PayoutContract();
	private PayoutBalance payoutBalance = new PayoutBalance();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ContractID"]))
		{
			this.contractID = base.Request["ContractID"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdContractID.Value = this.contractID;
			PayoutContractModel model = this.payoutContract.GetModel(this.contractID);
			if (model != null)
			{
				if (model.FlowState == 1)
				{
					this.btnAdd.Enabled = true;
				}
				else
				{
					this.btnAdd.Enabled = false;
				}
			}
			string text = " Con_Payout_Contract.IsArchived = 0 ";
			text += string.Format(" AND Con_Payout_Contract.ContractID = '{0}' ", this.contractID);
			List<PayoutBalanceModel> list = this.payoutBalance.GetList(text, base.UserCode);
			this.gvwConract.DataSource = list;
			this.gvwConract.DataBind();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		if (this.hfldBalanceId.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldBalanceId.Value);
		}
		else
		{
			list.Add(this.hfldBalanceId.Value);
		}
		try
		{
			this.payoutBalance.Delete(list);
			base.RegisterScript("window.location = window.location;");
		}
		catch (Exception)
		{
			base.RegisterScript("alert('系统提示：\n\n删除失败！')");
		}
	}
	protected void gvwConract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwConract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwConract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjGuid"] = this.gvwConract.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["auditContent"] = "支出合同结算：" + this.gvwConract.DataKeys[e.Row.RowIndex].Values[2].ToString();
		}
	}
}


