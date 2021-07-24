using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.DAL;
using cn.justwin.Web;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractExecution_ContractArchived : NBasePage, IRequiresSessionState
{
	private DataTable dtCount = new DataTable();
	private IncometContractBll incometContractBll = new IncometContractBll();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = 5;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			this.BindGv();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	public void BindGv()
	{
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.Append(" and IsArchived='false' and conState in (4,5) ");
		if (this.txtSignPeople.Value.Trim() != "")
		{
			stringBuilder.AppendFormat(" AND v_xm LIKE '%{0}%'", this.txtSignPeople.Value.Trim());
		}
		if (this.txtParty.Value.Trim() != "")
		{
			stringBuilder.AppendFormat(" AND p1.Party LIKE '%{0}%'", this.txtParty.Value.Trim());
		}
		if (this.hldfIsExamineApprove.Value == "1")
		{
			stringBuilder.Append(" AND p1.FlowState=1 ");
		}
		this.dtCount = this.incometContractBll.GetTbByParam(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtStartSignedTime.Text.Trim(), this.txtEndSignedTime.Text.Trim(), this.txtStartContractPrice.Text.Trim(), this.txtEndContractPrice.Text.Trim(), this.txtProject.Value.Trim(), base.UserCode, stringBuilder.ToString());
		this.AspNetPager1.RecordCount = this.dtCount.Rows.Count;
		this.BindGv(this.incometContractBll.GetTbByParamSort(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtStartSignedTime.Text.Trim(), this.txtEndSignedTime.Text.Trim(), this.txtStartContractPrice.Text.Trim(), this.txtEndContractPrice.Text.Trim(), this.txtProject.Value.Trim(), base.UserCode, stringBuilder.ToString(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize));
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
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
				CheckBox checkBox = e.Row.FindControl("cbBox") as CheckBox;
				checkBox.Attributes.Add("onclick", "Change(" + checkBox.ClientID + ")");
			}
			catch
			{
			}
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor ='#EAF4FD'");
			e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor =currentcolor");
		}
	}
	public string GetParty(string party)
	{
		if (!string.IsNullOrEmpty(party))
		{
			return party.Split(new char[]
			{
				','
			})[1];
		}
		return "";
	}
	protected void btnArchived_Click(object sender, System.EventArgs e)
	{
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
						Common2.ExecuteNoQuery(string.Concat(new object[]
						{
							"update Con_Incomet_Contract set IsArchived='true',FileTime='",
							System.DateTime.Now,
							"' where contractId='",
							checkBox.ToolTip,
							"'"
						}));
					}
				}
				base.RegisterScript("alert('系统提示：\\n\\n数据归档成功！');location='ContractArchived.aspx'");
				sqlTransaction.Commit();
				this.BindGv();
			}
			catch (System.Exception)
			{
				sqlTransaction.Rollback();
				base.RegisterScript("alert('系统提示：\\n\\n对不起归档失败！');");
			}
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.hfldPurchaseChecked.Value = "";
		this.BindGv();
	}
}


