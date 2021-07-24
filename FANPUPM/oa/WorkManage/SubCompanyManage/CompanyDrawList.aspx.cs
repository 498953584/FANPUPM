using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_SubCompanyManage_CompanyDrawList : BasePage, IRequiresSessionState
{

	public OAOfficeResDepartmentApplicationDetailAction pAction
	{
		get
		{
			return new OAOfficeResDepartmentApplicationDetailAction();
		}
	}
	public OAOfficeResApplicationCollectDetailAction amAction
	{
		get
		{
			return new OAOfficeResApplicationCollectDetailAction();
		}
	}
	public Guid RecordID
	{
		get
		{
			object obj = this.ViewState["INSTORAGEID"];
			if (obj != null)
			{
				return (Guid)this.ViewState["INSTORAGEID"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["INSTORAGEID"] = value;
		}
	}
	public Guid ICRecordID
	{
		get
		{
			object obj = this.ViewState["ICRecordID"];
			if (obj != null)
			{
				return (Guid)this.ViewState["ICRecordID"];
			}
			return Guid.NewGuid();
		}
		set
		{
			this.ViewState["ICRecordID"] = value;
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
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["ic"] == null || base.Request["id"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');returnValue=false;</script>");
			return;
		}
		if (base.Request["id"].ToString() != "")
		{
			this.RecordID = new Guid(base.Request["id"].ToString());
		}
		if (base.Request["ic"].ToString() != "")
		{
			this.ICRecordID = new Guid(base.Request["ic"].ToString());
		}
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
	}
	private void Bind()
	{
		this.GVBook.DataSource = this.pAction.GetList("a.DARecordID='" + this.RecordID + "'");
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
				"OnRecord(this);ClickRow('",
				dataRowView["DADRecordID"].ToString(),
				"','",
				dataRowView["DARecordID"].ToString(),
				"','",
				this.AuditState,
				"');"
			});
			string a;
			if ((a = dataRowView["UseType"].ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (a == "1")
					{
						e.Row.Cells[2].Text = "以旧换新";
					}
				}
				else
				{
					e.Row.Cells[2].Text = "不回收";
				}
			}
		}
		switch (e.Row.RowType)
		{
		case DataControlRowType.Header:
		case DataControlRowType.DataRow:
			e.Row.Cells[6].Style["display"] = "none";
			break;
		case DataControlRowType.Footer:
			break;
		default:
			return;
		}
	}
	private ArrayList GetData()
	{
		ArrayList arrayList = new ArrayList();
		foreach (GridViewRow gridViewRow in this.GVBook.Rows)
		{
			HtmlInputCheckBox htmlInputCheckBox = (HtmlInputCheckBox)gridViewRow.Cells[0].FindControl("chkSub");
			if (htmlInputCheckBox.Checked)
			{
				arrayList.Add(new OAOfficeResApplicationCollectDetail
				{
					ACDetailID = 0,
					ACRecordID = this.ICRecordID,
					ApplyNumber = (gridViewRow.Cells[5].Text.Trim() == "") ? 0 : int.Parse(gridViewRow.Cells[5].Text.Trim()),
					MaterialID = (gridViewRow.Cells[6].Text.Trim() == "") ? 0 : int.Parse(gridViewRow.Cells[6].Text.Trim())
				});
			}
		}
		return arrayList;
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		ArrayList data = this.GetData();
		if (this.amAction.Add(data) > 0)
		{
			this.pAction.Update(this.RecordID, this.ICRecordID);
			this.Page.RegisterStartupScript("", "<script>alert('添加成功!');returnValue=true;window.close();</script>");
		}
	}
}


