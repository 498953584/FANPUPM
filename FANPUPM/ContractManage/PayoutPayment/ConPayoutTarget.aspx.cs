using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutPayment_ConPayoutTarget : NBasePage, IRequiresSessionState
{
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			if (!string.IsNullOrEmpty(base.Request["contractId"]))
			{
				this.bindConTarget(base.Request["contractId"]);
			}
		}
	}
	protected void bindConTarget(string contractId)
	{
		PaymentTarget paymentTarget = new PaymentTarget();
		DataTable conTarget = paymentTarget.GetConTarget(contractId, this.hfldIsWBSRelevance.Value.Trim());
		this.gvBudget.DataSource = conTarget;
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void keepCheckedSatae()
	{
		if (this.Session["targetIds"] != null)
		{
			string text = this.Session["targetIds"].ToString();
			this.hfldSendCheckedIds.Value = text;
			List<string> list = new List<string>();
			if (text.Contains("["))
			{
				list = JsonHelper.GetListFromJson(text);
			}
			else
			{
				list.Add(text);
			}
			foreach (GridViewRow gridViewRow in this.gvBudget.Rows)
			{
				CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
				foreach (string current in list)
				{
					if (checkBox.ToolTip == current)
					{
						checkBox.Checked = true;
						break;
					}
				}
			}
		}
	}
}


