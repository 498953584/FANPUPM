using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Fund.Account;
using cn.justwin.stockBLL.Fund.Account;
using cn.justwin.Web;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjAccount : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
	public AccountLogic AL = new AccountLogic();

	protected Guid PrjGuid
	{
		get
		{
			object obj = this.ViewState["PRJGUID"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["PRJGUID"] = value;
		}
	}
	public string plantype
	{
		get
		{
			object obj = this.ViewState["plantype"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["plantype"] = value;
		}
	}
	public string prjName
	{
		get
		{
			object obj = this.ViewState["prjName"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["prjName"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.gvBudget.PageSize = NBasePage.pagesize;
			string text = this.Session["yhdm"].ToString();
			DataTable dataTable = PersonnelAction.QueryPersonnelById(text);
			if (dataTable != null && dataTable.Rows.Count == 1)
			{
				this.Session["HumanName"] = dataTable.Rows[0]["v_xm"].ToString();
			}
			this.Session["HumanCode"] = text;
			if (base.Request["plantype"] != null)
			{
				this.plantype = base.Request["plantype"].ToString();
				this.hfplantype.Value = this.plantype;
			}
			this.BindGv();
			this.hfldAdjunctPath.Value = ConfigHelper.Get("PrjAccount");
		}
	}
	public void BindGv()
	{
		DataTable dataTable = this.AL.QueryAccount(this.txtaccountNum.Text.Trim().ToString(), this.txtacountName.Text.Trim().ToString(), base.UserCode);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dataTable.Rows[i]["PrjGuid"] == "")
			{
				dataTable.Rows.RemoveAt(i);
			}
		}
		this.gvBudget.DataSource = dataTable;
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex]["AccountID"].ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex]["AccountID"].ToString();
			e.Row.Attributes["onclick"] = "clickRows('" + this.gvBudget.DataKeys[e.Row.RowIndex]["AccountID"].ToString() + "',this)";
			DataRowView arg_ED_0 = (DataRowView)e.Row.DataItem;
			Label label = (Label)e.Row.Cells[7].FindControl("lblPrjName");
			if (label.Text.Length > 50)
			{
				label.ToolTip = label.Text;
				label.Text = label.Text.Substring(0, 49) + "...";
			}
		}
	}
	protected void btnActivation_Click(object sender, EventArgs e)
	{
		ActivityModel activityModel = new ActivityModel();
		string text = this.hfAccountID.Value.ToString();
		if (text != "")
		{
			activityModel.AccountID = text;
			if (this.HD.Value == "1")
			{
				activityModel.activeDate = null;
				activityModel.activeMan = "";
				activityModel.AccountState = new int?(2);
				activityModel.closeMan = this.Session["yhdm"].ToString();
				activityModel.closeDate = new DateTime?(DateTime.Now);
			}
			else
			{
				activityModel.activeDate = new DateTime?(DateTime.Now);
				activityModel.activeMan = this.Session["yhdm"].ToString();
				activityModel.AccountState = new int?(1);
				activityModel.closeMan = "";
				activityModel.closeDate = null;
			}
			if (this.AL.UpdateisActivity(activityModel, null))
			{
				if (this.HD.Value == "1")
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("window.location.href = '/fund/Account/PrjAccount.aspx';");
					stringBuilder.Append("alert('系统提示：\\n\\n注销成功！');");
					base.RegisterScript(stringBuilder.ToString());
				}
				else
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					stringBuilder2.Append("window.location.href = '/fund/Account/PrjAccount.aspx';");
					stringBuilder2.Append("alert('系统提示：\\n\\n激活成功！');");
				}
				this.BindGv();
				return;
			}
			base.RegisterScript("alert('系统提示：\\n\\n操作失败！');");
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		string text = this.hfldPurchaseChecked.Value.ToString();
		List<string> list = new List<string>();
		if (text.Contains("["))
		{
			list = JsonHelper.GetListFromJson(text);
		}
		else
		{
			list.Add(text);
		}
		try
		{
			foreach (string current in list)
			{
				this.AL.Delete(current);
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
		this.BindGv();
	}
	protected void btnQuery_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void gvBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvBudget.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
}


