using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_IncometContract_Permission : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();

	protected override void OnInit(System.EventArgs e)
	{
		this.gvConract.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
			if (this.hldfIsExamineApprove.Value == "0")
			{
				this.gvConract.Columns[15].Visible = false;
			}
		}
	}
	public void BindGv()
	{
		this.AspNetPager1.RecordCount = this.incometContractBll.GetCountByParam(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtStartSignedTime.Text.Trim(), this.txtEndSignedTime.Text.Trim(), this.txtStartContractPrice.Text.Trim(), this.txtEndContractPrice.Text.Trim(), this.txtProject.Text.Trim(), this.txtSignPeople.Value.Trim(), this.txtParty.Value.Trim());
		DataTable tbByParam = this.incometContractBll.GetTbByParam(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtStartSignedTime.Text.Trim(), this.txtEndSignedTime.Text.Trim(), this.txtStartContractPrice.Text.Trim(), this.txtEndContractPrice.Text.Trim(), this.txtProject.Text.Trim(), this.txtSignPeople.Value.Trim(), this.txtParty.Value.Trim(), this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.BindGv(tbByParam);
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
		for (int i = 0; i < storageDataSource.Rows.Count; i++)
		{
			d += System.Convert.ToDecimal(WebUtil.GetEnPrice(storageDataSource.Rows[i]["ContractPrice"].ToString(), storageDataSource.Rows[i]["ContractId"].ToString()));
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
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvConract.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
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
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


