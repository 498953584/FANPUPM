using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_FileTransaction : BasePage, IRequiresSessionState
{
	public OAFileLendAction amAction
	{
		get
		{
			return new OAFileLendAction();
		}
	}
	public string LibraryCode
	{
		get
		{
			object obj = this.ViewState["LIBRARYCODE"];
			if (obj != null)
			{
				return (string)this.ViewState["LIBRARYCODE"];
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["LIBRARYCODE"] = value;
		}
	}
	public string strWhere
	{
		get
		{
			object obj = this.ViewState["STRWHERE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["STRWHERE"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["lc"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.LibraryCode = base.Request["lc"].ToString();
		if (!this.Page.IsPostBack)
		{
			this.strWhere = "LibraryCode='" + this.LibraryCode + "'";
			this.Bind(this.strWhere);
		}
		this.btnLend.Attributes["onclick"] = "javascript:if(!confirm('即将进行借阅确认，请确认!')) return false;";
		this.btnReturn.Attributes["onclick"] = "javascript:if(!confirm('即将进行归还确认，请确认!')) return false;";
	}
	private void Bind(string strWhere)
	{
		this.GVBook.DataSource = this.amAction.GetList(strWhere);
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["LendState"].ToString(),
				"','",
				dataRowView["BorrowMan"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = BooksCommonClass.GetUserName(this.Session["yhdm"].ToString());
			switch (int.Parse(dataRowView["LendState"].ToString()))
			{
			case 1:
				e.Row.Cells[5].Text = "未借出";
				return;
			case 2:
				e.Row.Cells[5].Text = "已借出";
				return;
			case 3:
				e.Row.Cells[5].Text = "归还申请";
				return;
			case 4:
				e.Row.Cells[5].Text = "已归还";
				break;
			default:
				return;
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind(this.strWhere);
	}
	protected void btnBookSearch_Click(object sender, EventArgs e)
	{
		this.strWhere = "LibraryCode='" + this.LibraryCode + "'";
		if (this.DDLSearch.SelectedValue == "0")
		{
			this.strWhere = this.strWhere + " and FileName='" + this.txtotherSearch.Text + "' ";
		}
		if (this.DDLSearch.SelectedValue == "1")
		{
			this.strWhere = this.strWhere + " and BorrowMan='" + BooksCommonClass.GetUserCode(this.txtotherSearch.Text) + "' ";
		}
		this.Bind(this.strWhere);
	}
	protected void btnLend_Click(object sender, EventArgs e)
	{
		if (this.amAction.FileConfirm(int.Parse(this.HdnRecordID.Value), 2, DateTime.Now) > 0)
		{
			this.JS.Text = "alert('确认成功!');";
			this.Bind(this.strWhere);
			return;
		}
		this.JS.Text = "alert('确认失败!');";
	}
	protected void btnReturn_Click(object sender, EventArgs e)
	{
		if (this.amAction.FileConfirm(int.Parse(this.HdnRecordID.Value), 4, DateTime.Now) > 0)
		{
			this.JS.Text = "alert('确认成功!');";
			this.Bind(this.strWhere);
			return;
		}
		this.JS.Text = "alert('确认失败!');";
	}
	private SMSLog GetSMSData()
	{
		string userName = BooksCommonClass.GetUserName(this.Session["yhdm"].ToString());
		return new SMSLog
		{
			ID = 0,
			I_XGID = this.HdnRecordID.Value,
			Message = userName + "向您发送档案借阅催还信息!",
			ReceiveUser = this.HdnBorrowMan.Value,
			SendTime = DateTime.Now,
			SendUser = this.HdnBorrowMan.Value,
			V_LXBM = "007"
		};
	}
	protected void btnHasten_Click(object sender, EventArgs e)
	{
		SMSLog sMSData = this.GetSMSData();
		int num = PublicInterface.SendSmsMsg(sMSData);
		if (num > 0)
		{
			this.Page.RegisterStartupScript("", "<script>alert('档案借阅催还信息发送成功!');</script>");
			return;
		}
		this.Page.RegisterStartupScript("", "<script>alert('档案借阅催还信息发送失败!');</script>");
	}
}


