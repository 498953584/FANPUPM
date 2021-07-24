using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.DAL;
using com.jwsoft.pm.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_MonthPlan_DivSelectIncometCont : NBasePage, IRequiresSessionState
{
	private string ProjectCode = string.Empty;
	private IncometContractBll incometContractBll = new IncometContractBll();
	private string bName = "";
	private DataTable dtName;

	protected override void OnInit(EventArgs e)
	{
		if (base.Request.QueryString["prjcode"] != null && base.Request.QueryString["prjcode"].ToString() != "")
		{
			this.ProjectCode = base.Request.QueryString["prjcode"].ToString();
		}
		this.gvConract.PageSize = 15;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.GetIncomeName();
			this.BindGv();
		}
	}
	private string getJs(string contracID)
	{
		string text = string.Empty;
		if (contracID != "")
		{
			string sqlString = "SELECT SUM(ClearingPrice) AS BalanceMoney FROM  dbo.Con_Incomet_Balance WHERE  (ContractID= '" + contracID + "')";
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count > 0)
			{
				text = dataTable.Rows[0]["BalanceMoney"].ToString() + "|";
			}
			sqlString = "SELECT SUM(CllectionPrice) AS PaymentMoney  FROM dbo.Con_Incomet_Payment WHERE  (ContractID= '" + contracID + "')";
			DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable2.Rows.Count > 0)
			{
				text += dataTable2.Rows[0]["PaymentMoney"].ToString();
			}
		}
		return text;
	}
	public void BindGv()
	{
		new DataTable();
		this.BindGv(Common2.GetTable("Con_Incomet_Contract", this.GetWhere()));
	}
	private string GetWhere()
	{
		string str = "where Project='" + this.ProjectCode + "' ";
		if (!string.IsNullOrEmpty(this.txtContractCode.Text))
		{
			str = str + " and contractcode like '%" + this.txtContractCode.Text.Trim() + "%'";
		}
		if (!string.IsNullOrEmpty(this.txtContractName.Text))
		{
			str = str + " and contractname like '%" + this.txtContractName.Text.Trim() + "%'   ";
		}
		if (base.Request["mpid"] != null && base.Request.QueryString["mpid"] != "")
		{
			string str2 = base.Request.QueryString["mpid"].ToString();
			str = str + " and  ContractID  not in  (SELECT ContractID  FROM [Fund_Plan_MonthDetail] where MonthPlanID ='" + str2 + "')";
		}
		str += "  AND FlowState='1' ";
		return str + " order by SignedTime desc ";
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
		if (!string.IsNullOrEmpty(this.ProjectCode))
		{
			DataRow[] array = storageDataSource.Select("Project='" + this.ProjectCode + "'");
			DataTable dataTable = new DataTable();
			dataTable = storageDataSource.Clone();
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row = array2[i];
				dataTable.ImportRow(row);
			}
			this.gvConract.DataSource = dataTable;
		}
		else
		{
			this.gvConract.DataSource = storageDataSource;
		}
		this.gvConract.DataBind();
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvConract.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				string text = this.gvConract.DataKeys[e.Row.RowIndex].Values[0].ToString();
				string text2 = this.gvConract.DataKeys[e.Row.RowIndex].Values[1].ToString();
				string text3 = this.gvConract.DataKeys[e.Row.RowIndex].Values[2].ToString();
				e.Row.Attributes["id"] = text;
				e.Row.Attributes.Add("onclick", string.Concat(new string[]
				{
					"trClick('",
					text,
					"','",
					text2,
					"','",
					text3,
					"')"
				}));
				e.Row.Attributes.Add("ondblclick", "trdbClick()");
				DataRowView arg_12C_0 = (DataRowView)e.Row.DataItem;
				e.Row.Cells[6].Text = "0.000";
				e.Row.Cells[5].Text = "0.000";
				if (text != "")
				{
					string text4 = string.Empty;
					text4 = this.getJs(text);
					string[] array = text4.Split(new char[]
					{
						'|'
					});
					if (array[0].ToString() != "")
					{
						e.Row.Cells[5].Text = string.Format("{0:###.##}", array[0].ToString());
					}
					if (array[1].ToString() != "")
					{
						e.Row.Cells[6].Text = string.Format("{0:N}", array[1]);
					}
				}
			}
			catch
			{
			}
		}
	}
	private string GetIncomeName()
	{
		string text = "select second from Con_Incomet_Contract";
		this.dtName = SqlHelper.ExecuteQuery(CommandType.Text, text.ToString(), null);
		return string.Empty;
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
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	public string GetFeedbackState(string contractId)
	{
		List<ContractPayendModel> listArray = new ContractPayendBll().GetListArray(" where contractId='" + contractId + "'");
		if (listArray.Count > 0)
		{
			return "已交底";
		}
		return "未交底";
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
}


