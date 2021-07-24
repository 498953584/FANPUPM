using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_DivSelectIncometCont : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();

	protected override void OnInit(System.EventArgs e)
	{
		this.gvConract.PageSize = 15;
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
	public void BindGv()
	{
		this.BindGv(this.incometContractBll.GetSelectContract(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), base.UserCode, this.hldfIsExamineApprove.Value.Trim()));
	}
	private string GetWhere()
	{
		string text = "where 1=1 ";
		if (!string.IsNullOrEmpty(this.txtContractCode.Text))
		{
			text = text + " and contractcode like '%" + this.txtContractCode.Text + "%'";
		}
		if (!string.IsNullOrEmpty(this.txtContractName.Text))
		{
			text = text + " and contractname like '%" + this.txtContractName.Text + "%'";
		}
		if (base.Request.QueryString["a"] != null)
		{
			text = text + " and UserCodes like '%" + base.UserCode + "%'";
		}
		return text;
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
				string text = this.gvConract.DataKeys[e.Row.RowIndex]["ContractId"].ToString();
				string text2 = this.gvConract.DataKeys[e.Row.RowIndex]["ContractName"].ToString();
				string text3 = this.gvConract.DataKeys[e.Row.RowIndex]["ContractCode"].ToString();
				string time = Common2.GetTime(this.gvConract.DataKeys[e.Row.RowIndex]["SignedTime"].ToString());
				string enPrice = WebUtil.GetEnPrice(this.gvConract.DataKeys[e.Row.RowIndex]["ContractPrice"].ToString(), text);
				e.Row.Attributes["id"] = text;
				string value = string.Format("trClick('{0}', '{1}', '{2}', '{3}', '{4}')", new object[]
				{
					text,
					text2,
					text3,
					time,
					enPrice
				});
				e.Row.Attributes.Add("onclick", value);
				e.Row.Attributes.Add("ondblclick", "trdbClick()");
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
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	public string GetFeedbackState(string contractId)
	{
		System.Collections.Generic.List<ContractPayendModel> listArray = new ContractPayendBll().GetListArray(" where contractId='" + contractId + "'");
		if (listArray.Count > 0)
		{
			return "已交底";
		}
		return "未交底";
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


