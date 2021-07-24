using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.AccounIncome.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccounIncome_AccounIncome : NBasePage, IRequiresSessionState
{
	private string accountid = string.Empty;
	private AccounIncome accountBLL = new AccounIncome();
	private string module = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.accountid = base.Request["accountid"];
			this.hfldProjectGuid.Value = this.accountid;
			this.gvwAccount.PageSize = NBasePage.pagesize4;
			this.BindGv();
			this.hfldAdjunctPath.Value = ConfigurationManager.AppSettings["AccounIncome"];
		}
	}
	protected override void OnInit(EventArgs e)
	{
		this.accountid = base.Request["accountid"];
		this.hfldProjectGuid.Value = this.accountid;
		base.OnInit(e);
	}
	protected void gvwAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwAccount.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	private void BindGv()
	{
		string text = "";
		if (!string.IsNullOrEmpty(this.accountid))
		{
			text += string.Format(" AND (select PrjGuid from dbo.Fund_Prj_Accoun where accountid='{0}') like '%'+CONVERT(VARCHAR(50),acc.PrjGuid) +'%' ", this.accountid);
			this.gvwAccount.DataSource = this.accountBLL.GetList(0, text, " GetDate desc");
			this.gvwAccount.DataBind();
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		List<string> list = new List<string>();
		if (this.HdnAccountID.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.HdnAccountID.Value);
		}
		else
		{
			list.Add(this.HdnAccountID.Value);
		}
		try
		{
			foreach (string current in list)
			{
				this.accountBLL.Delete(new Guid(current));
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
		this.BindGv();
	}
	protected void gvwAccount_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Value.ToString();
			string text;
			if ((text = e.Row.Cells[6].Text) != null)
			{
				if (text == "0")
				{
					e.Row.Cells[6].Text = "合同入账";
					return;
				}
				if (text == "1")
				{
					e.Row.Cells[6].Text = "启动资金";
					return;
				}
				if (!(text == "2"))
				{
					return;
				}
				e.Row.Cells[6].Text = "其他入账";
			}
		}
	}
}


