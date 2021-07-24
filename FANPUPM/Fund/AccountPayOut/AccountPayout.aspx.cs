using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.AccounPayout.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccountPayOut_AccountPayOut : NBasePage, IRequiresSessionState
{
	private string accountid = string.Empty;
	private AccounPayoutBLL accountBLL = new AccounPayoutBLL();
	private string module = string.Empty;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.accountid = base.Request["accountid"];
			this.hdnZHID.Value = this.accountid;
			this.gvwAccount.PageSize = NBasePage.pagesize4;
			this.hdnSub.Value = base.Request.QueryString["Sub"].ToString();
			string value;
			if ((value = this.hdnSub.Value) != null)
			{
				if (!(value == "0"))
				{
					if (value == "1")
					{
						this.WF1.BusiCode = "094";
					}
				}
				else
				{
					this.WF1.BusiCode = "098";
				}
			}
			this.BindGv();
			this.hfldAdjunctPath.Value = ConfigurationManager.AppSettings["AccountPayOut"];
		}
	}
	protected override void OnInit(EventArgs e)
	{
		this.accountid = base.Request["accountid"];
		this.hdnZHID.Value = this.accountid;
		this.hdnSub.Value = base.Request.QueryString["Sub"].ToString();
		string value;
		if ((value = this.hdnSub.Value) != null)
		{
			if (!(value == "0"))
			{
				if (value == "1")
				{
					this.WF1.BusiCode = "094";
				}
			}
			else
			{
				this.WF1.BusiCode = "098";
			}
		}
		base.OnInit(e);
	}
	protected void gvwAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwAccount.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	private void BindGv()
	{
		string text = " and Type =" + this.hdnSub.Value;
		if (!string.IsNullOrEmpty(this.accountid))
		{
			text += string.Format(" AND (select PrjGuid from dbo.Fund_Prj_Accoun where accountid='{0}') like '%'+CONVERT(VARCHAR(50),Fund_Prj_Accoun_Payout.PrjGuid) +'%' ", this.accountid);
			this.gvwAccount.DataSource = this.accountBLL.GetList(0, text, " PayOutTime desc");
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
		string value;
		if (e.Row.RowType == DataControlRowType.Header && (value = this.hdnSub.Value) != null)
		{
			if (!(value == "0"))
			{
				if (value == "1")
				{
					e.Row.Cells[3].Visible = false;
					e.Row.Cells[4].Visible = true;
				}
			}
			else
			{
				e.Row.Cells[3].Visible = true;
				e.Row.Cells[4].Visible = false;
			}
		}
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Value.ToString();
			string value2;
			if ((value2 = this.hdnSub.Value) != null)
			{
				if (value2 == "0")
				{
					e.Row.Cells[3].Visible = true;
					e.Row.Cells[4].Visible = false;
					return;
				}
				if (!(value2 == "1"))
				{
					return;
				}
				e.Row.Cells[3].Visible = false;
				e.Row.Cells[4].Visible = true;
			}
		}
	}
}


