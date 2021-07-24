using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.stockBLL;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_PlanReport_InExPlanReport : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();
	private IncometPaymentBll incometPaymentBll = new IncometPaymentBll();
	private InExPlanBLL bll = new InExPlanBLL();
	private decimal payMoney;
	private decimal incomMoney;

	public decimal PayMoney
	{
		get
		{
			return this.payMoney;
		}
		set
		{
			this.payMoney = value;
		}
	}
	public decimal IncomMoney
	{
		get
		{
			return this.incomMoney;
		}
		set
		{
			this.incomMoney = value;
		}
	}
	protected override void OnInit(EventArgs e)
	{
		this.gvConract.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public void BindGv()
	{
		this.BindGv(this.GetTable());
	}
	public DataTable GetTable()
	{
		DataTable list = this.bll.GetList(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.hdPrjCode.Value.Trim());
		list.Columns.Add("PayMoney", typeof(decimal));
		list.Columns.Add("IncomMoney", typeof(decimal));
		if (list.Rows.Count > 0)
		{
			for (int i = 0; i < list.Rows.Count; i++)
			{
				this.GetSumMony(list.Rows[i]["ID"].ToString());
				list.Rows[i]["payMoney"] = this.payMoney.ToString();
				list.Rows[i]["incomMoney"] = this.IncomMoney.ToString();
				this.payMoney = 0m;
				this.incomMoney = 0m;
			}
		}
		return list;
	}
	protected void GetSumMony(string planid)
	{
		IEPInfoBLL iEPInfoBLL = new IEPInfoBLL();
		DataTable list = iEPInfoBLL.GetList(planid);
		if (list.Rows.Count > 0)
		{
			for (int i = 0; i < list.Rows.Count; i++)
			{
				if (list.Rows[i]["type"].ToString() == "0")
				{
					this.incomMoney += Convert.ToDecimal(list.Rows[i]["Money"].ToString());
				}
				else
				{
					this.payMoney += Convert.ToDecimal(list.Rows[i]["Money"].ToString());
				}
			}
		}
	}
	public void BindGv(DataTable storageDataSource)
	{
		this.gvConract.DataSource = storageDataSource;
		this.gvConract.DataBind();
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
		string[] array = new string[]
		{
			storageDataSource.Compute("sum(PayMoney)", "").ToString(),
			storageDataSource.Compute("sum(IncomMoney)", "").ToString()
		};
		this.hdfsummony.Value = array[0];
		this.hdfIncomSum.Value = array[1];
		int[] index = new int[]
		{
			4,
			5
		};
		GridViewUtility.AddTotalRow(this.gvConract, array, index);
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
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["isMainContract"] = this.gvConract.DataKeys[e.Row.RowIndex].Values[1].ToString();
			}
			catch
			{
			}
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
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void btnexecl_Click(object sender, EventArgs e)
	{
		DataTable table = this.GetTable();
		Common2 common = new Common2();
		common.ExportToExecelAll(this.GetTitleByTable(table), this.lblTitle.Text + ".xls", base.Request.Browser.Browser);
	}
	protected void btnWord_Click(object sender, EventArgs e)
	{
		Common2 common = new Common2();
		common.ExportToWordAll(this.GetTitleByTable(this.GetTable()), this.lblTitle.Text + ".doc");
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		dt.Columns["IEPNum"].ColumnName = "计划编号";
		dt.Columns["IEPname"].ColumnName = "计划名称";
		dt.Columns["SumMony"].ColumnName = "金额";
		dt.Columns["planType"].ColumnName = "计划类型";
		dt.Columns["prjName"].ColumnName = "项目名称";
		dt.Columns["IEPdate"].ColumnName = "签约时间";
		dt.Columns.Remove(dt.Columns["ID"]);
		dt.Columns.Remove(dt.Columns["state"]);
		dt.Columns.Remove(dt.Columns["IEPtype"]);
		dt.Columns.Remove(dt.Columns["prjNum"]);
		DataRow dataRow = dt.NewRow();
		dataRow[4] = "合计";
		dataRow[5] = Convert.ToDecimal((this.hdfsummony.Value == "") ? "0" : this.hdfsummony.Value);
		dataRow[5] = Convert.ToDecimal((this.hdfIncomSum.Value == "") ? "0" : this.hdfIncomSum.Value);
		dt.Rows.Add(dataRow);
		return dt;
	}
}


