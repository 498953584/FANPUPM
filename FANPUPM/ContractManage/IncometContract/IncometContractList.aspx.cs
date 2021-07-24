using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_IncometContract_IncometContractList : NBasePage, IRequiresSessionState
{
	private DataTable dtCount = new DataTable();
	private IncometContractBll incometContractBll = new IncometContractBll();
	private BudContractConsReportService rptSev = new BudContractConsReportService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			this.BindGv();
			if (this.hldfIsExamineApprove.Value == "0")
			{
				this.gvConract.Columns[15].Visible = false;
			}
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	public void BindGv()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.Append(" and IsArchived='false' ");
		if (this.txtSignPeople.Value.Trim() != "")
		{
			stringBuilder.AppendFormat("AND v_xm LIKE '%{0}%'", this.Replace(this.txtSignPeople.Value.Trim()));
		}
		if (this.txtParty.Value.Trim() != "")
		{
			stringBuilder.AppendFormat("AND Party LIKE '%{0}%'", this.Replace(this.txtParty.Value.Trim()));
		}
		if (this.txtCParty.Text.Trim() != "")
		{
			stringBuilder.AppendFormat("AND CParty LIKE '%{0}%'", this.Replace(this.txtCParty.Text.Trim()));
		}
		this.dtCount = this.incometContractBll.GetTbByParam(this.Replace(this.txtContractCode.Text.Trim()), this.Replace(this.txtContractName.Text.Trim()), this.Replace(this.txtConType.Text.Trim()), this.txtStartSignedTime.Text.Trim(), this.txtEndSignedTime.Text.Trim(), this.txtStartContractPrice.Text.Trim(), this.txtEndContractPrice.Text.Trim(), this.txtProject.Text.Trim(), base.UserCode, stringBuilder.ToString());
		this.AspNetPager1.RecordCount = this.dtCount.Rows.Count;
		this.BindGv(this.incometContractBll.GetTbByParamSort(this.Replace(this.txtContractCode.Text.Trim()), this.Replace(this.txtContractName.Text.Trim()), this.Replace(this.txtConType.Text.Trim()), this.txtStartSignedTime.Text.Trim(), this.txtEndSignedTime.Text.Trim(), this.txtStartContractPrice.Text.Trim(), this.txtEndContractPrice.Text.Trim(), this.txtProject.Text.Trim(), base.UserCode, stringBuilder.ToString(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize));
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
		decimal d = 0m;
		for (int i = 0; i < this.dtCount.Rows.Count; i++)
		{
			d += System.Convert.ToDecimal(WebUtil.GetEnPrice(this.dtCount.Rows[i]["ContractPrice"].ToString(), this.dtCount.Rows[i]["ContractId"].ToString()));
		}
		string[] value = new string[]
		{
			d.ToString()
		};
		int[] index = new int[]
		{
			4
		};
		GridViewUtility.AddTotalRow(this.gvConract, value, index);
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		ConIncometContractService conIncometContractService = new ConIncometContractService();
		BudContractTaskService budContractTaskService = new BudContractTaskService();
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				string message = "";
				System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
				foreach (GridViewRow gridViewRow in this.gvConract.Rows)
				{
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (checkBox != null && checkBox.Checked)
					{
						if (!this.incometContractBll.IsDel(checkBox.ToolTip))
						{
							list.Clear();
							message = "alert('系统提示：\\n\\n请先删除与收款合同相关的其他数据！')";
							break;
						}
						ConIncometContract byContractId = conIncometContractService.GetByContractId(checkBox.ToolTip);
						budContractTaskService.DelRalationBudgetAndContract(byContractId.Project, checkBox.ToolTip);
						this.rptSev.DelByContractId(checkBox.ToolTip);
						list.Add(checkBox.ToolTip);
						message = "alert('系统提示：\\n\\n数据删除成功！');location='IncometContractList.aspx'";
					}
				}
				Common2.DelByStrWhere("Con_Incomet_Contract", " where ContractId in (" + StringUtility.GetArrayToInStr(list.ToArray()) + ")");
				ConConfigContractService conConfigContractService = new ConConfigContractService();
				conConfigContractService.Deltes(list);
				base.RegisterScript(message);
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (System.Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起删除失败！');");
			}
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["prjGuid"] = this.gvConract.DataKeys[e.Row.RowIndex].Values[3].ToString();
				if (this.gvConract.DataKeys[e.Row.RowIndex].Values[1].ToString() == "False")
				{
					e.Row.Attributes["bstate"] = "False";
				}
				e.Row.Attributes["isMainContract"] = this.gvConract.DataKeys[e.Row.RowIndex].Values[1].ToString();
				e.Row.Attributes["FlowState"] = this.gvConract.DataKeys[e.Row.RowIndex].Values[2].ToString();
			}
			catch
			{
			}
		}
	}
	public string GetParty(string party)
	{
		PayoutContract payoutContract = new PayoutContract();
		DataTable dataTable = new DataTable();
		string result = string.Empty;
		if (!string.IsNullOrEmpty(party))
		{
			dataTable = payoutContract.GetBName(party.Split(new char[]
			{
				','
			})[0]);
		}
		if (dataTable.Rows.Count > 0)
		{
			result = dataTable.Rows[0]["CorpName"].ToString();
		}
		return result;
	}
	public string Replace(string sourceStr)
	{
		sourceStr = sourceStr.Replace("'", "''");
		sourceStr = sourceStr.Replace("%", "[%]");
		return sourceStr;
	}
}


