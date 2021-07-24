using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometModify_ShowModifyList : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();
	private IncometModifyBll incometModifyBll = new IncometModifyBll();
	private BudConModifyTaskService modifyTaskSev = new BudConModifyTaskService();

	protected override void OnInit(EventArgs e)
	{
		this.gvConract.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdContractID.Value = base.Request.QueryString["ContractID"];
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			this.BindGv();
		}
	}
	public void BindGv()
	{
		if (this.hldfIsExamineApprove.Value == "0")
		{
			this.BindGv(this.incometContractBll.GetInfoByParam(this.hdContractID.Value, "Con_Incomet_Modify", base.UserCode, ""));
			return;
		}
		this.BindGv(this.incometContractBll.GetInfoByParam(this.hdContractID.Value, "Con_Incomet_Modify", base.UserCode, "AND FlowState=1"));
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvConract.DataSource = storageDataSource;
			this.gvConract.DataBind();
			this.gvConract.HeaderRow.Cells[0].Visible = false;
			this.gvConract.Rows[0].Visible = false;
			return;
		}
		this.gvConract.DataSource = storageDataSource;
		this.gvConract.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		BudConModifyService budConModifyService = new BudConModifyService();
		BudConModifyTaskService budConModifyTaskService = new BudConModifyTaskService();
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		List<string> list = new List<string>();
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				foreach (GridViewRow gridViewRow in this.gvConract.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						int num = this.incometModifyBll.Delete(sqlTransaction, checkBox.ToolTip);
						if (num > 0)
						{
							BudConModify budConModify = budConModifyService.GetByConInModifyID(checkBox.ToolTip);
							if (budConModify != null)
							{
								this.DeleteInModifyTaskAndMeasure(budConModify.ModifyId);
								List<string> list2 = (
									from r in budConModifyTaskService
									where r.ModifyId == budConModify.ModifyId
									select r.TaskId).ToList<string>();
								foreach (string taskId in list2)
								{
									BudContractTask budContractTask = (
										from r in budContractTaskService
										where r.TaskId == taskId
										select r).FirstOrDefault<BudContractTask>();
									BudConModifyTask budConModifyTask = (
										from r in budConModifyTaskService
										where r.TaskId == taskId && r.ModifyId == budConModify.ModifyId
										select r).FirstOrDefault<BudConModifyTask>();
									if (budContractTask != null)
									{
										if (budContractTask.ModifyType == "0")
										{
											budContractTaskService.Delete(budContractTask);
										}
										else
										{
											budContractTask.EndDate = budConModifyTask.EndDate;
											budContractTask.StartDate = budConModifyTask.StartDate;
											budContractTask.TaskCode = budConModifyTask.ModifyTaskCode;
											budContractTask.TaskName = budConModifyTask.ModifyTaskContent;
											budContractTask.Unit = budConModifyTask.Unit;
											budContractTask.Total = new decimal?(Convert.ToDecimal(budContractTask.Total) - budConModifyTask.Total);
											budContractTask.InputDate = DateTime.Now;
											budContractTask.IsValid = new bool?(false);
											budContractTask.Note = budConModifyTask.Note;
											budContractTask.OrderNumber = budConModifyTask.OrderNumber;
											budContractTask.ParentId = budConModifyTask.ParentId;
											budContractTask.PrjId = budConModifyTask.PrjId2;
											budContractTask.Quantity = Convert.ToDecimal(budContractTask.Quantity) - budConModifyTask.Quantity;
											budContractTask.TaskId = budConModifyTask.TaskId;
											budContractTask.ModifyId = null;
											budContractTask.ModifyType = "1";
											if (budContractTask.Quantity != 0m)
											{
												budContractTask.UnitPrice = budContractTask.Total / budContractTask.Quantity;
											}
											budContractTask.TaskType = string.Empty;
											budContractTask.InputUser = PageHelper.QueryUser(this, base.UserCode);
											budContractTask.FeatureDescription = budConModifyTask.FeatureDescription;
											budContractTask.ConstructionPeriod = budConModifyTask.ConstructionPeriod;
											budContractTask.MainMaterial = new decimal?(Convert.ToDecimal(budContractTask.MainMaterial) - Convert.ToDecimal(budConModifyTask.MainMaterial));
											budContractTask.Labor = new decimal?(Convert.ToDecimal(budContractTask.Labor) - Convert.ToDecimal(budConModifyTask.Labor));
											budContractTask.SubMaterial = new decimal?(Convert.ToDecimal(budContractTask.SubMaterial) - Convert.ToDecimal(budConModifyTask.SubMaterial));
											budContractTaskService.Update(budContractTask);
										}
									}
								}
								budConModifyTaskService.DelModifyTask(budConModify.ModifyId);
								list.Add(budConModify.ModifyId);
								budConModifyService.Delete(list);
							}
							base.RegisterScript("top.ui.show('数据删除成功！');");
							base.RegisterScript("window.location = window.location");
						}
					}
				}
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起添加失败！');");
			}
		}
	}
	public void DeleteInModifyTaskAndMeasure(string modifyId)
	{
		List<BudConModifyTask> listByModifyId = this.modifyTaskSev.GetListByModifyId(modifyId);
		foreach (BudConModifyTask current in listByModifyId)
		{
			if (current.ModifyType == 0)
			{
				this.DeleteTaskAndMeasure(current.ModifyTaskId);
			}
		}
	}
	public void DeleteTaskAndMeasure(string taskId)
	{
		SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE bud_conModifyTask WHERE TaskId='" + taskId + "'", null);
		SqlHelper.ExecuteNonQuery(CommandType.Text, "DELETE Bud_ContractConsTask WHERE TaskId='" + taskId + "'", null);
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvConract.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
}


