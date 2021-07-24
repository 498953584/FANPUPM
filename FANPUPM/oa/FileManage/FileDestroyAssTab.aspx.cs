using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_FileDestroyAssTab : BasePage, IRequiresSessionState
{
	private string strWhere = "";

	public OAFileDestroyAction amAction
	{
		get
		{
			return new OAFileDestroyAction();
		}
	}
	public Guid RecordCode
	{
		get
		{
			object obj = this.ViewState["RECORDCODE"];
			if (obj != null)
			{
				return (Guid)this.ViewState["RECORDCODE"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["RECORDCODE"] = value;
		}
	}
	public int Flag
	{
		get
		{
			object obj = this.ViewState["FLAG"];
			if (obj != null)
			{
				return (int)this.ViewState["FLAG"];
			}
			return 0;
		}
		set
		{
			this.ViewState["FLAG"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["ac"] == null || base.Request["ac"].ToString() == "" || base.Request["ia"] == null || base.Request["ia"].ToString() == "")
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
			return;
		}
		this.Flag = int.Parse(base.Request["ia"].ToString());
		this.RecordCode = new Guid(base.Request["ac"].ToString().Trim());
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
	}
	private void Bind()
	{
		DataTable dataSource = new DataTable();
		if (this.Flag != -3)
		{
			object obj = this.strWhere;
			this.strWhere = string.Concat(new object[]
			{
				obj,
				" DestroyRecordID= '",
				this.RecordCode,
				"' "
			});
			dataSource = this.amAction.GetList(this.strWhere);
		}
		this.GVBook.DataSource = dataSource;
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
				dataRowView["DestroyFileID"].ToString(),
				"','",
				this.Flag.ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			switch (int.Parse(dataRowView["SecretLevel"].ToString()))
			{
			case 1:
				e.Row.Cells[4].Text = "密秘";
				return;
			case 2:
				e.Row.Cells[4].Text = "机密";
				return;
			case 3:
				e.Row.Cells[4].Text = "绝密";
				break;
			default:
				return;
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
}


