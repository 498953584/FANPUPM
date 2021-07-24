using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutContract_PayoutTarger : NBasePage, IRequiresSessionState
{
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private string prjId = string.Empty;
	private string contractId = string.Empty;
	private string type = string.Empty;
	private string mType = string.Empty;
	private System.Collections.Generic.Dictionary<int, string> taskTypeDic;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["contractId"]))
		{
			this.contractId = base.Request["contractId"];
		}
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		if (!string.IsNullOrEmpty(base.Request["mType"]))
		{
			this.mType = base.Request["mType"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.hfldMtype.Value = this.mType;
			if (this.type == "con")
			{
				this.BindTaskByContract(this.contractId);
			}
			else
			{
				this.BindTaskByPrj(this.prjId);
			}
		}
		this.keepCheckedSatae();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			string value2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			int num = text.Length / 3;
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["orderNumber"] = text;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = value2;
			string text2 = this.gvBudget.DataKeys[e.Row.RowIndex]["ModifyType"].ToString();
			e.Row.Attributes["modifyType"] = text2;
			if (text2 == "0")
			{
				e.Row.CssClass = "tr-waring";
			}
		}
	}
	private void keepCheckedSatae()
	{
		if (this.Session["targetIds"] != null)
		{
			string text = this.Session["targetIds"].ToString();
			this.hfldSendCheckedIds.Value = text;
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
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
	private void BindTaskByPrj(string prjId)
	{
		this.gvBudget.DataSource = BudTask.GetTaskInfo2(prjId);
		this.gvBudget.DataBind();
	}
	private void BindTaskByContract(string contractId)
	{
		this.gvBudget.DataSource = BudTask.GetTaskByContract(contractId);
		this.gvBudget.DataBind();
	}
}


