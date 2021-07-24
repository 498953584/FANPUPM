using AccountManage.BLL;
using ASP;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using com.jwsoft.pm.entpm;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_fund_accountInfoList : NBasePage, IRequiresSessionState
{
	private decimal chu;
	private decimal jin;
	private decimal lastmony;
	private fund_accountInfoBLL Bll = new fund_accountInfoBLL();

	protected override void OnInit(EventArgs e)
	{
		this.gvMony.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string text = base.Request["accouid"].ToString();
			this.ViewState["accouid"] = text;
			this.lbl.Text = text;
			this.lblname.Text = this.GetAccountName(text, "0");
			int num = DateTime.Now.Month - 3;
			this.txtEndSignedTime.Text = string.Concat(new string[]
			{
				DateTime.Now.Year.ToString(),
				"-",
				DateTime.Now.Month.ToString(),
				"-",
				(DateTime.Now.Day + 1).ToString()
			});
			this.txtStartSignedTime.Text = string.Concat(new string[]
			{
				DateTime.Now.Year.ToString(),
				"-",
				num.ToString(),
				"-",
				DateTime.Now.Day.ToString()
			});
			this.BindGv(text);
		}
	}
	protected string GetAccountName(string accid, string type)
	{
		accountMange accountMange = new accountMange();
		prjaccountModel prjaccountModel = new prjaccountModel();
		prjaccountModel = accountMange.GetModelByConId(accid);
		if (prjaccountModel != null)
		{
			if (type != null)
			{
				if (type == "0")
				{
					return prjaccountModel.accountNum.ToString();
				}
				if (type == "1")
				{
					if (!(prjaccountModel.isnullify.ToString() == "0"))
					{
						return "注销";
					}
					return "正常";
				}
			}
			return prjaccountModel.acountName.ToString();
		}
		return string.Empty;
	}
	protected string GetAccountNum(string accounum)
	{
		accountMange accountMange = new accountMange();
		prjaccountModel prjaccountModel = new prjaccountModel();
		prjaccountModel = accountMange.GetModelByConId(accounum);
		if (prjaccountModel != null)
		{
			return prjaccountModel.accountNum;
		}
		return "";
	}
	public void BindGv(string accouid)
	{
		string text = "";
		text = text + " and accountNum='" + accouid + "'";
		text += ((this.txtStartSignedTime.Text.Trim() == "") ? "" : (" and opTime >= '" + Convert.ToDateTime(this.txtStartSignedTime.Text.Trim()) + "'"));
		text += ((this.txtEndSignedTime.Text.Trim() == "") ? "" : (" and opTime <= '" + Convert.ToDateTime(this.txtEndSignedTime.Text.Trim()) + "'"));
		DataTable dataTable = this.CerateTable(this.Bll.GetList(text));
		if (dataTable.Rows.Count > 0)
		{
			this.gvMony.DataSource = dataTable;
			this.gvMony.DataBind();
		}
	}
	protected DataTable CerateTable(DataTable dt)
	{
		DataTable dataTable = new DataTable();
		DataColumn column = new DataColumn("id", typeof(string));
		DataColumn column2 = new DataColumn("accountNum", typeof(string));
		DataColumn column3 = new DataColumn("opMoney1", typeof(string));
		DataColumn column4 = new DataColumn("opMoney2", typeof(string));
		DataColumn column5 = new DataColumn("opMoney3", typeof(string));
		DataColumn column6 = new DataColumn("opType", typeof(string));
		DataColumn column7 = new DataColumn("opTime", typeof(string));
		DataColumn column8 = new DataColumn("opMan", typeof(string));
		DataColumn column9 = new DataColumn("isValid", typeof(string));
		DataColumn column10 = new DataColumn("sysPeop", typeof(string));
		DataColumn column11 = new DataColumn("opClass", typeof(string));
		dataTable.Columns.Add(column);
		dataTable.Columns.Add(column2);
		dataTable.Columns.Add(column3);
		dataTable.Columns.Add(column4);
		dataTable.Columns.Add(column5);
		dataTable.Columns.Add(column6);
		dataTable.Columns.Add(column7);
		dataTable.Columns.Add(column8);
		dataTable.Columns.Add(column9);
		dataTable.Columns.Add(column10);
		dataTable.Columns.Add(column11);
		if (dt.Rows.Count > 0)
		{
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["id"] = dt.Rows[i]["id"].ToString();
				dataRow["accountNum"] = dt.Rows[i]["accountNum"].ToString();
				if (dt.Rows[i]["opClass"].ToString() == "0")
				{
					dataRow["opMoney1"] = dt.Rows[i]["opMoney"].ToString();
					dataRow["opMoney2"] = "";
				}
				else
				{
					dataRow["opMoney2"] = dt.Rows[i]["opMoney"].ToString();
					dataRow["opMoney1"] = "";
				}
				dataRow["opMoney3"] = this.GetLast(dt.Rows[i]["accountNum"].ToString(), Convert.ToDateTime(dt.Rows[i]["opTime"]).ToString("yyyy-MM-dd HH:mm:ss.fff"));
				dataRow["opType"] = dt.Rows[i]["opType"].ToString();
				dataRow["opTime"] = dt.Rows[i]["opTime"].ToString();
				dataRow["opMan"] = dt.Rows[i]["opMan"].ToString();
				dataRow["isValid"] = dt.Rows[i]["isValid"].ToString();
				dataRow["sysPeop"] = dt.Rows[i]["sysPeop"].ToString();
				dataRow["opClass"] = dt.Rows[i]["opClass"].ToString();
				if (i == dt.Rows.Count - 1)
				{
					this.hdfvalue.Value = dataRow["opMoney3"].ToString();
				}
				dataTable.Rows.Add(dataRow);
			}
		}
		return dataTable;
	}
	protected string GetLast(string accouid, string date)
	{
		DataTable list = this.Bll.GetList(string.Concat(new string[]
		{
			" and accountNum='",
			accouid,
			"' and opTime<='",
			date,
			"'"
		}));
		if (list.Rows.Count > 0)
		{
			decimal d = 0m;
			for (int i = 0; i < list.Rows.Count; i++)
			{
				if (list.Rows[i]["opClass"].ToString() == "0")
				{
					d += Convert.ToDecimal(list.Rows[i]["opMoney"].ToString());
				}
				else
				{
					d -= Convert.ToDecimal(list.Rows[i]["opMoney"].ToString());
				}
			}
			return d.ToString();
		}
		return "0";
	}
	protected string GetAccount(string accounum)
	{
		accountMange accountMange = new accountMange();
		return accountMange.GetModelByConId(accounum).accountNum;
	}
	protected string GetTypeName(string type)
	{
		if (type != null)
		{
			if (type == "0")
			{
				return "启动资金";
			}
			if (type == "1")
			{
				return "合同款";
			}
			if (type == "2")
			{
				return "拆借";
			}
			if (type == "3")
			{
				return "其它";
			}
		}
		return "启动资金";
	}
	protected string GetClassName(string classname)
	{
		if (classname != null)
		{
			if (classname == "0")
			{
				return "收";
			}
			if (classname == "1")
			{
				return "支";
			}
		}
		return "收";
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvMony.PageIndex = e.NewPageIndex;
		this.BindGv(this.ViewState["accouid"].ToString());
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv(this.ViewState["accouid"].ToString());
	}
	protected void gvMony_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		int arg_0D_0 = e.Row.RowIndex;
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			this.chu += Convert.ToDecimal((dataRowView[3].ToString() == "") ? "0" : dataRowView[3].ToString());
			this.jin += Convert.ToDecimal((dataRowView[2].ToString() == "") ? "0" : dataRowView[2].ToString());
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[2].Text = "支出" + this.chu.ToString();
			e.Row.Cells[3].Text = "转入" + this.jin.ToString();
			e.Row.Cells[4].Text = this.hdfvalue.Value.Trim();
		}
	}
	protected string GetBefore(GridView gr, int k)
	{
		decimal d = 0m;
		if (k > 0)
		{
			for (int i = 0; i < k; i++)
			{
				if (gr.Rows[i].Cells[9].ToString() == "0")
				{
					d += Convert.ToDecimal(gr.Rows[i].Cells[3].ToString());
				}
				else
				{
					d -= Convert.ToDecimal(gr.Rows[i].Cells[2].ToString());
				}
			}
			return d.ToString();
		}
		return "0";
	}
	protected string GetName(string usercode)
	{
		return PageHelper.QueryUser(this, usercode);
	}
}


