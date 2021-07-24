using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutModify_PayoutModifyEdit : NBasePage, IRequiresSessionState
{
	private string contractID = string.Empty;
	protected PayoutContract payoutContract = new PayoutContract();
	private PayoutModify payoutModify = new PayoutModify();
	private ConPayoutModifyService payOutSer = new ConPayoutModifyService();
	private BudModifyService modifySer = new BudModifyService();
	private BudTaskService budTaskSer = new BudTaskService();
	private BudModifyTaskService budModifyTaskSer = new BudModifyTaskService();

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
			List<PayoutModifyModel> list = this.payoutModify.GetList(text, base.UserCode);
			this.gvwContract.DataSource = list;
			this.gvwContract.DataBind();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldModifyId.Value);
		foreach (string modifyId in listFromJson)
		{
			ConPayoutModify payOutModify = (
				from r in this.payOutSer
				where r.ModifyID == modifyId
				select r).FirstOrDefault<ConPayoutModify>();
			BudModify budModify = (
				from r in this.modifySer
				where r.ModifyId == payOutModify.BudModifyId
				select r).FirstOrDefault<BudModify>();
			if (budModify != null)
			{
				List<BudTask> list = (
					from r in this.budTaskSer
					where r.ModifyId == budModify.ModifyId
					select r).ToList<BudTask>();
				foreach (BudTask model in list)
				{
					BudModifyTask budModifyTask = (
						from r in this.budModifyTaskSer
						where r.TaskId == model.TaskId
						select r).FirstOrDefault<BudModifyTask>();
					if (budModifyTask.ModifyType == 0)
					{
						this.budTaskSer.Delete(model);
						this.budModifyTaskSer.UpdateTotal2(budModifyTask.ModifyTaskId);
						this.budModifyTaskSer.Delete(budModifyTask);
					}
					else
					{
						model.ModifyId = budModifyTask.ModifyId;
						model.Total2 -= budModifyTask.Total2;
						model.Quantity -= budModifyTask.Quantity;
						if (model.Quantity != 0m)
						{
							model.UnitPrice = model.Total2 / model.Quantity;
						}
						this.budTaskSer.Update(model);
						this.budModifyTaskSer.UpdateTotal2(budModifyTask.ModifyTaskId);
						this.budModifyTaskSer.Delete(budModifyTask);
					}
				}
				this.modifySer.Delete(budModify);
			}
		}
		try
		{
			foreach (string current in listFromJson)
			{
				SelfEventAction.SuperDelete(current, "Con_Payout_Modify", "FlowState");
			}
			this.payoutModify.Delete(listFromJson);
			base.RegisterScript("window.location = window.location;");
		}
		catch (Exception)
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


