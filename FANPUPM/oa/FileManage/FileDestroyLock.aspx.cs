using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_FileDestroyLock : BasePage, IRequiresSessionState
{

	public OAFileDestroyMainAction amAction
	{
		get
		{
			return new OAFileDestroyMainAction();
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
	public OAFileDestroyAction da
	{
		get
		{
			return new OAFileDestroyAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack && base.Request["ic"] != null)
		{
			this.RecordCode = new Guid(base.Request["ic"].ToString().Trim());
			this.BookEditDisplay();
			this.Bind();
		}
	}
	private void BookEditDisplay()
	{
		string strWhere = " RecordID='" + this.RecordCode + "'  ";
		DataTable list = this.amAction.GetList(strWhere);
		if (list.Rows.Count > 0)
		{
			DataRow dataRow = list.Rows[0];
			this.LbApplyPerson.Text = PTMultiClassesAction.GetNameOfUser(dataRow["UserCode"].ToString());
			this.LbApplyDate.Text = Convert.ToDateTime(dataRow["RecordDate"].ToString()).ToShortDateString();
		}
	}
	private void Bind()
	{
		DataTable dataSource = new DataTable();
		string strWhere = " DestroyRecordID= '" + this.RecordCode + "' ";
		dataSource = this.da.GetList(strWhere);
		this.GVBook.DataSource = dataSource;
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
}


