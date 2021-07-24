using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_BlocManage_BlocPersonDraw : BasePage, IRequiresSessionState
{

	public OAOfficeResPersonalApplicationDetailAction amAction
	{
		get
		{
			return new OAOfficeResPersonalApplicationDetailAction();
		}
	}
	public int InStorageID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (int)this.ViewState["INSTORAGEID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["INSTORAGEID"] = value;
		}
	}
	public string UsePerson
	{
		get
		{
			object obj = this.ViewState["USEPERSON"];
			if (obj != null)
			{
				return (string)this.ViewState["USEPERSON"];
			}
			return "";
		}
		set
		{
			this.ViewState["USEPERSON"] = value;
		}
	}
	public int AuditState
	{
		get
		{
			object obj = this.ViewState["AUDITSTATE"];
			if (obj != null)
			{
				return (int)this.ViewState["AUDITSTATE"];
			}
			return -1;
		}
		set
		{
			this.ViewState["AUDITSTATE"] = value;
		}
	}
	public string strWhere
	{
		get
		{
			object obj = this.ViewState["STRWHERE"];
			if (obj != null)
			{
				return (string)this.ViewState["STRWHERE"];
			}
			return "";
		}
		set
		{
			this.ViewState["STRWHERE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["id"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		if (base.Request["id"].ToString() != "")
		{
			this.InStorageID = int.Parse(base.Request["id"].ToString());
		}
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList("a.PARecordID=" + this.InStorageID);
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			string a;
			if ((a = dataRowView["UseType"].ToString()) != null)
			{
				if (a == "0")
				{
					e.Row.Cells[2].Text = "不回收";
					return;
				}
				if (!(a == "1"))
				{
					return;
				}
				e.Row.Cells[2].Text = "以旧换新";
			}
		}
	}
}


