using AccountManage.BLL;
using ASP;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_AccountQuery_AccountList : NBasePage, IRequiresSessionState
{
	private decimal chu;
	private decimal jin;
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
			this.BindGv();
		}
	}
	public void BindGv()
	{
		string where = " and opMan like '%" + base.UserCode + "%' ";
		this.BindGv(this.Bll.GetDistinctNumber(this.Session["yhdm"].ToString(), where));
	}
	public void BindGv(DataTable DataSource)
	{
		this.gvMony.DataSource = DataSource;
		this.gvMony.DataBind();
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
					return prjaccountModel.acountName.ToString();
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
	protected string GetSum(string accouid)
	{
		return this.Bll.GetSum(accouid);
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
				return "进";
			}
			if (classname == "1")
			{
				return "出";
			}
		}
		return "进";
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvMony.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void gvMony_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		int arg_0D_0 = e.Row.RowIndex;
	}
	protected string GetAccount(string accounum)
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


