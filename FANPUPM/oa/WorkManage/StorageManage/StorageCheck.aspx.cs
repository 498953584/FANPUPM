using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_StorageCheck : BasePage, IRequiresSessionState
{

	public ptOfficeResStockCheckAction amAction
	{
		get
		{
			return new ptOfficeResStockCheckAction();
		}
	}
	public int DepotID
	{
		get
		{
			object obj = this.ViewState["DepotID"];
			if (obj != null)
			{
				return (int)this.ViewState["DepotID"];
			}
			return -1;
		}
		set
		{
			this.ViewState["DepotID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["dd"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;window.close();</script>");
			return;
		}
		if (base.Request["dd"].ToString() != "")
		{
			this.DepotID = int.Parse(base.Request["dd"].ToString());
		}
		if (!this.Page.IsPostBack)
		{
			if (this.DepotID == -1)
			{
				this.btnStartWF.Enabled = false;
			}
			else
			{
				this.btnStartWF.Enabled = true;
			}
			this.Bind();
		}
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.amAction.GetList("DepotID=" + this.DepotID);
		this.GVBook.DataBind();
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);ClickRow(",
				this.DepotID,
				",'",
				dataRowView["CheckRecordID"].ToString(),
				"','",
				dataRowView["IsConfirm"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.Cells[2].Text = BooksCommonClass.GetUserName(dataRowView["UserCode"].ToString());
			string a;
			if ((a = dataRowView["IsConfirm"].ToString()) != null)
			{
				if (a == "0")
				{
					e.Row.Cells[3].Text = "未确认";
					return;
				}
				if (!(a == "1"))
				{
					return;
				}
				e.Row.Cells[3].Text = "已确认";
			}
		}
	}
	protected void GVBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVBook.PageIndex = e.NewPageIndex;
		this.Bind();
	}
	private ptOfficeResStockCheck GetData()
	{
		return new ptOfficeResStockCheck
		{
			CheckRecordID = 0,
			DepotID = this.DepotID,
			IsConfirm = "0",
			RecordDate = DateTime.Now,
			UserCode = base.UserCode
		};
	}
	protected void btnStartWF_Click(object sender, EventArgs e)
	{
		ptOfficeResStockCheck data = this.GetData();
		if (this.amAction.Add(data) > 0)
		{
			this.Bind();
		}
	}
}


